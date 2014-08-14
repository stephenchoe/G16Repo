using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using G16_2013.Models.TradeModel;

namespace G16_2013.DAL
{
  public  interface ITradeRepository : IDisposable
    {
        Futures InsertFuturesSymbol(Futures futuresSymbol);
        Futures UpdateFuturesSymbol(Futures futuresSymbol);
        int DeleteFuturesSymbol(Futures futuresSymbol);
        int DeleteFuturesSymbol(int id);
        Futures GetFuturesSymbolById(int id);
       
    }
}
