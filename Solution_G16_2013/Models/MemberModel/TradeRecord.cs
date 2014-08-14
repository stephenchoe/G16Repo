using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.MemberModel
{
    public  abstract  class TextTradeRecord:BaseEntity
    {
        public int TradeRecordId { get; set; }
        public string CompanyName { get; set; }
        public string AccountNumber { get; set; }
        public DateTime DateOfTrade { get; set; }
        public int DealTime { get; set; }
        public string SymbolCode { get; set; }
        public bool IsBuy { get; set; }
        public double ChargeFee { get; set; }
        public string DealPrice { get; set; }
        public string AECode { get; set; }
    }
    public  class TextFuturesTradeRecord : TextTradeRecord
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Week { get; set; }
        public bool IsOptions { get; set; }
        public double StrikePrice { get; set; }
        public string OrderType { get; set; }
        public bool IsOffset { get; set; }
        public bool IsComplex { get; set; }



        public int DealLot { get; set; }

    }

    public  class TextStockTradeRecord : TextTradeRecord
    {
        public double DealStock { get; set; }

    }
}
