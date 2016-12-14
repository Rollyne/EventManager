using EventManager.Data.Models;
using EventManager.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.CrEvent
{
    public class CreateEventViewModel : IMapFrom<Event>
    {
        public IList<ImageAndTitle> Images { get; set; }

        public CreateEventViewModel()
        {
            this.Images = new List<ImageAndTitle>();
            this.StartEventDate = null;
            this.EndEventDate = null;
        }

        public int Id { get; set; }

        public string Destination { get; set; }

        public string Content { get; set; }

        private DateTime? startEventDate;
        public string StartEventDate
        {
            get
            {
                return startEventDate.ToString();
            }
            set
            {
                if (value == null)
                {
                    startEventDate = null;
                }
                else
                {
                    startEventDate = DateTime.ParseExact(value, "M/d/yyyy", CultureInfo.InvariantCulture);
                }
                
            }
        }

        public DateTime? GetStartEventDate
        {
            get
            {
                return startEventDate;
            }
        }

        private DateTime? endEventDate;
        public string EndEventDate
        {
            get
            {
                return endEventDate.ToString();
            }
            set
            {
                if (value == null)
                {
                    endEventDate = null;
                }
                else
                {
                    endEventDate = DateTime.ParseExact(value, "M/d/yyyy", CultureInfo.InvariantCulture);
                }
            }
        }

        public DateTime? GetEndEventDate
        {
            get
            {
                return endEventDate;
            }
        }

        public int? EventLength { get; set; }

        public HttpPostedFileBase BannerImage { get; set; }

        public string BannerPath { get; set; }
    }
}