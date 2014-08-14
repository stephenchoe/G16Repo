using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace G16_2013.Models.MemberModel
{
    public class Company : BaseEntity
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public BusinessType CompanyType { get; set; }
        public int ParentCompanyId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public CompanyManeger Manager { get; set; }

        public int? ManagerId { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<AE> AEs { get; set; }


        public virtual ICollection<CompanyAE> AERecords { get; set; }
        public virtual ICollection<AECode> AECodes { get; set; }
        public virtual ICollection<CompanyContract> CompanyContracts { get; set; }

        //public Company()
        //{
        //    AERecords = new List<CompanyAE>();
        //    AECodes = new List<AECode>();
        //}

    }
    public class CompanyContract : BaseEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string PS { get; set; }
        public virtual Company Company { get; set; }
    }

    public class CompanyAE : BaseEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int AEId { get; set; }        
        public string StaffNumber { get; set; }      
        public string Title { get; set; }      
        public string PersonalTEL { get; set; }       
        public string TELCode { get; set; }       
        public bool IsActive { get; set; }     
        public DateTime? BeginDate { get; set; }       
        public DateTime? EndDate { get; set; }

        public virtual AE AE { get; set; }
        public virtual Company Company { get; set; }



    }
    public class AECode : BaseEntity
    {
        public int AECodeId { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public BusinessType BusinessType { get; set; }
        public bool IsActive { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<AECodeAE> AERecords { get; set; }
        public virtual ICollection<AccountAECode> AccountRecords { get; set; }

        //public AECode()
        //{

        //    //AERecords = new List<AECodeAE>();
        //    //AccountRecords = new List<AccountAECode>();
        //}
        public AE GetCurrentAE()
        {
            if (AERecords == null) return null;
            var currentRecord = (from record in AERecords
                                 where record.IsActive &&
                                     (record.EndDate == null || record.EndDate > DateTime.Now)
                                 orderby record.LastUpdated
                                 select record).FirstOrDefault();
            if (currentRecord == null) return null;
            return currentRecord.AE;
        }



    }


    public enum BusinessType
    {
        Futures,
        Stock,
        Fund
    }
}


