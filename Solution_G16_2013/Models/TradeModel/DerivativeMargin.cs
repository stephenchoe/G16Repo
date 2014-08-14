using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
  public  class DerivativeMargin
    {
      public int SymbolId { get; set; }
    //  public int DerivativeMarginId { get; set; }
      public int InitialMargin { get; set; }
      public int MaintenanceMargin { get; set; }
      public DateTime LastUpdate { get; set; }
    
    //  public int DerivativeId { get; set; }
      public virtual Derivative MarginFor { get; set; }
    }
  public class OptionsMargin : DerivativeMargin
  {
      public string MarginType { get; set; }
  }
}
