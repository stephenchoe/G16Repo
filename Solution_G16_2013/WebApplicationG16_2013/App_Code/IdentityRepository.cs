using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationG16_2013.Models;
using System.Data.Entity;

using System.Web.Security;

namespace WebApplicationG16_2013.UserIdentity
{
    public class IdentityRepository : IUserIdentity
    {
        UserIdentityDbContext context;
      
        RoleManager<IdentityRole> roleManager;
       
        UserManager<ApplicationUser> userManager ;

        public IdentityRepository()
        {
            context = new UserIdentityDbContext();
           
             roleManager = new RoleManager();
            
             userManager = new UserManager();

        }
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return context.Roles;
        }
        public void AddRole(string roleName)
        {
            try
            {
                IdentityResult result = roleManager.Create(new IdentityRole(roleName));
                if (!result.Succeeded)
                {

                    string errMsg = result.Errors.FirstOrDefault();
                    throw new AddRoleFailedException("新增角色失敗. " + errMsg);
                }
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
                
            
            
        }

        public void AddUser(ApplicationUser user, string passWord)
        {
            try
            {
                IdentityResult result = userManager.Create(user, passWord);
                if (!result.Succeeded)
                {
                    
                    string errMsg = result.Errors.FirstOrDefault();
                    throw new AddUserFailedException("新增使用者失敗. "+errMsg);
                }
              
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AddUser(ApplicationUser user, string passWord, string roleName)
        {
            try
            {
                if (!IsRoleExsits(roleName))
                {
                    throw new WrongRoleNameException("角色名稱錯誤.(找不到指定名稱的角色)");
                }

                //IdentityRole role = GetRole(roleName);
                //IdentityUserRole userRole = new IdentityUserRole();
                //userRole.Role = role;
                //user.Roles.Add(userRole);

                AddUser(user,passWord);

            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public void DeleteUser(string userName)
        {
            ApplicationUser user = userManager.FindByName(userName);
         
            if (user != null)
            {
                var logins = user.Logins;
                foreach (var login in logins)
                { 
                  
                }

                context.Users.Remove(user);
                context.SaveChanges();
            }
           
        }

        public ApplicationUser FindUserByName(string userName)
        {
            return userManager.FindByName(userName);
        }

        public void AddUserToRole(ApplicationUser user, string roleName)
        {
            try
            {
                IdentityResult result = userManager.AddToRole(user.Id, roleName);
                if (!result.Succeeded)
                {

                    string errMsg = result.Errors.FirstOrDefault();
                    throw new AddUserToRoleFailedException("使用者加入角色失敗. " + errMsg);
                }

             
              
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public bool IsRoleExsits(string roleName)
        {
            return roleManager.RoleExists(roleName);
            
        }

        public IdentityRole GetRole(string roleName)
        {
            return roleManager.FindByName(roleName);
        }

        public ApplicationUser GetUser(string userName,string passWord)
        {

           return userManager.Find(userName, passWord);
          
        }

        #region dispose
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    context.Dispose();
                  
                    roleManager.Dispose();

                  
                    userManager.Dispose();


                }
            }

            this.disposedValue = true;

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}