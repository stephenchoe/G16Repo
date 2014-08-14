using G16_2013.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using G16_2013.DAL;


using System.Threading.Tasks;

namespace G16_2013.BLL
{
    public class TestHelper
    {
        public static void IniDBData(string dataFilePath)
        {
            InsertCity(dataFilePath);
            InsertCompany();
           
            InsertPeople();
            InsertAE();
            InsertIdentities();
            string aeName = "何金銀";
            string companyName = "台北總公司";
            string staffNumber = "95011";
            DateTime beginDate = new DateTime(2010, 7, 1);
            DesignateAECompany(aeName, companyName, staffNumber, beginDate);
            aeName = "何金水";
            companyName = "台中分公司";
            staffNumber = "98009";
            beginDate = new DateTime(2012, 3, 1);
            DesignateAECompany(aeName, companyName, staffNumber, beginDate);

            string code = "z0077";
            companyName = "台北總公司";
            InsertAECode(code, companyName, BusinessType.Futures);
            code = "s0065";
            companyName = "台北總公司";
            InsertAECode(code, companyName, BusinessType.Stock);
            code = "z0145";
            companyName = "台中分公司";
            InsertAECode(code, companyName, BusinessType.Futures);
            beginDate = new DateTime(2012, 3, 5);

            DesignateAECodeToAE(code, companyName, aeName, beginDate);

            code = "z0077";
            companyName = "台北總公司";
            aeName = "何金銀";
            beginDate = new DateTime(2010, 8, 1);
            DesignateAECodeToAE(code, companyName, aeName, beginDate);

            code = "s0065";
            companyName = "台北總公司";
            aeName = "何金銀";
            beginDate = new DateTime(2013, 9, 1);
            DesignateAECodeToAE(code, companyName, aeName, beginDate);


            string accountNumber = "9800312";
            string ownerName = "何金銀";
            string openAEName = "何金銀";
            companyName = "台北總公司";
            DateTime openDate = new DateTime(2013, 8, 5);
            string aeCode = "z0077";
            string referralName = "";
            string traderName = "";
            InsertFuturesAccount(accountNumber, ownerName, openAEName,
                companyName, openDate, aeCode, referralName, traderName);

            accountNumber = "8535678";
            ownerName = "蔡明義";
            openAEName = "何金銀";
            openDate = new DateTime(2012, 2, 19);
            referralName = "陳文茜";
            InsertFuturesAccount(accountNumber, ownerName, openAEName,
               companyName, openDate, aeCode, referralName, traderName);

            accountNumber = "8510068";
            ownerName = "丁遠超";
            openAEName = "何金水";
            companyName = "台中分公司";
            openDate = new DateTime(2011, 9, 29);
            aeCode = "z0145";
            referralName = "蔡明義";
            traderName = "林義守";
            InsertFuturesAccount(accountNumber, ownerName, openAEName,
                companyName, openDate, aeCode, referralName, traderName);

            accountNumber = "8532789";
            ownerName = "簡欽海";
            openAEName = "何金水";
            companyName = "台中分公司";
            openDate = new DateTime(2009, 1, 13);
            aeCode = "z0145";
            referralName = "";
            traderName = "林義守";
            InsertFuturesAccount(accountNumber, ownerName, openAEName,
                companyName, openDate, aeCode, referralName, traderName);

            accountNumber = "8556732";
            ownerName = "潘正雄";
            openAEName = "何金銀";
            companyName = "台北總公司";
            openDate = new DateTime(2013, 4, 1);
            aeCode = "z0077";
            referralName = "簡欽海";
            traderName = "";
            InsertFuturesAccount(accountNumber, ownerName, openAEName,
                companyName, openDate, aeCode, referralName, traderName);

            accountNumber = "8589767";
            ownerName = "陳啟明";
            openAEName = "何金銀";
            companyName = "台北總公司";
            openDate = new DateTime(2011, 8, 11);
            aeCode = "z0077";
            referralName = "丁遠超";
            traderName = "陳文茜";
            InsertFuturesAccount(accountNumber, ownerName, openAEName,
                companyName, openDate, aeCode, referralName, traderName);

            accountNumber = "8612660";
            ownerName = "涂瑞雄";
            openAEName = "何金銀";
            companyName = "台北總公司";
            openDate = new DateTime(2010, 1, 20);
            aeCode = "z0077";
            referralName = "";
            traderName = "";
            InsertFuturesAccount(accountNumber, ownerName, openAEName,
                companyName, openDate, aeCode, referralName, traderName);

            accountNumber = "8608231";
            ownerName = "李瑞儀";
            openAEName = "何金銀";
            companyName = "台北總公司";
            openDate = new DateTime(2014, 1, 12);
            aeCode = "z0077";
            referralName = "涂瑞雄";
            traderName = "";
            var futuresAccount = InsertFuturesAccount(accountNumber, ownerName, openAEName,
                 companyName, openDate, aeCode, referralName, traderName);

           

            //StockAccount
            accountNumber = "50912098";
            ownerName = "李瑞儀";
            openAEName = "何金銀";
            companyName = "台北總公司";
            openDate = new DateTime(2014, 3, 1);
            aeCode = "s0065";
            referralName = "涂瑞雄";
            traderName = "";
            InsertStockAccount(accountNumber, ownerName, openAEName,
                companyName, openDate, aeCode, referralName, traderName);

            accountNumber = "50077647";
            ownerName = "蔡明義";
            openAEName = "何金銀";
            companyName = "台北總公司";
            openDate = new DateTime(2013, 12, 11);
            aeCode = "s0065";
            referralName = "";
            traderName = "潘正雄";
            var stockAccount = InsertStockAccount(accountNumber, ownerName, openAEName,
                companyName, openDate, aeCode, referralName, traderName);
           
        }

        public void InsertRoles(string name)
        {
            string[] names = new string[] { "老闆", "業務員", "客戶" };
            using (var memberBL = new MemberBL())
            {
                //foreach (string name in names)
                //{
                    memberBL.InsertRole(name);

                //}

            }
        }

        static void InsertIdentities()
        {
            string[] names = new string[] { "操盤人", "介紹人",  };
            using (var context = new G16MemberContext())
            {
                foreach (string name in names)
                {
                    var identity = new Identity
                    {
                        Name = name
                    };
                    context.Identities.Add(identity);
                }

                context.SaveChanges();
            }
           
                                            
        }
        public static void InsertCity(string dataFilePath)
        {
            List<City> allCity = new List<City>();
            
            XmlDocument doc = new XmlDocument();
            doc.Load(dataFilePath);

            XmlNode root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                XmlElement element = (XmlElement)node;
                XmlAttribute attributeID = element.GetAttributeNode("ID");
                string id = attributeID.Value;
                XmlAttribute attributeName = element.GetAttributeNode("Name");
                string name = (attributeName.Value).Replace("臺", "台");
                XmlAttribute attributeTelCode = element.GetAttributeNode("TelCode");
                string telCode = attributeTelCode.Value;
                var city = new City
                {
                    Code = id,
                    Name = name,
                    TelCode = telCode
                };
               
                foreach (XmlNode item in node.ChildNodes)
                {
                    XmlElement itemElement = (XmlElement)item;
                    XmlAttribute districtNameAttribute = itemElement.GetAttributeNode("Name");
                    string districtName = (districtNameAttribute.Value).Replace("臺", "台");
                    XmlAttribute districtAttributeZip = itemElement.GetAttributeNode("Zip");
                    string districtZip = districtAttributeZip.Value;

                    District district = new District
                    {
                        Name = districtName,
                        ZipCode = districtZip
                    };
                   
                    city.Districts.Add(district);
                }
                
                allCity.Add(city);
            }

            using (var context = new G16MemberContext())
            {
                foreach (City c in allCity)
                {
                    context.Cities.Add(c);
                }
                context.SaveChanges();
            }
        }
        public static int InsertCompany()
        {

            var company = new G16_2013.Models.MemberModel.Company
            {
                CompanyType = BusinessType.Futures,
                ParentCompanyId=0,
                Name = "康和期貨",
                DisplayOrder = 1,
                BeginDate = DateTime.Now,
                
            };
            //using (MemberBL memberBL = new MemberBL())
            //{

            //    company = memberBL.InsertCompany(company);

            //}

            var branchTaipei = new G16_2013.Models.MemberModel.Company
            {
                CompanyType = BusinessType.Futures,
                ParentCompanyId = company.CompanyId,
                Name = "台北總公司",
                DisplayOrder = 1,
                BeginDate = DateTime.Now,
              
            };
            var branchTaichung = new G16_2013.Models.MemberModel.Company
            {
                CompanyType = BusinessType.Futures,
                ParentCompanyId = company.CompanyId,
                Name = "台中分公司",
                DisplayOrder = 2,
                BeginDate = DateTime.Now,
              
            };
            List<Company> listCompany = new List<Company>() {  branchTaipei, branchTaichung };
            int isOk = 0;
            using (MemberBL memberBL = new MemberBL())
            {

                //isOk = memberBL.InsertMultiCompany(listCompany);

            }

            return isOk;
        }

        public static int InsertPeople()
        {

           
            int isOk = 0;
            using (MemberBL memberBL = new MemberBL())
            {
                string[] names = new string[] { "何金銀", "何金水", "蔡明義",
                                            "陳西田","陳文茜","林義守",
                                              "丁遠超","簡欽海","鄭麗文",
                                            "潘正雄","陳啟明","涂瑞雄",
                                            "李瑞儀","劉國棟","廖偉國",};
                bool gender = true;
                string twid = "F";
                int id = 125670090;
                DateTime birthday = new DateTime(1980, 3, 25);
                int tel = 23389090;
                int phone = 935678904;

                List<Person> listPerson = new List<Person>();
                for (int i = 0; i < 15; i++)
                {
                    gender = !gender;
                    id++;
                    birthday = birthday.AddYears(i - 5);
                    birthday = birthday.AddDays(i);
                    var person = new Person()
                    {
                        Name = names[i],
                        Gender = gender,
                        TWID = twid + id,
                        Birthday = birthday
                    };
                    var contactInfo = new PersonContactInfo()
                    {
                        TEL = "02-" + (tel + i).ToString(),
                        Phone = "0" + (phone + i).ToString(),
                        Email = "stephen" + i.ToString() + "@yahoo.com.tw",

                    };

                    if (i % 2 == 0)
                    {
                        contactInfo.Address = new Address()
                        {
                            StreetAddress = "信義路三段70號5樓",
                            //City =memberBL.GetCityByName("台北市").CityId.ToString(),
                           
                            //ZipCode = "106",
                            //District = memberBL.GetDistrictByName("大安區").DistrictId.ToString(),
                           
                        };
                      
                    }
                    else
                    {
                        contactInfo.Address = new Address()
                        {
                            StreetAddress = "福和路278號2樓",
                            //City = memberBL.GetCityByName("新北市").CityId.ToString(),
                           
                            //ZipCode = "234",
                            //District = memberBL.GetDistrictByName("永和區").DistrictId.ToString()
                           
                        };
                     
                    }

                    person.ContactInfo = contactInfo;

                    listPerson.Add(person);

                }
                //isOk = memberBL.InsertMultiPeople(listPerson);

            }

            return isOk;
        }

        static int GetRandomNumber(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }

        public static int InsertAE()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var person_A = memberBL.GetPersonByName("何金銀");
                var aeNumberOne = new AE()
                {
                    Person = person_A,
                    IsActive = true,
                    BeginDate = new DateTime(2014, 2, 12),
                    LastUpdated = DateTime.Now,
                    //DisplayOrder = 0,
                    UpdatedBy = "Stephen",
                };
                var person_B = memberBL.GetPersonByName("何金水");
                var aeNumberTwo = new AE()
                {
                    Person = person_B,
                    IsActive = true,
                    BeginDate = new DateTime(2013, 1, 1),
                    LastUpdated = DateTime.Now,
                    //DisplayOrder = 0,
                    UpdatedBy = "Stephen",
                };
                List<AE> listAE = new List<AE>() { aeNumberOne, aeNumberTwo };

                //return memberBL.InsertMultiAE(listAE);
                return 0;
            }

        }

        public static int DesignateAECompany(string personName, string companyName, string staffNumber, DateTime beginDate)
        {
            return 0;
            //using (MemberBL memberBL = new MemberBL())
            //{
            //    var ae = memberBL.GetAEByName(personName);
            //    if (ae == null) return 0;
            //    var company = memberBL.GetCompanyByName(companyName);
            //    if (company == null) return 0;

            //    var companyAE = new CompanyAE
            //    {
            //        AE = ae,
            //        Company = company,
            //        StaffNumber = staffNumber,
            //        IsActive = true,
            //        BeginDate = beginDate,
                   
            //    };
            //    //companyAE = memberBL.InsertCompanyAE(companyAE);
            //    return companyAE == null ? 0 : 1;
            //}
        }

        public static int InsertAECode(string code, string companyName, BusinessType type)
        {
            //using (MemberBL memberBL = new MemberBL())
            //{
            //    var company = memberBL.GetCompanyByName(companyName);
            //    if (company == null) return 0;

            //    var aeCode = new AECode
            //    {
            //        Code = code,
            //        BusinessType = type,
            //        Company = company,
            //        IsActive = true
            //    };
            //    aeCode = memberBL.InsertAECode(aeCode);
            //    return aeCode == null ? 0 : 1;

            //}
            return 0;
        }
        public static int DesignateAECodeToAE(string code, string companyName, string AEName, DateTime beginDate)
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var company = memberBL.GetCompanyByName(companyName);
                if (company == null) return 0;
                var aeCode = memberBL.GetAECodeByCode(code, company);
                if (aeCode == null) return 0;
                var ae = memberBL.GetAEByName(AEName);
                if (ae == null) return 0;

                var record = new AECodeAE
                {
                    Code = aeCode,
                    IsActive = true,
                    BeginDate = beginDate,
                   
                };

                ae.AECodeRecords.Add(record);
              //  ae = memberBL.UpdateAE(ae);
                return ae == null ? 0 : 1;

            }
        }

        public static FuturesAccount InsertFuturesAccount
                (string accountNumber, string ownerName, string 
                openAEName, string companyName, DateTime openDate, string aeCode, 
                string referralName, string traderName)
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var person = memberBL.GetPersonByName(ownerName);
                var companyBranch=memberBL.GetCompanyByName(companyName);
                var futuresAccount = new FuturesAccount
                {
                    AccountType = BusinessType.Futures,
                    AccountNumber = accountNumber,
                    CompanyBranch = companyBranch as Company,
                    Owner = person,
                    OpenAE = memberBL.GetAEByName(openAEName),
                    OpenDate = openDate,
                    AECode = memberBL.GetAECodeByCode(aeCode, companyBranch),
                    Status = AccountStatus.Active,
                    ContactInfo = new AccountContactInfo
                    {
                       Phone = "0936060049",
                       TEL = "02-27170305",
                       Email = "opmart2008@yahoo.com.tw",
                       CensusAddress = new Address
                       {
                           StreetAddress = "信義路三段70號5樓",
                           //City = memberBL.GetCityByName("台北市").CityId.ToString(),
                           //ZipCode = "106",
                           //District = memberBL.GetDistrictByName("大安區").DistrictId.ToString(),
                       },
                       ContactAddress = person.ContactInfo.Address
                     },
              
                };
                if (referralName != "")
                {
                    var referral = memberBL.GetPersonByName(referralName);
                    if (referral != null)
                    {
                        futuresAccount.Referral = referral;
                    }
                }
                if (traderName != "")
                {
                    var trader = memberBL.GetPersonByName(traderName);
                    if (trader != null)
                    {
                        futuresAccount.Trader = trader;
                    }
                }
                
                var accountAECode = new AccountAECode
                {                    
                    AECode = memberBL.GetAECodeByCode(aeCode, companyBranch),
                    BeginDate = openDate,
                    IsActive = true,               
                };
                futuresAccount.AECodeRecords = new List<AccountAECode>();
                futuresAccount.AECodeRecords.Add(accountAECode);

                AccountBankInfo withdrawBankInfo = new AccountBankInfo
                {
                    Currency = BankAccountCurrency.TWD,
                    BankName = "中國信託",
                    Title = ownerName,
                    BankBranch = "雙和分行",
                    AccountNumber = "556468900877",
                    Type=BankInfoType.Withdraw
                };
                futuresAccount.AccountBankInfos = new List<AccountBankInfo>();
                futuresAccount.AccountBankInfos.Add(withdrawBankInfo);
                AccountBankInfo depositBankInfo_A = new AccountBankInfo
                {
                    Currency = BankAccountCurrency.TWD,
                    BankName = "中國信託",
                    Title = ownerName,
                    BankBranch = "雙和分行",
                    AccountNumber = "556468900877",
                    Type = BankInfoType.Deposit
                };
                futuresAccount.AccountBankInfos.Add(depositBankInfo_A);
                AccountBankInfo depositBankInfo_B = new AccountBankInfo
                {
                    Currency = BankAccountCurrency.TWD,
                    BankName = "第一銀行",
                    Title = ownerName,
                    BankBranch = "東門分行",
                    AccountNumber = "167900987767",
                    Type = BankInfoType.Deposit
                };
                futuresAccount.AccountBankInfos.Add(depositBankInfo_B);

             //return   (memberBL.InsertAccount(futuresAccount)) as FuturesAccount;
                return null;
            }

        }
        public static StockAccount InsertStockAccount(string accountNumber, string ownerName, string
                openAEName, string companyName, DateTime openDate, string aeCode,
                string referralName, string traderName)
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var person = memberBL.GetPersonByName(ownerName);
                var companyBranch = memberBL.GetCompanyByName(companyName);
                var stockAccount = new StockAccount
                {
                    AccountType = BusinessType.Stock,
                    AccountNumber = accountNumber,
                    CompanyBranch = companyBranch,
                    Owner = person,
                    OpenAE = memberBL.GetAEByName(openAEName),
                    OpenDate = openDate,
                    Status = AccountStatus.Active,
                    ContactInfo = new AccountContactInfo
                    {
                        Phone = "0936060049",
                        TEL = "02-27170305",
                        Email = "opmart2008@yahoo.com.tw",
                        CensusAddress = new Address
                        {
                            StreetAddress = "信義路三段70號5樓",
                            //City = memberBL.GetCityByName("台北市").CityId.ToString(),
                            //ZipCode = "106",
                            //District = memberBL.GetDistrictByName("大安區").DistrictId.ToString(),
                        },
                        ContactAddress = person.ContactInfo.Address
                    },

                    HasCredit = true,
                    FinancingQuota = 50,
                    BorrowingQuota = 1000,
                       

                };

                AccountBankInfo clearBankInfo = new AccountBankInfo
                {
                    Currency = BankAccountCurrency.TWD,
                    BankName = "中國信託",
                    Title = ownerName,
                    BankBranch = "雙和分行",
                    AccountNumber = "556468900877",
                    Type = BankInfoType.Withdraw
                };
                stockAccount.AccountBankInfos.Add(clearBankInfo);
                return null;
                //return (memberBL.InsertAccount(stockAccount)) as StockAccount;
            }
        }



    }
}
