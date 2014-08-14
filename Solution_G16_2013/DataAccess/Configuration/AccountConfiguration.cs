using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using G16_2013.Models.MemberModel;

namespace G16_2013.DAL.Configuration
{
    public class AccountConfiguration : EntityTypeConfiguration<Account>
    {
        public AccountConfiguration()
        {
            HasRequired(a => a.Owner).WithMany(p => p.Accounts).HasForeignKey(a => a.PersonId);
            HasRequired(a => a.ContactInfo).WithRequiredPrincipal(c => c.ContactInformatonOf);
            HasRequired(a => a.CompanyBranch)
                                .WithMany(c => c.Accounts)
                                    .HasForeignKey(a => a.CompanyBranchId);
            HasRequired(a => a.AECode).WithMany(c => c.Accounts)
                                                .HasForeignKey(a => a.AECodeId);
            HasRequired(a => a.ServiceAE).WithMany(ae => ae.AccountsService)
                                               .HasForeignKey(a => a.ServiceAEId);

            HasOptional(a => a.Referral).WithMany(p => p.ReferralAccounts)
                                              .HasForeignKey(a => a.ReferralId);

            HasOptional(a => a.Trader).WithMany(p => p.TraderAccounts)
                                             .HasForeignKey(a => a.TraderId);
            HasOptional(a => a.OpenAE).WithMany(ae => ae.AccountsOpened)
                                                .HasForeignKey(a => a.OpenAEId);

            Property(a => a.AccountNumber).IsRequired().HasMaxLength(50);

        }
    }

    public class AccountContactInfoConfiguration : EntityTypeConfiguration<AccountContactInfo>
    {
        public AccountContactInfoConfiguration()
        {
            HasKey(c => c.AccountId);

            Property(c => c.Phone).HasMaxLength(50);
            Property(c => c.TEL).HasMaxLength(50);
            Property(c => c.Email).IsMaxLength();
        }
    }
    public class AccountAECodeConfiguration : EntityTypeConfiguration<AccountAECode>
    {
        public AccountAECodeConfiguration()
        {
           
            HasRequired(aac => aac.Account).WithMany(a => a.AECodeRecords)
                .HasForeignKey(aac => aac.AccountId).WillCascadeOnDelete(false);
            HasRequired(aac => aac.AECode).WithMany(ac => ac.AccountRecords)
                .HasForeignKey(aac => aac.AECodeId).WillCascadeOnDelete(false);
          

        }
    }

    public class AccountTraderConfiguration : EntityTypeConfiguration<AccountTrader>
    {
        public AccountTraderConfiguration()
        {
            HasRequired(at => at.Trader).WithMany(p => p.TraderAccountRecords)
                .HasForeignKey(at => at.TraderId).WillCascadeOnDelete(false);
            HasRequired(at => at.Account).WithMany(a => a.TraderRecords)
                .HasForeignKey(at => at.AccountId).WillCascadeOnDelete(false);
        }
    }
    public class AccountAEConfiguration : EntityTypeConfiguration<AccountAE>
    {
        public AccountAEConfiguration()
        {
            HasRequired(aa => aa.Account).WithMany(a => a.ServiceAERecords)
                .HasForeignKey(aa => aa.AccountId).WillCascadeOnDelete(false);
            HasRequired(aa => aa.AE).WithMany(ae => ae.AccountsRecords)
                .HasForeignKey(aa => aa.AEId).WillCascadeOnDelete(false);
        }
    }
    public class TextTradeRecordConfiguration : EntityTypeConfiguration<TextTradeRecord>
    {
        public TextTradeRecordConfiguration()
        {
            HasKey(r => r.TradeRecordId);

        }

    }
}
