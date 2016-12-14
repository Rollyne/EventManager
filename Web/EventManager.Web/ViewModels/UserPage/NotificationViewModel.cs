using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.UserPage
{
    public class NotificationViewModel
    {
        public int PendingsCount { get; set; }

        public IList<Event> PendingEvents { get; set; }

        public IList<User> PendingUsers { get; set; }
    }

    public class User
    {
        public int ConnectionId { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string PhotoPath { get; set; }
    }

    public class Event
    {
        public int ConnectionId { get; set; }

        public int Id { get; set; }

        public string InvitorName { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}