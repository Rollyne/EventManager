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

        void AddFriend(ApplicationUser user);

        void RemoveFriend(ApplicationUser user);

        void UploadPhoto(HttpPostedFileBase photo);

        void RemovePhoto();

        IList<ApplicationUser> FriendsByName();

        IList<ApplicationUser> FindFriendByName(string name);
    }
}
