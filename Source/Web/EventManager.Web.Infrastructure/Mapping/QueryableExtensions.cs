namespace EventManager.Web.Infrastructure.Mapping
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper.QueryableExtensions;
    using AutoMapper;
    //using System.Collections.Generic;
    //using System.Collections;

    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            //return source.AsQueryable().ProjectTo(AutoMapperConfig.Configuration, membersToExpand).ToList();
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}
