using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace G16_2013.Models.MemberModel.Configurations
{
    //public class UserConfiguration : EntityTypeConfiguration<G16ApplicationUser>
    //{
    //    public UserConfiguration()
    //    {
    //        HasRequired(u => u.Person).WithMany(p => p.Users).HasForeignKey(u => u.PersonId);
    //    }
    //}
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {

        }
    }
    public class PersonContactInfoConfig : EntityTypeConfiguration<PersonContactInfo>
    {
        public PersonContactInfoConfig()
        {
            HasKey(p => p.PersonId);
            HasRequired(c => c.ContactInformatonOf).WithOptional(p => p.ContactInfo).WillCascadeOnDelete();
          
        }
    }
    public class IdentityConfiguration : EntityTypeConfiguration<Identity>
    {
        public IdentityConfiguration()
        {
            Map(m =>
                {
                    m.ToTable("Identities");

                    m.Requires("IsPublic").HasValue(true);
                })
                .Map<CustomIdentity>(m =>
                    {
                        m.Requires("IsPublic").HasValue(false);
                    });
        }
     
    }

}
