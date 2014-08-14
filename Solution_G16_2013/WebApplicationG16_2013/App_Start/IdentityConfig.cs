using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApplicationG16_2013.Models;


namespace WebApplicationG16_2013
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // You want to create a new database if the Model changes
    // public class MyDbInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>

    //public class UserIdentityDbInitializer : DropCreateDatabaseAlways<UserIdentityDbContext>
    //public class UserIdentityDbInitializer :DropCreateDatabaseIfModelChanges<UserIdentityDbContext>
    //{
        
    //    protected override void Seed(UserIdentityDbContext context)
    //    {

    //        InitializeIdentityForEF(context);
    //        base.Seed(context);
    //    }
    //    private void InitializeIdentityForEF(UserIdentityDbContext context)
    //    {

    //        var userManager = new UserManager(context);
    //        var roleManager = new RoleManager(context);

    //        string roleAdmin = "Admin";
    //        string adminUserName = "stephenchoe";
    //        string adminPassWord = "bonds25";
    //        string adminEmail = "opmart2008@yahoo.com.tw";
          

    //        string roleMember = "Member";
    //        string memberUserName = "TestMember";
    //        string memberPassWord = "abc123";
    //        string memberEmail = "stephen.chung@concords.com.tw";
           

    //        //Create Role "Member"
    //        if (!roleManager.RoleExists(roleMember))
    //        {               
    //            roleManager.Create(new IdentityRole(roleMember));
    //        }

    //        // Create User "TestMember"
    //        var user = new ApplicationUser() { UserName = memberUserName,
    //                                           Email=  memberEmail                                                 
    //                                         };
    //        user.Profile = new UserInfo()
    //                                {
    //                                    RealName = "何金銀",
    //                                    NickName = "阿銀"
    //                                };

            
           

           
    //        IdentityResult result = userManager.Create(user, memberPassWord);
    //        if (result.Succeeded)
    //        {
    //            userManager.AddToRole(user.Id, roleMember);
    //        }
         


    //        //Create Role Admin if it does not exist
    //        if (!roleManager.RoleExists(roleAdmin))
    //        {
    //            roleManager.Create(new IdentityRole(roleAdmin));
    //        }

    //        //Create Admin User
    //        var adminUser = new ApplicationUser();
    //        adminUser.UserName = adminUserName;
    //        adminUser.Email = adminEmail;

    //        var adminProfile = new UserInfo();
    //        adminProfile.RealName = "何金水";
    //        adminProfile.NickName = "阿水";
    //        adminUser.Profile = adminProfile;

    //        result = userManager.Create(adminUser, adminPassWord);


    //        //Add User Admin to Role Admin
    //        if (result.Succeeded)
    //        {
    //            userManager.AddToRole(adminUser.Id, roleAdmin);
    //        }

    //    }
    //}
}