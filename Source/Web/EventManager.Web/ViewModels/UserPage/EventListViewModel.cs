using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.UserPage
{
    public class EventListViewModel
    {
        public IList<EventViewModel> OwnedEvents { get; set; }

        public IList<EventViewModel> NotOwnedEvents { get; set; }
    }
}