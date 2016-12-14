using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Data.Models;
using EventManager.Data.Common;
using System.Web;
using System.IO;
using EventManager.Tools.Helpers;
using Newtonsoft.Json;
using EventManager.Web.ViewModels.CrEvent;

namespace EventManager.Services.Data
{
    public class EventService : IEventService
    {
        private readonly IDbRepository<Event> events;
        private readonly IDbRepository<UserEvent> usersEvents;
        private readonly IUserRepository<ApplicationUser> users;
        private readonly IDateService dates;

        public EventService(
            IDbRepository<Event> events,
            IUserRepository<ApplicationUser> users,
            IDateService dates,
            IDbRepository<UserEvent> usersEvents)
        {
            this.events = events;
            this.users = users;
            this.dates = dates;
            this.usersEvents = usersEvents;
        }

        public void AddImage(int eventId, HttpPostedFileBase image)
        {
            var _event = this.events.GetById(eventId);
            var imageName = string.Format("{0}.jpg", Guid.NewGuid());
            var imageFullPath = Path.Combine(_event.ImagesFilePath, imageName);

            if (image != null)
            {
                image.SaveAs(imageFullPath);
            }
        }

        public void AddUser(int eventId, ApplicationUser user)
        {
            var _event = this.events.GetById(eventId);
            if (!usersEvents.All().Where(x => x.Event.Id == eventId && x.User.Id == user.Id).Any())
            {
                usersEvents.Add(new UserEvent { User = user, Event = _event, Status = true });
                usersEvents.Save();
            }
        }

        public void UserAccept(int eventId, ApplicationUser user)
        {

            var userEvent = usersEvents.All().Where(x => x.Event.Id == eventId && x.User.Id == user.Id && x.Status == false).FirstOrDefault();
            if (userEvent != null)
            {
                userEvent.Status = true;

                usersEvents.Edit(userEvent);
                usersEvents.Save();
            }
        }

        public IList<Event> AllEvents(string userId)
        {
            //var allEvents = this.users.GetCurrentUser().Events.ToList();
            var allEvents = this.usersEvents.All().Where(x => x.User.Id == userId).Select(x => x.Event).ToList();

            return allEvents;
        }

        public void CalculateEventTime(int eventId)
        {
            var eventDates = this.dates.DatesForThisEvent(eventId);
            List<FreePeople> peopleCount = new List<FreePeople>();

            foreach (var eventDate in eventDates)
            {
                var days = eventDate.EndDate - eventDate.StartDate;

                for (int i = 0; i <= days.Days; i++)
                {
                    var date = eventDate.StartDate.AddDays(i);

                    if (peopleCount.Where(x => x.Date == date).Any())
                    {
                        var index = peopleCount.FindIndex(x => x.Date == date);
                        peopleCount[index].FreePeopleCount++;
                    }
                    else
                    {
                        peopleCount.Add(new FreePeople
                        {
                            Date = eventDate.StartDate.AddDays(i),
                            //DaysCount = 1,
                            FreePeopleCount = 1
                        });
                    }
                }
            }

            peopleCount = peopleCount.OrderByDescending(x => x.FreePeopleCount).ThenBy(x => x.Date).ToList();

            var currentEvent = this.events.GetById(eventId);

            var structuredJson = new
            {
                MaxPeople = this.usersEvents.All().Where(x => x.Event.Id == eventId).Count() + 1, // TODO: Implement function for max people count;
                DateAfter = new
                {
                    Day = currentEvent.StartEventDate.Value.Day,
                    Month = currentEvent.StartEventDate.Value.Month,
                    Year = currentEvent.StartEventDate.Value.Year
                },
                DateBefore = new
                {
                    Day = currentEvent.EndEventDate.Value.Day,
                    Month = currentEvent.EndEventDate.Value.Month,
                    Year = currentEvent.EndEventDate.Value.Year
                },
                EventLength = currentEvent.EventLength,
                Dates = peopleCount.Select(x => new { Day = x.Date.Day, Month = x.Date.Month, Year = x.Date.Year, x.FreePeopleCount }).ToList()
            };

            //var structuredJson = peopleCount.Select(x => new { Day = x.Date.Day, Month = x.Date.Month, Year = x.Date.Year, x.FreePeopleCount }).ToList();
            var json = JsonConvert.SerializeObject(structuredJson, Formatting.Indented);

            using (StreamWriter file = new StreamWriter(Path.Combine(currentEvent.ImagesFilePath, "Dates.json")))
            {
                file.WriteLine(json);
            }

            if (peopleCount.Count > 0)
            {
                DateTime startDate = peopleCount[0].Date;
                //DateTime endDate;

                //int j = 0;
                //do
                //{
                //    endDate = peopleCount[j].Date;
                //    j++;
                //}
                //while (peopleCount[j - 1].Date.AddDays(1) == peopleCount[j].Date && peopleCount[0].FreePeopleCount == peopleCount[j].FreePeopleCount);

                currentEvent.OptimalStartDate = startDate;
                currentEvent.OptimalEndDate = startDate.AddDays((int)currentEvent.EventLength);
            }
            else
            {
                DateTime startDate = (DateTime)currentEvent.StartEventDate;

                currentEvent.OptimalStartDate = startDate;
                currentEvent.OptimalEndDate = startDate.AddDays((int)currentEvent.EventLength);
            }

            this.events.Edit(currentEvent);
            this.events.Save();
        }

        public void CreateEvent(string destination, string content, DateTime? startDate, DateTime? endDate, int? length, IList<ImageAndTitle> images, HttpPostedFileBase bannerImage)
        {

            var _event = new Event
            {
                Destination = destination,
                Creator = this.users.GetCurrentUser(),
                StartEventDate = startDate,
                EndEventDate = endDate,
                EventLength = length
            };

            if (!string.IsNullOrWhiteSpace(content))
            {
                _event.Content = content;
            }

            this.events.Add(_event);
            this.events.Save();


            this.CalculateEventTime(_event.Id);

            var bannerName = string.Format("{0}.png", "Banner123");
            var bannerFullPath = Path.Combine(_event.ImagesFilePath, bannerName);
            if (bannerImage != null)
            {
                bannerImage.SaveAs(bannerFullPath);
            }

            foreach (var item in images)
            {
                string imageName;
                if (string.IsNullOrEmpty(item.Title))
                {
                    imageName = string.Format("{0}.png", Guid.NewGuid().ToString());
                }
                else
                {
                    imageName = string.Format("{0}.png", item.Title.Replace(" ", "_"));
                }

                var imageFullPath = Path.Combine(_event.ImagesFilePath, imageName);
                if (item.Image != null)
                {
                    item.Image.SaveAs(imageFullPath);
                }
            }

            var user = this.users.GetCurrentUser();
            this.AddUser(_event.Id, user);

            //var connection = this.usersEvents.All().Where(x => x.Event.Creator.Id == user.Id && x.User.Id == user.Id && x.Status == false).FirstOrDefault();

            //connection.Status = true;

            //this.usersEvents.Edit(connection);
            //this.usersEvents.Save();
        }

        public void DeleteEvent(Event _event)
        {
            this.events.HardDelete(_event);
        }

        public void DeleteImage(int eventId, string imageName)
        {
            var _event = this.events.GetById(eventId);
            var imageFullPath = Path.Combine(_event.ImagesFilePath, imageName);

            if(File.Exists(imageFullPath))
            {
                File.Delete(imageFullPath);
            }
        }

        public void EditEvent(int eventId, string destination, string content, DateTime? startDate, DateTime? endDate, int? length, IList<ImageAndTitle> images, HttpPostedFileBase bannerImage)
        {

            var _event = this.events.GetById(eventId);

            _event.Destination = destination;
            _event.StartEventDate = startDate;
            _event.EndEventDate = endDate;
            _event.EventLength = length;

            if (!string.IsNullOrWhiteSpace(content))
            {
                _event.Content = content;
            }

            this.events.Edit(_event);
            this.events.Save();

            this.CalculateEventTime(_event.Id);

            if (bannerImage != null)
            {
                this.DeleteImage(eventId, "Banner123.png");
                var bannerName = string.Format("{0}.png", "Banner123");
                var bannerFullPath = Path.Combine(_event.ImagesFilePath, bannerName);
                bannerImage.SaveAs(bannerFullPath);
            }

            foreach (var item in images)
            {
                if (item.Image != null)
                {
                    if (!string.IsNullOrEmpty(item.ImagePath))
                    {
                        string oldTitle = string.IsNullOrEmpty(item.OldTitle) ? item.ImagePath.Substring(item.OldTitle.LastIndexOf('/')) : string.Format("{0}.png", item.OldTitle.Replace(" ", "_"));
                        this.DeleteImage(eventId, oldTitle);
                    }

                    string imageName;
                    if (string.IsNullOrEmpty(item.Title))
                    {
                        imageName = string.Format("{0}.png", Guid.NewGuid().ToString());
                    }
                    else
                    {
                        imageName = string.Format("{0}.png", item.Title.Replace(" ", "_"));
                    }

                    var imageFullPath = Path.Combine(_event.ImagesFilePath, imageName);
                    if (item.Image != null)
                    {
                        item.Image.SaveAs(imageFullPath);
                    }
                }
            }
        }

        public IList<Event> FindEventByDestination(string destination, string userId)
        {
            // x => x.Destination.Contains(destination)
            var eventsByDest = this.AllEvents(userId).Where(x => x.Destination == destination).ToList();

            return eventsByDest;
        }

        public Event FindEventById(int id)
        {
            // x => x.Destination.Contains(destination)
            var eventById = this.events.All().Where(x => x.Id == id).FirstOrDefault();

            return eventById;
        }

        public IList<string> ImageFilePaths(int eventId)
        {
            var _event = this.events.GetById(eventId);
            var imagePaths = Directory.GetFiles(_event.ImagesFilePath).Where(x => !x.Contains("Dates.json")).ToList();

            return imagePaths;
        }

        public IList<Event> NotOwnedEvents(string userId)
        {
            var notOwnedEvents = this.usersEvents.All().Where(x => x.User.Id == userId && x.Event.Creator.Id != userId && x.Status == true);

            return notOwnedEvents.Select(x => x.Event).ToList();
        }

        public IList<Event> OwnedEvents(string userId)
        {
            var ownedEvents = this.usersEvents.All().Where(x => x.Event.Creator.Id == userId);

            return ownedEvents.Select(x => x.Event).ToList();
        }

        public void RemoveUser(int eventId, ApplicationUser user)
        {
            var userEvent = usersEvents.All().Where(x => x.Event.Id == eventId && x.User.Id == user.Id).FirstOrDefault();
            if (userEvent != null)
            {
                usersEvents.Delete(userEvent);
                usersEvents.Save();
            }
        }

        public IList<UserEvent> PendingEvents()
        {
            var currentUser = this.users.GetCurrentUser();

            var pendingEvents = this.usersEvents.All().Where(x => x.User.Id == currentUser.Id && x.Status == false);

            return pendingEvents.ToList();
        }
    }
}
