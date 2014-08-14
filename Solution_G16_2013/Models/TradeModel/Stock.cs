using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace G16_2013.Models.TradeModel
{
 
    public class Stock : TradingSymbol
    {
        public int IndustryId { get; set; }
        public virtual Industry Industry { get; set; }
    }

    public class Industry
    {
        public int IndustryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
