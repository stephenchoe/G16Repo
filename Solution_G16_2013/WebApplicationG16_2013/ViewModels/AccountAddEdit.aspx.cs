using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using G16_2013.Models.MemberModel;
using G16_2013.BLL;

namespace WebApplicationG16_2013.ViewModels
{
    public partial class AccountAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // if (Page.IsPostBack) return;
           

           

        }

        void CreateFuturesAccountInputModel()
        {
            var bankInfo = new AccountBankInfo("第一銀行", "南松山分行", 0, "160778990");
            var forexBankInfo = new AccountBankInfo("華南銀行", "信義分行", 3, "55899006544");

            using (MemberBL memberBL = new MemberBL())
            {
                var person = memberBL.GetPersonById(3);
                var inputModel = new FuturesAccountInputModel()
                {
                    AccountNumber = "99009900",
                    AccountType = Convert.ToInt32(BusinessType.Futures),
                    BranchId = 2,
                    AECodeId = 1,
                    OpenAEId = 1,
                    OpenDate = new DateTime(2014, 2, 11),
                    PersonId = 3,
                    ReferralId = 5,
                    Status = 1,
                    //Phone = person.ContactInfo.Phone,
                    //TEL = person.ContactInfo.TEL,
                    //Email = person.ContactInfo.Email,
                    //CensusAddress = person.ContactInfo.Address,
                    //ContactAddress = person.ContactInfo.Address,

                    //TaiwanWithdrawBank = bankInfo,
                    //FirstTWDepositBank = bankInfo,
                    //ForexWithdrawBank = forexBankInfo,
                    //FirstForexDepositBank = forexBankInfo
                };

                //inputModel.CensusAddress = new Address(person.ContactInfo.Address);
                //inputModel.ContactAddress = new Address(person.ContactInfo.Address);

                InsertAccount(inputModel);
            }
        }

        void InsertAccount(AccountInputModel inputModel)
        {
            if (inputModel.AccountType == Convert.ToInt32(BusinessType.Futures))
            {
                InsertFuturesAccount(inputModel as FuturesAccountInputModel);
            }
        }

        void InsertFuturesAccount(FuturesAccountInputModel inputModel)
        {
            //using (MemberBL memberBL = new MemberBL())
            //{
            //    FuturesAccount account = memberBL.InsertFuturesAccount(inputModel);
            //    Label1.Text = account.AccountId.ToString();
               
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DeleteAccount(11);
        }

        void DeleteAccount(int id)
        {
            //using (MemberBL memberBL = new MemberBL())
            //{
            //    int isOK = memberBL.DeleteAccount(id);
            //    Label1.Text = isOK.ToString();

            //}

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CreateFuturesAccountInputModel();
        }

    }
}