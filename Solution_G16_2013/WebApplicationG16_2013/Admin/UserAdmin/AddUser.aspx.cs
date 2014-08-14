using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationG16_2013.Models;
using WebApplicationG16_2013.UserIdentity;


namespace WebApplicationG16_2013.UserAdmin
{

    public partial class AddUser : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            string userName = UserName.Text.Trim();
            string passWord = Password.Text.Trim();
            string email = txtEmail.Text.Trim();

            string realName = UC_UserProfile.RealName;
            string nickName = UC_UserProfile.NickName;

           

            var user = new ApplicationUser()
            {
                UserName = userName,
                Email = email
            };
            user.Profile = new UserInfo()
            {
                RealName = realName,
                NickName = nickName
            };

            

           
           
            using (UserIdentityBL userIdentityBL = new UserIdentityBL())
            {
                try
                {
                    userIdentityBL.AddUser(user, passWord);
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = ex.Message;
                }
                
            }
         
        }
    }
}