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
    public partial class AccountInputModelTest : BaseTestPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Test();
        }
        void Test()
        {
            AccountInputModel accountInput = GetFuturesAccountInput();
            if (accountInput is FuturesAccountInputModel)
            {
                FuturesAccountInputModel futuresAccountInput = accountInput as FuturesAccountInputModel;
                lblId.Text = "accountInput is FuturesAccountInputModel";
            }
               
        }
        //void ShowAccountView(int id)
        //{
        //    var futuresAccount = memberBL.GetFuturesAccountById(id);
        //    var view = memberBL.GetFuturesAccountView(futuresAccount);
        //    List<FuturesAccountViewModel> listAccount = new List<FuturesAccountViewModel>() { view };
        //    //List<FuturesAccount> listAccount = new List<FuturesAccount>() { futuresAccount };
        //    DetailsView1.DataSource = listAccount;
        //    DetailsView1.DataBind();
        //}
        //void InsertFuturesAccount()
        //{
        //    var futuresAccountInput = GetFuturesAccountInput();
        //    var contactInfoInput = GetAccountContactInfoInputModel();
        //    var futuresAccount = memberBL.AddFuturesAccount(futuresAccountInput, contactInfoInput);
        //    if (futuresAccount == null)
        //    {
        //        lblId.Text = "0";
               
        //        return;
        //    }
        //    lblId.Text = futuresAccount.AccountId.ToString();
           
        //}

        void UpdateFuturesAccount()
        {
            var futuresAccountInput = GetFuturesAccountInput();
            futuresAccountInput.AccountNumber = "00789098AA";

        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            //InsertFuturesAccount();
        }

        FuturesAccountInputModel GetFuturesAccountInput()
        {
            var bankInfo = new AccountBankInfo("第一銀行", "南松山分行", 0, "160778990");
            var forexBankInfo = new AccountBankInfo("華南銀行", "信義分行", 3, "55899006544");
            var person = memberBL.GetPersonById(3);
            var inputModel = new FuturesAccountInputModel()
            {
                AccountNumber = "99009900",
                AccountType = Convert.ToInt32(BusinessType.Futures),
                CompanyId=1,
                BranchId = 2,
                AECodeId = 1,
                OpenAEId = 1,
                ServiceAEId=2,
                OpenDate = new DateTime(2014, 2, 11),
                PersonId = 3,
                ReferralId = 5,
                Status = 1,
                ContactInfoInput=new AccountContactInfoInputModel(),
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
            inputModel.ContactInfoInput.Phone = person.ContactInfo.Phone;
            inputModel.ContactInfoInput.TEL = person.ContactInfo.TEL;
            inputModel.ContactInfoInput.Email = person.ContactInfo.Email;
            //Address address=person.ContactInfo.Address;
            //inputModel.ContactInfoInput.CensusAddressZip = address.ZipCode;
            //inputModel.ContactInfoInput.CensusAddressCityId = address.City;
            //inputModel.ContactInfoInput.CensusAddressDistrictId = address.District;
            //inputModel.ContactInfoInput.CensusAddressStreet = address.StreetAddress;

            //inputModel.ContactInfoInput.ZipCode = address.ZipCode;
            //inputModel.ContactInfoInput.CityId = address.City;
            //inputModel.ContactInfoInput.DistrictId = address.District;
            //inputModel.ContactInfoInput.Street = address.StreetAddress;


            return inputModel;
        }

        AccountContactInfoInputModel GetAccountContactInfoInputModel()
        {
            var person = memberBL.GetPersonById(3);
            var inputModel = new AccountContactInfoInputModel()
            {
                Phone = person.ContactInfo.Phone,
                TEL = person.ContactInfo.TEL,
                Email = person.ContactInfo.Email,
                //CensusAddress = new Address(),
                //ContactAddress = person.ContactInfo.Address,                 
            };

            return inputModel;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lblId.Text);
           // ShowAccountView(id);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

    }
}