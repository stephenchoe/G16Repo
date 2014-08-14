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
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(50);
            Property(p => p.TWID).HasMaxLength(50);
            Property(p => p.PS).IsMaxLength();
           
        }
    }
    public class PersonContactInfoConfig : EntityTypeConfiguration<PersonContactInfo>
    {
        public PersonContactInfoConfig()
        {
            HasKey(c => c.PersonId);
            HasRequired(c => c.ContactInformatonOf).WithOptional(p => p.ContactInfo).WillCascadeOnDelete();

            Property(c => c.Phone).HasMaxLength(50);
            Property(c => c.TEL).HasMaxLength(50);
            Property(c => c.Email).IsMaxLength();

        }
    }
    public class AddressConfiguration :ComplexTypeConfiguration<Address> 
    {
        public AddressConfiguration()
        {
            Property(a => a.City).HasMaxLength(50);
            Property(c => c.District).HasMaxLength(50);
            Property(c => c.ZipCode).HasMaxLength(50);
            Property(c => c.StreetAddress).IsMaxLength();

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
