using EventManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EventManager.Services.Data
{
    public interface IEventService
    {
        void CreateEvent(string destination);

        void EditEvent(int eventId, string content, string destination);

        void DeleteEvent(Event _event);

        void AddImage(int eventId, HttpPostedFileBase image);

        void DeleteImage(int eventId, string imageName);

        IList<string> ImageFilePaths(int eventId);

        IList<Event> AllEvents();

        IList<Event> OwnedEvents();

        IList<Event> NotOwnedEvents();

        IList<Event> FindEventByDestination(string destination);

        void AddUser(int eventId, ApplicationUser user);

        void RemoveUser(int eventId, ApplicationUser user);

    }
}
