using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace G16_2013.Models.MemberModel
{
    public class AE :BaseEntity
    {
        public int AEId { get; set; }
        public bool IsActive { get; set; }

        //public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<AEContract> AEContractRecords { get; set; }
        public virtual ICollection<Account> AccountsOpened { get; set; }
        public virtual ICollection<Account> AccountsService { get; set; }

        public virtual ICollection<CompanyAE> CompanyRecords { get; set; }
        public virtual ICollection<AccountAE> AccountsRecords { get; set; }

        public virtual ICollection<AECodeAE> AECodeRecords { get; set; }


        //public AE()
        //{
        //    AECodeRecords = new List<AECodeAE>();
        //    CompanyRecords = new List<CompanyAE>();
        //}

    }
    public class AEContract : BaseEntity
    {
        public int Id { get; set; }
        public int AEId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual AE AE { get; set; }
    }

    public class AECodeAE : BaseEntity
    {
      
        public int Id { get; set; }
        public int AECodeId { get; set; }
        public int AEId { get; set; }
        public bool IsActive { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }


        public virtual AECode Code { get; set; }
        public virtual AE AE { get; set; }

    }





}
