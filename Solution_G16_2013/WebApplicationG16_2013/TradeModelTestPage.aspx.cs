using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using G16_2013.DAL;
using G16_2013.Models.TradeModel;


namespace WebApplicationG16_2013
{
    public partial class TradeModelTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "";
            using (TradeContext context = new TradeContext())
            {
                var tradeSession = (context.TradeSessions.FirstOrDefault()) as MatchingSession;
                var preOpenSession = tradeSession.PreOpening.PreOpeningSessions.FirstOrDefault();
                Label1.Text = preOpenSession.TradeSessionId.ToString();
            }
        }
    }
}