using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using StoreManagment.Models;

[assembly: OwinStartupAttribute(typeof(StoreManagment.Startup))]
namespace StoreManagment
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AddRoleAndUser();
        }

        public void AddRoleAndUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            IdentityRole role;
            ApplicationUser user;


            // Crete Roles

            if (!roleManager.RoleExists("Admins"))
            {
                role = new IdentityRole();
                role.Name = "Admins";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Customers"))
            {
                role = new IdentityRole();
                role.Name = "Customers";
                roleManager.Create(role);
            }




            // create user and Add to role of Admins
            user = new ApplicationUser();

            user.UserName = "Daif";
            user.Email = "di.hamdan@gmail.com";

            var check = userManager.Create(user, "@Qwe12");
            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admins");
            }












        }
    }
}
