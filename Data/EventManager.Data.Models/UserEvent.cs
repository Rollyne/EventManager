using EventManager.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Data.Models
{
    public class UserEvent : BaseModel<int>
    {

        public virtual ApplicationUser User { get; set; }

        public virtual Event Event { get; set; }

        public bool Status { get; set; } = false;
    }
}
