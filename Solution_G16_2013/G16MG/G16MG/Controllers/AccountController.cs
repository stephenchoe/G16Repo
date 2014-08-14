using G16_2013.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G16MG.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            FuturesAccountInputModel input = memberBL.GetFuturesAccountCreateInputModel();
            return View(input);
        }
        [HttpPost]
        public ActionResult CreateFuturesAccount(FuturesAccountInputModel input)
        {
            int accountType = input.AccountType;
            if (accountType == Convert.ToInt32(BusinessType.Stock))
            {
                StockAccountInputModel stockInput = memberBL.GetStockAccountCreateInput(input);
                return View(stockInput);
            }
            else if (accountType == Convert.ToInt32(BusinessType.Fund))
            {
              
            }
            return View(input);

        }

    }
}
