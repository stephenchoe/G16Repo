using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;

using G16_2013.BLL;
using G16_2013.Models.MemberModel;



namespace WebApplicationG16_2013.Account
{
    public partial class Register : Page
    {

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            //string userName = UserName.Text.Trim();
            //string pass = Password.Text.Trim();

            //InsertUser(userName,pass);
            GetCompany();
        }
        void GetCompany()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var company = memberBL.GetCompanyById(1);
                if (company == null)
                {
                    ErrorMessage.Text = "null";
                }
                else
                {
                    ErrorMessage.Text = company.Name;
                }
            }
        }
        void InsertUser(string userName,string pass)
        {
            //using (MemberBL memberBL = new MemberBL())
            //{
            //    var user = new G16ApplicationUser
            //    {
            //        Email = userName,
            //        UserName = userName
            //    };

            //    user = memberBL.InsertUser(user, pass);
            //    ErrorMessage.Text = user.Id.ToString();
            //}
        }

        //void CreateUser()
        //{
        //    var manager = new UserManager();
        //    var user = new ApplicationUser() { UserName = UserName.Text };
        //    IdentityResult result = manager.Create(user, Password.Text);
        //    if (result.Succeeded)
        //    {
        //        IdentityHelper.SignIn(manager, user, isPersistent: false);
        //        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        //    }
        //    else
        //    {
        //        ErrorMessage.Text = result.Errors.FirstOrDefault();
        //    }
        //}
    }
}