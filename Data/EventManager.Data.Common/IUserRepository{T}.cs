using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Data.Common
{
    public interface IUserRepository<T>
        where T : IdentityUser
    {
        T GetCurrentUser();

        IList<T> GetAllUsers();

        void Save();

        void Edit(T user);
    }
}
