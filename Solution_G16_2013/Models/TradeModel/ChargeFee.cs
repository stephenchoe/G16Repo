using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
    public class ChargeFeeGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CompanyId { get; set; }
        public virtual TradeCompany Company { get; set; }
        public double Cost { get; set; }

        public virtual Currency Currency { get; set; }
        public double RedPointLimit { get; set; }
        public virtual RedPointGroup RedPointGroup { get; set; }
        public virtual  ICollection<TradingSymbol> Symbols { get; set; }
    }
    public class FuturesChargeFeeGroup : ChargeFeeGroup
    {

    }
    public class StockChargeFeeGroup : ChargeFeeGroup
    {

    }

 
}
