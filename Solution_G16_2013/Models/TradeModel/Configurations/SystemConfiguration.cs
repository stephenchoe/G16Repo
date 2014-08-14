using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G16_2013.Models.TradeModel;
using System.Data.Entity.ModelConfiguration;

namespace G16_2013.Models.TradeModel.Configurations
{
    public class TradeAccountConfiguration : EntityTypeConfiguration<TradeAccount>
    {
        public TradeAccountConfiguration()
        {
            HasRequired(a => a.Company)
                    .WithMany(c => c.TradeAccounts).HasForeignKey(a => a.CompanyId);

        }
    }
}
