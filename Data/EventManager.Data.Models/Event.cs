using System.Collections.Generic;
using EventManager.Data.Common.Models;
using System;
using System.IO;
using System.Web;

namespace EventManager.Data.Models
{
    public class Event : BaseModel<int>
    {
        public Event()
        {
            this.Dates = new HashSet<EventDate>();
            this.Comments = new HashSet<Comment>();
        }

        public virtual ApplicationUser Creator { get; set; }

        public string Destination { get; set; }

        public string Content { get; set; }

        private readonly string eventFilePath = HttpContext.Current.Server.MapPath("~\\Images\\ApplicationImages\\EventImages");

        public string ImagesFilePath
        {
            get
            {
                var path = Path.Combine(this.eventFilePath, this.Id.ToString());
                Directory.CreateDirectory(path);
                return path;
            }
        }

        public virtual ICollection<EventDate> Dates { get; set; }

        public virtual DateTime? OptimalStartDate { get; set; }

        public virtual DateTime? OptimalEndDate { get; set; }

        public virtual DateTime? StartEventDate { get; set; }

        public virtual DateTime? EndEventDate { get; set; }

        public int? EventLength { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
