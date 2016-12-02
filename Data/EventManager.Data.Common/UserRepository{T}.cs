using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace EventManager.Data.Common
{
    public class UserRepository<T> : IUserRepository<T>
        where T : IdentityUser
    {
        public UserRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private IDbSet<T> DbSet { set; get; }

        private DbContext Context { set; get; }

        public T GetCurrentUser()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = this.DbSet.Where(x => x.Id == userId).FirstOrDefault();

            return user;
        }

        public IList<T> GetAllUsers()
        {
            var users = this.DbSet.ToList();

            return users;
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public void Edit(T user)
        {
            this.Context.Entry(user).State = EntityState.Modified;
        }
    }
}
