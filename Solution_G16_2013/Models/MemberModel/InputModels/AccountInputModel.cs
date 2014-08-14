using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace G16_2013.Models.MemberModel
{
   
   public class AccountInputModel
    {
       public int AccountId { get; set; }
       [Display(Name = "帳戶類型")]
       [Required]
       public int AccountType { get; set; }
       [Display(Name = "帳號")]
       [Required(ErrorMessage="請填寫帳號")]
       public string AccountNumber { get; set; }
         [Display(Name = "公司")]
       public int CompanyId { get; set; }
       public int BranchId { get; set; }
        [Display(Name = "戶名")]
       public int PersonId { get; set; }
        [Display(Name = "戶名")]
       public string OwnerName { get; set; }
        [Display(Name = "介紹人")]
       public int ReferralId { get; set; }
       public int TraderId { get; set; }

       public bool OfficialTrader { get; set; }
        [Display(Name = "開戶AE")]
       public int  OpenAEId { get; set; }
        [Display(Name = "服務AE")]
       public int ServiceAEId { get; set; }

       [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
       [Display(Name = "開戶日期")]
       public DateTime? OpenDate { get; set; }
       public int Status { get; set; }
         [Display(Name = "營業員代碼")]
       public int AECodeId { get; set; }

     
       public PersonInputModel PersonInput { get; set; }
       public AccountContactInfoInputModel ContactInfoInput { get; set; }


       #region Options
       public List<BaseOption> AccountTypeOptions { get; set; }
       public List<BaseOption> CompanyOptions { get; set; }
       public List<BaseOption> BranchOptions { get; set; }

       public List<BaseOption> PersonOptions { get; set; }
       public List<BaseOption> AEOptions { get; set; }
       public List<BaseOption> AECodeOptions { get; set; }
       public List<BaseOption> StatusOptions { get; set; }
       #endregion
    }
   public class AccountContactInfoInputModel : PersonContactInfoInputModel
   {
       public AddressInputModel CensusAddressInput { get; set; }
     
   }
   public class AccountContactInfoViewModel : AccountContactInfoInputModel
   {
       public AddressViewModel ContactAddressView { get; set; }
       public AddressViewModel CensusAddressView { get; set; }
       
     
   }

   public class FuturesAccountInputModel : AccountInputModel
   {
       #region 台幣出入金銀行

       public BankInfoInputModel TaiwanWithdrawBank{ get; set; }
     
     
       public List<BankInfoInputModel> TWDepositBanks { get; set; }
       
      
       //BankInfoInputModel _thirdTWDepositBank;
       //public BankInfoInputModel ThirdTWDepositBank
       //{
       //    get { return _thirdTWDepositBank; }
       //    set
       //    {
       //        value.CurrencyId = Convert.ToInt32(BankAccountCurrency.TWD);
       //        value.TypeId = Convert.ToInt32(BankInfoType.Deposit);
       //        _thirdTWDepositBank = value;
       //    }    
       //}
      
       #endregion
       #region 外幣出入金銀行
    
       public BankInfoInputModel ForexWithdrawBank { get; set; }

       public List<BankInfoInputModel> ForexDepositBanks{ get; set; }
     
      // BankInfoInputModel _thirdForexDepositBank;
       //public BankInfoInputModel ThirdForexDepositBank
       //{
       //    get { return _thirdForexDepositBank; }
       //    set
       //    {
       //        value.TypeId = Convert.ToInt32(BankInfoType.Deposit);
       //        _thirdForexDepositBank = value;
       //    }
       //}
       //public string ThirdForexDepositBankName { get; set; }
       //public string ThirdForexDepositBankBranch { get; set; }
       //public string ThirdForexDepositBankNumber { get; set; }
       #endregion
      
      
   }

   public class StockAccountInputModel : AccountInputModel
   {
       public bool HasCredit { get; set; }
       public int FinancingQuota { get; set; }
       public int BorrowingQuota { get; set; }
       public BankInfoInputModel DeliveryBank{ get; set; }
   }

   public class AccountViewModel : AccountInputModel
   {
       public string CompanyName { get; set; }
       public string BranchName { get; set; }
       public string ReferralName { get; set; }
       public string TraderName { get; set; }
       public string OpenAEName { get; set; }
       public string ServiceAEName { get; set; }
       public string OpenDateText { get; set; }
       public string StatusText { get; set; }
       public string AECodeText { get; set; }
       //public string CensusAddressText { get; set; }
       //public string ContactAddressText { get; set; }

       //public AccountViewModel(Account account)
       //{
       //    var parentCompanyId = account.CompanyBranch.ParentCompanyId;
       //    if (parentCompanyId == null || parentCompanyId == 0)
       //    {
       //        CompanyName = account.CompanyBranch.Name;
       //    }
       //    else
       //    { 
       //      CompanyName=
       //    }
       //}
       public AccountContactInfoViewModel ContactInfoView { get; set; }
   }
   public class FuturesAccountViewModel : AccountViewModel
   {
       public BankInfoViewModel TaiwanWithdrawBankView { get; set; }

       public List<BankInfoViewModel> TWDepositBankViewList { get; set; }


       public BankInfoViewModel ForexWithdrawBankView { get; set; }

       public List<BankInfoViewModel> ForexDepositBankViewList { get; set; }
     
   }
   public class StockAccountViewModel : AccountViewModel
   {
       public string FinancingQuota { get; set; }
       public string BorrowingQuota { get; set; }
       public BankInfoViewModel DeliveryBankView { get; set; }
   }


   public class BankInfoInputModel
   {

       public int Id { get; set; }
       public int AccountId { get; set; }
         [Display(Name = "銀行名稱")]
       public string BankName { get; set; }
        [Display(Name = "分行名稱")]
       public string BankBranch { get; set; }
       public string Title { get; set; }
       public string AccountNumber { get; set; }
       public string SwiftCode { get; set; }
          [Display(Name = "幣別")]
       public int CurrencyId { get; set; }
         [Display(Name = "出金/入金")]
       public int TypeId { get; set; }

       public List<BaseOption> CurrencyOptions { get; set; }
       public List<BaseOption> TypeOptions { get; set; }

       public int Serial { get; set; }
       public bool AllowDelete { get; set; }
   }

   public class BankInfoViewModel : BankInfoInputModel
   {
       public string TypeText { get; set; }
       public string CurrencyText { get; set; }
   }

   
  

}
