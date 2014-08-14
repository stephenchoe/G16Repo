using G16_2013.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using WebApplicationG16_2013.Models;
using G16_2013.Models.MemberModel;
using G16_2013.BLL;

namespace WebApplicationG16_2013.Controllers
{
    public class AccountController : BaseController
    {
        private G16ApplicationUserManager _userManager;
        public G16ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<G16ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Create(int? personId)
        {
             var accountInput=new AccountInputModel();
             if (personId == null) accountInput = memberBL.GetAccountCreateInputModel();
             else accountInput = memberBL.GetAccountCreateInputModel((int)personId);
             return View(accountInput);
        }
       
        [HttpPost]
        public ActionResult Create(AccountInputModel input)
        {
            return View("Test", input);
          
        }

        //private void PopulateDropDownList(AccountInputModel inputModel)
        //{
        //    var options = inputModel.CompanyOptions;
        //    object selectedItem = inputModel.CompanyId;
        //    ViewBag.CompanyId = new SelectList(options, "Value", "Text", selectedItem);

        //    options = inputModel.BranchOptions;
        //    selectedItem = inputModel.BranchId;
        //    ViewBag.BranchId = new SelectList(options, "Value", "Text", selectedItem);

        //    options = inputModel.PersonOptions;
        //    selectedItem = inputModel.PersonId;
        //    ViewBag.PersonId = new SelectList(options, "Value", "Text", selectedItem);

        //    options = inputModel.AccountTypeOptions;
        //    selectedItem = inputModel.AccountType;
        //    ViewBag.AccountType = new SelectList(options, "Value", "Text", selectedItem);

        //    options = inputModel.AEOptions;
        //    selectedItem = inputModel.OpenAEId;
        //    ViewBag.OpenAEId = new SelectList(options, "Value", "Text", selectedItem);
           
        //    selectedItem = inputModel.ServiceAEId;
        //    ViewBag.ServiceAEId = new SelectList(options, "Value", "Text", selectedItem);

        //    options = inputModel.AECodeOptions;
        //    selectedItem = inputModel.AECodeId;
        //    ViewBag.AECodeId = new SelectList(options, "Value", "Text", selectedItem);


        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        public async Task<ActionResult>  Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //using (MemberBL memberBL = new MemberBL())
                //{
                //    var user = new G16ApplicationUser { UserName = model.Email, Email = model.Email };
                //    user = memberBL.InsertUser(user, model.Password);
                //    if (user != null)
                //    {
                //        var code = memberBL.GenerateEmailConfirmationToken(user.Id);
                //        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //        //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                //        ViewBag.Link = callbackUrl;
                //        return View("DisplayEmail");
                //    }
                //}
               
                var user = new G16ApplicationUser { UserName = model.Email, Email = model.Email };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    ViewBag.Link = callbackUrl;
                    return View("DisplayEmail");
                }
               // AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
           
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("Register");
            }
           // AddErrors(result);
            return View();
        }



	}
}