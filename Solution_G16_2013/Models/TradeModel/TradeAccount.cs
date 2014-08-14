using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
 public  class TradeAccount
    {
      public int TradeAccountId { get; set; }
      public int CompanyId { get; set; }    
      public string AccountNumber { get; set; }

      public virtual TradeCompany Company { get; set; }
    }

 public class FuturesTradeAccount : TradeAccount
 { 
   
 }
 public class StockTradeAccount : TradeAccount
 { 
   
 }
}
