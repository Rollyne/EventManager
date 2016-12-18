using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Data.Models;
using EventManager.Data.Common;
using System.Web;
using System.IO;

namespace EventManager.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<ApplicationUser> users;
        private readonly IDbRepository<Friends> friends;

        public UserService(IUserRepository<ApplicationUser> users, IDbRepository<Friends> friends)
        {
            this.users = users;
            this.friends = friends;
        }

        public void AddFriend(string userId)
        {
            var newUser = this.users.GetAllUsers().Where(x => x.Id == userId).FirstOrDefault();
            var currentUser = this.users.GetCurrentUser();
            this.friends.Add(new Friends { Sender = currentUser, Receiver = newUser });
            this.friends.Save();
        }

        public void AcceptFriend(string senderId)
        {
            var currentUser = this.users.GetCurrentUser();

            var friend = this.friends.All().Where(x => x.Receiver.Id == currentUser.Id && x.Sender.Id == senderId).FirstOrDefault();
            friend.Status = true;

            this.friends.Edit(friend);
            this.friends.Save();
        }

        public void UploadPhoto(HttpPostedFileBase photo, HttpPostedFileBase banner)
        {
            var currentUser = this.users.GetCurrentUser();
            var photoName = "Photo.png";
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"Images\ApplicationImages\UserImages\", currentUser.Id.ToString());
            Directory.CreateDirectory(path);

            if (photo != null)
            {
                photo.SaveAs(Path.Combine(path, photoName));
            }
            else
            {
                //this.RemovePhoto();
            }

            if (banner != null)
            {
                banner.SaveAs(Path.Combine(path, "Banner.png"));
            }
            else
            {
                //File.Delete(Path.Combine(path, "Banner.png"));
            }
        }

        public IList<ApplicationUser> FindFriendByName(string name)
        {
            var currentUser = this.users.GetCurrentUser();

            var recFriends = this.friends.All().Where(x => x.Receiver.Id == currentUser.Id && x.Sender.Name.ToLower().Contains(name.ToLower()) && x.Status == true).Select(x => x.Sender);
            var sendFriends = this.friends.All().Where(x => x.Sender.Id == currentUser.Id && x.Receiver.Name.ToLower().Contains(name.ToLower()) && x.Status == true).Select(x => x.Receiver);

            var userFriends = recFriends.Concat(sendFriends).ToList();

            return userFriends;
        }

        public IList<ApplicationUser> FindUserByName(string name)
        {
            var allUsers = this.users.GetAllUsers();

            // x => x.UserName.Contains(name)
            var searchResult = allUsers.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

            return searchResult;
        }

        public IList<ApplicationUser> FriendsByName(string userId)
        {
            var recFriends = this.friends.All().Where(x => x.Receiver.Id == userId && x.Status == true).Select(x => x.Sender);
            var sendFriends = this.friends.All().Where(x => x.Sender.Id == userId && x.Status == true).Select(x => x.Receiver);

            var userFriends = recFriends.Concat(sendFriends).ToList();
            userFriends = userFriends.OrderBy(x => x.Name).ToList();

            return userFriends;
        }

        public void RemoveFriend(string userId)
        {
            var currentUser = this.users.GetCurrentUser();

            var recFriends = this.friends.All().Where(x => x.Receiver.Id == currentUser.Id && x.Sender.Id == userId);
            var sendFriends = this.friends.All().Where(x => x.Sender.Id == currentUser.Id && x.Receiver.Id == userId);

            var userFriend = recFriends.Concat(sendFriends).FirstOrDefault();

            this.friends.HardDelete(userFriend);
            this.friends.Save();
        }

        public void RemovePhoto()
        {
            var currentUser = this.users.GetCurrentUser();
            var photoFilePath = currentUser.PhotoFilePath;

            if (File.Exists(Path.Combine(photoFilePath, "Photo.png")))
            {
                File.Delete(Path.Combine(photoFilePath, "Photo.png"));
                File.Copy(Path.Combine(HttpContext.Current.Server.MapPath("~"), @"Images\ApplicationImages\UserImages\NoPhoto.png"), Path.Combine(HttpContext.Current.Server.MapPath("~"), @"Images\ApplicationImages\UserImages\", currentUser.Id.ToString(), "Photo.png"), true);
            }
        }

        public string CurrentUserName()
        {
            var user = this.users.GetCurrentUser();
            if (user == null)
            {
                return string.Empty;
            }

            return user.Name;
        }

        public void ChangeProfileInfo(string userName, string description)
        {
            var currentUser = this.users.GetCurrentUser();

            if (userName != null)
            {
                currentUser.Name = userName;
            }

            if (description != null)
            {
                currentUser.Description = description;
            }

            this.users.Edit(currentUser);
            this.users.Save();
        }

        public string UserDescription()
        {
            var description = this.users.GetCurrentUser().Description;

            return description;
        }

        public string GetUserPath()
        {
            var path = this.users.GetCurrentUser().PhotoFilePath;

            path = path.Replace("\\", "/");

            return path;
        }

        public string CurrentUserId()
        {
            var user = this.users.GetCurrentUser();
            if (user == null)
            {
                return string.Empty;
            }

            return user.Id;
        }

        public void ChangeEmail(string newEmail)
        {
            var currentUser = this.users.GetCurrentUser();
            currentUser.Email = newEmail;
            currentUser.UserName = newEmail;

            this.users.Edit(currentUser);
            this.users.Save();
        }

        public string Email()
        {
            var currentUser = this.users.GetCurrentUser();

            return currentUser.Email;
        }

        // TODO: To delete this very bad function and implement logic only that I need!!!
        public ApplicationUser User(string userId)
        {
            var user = this.users.GetAllUsers().Where(x => x.Id == userId).FirstOrDefault();

            return user;
        }

        public IList<Friends> PendingUsers()
        {
            var currentUser = this.users.GetCurrentUser();

            var recFriends = this.friends.All().Where(x => x.Receiver.Id == currentUser.Id && x.Status == false);

            return recFriends.ToList();
        }
    }
}
