using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using G16_2013.DAL;
using G16_2013.BLL;
using G16_2013.Models.MemberModel;

namespace WebApplicationG16_2013.TestPages.MemberModel
{
    public partial class InputModelTest : System.Web.UI.Page
    {
        private MemberBL _memberBL;
        MemberBL memberBL
        {

            get
            {
                if (_memberBL == null) InitialMemberBL();
                return _memberBL;
            }
        }

        void InitialMemberBL()
        {
            var userInput = new UserInputModel()
            {
                UserName = "stephenchoe",
                PassWord = "bonds25",
                Email = "opmart2008@yahoo.com.tw",
            };
            var user = MemberBL.GetUserByLogin(userInput);
            _memberBL = new MemberBL(user);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            AccountInputModel input = GetFuturesAccountInputModel();
            Test(input);
        }
        AccountInputModel GetFuturesAccountInputModel()
        {
            FuturesAccountInputModel input = new FuturesAccountInputModel()
            {
                AccountNumber = "9800312",
                //TaiwanWithdrawBankName = "第一銀行"
            };
            return input;
        }
        void Test(AccountInputModel accountInput)
        {
            if (accountInput is FuturesAccountInputModel && accountInput.AccountType == (int)BusinessType.Futures)
            {
                FuturesAccountInputModel futuresInput = accountInput as FuturesAccountInputModel;
                if (futuresInput == null)
                {
                    Label1.Text = "null";
                }
                else
                {
                    Label1.Text = futuresInput.AccountNumber;
                }

               
            }
        }
        void DesignateAECodeToAE()
        {
            var companyId = 2;
            var aeCode = memberBL.GetAECodesByCompany(companyId).FirstOrDefault();
            var input = memberBL.GetAECodeAEInputModel(aeCode.AECodeId);

            input.BeginDate = new DateTime(2013, 3, 1);

            aeCode = memberBL.DesignateAECodeToAE(input);
            if (aeCode == null)
            {
                Label1.Text = "null";
            }
            else
            {
                Label1.Text = aeCode.AECodeId.ToString();
            }
        }
        void CreateAECode()
        {
            //var input=memberBL.GetAECodeInputModel();
            //input.CompanyId = Convert.ToInt32(input.CompanyOptions.FirstOrDefault().Value);
            //input.BranchId = Convert.ToInt32(input.BranchOptions.Where(b => b.Value != "0").FirstOrDefault().Value);
            //input.BusinessTypeId = Convert.ToInt32(input.BusinessTypeOptions.FirstOrDefault().Value);
            //input.Code = "z0077";

            //var aeCode = memberBL.CreateAECode(input);
            //if (aeCode == null)
            //{
            //    Label1.Text = "null";
            //}
            //else
            //{
            //    Label1.Text = aeCode.AECodeId.ToString();
            //}

        }

        void CreateAccount(AccountInputModel accountInput)
        { 
           
        }

        //FuturesAccountInputModel GetFuturesAccountInput()
        //{
        //    var futuresAccountInput = new FuturesAccountInputModel
        //    {
        //         AccountNumber="9800312",
                  

        //    };
        //}

        void DesignateCompanyToAE()
        {
            AECompanyInput input = new AECompanyInput()
            {
                BranchId = 2,
                CompanyId = 1,
                BeginDate = new DateTime(2014, 1, 2),
                StaffNumber = "102011",
                PersonalTEL = "02-27170980",
                Title = "業務經理",
                TELCode = "355",
                AEId = 1,
            };

            var ae = memberBL.DesignateCompanyToAE(input);
            if (ae == null)
            {
                Label1.Text = "null";
            }
            else
            {
                Label1.Text = ae.AEId.ToString();
            }
        }


      
        void CreateAE()
        {
            var input = new AEInputModel()
            {
                BeginDate = new DateTime(2011, 5, 2),
               // EndDate = new DateTime(2013, 12, 31),
            };
            var personInput = new PersonInputModel
            {
                Birthday = new DateTime(1982, 7, 23),
                Gender = true,
                Name = "邱正義",
                TWID = "F124908770",
                
            };
            var contactInfoInput = new PersonContactInfoInputModel
            {
                Phone = "0932980711",
                TEL = "02-29220329",
                Email = "peter.chu@yahoo.com.tw",
                //ZipCode = "234",
                //CityId = memberBL.GetCityByName("新北市").CityId.ToString(),
                //DistrictId = memberBL.GetDistrictByName("永和區").DistrictId.ToString(),
                //Street = "安樂路119巷33號2樓",

            };
            personInput.ContactInfoInput = contactInfoInput;
            input.PersonInput = personInput;

            //var ae = null;
            //if (ae == null)
            //{
            //    Label1.Text = "null";
            //}
            //else
            //{
            //    Label1.Text = ae.AEId.ToString();
            //}
           

        }
       

        PersonInputModel GetPersonInput()
        {
            var personInput = new PersonInputModel
            {              
                Birthday = new DateTime(1980, 3, 3),
                Gender = true,
                Name = "合金金",
                TWID = "A123499033",
                PS="PSPSPS"
            };
            return personInput;
        
        }
        PersonContactInfoInputModel GetContactInfoInput()
        {
            var contactInfoInput = new PersonContactInfoInputModel
            {
                Phone = "098989086",
                TEL = "02-36977533",
                Email = "uyi@yahoo.com.tw",
                //ZipCode = "106",
                //CityId = memberBL.GetCityByName("台北市").CityId.ToString(),
                //DistrictId = memberBL.GetDistrictByName("大安區").DistrictId.ToString(),
                //Street = "信義路三段70號5樓",
      
            };
            return contactInfoInput;

        }

        //void PersonInsert()
        //{
        //    var personInput = GetPersonInput();
        //    var contactInfoInput = GetContactInfoInput();
        //    var person = memberBL.InsertPerson(personInput, contactInfoInput);
        //    if (person == null)
        //    {
        //        Label1.Text = "failed";
        //    }
        //    {
        //        Label1.Text = person.PersonId.ToString();
        //    }
        //}

        void PersonUpdate()
        {
            var personInput = GetPersonInput();
            personInput.PersonId=16;
            personInput.Name = "陳改名";
            
            var person = memberBL.UpdatePerson(personInput);
            if (person == null)
            {
                Label1.Text = "failed";
            }
            {
                Label1.Text = person.PersonId.ToString();
            }
        }
        void UpdatePersonContactInfo()
        {
            var input = GetContactInfoInput();
            input.Id = 16;
            //input.Street = "復興北路230號3樓";

            var contactInfo= memberBL.UpdatePersonContactInfo(input);
            if (contactInfo == null)
            {
                Label1.Text = "failed";
            }
            {
                Label1.Text = contactInfo.PersonId.ToString();
            }
        }
        void RemovePerson()
        {
            int isOK = memberBL.RemovePerson(17);
            Response.Write(isOK.ToString());
        }

     

        public override void Dispose()
        {
            if (memberBL != null)
            {
                _memberBL.Dispose();
                _memberBL = null;
            }
            base.Dispose();
           
        }


       
    }
}