using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
   //[Table("Derivatives")]
    public abstract class Derivative : TradingSymbol
    {      
       public int ParentSymbolId { get; set; }
       public ClearWay ClearWay { get; set; }
       public virtual DerivativeContractSpec ContractSpec { get; set; }
       public virtual ICollection<MonthCode> TradingMonth { get; set; }
      // public List<DerivativeMargin> Margins { get; set; } 
       public virtual DerivativeMargin Margin { get; set; }
       public virtual ICollection<DerivativeContract> Contracts { get; set; }
      
    }//end class

   public class MonthCode
   {
       public int MonthCodeId { get; set; }
       public string Code { get; set; }
       public int Month { get; set; }
       public virtual ICollection<Derivative> MonthDerivatives { get; set; } 
   }

   public enum ClearWay
   {
       Cash,
       Physical
   }
  
 
}
