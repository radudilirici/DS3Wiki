using DS3Wiki.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(DS3Wiki.Startup))]
namespace DS3Wiki
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            SeedUserRolesAndAdministrator();
        }

        private void SeedUserRolesAndAdministrator()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("User"))
            {
                var newRole = new IdentityRole();
                newRole.Name = "User";
                roleManager.Create(newRole);
            }

            if (!roleManager.RoleExists("Admin"))
            {
                var newRole = new IdentityRole();
                newRole.Name = "Admin";
                roleManager.Create(newRole);

                var newUser = new ApplicationUser();
                newUser.UserName = "admin@test.com";
                newUser.Email = "admin@test.com";

                var result = userManager.Create(newUser, "Semestru1Naspa!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(newUser.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Contributor"))
            {
                var newRole = new IdentityRole();
                newRole.Name = "Contributor";
                roleManager.Create(newRole);

                var newUser = new ApplicationUser();
                newUser.UserName = "contributor@test.com";
                newUser.Email = "contributor@test.com";

                var result = userManager.Create(newUser, "Semestru1Naspa!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(newUser.Id, "Contributor");
                }
            }
        }
    }


}