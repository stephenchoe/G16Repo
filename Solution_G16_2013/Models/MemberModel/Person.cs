using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.MemberModel
{
    public  class Person : BaseEntity
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string TWID { get; set; }
       
        public bool Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string PS { get; set; }

        public virtual ICollection<G16ApplicationUser> Users { get; set; }
        public virtual PersonContactInfo ContactInfo { get; set; }
        public virtual AE PersonOfAE { get; set; }

       
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Identity> Identities { get; set; }

        public virtual ICollection<Account> ReferralAccounts { get; set; }
        public virtual ICollection<Account> TraderAccounts { get; set; }
        public virtual ICollection<AccountTrader> TraderAccountRecords { get; set; }

        //public int CreatePersonId { get; set; }
       

    }
    public  class PersonContactInfo : BaseEntity
    {
        public int PersonId { get; set; }

        public string Phone { get; set; }
        public string TEL { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public virtual Person ContactInformatonOf { get; set; }

        public PersonContactInfo()
        {
            Address = new Address();
        }

    }

    public class CompanyManeger : Person
    {
        public virtual ICollection<Company> ManagerOfCompany { get; set; }
    }


    public  class Address
    {
        public string City { get; set; }
        public string District { get; set; }

        public string ZipCode { get; set; }
        public string StreetAddress { get; set; }
        public Address()
        { 
        
        }
        public Address(Address address)
        {
            City = address.City;
            District = address.District;
            ZipCode = address.ZipCode;
            StreetAddress = address.StreetAddress;
        }
        public Address(string city,string district,string zip, string street)
        {
            City = city;
            District = district;
            ZipCode = zip;
            StreetAddress = street;
        }

    }
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string TelCode { get; set; }
        public virtual ICollection<District> Districts { get; set; }

        //public City()
        //{
        //    Districts = new List<District>();
        //}
    }
    public class District
    {
        public int DistrictId { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

    }

    public  class Identity : BaseEntity
    {
        public int IdentityId { get; set; }
        public string Name { get; set; }      
        public virtual ICollection<Person> People { get; set; }
    }

    public  class CustomIdentity : Identity
    {
        public int CreatePersonId { get; set; }
    }




    public enum SearchWay
    {
        Name,
        Phone,
        Email,
        Account
    }
    public class SearchWayOption:BaseOption
    {
      
    }
  

}
