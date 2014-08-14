using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace G16_2013.Models.MemberModel
{
    public class TestFuturesAccount
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AccountNum { get; set; }       
        public string AccountEmail { get; set; }

        public TestBankInfoInput TWOutMoneyBank { get; set; }
        public List<TestBankInfoInput> TWInMoneyBanks { get; set; }
        public TestForexBankInfoInput ForexOutMoneyBank { get; set; }   
        public List<TestForexBankInfoInput> ForexInMoneyBanks { get; set; }

    }

    public class TestAccountInput
    {
        public int AccountTypeId { get; set; }
        public List<BaseOption> AccountTypeOptions { get; set; }
        public string AccountNum { get; set; }
        public int CompanyId { get; set; }
        public List<BaseOption> CompanyOptions { get; set; }
    }
    public class TestPersonInput
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string TWID { get; set; }
    }
    public class TestPersonContactInfoInput
    {
        public int PersonId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class TestAccountContactInfoInput
    {
        public string Phone { get; set; }
         public string Email { get; set; }
    }
    public class TestBankInfoInput
    {
        public int Serial { get; set; }
        public bool IsIn { get; set; }
           [Display(Name = "名稱")]
        public string BankName { get; set; }
           [Display(Name = "帳號")]
        public string BankNumber { get; set; }
         public bool AllowDelete { get; set; }
      
    }
    public class TestForexBankInfoInput : TestBankInfoInput
    {
        public int CurrencyId { get; set; }
        public List<BaseOption> CurrencyOptions { get; set; }
    }
    public class TestFuturesAccountInput
    {
        public TestPersonInput PersonInput { get; set; }
        public TestPersonContactInfoInput PersonContactInfoInput { get; set; }
        public TestAccountInput AccountInput { get; set; }
        public TestAccountContactInfoInput AccountContactInfoInput { get; set; }

        public TestBankInfoInput TWOutMoneyBank { get; set; }
        public List<TestBankInfoInput> TWInMoneyBanks { get; set; }

        public bool HasForexOutMoneyBank { get; set; }
        public TestForexBankInfoInput ForexOutMoneyBank { get; set; }
        public bool HasForexInMoneyBanks { get; set; }
        public List<TestForexBankInfoInput> ForexInMoneyBanks { get; set; }
        
    }
}
