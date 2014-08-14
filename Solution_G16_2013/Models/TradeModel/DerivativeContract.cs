using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
    public  class DerivativeContract
    {
        public int DerivativeContractId { get; set; }
        public virtual Derivative Symbol { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime OnBoardDate { get; set; }
        public DateTime FirstNoticeDate { get; set; }
        public DateTime LastTradingDate { get; set; }
        public DateTime ClearDate { get; set; }
        public double FinalClearPrice { get; set; }
        public ContractStatus ContractStatus { get; set; }
    }
   
    public enum ContractStatus
    {
        NotOnBoard,
        OnBoard,
        Cleared
    }
}
