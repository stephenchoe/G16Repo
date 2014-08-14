using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationG16_2013.Admin.Controls
{
    public partial class UC_UserProfile : System.Web.UI.UserControl
    {
        public string RealName
        {
            get
            {
                return txtRealName.Text.Trim();
            }
            set
            {
                txtRealName.Text = value;
            }
        }
        public string NickName
        {
            get
            {
                return txtNickName.Text.Trim();
            }
            set
            {
                txtNickName.Text = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}