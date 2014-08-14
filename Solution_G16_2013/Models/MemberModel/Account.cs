using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace G16_2013.Models.MemberModel
{
    public  class Account : BaseEntity
    {
        public int AccountId { get; set; }
        public BusinessType AccountType { get; set; }
        public string AccountNumber { get; set; }
        //分公司
        public int CompanyBranchId { get; set; }
        public virtual Company CompanyBranch { get; set; }
       
        public int PersonId { get; set; }
        public virtual Person Owner { get; set; }
        //介紹人 
        public Nullable<int> ReferralId { get; set; }
        public virtual Person Referral { get; set; }
        //操盤人
        public Nullable<int> TraderId { get; set; }
        public virtual Person Trader { get; set; }
        public Nullable<int> OpenAEId { get; set; }
        public virtual AE OpenAE { get; set; }
        public int ServiceAEId { get; set; }
        public virtual AE ServiceAE { get; set; }
        //開戶日期
        public DateTime ?OpenDate { get; set; }
        public AccountStatus Status { get; set; }

        public DateTime? CancelDate { get; set; }

        public int AECodeId { get; set; }
        public virtual AECode AECode { get; set; }
       

        //public virtual AECode CurrentAECode
        //{
        //    get
        //    {
        //        if (AECodeRecords == null) return null;
        //        var record= AECodeRecords.Where(r => r.IsActive == true).FirstOrDefault();
        //        if (record == null) return null;
        //        return record.AECode;
        //    }
           
        //}
        //聯絡資料

        public virtual AccountContactInfo ContactInfo { get; set; }
        public virtual ICollection<AccountBankInfo> AccountBankInfos { get; set; }

        public virtual ICollection<AccountAE> ServiceAERecords { get; set; }
        public virtual ICollection<AccountAECode> AECodeRecords { get; set; }
        public virtual ICollection<AccountTrader> TraderRecords { get; set; }
        //public Account()
        //{
        //    //ContactInfo = new AccountContactInfo();
        //    //AccountBankInfos = new List<AccountBankInfo>();

        //    //AECodeRecords = new List<AccountAECode>();
        //    //ServiceAERecords = new List<AccountAE>();
        //    //TraderRecords = new List<AccountTrader>();
        //}

        public AccountTrader GetCurrentTraderRecord()
        {
            if (TraderRecords == null) return null;
            var traderRecord=(from record in TraderRecords
                       where record.IsActive &&                      
                       (record.EndDate == null || record.EndDate > DateTime.Now)
                         orderby record.LastUpdated
                         select record).FirstOrDefault();
            
            return traderRecord ?? null;                     
        }

       
        

    }//end class
    public class AccountAE : BaseEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int AEId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }


        public virtual Account Account { get; set; }
        public virtual AE AE { get; set; }
    }
    public  class AccountAECode : BaseEntity
    {
        public int Id { get; set; }     
        public int AccountId { get; set; }
        public int AECodeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }


        public virtual Account Account { get; set; }
        public virtual AECode AECode { get; set; }
        
      
       
    }
    public class AccountTrader : BaseEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int TraderId { get; set; }
        public bool IsOfficial { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }


        public virtual Account Account { get; set; }
        public virtual Person Trader { get; set; }
    }


    public  class FuturesAccount : Account
    {
        public FuturesAccount()
        {
            AccountType = BusinessType.Futures;
        }

        //ICollection<AccountBankInfo> GetActiveAccountBankInfos()
        //{
        //    if (AccountBankInfos == null) return null;
        //    return (from record in AccountBankInfos
        //            where record.Removed != true
        //            select record).ToList();
        //}

        public AccountBankInfo GetTaiwanWithdrawBank()
        {
            if (AccountBankInfos == null) return null;
            var bankInfo = (from record in AccountBankInfos
                            where record.Removed != true && record.Type==BankInfoType.Withdraw
                            && record.Currency == BankAccountCurrency.TWD
                            orderby record.LastUpdated
                            select record).FirstOrDefault();
            return bankInfo;
        }
        public AccountBankInfo GetForexWithdrawBank()
        {
            if (AccountBankInfos == null) return null;
            var bankInfo = (from record in AccountBankInfos
                            where record.Removed != true && record.Type == BankInfoType.Withdraw
                            && record.Currency != BankAccountCurrency.TWD
                            orderby record.LastUpdated
                            select record).FirstOrDefault();
            return bankInfo;
        }

        public List<AccountBankInfo> GetTWDepositBankList()
        {
            if (AccountBankInfos == null) return null;
            var bankInfos = from record in AccountBankInfos
                            where record.Removed != true && record.Type == BankInfoType.Deposit
                            && record.Currency == BankAccountCurrency.TWD
                            orderby record.LastUpdated
                            select record;
            if (bankInfos == null) return null;
            return bankInfos.ToList();
        }
        public List<AccountBankInfo> GetForexDepositBankList()
        {
            if (AccountBankInfos == null) return null;
            var bankInfos = from record in AccountBankInfos
                            where record.Removed != true && record.Type == BankInfoType.Deposit
                            && record.Currency != BankAccountCurrency.TWD
                            orderby record.LastUpdated
                            select record;
            if (bankInfos == null) return null;
            return bankInfos.ToList();
        }
    }
    public  class StockAccount : Account
    {
        
        public bool HasCredit { get; set; }
        public decimal FinancingQuota { get; set; }
        public decimal BorrowingQuota { get; set; }

        public StockAccount()
        {
            AccountType = BusinessType.Stock;
        }
    }
    public class AccountContactInfo : BaseEntity
    {
        public int AccountId { get; set; }
        public string Phone { get; set; }
        public string TEL { get; set; }
        public string Email { get; set; }
        public Address CensusAddress { get; set; }
        public Address ContactAddress { get; set; }
       
        public virtual Account ContactInformatonOf { get; set; }

    }

    
    public partial class AccountBankInfo:BaseEntity
    {
        public int AccountBankInfoId { get; set; }
        public int AccountId { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Title { get; set; }
        public string AccountNumber { get; set; }
        public string SwiftCode { get; set; }
        public BankAccountCurrency Currency { get; set; }
        public BankInfoType Type { get; set; }
       
        public Account BankInformationOf { get; set; }
        public AccountBankInfo()
        {
           
        }

        public AccountBankInfo(string name,string branch,int currency,string number)
        { 
                BankName = name;
                BankBranch = branch;
                AccountNumber =number;
                Currency = (BankAccountCurrency)currency;                             
        }
        //BankAccountCurrency GetBankAccountCurrencyByInput(string input)
        //{
        //    BankAccountCurrency currency = BankAccountCurrency.TWD;
        //    switch (input)
        //    {
        //        case "CompositeForex":
        //            currency = BankAccountCurrency.CompositeForex;
        //            break;
        //        case "USD":
        //            currency = BankAccountCurrency.USD;
        //            break;
        //        case "HKD":
        //            currency = BankAccountCurrency.USD;
        //            break;
        //        case "JPY":
        //            currency = BankAccountCurrency.USD;
        //            break;
        //    }
        //    return currency;
        //}
    }

    public enum AccountStatus
    { 
        Canceled,
        Active,
        Static

    }
    public class AccountStatusOption
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }


    public enum BankAccountCurrency
    {
        TWD,
        CompositeForex,
        USD,
        HKD,
        JPY,
        ERD
    }
    //public class BankAccountCurrencyOption : BaseOption
    //{
      
    //}
    public enum BankInfoType
    {
        Withdraw,
        Deposit
    }
    //public class BankInfoTypeOption : BaseOption
    //{

    //}


    public class BaseOption
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public BaseOption()
        {

        }
        public BaseOption(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }

    //public class CompanyOption : BaseOption
    //{ 
      
    //}


}
