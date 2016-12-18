namespace EventManager.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using EventManager.Common;
    using EventManager.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            const string AdministratorUserName = "user@user.com";
            const string AdministratorPassword = "user12";

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser { UserName = AdministratorUserName, Email = AdministratorUserName, Name = "User", Id = "c977f032-4a71-48d3-bb4e-aacf93e3e120" };
            userManager.Create(user, AdministratorPassword);

            //if (!context.Roles.Any())
            //{
            //    // Create admin role
            //    var roleStore = new RoleStore<IdentityRole>(context);
            //    var roleManager = new RoleManager<IdentityRole>(roleStore);
            //    var role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
            //    roleManager.Create(role);

            //    // Create admin user
            //    var userStore = new UserStore<ApplicationUser>(context);
            //    var userManager = new UserManager<ApplicationUser>(userStore);
            //    var user = new ApplicationUser { UserName = AdministratorUserName, Email = AdministratorUserName, Name = "User" };
            //    userManager.Create(user, AdministratorPassword);

            //    // Assign user to admin role
            //    userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
            //}
        }
    }
}
