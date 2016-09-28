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

namespace EventManager.Services.Data
{
    public class EventService : IEventService
    {
        private readonly IDbRepository<Event> events;
        private readonly IUserRepository<ApplicationUser> users;
        private readonly IDateService dates;

        public EventService(
            IDbRepository<Event> events,
            IUserRepository<ApplicationUser> users,
            IDateService dates)
        {
            this.events = events;
            this.users = users;
            this.dates = dates;
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
            if (!_event.Users.Contains(user))
            {
                _event.Users.Add(user);
                this.events.Edit(_event);
                this.events.Save();
            }
        }

        public IList<Event> AllEvents()
        {
            var allEvents = this.users.GetCurrentUser().Events.ToList();

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
                    if (peopleCount.Where(x => x.Date == eventDate.StartDate.AddDays(i)).Any())
                    {
                        var index = peopleCount.FindIndex(x => x.Date == eventDate.StartDate.AddDays(i));
                        peopleCount[index].FreePeopleCount++;
                    }
                    else
                    {
                        peopleCount.Add(new FreePeople
                        {
                            Date = eventDate.StartDate.AddDays(i),
                            DaysCount = 1,
                            FreePeopleCount = 1
                        });
                    }
                }
            }
            peopleCount = peopleCount.OrderByDescending(x => x.FreePeopleCount).ThenBy(x => x.Date).ToList();

            var startDate = peopleCount[0].Date;
            DateTime endDate;

            int j = 0;
            do
            {
                endDate = peopleCount[j].Date;
                j++;
            }
            while (peopleCount[j - 1].Date.AddDays(1) == peopleCount[j].Date && peopleCount[0].FreePeopleCount == peopleCount[j].FreePeopleCount);

            var _event = this.events.GetById(eventId);

            _event.StartEventDate = startDate;
            _event.EndEventDate = endDate;

            this.events.Edit(_event);
            this.events.Save();
        }

        public void CreateEvent(string destination)
        {
            var _event = new Event { Destination = destination };
            this.events.Add(_event);
            this.events.Save();
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

        public void EditEvent(int eventId, string content, string destination)
        {
            var _event = this.events.GetById(eventId);
            if (!string.IsNullOrWhiteSpace(destination))
            {
                _event.Destination = destination;
            }
            _event.Content = content;
            this.events.Edit(_event);
            this.events.Save();
        }

        public IList<Event> FindEventByDestination(string destination)
        {
            // x => x.Destination.Contains(destination)
            var eventsByDest = this.AllEvents().Where(x => x.Destination == destination).ToList();

            return eventsByDest;
        }

        public IList<string> ImageFilePaths(int eventId)
        {
            var _event = this.events.GetById(eventId);
            var imagePaths = Directory.GetFiles(_event.ImagesFilePath).ToList();

            return imagePaths;
        }

        public IList<Event> NotOwnedEvents()
        {
            var allEvents = this.AllEvents();
            var currentUser = this.users.GetCurrentUser();
            var notOwnedEvents = allEvents.Where(x => x.Creator != currentUser).ToList();

            return notOwnedEvents;
        }

        public IList<Event> OwnedEvents()
        {
            var allEvents = this.AllEvents();
            var currentUser = this.users.GetCurrentUser();
            var ownedEvents = allEvents.Where(x => x.Creator == currentUser).ToList();

            return ownedEvents;
        }

        public void RemoveUser(int eventId, ApplicationUser user)
        {
            var _event = this.events.GetById(eventId);
            if (_event.Users.Contains(user))
            {
                _event.Users.Remove(user);
                this.events.Edit(_event);
                this.events.Save();
            }
        }
    }
}
