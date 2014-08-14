using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using G16_2013.Models.MemberModel;

namespace G16_2013.BLL.TradeReport
{
    public class ConcordsTradeReportResolver : TradeReportResolver
    {
        private Dictionary<string, int> reportFormat;

        private Dictionary<string, int> ReportFormat
        {
            get { return reportFormat; }
        }
   
       public ConcordsTradeReportResolver(BusinessType reportType, DateTime reportDate,string filePath)
            : base(reportDate, filePath)
       {
        
           if (reportType == BusinessType.Futures)
           {
               IniConcordsFuturesReportFormat();
           }
       }

       public override List<TextFuturesTradeRecord> ResolveFuturesTradeReport()
       {
           List<TextFuturesTradeRecord> textFuturesTradeRecords = new List<TextFuturesTradeRecord>();
           using (StreamReader file = new StreamReader(FilePath, Encoding.Default))
           {
               string line = "";
               while ((line = file.ReadLine()) != null)
               {
                   string[] splitLine = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                   List<string> columns = splitLine.ToList();
                   if (columns.Count == 20 || columns.Count == 21)
                   {                       
                       if (columns.Count == 20)
                       {
                           columns.Insert(13, "N");
                       }
                       TextFuturesTradeRecord record = ResolveTradeRecord(columns);
                       textFuturesTradeRecords.Add(record);
                   }
               }
           }
           return textFuturesTradeRecords;
       }

       public TextFuturesTradeRecord ResolveTradeRecord(List<string> tradeReportRecord)
       {           
           var record = new TextFuturesTradeRecord
           {
               CompanyName = "康和",
               AccountNumber = tradeReportRecord[ReportFormat["帳號"]],
               DateOfTrade = TradeDate,
               DealTime = GetDealTime(tradeReportRecord[ReportFormat["成交時間"]]),
               OrderType = tradeReportRecord[ReportFormat["委託單別"]],
               SymbolCode = tradeReportRecord[ReportFormat["商品代碼"]],
               Year = GetYear(tradeReportRecord[ReportFormat["到期年月"]]),
               Month = GetMonth(tradeReportRecord[ReportFormat["到期年月"]]),
               Week = GetWeek(tradeReportRecord[ReportFormat["商品代碼"]]),
               IsOptions = GetIsOptions(tradeReportRecord[ReportFormat["履約價"]]),
               StrikePrice = GetStrikePrice(tradeReportRecord[ReportFormat["履約價"]]),
               IsComplex = GetIsComplex(tradeReportRecord[ReportFormat["複式單"]]),
               IsBuy = GetIsBuy(tradeReportRecord[ReportFormat["買進"]]),
               IsOffset = GetIsOffset(tradeReportRecord[ReportFormat["新平倉"]]),
               DealPrice = tradeReportRecord[ReportFormat["成交價"]],
               ChargeFee = Convert.ToDouble(tradeReportRecord[ReportFormat["手續費"]]),
               AECode = tradeReportRecord[ReportFormat["營業員代碼"]]
           };

           if (record.IsBuy)
           {
               record.DealLot = Convert.ToInt32(tradeReportRecord[ReportFormat["買進"]]);
           }
           else
           {
               record.DealLot = Convert.ToInt32(tradeReportRecord[ReportFormat["賣出"]]);
           }

           return record;
       }
       

       private void IniConcordsFuturesReportFormat()
       {
           reportFormat = new Dictionary<string, int>();
           reportFormat.Add("帳號", 3);
           reportFormat.Add("委託單別", 5);
           reportFormat.Add("複式單", 6);
           reportFormat.Add("商品代碼", 7);
           reportFormat.Add("到期年月", 8);
           reportFormat.Add("履約價", 9);
           reportFormat.Add("買進", 10);
           reportFormat.Add("賣出", 11);
           reportFormat.Add("成交價", 12);
           reportFormat.Add("當沖", 13);
           reportFormat.Add("新平倉", 15);
           reportFormat.Add("委託條件", 16);
           reportFormat.Add("手續費", 17);
           reportFormat.Add("成交時間", 18);
           reportFormat.Add("營業員代碼", 20);
       }
       private int GetDealTime(string input)
       {
           return Convert.ToInt32(input.Replace(":",""));
       }
       private int GetYear(string input)
       {
           string[] splitLine = input.Split(new string[] { "/" }, StringSplitOptions.None);
           return Convert.ToInt32(splitLine[0]);
       }
       private int GetMonth(string input)
       {
           string[] splitLine = input.Split(new string[] { "/" }, StringSplitOptions.None);
           return Convert.ToInt32(splitLine[1]); 
       }
       private int GetWeek(string input)
       {
           if (input.EndsWith("1"))
           {
               return 1;
           }
           else if (input.EndsWith("2"))
           {
               return 2;
           }
           else if (input.EndsWith("4"))
           {
               return 4;
           }
           else if (input.EndsWith("5"))
           {
               return 5;
           }
           else
           {
               return 0;
           }
          
       }
       private bool GetIsOptions(string input)
       {
           if (input.EndsWith("C"))
           {
               return true;
           }
           else if (input.EndsWith("P"))
           {
               return true;
           }
           else
           {
               return false;
           }
          
       }
       private double GetStrikePrice(string input)
       {
           if (input.EndsWith("C") || input.EndsWith("P"))
           {
               string[] splitLine = input.Split(new string[] { "." }, StringSplitOptions.None);
               return Convert.ToDouble(splitLine[0]);
           }
           else
           {
               return 0d;
           }
       }
       private bool GetIsComplex(string input)
       {
           if (input=="Y")
           {
               return true;
           }
           else if (input == "N")
           {
               return false;
           }
           else
           {
               return false;
           }
       }
       private bool GetIsOffset(string input)
       {
           if (input == "新")
           {
               return false;
           }
           else if (input == "平")
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       private bool GetIsBuy(string input)
       {
           int buyLots = Convert.ToInt32(input);
           return buyLots > 0 ? true : false;
       }
      

       

    }
}
