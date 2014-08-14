using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
    public class TradeCompany
    {
        public int TradeCompanyId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<TradeSystem> TradeSystems { get; set; }
        public virtual ICollection<TradeAccount> TradeAccounts { get; set; }
        public virtual ICollection<ChargeFeeGroup> ChargeFeeGroups { get; set; }
    }
   
  public  class TradeSystem
    {
      public int TradeSystemId { get; set; }
      public string Name { get; set; }
      public bool IsActive { get; set; }

      public virtual ICollection<TradeCompany> TradeCompanies { get; set; }
    }

    public class SystemSymbolCode
    {
        public int SystemId { get; set; }
        public int SymbolId { get; set; }
        public string Code { get; set; }

    }

  
    
}
