using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G16_2013.Models.MemberModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace G16_2013.DAL
{
    public interface IMemberRepository : IDisposable
    {
        #region Address
        List<City> GetCities();
        City GetCityById(int id);
        District GetDistrictById(int id);
        List<District> GetDistrictsByCity(int cityId);
        #endregion

        #region Company
        Company InsertCompany(Company company);       
        Company UpdateCompany(Company company);       
        Company GetCompanyById(int companyId);
        IQueryable<Company> GetAllCompanies();
       
       
        #endregion

        #region CompanyAE
        CompanyAE InsertCompanyAE(CompanyAE companyAE);
        CompanyAE UpdateCompanyAE(CompanyAE companyAE);
        CompanyAE GetCompanyAEById(int id);
        IQueryable<CompanyAE> GetAllCompanyAERecords();


        #endregion

        #region Person
        Person InsertPerson(Person person);
        Person UpdatePerson(Person person);
        Person GetPersonById(int personId);
        IQueryable<Person> GetAllPerson();
       

        #region PersonContactInfo
        //PersonContactInfo InsertPersonContactInfo(PersonContactInfo personContactInfo);
        PersonContactInfo UpdatePersonContactInfo(PersonContactInfo personContactInfo);     
        PersonContactInfo GetPersonContactInfoById(int personId);
       
        #endregion
        #endregion


        #region Account
        Account InsertAccount(Account account);
        Account UpdateAccount(Account account);
        Account GetAccountById(int accountId);
        IQueryable<Account> GetAllAccounts();
      
        #endregion


        #region Identity
        #region UserManager Fuctions
        G16ApplicationUser InsertUser(G16ApplicationUser user, string passWord);
        G16ApplicationUser UpdateUser(G16ApplicationUser user);        
        G16ApplicationUser GetUser(string userName, string passWord);
        G16ApplicationUser GetUserById(string userId);
        IQueryable<G16ApplicationUser> GetAllUsers();
        G16ApplicationUser FindUserByUserName(string userName);
        G16ApplicationUser AddUserToRole(G16ApplicationUser user, string roleName);
        G16ApplicationUser RemoveUserFromRole(G16ApplicationUser user, string role);
        bool IsUserInRole(string userId, string roleName);
        void SendMailToUser(string userId, string subject, string body);
        string GenerateEmailConfirmationToken(string userId);
        bool ConfirmEmail(string userId, string tokenCode);
        #endregion

        #region RoleManager Fuctions
        IdentityRole InsertRole(string roleName);
        IdentityRole UpdateRole(IdentityRole role);
        IQueryable<IdentityRole> GetAllRoles();
        IdentityRole GetRollByName(string roleName);
        bool IsRoleExsits(string roleName);
       
        #endregion

        #region Custom Identity        
        Identity InsertIdentity(Identity identity);
        Identity UpdateIdentity(Identity identity);       
        Identity GetIdentityById(int id);
        IQueryable<Identity> GetAllIdentities();

        #endregion
        #endregion

        #region AE
        AE InsertAE(AE AE);
        AE UpdateAE(AE AE);  
        AE GetAEById(int AEId);
        IQueryable<AE> GetAllAEs();
     
        #endregion

        #region AECode
        AECode InsertAECode(AECode code);
        AECode UpdateAECode(AECode code);
        AECode GetAECodeById(int codeId);
        IQueryable<AECode> GetAllAECodes();
       

        #endregion

        #region FuturesTradeRecord
        TextFuturesTradeRecord InsertTextFuturesTradeRecord(TextFuturesTradeRecord record);
        TextFuturesTradeRecord UpdateTextFuturesTradeRecord(TextFuturesTradeRecord record);    
        TextFuturesTradeRecord GetTextFuturesTradeRecordById(int id);
        IQueryable<TextFuturesTradeRecord> GetAllTextFuturesTradeRecords();
        #endregion

        #region StockTradeRecord
        TextStockTradeRecord InsertTextStockTradeRecord(TextStockTradeRecord record);
        TextStockTradeRecord UpdateTextStockTradeRecord(TextStockTradeRecord record);
        TextStockTradeRecord GetTextStockTradeRecordById(int id);
        IQueryable<TextStockTradeRecord> GetAllTextStockTradeRecords();

        #endregion
      
    }
}
