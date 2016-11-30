using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.UserPage
{
    public class FriendListViewModel
    {
        public IList<Friend> Friends { get; set; }
    }

    public class Friend
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string PhotoFileName { get; set; }

        public int CreatedEvents { get; set; }

        public int AttendedEvents { get; set; }

        public int FriendsCount { get; set; }
    }
}