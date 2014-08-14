using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using G16_2013.BLL;
using G16_2013.Models.MemberModel;

namespace G16MG.Controllers
{
    public class BaseController : Controller
    {
        private MemberBL _memberBL;
        protected MemberBL memberBL
        {

            get
            {
                if (_memberBL == null) InitialMemberBL();
                return _memberBL;
            }
        }

       
        protected string EditorTemplateFolderPath
        {
            get { return "~/Views/Shared/EditorTemplates/"; }
        }
       
        protected string DisplayTemplateFolderPath
        {
            get { return "~/Views/Shared/DisplayTemplates/"; }
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
        #region Dispose
        private bool disposedValue = false;

        protected override void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    memberBL.Dispose();
                }
            }
            this.disposedValue = true;
            base.Dispose(disposing);
        }


        #endregion

        public ActionResult StatusAlert(string status, string positionId = "", int timeout = 1500)
        {
            StatusAlert data = new StatusAlert();
            data.Status = status;
            data.PositionId = positionId;
            data.Timeout = timeout;
            return PartialView(data);
        }
    }
}
