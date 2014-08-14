using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace G16_2013.Models.MemberModel
{
    public class AEInputModel : BaseInput
    {
         
        public int AEId { get; set; }
        [Display(Name = "合約狀態")]
        public bool IsActive { get; set; }

        public PersonInputModel PersonInput { get; set; }
        public Nullable<int> CompanyId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]
        [Required(ErrorMessage = "請填寫開始日期")]
        public DateTime? BeginDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public DateTime? EndDate { get; set; }

        public bool PersonExsit { get; set; }
        public int ExsitPersonId { get; set; }
    }

    public class AECreateInputModel
    {
        public PersonSelectCreateInput PersonSelectCreateInput { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]
        [Required(ErrorMessage = "請填寫開始日期")]
        public DateTime? BeginDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public DateTime? EndDate { get; set; }
    }
    public class AEViewModel : AEInputModel        
    {
        public string AEName { get; set; }
        public PersonViewModel PersonView { get; set; }
        [Display(Name = "合約狀態")]
        public string IsActiveText { get; set; }
        [Display(Name = "現職公司")]
        public string CurrentCompanyText { get; set; }

        public List<AECompanyView> CompanyRecordsView { get; set; }
    }



    public class AECompanyInput : BaseInput
    {
        public int Id { get; set; }
        [Display(Name = "就職公司")]
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
          [Display(Name = "AE")]
        public int AEId { get; set; }
        [Display(Name = "公司員編")]
        public string StaffNumber { get; set; }
        [Display(Name = "職稱")]
        public string Title { get; set; }
        [Display(Name = "專線")]
        public string PersonalTEL { get; set; }

        [Display(Name = "分機")]
        public string TELCode { get; set; }
        [Display(Name = "在職狀態")]
        public bool IsActive { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]
        [Required(ErrorMessage = "請填寫開始日期")]
        public DateTime? BeginDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "AE")]
        public string AENameText { get; set; }

          [Display(Name = "就職公司")]
        public string CompanyFullName { get; set; }

     
        public List<BaseOption> CompanyOptions { get; set; }
        public List<BaseOption> BranchOptions { get; set; }

        public List<BaseOption> AEOptions { get; set; }

    }

    public class AECompanyView : AECompanyInput
    {
        public string CompanyText { get; set; }
        public string StatusText { get; set; }  //就職中/已離職
    
    }

  
    //public class AECodeAEInputModel : BaseInput
    //{
    //    public int Id { get; set; }
    //    public int AECodeId { get; set; }

    //    [Display(Name = "AE")]
    //    public int AEId { get; set; }
    //    public bool IsActive { get; set; }

    //    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "開始日期")]
    //    public DateTime BeginDate { get; set; }


    //    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "結束日期")]
    //    public DateTime? EndDate { get; set; }


    //    public List<BaseOption> AEOptions { get; set; }

    //}

    public class AEContractInputModel : BaseInput
    {
        public int Id { get; set; }
        public int AEId { get; set; }

        [Display(Name = "合約狀態")]
        public bool IsActive { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]
        public DateTime? BeginDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "AE")]
        public string AENameText { get; set; }
        public string StatusText { get; set; }
        [Display(Name = "備註")]
        public string PS { get; set; }
     

    }


}
