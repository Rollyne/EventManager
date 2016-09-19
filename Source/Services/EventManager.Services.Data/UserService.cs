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

        public void AddFriend(ApplicationUser user)
        {
            var currentUser = this.users.GetCurrentUser();
            var userFriends = currentUser.Friends;
            userFriends.Add(user);

            this.users.Edit(currentUser);
            this.users.Save();

        }

        public void UploadPhoto(HttpPostedFileBase photo)
        {
            var currentUser = this.users.GetCurrentUser();
            var photoName = string.Format("{0}.jpg", currentUser.Id);
            string path = Path.Combine(Environment.CurrentDirectory, @"Images\UserPhotos\", photoName);

            if (photo != null)
            {
                photo.SaveAs(path);
            }

            currentUser.PhotoFileName = path;

            this.users.Edit(currentUser);
            this.users.Save();
        }

        public IList<ApplicationUser> FindFriendByName(string name)
        {
            var userFriends = this.users.GetCurrentUser().Friends;

            // x => x.UserName.Contains(name)
            var searchResult = userFriends.Where(x => x.UserName == name).ToList();

            return searchResult;
        }

        public IList<ApplicationUser> FindUserByName(string name)
        {
            var allUsers = this.users.GetAllUsers();

            // x => x.UserName.Contains(name)
            var searchResult = allUsers.Where(x => x.UserName == name).ToList();

            return searchResult;
        }

        public IList<ApplicationUser> FriendsByName()
        {
            var currentUser = this.users.GetCurrentUser();
            var userFriends = currentUser.Friends.OrderBy(x => x.UserName).ToList();

            return userFriends;
        }

        public void RemoveFriend(ApplicationUser user)
        {
            var currentUser = this.users.GetCurrentUser();
            var userFriends = currentUser.Friends;
            userFriends.Remove(user);

            this.users.Edit(currentUser);
            this.users.Save();
        }

        public void RemovePhoto()
        {
            var currentUser = this.users.GetCurrentUser();
            var photoFilePath = currentUser.PhotoFileName;

            if (File.Exists(photoFilePath))
            {
                File.Delete(photoFilePath);
            }

            currentUser.PhotoFileName = string.Empty;

            this.users.Edit(currentUser);
            this.users.Save();
        }
    }
}
