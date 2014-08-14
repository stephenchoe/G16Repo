using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using G16_2013.BLL;
using G16_2013.Models.MemberModel;

namespace WebApplicationG16_2013.Controllers
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


        #endregion //
        // GET: /Base/
        
	}
}