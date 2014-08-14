using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G16_2013.Models.MemberModel;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace G16_2013.DAL
{
    public class MemberRepository : IMemberRepository
    {
        #region Fields and Properties
        private G16MemberContext _context;
        G16MemberContext memberContext
        {

            get
            {
                if (_context == null) _context = new G16MemberContext();
                return _context;
            }
        }
        private G16ApplicationUserManager _userManager;
        G16ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    InitialUserManager();
                }

                return _userManager;
            }

        }
        private G16ApplicationRoleManager _roleManager;
        G16ApplicationRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    InitialRoleManager();
                }

                return _roleManager;
            }
        }

        void InitialUserManager()
        {
            _userManager = new G16ApplicationUserManager(new UserStore<G16ApplicationUser>(memberContext));
            //if (HttpContext.Current == null)
            //{
            //    _userManager = new G16ApplicationUserManager(new UserStore<G16ApplicationUser>(MemberContext));

            //}
            //else
            //{
            //    _userManager = HttpContext.Current.GetOwinContext().GetUserManager<G16ApplicationUserManager>();

            //}
        }
        void InitialRoleManager()
        {
            _roleManager = new G16ApplicationRoleManager(new RoleStore<IdentityRole>(memberContext));
            //if (HttpContext.Current == null)
            //{
            //    _roleManager = new G16ApplicationRoleManager(new RoleStore<IdentityRole>(MemberContext));

            //}
            //else
            //{

            //    _roleManager = HttpContext.Current.GetOwinContext().Get<G16ApplicationRoleManager>();

            //}
        }
        #endregion
        public MemberRepository()
        {

        }

        #region Person
        public Person InsertPerson(Person person)
        {
            memberContext.People.Add(person);

            return memberContext.SaveChanges() > 0 ? person : null;
        }
        public Person UpdatePerson(Person person)
        {
            memberContext.Entry(person).State = EntityState.Modified;

            return memberContext.SaveChanges() > 0 ? person : null;
        }
        public Person GetPersonById(int personId)
        {
            Person person = memberContext.People.Find(personId);
            if (person == null) return null;
            if (person.Removed) return null;
            return person;
        }
        public IQueryable<Person> GetAllPerson()
        {
            return memberContext.People.Where(p => !p.Removed);
        }
       
        #region PersonContactInfo
        //public PersonContactInfo InsertPersonContactInfo(PersonContactInfo personContactInfo)
        //{
        //    memberContext.PersonContactInfos.Add(personContactInfo);

        //    return memberContext.SaveChanges() > 0 ? personContactInfo : null;
        //}
        public PersonContactInfo UpdatePersonContactInfo(PersonContactInfo personContactInfo)
        {
            memberContext.Entry(personContactInfo).State = EntityState.Modified;

            return memberContext.SaveChanges() > 0 ? personContactInfo : null;
        }
       
        public PersonContactInfo GetPersonContactInfoById(int personId)
        {
            PersonContactInfo entry = memberContext.PersonContactInfos.Find(personId);
            if (entry == null) return null;
            if (entry.Removed) return null;
            return entry;
        }
        #region Address
        public List<City> GetCities()
        {
            return memberContext.Cities.ToList();
        }

        public City GetCityById(int id)
        {
            return memberContext.Cities.Find(id);
        }
       
        public District GetDistrictById(int id)
        {
            return memberContext.Districts.Find(id);
        }
        public List<District> GetDistrictsByCity(int cityId)
        {
            return memberContext.Districts.Where(d => d.CityId == cityId).ToList();
        }
        #endregion
        #endregion
        #endregion
        #region Account
        public Account InsertAccount(Account account)
        {
            memberContext.Accounts.Add(account);

            return memberContext.SaveChanges() > 0 ? account : null;
        }
        public Account UpdateAccount(Account account)
        {
            memberContext.Entry(account).State = EntityState.Modified;

            return memberContext.SaveChanges() > 0 ? account : null;
        }      
        public Account GetAccountById(int accountId)
        {
            return memberContext.Accounts.Find(accountId);
        }
        public IQueryable<Account> GetAllAccounts()
        {
              return memberContext.Accounts.Where(a=>!a.Removed);
        }
        #endregion


        #region Identity
        #region UserManager Fuctions
        public G16ApplicationUser InsertUser(G16ApplicationUser user, string passWord)
        {
            var result = UserManager.Create(user, passWord);
            return result.Succeeded ? user : null;
        }      
        public G16ApplicationUser UpdateUser(G16ApplicationUser user)
        {
            var result = UserManager.Update(user);
            return result.Succeeded ? user : null;
        }
        public G16ApplicationUser GetUser(string userName, string passWord)
        {
            return UserManager.Find(userName, passWord);
        }
        public G16ApplicationUser GetUserById(string userId)
        {
            return UserManager.FindById(userId);
        }
        public IQueryable<G16ApplicationUser> GetAllUsers()
        {
            return UserManager.Users;
        }
        public G16ApplicationUser FindUserByUserName(string userName)
        {
            return UserManager.FindByName(userName);
        }
        public bool IsUserInRole(string userId, string roleName)
        {
            return UserManager.IsInRole(userId, roleName);
        }
        public G16ApplicationUser AddUserToRole(G16ApplicationUser user, string roleName)
        {
            var result = UserManager.AddToRole(user.Id, roleName);
            return result.Succeeded ? user : null;
        }
        public G16ApplicationUser RemoveUserFromRole(G16ApplicationUser user, string role)
        {
            var result = UserManager.RemoveFromRole(user.Id, role);
            return result.Succeeded ? user : null;
        }
        public void SendMailToUser(string userId, string subject, string body)
        {
            UserManager.SendEmail(userId, subject, body);
        }
        public string GenerateEmailConfirmationToken(string userId)
        {
            return UserManager.GenerateEmailConfirmationToken(userId);
        }
        public bool ConfirmEmail(string userId, string tokenCode)
        {
            var result = UserManager.ConfirmEmail(userId, tokenCode);
            return result.Succeeded ? true : false;
        }
        //public G16ApplicationUser FindUserByPerson(int personId)
        //{
        //    var user = memberContext.Users.Where(u => u.Person.PersonId == personId).FirstOrDefault();
        //    return user;
        //}
        #endregion

        #region RoleManager Fuctions
        public IdentityRole InsertRole(string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = RoleManager.Create(role);
            return result.Succeeded ? role : null;
        }
        public IdentityRole UpdateRole(IdentityRole role)
        {
            memberContext.Entry(role).State = EntityState.Modified;
            return memberContext.SaveChanges() > 0 ? role : null;
        }
        public IQueryable<IdentityRole> GetAllRoles()
        {
            return RoleManager.Roles;
        }
       
        public IdentityRole GetRollByName(string roleName)
        {
            return RoleManager.FindByName(roleName);
        }
        public bool IsRoleExsits(string roleName)
        {
            return RoleManager.RoleExists(roleName);

        }
       
        #endregion

        #region Custom Identity
        public Identity InsertIdentity(Identity identity)
        {
            memberContext.Identities.Add(identity);

            return memberContext.SaveChanges() > 0 ? identity : null;
        }
        public Identity UpdateIdentity(Identity identity)
        {
            memberContext.Entry(identity).State = EntityState.Modified;

            return memberContext.SaveChanges() > 0 ? identity : null;
        }
        public Identity GetIdentityById(int id)
        {
            Identity entry = memberContext.Identities.Find(id);
            if (entry == null) return null;
            if (entry.Removed) return null;
            return entry;
        }

        public IQueryable<Identity> GetAllIdentities()
        {
            return memberContext.Identities.Where(i => !i.Removed);
        }
       
        //public Identity GetPublicIdentityByName(string name)
        //{
        //    return memberContext.Identities.OfType<Identity>()
        //        .Where(i => i.Removed == false && i.Name == name).FirstOrDefault();
        //}
        //public List<Identity> GetPublicIdentities()
        //{
        //    return memberContext.Identities.OfType<Identity>()
        //                        .Where(i => i.Removed == false).ToList();
        //}
        //public CustomIdentity GetCustomIdentityByName(string name, int createPersonId)
        //{
        //    return memberContext.Identities.OfType<CustomIdentity>()
        //        .Where(i =>i.Removed==false && i.CreatePersonId == createPersonId && i.Name == name).FirstOrDefault();
        //}
        //public List<CustomIdentity> GetCustomIdentities(int createPersonId)
        //{
        //    return memberContext.Identities.OfType<CustomIdentity>()
        //                        .Where(i =>i.Removed==false && i.CreatePersonId == createPersonId).ToList();               
        //}
        #endregion
        #endregion

        #region AE
        public AE InsertAE(AE AE)
        {
            memberContext.AEs.Add(AE);
            return memberContext.SaveChanges() > 0 ? AE : null;
        }
        public AE UpdateAE(AE AE)
        {
            memberContext.Entry(AE).State = EntityState.Modified;
            return memberContext.SaveChanges() > 0 ? AE : null;
        }      
       
        public AE GetAEById(int AEId)
        {
            var entry = memberContext.AEs.Find(AEId);
            if (entry == null) return null;
            if (entry.Removed) return null;
            return entry;
           
        }
        public IQueryable<AE> GetAllAEs()
        {
            return memberContext.AEs.Where(ae=>!ae.Removed);
        }
       
        #endregion
        #region AECode
        public AECode InsertAECode(AECode code)
        {
            memberContext.AECodes.Add(code);
            return memberContext.SaveChanges() > 0 ? code : null;
        }
        public AECode UpdateAECode(AECode code)
        {
            memberContext.Entry(code).State = EntityState.Modified;
            return memberContext.SaveChanges() > 0 ? code : null;
        }
     
        public AECode GetAECodeById(int codeId)
        {
            var entry = memberContext.AECodes.Find(codeId);
            if (entry == null) return null;
            if (entry.Removed) return null;
            return entry;            
        }
        public IQueryable<AECode> GetAllAECodes()
        {
             return memberContext.AECodes.Where(a=>!a.Removed);            
        }


        #endregion


        #region Company
        public Company InsertCompany(Company company)
        {
            memberContext.Companies.Add(company);
            return memberContext.SaveChanges() > 0 ? company : null;
        }
       
        public Company UpdateCompany(Company company)
        {
            memberContext.Entry(company).State = EntityState.Modified;
            return memberContext.SaveChanges() > 0 ? company : null;
        }
       
        public Company GetCompanyById(int companyId)
        {
            var entry = memberContext.Companies.Find(companyId);
            if (entry == null) return null;
            if (entry.Removed) return null;
            return entry;
            
        }

        public IQueryable<Company> GetAllCompanies()
        {
            return memberContext.Companies.Where(c =>!c.Removed);
        }
      
       

        #endregion
        #region CompanyAE
        public CompanyAE InsertCompanyAE(CompanyAE companyAE)
        {
            memberContext.CompaniesAEs.Add(companyAE);
            return memberContext.SaveChanges() > 0 ? companyAE : null;
        }
        public CompanyAE UpdateCompanyAE(CompanyAE companyAE)
        {
            memberContext.Entry(companyAE).State = EntityState.Modified;
            return memberContext.SaveChanges() > 0 ? companyAE : null;
        }      
        public CompanyAE GetCompanyAEById(int id)
        {
            var entry = memberContext.CompaniesAEs.Find(id);
            if (entry == null) return null;
            if (entry.Removed) return null;
            return entry;
            
        }
        public IQueryable<CompanyAE> GetAllCompanyAERecords()
        {
            return memberContext.CompaniesAEs.Where(c => !c.Removed);
        }

        #endregion

        #region  TextTradeRecord

        #region FuturesTextTradeRecord
        public TextFuturesTradeRecord InsertTextFuturesTradeRecord(TextFuturesTradeRecord record)
        {
            memberContext.TextFuturesTradeRecords.Add(record);

            return memberContext.SaveChanges() > 0 ? record : null;
        }
        public TextFuturesTradeRecord UpdateTextFuturesTradeRecord(TextFuturesTradeRecord record)
        {
            memberContext.Entry(record).State = EntityState.Modified;

            return memberContext.SaveChanges() > 0 ? record : null;
        }
        public TextFuturesTradeRecord GetTextFuturesTradeRecordById(int id)
        {
            var entry = memberContext.TextFuturesTradeRecords.Find(id);
            if (entry == null) return null;
            if (entry.Removed) return null;
            return entry;
          
        }
        public IQueryable<TextFuturesTradeRecord> GetAllTextFuturesTradeRecords()
        {
            return memberContext.TextFuturesTradeRecords.Where(t =>!t.Removed);
        }
        //public List<TextFuturesTradeRecord> GetTextFuturesTradeRecordByDate(DateTime date)
        //{
        //    return GetAllTextFuturesTradeRecords().Where(t => t.DateOfTrade == date).ToList();
        //}
        //public int InsertMultiTextFuturesTradeRecord(List<TextFuturesTradeRecord> listTextFuturesTradeRecord)
        //{
        //    foreach (var record in listTextFuturesTradeRecord)
        //    {
        //        memberContext.TextFuturesTradeRecords.Add(record);
        //    }
        //    return memberContext.SaveChanges();
        //}
        #endregion

        #region StockTextTradeRecord
        public TextStockTradeRecord InsertTextStockTradeRecord(TextStockTradeRecord record)
        {
            memberContext.TextStockTradeRecords.Add(record);

            return memberContext.SaveChanges() > 0 ? record : null;
        }
        public TextStockTradeRecord UpdateTextStockTradeRecord(TextStockTradeRecord record)
        {
            memberContext.Entry(record).State = EntityState.Modified;

            return memberContext.SaveChanges() > 0 ? record : null;
        }       
        public TextStockTradeRecord GetTextStockTradeRecordById(int id)
        {
            var entry = memberContext.TextStockTradeRecords.Find(id);
            if (entry == null) return null;
            if (entry.Removed) return null;
            return entry;
           
        }
        //public int InsertMultiTextStockTradeRecord(List<TextStockTradeRecord> listTextStockTradeRecord)
        //{
        //    foreach (var record in listTextStockTradeRecord)
        //    {
        //        memberContext.TextStockTradeRecords.Add(record);
        //    }
        //    return memberContext.SaveChanges();
        //}
        public IQueryable<TextStockTradeRecord> GetAllTextStockTradeRecords()
        {
            return memberContext.TextStockTradeRecords.Where(t => !t.Removed);
        }

        #endregion

        #endregion

        #region Dispose
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    memberContext.Dispose();
                    UserManager.Dispose();
                    RoleManager.Dispose();
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
