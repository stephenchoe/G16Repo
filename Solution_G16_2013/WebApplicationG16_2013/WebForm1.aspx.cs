using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Entity;
using G16_2013.DAL;
using G16_2013.BLL;

namespace WebApplicationG16_2013
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            IniDB();
        }

        void IniDB()
        {
            //string user = "stephenchoe";
            //Database.SetInitializer(new DropCreateMemberDatabaseWithSeedData(user));
            string dataPath = HttpContext.Current.Server.MapPath("~/App_Data/City.xml");
          
            //TestHelper.IniDBData(dataPath);
           

        }
        void CreateRoles(string roleName)
        {
            //using (var memberBL = new MemberBL())
            //{
            //    int isOK = memberBL.CreateRole(roleName);
            //    Label1.Text = isOK.ToString();
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CreateUserFromPerson();
           
        }
        void RemoveUserFromRole()
        {
            using (var memberBL = new MemberBL())
            {    
            
              //  var user = memberBL.GetUser()
                //Label1.Text = user.Id;
            }
        }
        void CreateUserFromPerson()
        {
            //List<string> roles = new List<string>() { "老闆", "業務員", "客戶" };
            //using (var memberBL = new MemberBL())
            //{
            //    var person=memberBL.GetPersonByName("何金銀");
            //    var user = memberBL.CreateUserFromPerson(person, roles, "$Bonds25");
            //    Label1.Text = user.Id;
            //}
        }
     
        void DeleteRole(string roleName)
        {
            //using (var memberBL = new MemberBL())
            //{
                
            //    int isOK = memberBL.DeleteRole(roleName);
            //    Label1.Text = isOK.ToString();
            //}
       
           
       
        }
    }
}