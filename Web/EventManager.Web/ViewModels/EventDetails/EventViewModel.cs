using EventManager.Data.Models;
using EventManager.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.EventDetails
{
    public class EventViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string CurrUrsPic { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Banner { get; set; }

        public IList<ImagesAndNames> ImagesAndNames { get; set; }

        public string DatesPath { get; set; }

        public bool IsCreator { get; set; }

        public bool IsDateAdded { get; set; }

        public int? MyDateId { get; set; }

        public int AttendersCount { get; set; }

        public IList<Attenders> Attenders { get; set; }

        public DateTime? EventStartDate { get; set; }

        public DateTime? EventEndDate { get; set; }

        public int? EventLength { get; set; }

        public DateTime? BestStartDate { get; set; }

        public DateTime? BestEndDate { get; set; }

        public DateTime? MyStartDate { get; set; }

        public DateTime? MyEndDate { get; set; }

        public IList<Comments> Comments { get; set; }

    }

    public class Comments
    {
        public string ComentatorName { get; set; }

        public string Comment { get; set; }

        public string ComentatorPhoto { get; set; }

        public DateTime CommentDate { get; set; }
    }

    public class Attenders
    {
        public string Name { get; set; }

        public string PhotoPath { get; set; }
    }

    public class ImagesAndNames
    {
        public string ImageName { get; set; }

        public string ImagePath { get; set; }
    }
}