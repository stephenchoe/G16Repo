using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using G16_2013.BLL;
using G16_2013.Models.MemberModel;


namespace WebApplicationG16_2013.TestPages
{
    public class BaseTestPage : System.Web.UI.Page
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