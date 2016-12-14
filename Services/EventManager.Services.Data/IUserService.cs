using EventManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EventManager.Services.Data
{
    public interface IUserService
    {
        IList<ApplicationUser> FindUserByName(string name);

        string CurrentUserName();

        string CurrentUserId();

        string UserDescription();

        ApplicationUser User(string userId);

        void AddFriend(string userId);

        void AcceptFriend(string senderId);

        void RemoveFriend(string userId);

        void UploadPhoto(HttpPostedFileBase photo, HttpPostedFileBase banner);

        void RemovePhoto();

        string Email();

        void ChangeEmail(string newEmail);

        string GetUserPath();

        void ChangeProfileInfo(string userName, string description);

        IList<ApplicationUser> FriendsByName(string userId);

        IList<ApplicationUser> FindFriendByName(string name);

        IList<Friends> PendingUsers();
    }
}
