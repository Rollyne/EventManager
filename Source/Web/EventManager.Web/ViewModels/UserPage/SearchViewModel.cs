using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.UserPage
{
    public class SearchViewModel
    {
        public IList<Friend> Users { get; set; }

        public IList<EventViewModel> Events { get; set; }

        public IList<Friend> Friends { get; set; }
    }
}