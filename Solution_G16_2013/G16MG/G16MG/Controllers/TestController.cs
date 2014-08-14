using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using G16_2013.Models.MemberModel;

namespace G16MG.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cancel()
        {
            return View();
        }
        public ActionResult Create()
        {
            TestFuturesAccountInput input = new TestFuturesAccountInput()
            {
                PersonInput= new TestPersonInput(),
                PersonContactInfoInput= new TestPersonContactInfoInput(),
                AccountContactInfoInput=new TestAccountContactInfoInput(),
                TWOutMoneyBank=new TestBankInfoInput(),
                TWInMoneyBanks=new List<TestBankInfoInput>()

            };
            input.AccountInput = new TestAccountInput();
            input.AccountInput.AccountTypeOptions = new List<BaseOption>();
            input.AccountInput.AccountTypeOptions.Add(new BaseOption { Text = "期貨", Value = "0" });
            input.AccountInput.AccountTypeOptions.Add(new BaseOption { Text = "股票", Value = "1" });

            input.AccountInput.CompanyOptions = new List<BaseOption>();
            input.AccountInput.CompanyOptions.Add(new BaseOption { Text = "康和", Value = "0" });
            input.AccountInput.CompanyOptions.Add(new BaseOption { Text = "大昌", Value = "1" });
           
            TestBankInfoInput TWInMoneyBank = new TestBankInfoInput()
            {
                Serial=1,
                IsIn=true                
            };
            input.TWInMoneyBanks.Add(TWInMoneyBank);
           


            return View(input);
        }

        [HttpPost]
        public ActionResult Create(TestFuturesAccountInput input,string Command)
        {
            if (Command == "Continue")
            {
                string phone = input.PersonContactInfoInput.Phone;
                string email = input.PersonContactInfoInput.Email;

                ModelState.Remove("AccountContactInfoInput.Phone");
                ModelState.Remove("AccountContactInfoInput.Email");
                input.AccountContactInfoInput.Email = email;
                input.AccountContactInfoInput.Phone = phone;

                input.AccountInput.AccountTypeOptions = new List<BaseOption>();
                input.AccountInput.AccountTypeOptions.Add(new BaseOption { Text = "期貨", Value = "0" });
                input.AccountInput.AccountTypeOptions.Add(new BaseOption { Text = "股票", Value = "1" });

                input.AccountInput.CompanyOptions = new List<BaseOption>();
                input.AccountInput.CompanyOptions.Add(new BaseOption { Text = "康和", Value = "0" });
                input.AccountInput.CompanyOptions.Add(new BaseOption { Text = "大昌", Value = "1" });
                return View(input);
            }
            else if (Command == "AddTWInMoneyBank")
            {
                int index = input.TWInMoneyBanks.Count;
                if (index < 3)
                {
                    TestBankInfoInput TWInMoneyBank = new TestBankInfoInput()
                    {
                        Serial = index + 1,
                        IsIn = true,
                        AllowDelete = true
                    };
                    input.TWInMoneyBanks.Add(TWInMoneyBank);
                }
               
            }
            else if (Command == "AddForexOutBank")
            {
                input.ForexOutMoneyBank = new TestForexBankInfoInput()
                {
                    Serial = 1,
                    IsIn = false,
                    AllowDelete = true
                };
             
            
            }
            else if (Command == "AddForexInBank")
            {
                if (input.ForexInMoneyBanks == null) input.ForexInMoneyBanks = new List<TestForexBankInfoInput>();
                int index = input.ForexInMoneyBanks.Count;
                if (index < 3)
                {
                    TestForexBankInfoInput forexBankInfoInput = new TestForexBankInfoInput()
                    {
                        Serial = index + 1,
                        IsIn = true,
                        AllowDelete = true
                    };
                    input.ForexInMoneyBanks.Add(forexBankInfoInput);
                }
               
               

            }
            else if (Command == "Submit")
            {
                TestFuturesAccount futuresAccount = new TestFuturesAccount();
                futuresAccount.Name = input.PersonInput.Name;
                futuresAccount.Phone = input.PersonContactInfoInput.Phone;
                futuresAccount.AccountNum = input.AccountInput.AccountNum;
                futuresAccount.AccountEmail = input.AccountContactInfoInput.Email;
                futuresAccount.TWOutMoneyBank = new TestBankInfoInput();
                futuresAccount.TWOutMoneyBank.BankName = input.TWOutMoneyBank.BankName;
                futuresAccount.TWOutMoneyBank.BankNumber = input.TWOutMoneyBank.BankNumber;

                futuresAccount.TWInMoneyBanks = new List<TestBankInfoInput>();
                foreach (var item in input.TWInMoneyBanks)
                {
                    TestBankInfoInput bank = new TestBankInfoInput
                    {
                        BankName = item.BankName,
                        BankNumber = item.BankNumber
                    };
                    futuresAccount.TWInMoneyBanks.Add(bank);
                }

                if (input.ForexOutMoneyBank != null)
                {
                    futuresAccount.ForexOutMoneyBank = new TestForexBankInfoInput();
                    futuresAccount.ForexOutMoneyBank.BankName = input.ForexOutMoneyBank.BankName;
                    futuresAccount.ForexOutMoneyBank.BankNumber = input.ForexOutMoneyBank.BankNumber;
                    futuresAccount.ForexOutMoneyBank.CurrencyId = input.ForexOutMoneyBank.CurrencyId;
                }
                if (input.ForexInMoneyBanks != null)
                {
                    futuresAccount.ForexInMoneyBanks = new List<TestForexBankInfoInput>();

                    foreach (var item in input.ForexInMoneyBanks)
                    {
                        TestForexBankInfoInput bank = new TestForexBankInfoInput()
                        {
                            BankName = item.BankName,
                            BankNumber = item.BankNumber,
                            CurrencyId = item.CurrencyId
                        };
                        futuresAccount.ForexInMoneyBanks.Add(bank);
                    }
                    
                }


                return View("Test", futuresAccount);
            }


            input.AccountInput.AccountTypeOptions = new List<BaseOption>();
            input.AccountInput.AccountTypeOptions.Add(new BaseOption { Text = "期貨", Value = "0" });
            input.AccountInput.AccountTypeOptions.Add(new BaseOption { Text = "股票", Value = "1" });

            input.AccountInput.CompanyOptions = new List<BaseOption>();
            input.AccountInput.CompanyOptions.Add(new BaseOption { Text = "康和", Value = "0" });
            input.AccountInput.CompanyOptions.Add(new BaseOption { Text = "大昌", Value = "1" });

            if (input.ForexOutMoneyBank != null)
            {
                input.ForexOutMoneyBank.CurrencyOptions = new List<BaseOption>();
                input.ForexOutMoneyBank.CurrencyOptions.Add(new BaseOption { Text = "美元", Value = "0" });
                input.ForexOutMoneyBank.CurrencyOptions.Add(new BaseOption { Text = "宗和外幣", Value = "1" });
            }
            if (input.ForexInMoneyBanks != null)
            {
                foreach (var item in input.ForexInMoneyBanks)
                {
                    item.CurrencyOptions = new List<BaseOption>();
                    item.CurrencyOptions.Add(new BaseOption { Text = "美元", Value = "0" });
                    item.CurrencyOptions.Add(new BaseOption { Text = "宗和外幣", Value = "1" });
                }
               
            }

            return View(input);
        }



    }
}
