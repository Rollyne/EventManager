using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.UserPage
{
    public class UserPageViewModel
    {
        public UserVewModel User { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; }
    }
}