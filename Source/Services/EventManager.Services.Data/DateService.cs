using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Data.Models;
using EventManager.Data.Common;

namespace EventManager.Services.Data
{
    class DateService : IDateService
    {
        private readonly IDbRepository<EventDate> dates;
        private readonly IDbRepository<Event> events;
        private readonly IUserRepository<ApplicationUser> users;

        public DateService(
            IDbRepository<Event> events,
            IUserRepository<ApplicationUser> users,
            IDbRepository<EventDate> dates)
        {
            this.dates = dates;
            this.events = events;
            this.users = users;
        }

        public void AddDate(int eventId, DateTime date)
        {
            var _event = this.events.GetById(eventId);

            var eventDate = new EventDate
            {
                Date = date,
                Event = _event,
                Creator = this.users.GetCurrentUser()
            };

            this.dates.Add(eventDate);
            this.dates.Save();
        }

        public IList<EventDate> DatesForThisEvent(int eventId)
        {
            var _event = this.events.GetById(eventId);
            var dates = _event.Dates.OrderBy(x => x.CreatedOn).ToList();

            return dates;
        }

        public void DeleteDate(EventDate eventDate)
        {
            this.dates.HardDelete(eventDate);
            this.dates.Save();
        }

        public void EditDate(int dateId, DateTime date)
        {
            var eventDate = new EventDate
            {
                Id = dateId,
                Date = date
            };

            this.dates.Edit(eventDate);
            this.dates.Save();
        }
    }
}
