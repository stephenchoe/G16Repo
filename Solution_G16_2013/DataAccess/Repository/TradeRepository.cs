using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using G16_2013.Models.TradeModel;
using System.Data.Entity;

namespace G16_2013.DAL
{
    public class TradeRepository : ITradeRepository
    {
        #region Fields and Properties
        private TradeContext _context;
        TradeContext TradeContext
        {

            get
            {
                if (_context == null) _context = new TradeContext();
                return _context;
            }
        }

        #endregion

        public Futures InsertFuturesSymbol(Futures futuresSymbol)
        {
            TradeContext.FuturesSymbols.Add(futuresSymbol);
            return TradeContext.SaveChanges() > 0 ? futuresSymbol : null;
        }

        public Futures UpdateFuturesSymbol(Futures futuresSymbol)
        {
            TradeContext.Entry(futuresSymbol).State = EntityState.Modified;
            return TradeContext.SaveChanges() > 0 ? futuresSymbol : null;
        }

        public int DeleteFuturesSymbol(Futures futuresSymbol)
        {
            TradeContext.Entry(futuresSymbol).State = EntityState.Deleted;
            return TradeContext.SaveChanges() > 0 ? 1 : 0;
        }

        public int DeleteFuturesSymbol(int id)
        {
            var futuresSymbol = new Futures { SymbolId = id };
            return DeleteFuturesSymbol(futuresSymbol);
        }

        public Futures GetFuturesSymbolById(int id)
        {
            return TradeContext.FuturesSymbols.Find(id);
        }

        
        #region Dispose

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    TradeContext.Dispose();                   
                }
            }

            this.disposedValue = true;

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
