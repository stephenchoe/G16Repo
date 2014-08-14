using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace G16_2013.Models.TradeModel.Configurations
{
    public class ExchangeConfiguration : EntityTypeConfiguration<Exchange>
    {
        public ExchangeConfiguration()
        {
            HasOptional(e => e.ParentExchange).WithMany(p => p.SubExchanges).HasForeignKey(e => e.ParentExchangeId).WillCascadeOnDelete(false);
            //HasOptional(e => e.ParentExchange).WithMany(p => p.SubExchanges).
        }
    }
    public class TradeSessionConfiguration : EntityTypeConfiguration<TradeSession>
    {
        public TradeSessionConfiguration()
        {
            HasKey(t => t.TradeSessionId);
            
        }
    }
    public class PreOpeningConfiguration : EntityTypeConfiguration<PreOpening>
    {
        public PreOpeningConfiguration()
        {
            HasKey(p => p.TradeSessionId);
            HasRequired(p => p.MatchingSession).WithOptional(t => t.PreOpening);

        }
    }
    public class PreClosingConfiguration : EntityTypeConfiguration<PreClosing>
    {
        public PreClosingConfiguration()
        {
            HasKey(p => p.TradeSessionId);
            HasRequired(p => p.MatchingSession).WithOptional(t => t.PreClosing);

        }
    }
   
    public class PreOpeningSessionConfiguration : EntityTypeConfiguration<PreOpeningSession>
    {
        public PreOpeningSessionConfiguration()
        {
            HasRequired(pos => pos.PreOpening).WithMany(po => po.PreOpeningSessions).
                HasForeignKey(pos => pos.PreOpeningId);

        }
    }
    public class PreClosingSessionConfiguration : EntityTypeConfiguration<PreClosingSession>
    {
        public PreClosingSessionConfiguration()
        {
            HasRequired(pos => pos.PreClosing).WithMany(po => po.PreClosingSessions).
                HasForeignKey(pos => pos.PreClosingId);

        }
    }
}
