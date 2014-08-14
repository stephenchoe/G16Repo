using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using G16_2013.BLL;
using G16_2013.DAL;
using G16_2013.Models.TradeModel;
using G16_2013.Models.MemberModel;
using System.Data.Entity;

namespace WebApplicationG16_2013
{
    public partial class TestWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string acc = "9800312";
            string test = acc.Substring(acc.Length - 3);
            Label1.Text = test;
        }
        static TimeSpan GetTimeSpan(string input, bool isEnd)
        {
            string[] values = input.Split(':');
            int hour = Convert.ToInt32(values[0]);
            int minute = Convert.ToInt32(values[1]);
            int second = Convert.ToInt32(values[2]);
            int mileSecond = Convert.ToInt32(values[3]);
            TimeSpan returnValue = new TimeSpan(0, hour, minute, second, mileSecond);
            if (isEnd)
            {
                TimeSpan diff = new TimeSpan(0, 0, 0, 0, -1);
                returnValue = returnValue.Add(diff);
            }


            return returnValue;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            using (var context = new G16MemberContext())
            {
                var account = context.Accounts.Find(2);
                account.ReferralId = null;
                context.SaveChanges();
                
            }

            GetAccount();
          
        }
        void GetAccount()
        {
            using (var context = new G16MemberContext())
            {
                var account = context.Accounts.Find(2);
                if (account.Referral != null)
                {
                    Label1.Text = account.Referral.Name;
                }
                else
                {
                    Label1.Text = "null";
                }
            }
        }

        void CreatePrivateIdentity()
        {
            //using (var memberBL = new MemberBL())
            //{
            //    var person = memberBL.GetPersonByName("何金銀");
            //    var identity = memberBL.CreateCustomIdentity("自訂測試", person.PersonId);
            //    if (identity == null)
            //    {
            //        Label1.Text = "null";
            //    }
            //    else
            //    {
            //        Label1.Text = identity.IdentityId.ToString();
            //    }
            //}
        }
        void CreatePublicIdentity()
        {
            using (var memberBL = new MemberBL())
            {
                
                var identity = memberBL.CreatePublicIdentity("公開測試");
                if (identity == null)
                {
                    Label1.Text = "null";
                }
                else
                {
                    Label1.Text = identity.IdentityId.ToString();
                }
            }
        }

        void GetUserFromPerson()
        {
            //using (var memberBL = new MemberBL())
            //{
            //    var person = memberBL.GetPersonByName("何金銀");
            //    var user = memberBL.GetUserByPerson(person);

            //    Label1.Text = user.Id;
            //}
        }

        void GetAllRoles()
        {
            using (var memberBL = new MemberBL())
            {
                List<string> roleNames = memberBL.GetAllRoleNames();
                if (roleNames == null) return;
                foreach (var item in roleNames)
                {
                    CheckBoxList1.Items.Add(new ListItem(item));
                }
            }
        }
    }
}