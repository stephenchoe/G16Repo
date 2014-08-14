using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using System.Data.Entity;

using Microsoft.Owin;
using Microsoft.Owin.Security;



namespace G16_2013.Models.MemberModel
{
    public class G16ApplicationUser : IdentityUser
    {

        public virtual Person Person { get; set; }
      

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<G16ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
  
}
