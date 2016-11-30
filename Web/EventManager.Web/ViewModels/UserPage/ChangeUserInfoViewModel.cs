using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.UserPage
{
    public class ChangeUserInfoViewModel
    {
        public HttpPostedFileBase Photo { get; set; }

        public string PhotoPath { get; set; }

        public HttpPostedFileBase Banner { get; set; }

        public string BannerPath { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }
    }
}