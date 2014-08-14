using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Entity;
using G16_2013.DAL;
using G16_2013.BLL;
using System.Xml;
using G16_2013.Models.MemberModel;

namespace WebApplicationG16_2013.TestPages.MemberModel
{
    public partial class InitializeMemberDB : System.Web.UI.Page
    {
        UserInputModel UserInput { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var userInput = new UserInputModel()
            {
                UserName = "stephenchoe",
                PassWord = "bonds25",
                Email = "opmart2008@yahoo.com.tw",
            };
            UserInput = userInput;

            if (Page.IsPostBack) return;

            IniDB();
            
        }

        void IniDB()
        {

            Database.SetInitializer(new DropCreateMemberDatabaseWithSeedData(UserInput));
              Response.Write("InitializeMemberDB OK.");
        }

        void SeedData()
        {
            var user = MemberBL.GetUserByLogin(UserInput);
            if (user == null)
            {
                Response.Write("Login Failed");
                return ;
            }
            string dataPath = HttpContext.Current.Server.MapPath("~/App_Data/City.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(dataPath);
            var memberDataBaseInitialHelper = new G16MemberDataBaseInitialHelper(user, doc);
            memberDataBaseInitialHelper.InitialMemberDB();

            Response.Write("SeedData OK.");
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SeedData();
           
           
        }

    }
}