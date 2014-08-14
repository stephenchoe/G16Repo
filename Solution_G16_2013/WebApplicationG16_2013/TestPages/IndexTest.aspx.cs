using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using G16_2013.Models.MemberModel;
using G16_2013.BLL;

namespace WebApplicationG16_2013.TestPages
{
    public partial class IndexTest : BaseTestPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDropList();
            }
           
        }
        void LoadDropList()
        {
            var options = MemberBL.Helper.GetPersonSearchWayOptions();
            foreach (var item in options)
            {
                ddlSearchWay.Items.Add(new ListItem(item.Text, item.Value));
            }

            ddlOrderBy.Items.Add(new ListItem("Name", "Name"));
            ddlOrderBy.Items.Add(new ListItem("PersonId", "PersonId"));
        }

        void DoSearch()
        {
            string keyWord = txtKeyWord.Text.Trim();
            int option = Convert.ToInt32(ddlSearchWay.SelectedValue);
            var people = memberBL.GetPeopleByKeyWord(option, keyWord).AsQueryable();
           
            string sortBy = ddlOrderBy.SelectedValue;
            if (sortBy == "Name")
            {
                people = people.OrderByDescending(p => p.Name);
            }
            else
            {
                people = people.OrderBy(m => m.PersonId);
            }

            GridView1.DataSource = people;
            GridView1.DataBind();
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            DoSearch();
        }
    }
}