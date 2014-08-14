
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationG16_2013.UserIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplicationG16_2013.Models;
namespace WebApplicationG16_2013.Admin.UserAdmin
{
    public partial class UserAdminIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
              //  BindRoles();

            }

            using (IdentityRepository identityRepository = new IdentityRepository())
            {
                ApplicationUser user = identityRepository.GetUser("stephenchoe", "bonds25");
                if (user != null)
                {
                    Label1.Text = user.Profile.RealName;
                    IdentityUserRole userRole = user.Roles.FirstOrDefault();
                    IdentityRole role = identityRepository.GetRole("uuu");
                    if (role == null)
                    {
                        Label2.Text = "null";
                    }
                    else
                    {
                        Label2.Text = role.Id + "," + role.Name;
                    }
                    
                }
            }

        }

        void BindRoles()
        {
            ddlRoles.Items.Clear();
            using (IdentityRepository identityRepository = new IdentityRepository())
            {
                IEnumerable<IdentityRole> roles = identityRepository.GetAllRoles();
                foreach (IdentityRole role in roles)
                {
                    ddlRoles.Items.Add(new ListItem(role.Name));

                }
            }
        }


        void AddUser()
        {
            using (IdentityRepository identityRepository = new IdentityRepository())
            {
                
                var user = new ApplicationUser()
                {
                    UserName = "testMember201302",
                    Email = "testMemberEmail"
                };
                user.Profile = new UserInfo()
                {
                    RealName = "testMemberRealName",
                    NickName = "testMemberNickName"
                };

                string pw = "pass20130209";
                string roleName="Member";

                identityRepository.AddUser(user, pw, roleName);

            }
        
        }

        void DeleteUser()
        {
            using (IdentityRepository identityRepository = new IdentityRepository())
            {
                //ApplicationUser user = identityRepository.FindUserByName("testMember201302");

                //if (user == null)
                //{
                //    Label2.Text = "null";
                //}
                //else
                //{
                //    Label2.Text = user.Id;
                //}
                
                 identityRepository.DeleteUser("testMember201302");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }

    }
}