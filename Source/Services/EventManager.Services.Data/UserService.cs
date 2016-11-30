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

        public UserService(IUserRepository<ApplicationUser> users)
        {
            this.users = users;
        }

        public void AddFriend(string userId)
        {
            var newUser = this.users.GetAllUsers().Where(x => x.Id == userId).FirstOrDefault();
            var currentUser = this.users.GetCurrentUser();
            var userFriends = currentUser.Friends;
            userFriends.Add(newUser);
            //currentUser.FriendsId.Add(newUser.Id);
            newUser.Friends.Add(currentUser);
            //newUser.FriendsId.Add(currentUser.Id);

            this.users.Edit(currentUser);
            this.users.Edit(newUser);
            this.users.Save();

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
            var userFriendsId = this.users.GetCurrentUser().FriendsId;
            var allUsers = this.users.GetAllUsers();
            var userFriends = new List<ApplicationUser>();

            foreach (var item in userFriendsId)
            {
                userFriends.Add(allUsers.Where(x => x.Id == item).FirstOrDefault());
            }

            // x => x.UserName.Contains(name)
            var searchResult = userFriends.Where(x => x.Name == name).ToList();

            return searchResult;
        }

        public IList<ApplicationUser> FindUserByName(string name)
        {
            var allUsers = this.users.GetAllUsers();

            // x => x.UserName.Contains(name)
            var searchResult = allUsers.Where(x => x.Name == name).ToList();

            return searchResult;
        }

        public IList<ApplicationUser> FriendsByName()
        {
            var currentUser = this.users.GetCurrentUser();
            var userFriends = currentUser.Friends.OrderBy(x => x.Name).ToList();

            return userFriends;
        }

        public void RemoveFriend(string userId)
        {
            var currentUser = this.users.GetCurrentUser();
            var friendUser = currentUser.Friends.Where(x => x.Id == userId).FirstOrDefault();
            var userFriends = currentUser.Friends;
            userFriends.Remove(friendUser);
            friendUser.Friends.Remove(currentUser);

            this.users.Edit(currentUser);
            this.users.Edit(friendUser);
            this.users.Save();
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

        public ApplicationUser User(string userId)
        {
            var user = this.users.GetAllUsers().Where(x => x.Id == userId).FirstOrDefault();

            return user;
        }
    }
}
