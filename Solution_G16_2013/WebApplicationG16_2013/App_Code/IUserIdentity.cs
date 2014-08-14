using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationG16_2013.Models;

namespace WebApplicationG16_2013.UserIdentity
{
    public interface IUserIdentity : IDisposable
    {
        IEnumerable<IdentityRole> GetAllRoles();
        void AddRole(string roleName);
        void AddUser(ApplicationUser user, string passWord);
        void AddUser(ApplicationUser user, string passWord,string roleName);
        void AddUserToRole(ApplicationUser user, string roleName);

        bool IsRoleExsits(string roleName);
    }
}
