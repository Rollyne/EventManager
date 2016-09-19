using System.Collections.Generic;
using EventManager.Data.Common.Models;
using System;
using System.IO;

namespace EventManager.Data.Models
{
    public class Event : BaseModel<int>
    {
        public Event()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Dates = new HashSet<EventDate>();
            this.Comments = new HashSet<Comment>();
        }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public string Destination { get; set; }

        public string Content { get; set; }

        private readonly string eventFilePath = Path.Combine(Environment.CurrentDirectory, @"Images\EventImages\");

        public string ImagesFilePath
        {
            get
            {
                var path = Path.Combine(this.eventFilePath, this.Id.ToString());
                return path;
            }
        }

        public virtual ICollection<EventDate> Dates { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
