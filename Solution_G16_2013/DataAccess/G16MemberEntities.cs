using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using G16_2013.Models.MemberModel;
using G16_2013.DAL.Configuration;


namespace G16_2013.DAL
{
    public class G16MemberEntities : IdentityDbContext<G16ApplicationUser>
    {
        public G16MemberEntities()
            : base("G16MemberEntities")
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Identity> Identities { get; set; }
        public DbSet<PersonContactInfo> PersonContactInfos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }


       

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyContract> CompanyContracts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountContactInfo> AccountContactInfos { get; set; }
        public DbSet<AccountBankInfo> AccountBankInfos { get; set; }
        public DbSet<AE> AEs { get; set; }
        public DbSet<AEContract> AEContracts { get; set; }
        public DbSet<AECode> AECodes { get; set; }


        public DbSet<CompanyAE> CompaniesAEs { get; set; }     
        public DbSet<AECodeAE> AECodesAEs { get; set; }
        public DbSet<AccountAE> AccountsAEs { get; set; }
        public DbSet<AccountAECode> AccountsAECodes { get; set; }
        public DbSet<AccountTrader> AccountsTraders { get; set; }
        
        public DbSet<TextFuturesTradeRecord> TextFuturesTradeRecords { get; set; }
        public DbSet<TextStockTradeRecord> TextStockTradeRecords { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
           // modelBuilder.ComplexType<Address>();
            
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new PersonContactInfoConfig());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new IdentityConfiguration());

            modelBuilder.Configurations.Add(new AccountConfiguration());
            modelBuilder.Configurations.Add(new AccountContactInfoConfiguration());
            modelBuilder.Configurations.Add(new AccountTraderConfiguration());
            modelBuilder.Configurations.Add(new AccountAECodeConfiguration());
            modelBuilder.Configurations.Add(new AccountAEConfiguration());

            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new AEConfiguration());
            modelBuilder.Configurations.Add(new AECodeConfiguration());
            modelBuilder.Configurations.Add(new AECodeAEConfiguration());
            modelBuilder.Configurations.Add(new CompanyAEConfiguration());

            modelBuilder.Configurations.Add(new TextTradeRecordConfiguration());

          
         

           
        }
    }
}
