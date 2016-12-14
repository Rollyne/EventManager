using EventManager.Services.Data;
using EventManager.Web.ViewModels.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManager.Web.Controllers
{
    public class AjaxController : BaseController
    {
        private readonly IEventService events;
        private readonly IUserService user;

        public AjaxController(IEventService events, IUserService user)
        {
            this.events = events;
            this.user = user;
        }

        // GET: Ajax
        [HttpPost]
        public ActionResult SearchResult(string search)
        {
            var model = new SearchViewModel();

            var users = this.user.FindUserByName(search).Where(x => x.Id != this.user.CurrentUserId() && !this.user.FriendsByName(this.user.CurrentUserId()).Where(y => x.Id == y.Id).Any()).ToList();

            model.Users = new List<Friend>();
            foreach (var item in users)
            {
                model.Users.Add(new Friend
                {
                    Id = item.Id,
                    UserName = item.Name,
                    PhotoFileName = "../../Images/ApplicationImages/UserImages/" + item.Id + "/Photo.png"
                });
            }

            var friends = this.user.FindFriendByName(search).Where(x => x.Id != this.user.CurrentUserId()).ToList();

            model.Friends = new List<Friend>();
            foreach (var item in friends)
            {
                model.Friends.Add(new Friend
                {
                    Id = item.Id,
                    UserName = item.Name,
                    PhotoFileName = "../../Images/ApplicationImages/UserImages/" + item.Id + "/Photo.png"
                });
            }

            var userId = this.user.CurrentUserId();
            var events = this.events.FindEventByDestination(search, userId);

            var eventsView = this.Mapper.Map<IList<EventViewModel>>(events);

            foreach (var item in eventsView)
            {
                item.ImagesFilePath = this.events.ImageFilePaths(item.Id).Where(x => !x.Contains("Banner123.jpg")).FirstOrDefault();
                if (item.ImagesFilePath != null)
                {
                    item.ImagesFilePath = item.ImagesFilePath.Replace('\\', '/');
                    item.ImagesFilePath = "../.." + item.ImagesFilePath.Remove(0, item.ImagesFilePath.IndexOf(@"/Images/"));
                }
                else
                {
                    item.ImagesFilePath = "../../Images/ApplicationImages/EventImages/NoImage.png";
                }
            }

            model.Events = eventsView;

            return this.PartialView("_SearchResult", model);
        }

        public ActionResult Notifications()
        {
            var model = new NotificationViewModel();

            model.PendingsCount = this.events.PendingEvents().Count() + this.user.PendingUsers().Count();

            foreach (var item in this.events.PendingEvents())
            {
                var imagePath = this.events.ImageFilePaths(item.Event.Id).Where(x => !x.Contains("Banner123.jpg")).FirstOrDefault();

                if (imagePath != null)
                {
                    imagePath = imagePath.Replace('\\', '/');
                    imagePath = "../.." + imagePath.Remove(0, imagePath.IndexOf(@"/Images/"));
                }
                else
                {
                    imagePath = "../../Images/ApplicationImages/EventImages/NoImage.png";
                }

                model.PendingEvents.Add(new Event
                {
                    ConnectionId = item.Id,
                    InvitorName = item.User.Name,
                    Id = item.Event.Id,
                    Name = item.Event.Destination,
                    ImagePath = imagePath
                });
            }

            foreach (var item in this.user.PendingUsers())
            {
                var user = item.Receiver.Id == this.user.CurrentUserId() ? item.Sender : item.Receiver;

                model.PendingUsers.Add(new User
                {
                    ConnectionId = item.Id,
                    Id = user.Id,
                    Name = user.Name,
                    PhotoPath = "../../Images/ApplicationImages/UserImages/" + user.Id + "/Photo.png"
                });
            }

            return this.PartialView("_NotificationResult", model);
        }
    }
}