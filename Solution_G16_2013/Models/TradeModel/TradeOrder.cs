using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
    public class OrderKind
    {
        public int OrderKindId { get; set; }
        public int ExchangeId { get; set; }
        public virtual Exchange Exchange { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TradeSession> TradeSessions { get; set; }
        public virtual ICollection<OrderCondition> OrderConditions { get; set; }
        //public virtual ICollection<PreOpeningSession> PreOpeningSessions { get; set; }
        //public virtual ICollection<PreClosingSession> PreClosingSessions { get; set; }
        public OrderKind()
        {
            TradeSessions = new List<TradeSession>();
            OrderConditions = new List<OrderCondition>();
        }
       
    }
    public enum OrderType
    {
      Single,
      Combination       
    }

    public class OrderCondition
    {
        public int OrderConditionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<OrderKind> OrderKinds { get; set; }

    }
}
