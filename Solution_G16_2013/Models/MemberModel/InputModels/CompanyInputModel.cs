using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace G16_2013.Models.MemberModel
{
    public class CompanyInputModel : BaseInput
    {
        public int CompanyId { get; set; }
        [Display(Name = "公司名稱")]
        public string Name { get; set; }
        [Display(Name = "公司類型")]
        public int CompanyTypeId { get; set; }
        [Display(Name = "母公司")]
        public int ParentCompanyId { get; set; }
        public int DisplayOrder { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]
        public DateTime? BeginDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "聯絡人")]
        public bool NoManager { get; set; }
        public PersonInputModel ManagerInput { get; set; }

        [Display(Name = "合約狀態")]
        public bool IsActive { get; set; }

        public int? ManagerId { get; set; }
        public List<BaseOption> ParentCompanyOptions { get; set; }
        public List<BaseOption> CompanyTypeOptions { get; set; }

    }
    public class CompanyViewModel : CompanyInputModel
    {
        [Display(Name = "母公司")]
        public string ParentCompanyName { get; set; }
        [Display(Name = "公司類型")]
        public string CompanyTypeName { get; set; }
        [Display(Name = "合約狀態")]
        public string StatusText { get; set; }
        public string FullName { get; set; }
        public PersonViewModel ManagerView { get; set; }

    }
    public class AECodeInputModel : BaseInput
    {
        public int AECodeId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        [Display(Name = "業務代碼")]
        public string Code { get; set; }
        [Display(Name = "業務類別")]
        public int BusinessTypeId { get; set; }
        [Display(Name = "狀態")]
        public bool IsActive { get; set; }
        [Display(Name = "AE")]
        public int AEId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]
        public DateTime? BeginDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public DateTime? EndDate { get; set; }
        public List<BaseOption> BusinessTypeOptions { get; set; }
        public List<BaseOption> AEOptions { get; set; }

    }
    public class AECodeViewModel : AECodeInputModel
    {
            [Display(Name = "公司名稱")]
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string TypeName { get; set; }
        public string StatusName { get; set; }
        public string AEName { get; set; }

    }

    public class CompanyContractInputModel : BaseInput
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        [Display(Name = "合約狀態")]
        public bool IsActive { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]
        public DateTime? BeginDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "公司")]
        public string CompanyNameText { get; set; }

        [Display(Name = "合約狀態")]
        public string StatusText { get; set; }

        [Display(Name = "備註")]
        public string PS { get; set; }


    }


    public class AECodeAEInputModel : BaseInput
    {
        public int Id { get; set; }
        public int AECodeId { get; set; }
        public int AEId { get; set; }
           [Display(Name = "狀態")]
        public bool IsActive { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]
        public DateTime BeginDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public DateTime? EndDate { get; set; }


        public List<BaseOption> AEOptions { get; set; }
    }
    public class AECodeAEViewModel : AECodeAEInputModel
    {
        public string AEName { get; set; }
        public string StatusText { get; set; }
    }

    public class DesignateAECodeToAEInput
    {
        public AECodeViewModel AECodeView { get; set; }
        public AECodeAEViewModel CurrentUserInput { get; set; }
        public AECodeAEInputModel NewUserInput { get; set; }
    }

}
