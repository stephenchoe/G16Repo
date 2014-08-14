using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
   public  class Symbol
    {
       public int SymbolId { get; set; }
       public string Name { get; set; }
       public string Code { get; set; }
      
       public int DisplayOrder { get; set; }
       public Nullable<int> CountryId { get; set; }
       public virtual Country Country { get; set; }
       public int SymbolTypeId { get; set; }
       public virtual SymbolType SymbolType { get; set; }

    }//end class
   
   public class TradingSymbol : Symbol
   {
       public int ExchangeId { get; set; }
       public virtual Exchange Exchange { get; set; }
       public int CurrencyId { get; set; }
       public virtual Currency Currency { get; set; }
       public QuoteWay QuoteWay { get; set; }

       public virtual ICollection<MatchingSession> TradeSessions { get; set; }
       public virtual ICollection<ChargeFeeGroup> ChargeFeeGroups { get; set; }

   }

   
   public class Currency
   {
       public int CurrencyId { get; set; }
       public string Name { get; set; }
       public string Code { get; set; }
       public int DisplayOrder { get; set; }
   }
    //ComplexType
   public class QuoteWay
   {
       public string LeftText { get; set; }
       public Nullable<int> IntDigits { get; set; }
       public string PointSymbol { get; set; }
       public Nullable<int> FloatDigits { get; set; }
       public string RightText { get; set; }
   }
  
  
   public class SymbolType
   {
       public int SymbolTypeId { get; set; }
       public string Name { get; set; }
       public int ParentSymbolTypeId { get; set; }
       public int DisplayOrder { get; set; }
   }//end class
   
   public class Country
   {
       public int CountryId { get; set; }
       public string Name { get; set; }
       public byte[] Photo { get; set; }
       public int DisplayOrder { get; set; }
       public int ContinentId { get; set; }
       public virtual Continent Continent { get; set; }
       public virtual ICollection<Symbol> Symbols { get; set; }
       public virtual ICollection<Holiday> Holidays { get; set; }

     
   }//end class

   public class Continent
   {
       public int ContinentId { get; set; }
       public string Name { get; set; }
       public int DisplayOrder { get; set; }
       public virtual ICollection<Country> Countries { get; set; }
   }//end class

   public class Holiday
   {
       public int HolidayId { get; set; }
       public int CountryId { get; set; }
       public virtual Country Country { get; set; }
       public int Year { get; set; }
       public DateTime DateOfHoliday { get; set; }
       public string HolidayName { get; set; }

   }
 
}
