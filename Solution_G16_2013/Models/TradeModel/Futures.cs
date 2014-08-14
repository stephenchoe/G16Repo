using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
 
    public class Futures : Derivative
    {
       
       
     
    }
    public class DerivativeContractSpec
    {
        public int SymbolId { get; set; }
        public ContractSize ContractSize { get; set; }
        public double OnePointValue { get; set; }      
        public MonthRule MonthRule { get; set; }
        public string MinimumTick { get; set; }

        public virtual Derivative DerivativeSymbol { get; set; }
        public virtual ICollection<ContractDayRule> ContractDayRules { get; set; }
        
    }
    public class ContractSize
    {
        public Nullable<decimal> Quantity { get; set; }
        public string Unit { get; set; }
    }
    public class ContractDayRule
    {
        public int ContractDayRuleId { get; set; }
        public int SymbolId { get; set; }
        public virtual DerivativeContractSpec ContractSpec { get; set; }

        public string HolidayCountry { get; set; }
        public ContractDay ContractDay { get; set; }
        public int MonthDiff { get; set; }
        public CountWay CountWay { get; set; }
        public int CountOrder { get; set; }
        public DayKind DayKind { get; set; }
        public WeekDay WeekDay { get; set; }
    }

    public enum ContractDay
    { 
      FirstNotice,
        LastTrade,
        Clear
    }
    public enum CountWay
    {
        Forward,
        Backward
      
    }
    public enum DayKind
    {
        BusinessDay,
        WeekDay

    }
    public enum WeekDay
    {  
        Sun,
      Mon,
        Tue,
        Wed,
        Thu,
        Fri,
        Sat
       
    }

    public class FuturesContract : DerivativeContract
    {

    }

  
    public class MonthRule
    {
        public string TradeMonth { get; set; }
        public int RegularMonth { get; set; }
        public int QuarterlyMonth { get; set; }
    }
}
