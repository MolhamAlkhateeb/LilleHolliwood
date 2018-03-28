namespace LilleHaboov.Migrations
{
    using LilleHaboov.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LilleHaboov.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LilleHaboov.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }


            if (!context.Users.Any(u => u.UserName == "Bjorn@Bjorn.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "Bjorn@Bjorn.com",
                    Email = "Bjorn@Bjorn.com",
                    Cart = new Cart()
                };

                manager.Create(user, "Haboov!");
                manager.AddToRole(user.Id, "Admin");

                var id = context.Users.Where(u => u.UserName == user.UserName).FirstOrDefault().Id;

                context.AdminPanels.AddOrUpdate(p => p.UserID, new AdminPanel { UserID = id });


            }
        }
    }
}
