using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using G16_2013.Models.MemberModel;
using G16_2013.Models.MemberModel.Configurations;
using System.Data.Entity;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace G16_2013.DAL
{
  
    public class G16MemberContext :IdentityDbContext<G16ApplicationUser>
    {
        public DbSet<Person> People { get; set; }       
        public DbSet<Identity> Identities { get; set; }
        public DbSet<PersonContactInfo> PersonContactInfos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
    
        
      
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyAE> CompaniesAEs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AE> AEs { get; set; }
        public DbSet<AECode> AECodes { get; set; }

        public DbSet<TextFuturesTradeRecord> TextFuturesTradeRecords { get; set; }
        public DbSet<TextStockTradeRecord> TextStockTradeRecords { get; set; }


        public G16MemberContext()
            : base("G16MemberContext", throwIfV1Schema: false)
        {
        }
        static G16MemberContext()
        {
            // Set the database intializer which is run once during application start
            
           //Database.SetInitializer<G16MemberContext>(new DropCreateMemberDatabaseWithSeedData());
           // Database.SetInitializer<G16MemberContext>(new DropCreateMemberDatabaseIfModelChanges());
            
        }
        public static G16MemberContext Create()
        {
            return new G16MemberContext();
        }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new PersonContactInfoConfig());
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
           

            modelBuilder.ComplexType<Address>();
         
            base.OnModelCreating(modelBuilder);
        }

       

    }


    public class G16ApplicationUserManager : UserManager<G16ApplicationUser>
    {
        public G16ApplicationUserManager(IUserStore<G16ApplicationUser> store)
            : base(store)
        {
        }

        public static G16ApplicationUserManager Create(IdentityFactoryOptions<G16ApplicationUserManager> options, IOwinContext context)
        {

            var manager = new G16ApplicationUserManager(new UserStore<G16ApplicationUser>(context.Get<G16MemberContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<G16ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<G16ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<G16ApplicationUser>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<G16ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        /// <summary>
        /// Method to add user to multiple roles
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="roles">list of role names</param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> AddUserToRolesAsync(string userId, IList<string> roles)
        {
            var userRoleStore = (IUserRoleStore<G16ApplicationUser, string>)Store;

            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid user Id");
            }

            var userRoles = await userRoleStore.GetRolesAsync(user).ConfigureAwait(false);
            // Add user to each role using UserRoleStore
            foreach (var role in roles.Where(role => !userRoles.Contains(role)))
            {
                await userRoleStore.AddToRoleAsync(user, role).ConfigureAwait(false);
            }

            // Call update once when all roles are added
            return await UpdateAsync(user).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove user from multiple roles
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="roles">list of role names</param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> RemoveUserFromRolesAsync(string userId, IList<string> roles)
        {
            var userRoleStore = (IUserRoleStore<G16ApplicationUser, string>)Store;

            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid user Id");
            }

            var userRoles = await userRoleStore.GetRolesAsync(user).ConfigureAwait(false);
            // Remove user to each role using UserRoleStore
            foreach (var role in roles.Where(userRoles.Contains))
            {
                await userRoleStore.RemoveFromRoleAsync(user, role).ConfigureAwait(false);
            }

            // Call update once when all roles are removed
            return await UpdateAsync(user).ConfigureAwait(false);
        }
    }
    public class G16ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public G16ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static G16ApplicationRoleManager Create(IdentityFactoryOptions<G16ApplicationRoleManager> options, IOwinContext context)
        {
            var manager = new G16ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<G16MemberContext>()));

            return manager;
        }
    }

     

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            //return Task.FromResult(0);
            MailMessage email = new MailMessage("g16@livemail.tw", message.Destination);
            email.Subject = message.Subject;
            email.Body = message.Body;
            email.IsBodyHtml = true;

            var mailClient = new SmtpClient("smtp.live.com", 587)
            {
                Credentials = new NetworkCredential("g16@livemail.tw", "bonds25"),
                EnableSsl=true                
            };

            return mailClient.SendMailAsync(email);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public enum SignInStatus
    {
        Success,
        LockedOut,
        RequiresTwoFactorAuthentication,
        Failure
    }
    // These help with sign and two factor (will possibly be moved into identity framework itself)
    public class SignInHelper
    {
        public SignInHelper(G16ApplicationUserManager userManager, IAuthenticationManager authManager)
        {
            UserManager = userManager;
            AuthenticationManager = authManager;
        }

        public G16ApplicationUserManager UserManager { get; private set; }
        public IAuthenticationManager AuthenticationManager { get; private set; }

        public async Task SignInAsync(G16ApplicationUser user, bool isPersistent, bool rememberBrowser)
        {
            // Clear any partial cookies from external or two factor partial sign ins
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            var userIdentity = await user.GenerateUserIdentityAsync(UserManager);
            if (rememberBrowser)
            {
                var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
            }
            else
            {
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
            }
        }

        public async Task<bool> SendTwoFactorCode(string provider)
        {
            var userId = await GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return false;
            }

            var token = await UserManager.GenerateTwoFactorTokenAsync(userId, provider);
            // See IdentityConfig.cs to plug in Email/SMS services to actually send the code
            await UserManager.NotifyTwoFactorTokenAsync(userId, provider, token);
            return true;
        }

        public async Task<string> GetVerifiedUserIdAsync()
        {
            var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie);
            if (result != null && result.Identity != null && !String.IsNullOrEmpty(result.Identity.GetUserId()))
            {
                return result.Identity.GetUserId();
            }
            return null;
        }

        public async Task<bool> HasBeenVerified()
        {
            return await GetVerifiedUserIdAsync() != null;
        }

        public async Task<SignInStatus> TwoFactorSignIn(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            var userId = await GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return SignInStatus.Failure;
            }
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await UserManager.IsLockedOutAsync(user.Id))
            {
                return SignInStatus.LockedOut;
            }
            if (await UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code))
            {
                // When token is verified correctly, clear the access failed count used for lockout
                await UserManager.ResetAccessFailedCountAsync(user.Id);
                await SignInAsync(user, isPersistent, rememberBrowser);
                return SignInStatus.Success;
            }
            // If the token is incorrect, record the failure which also may cause the user to be locked out
            await UserManager.AccessFailedAsync(user.Id);
            return SignInStatus.Failure;
        }

        public async Task<SignInStatus> ExternalSignIn(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await UserManager.IsLockedOutAsync(user.Id))
            {
                return SignInStatus.LockedOut;
            }
            return await SignInOrTwoFactor(user, isPersistent);
        }

        private async Task<SignInStatus> SignInOrTwoFactor(G16ApplicationUser user, bool isPersistent)
        {
            if (await UserManager.GetTwoFactorEnabledAsync(user.Id) &&
                !await AuthenticationManager.TwoFactorBrowserRememberedAsync(user.Id))
            {
                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                AuthenticationManager.SignIn(identity);
                return SignInStatus.RequiresTwoFactorAuthentication;
            }
            await SignInAsync(user, isPersistent, false);
            return SignInStatus.Success;

        }

        public async Task<SignInStatus> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await UserManager.IsLockedOutAsync(user.Id))
            {
                return SignInStatus.LockedOut;
            }
            if (await UserManager.CheckPasswordAsync(user, password))
            {
                return await SignInOrTwoFactor(user, isPersistent);
            }
            if (shouldLockout)
            {
                // If lockout is requested, increment access failed count which might lock out the user
                await UserManager.AccessFailedAsync(user.Id);
                if (await UserManager.IsLockedOutAsync(user.Id))
                {
                    return SignInStatus.LockedOut;
                }
            }
            return SignInStatus.Failure;
        }
    }


}
