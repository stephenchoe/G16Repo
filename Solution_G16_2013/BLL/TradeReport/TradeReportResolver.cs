using G16_2013.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.BLL.TradeReport
{
    public abstract class TradeReportResolver
    {

        public string FilePath { get; set; }
        public DateTime TradeDate { get; set; }





        public TradeReportResolver(DateTime reportDate, string filePath)
        {
            TradeDate = reportDate;
            FilePath = filePath;

        }

        public virtual List<TextFuturesTradeRecord> ResolveFuturesTradeReport()
        {
            return null;
        }
        public virtual List<TextStockTradeRecord> ResolveStockTradeReport()
        {
            return null;
        }

    }
}
