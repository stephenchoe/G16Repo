using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace G16_2013.Models.MemberModel.Configurations
{
    public class AccountConfiguration : EntityTypeConfiguration<Account>
    {
        public AccountConfiguration()
        {
            HasRequired(a => a.Owner).WithMany(p=>p.Accounts).HasForeignKey(a=>a.PersonId);
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
                                                .HasForeignKey(a=>a.OpenAEId);

        }
    }
    public class AccountContactInfoConfiguration : EntityTypeConfiguration<AccountContactInfo>
    {
        public AccountContactInfoConfiguration()
        {
            HasKey(a => a.AccountId);

        }
    }
    public class AccountAECodeConfiguration : EntityTypeConfiguration<AccountAECode>
    {
        public AccountAECodeConfiguration()
        {
            //HasKey(a => new {a.AccountId,a.AECodeId});
            //HasRequired(aac => aac.Account).WithMany(a => a.AECodeRecords).WillCascadeOnDelete(false);
            HasRequired(aac => aac.Account).WithMany(a => a.AECodeRecords)
                .HasForeignKey(aac=>aac.AccountId).WillCascadeOnDelete(false);
            HasRequired(aac => aac.AECode).WithMany(ac => ac.AccountRecords)
                .HasForeignKey(aac => aac.AECodeId).WillCascadeOnDelete(false);
            //HasRequired(aac => aac.AECode).WithMany(ac => ac.AccountRecords).WillCascadeOnDelete(false);
          
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
            HasRequired(aa => aa.Account).WithMany(a=>a.ServiceAERecords)
                .HasForeignKey(aa => aa.AccountId).WillCascadeOnDelete(false);
            HasRequired(aa => aa.AE).WithMany(ae => ae.AccountsRecords)
                .HasForeignKey(aa=>aa.AEId).WillCascadeOnDelete(false);
        }
    }
    public class FuturesAccountConfiguration : EntityTypeConfiguration<FuturesAccount>
    {
        public FuturesAccountConfiguration()
        {
            //Ignore(fa => fa.WithdrawBankAccount);
            //Ignore(fa => fa.ForexWithdrawBankAccount);
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
