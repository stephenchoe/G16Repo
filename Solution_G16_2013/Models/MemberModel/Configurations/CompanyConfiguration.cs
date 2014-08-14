using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.MemberModel.Configurations
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            HasOptional(c => c.Manager).WithMany(m => m.ManagerOfCompany).HasForeignKey(c => c.ManagerId);
        }
    }

  
}
