using EventManager.Data.Common;
using EventManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Services.Data
{
    class CommentService : ICommentService
    {
        private readonly IDbRepository<Comment> comments;
        private readonly IDbRepository<Event> events;
        private readonly IUserRepository<ApplicationUser> users;

        public CommentService(
            IDbRepository<Event> events,
            IUserRepository<ApplicationUser> users,
            IDbRepository<Comment> comments)
        {
            this.comments = comments;
            this.events = events;
            this.users = users;
        }

        public void AddComment(int eventId, string content)
        {
            var _event = this.events.GetById(eventId);

            var comment = new Comment
            {
                Content = content,
                Event = _event,
                Creator = this.users.GetCurrentUser()
            };

            this.comments.Add(comment);
            this.comments.Save();
        }

        public IList<Comment> CommentsForThisEvent(int eventId)
        {
            var _event = this.events.GetById(eventId);
            var comments = _event.Comments.OrderByDescending(x => x.CreatedOn).ToList();

            return comments;
        }

        public void DeleteComment(Comment comment)
        {
            this.comments.HardDelete(comment);
            this.comments.Save();
        }

        public void EditComment(int commentId, string content)
        {

            var comment = new Comment
            {
                Id = commentId,
                Content = content
            };

            this.comments.Edit(comment);
            this.comments.Save();
        }
    }
}
