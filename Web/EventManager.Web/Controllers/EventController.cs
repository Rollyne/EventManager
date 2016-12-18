using EventManager.Services.Data;
using EventManager.Web.ViewModels.CrEvent;
using EventManager.Web.ViewModels.EventDetails;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManager.Web.Controllers
{
    [Authorize]
    public class EventController : BaseController
    {
        private readonly IUserService user;
        private readonly IEventService _event;
        private readonly IDateService date;
        private readonly ICommentService comment;

        public EventController(IUserService user, IEventService _event, IDateService date, ICommentService comment)
        {
            this.user = user;
            this._event = _event;
            this.date = date;
            this.comment = comment;

            this.ViewBag.Name = this.user.CurrentUserName();
            this.ViewBag.PhotoPath = "../../Images/ApplicationImages/UserImages/" + this.user.CurrentUserId() + "/Photo.png";
        }

        // GET: Event/CreateEvent
        [HttpGet]
        public ActionResult CreateEvent()
        {
            return this.View(new CreateEventViewModel());
        }

        // POST: Event/CreateEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(CreateEventViewModel model)
        {
            _event.CreateEvent(model.Destination, model.Content, model.GetStartEventDate, model.GetEndEventDate, model.EventLength, model.Images, model.BannerImage);

            return this.RedirectToAction("UserProfile", "UserPage");
        }

        [HttpGet]
        public ActionResult Event(int id)
        {
            var currentEvent = this._event.FindEventById(id);

            var model = new EventViewModel();

            model.Id = currentEvent.Id;
            model.Name = currentEvent.Destination;
            model.Description = currentEvent.Content;

            model.ImagesAndNames = new List<ImagesAndNames>();
            foreach (var item in this._event.ImageFilePaths(id))
            {

                var imagePath = item.Replace('\\', '/');
                imagePath = "../.." + imagePath.Remove(0, imagePath.IndexOf(@"/Images/"));

                var slashIndex = item.LastIndexOf("\\");
                var dotIndex = item.LastIndexOf(".");
                var length = dotIndex - slashIndex;

                if (item.Substring(slashIndex + 1, length - 1) == "Banner123")
                {
                    model.Banner = imagePath;
                }
                else
                {
                    var imageName = item.Substring(slashIndex + 1, length - 1);
                    Guid newGuid;
                    if (Guid.TryParse(imageName, out newGuid))
                    {
                        imageName = string.Empty;
                    }

                    model.ImagesAndNames.Add(new ImagesAndNames
                    {
                        ImageName = imageName.Replace("_", " "),
                        ImagePath = imagePath

                    });
                }
            }

            if (currentEvent.Creator.Id == this.user.CurrentUserId())
            {
                model.IsCreator = true;
            }
            else
            {
                model.IsCreator = false;
            }

            var myDate = this.date.DatesForThisEvent(id).Where(x => x.Creator.Id == this.user.CurrentUserId()).FirstOrDefault();
            if (myDate != null)
            {
                model.IsDateAdded = true;
                model.MyDateId = myDate.Id;
            }
            else
            {
                model.IsDateAdded = false;
            }

            model.AttendersCount = this._event.EventAttenders(id).Count();
            model.Attenders = new List<Attenders>();
            foreach (var item in this._event.EventAttenders(id))
            {
                model.Attenders.Add(new Attenders { Name = item.Name, PhotoPath = "../../Images/ApplicationImages/UserImages/" + item.Id + "/Photo.png" });
            }

            model.EventStartDate = currentEvent.StartEventDate;
            model.EventEndDate = currentEvent.EndEventDate;
            model.EventLength = currentEvent.EventLength;

            model.BestStartDate = currentEvent.OptimalStartDate;
            model.BestEndDate = currentEvent.OptimalEndDate;

            var myDates = this.date.DatesForThisEvent(id).Where(x => x.Creator.Id == this.user.CurrentUserId()).FirstOrDefault();
            if (myDates != null)
            {
                model.MyStartDate = myDates.StartDate;
                model.MyEndDate = myDates.EndDate;
            }

            model.CurrUrsPic = "../../Images/ApplicationImages/UserImages/" + this.user.CurrentUserId() + "/Photo.png";

            model.Comments = new List<Comments>();

            foreach (var item in currentEvent.Comments)
            {
                model.Comments.Add(new Comments {
                    ComentatorName = item.Creator.Name,
                    ComentatorPhoto = "../../Images/ApplicationImages/UserImages/" + item.Creator.Id + "/Photo.png",
                    Comment = item.Content,
                    CommentDate = item.CreatedOn
                });
            }

            model.DatesPath = "../../Images/ApplicationImages/EventImages/" + currentEvent.Id + "/Dates.json";

            return this.View(model);
        }

        [HttpPost]
        public ActionResult AddDate(int id, string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return this.RedirectToAction("Event", new { id = id });
            }

            var perseDate = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);
            this.date.AddDate(id, perseDate);

            _event.CalculateEventTime(id);

            return this.RedirectToAction("Event", new { id = id });
        }

        [HttpPost]
        public ActionResult AddComment(int id, string comment)
        {
            this.comment.AddComment(id, comment);

            return this.RedirectToAction("Event", new { id = id });
        }

        [HttpPost]
        public ActionResult AddAttender(int id, string name)
        {
            var user = this.user.FindFriendByName(name).FirstOrDefault();
            if (user != null)
            {
                _event.AddUser(id, user);
            }

            return this.RedirectToAction("Event", new { id = id });
        }

        [HttpPost]
        public ActionResult AcceptEvent(int eventId)
        {
            var userId = this.user.CurrentUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                _event.UserAccept(eventId, userId);
                _event.CalculateEventTime(eventId);
            }

            return this.RedirectToAction("Event", new { id = eventId });
        }

        [HttpPost]
        public ActionResult DeclineEvent(int eventId)
        {
            var userId = this.user.CurrentUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                _event.RemoveUser(eventId, userId);
                _event.CalculateEventTime(eventId);
            }

            return this.RedirectToAction("UserProfile", "UserPage");
        }

        [HttpGet]
        public ActionResult EditEvent(int id)
        {
            var currentEvent = this._event.FindEventById(id);

            var model = new CreateEventViewModel();

            model.Id = id;
            model.Destination = currentEvent.Destination;
            model.Content = currentEvent.Content;

            model.Images = new List<ImageAndTitle>();
            foreach (var item in this._event.ImageFilePaths(id))
            {

                var imagePath = item.Replace('\\', '/');
                imagePath = "../.." + imagePath.Remove(0, imagePath.IndexOf(@"/Images/"));

                var slashIndex = item.LastIndexOf("\\");
                var dotIndex = item.LastIndexOf(".");
                var length = dotIndex - slashIndex;

                if (item.Substring(slashIndex + 1, length - 1) == "Banner123")
                {
                    model.BannerPath = imagePath;
                }
                else
                {
                    var imageName = item.Substring(slashIndex + 1, length - 1);
                    Guid newGuid;
                    if (Guid.TryParse(imageName, out newGuid))
                    {
                        imageName = string.Empty;
                    }

                    model.Images.Add(new ImageAndTitle
                    {
                        Title = imageName.Replace("_", " "),
                        OldTitle = imageName.Replace("_", " "),
                        ImagePath = imagePath

                    });
                }
            }

            model.StartEventDate = ((DateTime)currentEvent.StartEventDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.EndEventDate = ((DateTime)currentEvent.EndEventDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.EventLength = currentEvent.EventLength;

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent(CreateEventViewModel model)
        {
            var images = this._event.ImageFilePaths(model.Id);

            for (int i = 0; i < model.Images.Count(); i++)
            {
                if (i < images.Count())
                {
                    var imagePath = images[i].Replace('\\', '/');
                    imagePath = "../.." + imagePath.Remove(0, imagePath.IndexOf(@"/Images/"));

                    if (images[i].Contains("\\Banner123.png"))
                    {
                        model.BannerPath = imagePath;
                        images.RemoveAt(i);
                    }

                    var slashIndex = images[i].LastIndexOf("\\");
                    var dotIndex = images[i].LastIndexOf(".");
                    var length = dotIndex - slashIndex;

                    var imageName = images[i].Substring(slashIndex + 1, length - 1);
                    Guid newGuid;
                    if (Guid.TryParse(imageName, out newGuid))
                    {
                        imageName = string.Empty;
                    }

                    var image = model.Images[i];

                    image.OldTitle = imageName.Replace("_", " ");
                    image.ImagePath = imagePath;
                }
            }

            this._event.EditEvent(model.Id, model.Destination, model.Content, model.GetStartEventDate, model.GetEndEventDate, model.EventLength, model.Images, model.BannerImage);

            return this.RedirectToAction("Event", new { id = model.Id });
        }
    }
}
