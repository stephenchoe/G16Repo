using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel.Configurations
{
    public class ChargeFeeGroupConfiguration : EntityTypeConfiguration<ChargeFeeGroup>
    {
        public ChargeFeeGroupConfiguration()
        {
            HasRequired(c => c.Company)
                .WithMany(c => c.ChargeFeeGroups).HasForeignKey(c => c.CompanyId);
           
        
        }
    }
}
