﻿using AutoMapper;
using EventManager.Services.Data;
using EventManager.Web.Infrastructure.Mapping;
using EventManager.Web.ViewModels.UserPage;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManager.Web.Controllers
{
    [Authorize]
    public class UserPageController : BaseController
    {
        private readonly IEventService events;
        private readonly IUserService user;

        public UserPageController(IEventService events, IUserService user)
        {
            this.events = events;
            this.user = user;
            this.ViewBag.Name = this.user.CurrentUserName();
            this.ViewBag.PhotoPath = "../../Images/ApplicationImages/UserImages/" + this.user.CurrentUserId() + "/Photo.png";
        }

        // GET: UserPage
        [HttpGet]
        public ActionResult UserProfile()
        {
            Directory.CreateDirectory(this.Server.MapPath("~/Images/ApplicationImages/UserImages/") + User.Identity.GetUserId().ToString());
            if (!System.IO.File.Exists(this.Server.MapPath("~/Images/ApplicationImages/UserImages/" + User.Identity.GetUserId().ToString() + "/Photo.png")))
            {
                System.IO.File.Copy(this.Server.MapPath("~/Images/ApplicationImages/UserImages/NoPhoto.png"), this.Server.MapPath("~/Images/ApplicationImages/UserImages/" + User.Identity.GetUserId().ToString() + "/Photo.png"));
            }

            var ownedEvents = this.events.OwnedEvents(this.user.CurrentUserId());
            //var ownedEventsView = this.Mapper.Map<IList<EventViewModel>>(ownedEvents);

            IList<EventViewModel> ownedEventsView = new List<EventViewModel>();

            foreach (var item in ownedEvents)
            {
                ownedEventsView.Add(new EventViewModel
                {
                    Id = item.Id,
                    Destination = item.Destination,
                    Content = item.Content,
                    StartEventDate = (DateTime)item.StartEventDate
                });
            }

            foreach (var item in ownedEventsView)
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

            var userDetails = new UserVewModel();
            userDetails.UserName = this.user.CurrentUserName();
            userDetails.Description = this.user.UserDescription();

            var photoPath = "../../Images/ApplicationImages/UserImages/" + User.Identity.GetUserId().ToString() + "/Photo.png";

            userDetails.PhotoFileName = photoPath;

            photoPath = "../../Images/ApplicationImages/UserImages/" + User.Identity.GetUserId().ToString() + "/Banner.png";
            if (!Directory.EnumerateFiles(Server.MapPath("~/Images/ApplicationImages/UserImages/" + User.Identity.GetUserId().ToString())).Any(x => x.Contains("Banner")))
            {
                photoPath = null;
            }


            userDetails.BannerFileName = photoPath;
            userDetails.CreatedEvents = this.events.OwnedEvents(this.user.CurrentUserId()).Count;
            userDetails.AttendedEvents = this.events.NotOwnedEvents(this.user.CurrentUserId()).Count;
            userDetails.FriendsCount = this.user.FriendsByName(User.Identity.GetUserId().ToString()).Count;

            var model = new UserPageViewModel
            {
                User = userDetails,
                Events = ownedEventsView
            };

            return this.View(model);
        }

        [HttpGet]
        public ActionResult ChangeUserInfo()
        {

            var model = new ChangeUserInfoViewModel();
            model.UserName = this.user.CurrentUserName();
            model.Description = this.user.UserDescription();
            model.Email = this.user.Email();
            model.PhotoPath = "../../Images/ApplicationImages/UserImages/" + this.User.Identity.GetUserId().ToString() + "/Photo.png";
            model.BannerPath = "../../Images/ApplicationImages/UserImages/" + this.User.Identity.GetUserId().ToString() + "/Banner.png";

            return this.View(model);
        }

        [HttpPost]
        public ActionResult ChangeUserInfo(ChangeUserInfoViewModel model)
        {
            this.user.ChangeProfileInfo(model.UserName, model.Description);
            this.user.UploadPhoto(model.Photo, model.Banner);

            return this.RedirectToAction("UserProfile");
        }

        [HttpPost]
        public ActionResult ChangeEmail(string email)
        {
            if (email != null)
            {
                this.user.ChangeEmail(email);
            }

            return this.RedirectToAction("UserProfile");
        }


        [HttpGet]
        public ActionResult FriendList()
        {
            var model = new FriendListViewModel();

            var friends = this.user.FriendsByName(User.Identity.GetUserId());

            model.Friends = new List<Friend>();
            foreach (var item in friends)
            {
                model.Friends.Add(new Friend
                {
                    Id = item.Id,
                    UserName = item.Name,
                    PhotoFileName = "../../Images/ApplicationImages/UserImages/" + item.Id + "/Photo.png",
                    AttendedEvents = this.events.NotOwnedEvents(item.Id).Count(),
                    CreatedEvents = this.events.OwnedEvents(item.Id).Count(),
                    FriendsCount = this.user.FriendsByName(item.Id).Count()
                });
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult EventList()
        {
            var ownedEvents = this.events.OwnedEvents(this.user.CurrentUserId());
            //var ownedEventsView = this.Mapper.Map<IList<EventViewModel>>(ownedEvents);

            IList<EventViewModel> ownedEventsView = new List<EventViewModel>();

            foreach (var item in ownedEvents)
            {
                ownedEventsView.Add(new EventViewModel
                {
                    Id = item.Id,
                    Destination = item.Destination,
                    Content = item.Content,
                    StartEventDate = (DateTime)item.StartEventDate
                });
            }

            foreach (var item in ownedEventsView)
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

            var notOwnedEvents = this.events.NotOwnedEvents(this.user.CurrentUserId());
            //var notOwnedEventsView = this.Mapper.Map<IList<EventViewModel>>(notOwnedEvents);

            IList<EventViewModel> notOwnedEventsView = new List<EventViewModel>();

            foreach (var item in notOwnedEvents)
            {
                notOwnedEventsView.Add(new EventViewModel
                {
                    Id = item.Id,
                    Destination = item.Destination,
                    Content = item.Content,
                    StartEventDate = (DateTime)item.StartEventDate
                });
            }

            foreach (var item in notOwnedEventsView)
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

            var model = new EventListViewModel();

            model.OwnedEvents = ownedEventsView;
            model.NotOwnedEvents = notOwnedEventsView;

            return this.View(model);
        }

        [HttpGet]
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
                    PhotoFileName = "../../Images/ApplicationImages/UserImages/" + item.Id + "/Photo.png",
                    AttendedEvents = this.events.NotOwnedEvents(item.Id).Count(),
                    CreatedEvents = this.events.OwnedEvents(item.Id).Count(),
                    FriendsCount = this.user.FriendsByName(this.user.CurrentUserId()).Count()
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
                    PhotoFileName = "../../Images/ApplicationImages/UserImages/" + item.Id + "/Photo.png",
                    AttendedEvents = this.events.NotOwnedEvents(item.Id).Count(),
                    CreatedEvents = this.events.OwnedEvents(item.Id).Count(),
                    FriendsCount = this.user.FriendsByName(item.Id).Count()
                });
            }

            var userId = this.user.CurrentUserId();
            var events = this.events.FindEventByDestination(search, userId);

            //var eventsView = this.Mapper.Map<IList<EventViewModel>>(events);

            IList<EventViewModel> eventsView = new List<EventViewModel>();

            foreach (var item in events)
            {
                eventsView.Add(new EventViewModel
                {
                    Id = item.Id,
                    Destination = item.Destination,
                    Content = item.Content,
                    StartEventDate = (DateTime)item.StartEventDate
                });
            }

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

            return this.View(model);
        }

        [HttpPost]
        public ActionResult AddFriend(string userId)
        {
            this.user.AddFriend(userId);

            return this.RedirectToAction("UserProfile");
        }

        [HttpPost]
        public ActionResult AcceptFriend(string userId)
        {
            this.user.AcceptFriend(userId);

            return this.RedirectToAction("UserProfile");
        }

        [HttpPost]
        public ActionResult RemoveFriend(string userId)
        {
            this.user.RemoveFriend(userId);

            return this.RedirectToAction("UserProfile");
        }

        [HttpGet]
        public ActionResult UserDetails(string userId)
        {
            var user = this.user.User(userId);

            var model = new UserVewModel();
            model.UserName = user.Name;
            model.Description = user.Description;

            Directory.CreateDirectory(this.Server.MapPath("~/Images/ApplicationImages/UserImages/") + user.Id.ToString());

            var photoPath = "../../Images/ApplicationImages/UserImages/" + user.Id.ToString() + "/Photo.png";

            model.PhotoFileName = photoPath;

            photoPath = "../../Images/ApplicationImages/UserImages/" + user.Id.ToString() + "/Banner.png";
            if (!Directory.EnumerateFiles(this.Server.MapPath("~/Images/ApplicationImages/UserImages/" + user.Id.ToString())).Any(x => x.Contains("Banner")))
            {
                photoPath = null;
            }

            model.BannerFileName = photoPath;
            model.CreatedEvents = this.events.OwnedEvents(user.Id).Count();
            model.AttendedEvents = this.events.NotOwnedEvents(user.Id).Count();
            model.FriendsCount = this.user.FriendsByName(user.Id).Count();

            return this.View(model);
        }
    }
}
