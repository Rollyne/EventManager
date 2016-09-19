using EventManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Services.Data
{
    interface ICommentService
    {
        void AddComment(int eventId, string content);

        void EditComment(int commentId, string content);

        void DeleteComment(Comment comment);

        IList<Comment> CommentsForThisEvent(int eventId);
    }
}
