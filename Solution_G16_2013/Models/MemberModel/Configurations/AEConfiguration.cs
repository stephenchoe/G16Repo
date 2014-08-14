using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.MemberModel.Configurations
{
    public class AEConfiguration : EntityTypeConfiguration<AE>
    {
        public AEConfiguration()
        {
           HasRequired(a => a.Person).WithOptional(p=>p.PersonOfAE);
        }
    }
    public class AECodeConfiguration : EntityTypeConfiguration<AECode>
    {
        public AECodeConfiguration()
        {
            HasRequired(ac => ac.Company).WithMany(c => c.AECodes)
                .HasForeignKey(ac => ac.CompanyId).WillCascadeOnDelete(false); 
          
        }
     
    }
    public class AECodeAEConfiguration : EntityTypeConfiguration<AECodeAE>
    {
        public AECodeAEConfiguration()
        {
            //HasKey(ac => new { ac.AEId, ac.AECodeId });
            //HasRequired(ac => ac.AE).WithMany(ae => ae.AECodeRecords).WillCascadeOnDelete(false);
            //HasRequired(ac => ac.Code).WithMany(c => c.AERecords).WillCascadeOnDelete(false);
            HasRequired(ac => ac.AE).WithMany(ae => ae.AECodeRecords).HasForeignKey(ac => ac.AEId).WillCascadeOnDelete(false);
            HasRequired(ac => ac.Code).WithMany(c => c.AERecords).HasForeignKey(ac => ac.AECodeId).WillCascadeOnDelete(false);
        }
    }
   
    public class CompanyAEConfiguration : EntityTypeConfiguration<CompanyAE>
    {
        public CompanyAEConfiguration()
        {
            //HasKey(ca => new { ca.CompanyId, ca.AEId });
            //HasRequired(ca => ca.Company).WithMany(c => c.AERecords).WillCascadeOnDelete(false);
            //HasRequired(ca => ca.AE).WithMany(ae => ae.CompanyRecords).WillCascadeOnDelete(false);
            HasRequired(ca => ca.Company).WithMany(c => c.AERecords).HasForeignKey(ca => ca.CompanyId).WillCascadeOnDelete(false);
            HasRequired(ca => ca.AE).WithMany(ae => ae.CompanyRecords).HasForeignKey(ca => ca.AEId).WillCascadeOnDelete(false);
        }
    }

   
    
}
