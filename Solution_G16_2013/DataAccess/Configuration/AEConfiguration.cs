using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using G16_2013.Models.MemberModel;

namespace G16_2013.DAL.Configuration
{
    public class AEConfiguration : EntityTypeConfiguration<AE>
    {
        public AEConfiguration()
        {
            HasRequired(a => a.Person).WithOptional(p => p.PersonOfAE);
        }
    }
    public class AECodeConfiguration : EntityTypeConfiguration<AECode>
    {
        public AECodeConfiguration()
        {
            HasRequired(ac => ac.Company).WithMany(c => c.AECodes)
                .HasForeignKey(ac => ac.CompanyId).WillCascadeOnDelete(false);

            Property(a => a.Code).HasMaxLength(50);

        }

    }
    public class AECodeAEConfiguration : EntityTypeConfiguration<AECodeAE>
    {
        public AECodeAEConfiguration()
        {
            HasRequired(ac => ac.AE).WithMany(ae => ae.AECodeRecords).HasForeignKey(ac => ac.AEId).WillCascadeOnDelete(false);
            HasRequired(ac => ac.Code).WithMany(c => c.AERecords).HasForeignKey(ac => ac.AECodeId).WillCascadeOnDelete(false);
        }
    }

    public class CompanyAEConfiguration : EntityTypeConfiguration<CompanyAE>
    {
        public CompanyAEConfiguration()
        {
            HasRequired(ca => ca.Company).WithMany(c => c.AERecords).HasForeignKey(ca => ca.CompanyId).WillCascadeOnDelete(false);
            HasRequired(ca => ca.AE).WithMany(ae => ae.CompanyRecords).HasForeignKey(ca => ca.AEId).WillCascadeOnDelete(false);
        }
    }

}
