namespace Abruple.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<PhotoMasterDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        // SEED
        protected override void Seed(PhotoMasterDbContext context)
        {
            // ADMIN ROLE
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleCreateResult = roleManager.Create(new IdentityRole("Admin"));

                if (!roleCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", roleCreateResult.Errors));
                }
            }

            // ADMIN
            if (!(context.Users.Any(u => u.UserName == "admin")))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var userToInsert = new User { UserName = "admin", Email = "admin@admin.com" };
                var userCreate = userManager.Create(userToInsert, "123123");

                if (!userCreate.Succeeded)
                {
                    throw new Exception(string.Join("; ", userCreate.Errors));
                }

                var addAdminRoleResult = userManager.AddToRole(userToInsert.Id, "Admin");

                if (!addAdminRoleResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
                }
            }

            // CONTENTS
            if (!context.Contests.Any() && context.Users.Any(u => u.UserName == "admin"))
            {
                var admin = context.Users.First(u => u.UserName == "admin");

                for (int i = 1; i <= 100; i++)
                {
                    context.Contests.AddOrUpdate(c => c.Title,
                       new Contest()
                       {
                           Title = "Conttest " + i,
                           Creator = admin,
                           CreatorId = admin.Id,
                           CreatedOn = DateTime.UtcNow.AddHours(i),
                           Description = "Contest generated by SEED",
                       }
                    );
                }
            }
        }
    }
}
