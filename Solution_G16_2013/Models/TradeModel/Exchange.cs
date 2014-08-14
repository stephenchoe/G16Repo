using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
    public class Exchange
    {
        public int ExchangeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<int> ParentExchangeId { get; set; }
        public virtual Exchange ParentExchange { get; set; }
        public virtual ICollection<Exchange> SubExchanges { get; set; }
        public virtual ICollection<OrderKind> OrderKinds { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }


        public virtual ICollection<TradingSymbol> TradingSymbols { get; set; }
    }
    public abstract class TradeSession
    {
        public int TradeSessionId { get; set; }      
        public string Name { get; set; }
      
        public int OrderNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int DisplayOrder { get; set; }
        public virtual ICollection<OrderKind> AllowOrderKinds { get; set; }
        public bool AllowNewOrder { get; set; }
        public bool AllowCancel { get; set; }
        public string Description { get; set; }
       
    }
    public class MatchingSession : TradeSession
    {
        public SessionType SessionType { get; set; }
        public ContractsFor ContractsFor { get; set; }
        public string ExchangeCode { get; set; }
       
        public TimeSpan MatchStartTime { get; set; }
        public TimeSpan MatchEndTime { get; set; }
        public string TimeZoneName { get; set; }
      
      
        public virtual ICollection<TradingSymbol> TradingSymbols { get; set; }
        public virtual PreOpening PreOpening { get; set; }
        public virtual PreClosing PreClosing { get; set; }

        public MatchingSession()
        {
            AllowNewOrder = true;
            AllowCancel = true;
          
        }
    }
    public enum ContractsFor
    { 
      All,
        Expiring
    }

   
    public class PreOpening 
    {
        public int TradeSessionId { get; set; }
        public virtual MatchingSession MatchingSession { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
      
        public virtual ICollection<PreOpeningSession> PreOpeningSessions { get; set; }
      
        public TimeSpan OpenPriceMatchTime { get; set; }

        
    }
    public class PreOpeningSession : TradeSession
    {
        public Nullable<int> PreOpeningId { get; set; }
        public virtual PreOpening PreOpening { get; set; }
       
    }
    public class PreClosing
    {
        public int TradeSessionId { get; set; }
        public virtual MatchingSession MatchingSession { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public virtual ICollection<PreClosingSession> PreClosingSessions { get; set; }
       
        public TimeSpan ClosePriceMatchTime { get; set; }
    }
    public class PreClosingSession : TradeSession
    {
        public Nullable<int> PreClosingId { get; set; }
        public virtual PreClosing PreClosing { get; set; }
      
    
    }
   

    public enum SessionType
    {
        Regular,
        AfterHours,
       

    }
}
