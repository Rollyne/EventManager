using EventManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Services.Data
{
    public interface IDateService
    {
        void AddDate(int eventId, DateTime date);

        void EditDate(int dateId, DateTime date);

        void DeleteDate(EventDate date);

        IList<EventDate> DatesForThisEvent(int eventId);
    }
}
