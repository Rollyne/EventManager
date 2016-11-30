using AutoMapper;
using EventManager.Data.Models;
using EventManager.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.UserPage
{
    public class EventViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Destination { get; set; }

        public string Content { get; set; }

        public string ImagesFilePath { get; set; }

        public DateTime StartEventDate { get; set; }
    }
}