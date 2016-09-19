using EventManager.Data.Common.Models;

namespace EventManager.Data.Models
{
    public class Comment : BaseModel<int>
    {
        public virtual ApplicationUser Creator { get; set; }

        public virtual Event Event { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
