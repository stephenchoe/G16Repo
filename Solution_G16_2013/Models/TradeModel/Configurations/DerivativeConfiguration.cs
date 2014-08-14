using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel.Configurations
{
  
    public class DerivativeConfiguration : EntityTypeConfiguration<Derivative>
    {
        public DerivativeConfiguration()
        {

        }
    }

    public class DerivativeContractSpecConfiguration : EntityTypeConfiguration<DerivativeContractSpec>
    {
        public DerivativeContractSpecConfiguration()
        {
            HasKey(dc => dc.SymbolId);
            HasRequired(dc => dc.DerivativeSymbol).WithOptional(d => d.ContractSpec).WillCascadeOnDelete();

        }
    }
    public class DerivativeMarginConfiguration : EntityTypeConfiguration<DerivativeMargin>
    {
        public DerivativeMarginConfiguration()
        {
            HasKey(dm => dm.SymbolId);
            HasRequired(dc => dc.MarginFor).WithOptional(d => d.Margin).WillCascadeOnDelete();
        }
    }

    public class SymbolCodeConfiguration : EntityTypeConfiguration<SystemSymbolCode>
    {
        public SymbolCodeConfiguration()
        {
            HasKey(sc => new { sc.SystemId, sc.SymbolId });

        }
    }

   
}
