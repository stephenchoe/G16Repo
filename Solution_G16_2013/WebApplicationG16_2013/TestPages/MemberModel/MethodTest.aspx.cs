using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using G16_2013.Models.MemberModel;
using G16_2013.BLL;

namespace WebApplicationG16_2013.TestPages.MemberModel
{
    public partial class MethodTest  : BaseTestPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            GetAccount();
        }
        

        void GetAccount()
        { 
            int id=Convert.ToInt32(lblId.Text);
            var account = memberBL.GetFuturesAccountById(id);
            if (account == null) return;
            lblAccountNumber.Text = account.AccountNumber;
            if (account.ContactInfo == null)
            {
                lblBankCount.Text = "null";
            }
            else
            {
                lblBankCount.Text = account.ContactInfo.ContactAddress.StreetAddress;
            }
            if (account.GetTaiwanWithdrawBank() == null)
            {
                lblTWDWithdrawBank.Text = "null";
            }
            else
            {
                lblTWDWithdrawBank.Text = account.GetTaiwanWithdrawBank().AccountNumber;
            }

            //var view = memberBL.GetFuturesAccountViewModel(id);
            //List<FuturesAccountViewModel> listAccount = new List<FuturesAccountViewModel>() { view };
            //DetailsView1.DataSource = listAccount;
            //DetailsView1.DataBind();
            
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lblId.Text);
            
            lblId.Text = (id + 1).ToString();

            GetAccount();
        }
    }
}