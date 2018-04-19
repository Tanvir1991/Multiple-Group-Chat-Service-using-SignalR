namespace chatapp.Migrations
{
    using chatapp.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<chatapp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(chatapp.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (!context.Roles.Any(rol => rol.Name == "Admin"))
            {
                var result = roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(rol => rol.Name == "Member"))
            {
                var result = roleManager.Create(new IdentityRole { Name = "Member" });
            }
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (!context.Users.Any(rol => rol.UserName == "tanvir@gmail.com"))
            {
                var user = new ApplicationUser { UserName = "tanvir@gmail.com", PhoneNumber = "01685793893" };
                var result = UserManager.Create(user, "tanvir@123");
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }
            List<UserAccess> lst = new List<UserAccess>
            { 
                new UserAccess {RoleName = "Admin",ActionName = "Create",ControllerName = "GroupInfos",MenuItem = "Create"},
                new UserAccess {RoleName = "Admin",ActionName = "Index",ControllerName = "GroupInfos",MenuItem = "View Group"},
                new UserAccess {RoleName = "Member",ActionName = "Chat",ControllerName = "Home",MenuItem = "Chat"},
              
            };
            lst.ForEach(s => context.UserAccesss.Add(s));
            context.SaveChanges();





        }
    }
}
