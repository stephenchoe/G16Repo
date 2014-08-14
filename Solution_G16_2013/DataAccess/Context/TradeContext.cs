using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G16_2013.Models.TradeModel;
using G16_2013.Models.TradeModel.Configurations;

namespace G16_2013.DAL
{
    public class TradeContext : DbContext
    {
        public DbSet<TradeCompany> TradeCompanies { get; set; }
        public DbSet<TradeSystem> TradeSystems { get; set; }
        public DbSet<TradeAccount> TradeAccounts { get; set; }
        public DbSet<TradeRecord> TradeRecords { get; set; }
        #region TradeSessions
        //public DbSet<MatchingSession> TradeSessions { get; set; }
        public DbSet<TradeSession> TradeSessions { get; set; }
        //public DbSet<PreOpeningSession> PreOpeningSessions { get; set; }
        //public DbSet<TradeSession> PreClosingSessions { get; set; }
        //public DbSet<PreTradeSession> PreTradeSessions { get; set; }
       
        #endregion
        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<Futures> FuturesSymbols { get; set; }
       
        public DbSet<SymbolType> SymbolTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<OrderCondition> OrderConditions { get; set; }
        


        public DbSet<Currency> Currencies { get; set; }
        public DbSet<MonthCode> MonthCodes { get; set; }

        public DbSet<ChargeFeeGroup> ChargeFeeGroups { get; set; }
        public DbSet<RedPointGroup> RedPointGroups { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ExchangeConfiguration());
            modelBuilder.Configurations.Add(new TradeSessionConfiguration());
            modelBuilder.Configurations.Add(new PreOpeningConfiguration());
            modelBuilder.Configurations.Add(new PreClosingConfiguration());
            modelBuilder.Configurations.Add(new PreOpeningSessionConfiguration());
            modelBuilder.Configurations.Add(new PreClosingSessionConfiguration());

            modelBuilder.Configurations.Add(new TradeAccountConfiguration());
            modelBuilder.Configurations.Add(new DerivativeContractSpecConfiguration());
            modelBuilder.Configurations.Add(new DerivativeMarginConfiguration());
            modelBuilder.Configurations.Add(new SymbolCodeConfiguration());
            modelBuilder.Configurations.Add(new ChargeFeeGroupConfiguration());
         
            modelBuilder.ComplexType<QuoteWay>();
            modelBuilder.ComplexType<ContractSize>();
            modelBuilder.ComplexType<MonthRule>();

           

            //modelBuilder.Entity<Stock>().ToTable("Stocks");
            //modelBuilder.Entity<FuturesContract>().ToTable("FuturesContracts");
            //modelBuilder.Entity<OptionsContract>().ToTable("OptionsContracts");

            base.OnModelCreating(modelBuilder);
        }

        
    }//end class
}
