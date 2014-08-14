using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using System;
using WebApplicationG16_2013.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace WebApplicationG16_2013.Models
{
    // You can add User data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    { 
        // Email will be stored in the same table as Users
       // public string Email { get; set; }

        // RealName & NickName will be stored in a different table called UserInfo
        public int UserInfoId { get; set; }
        public virtual UserInfo Profile { get; set; }


    }

    public class UserInfo
    {
        public int UserInfoId { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
    }
  
    public class UserIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserIdentityDbContext()
            : base("UserIdentityConnection")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");
        }

        public DbSet<UserInfo> UserInfoSet { get; set; }
    
    }


    #region Helpers
    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager()
            : base(new UserStore<ApplicationUser>(new UserIdentityDbContext()))
        {
        }
        public UserManager(UserIdentityDbContext context)
            : base(new UserStore<ApplicationUser>(context))
        {
        }
    }
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager()
            : base(new RoleStore<IdentityRole>(new UserIdentityDbContext()))
        {
        }
        public RoleManager(UserIdentityDbContext context)
            : base(new RoleStore<IdentityRole>(context))
        {
        }
    }
}

namespace WebApplicationG16_2013
{
    public static class IdentityHelper
    {
        // Used for XSRF when linking external logins
        public const string XsrfKey = "XsrfId";

        public static void SignIn(UserManager manager, ApplicationUser user, bool isPersistent)
        {
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        public const string ProviderNameKey = "providerName";
        public static string GetProviderNameFromRequest(HttpRequest request)
        {
            return request[ProviderNameKey];
        }

        public static string GetExternalLoginRedirectUrl(string accountProvider)
        {
            return "/Account/RegisterExternalLogin?" + ProviderNameKey + "=" + accountProvider;
        }

        private static bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response)
        {
            if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
            {
                response.Redirect(returnUrl);
            }
            else
            {
                response.Redirect("~/");
            }
        }
    }
    #endregion
}