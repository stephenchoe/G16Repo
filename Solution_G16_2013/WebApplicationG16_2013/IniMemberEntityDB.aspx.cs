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

namespace WebApplicationG16_2013
{
    public partial class IniMemberEntityDB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            IniDB();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (G16MemberEntities context = new G16MemberEntities())
            {
                var allCity = context.Cities;

                foreach (var city in allCity)
                {
                    Response.Write(city.Name + "<br/>");
                }
            }
        }
        void IniDB()
        {
            string dataPath = HttpContext.Current.Server.MapPath("~/App_Data/City.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(dataPath);

            Database.SetInitializer(new G16MemberEntitiesSeedData(doc));
         
        }
    }
}