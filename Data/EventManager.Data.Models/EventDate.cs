using EventManager.Data.Common.Models;
using System;

namespace EventManager.Data.Models
{
    public class EventDate : BaseModel<int>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Event Event { get; set; }

        public virtual ApplicationUser Creator { get; set; }
    }
}