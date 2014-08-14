using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G16_2013.BLL;
using G16_2013.DAL;
using G16_2013.Models.MemberModel;
using System.Data.Entity;

namespace G16WinForm
{
    public partial class G16MemberForm : Form
    {
        public G16MemberForm()
        {
            InitializeComponent();
            //GetCompany();
            //InsertBranchCompany();
            //InsertPerson();
            //TestHelper.InsertPeople();
            //TestHelper.InsertAE();
            //InsertRole("newRole");
            //  AddUserToRole("newRole");
            //AddUserToRole("newRole");
            //InsertUser();
           // GenerateEmailConfirmationToken();
           

        }

        void ConfirmEmail()
        {
            using (MemberBL memberBL = new MemberBL())
            {

                var user = memberBL.FindUserByName("admin@admin.com");

                string token = label1.Text;
                label1.Text = memberBL.ConfirmEmail(user.Id, token).ToString();
            }
        }
        void GenerateEmailConfirmationToken()
        {
            using (MemberBL memberBL = new MemberBL())
            {

                var user = memberBL.FindUserByName("admin@admin.com");


                label1.Text = memberBL.GenerateEmailConfirmationToken(user.Id);
            }
        }

        void DeleteUser()
        {
            using (MemberBL memberBL = new MemberBL())
            {
               
                int isOK = memberBL.DeleteUser("admin@admin.com");
                

                label1.Text = isOK.ToString();
            }
        }
        void UpdateUser()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var user = memberBL.FindUserByName("admin@admin.com");
                user.PhoneNumber = "updatedPhoneNumber";
                user.UserName = "updatedUserName";
                user = memberBL.UpdateUser(user);

                label1.Text = user.Id;
            }
        }




        void InsertUser()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var user = new G16ApplicationUser
                {
                    Email = "opmart2008@yahoo.com.tw",
                    UserName = "opmart2008@yahoo.com.tw"
                };
                string passWord = "bonds25";
                user = memberBL.InsertUser(user, passWord);
                label1.Text = user.Id.ToString();
            }
        }

        void DeleteRole()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                int isOK = memberBL.DeleteRole("newRole");
                label1.Text = isOK.ToString();
            }
        }

        void GetUser(string userName, string passWord)
        {
            using (MemberBL memberBL = new MemberBL())
            {
                if (passWord == "")
                {
                    var user = memberBL.FindUserByName(userName);
                    label1.Text = user.Id;
                }
                else
                {
                    var user = memberBL.GetUser(userName, passWord);
                    label1.Text = user.Id;
                }

            }
        }
        void AddUserToRole(string roleName)
        {
            using (MemberBL memberBL = new MemberBL())
            {

                var user = memberBL.FindUserByName("admin@admin.com");
                int isOK = memberBL.AddUserToRole(user, roleName);
                label1.Text = isOK.ToString();

            }
        }
        void InsertRole(string roleName)
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var role = memberBL.InsertRole(roleName);
                label1.Text = role.Id.ToString();
            }
        }

        void GetRole()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var role = memberBL.GetRoleByName("Admin");
                label1.Text = role.Id + ",Name:" + role.Name;
            }
        }
        void UpdateRole()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var role = memberBL.GetRoleByName("Admin");
                role.Name = "NewName";
                role = memberBL.UpdateRole(role);
                label1.Text = role.Id + ",Name:" + role.Name;
            }
        }

        void AddCompanyAE()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var company = memberBL.GetCompanyByName("台北總公司");
                var ae = memberBL.GetAEById(1);
                var companyAE = new CompanyAE
                {
                    AE = ae,
                    Company = company,
                    BeginDate = new DateTime(2014, 1, 2),
                    IsActive = true,
                    StaffNumber = "95011",
                    //LastUpdated = DateTime.Now,
                    //UpdatedBy = "Stephen",
                };

                memberBL.InsertCompanyAE(companyAE);

                var aeNumberTwo = memberBL.GetAEById(2);
                var companyAENumberTwo = new CompanyAE
                {
                    AE = aeNumberTwo,
                    Company = company,
                    BeginDate = new DateTime(2013, 7, 1),
                    IsActive = true,
                    StaffNumber = "99198",
                    //LastUpdated = DateTime.Now,
                    //UpdatedBy = "Stephen",
                };
                memberBL.InsertCompanyAE(companyAENumberTwo);

            }

        }
        void DeleteAECode()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var company = memberBL.GetCompanyByName("台北總公司");
                var code = memberBL.GetAECodeByCode("z0077", company);
                label1.Text = memberBL.DeleteAECode(code).ToString();
            }
        }
        void UpdateAECode()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var company = memberBL.GetCompanyByName("台北總公司");
                var code = memberBL.GetAECodesByCompany(company).First();
                code.IsActive = false;
                code.Code = "z0145";
                code = memberBL.UpdateAECode(code);
                label1.Text = code.Code;

            }
        }
        void InsertAECode(string codeString)
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var company = memberBL.GetCompanyByName("台北總公司");
                var code = new AECode
                {
                    Company = company,
                    BusinessType = BusinessType.Futures,
                    Code = codeString,
                    IsActive = true,
                };
                code = memberBL.InsertAECode(code);
                label1.Text = code.Code;

            }
        }

        void InsertAE()
        {

            using (MemberBL memberBL = new MemberBL())
            {
                var person = memberBL.GetPersonByName("testPersonName");

                var ae = new AE()
                {
                    Person = person,
                    IsActive = true,
                    BeginDate = new DateTime(2014, 2, 12),
                    LastUpdated = DateTime.Now,
                    //DisplayOrder = 0,
                    UpdatedBy = "Stephen",

                };
                ae = memberBL.InsertAE(ae);
                label1.Text = ae.AEId.ToString();
            }

        }
        void InsertAccount()
        {

        }
        void GetCompany()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var company = memberBL.GetCompanyById(1);
                if (company == null)
                {
                    label1.Text = "null";
                }
                else
                {
                    label1.Text = company.Name;
                }
            }
        }
        void InsertBranchCompany()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var parentCompany = memberBL.GetCompanyByName("康和期貨");
                if (parentCompany == null)
                {
                    label1.Text = "null";
                    return;
                }
                var branch = new  Company
                {
                    Name = "台北總公司",
                    CompanyType = BusinessType.Futures,
                    ParentCompanyId = parentCompany.CompanyId,
                    DisplayOrder = 1,
                    BeginDate = DateTime.Now,
                   // LastUpdated = DateTime.Now,
                };
                branch = (memberBL.InsertCompany(branch));
                label1.Text = branch.CompanyId.ToString();
            }
        }
        void InsertPerson()
        {
            var person = new Person()
            {
                Name = "testPersonName",
              //  UserName = "testUserName",
                Gender = true,
                TWID = "C100001110",
                Birthday = new DateTime(1980, 3, 4)
            };
            var contactInfo = new PersonContactInfo()
            {
                TEL = "02-23389090",
                Phone = "0935678904",
                Email = "stephen@yahoo.com.tw",

                Address = new Address()
                {
                    StreetAddress = "信義路三段70號5樓",
                    //City = "台北市",
                    //ZipCode = "106",
                    //District = "大安區"
                }
            };
            person.ContactInfo = contactInfo;
            using (MemberBL memberBL = new MemberBL())
            {
                person = memberBL.InsertPerson(person);
            }
            if (person == null)
            {
                label1.Text = "null";
            }
            else
            {
                label1.Text = person.PersonId.ToString();
            }
        }
        void ShowPersonContactInfo()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var person = memberBL.GetPersonByName("testPersonName");
                if (person == null) return;
                if (person.ContactInfo == null)
                {
                    label1.Text = "null";
                }
                else
                {
                    label1.Text = person.ContactInfo.Phone;
                }

            }
        }
        void UpdatePersonContact()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                int personId = Convert.ToInt32(label1.Text);
                var personContactInfo = memberBL.GetPersonContactInfoById(personId);
                if (personContactInfo == null) return;
                personContactInfo.Phone = "0000";
                personContactInfo = memberBL.UpdatePersonContactInfo(personContactInfo);
            }
        }
        void DeletePersonContact()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                int personId = Convert.ToInt32(label1.Text);
                int isOK = memberBL.DeletePersonContactInfo(personId);

            }
        }

        void InsertPersonContact()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var person = memberBL.GetPersonByName("testPersonName");
                if (person == null) return;
                var contactInfo = new PersonContactInfo()
                {
                    TEL = "02-23389090",
                    Phone = "0935678904",
                    Email = "stephen@yahoo.com.tw",

                    Address = new Address()
                    {
                        StreetAddress = "信義路三段70號5樓",
                        //City = "台北市",
                        //ZipCode = "106",
                        //District = "大安區"
                    }
                };
                person.ContactInfo = contactInfo;

                person = memberBL.UpdatePerson(person);
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            ConfirmEmail();
        }

        void InsertPeople()
        {
            label1.Text = TestHelper.InsertPeople().ToString();

        }
        void GetPeople()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var list = memberBL.GetAllPerson();
                dataGridView1.DataSource = list;
            }
        }

    }
}
