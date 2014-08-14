using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G16_2013.DAL;
using G16_2013.Models.TradeModel;
//using G16_2013.Models.MemberModel;
using System.Data.Entity;

namespace WebApplicationG16_2013.ViewModels
{
    public class FuturesContractViewModel
    {
         public string Name { get; set; }
        public string ContractSize { get; set; }
        public string MinTick { get; set; }
        public string TradeMonth { get; set; }
        public string Margin { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
        public string QuoteWay { get; set; }
        public string TradeSessions { get; set; }
      //  public SymbolType SymbolType { get; set; }

        public FuturesContractViewModel(Futures futuresSymbol)
        {
            Name = futuresSymbol.Name;
            if (futuresSymbol.Currency != null)
            
                Currency = futuresSymbol.Currency.Name;
           
            if (futuresSymbol.Exchange != null)
          
                Exchange = GetExchangeName(futuresSymbol);
          
            //if (futuresSymbol.SymbolType != null)
         
            //    SymbolType = futuresSymbol.SymbolType;
          
            if (futuresSymbol.QuoteWay != null)
           
                QuoteWay = GetQuoteWay(futuresSymbol.QuoteWay);
           
            if (futuresSymbol.ContractSpec != null)
           
                ContractSize = GetContractSize(futuresSymbol);
          
            if (futuresSymbol.ContractSpec != null)
          
                MinTick = GetTickValue(futuresSymbol);
           
            if (futuresSymbol.ContractSpec != null)
           
                TradeMonth = GetTradeMonth(futuresSymbol);
            if (futuresSymbol.ContractSpec != null)

                Margin = GetMargin(futuresSymbol);

            if (futuresSymbol.TradeSessions != null)
            {
               TradeSessions= GetTradeSessions(futuresSymbol.TradeSessions.ToList());
            }
            
           
           

        }

        string GetTradeSessions(List<MatchingSession> tradeSessions)
        {
           // var regularSessions=trade
            
            return "";
        }

        string GetMargin(Futures futuresSymbol)
        {
            if (futuresSymbol.Margin == null) return "";
            string returnValue = futuresSymbol.Margin.InitialMargin.ToString();

            return returnValue;
        }

        string GetTradeMonth(Futures futuresSymbol)
        {
            string month = futuresSymbol.ContractSpec.MonthRule.TradeMonth;
            string returnValue;
            switch (month)
            {
                case "All":
                    returnValue= "連續月份";
                    break;
                case "Quater":
                    returnValue = "3,6,9,12";
                    break;
                case "Even":
                    returnValue = "2,4,6,8,10,12";
                    break;
                default:
                    returnValue = month;
                    break;
            }

            return returnValue;
        }

        string GetTickValue(Futures futuresSymbol)
        {
            double tickValue;
            string tick = futuresSymbol.ContractSpec.MinimumTick;
            if (tick.Contains("/"))
            {
                string[] values = tick.Split('/');
                tickValue = Convert.ToDouble(values[0]) / Convert.ToDouble(values[1]);
            }
            else
            {
                tickValue = Convert.ToDouble(tick);
            }
            tickValue = tickValue * Convert.ToDouble(futuresSymbol.ContractSpec.OnePointValue);
            string currency = futuresSymbol.Currency.Name;
            return String.Format("{0} 點 ({1} {2})", tick, tickValue.ToString(), currency);
           
           
        }

        string GetExchangeName(Futures futuresSymbol)
        {
          
            if (futuresSymbol.Exchange == null) return "";
            if (String.IsNullOrEmpty(futuresSymbol.Exchange.Name))
            {
                string parent= futuresSymbol.Exchange.ParentExchange.Name;
                string sub=futuresSymbol.Exchange.Code;
                return String.Format("{0} ({1})", parent, sub);
            }
            else
            {
                return futuresSymbol.Exchange.Name;
            }

        }

        string GetContractSize(Futures futuresSymbol )
        {
            DerivativeContractSpec contractSpec=futuresSymbol.ContractSpec;
            string result = "";
            string unit = "";
            if (contractSpec.ContractSize.Unit!=null)
            {
             unit=contractSpec.ContractSize.Unit;
            }
            
            string pointValue=contractSpec.OnePointValue.ToString();
            decimal quantity;
            if(contractSpec.ContractSize.Quantity==null)
            {
              quantity=0;
            }
            else
            {
             quantity=Convert.ToDecimal(contractSpec.ContractSize.Quantity);
            }


            if (quantity == 0)
            {
                result = unit + " × " + pointValue + " " + futuresSymbol.Currency.Name ;
            }
            else
            {
                result = quantity.ToString("0.#####") + " " + unit;
            }

            return result;
        }
        string GetQuoteWay(QuoteWay quoteWay)
        {
            string result = "";
            result = quoteWay.LeftText;
            result += " = ";
            for (int i = 0; i < quoteWay.IntDigits; i++)
            {
                result += "#";
            }
            if (quoteWay.FloatDigits > 0)
            {
                result += ".";
                for (int i = 0; i < quoteWay.FloatDigits; i++)
                {
                    result += "#";
                }
            }
            result += " "+quoteWay.RightText;
            
                //if (SymbolType.Name == "指數期貨")
                //{
                //    result = quoteWay.LeftText;
                //}

                return result;
        }
    }
}