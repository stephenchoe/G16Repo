using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using G16_2013.DAL;
using G16_2013.Models.TradeModel;
using System.Data.Entity;
using WebApplicationG16_2013.ViewModels;

namespace WebApplicationG16_2013.TestPages.TradeModels
{
    public partial class FuturesContractView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (TradeContext context = new TradeContext())
            {
                var futuresSymbols = context.FuturesSymbols.ToList();
                List<FuturesContractViewModel> views = new List<FuturesContractViewModel>();
                foreach (var symbol in futuresSymbols)
                {
                    views.Add(new FuturesContractViewModel(symbol));
                }

                GridView1.DataSource = views;
                GridView1.DataBind();
            }
        }
    }
}