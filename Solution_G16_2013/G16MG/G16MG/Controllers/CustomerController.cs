using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using G16_2013.Models.MemberModel;

namespace G16MG.Controllers
{
    public class CustomerController : BaseController
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult AddFuturesCustomer()
        {
            FuturesCustomerInput futuresCustomerInput = memberBL.GetFuturesCustomerCreateInput();
           
            return PartialView("_CreateFuturesCustomer", futuresCustomerInput);

        }
        public PartialViewResult NextStep(PersonSelectCreateInput input)
        {
            FuturesCustomerInput futuresCustomerInput = new FuturesCustomerInput();
            input = memberBL.GetPersonSelectCreateInput(input);
            futuresCustomerInput.PersonSelectCreateInput = input;
            return PartialView("_CreateFuturesCustomer", futuresCustomerInput);

        }
        public ActionResult Create()
        {
            PersonSelectCreateInput input = new PersonSelectCreateInput();
            input.NewPersonInput = memberBL.GetPersonCreateInputModel();
            return View(input);
            //var accountInput = memberBL.GetAccountCreateInputModel();
            //var personInput = accountInput.PersonInput;
            //var contactInfoInput = personInput.ContactInfoInput;

            //personInput.CurrentMode = CurrentMode.ReadOnly;
            //personInput.AllowSelect = true;

            //contactInfoInput.CurrentMode = CurrentMode.ReadOnly;


            //string cityId = contactInfoInput.AddressInput.CityId;
            //string districtId = contactInfoInput.AddressInput.CityId;
            //PopulateCityDropDownList(cityId);
            //PopulateDistrictDropDownList(cityId, districtId);

           // return View(accountInput);
        }
        public ActionResult CreateAccount(AccountInputModel inputModel)
        {
            if (inputModel is FuturesAccountInputModel)
            {
                FuturesAccountInputModel futuresInputModel = inputModel as FuturesAccountInputModel;
                futuresInputModel.AccountNumber = "8509090";
                futuresInputModel.AccountType = 0;
                futuresInputModel.ServiceAEId = 1;
                futuresInputModel.CompanyId = 1;
                futuresInputModel.BranchId = 2;
                futuresInputModel.AECodeId = 1;
                futuresInputModel.OpenDate = new DateTime(2012, 3, 6);
                var person = memberBL.CreateCustomerWithAccount(futuresInputModel);
            }
            
            //FuturesAccountInputModel futuresInputModel = inputModel as FuturesAccountInputModel;
          //  AccountInputModel futuresInputModel = inputModel;
            
            //futuresInputModel.TaiwanWithdrawBank = new AccountBankInfo("第一銀行", "長春分行", 0, "13889977090");
            //futuresInputModel.FirstTWDepositBank = new AccountBankInfo("第一銀行", "長春分行", 0, "13889977090");
            //if(inputModel is FuturesAccountInputModel)
            //{
            //  inputModel=inputModel as FuturesAccountInputModel;
                 
            //     inputModel.TaiwanWithdrawBank = new AccountBankInfo("第一銀行", "長春分行", 0, "13889977090");
            //}
            

            return View();
        }
        public ActionResult ShowAccountInput()
        {
            var accountInput = memberBL.GetAccountCreateInputModel();
            //var personInput = accountInput.PersonInput;
            //var contactInfoInput = personInput.ContactInfoInput;

            //personInput.CurrentMode = CurrentMode.ReadOnly;
            //personInput.AllowSelect = true;

            //contactInfoInput.CurrentMode = CurrentMode.ReadOnly;


            //string cityId = contactInfoInput.AddressInput.CityId;
            //string districtId = contactInfoInput.AddressInput.CityId;
            //PopulateCityDropDownList(cityId);
            //PopulateDistrictDropDownList(cityId, districtId);
            return PartialView("~/Views/Shared/EditorTemplates/Account.cshtml", accountInput);
            
        }
       

    }
}
