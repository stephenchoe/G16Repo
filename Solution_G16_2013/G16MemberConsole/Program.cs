using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using G16_2013.DAL;
using G16_2013.Models.MemberModel;

namespace G16MemberConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<G16MemberContext>());
           // DeletePerson(3);
            DropCreateDatabase();
            //ShowCompanyActiveAEs();
            //InsertFuturesAccount();
            //InsertAccountContactInfo();
            //InsertAECodes();
        }
        private static void DropCreateDatabase()
        {
            //string user = "stephenchoe";
           // Database.SetInitializer(new DropCreateMemberDatabaseWithSeedData(user));
            //InsertCompany();
            InsertBranch();
            InsertPerson();
           // InsertCustomer();
            InsertAE();
            AddAECompany();
            AddAECodeFromCompany();
            AddCodeToAE();
            InsertFuturesAccountWithContactInfo();
            InsertAccountBankInfo();
            InsertAccountAECode();

            var person = new Person()
            {
                Name = "testPersonName",
               // UserName = "testUserName",
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
            using (var context = new G16MemberContext())
            {
                context.People.Add(person);
                context.SaveChanges();
            }

        }

       
        private static void ShowMessage(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadLine();
        }

        private static int 康和台北總公司Id()
        {
            using (var context = new G16MemberContext())
            {
                int parentCompanyId = 0;
                var concordsCompany = (from c in context.Companies
                                       where c.Name == "康和期貨"
                                       select c).FirstOrDefault();
                if (concordsCompany == null) return 0;

                parentCompanyId = concordsCompany.CompanyId;

                var department = (from c in context.Companies
                                  where c.Name == "台北總公司" 
                                  select c).FirstOrDefault();
                if (department == null) return 0;

                return department.CompanyId;
            }
        }

        private static int 何金水PersonId()
        {
            int personId = 0;
            using (var context = new G16MemberContext())
            {
                var person = (from p in context.People
                              where p.Name == "stephenchoe"
                              select p).FirstOrDefault();
                if (person != null)
                {
                    personId = person.PersonId;
                }
            }

            return personId;

        }

        private static int 何金水AccountId()
        {
            int accountId = 0;
            using (var context = new G16MemberContext())
            {
                var acc = (from a in context.Accounts
                           where a.AccountNumber == "9800312"
                           select a).FirstOrDefault();
                if (acc == null) return 0;
                accountId = acc.AccountId;
            }

            return accountId;

        }

        private static int z0077AECodeId()
        {
            int aeCodeId = 0;
            int companyId = 康和台北總公司Id();
            using (var context = new G16MemberContext())
            {
                var company = context.Companies.Find(companyId);
                var codes = context.Entry(company).Collection(c => c.AECodes)
                                         .Query();
               
                var z0077AECode = codes.Where(ac => ac.Code == "z0077").FirstOrDefault();
                if (z0077AECode == null) return 0;
                aeCodeId = z0077AECode.AECodeId;
            }
            return aeCodeId;
        }


        private static void InsertCompany()
        {
            var company = new Company
            {
                CompanyType = BusinessType.Futures,
                Name = "康和期貨"
            };

            using (var context = new G16MemberContext())
            {
                context.Companies.Add(company);
                context.SaveChanges();
            }

        }
        private static void InsertBranch()
        {
            var branch = new Company()
            {
                CompanyType = BusinessType.Futures,
                Name = "台北總公司",
            };

            int parentCompanyId = 0;
            using (var context = new G16MemberContext())
            {
                var concordsCompany = (from c in context.Companies
                                       where c.Name == "康和期貨"
                                       select c).FirstOrDefault();
                if (concordsCompany != null)
                {
                    parentCompanyId = concordsCompany.CompanyId;
                }
            }

            if (parentCompanyId == 0) return;

            branch.ParentCompanyId = parentCompanyId;
            using (var context = new G16MemberContext())
            {
                context.Companies.Add(branch);
                context.SaveChanges();
            }
        }

        private static void InsertPerson()
        {
            var person = new Person();
            person.Name = "何金水";
           // person.UserName = "stephenchoe";
            person.Gender = true;
            person.TWID = "F1245000989";
            person.Birthday = new DateTime(1979, 3, 12);

            PersonContactInfo contactInfo = new PersonContactInfo();
            contactInfo.TEL = "02-27170305";
            contactInfo.Email = "opmart2008@yahoo.com.tw";
            contactInfo.Phone = "0936060049";
            contactInfo.Address = new Address()
            {
                StreetAddress = "復興北路143號5樓",
                //City = "台北市",
                //ZipCode = "10547",
                //District = "松山區"
            };
            person.ContactInfo = contactInfo;
            using (var context = new G16MemberContext())
            {
                context.People.Add(person);
                context.SaveChanges();
            }


        }

        //private static void InsertCustomer()
        //{
        //    var customer = new Customer()
        //    {
        //        Name = "何金銀",
        //        UserName = "stephen1979",
        //        Gender = 1,
        //        TWID = "A100001110",
        //        Birthday = new DateTime(1980, 3, 4)

        //    };
        //    var contactInfo = new PersonContactInfo()
        //    {
        //        TEL = "02-23389090",
        //        Phone = "0935678904",
        //        Email = "stephen@yahoo.com.tw",

        //        Address = new Address()
        //        {
        //            StreetAddress = "信義路三段70號5樓",
        //            City = "台北市",
        //            ZipCode = "106",
        //            District = "大安區"
        //        }
        //    };

        //    customer.ContactInfo = contactInfo;
        //    using (var context = new G16MemberContext())
        //    {
        //        context.People.Add(customer);
        //        context.SaveChanges();
        //    }


        //}
        private static void InsertAE()
        {
            int personId = 0;
            using (var context = new G16MemberContext())
            {
                var person = (from p in context.People
                              where p.Name == "stephenchoe"
                              select p).FirstOrDefault();
                if (person != null)
                {
                    personId = person.PersonId;
                }
            }

            if (personId == 0) return;


            using (var context = new G16MemberContext())
            {
                var ae = new AE()
                {
                    AEId = personId,
                    IsActive = true,
                    BeginDate = new DateTime(2014, 2, 12),
                    LastUpdated = DateTime.Now,
                    // EndDate = new DateTime(2014, 3, 12),
                    UpdatedBy = "Stephen",

                };
                context.AEs.Add(ae);
                context.SaveChanges();

            }

        }

        private static void AddAECompany()
        {

            using (var context = new G16MemberContext())
            {
                int companId = 康和台北總公司Id();
                var companyDepartment = context.Companies.Find(companId);
                if (companyDepartment == null) return;

                int personId = 何金水PersonId();
                var person = context.People.Find(personId);
                if (person == null) return;


                var ae = (from a in context.AEs
                          where a.AEId == person.PersonId
                          select a).FirstOrDefault();
                if (ae == null) return;

                var companyAE = new CompanyAE()
                {
                    Company = companyDepartment,
                    StaffNumber = "95011",
                    IsActive = true,
                    BeginDate = new DateTime(2014, 3, 1),
                    //LastUpdated = DateTime.Now,
                    //UpdatedBy = "Stephen",
                };
             
                ae.CompanyRecords.Add(companyAE);
                context.SaveChanges();


            }



        }

        private static void AddAECodeFromCompany()
        {
            var aeCode = new AECode()
            {
                Code = "z0077",
                BusinessType = BusinessType.Futures,
                IsActive = true,

            };

            using (var context = new G16MemberContext())
            {
               
                int companId = 康和台北總公司Id();
                var companyDepartment = context.Companies.Find(companId);
                if (companyDepartment == null) return;

                if (companyDepartment.AECodes == null) companyDepartment.AECodes = new List<AECode>();
                companyDepartment.AECodes.Add(aeCode);
                context.SaveChanges();

            }



        }

        private static void AddCodeToAE()
        {
            int companyId = 康和台北總公司Id();
            if (companyId == 0)
            {
                ShowMessage("companyId=0");
                return;
            }
                
              
            int personId = 何金水PersonId();
            if (personId == 0)
            {
                ShowMessage("PersonId=0");
                return;
            }
            using (var context = new G16MemberContext())
            {
                var company = context.Companies.Find(companyId);
                
                context.Entry(company).Collection(c => c.AECodes).Load();

                var aeCode = company.AECodes.Where(ac => ac.Code == "z0077").FirstOrDefault();
                if (aeCode == null)
                {
                    ShowMessage("aeCode=null");
                    return;
                }

                var ae = (from a in context.AEs
                          where a.AEId == personId
                          select a).FirstOrDefault();
                if (ae == null)
                {
                    ShowMessage("ae=null");
                    return;
                }
                var aeCodeRecord = new AECodeAE()
                {
                    AECodeId = aeCode.AECodeId,
                    IsActive = true,
                    BeginDate = new DateTime(2014, 3, 1),
                    //LastUpdated = DateTime.Now,
                    //UpdatedBy = "Stephen",

                };

                ae.AECodeRecords.Add(aeCodeRecord);
                context.SaveChanges();

            }
        }



        private static void InsertFuturesAccountWithContactInfo()
        {
            var account = new FuturesAccount()
            {
                AccountType = BusinessType.Futures,
                AccountNumber = "9800312",
                Status = AccountStatus.Active,
                OpenDate = new DateTime(2014, 2, 4),

            };

            int companyId = 康和台北總公司Id();
            int personId = 何金水PersonId();
          

           
            var accountContactInfo = new AccountContactInfo()
            {
                TEL = "02-27170305",
                Email = "opmart2008@yahoo.com.tw",
                Phone = "0936060049",
                CensusAddress = new Address()
                {
                    StreetAddress = "復興北路143號5樓",
                    //City = "台北市",
                    //ZipCode = "10547",
                    //District = "松山區"
                },
                ContactAddress = new Address()
                {
                    StreetAddress = "信義路三段70號5樓",
                    //City = "台北市",
                    //ZipCode = "106",
                    //District = "大安區"
                },

            };

            account.ContactInfo = accountContactInfo;
            account.CompanyBranchId = companyId;
            account.PersonId = personId;
            using (var context = new G16MemberContext())
            {
                context.Accounts.Add(account);
                context.SaveChanges();
            }

        }

        private static void InsertAccountBankInfo()
        {
            int accountId = 何金水AccountId();
          

            AccountBankInfo withdrawInfo = new AccountBankInfo()
            {
                AccountId = accountId,
                BankName = "第一銀行",
                BankBranch = "長春分行",
                Title = "何金銀",
                AccountNumber = "14950228990",
                Currency = BankAccountCurrency.TWD,
                Type = BankInfoType.Withdraw
            };
            AccountBankInfo depositInfoA = new AccountBankInfo()
            {
                AccountId = accountId,
                BankName = "第一銀行",
                BankBranch = "長春分行",
                Title = "何金銀",
                AccountNumber = "14950228990",
                Currency = BankAccountCurrency.TWD,
                Type = BankInfoType.Deposit
            };
            AccountBankInfo depositInfoB = new AccountBankInfo()
            {
                AccountId = accountId,
                BankName = "聯邦銀行",
                BankBranch = "雙和分行",

                AccountNumber = "689959700",
                Currency = BankAccountCurrency.TWD,
                Type = BankInfoType.Deposit
            };

            using (var context = new G16MemberContext())
            {
                var acc = context.Accounts.Find(accountId);
                if (acc == null) return;
                if (acc.AccountBankInfos == null)
                {
                    acc.AccountBankInfos = new List<AccountBankInfo>();
                }

                acc.AccountBankInfos.Add(withdrawInfo);
                acc.AccountBankInfos.Add(depositInfoA);
                acc.AccountBankInfos.Add(depositInfoB);
                context.SaveChanges();

            }

        }



        private static void ShowCompanyActiveAEs()
        {
            string str = "";
            int companyId = 康和台北總公司Id();
            using (var context = new G16MemberContext())
            {
                var code = new AECode();

                var company = context.Companies.Find(companyId);
                context.Entry(company).Collection(c => c.AERecords).Load();
                foreach (var a in company.AERecords)
                {
                    context.Entry(a).Reference(ae => ae.AE).Load();

                    var theAE = a.AE;
                    context.Entry(theAE).Reference(p => p.Person).Load();

                    str += theAE.Person.Name;
                  //  str += (a.AE.IsActive.ToString());
                }
            }

            ShowMessage(str);
        }

        private static void InsertAccountAECode()
        {
            int aeCodeId= z0077AECodeId();
            int accountId = 何金水AccountId();

            var accountAECode = new AccountAECode()
            {
                AECodeId = aeCodeId,
                IsActive=true,
                BeginDate = new DateTime(2014, 3, 1),
                //LastUpdated = DateTime.Now,
                //UpdatedBy = 0,

            };

            using (var context = new G16MemberContext())
            {
                var account = context.Accounts.Find(accountId);
                account.AECodeRecords.Add(accountAECode);
                context.SaveChanges();
            }

        }

        private static void DeletePerson(int personId)
        {
            using (var context = new G16MemberContext())
            {
                var person = new Person { PersonId = personId };
                context.Entry(person).State = EntityState.Deleted;
               
                context.SaveChanges();
            }
        }



        //public class AccountAECode
        //{
        //    public int AccountId { get; set; }
        //    public int AECodeId { get; set; }

        //    public Account Account { get; set; }
        //    public AECode AECode { get; set; }
        //    public bool IsActive { get; set; }

        //    public DateTime BeginDate { get; set; }
        //    public DateTime EndDate { get; set; }
        //    public DateTime LastUpdated { get; set; }
        //    public string UpdatedBy { get; set; }
        //}
        //private static void InsertAECodes()
        //{

        //    var aeCode = new AECode()
        //    {
        //        Code="z0077",
        //        BusinessType=BusinessType.Futures,
        //        IsActive=true
        //    };
        //    using (var context = new G16MemberContext())
        //    {
        //        var concordsCompany = (from c in context.Companies
        //                               where c.Name == "康和期貨"
        //                               select c).FirstOrDefault();
        //        if (concordsCompany == null) return;

        //        var taipeiBranch = (from c in context.Companies
        //                            where c.Name == "台北總公司" && c.ParentCompanyId == concordsCompany.CompanyId
        //                            select c).FirstOrDefault();
        //        if (taipeiBranch == null) return;
        //        aeCode.CompanyId = taipeiBranch.CompanyId;

        //        var person = (from p in context.People
        //                      where p.UserName == "stephenchoe"
        //                      select p).FirstOrDefault();
        //        if (person == null) return;


        //        var ae = (from a in context.AEs
        //                  where a.PersonId == person.PersonId && a.CompanyId == aeCode.CompanyId
        //                  select a).FirstOrDefault();
        //        if (ae == null) return;

        //        aeCode.AEId = ae.AEId;
        //    }

        //    using (var context = new G16MemberContext())
        //    {
        //        var company = context.Companies.Find(aeCode.CompanyId);
        //        if(company==null) return;
        //        if(company.AECodes==null) company.AECodes=new List<AECode>();

        //        company.AECodes.Add(aeCode);
        //        context.SaveChanges();
        //    }

        //}


    }
}
