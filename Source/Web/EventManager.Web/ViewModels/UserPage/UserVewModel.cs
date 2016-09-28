using AutoMapper;
using EventManager.Data.Models;
using EventManager.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Web.ViewModels.UserPage
{
    public class UserVewModel : IMapFrom<ApplicationUser> //, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public string PhotoFileName { get; set; }

        //public IList<ApplicationUser> Friends { get; set; }

        //public void CreateMappings(IMapperConfiguration configuration)
        //{
        //    configuration.CreateMap<ApplicationUser, UserVewModel>()
        //        .ForMember(x => x.Friends, opt => opt.MapFrom(x => x.Friends.ToList()));
        //}
    }
}