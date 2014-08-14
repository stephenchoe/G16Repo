using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationG16_2013.Models;

namespace WebApplicationG16_2013.UserIdentity
{
    public class UserIdentityBL:IDisposable
    {
        private IUserIdentity userIdentityRepository;
        public UserIdentityBL()
        {
            this.userIdentityRepository = new IdentityRepository();
        }
        public UserIdentityBL(IUserIdentity identityRepository)
        {
            this.userIdentityRepository = identityRepository;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return userIdentityRepository.GetAllRoles();
        }

        public void AddRole(string roleName)
        {
            ValidateDuplicateRoleName(roleName);
            ValidateEmptyRoleName(roleName);

            try
            {
                userIdentityRepository.AddRole(roleName);
            }
            catch (Exception ex)
            {
                //Include catch blocks for specific exceptions first,
                //and handle or log the error as appropriate in each.
                //Include a generic catch block like this one last.

                throw ex;
            }
        }
        public void AddUser(ApplicationUser user, string passWord)
        {
            try
            {
                userIdentityRepository.AddUser(user, passWord);
            }
            catch (Exception ex)
            {
                //Include catch blocks for specific exceptions first,
                //and handle or log the error as appropriate in each.
                //Include a generic catch block like this one last.

                throw ex;
            }

        }
        //public void AddUser(string userName, string passWord)
        //{
        //    var user = new ApplicationUser() { UserName = userName };
        //    try
        //    {
        //        userIdentityRepository.AddUser(user, passWord);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Include catch blocks for specific exceptions first,
        //        //and handle or log the error as appropriate in each.
        //        //Include a generic catch block like this one last.

        //        throw ex;
        //    }

        //}

        void ValidateDuplicateRoleName(string roleName)
        {
            if (userIdentityRepository.IsRoleExsits(roleName))
            {
                string message = String.Format("角色名稱錯誤.(角色名稱『{0}』已經存在)", roleName);
                throw new DuplicateRoleException(message);
            }
           
        }
        void ValidateEmptyRoleName(string roleName)
        {
            if (String.IsNullOrEmpty(roleName))
            {
                throw new EmptyRoleNameException("角色名稱錯誤.(角色名稱不可空白)");
            }
        }

        #region dispose
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    userIdentityRepository.Dispose();
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
    }//end class
}