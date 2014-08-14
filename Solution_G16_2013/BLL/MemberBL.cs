using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using G16_2013.DAL;
using G16_2013.Models.MemberModel;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace G16_2013.BLL
{
    public class MemberBL : IDisposable
    {
        #region Fields as Properties
        private IMemberRepository memberRepository;
        public MemberBL()
        {
            this.memberRepository = new MemberRepository();
        }
        public MemberBL(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public MemberBL(G16ApplicationUser user)
        {
            User = user;
            this.memberRepository = new MemberRepository();
        }
        G16ApplicationUser User { get; set; }
        #endregion

        #region Help Methods
        void SetEditRecord(BaseEntity entry)
        {
            entry.UpdatedBy = User.Id;
            entry.LastUpdated = MyHelper.GetTaipeiTimeFromUtc(DateTime.UtcNow);

        }
        void SetCreateRecord(BaseEntity entry)
        {
            entry.Removed = false;
            entry.CreatedBy = User.Id;
            entry.CreatedDate = MyHelper.GetTaipeiTimeFromUtc(DateTime.UtcNow);
            SetEditRecord(entry);
        }
        #region Get Options
        public List<BaseOption> GetCityOptions()
        {
            var cities = GetCities();
            var options = new List<BaseOption>();
            foreach (var city in cities)
            {
                options.Add(new BaseOption(city.Name, city.CityId.ToString()));
            }
            return options;
        }
        public List<BaseOption> GetDistrictOptions(int cityId)
        {
            var districts = GetDistrictsByCity(cityId);
            var options = new List<BaseOption>();
            foreach (var d in districts)
            {
                options.Add(new BaseOption(d.Name, d.DistrictId.ToString()));
            }
            return options;
        }
        List<BaseOption> GetCompanyOptions()
        {
            var companies = GetParentCompanies();
            var options = new List<BaseOption>();
            foreach (var c in companies)
            {
                options.Add(new BaseOption(c.Name, c.CompanyId.ToString()));
            }
            return options;
        }
        public List<BaseOption> GetBranchOptions(int companyId)
        {
            var branches = GetBranchsByCompany(companyId).OrderBy(c => c.DisplayOrder);
            var options = new List<BaseOption>();
            options.Add(new BaseOption("-----", "0"));
            foreach (var b in branches)
            {
                options.Add(new BaseOption(b.Name, b.CompanyId.ToString()));
            }
            return options;
        }
        List<BaseOption> GetPersonOptions()
        {
            var people = GetAllPerson().ToList();
            var options = new List<BaseOption>();
            foreach (var p in people)
            {
                options.Add(new BaseOption(p.Name, p.PersonId.ToString()));
            }
            return options;
        }
        List<BaseOption> GetCustomIdentityOptions()
        {
            if (User == null) return null;
            if (User.Person == null) return null;
            var customIdentities = GetCustomIdentities(User.Person.PersonId);
            if (customIdentities == null) return null;

            var options = new List<BaseOption>();
            foreach (var identity in customIdentities)
            {
                options.Add(new BaseOption(identity.Name, identity.IdentityId.ToString()));
            }
            return options;
        }
        List<BaseOption> GetAECodeOptions(int companyId, int type)
        {
            var codes = GetAECodesByCompanyByType(companyId, (BusinessType)type);
            var options = new List<BaseOption>();
            foreach (var c in codes)
            {
                options.Add(new BaseOption(Helper.GetAECodeText(c), c.AECodeId.ToString()));
            }
            return options;
        }
        List<BaseOption> GetAEOptions(int companyId)
        {
            var listAE = GetAEsByCompany(companyId);
            if (listAE == null) return null;
            var options = new List<BaseOption>();
            foreach (var ae in listAE)
            {
                options.Add(new BaseOption(ae.Person.Name, ae.AEId.ToString()));
            }
            return options;
        }
        public List<BaseOption> GetBankAccountCurrencyList()
        {
            List<BaseOption> bankAccountCurrencyList = new List<BaseOption>();

            string[] currencies = new string[] { "台幣", "綜合外幣", "美元", "港幣", "日圓", "歐元" };
            for (int i = 0; i < currencies.Length; i++)
            {
                var currencyOption = new BaseOption()
                {
                    Text = currencies[i],
                    Value = i.ToString()
                };
                bankAccountCurrencyList.Add(currencyOption);
            }
            return bankAccountCurrencyList;
        }

        #endregion
        #endregion

        #region Address
        List<City> GetCities()
        {
            return memberRepository.GetCities();
        }
        City GetCityById(int id)
        {
            return memberRepository.GetCityById(id);
        }


        District GetDistrictById(int id)
        {
            return memberRepository.GetDistrictById(id);
        }
        public string GetZipCodeByDistrict(int districtId)
        {
            var district = GetDistrictById(districtId);
            if (district == null) return "";
            return district.ZipCode;
        }
        List<District> GetDistrictsByCity(int cityId)
        {
            return memberRepository.GetDistrictsByCity(cityId);
        }

        #endregion

        #region Customer
        #region Public Functions

        public FuturesCustomerInput GetFuturesCustomerCreateInput()
        {
            FuturesCustomerInput futuresCustomerInput = new FuturesCustomerInput();
            futuresCustomerInput.PersonSelectCreateInput = GetPersonSelectCreateInput();
            futuresCustomerInput.FuturesAccountInput = GetFuturesAccountCreateInputModel();

            return futuresCustomerInput;
        }
        public Person CreateCustomerWithAccount(AccountInputModel accountInput)
        {
            //var person = new Person();
            //var personInput=accountInput.PersonInput;
            //SetPersonValuesFromInput(person, personInput);
            //SetCreateRecord(person);

            //person.ContactInfo = new PersonContactInfo();
            //var contactInfoInput = personInput.ContactInfoInput;
            //SetContactInfoValuesFromInput(person.ContactInfo, contactInfoInput);
            //SetCreateRecord(person.ContactInfo);

            if (accountInput.AccountType == Convert.ToInt32(BusinessType.Futures))
            {
                FuturesAccount futuresAccount = new FuturesAccount();
                SetAccountValuesFromInput(futuresAccount, accountInput);
                SetCreateRecord(futuresAccount);
                if (accountInput is FuturesAccountInputModel)
                {
                    FuturesAccountInputModel futuresAccountInputModel = accountInput as FuturesAccountInputModel;
                    SetFuturesAccountBankValuesFromInput(futuresAccount, futuresAccountInputModel);
                }


            }

            return null;

            //var account = new Account();
            //SetAccountValuesFromInput(account, accountInput);
            //SetCreateRecord(account);

            //account.ContactInfo = new AccountContactInfo();
            //var accountContactInfoInput = accountInput.ContactInfoInput;
            //SetAccountContactInfoValuesFromInput(account.ContactInfo, accountContactInfoInput);
            //SetCreateRecord(account.ContactInfo);

            //if (accountInput.AccountType == Convert.ToInt32(BusinessType.Futures))
            //{
            //    FuturesAccount futuresAccount = account as FuturesAccount;
            //    FuturesAccountInputModel futuresAccountInput = accountInput as FuturesAccountInputModel;

            //    SetFuturesAccountBankValuesFromInput(futuresAccount, futuresAccountInput);

            //}

            //if (person.Accounts == null) person.Accounts = new List<Account>();            
            //person.Accounts.Add(account);

            //person = InsertPerson(person);
            //if (person == null) return null;

            ////為Person開通使用者帳號(擁有客戶角色)
            //string passWord = "";
            //var user = CreateUserFromCustomer(person, passWord);
            //if (user == null) return null;

            ////將Person加入客戶身分
            //int isOK=AddUserToRole(user, "客戶");
            //if (isOK <= 0) return null;

            //return person;
        }
        #endregion

        //Person CreateCustomerAppUser(Person person)
        //{
        //    if (person.PersonId <= 0) return null;

        //    if (person.Users == null || person.Users.Count == 0)
        //    {
        //        string passWord = "";
        //        var user = CreateUserFromCustomer(person, passWord);
        //        if (user == null) return null;
        //    }
        //    else
        //    {
        //        var user = person.Users.FirstOrDefault();
        //        bool hasRole = IsUserInRole(user.Id, "客戶");
        //        if (!hasRole) AddUserToRole(user, "客戶");
        //    }
        //}
        public Person CreateCustomerFromPerson(Person person)
        {
            if (person.PersonId <= 0) return null;

            if (person.Users == null || person.Users.Count == 0)
            {
                string passWord = "";
                var user = CreateUserFromCustomer(person, passWord);
                if (user == null) return null;
            }
            else
            {
                var user = person.Users.FirstOrDefault();
                bool hasRole = IsUserInRole(user.Id, "客戶");
                if (!hasRole) AddUserToRole(user, "客戶");
            }

            var identity = GetPublicIdentityByName("客戶");
            if (identity == null) return null;
            int identityId = identity.IdentityId;

            bool hasCustomerIdentity = IsPersonHasIdentity(person, identityId);
            if (!hasCustomerIdentity)
            {
                person = AddPersonToIdentity(person, identity);
            }
            return person;

        }
        #endregion

        #region Person

        #region Public Functions

        #region   Filter Fuctions
        public List<Person> GetPeopleByKeyWord(int searchWay, string keyWord)
        {
            var allPerson = GetAllPerson();
            switch (searchWay)
            {
                case 0:
                    return allPerson.Where(m => !m.Removed && m.Name.Contains(keyWord)).ToList();

                case 1:
                    return allPerson.Where(m => !m.Removed && m.ContactInfo.Phone.Contains(keyWord)).ToList();

                case 2:
                    return allPerson.Where(m => !m.Removed && m.ContactInfo.Email.Contains(keyWord)).ToList();

                case 3:

                    var allAccounts = GetAllAccounts();
                    var accounts = allAccounts.Where(a => !a.Removed && a.AccountNumber.Contains(keyWord)).ToList();
                    if (accounts == null) return null;

                    var accountOwners = (from a in accounts select a.Owner).Distinct();
                    var activeOwners = accountOwners.Where(p => !p.Removed);
                    return accountOwners.ToList();

                default:
                    return null;
            }
        }
        #endregion

        #region  Create , Update , Remove
        public Person CreatePerson(PersonInputModel personInput)
        {
            var person = new Person();
            SetPersonValuesFromInput(person, personInput);
            SetCreateRecord(person);

            var contactInfo = new PersonContactInfo();
            SetContactInfoValuesFromInput(contactInfo, personInput.ContactInfoInput);
            SetCreateRecord(contactInfo);

            person.ContactInfo = contactInfo;

            person = InsertPerson(person);
            return person ?? null;
        }
        public Person UpdatePerson(PersonInputModel personInput)
        {
            var person = GetPersonById(personInput.PersonId);
            if (person == null) return null;

            SetPersonValuesFromInput(person, personInput);
            SetEditRecord(person);

            person = UpdatePerson(person);
            return person ?? null;
        }
        public Person UpdatePerson(PersonInputModel personInput, PersonContactInfoInputModel contactInfoInput)
        {
            var person = GetPersonById(personInput.PersonId);
            if (person == null) return null;

            SetPersonValuesFromInput(person, personInput);
            SetEditRecord(person);

            SetContactInfoValuesFromInput(person.ContactInfo, contactInfoInput);
            SetEditRecord(person.ContactInfo);

            person = UpdatePerson(person);
            return person ?? null;
        }
        public PersonContactInfo UpdatePersonContactInfo(PersonContactInfoInputModel input)
        {
            var contactInfo = GetPersonContactInfoById(input.Id);
            if (contactInfo == null) return null;

            SetContactInfoValuesFromInput(contactInfo, input);
            SetEditRecord(contactInfo);

            contactInfo = UpdatePersonContactInfo(contactInfo);
            return contactInfo ?? null;
        }
        public int RemovePerson(int personId)
        {
            var person = GetPersonById(personId);
            person.ContactInfo.Removed = true;
            person.Removed = true;
            person = memberRepository.UpdatePerson(person);
            return (person != null) ? 1 : 0;
        }
        #endregion

        #region Get Input & ViewModel
        public PersonInputModel GetPersonCreateInputModel()
        {
            PersonInputModel personInput = CreatePersonInput();
            personInput.Birthday = new DateTime(1980, 7, 1);
            personInput.ContactInfoInput = CreatePersonContactInfoInput();
            return personInput;
        }
        public PersonInputModel GetPersonInputModel(int personId = 0)
        {
            if (personId == 0) return CreatePersonInput();
            var person = GetPersonById(personId);
            if (person == null) return new PersonInputModel();
            return CreatePersonInput(person);
        }
        public PersonContactInfoInputModel GetPersonContactInfoInput(int personId = 0)
        {
            if (personId == 0) return CreatePersonContactInfoInput();

            var contactInfo = GetPersonContactInfoById(personId);
            if (contactInfo == null) return CreatePersonContactInfoInput();
            return CreatePersonContactInfoInput(contactInfo);
        }
        public PersonViewModel GetPersonViewModel(int personId)
        {
            var person = GetPersonById(personId);
            if (person == null) return null;

            return CreatePersonView(person);
        }
        public PersonViewModel GetPersonViewModel(int personId, int contactInfoId)
        {
            var person = GetPersonById(personId);
            if (person == null) return null;
            PersonViewModel personView = CreatePersonView(person);
            personView.ContactInfoView = CreatePersonContactInfoView(person.ContactInfo);

            return personView;
        }
        public PersonContactInfoViewModel GetPersonContactInfoView(int personId)
        {
            var contactInfo = GetPersonContactInfoById(personId);
            if (contactInfo == null) return null;
            return CreatePersonContactInfoView(contactInfo);
        }
        public PersonSelectCreateInput GetPersonSelectCreateInput()
        {
            PersonSelectCreateInput input = new PersonSelectCreateInput();

            input.NewPersonInput = GetPersonCreateInputModel();
            return input;
        }
        public PersonSelectCreateInput GetPersonSelectCreateInput(PersonSelectCreateInput input)
        {
            if (input.NewPersonInput == null)
            {
                input.NewPersonInput = GetPersonCreateInputModel();
            }
            else
            {
                InitializePersonInput(input.NewPersonInput);
                InitializeContactInfoInput(input.NewPersonInput.ContactInfoInput);
            }

            if (input.ExsitPersonId > 0)
            {
                input.ExistPersonView = GetPersonViewModel(input.ExsitPersonId, input.ExsitPersonId);
            }

            return input;
        }
        #endregion

        #region  Helper Fuctions
        public string GetPersonName(int personId)
        {
            var person = GetPersonById(personId);
            if (person == null) return "";
            return person.Name;
        }
        public bool IsPersonExist(int personId)
        {
            if (personId <= 0) return false;
            var person = GetPersonById(personId);
            return (person != null);
        }
        #endregion

        #endregion

        #region Input & ViewModel
        #region Create Input/View
        PersonInputModel CreatePersonInput(Person person = null)
        {
            var inputModel = new PersonInputModel();
            InitializePersonInput(inputModel);
            if (person == null) return inputModel;

            SetPersonInputValues(inputModel, person);
            return inputModel;
        }
        PersonViewModel CreatePersonView(Person person)
        {

            var viewModel = new PersonViewModel()
            {
                GenderText = person.Gender ? "男" : "女"
            };
            SetPersonInputValues(viewModel, person);
            return viewModel;
        }
        PersonContactInfoInputModel CreatePersonContactInfoInput(PersonContactInfo contactInfo = null)
        {
            var inputModel = new PersonContactInfoInputModel()
            {
                AddressInput = new AddressInputModel()
            };
            InitializeContactInfoInput(inputModel, contactInfo);

            //var cityOptions = GetCityOptions();
            //int cityId = 0;
            //if (contactInfo == null) cityId = Convert.ToInt32(cityOptions.FirstOrDefault().Value);
            //else cityId = Convert.ToInt32(contactInfo.Address.City);

            //int districtId = 0;
            //var districtOptions = GetDistrictOptions(cityId);
            //if (contactInfo == null) districtId = Convert.ToInt32(districtOptions.FirstOrDefault().Value);

            //inputModel.AddressInput.CityOptions = cityOptions;
            //inputModel.AddressInput.DistrictOptions = districtOptions;

            //if (contactInfo == null)
            //{
            //    inputModel.AddressInput.CityId = cityId.ToString();
            //    inputModel.AddressInput.DistrictId = districtId.ToString();
            //    return inputModel;
            //}
            if (contactInfo == null) return inputModel;

            SetContactInfoInputValues(inputModel, contactInfo);
            SetAddressInputValues(inputModel.AddressInput, contactInfo.Address);
            return inputModel;
        }
        PersonContactInfoViewModel CreatePersonContactInfoView(PersonContactInfo contactInfo)
        {

            // var inputModel = CreatePersonContactInfoInput(contactInfo);
            var contactInfoViewModel = new PersonContactInfoViewModel();
            SetContactInfoInputValues(contactInfoViewModel, contactInfo);

            var addressView = new AddressViewModel();
            SetAddressInputValues(addressView, contactInfo.Address);
            SetAddressViewValues(addressView, contactInfo.Address);

            contactInfoViewModel.AddressView = addressView;
            //int cityId = Convert.ToInt32(contactInfo.Address.City);
            //int districtId = Convert.ToInt32(contactInfo.Address.District);

            //viewModel.AddressView.DistrictText = GetDistrictById(districtId).Name;
            //viewModel.AddressView.CityText = GetCityById(cityId).Name;

            return contactInfoViewModel;
        }
        #endregion

        #region Initialize Input Values
        void InitializePersonInput(PersonInputModel inputModel)
        {
            inputModel.IdentityOptions = GetCustomIdentityOptions();
        }
        void InitializeContactInfoInput(PersonContactInfoInputModel inputModel, PersonContactInfo contactInfo = null)
        {
            var addressInput = inputModel.AddressInput;
            if (contactInfo == null)
            {
                InitializeAddressInput(addressInput);
            }
            else
            {
                var address = contactInfo.Address;
                InitializeAddressInput(addressInput, address);
            }

        }
        void InitializeAddressInput(AddressInputModel inputModel, Address address = null)
        {
            var cityOptions = GetCityOptions();
            int cityId = 0;
            if (address == null) cityId = Convert.ToInt32(cityOptions.FirstOrDefault().Value);
            else cityId = Convert.ToInt32(address.City);

            int districtId = 0;
            var districtOptions = GetDistrictOptions(cityId);
            if (address == null) districtId = Convert.ToInt32(districtOptions.FirstOrDefault().Value);

            inputModel.CityOptions = cityOptions;
            inputModel.DistrictOptions = districtOptions;

            if (address == null)
            {
                inputModel.CityId = cityId.ToString();
                inputModel.DistrictId = districtId.ToString();
            }
        }
        #endregion

        #region Set Values From Input
        void SetPersonValuesFromInput(Person person, PersonInputModel input)
        {
            person.Name = input.Name;
            person.TWID = input.TWID;
            person.Birthday = input.Birthday;
            person.Gender = input.Gender;
            person.PS = input.PS;

            if (input.IdentityIds != null && input.IdentityIds.Count > 0)
            {
                foreach (int id in input.IdentityIds)
                {
                    var identity = GetIdentityById(id);
                    person.Identities.Add(identity);
                }
            }
        }
        void SetContactInfoValuesFromInput(PersonContactInfo contactInfo, PersonContactInfoInputModel input)
        {
            contactInfo.Email = input.Email;
            contactInfo.TEL = input.TEL;
            contactInfo.Phone = input.Phone;

            var addressInput = input.AddressInput;

            contactInfo.Address.ZipCode = addressInput.ZipCode;
            contactInfo.Address.City = addressInput.CityId;
            contactInfo.Address.District = addressInput.DistrictId;
            contactInfo.Address.StreetAddress = addressInput.Street;
        }
        #endregion

        #region Set Input or View Values
        void SetPersonInputValues(PersonInputModel personInputModel, Person person)
        {
            personInputModel.PersonId = person.PersonId;
            personInputModel.Birthday = person.Birthday;
            personInputModel.Gender = person.Gender;
            personInputModel.Name = person.Name;
            personInputModel.PS = person.PS;
            personInputModel.TWID = person.TWID;


            var identities = person.Identities;
            if (identities != null)
            {
                personInputModel.IdentityIds = new List<int>();
                foreach (var i in identities)
                {
                    personInputModel.IdentityIds.Add(i.IdentityId);
                }
            }
        }
        void SetContactInfoInputValues(PersonContactInfoInputModel inputModel, PersonContactInfo contactInfo)
        {
            inputModel.Id = contactInfo.PersonId;
            inputModel.Email = contactInfo.Email;
            inputModel.Phone = contactInfo.Phone;
            inputModel.TEL = contactInfo.TEL;
        }
        void SetAddressInputValues(AddressInputModel inputModel, Address address)
        {
            inputModel.ZipCode = address.ZipCode;
            inputModel.CityId = address.City;
            inputModel.DistrictId = address.District;
            inputModel.Street = address.StreetAddress;

        }
        void SetAddressViewValues(AddressViewModel viewModel, Address address)
        {
            int cityId = Convert.ToInt32(address.City);
            int districtId = Convert.ToInt32(address.District);

            viewModel.DistrictText = GetDistrictById(districtId).Name;
            viewModel.CityText = GetCityById(cityId).Name;
        }
        #endregion

        #endregion

        #region  memberRepository Fuctions
        Person InsertPerson(Person person)
        {
            SetCreateRecord(person);
            SetCreateRecord(person.ContactInfo);
            return memberRepository.InsertPerson(person);
        }
        Person UpdatePerson(Person person)
        {
            return memberRepository.UpdatePerson(person);
        }
        public Person GetPersonById(int personId)
        {
            return memberRepository.GetPersonById(personId);
        }
        public IQueryable<Person> GetAllPerson()
        {
            return memberRepository.GetAllPerson();
        }
        PersonContactInfo GetPersonContactInfoById(int personId)
        {
            return memberRepository.GetPersonContactInfoById(personId);
        }
        PersonContactInfo UpdatePersonContactInfo(PersonContactInfo contactInfo)
        {
            return memberRepository.UpdatePersonContactInfo(contactInfo);
        }

        #endregion


        #endregion

        #region Account

        #region Public Functions

        #region   Filter Fuctions
        public FuturesAccount GetFuturesAccountById(int id)
        {
            var allAccounts = GetAllAccounts();
            return allAccounts.OfType<FuturesAccount>().
                                          Where(a => a.AccountId == id).FirstOrDefault();
        }
        public List<Account> GetAccountByNumber(string accountNumber)
        {
            var allAccounts = GetAllAccounts();
            return allAccounts.Where(a => a.AccountNumber == accountNumber).ToList();
        }

        public List<StockAccount> GetStockAccountByOwner(int personId)
        {
            var allAccounts = GetAllAccounts();
            return allAccounts.OfType<StockAccount>().Where(a => a.PersonId == personId).ToList();
        }
        public List<StockAccount> GetStockAccountByNumber(string accountNumber)
        {
            var allAccounts = GetAllAccounts();
            return allAccounts.OfType<StockAccount>().Where(a => a.AccountNumber == accountNumber).ToList();
        }
        #endregion

        #region  Create , Update , Remove
        FuturesAccount CreateFuturesAccount(FuturesAccountInputModel futuresAccountInput)
        {
            int personId = futuresAccountInput.PersonId;
            if (!IsPersonExist(personId)) return null;


            var account = new FuturesAccount()
            {
                AccountType = BusinessType.Futures,
                ContactInfo = new AccountContactInfo()
            };
            SetAccountValuesFromInput(account, futuresAccountInput);
            SetCreateRecord(account);

            var contactInfoInput = futuresAccountInput.ContactInfoInput;
            SetAccountContactInfoValuesFromInput(account.ContactInfo, contactInfoInput);
            SetCreateRecord(account.ContactInfo);

            SetFuturesAccountBankValuesFromInput(account, futuresAccountInput);

            if (account.AccountBankInfos != null)
            {
                foreach (var bankInfo in account.AccountBankInfos)
                {
                    SetCreateRecord(bankInfo);
                }
            }
            //if (futuresAccountInput.ReferralId > 0) account.ReferralId = futuresAccountInput.ReferralId;
            //if (futuresAccountInput.OpenAEId > 0) account.OpenAEId = futuresAccountInput.OpenAEId;

            //if (futuresAccountInput.TraderId > 0)
            //{
            //    account.TraderId = futuresAccountInput.TraderId;
            //    account.TraderRecords = new List<AccountTrader>();
            //    AccountTrader traderRecord = new AccountTrader()
            //    {
            //        IsActive = true,
            //        BeginDate = account.OpenDate,
            //        TraderId = futuresAccountInput.TraderId,
            //        IsOfficial = futuresAccountInput.OfficialTrader
            //    };
            //    SetCreateRecord(traderRecord);
            //    account.TraderRecords.Add(traderRecord);
            //}


            AccountAECode aeCodeRecord = new AccountAECode()
            {
                AECodeId = futuresAccountInput.AECodeId,
                IsActive = true,
                BeginDate = account.OpenDate,
            };
            SetCreateRecord(aeCodeRecord);
            if (account.AECodeRecords == null) account.AECodeRecords = new List<AccountAECode>();
            account.AECodeRecords.Add(aeCodeRecord);

            AccountAE aeRecord = new AccountAE()
            {
                AEId = futuresAccountInput.ServiceAEId,
                BeginDate = account.OpenDate,
                IsActive = true,
            };
            SetCreateRecord(aeRecord);
            if (account.ServiceAERecords == null) account.ServiceAERecords = new List<AccountAE>();
            account.ServiceAERecords.Add(aeRecord);


            var result = InsertAccount(account);
            if (result == null) return null;
            return result as FuturesAccount;


        }
        #endregion

        #region Get Input & ViewModel
        public AccountInputModel GetAccountCreateInputModel(int personId = 0)
        {
            var personInput = GetPersonInputModel(personId);
            personInput.ContactInfoInput = GetPersonContactInfoInput(personId);

            var contactInfoInput = new AccountContactInfoInputModel()
            {
                AddressInput = new AddressInputModel(),
                CensusAddressInput = new AddressInputModel()
            };

            InitializeAccountContactInfoInput(contactInfoInput);


            var inputModel = new FuturesAccountInputModel()
            {
                AccountType = Convert.ToInt32(BusinessType.Futures),
                PersonInput = personInput,
                ContactInfoInput = contactInfoInput,
                PersonId = personInput.PersonId,
            };

            InitializeAccountInput(inputModel);

            return inputModel;


        }
        public FuturesAccountInputModel GetFuturesAccountCreateInputModel()
        {
            var contactInfoInput = new AccountContactInfoInputModel()
            {
                AddressInput = new AddressInputModel(),
                CensusAddressInput = new AddressInputModel()
            };
            InitializeAccountContactInfoInput(contactInfoInput);

            var inputModel = new FuturesAccountInputModel()
            {
                AccountType = Convert.ToInt32(BusinessType.Futures),
              
                ContactInfoInput = contactInfoInput,
                
            };
            InitializeAccountInput(inputModel);

            inputModel.TaiwanWithdrawBank = new BankInfoInputModel();
            InitializeAccountBankInfoInput(inputModel.TaiwanWithdrawBank);

            inputModel.TWDepositBanks = new List<BankInfoInputModel>();
            BankInfoInputModel bankInfoInputModel = new BankInfoInputModel();
            InitializeAccountBankInfoInput(bankInfoInputModel);
            inputModel.TWDepositBanks.Add(bankInfoInputModel);

            return inputModel;
        }
        public FuturesAccountInputModel GetFuturesAccountInputModel(int accountId)
        {
            FuturesAccount account = GetFuturesAccountById(accountId);
            if (account == null) return null;

            FuturesAccountInputModel futuresAccountInput = new FuturesAccountInputModel();
            InitializeAccountInput(futuresAccountInput);
            InitializeAccountContactInfoInput(futuresAccountInput.ContactInfoInput);
            SetAccountInputModelValues(futuresAccountInput, account);
            SetAccountContactInfoInputValues(futuresAccountInput.ContactInfoInput, account.ContactInfo);


            futuresAccountInput.TaiwanWithdrawBank = new BankInfoInputModel();
            InitializeAccountBankInfoInput(futuresAccountInput.TaiwanWithdrawBank);
            SetAccountBankInfoInputValues(futuresAccountInput.TaiwanWithdrawBank, account.GetTaiwanWithdrawBank());

            var twDepositBanks = account.GetTWDepositBankList();
            if (twDepositBanks != null)
            {
                futuresAccountInput.TWDepositBanks = new List<BankInfoInputModel>();
                foreach (var bankInfo in twDepositBanks)
                {
                    BankInfoInputModel bankInfoInputModel = new BankInfoInputModel();
                    InitializeAccountBankInfoInput(bankInfoInputModel);
                    SetAccountBankInfoInputValues(bankInfoInputModel, bankInfo);
                    futuresAccountInput.TWDepositBanks.Add(bankInfoInputModel);
                }
            }
            var forexWithdrawBank = account.GetForexWithdrawBank();
            if (forexWithdrawBank != null)
            {
                futuresAccountInput.ForexWithdrawBank = new BankInfoInputModel();
                InitializeAccountBankInfoInput(futuresAccountInput.TaiwanWithdrawBank);
                SetAccountBankInfoInputValues(futuresAccountInput.ForexWithdrawBank, forexWithdrawBank);
            }
            //InitializeAccountBankInfoInput(futuresAccountInput.FirstTWDepositBank);
            ////InitializeAccountBankInfoInput(futuresAccountInput.SecondTWDepositBank);
            ////InitializeAccountBankInfoInput(futuresAccountInput.ThirdTWDepositBank);

            //InitializeAccountBankInfoInput(futuresAccountInput.ForexWithdrawBank);
            //InitializeAccountBankInfoInput(futuresAccountInput.FirstForexDepositBank);
            //InitializeAccountBankInfoInput(futuresAccountInput.SecondForexDepositBank);
            //InitializeAccountBankInfoInput(futuresAccountInput.ThirdForexDepositBank);



            var forexDepositBankList = account.GetForexDepositBankList();
            if (forexDepositBankList != null)
            {
                futuresAccountInput.ForexDepositBanks = new List<BankInfoInputModel>();
                foreach (var bankInfo in forexDepositBankList)
                {
                    BankInfoInputModel bankInfoInputModel = new BankInfoInputModel();
                    InitializeAccountBankInfoInput(bankInfoInputModel);
                    SetAccountBankInfoInputValues(bankInfoInputModel, bankInfo);
                    futuresAccountInput.ForexDepositBanks.Add(bankInfoInputModel);
                }
            }



            return futuresAccountInput;

        }
        public StockAccountInputModel GetStockAccountCreateInput(AccountInputModel input=null)
        {
            //StockAccountInputModel stockInput = new StockAccountInputModel()
            //{
            //    PersonInput=new PersonInputModel(),
            //    ContactInfoInput=new AccountContactInfoInputModel()
            //};
          
            StockAccountInputModel stockInput = new StockAccountInputModel();
            if (input != null)
            {
                if (input is FuturesAccountInputModel)
                {
                    stockInput = (input as AccountInputModel) as StockAccountInputModel;
                }
                 stockInput = input as StockAccountInputModel;
            }
            InitializeAccountInput(stockInput);

            return stockInput;


        }
        public FuturesAccountViewModel GetFuturesAccountView(FuturesAccount futuresAccount)
        {
            if (futuresAccount == null) return null;

            var viewModel = new FuturesAccountViewModel()
            {
                TaiwanWithdrawBankView = CreateBankInfoView(futuresAccount.GetTaiwanWithdrawBank()),
                TWDepositBankViewList = CreateBankInfoViewList(futuresAccount.GetTWDepositBankList()),

                ForexWithdrawBankView = CreateBankInfoView(futuresAccount.GetForexWithdrawBank()),
                ForexDepositBankViewList = CreateBankInfoViewList(futuresAccount.GetForexDepositBankList())
            };
            SetAccountViewValues(viewModel, futuresAccount);

            return viewModel;
        }
        public AccountContactInfoViewModel GetAccountContactInfoView(AccountContactInfo contactInfo)
        {
            if (contactInfo == null) return null;
            var viewModel = new AccountContactInfoViewModel();

            SetAccountContactInfoViewValues(viewModel, contactInfo);

            return viewModel;
        }
        public StockAccountViewModel GetStockAccountView(StockAccount stockAccount)
        {
            if (stockAccount == null) return null;

            var viewModel = new StockAccountViewModel();

            viewModel.FinancingQuota = stockAccount.FinancingQuota.ToString();

            viewModel.BorrowingQuota = stockAccount.BorrowingQuota.ToString();

            SetAccountViewValues(viewModel, stockAccount);

            return viewModel;
        }

        AccountInputModel GetAccountCreateInputModel(AccountInputModel accountInput)
        {
            var personInput = accountInput.PersonInput;
            personInput.AllowSelect = personInput.IsPersonExist;
            if (personInput.IsPersonExist)   //現有人員
            {
                if (personInput.SelectedPersonId != personInput.PersonId)
                {
                    int id = personInput.SelectedPersonId;
                    var person = GetPersonById(id);
                    if (person == null)
                    {
                        personInput = GetPersonInputModel();
                        personInput.ContactInfoInput = GetPersonContactInfoInput();
                        accountInput.PersonInput = personInput;

                    }
                    else
                    {
                        // accountInput.PersonInput = GetPersonInputModel(person, person.ContactInfo);
                        accountInput.PersonInput = CreatePersonInput(person);
                        accountInput.PersonInput.ContactInfoInput = CreatePersonContactInfoInput(person.ContactInfo);

                    }
                }
            }

            return null;
        }
        #endregion
        //FuturesAccount CreateFuturesAccount(FuturesAccount futuresAccount)
        //{
        //    if (futuresAccount.Owner == null) return null;
        //    var result = InsertAccount(futuresAccount);
        //    if (result == null) return null;

        //    if (futuresAccount.Owner.Users == null)
        //    {
        //        Person person = futuresAccount.Owner;
        //        List<string> roles = new List<string>() { "客戶" };
        //        string passWord = "";
        //        var user = CreateUserFromPerson(person, roles, passWord);
        //    }
        //    return result as FuturesAccount;
        //}
        //public Account CreateAccount(AccountInputModel accountInput)
        //{
        //    var personInput = accountInput.PersonInput;
        //    if (personInput.PersonId <= 0)
        //    {
        //        var person = CreatePerson(personInput);
        //        if (person == null) return null;
        //        accountInput.PersonInput.PersonId = person.PersonId;
        //        accountInput.PersonId = person.PersonId;
        //    }

        //    if (accountInput is FuturesAccountInputModel && accountInput.AccountType == (int)BusinessType.Futures)
        //    {
        //        FuturesAccountInputModel futuresInput = accountInput as FuturesAccountInputModel;
        //        var futuresAccount = CreateFuturesAccount(futuresInput);
        //        if (futuresAccount == null) return null;
        //        Person owner = futuresAccount.Owner;
        //        owner = CreateCustomerFromPerson(owner);

        //        return futuresAccount;
        //    }

        //    return null;
        //}




        #endregion

        #region  memberRepository Fuctions
        Account InsertAccount(Account account)
        {
            return memberRepository.InsertAccount(account);
        }
        Account UpdateAccount(Account account)
        {
            return memberRepository.UpdateAccount(account);
        }
        public Account GetAccountById(int accountId)
        {
            return memberRepository.GetAccountById(accountId);
        }
        public IQueryable<Account> GetAllAccounts()
        {
            return memberRepository.GetAllAccounts();
        }


        #endregion

        #region Input & ViewModel
        #region Create Input/View

        BankInfoViewModel CreateBankInfoView(AccountBankInfo bankInfo)
        {
            BankInfoViewModel view = new BankInfoViewModel();
            SetAccountBankInfoInputValues(view, bankInfo);
            return view;
        }
        List<BankInfoViewModel> CreateBankInfoViewList(List<AccountBankInfo> bankInfoList)
        {
            if (bankInfoList == null || bankInfoList.Count == 0) return null;
            List<BankInfoViewModel> returnValue = new List<BankInfoViewModel>();
            foreach (var bankInfo in bankInfoList)
            {
                BankInfoViewModel view = CreateBankInfoView(bankInfo);
                returnValue.Add(view);
            }

            return returnValue;
        }
        #endregion

        #region Initialize Input Values
        void InitializeAccountInput(AccountInputModel inputModel)
        {
            var companyOptions = GetCompanyOptions();
            if (inputModel.CompanyId == 0)
            {
                inputModel.CompanyId = Convert.ToInt32(companyOptions.FirstOrDefault().Value);
            }

            var branchOptions = GetBranchOptions(inputModel.CompanyId);
            if (inputModel.BranchId == 0)
            {
                inputModel.BranchId = Convert.ToInt32(branchOptions.FirstOrDefault().Value);
            }

            inputModel.CompanyOptions = companyOptions;
            inputModel.BranchOptions = branchOptions;

            inputModel.StatusOptions = Helper.GetAccountTypeOptions();

            inputModel.AccountTypeOptions = Helper.GetAccountTypeOptions();
            inputModel.AECodeOptions = GetAECodeOptions(inputModel.CompanyId, inputModel.AccountType);

            inputModel.AEOptions = GetAEOptions(inputModel.CompanyId);
            inputModel.PersonOptions = GetPersonOptions();

        }
        void InitializeAccountContactInfoInput(AccountContactInfoInputModel input, AccountContactInfo contactInfo = null)
        {
            if (input == null) input = new AccountContactInfoInputModel();
            if (contactInfo == null)
            {
                InitializeAddressInput(input.AddressInput);
                InitializeAddressInput(input.CensusAddressInput);
            }
            else
            {
                InitializeAddressInput(input.AddressInput, contactInfo.ContactAddress);
                InitializeAddressInput(input.CensusAddressInput, contactInfo.CensusAddress);
            }

        }
        void InitializeAccountBankInfoInput(BankInfoInputModel input)
        {
            if (input == null) input = new BankInfoInputModel();
            input.CurrencyOptions = Helper.GetCurrencyOptions();
            input.TypeOptions = Helper.GetBankInfoTypeOptions();

        }
        #endregion

        #region Set Input/View Values
        void SetAccountInputValues(AccountInputModel inputModel, Account account)
        {
            var company = account.CompanyBranch;
            int companyId = 0;
            int branchId = 0;
            if (company.ParentCompanyId == 0)
            {
                companyId = company.CompanyId;
                branchId = 0;
            }
            else
            {
                companyId = company.ParentCompanyId;
                branchId = company.CompanyId;
            }

            inputModel.AccountId = account.AccountId;
            inputModel.AccountNumber = account.AccountNumber;
            inputModel.AccountType = Convert.ToInt32(account.AccountType);

            inputModel.CompanyId = companyId;
            inputModel.BranchId = branchId;

            inputModel.Status = Convert.ToInt32(account.Status);
            inputModel.OpenDate = account.OpenDate;
            inputModel.OpenAEId = account.OpenAE == null ? 0 : account.OpenAE.AEId;
            inputModel.AECodeId = account.AECodeId;
            inputModel.PersonId = account.PersonId;

            inputModel.TraderId = account.TraderId == null ? 0 : (int)account.TraderId;

            inputModel.ReferralId = account.ReferralId == null ? 0 : (int)account.ReferralId;

            inputModel.ServiceAEId = account.ServiceAEId;
            inputModel.OfficialTrader = Helper.GetIsOfficialTrader(account.GetCurrentTraderRecord());
        }
        void SetAccountViewValues(AccountViewModel viewModel, Account account)
        {
            viewModel.CompanyName = GetCompanyById(viewModel.CompanyId).Name;
            viewModel.BranchName = GetCompanyById(viewModel.BranchId).Name;
            viewModel.StatusText = Helper.GetAccountStatusText(account.Status);
            viewModel.OpenAEName = account.OpenAE == null ? "" : account.OpenAE.Person.Name;
            viewModel.AECodeText = Helper.GetAECodeText(account.AECode);
            viewModel.OpenDateText = account.OpenDate.ToString();
            viewModel.TraderName = account.Trader == null ? "" : account.Trader.Name;
            viewModel.ReferralName = account.Referral == null ? "" : account.Referral.Name;
            viewModel.ServiceAEName = account.ServiceAE.Person.Name;
        }
        void SetAccountContactInfoInputValues(AccountContactInfoInputModel inputModel, AccountContactInfo contactInfo)
        {
            inputModel.Id = contactInfo.AccountId;
            inputModel.Email = contactInfo.Email;
            inputModel.Phone = contactInfo.Phone;
            inputModel.TEL = contactInfo.TEL;

            SetAddressInputValues(inputModel.AddressInput, contactInfo.ContactAddress);
        }
        void SetAccountBankInfoInputValues(BankInfoInputModel input, AccountBankInfo bankInfo)
        {
            input.Id = bankInfo.AccountBankInfoId;
            input.AccountId = bankInfo.AccountId;
            input.BankName = bankInfo.BankName;
            input.BankBranch = bankInfo.BankBranch;
            input.Title = bankInfo.Title;
            input.AccountNumber = bankInfo.AccountNumber;
            input.SwiftCode = bankInfo.SwiftCode;
            input.CurrencyId = Convert.ToInt32(bankInfo.Currency);
            input.TypeId = Convert.ToInt32(bankInfo.Type);

        }
        void SetBankInfoViewValues(BankInfoViewModel view, AccountBankInfo bankInfo)
        {
            SetAccountBankInfoInputValues(view, bankInfo);
            view.TypeText = Helper.GetBankInfoTypeText(Convert.ToInt32(bankInfo.Type));
            view.CurrencyText = Helper.GetCurrencyText(Convert.ToInt32(bankInfo.Currency));
        }
        void SetAccountPersonInputValues(AccountInputModel accountInput)
        {
            //var personInput = accountInput.PersonInput;
            //personInput.AllowSelect = personInput.IsPersonExist;
            //if (personInput.IsPersonExist)   //現有人員
            //{
            //    if (personInput.SelectedPersonId != personInput.PersonId)
            //    {
            //        int id = personInput.SelectedPersonId;
            //        var person = GetPersonById(id);
            //        if (person == null)
            //        {
            //            personInput = GetPersonInputModel();
            //            personInput.ContactInfoInput = GetPersonContactInfoInput();
            //            accountInput.PersonInput = personInput;

            //        }
            //        else
            //        {
            //            accountInput.PersonInput = GetPersonInputModel(person, person.ContactInfo);
            //        }
            //    }
            //}
            int personId = accountInput.PersonInput.PersonId;
            var personInput = GetPersonInputModel(personId);
            personInput.ContactInfoInput = GetPersonContactInfoInput(personId);
            accountInput.PersonInput = personInput;


        }
        void ReSetAccountInputModelValues(AccountInputModel inputModel)
        {
            var branchOptions = GetBranchOptions(inputModel.CompanyId);
            if (inputModel.BranchId == 0)
            {
                inputModel.BranchId = Convert.ToInt32(branchOptions.FirstOrDefault().Value);
            }
            inputModel.BranchOptions = branchOptions;

            inputModel.AECodeOptions = GetAECodeOptions(inputModel.CompanyId, inputModel.AccountType);

            inputModel.AEOptions = GetAEOptions(inputModel.CompanyId);
        }
        void SetAccountInputModelValues(AccountInputModel inputModel, Account account)
        {
            var company = account.CompanyBranch;
            int companyId = 0;
            int branchId = 0;
            if (company.ParentCompanyId == 0)
            {
                companyId = company.CompanyId;
                branchId = 0;
            }
            else
            {
                companyId = company.ParentCompanyId;
                branchId = company.CompanyId;
            }

            inputModel.AccountId = account.AccountId;
            inputModel.AccountNumber = account.AccountNumber;
            inputModel.AccountType = Convert.ToInt32(account.AccountType);

            inputModel.CompanyId = companyId;
            inputModel.BranchId = branchId;
            //inputModel.BranchName = branchName;
            inputModel.Status = Convert.ToInt32(account.Status);
            //inputModel.StatusText = Helper.GetAccountStatusText(account.Status);

            inputModel.OpenDate = account.OpenDate;
            inputModel.OpenAEId = account.OpenAE == null ? 0 : account.OpenAE.AEId;
            // inputModel.OpenAEName = account.OpenAE == null ? "" : account.OpenAE.Person.Name;

            inputModel.AECodeId = account.AECodeId;
            //inputModel.AECodeText = Helper.GetAECodeText(account.AECode);

            inputModel.PersonId = account.PersonId;
            //inputModel.OpenDateText = account.OpenDate.ToString();
            inputModel.TraderId = account.TraderId == null ? 0 : (int)account.TraderId;
            //inputModel.TraderName = account.Trader == null ? "" : account.Trader.Name;
            inputModel.ReferralId = account.ReferralId == null ? 0 : (int)account.ReferralId;
            //inputModel.ReferralName = account.Referral == null ? "" : account.Referral.Name;
            inputModel.ServiceAEId = account.ServiceAEId;
            //inputModel.ServiceAEName = account.ServiceAE.Person.Name;

            inputModel.OfficialTrader = Helper.GetIsOfficialTrader(account.GetCurrentTraderRecord());

        }

        #endregion

        #region Set Values From Input
        void SetAccountValuesFromInput(Account account, AccountInputModel accountInput)
        {
            account.PersonId = accountInput.PersonId;
            account.AccountNumber = accountInput.AccountNumber;
            if (accountInput.BranchId == 0) account.CompanyBranchId = accountInput.CompanyId;
            else account.CompanyBranchId = accountInput.BranchId;
            account.AECodeId = accountInput.AECodeId;
            account.ServiceAEId = accountInput.ServiceAEId;
            account.OpenDate = accountInput.OpenDate;
            account.Status = (AccountStatus)accountInput.Status;

            if (account.ReferralId > 0) account.ReferralId = accountInput.ReferralId;
            if (account.OpenAEId > 0) account.OpenAEId = accountInput.OpenAEId;
        }

        void SetAccountContactInfoValuesFromInput(AccountContactInfo contactInfo, AccountContactInfoInputModel input)
        {
            contactInfo.AccountId = input.Id;
            contactInfo.Phone = input.Phone;
            contactInfo.TEL = input.TEL;
            contactInfo.Email = input.Email;

            var addressInput = input.AddressInput;

            contactInfo.ContactAddress.ZipCode = addressInput.ZipCode;
            contactInfo.ContactAddress.City = addressInput.CityId;
            contactInfo.ContactAddress.District = addressInput.DistrictId;
            contactInfo.ContactAddress.StreetAddress = addressInput.Street;

            //addressInput = input.CensusAddressInput;
            //contactInfo.CensusAddress.ZipCode = addressInput.ZipCode;
            //contactInfo.CensusAddress.City = addressInput.CityId;
            //contactInfo.CensusAddress.District = addressInput.DistrictId;
            //contactInfo.CensusAddress.StreetAddress = addressInput.Street;

        }
        void SetFuturesAccountBankValuesFromInput(FuturesAccount account, FuturesAccountInputModel futuresAccountInput)
        {
            //var bankInfoInput = futuresAccountInput.TaiwanWithdrawBank;
            //var bankInfo = CreateBankInfoFromInput(bankInfoInput);
            //if (account.AccountBankInfos == null) account.AccountBankInfos = new List<AccountBankInfo>();
            //account.AccountBankInfos.Add(bankInfo);

            ////台幣入金銀行
            //bankInfoInput = futuresAccountInput.FirstTWDepositBank;
            //if (bankInfoInput != null)
            //{
            //    bankInfo = CreateBankInfoFromInput(bankInfoInput);
            //    account.AccountBankInfos.Add(bankInfo);
            //}

            //bankInfoInput = futuresAccountInput.SecondTWDepositBank;
            //if (bankInfoInput != null)
            //{
            //    bankInfo = CreateBankInfoFromInput(bankInfoInput);
            //    account.AccountBankInfos.Add(bankInfo);
            //}

            //bankInfoInput = futuresAccountInput.ThirdTWDepositBank;
            //if (bankInfoInput != null)
            //{
            //    bankInfo = CreateBankInfoFromInput(bankInfoInput);
            //    account.AccountBankInfos.Add(bankInfo);
            //}

            ////外幣出金銀行
            //bankInfoInput = futuresAccountInput.ForexWithdrawBank;
            //if (bankInfoInput != null)
            //{
            //    bankInfo = CreateBankInfoFromInput(bankInfoInput);
            //    account.AccountBankInfos.Add(bankInfo);
            //}

            ////外幣入金銀行
            //bankInfoInput = futuresAccountInput.FirstForexDepositBank;

            //if (bankInfoInput != null)
            //{
            //    bankInfo = CreateBankInfoFromInput(bankInfoInput);
            //    account.AccountBankInfos.Add(bankInfo);
            //}

            //bankInfoInput = futuresAccountInput.SecondForexDepositBank;
            //if (bankInfoInput != null)
            //{
            //    bankInfo = CreateBankInfoFromInput(bankInfoInput);
            //    account.AccountBankInfos.Add(bankInfo);
            //}

            //bankInfoInput = futuresAccountInput.ThirdForexDepositBank;
            //if (bankInfoInput != null)
            //{
            //    bankInfo = CreateBankInfoFromInput(bankInfoInput);
            //    account.AccountBankInfos.Add(bankInfo);
            //}
        }
        void SetAccountContactInfoViewValues(AccountContactInfoViewModel view, AccountContactInfo contactInfo)
        {
            int cityId;
            int districtId;
            view.Id = contactInfo.AccountId;
            view.Email = contactInfo.Email;
            view.TEL = contactInfo.TEL;
            view.Phone = contactInfo.Phone;

            var address = contactInfo.CensusAddress;
            cityId = Convert.ToInt32(address.City);
            districtId = Convert.ToInt32(address.District);


            view.CensusAddressView.ZipCode = address.ZipCode;
            view.CensusAddressView.CityId = address.City;
            view.CensusAddressView.CityText = GetCityById(cityId).Name;
            view.CensusAddressView.DistrictId = address.District;
            view.CensusAddressView.DistrictText = GetDistrictById(districtId).Name;
            view.CensusAddressView.Street = address.StreetAddress;

            address = contactInfo.ContactAddress;
            cityId = Convert.ToInt32(contactInfo.ContactAddress.City);
            districtId = Convert.ToInt32(contactInfo.ContactAddress.District);
            view.ContactAddressView.ZipCode = address.ZipCode;
            view.ContactAddressView.CityId = address.City;
            view.ContactAddressView.CityText = GetCityById(cityId).Name;
            view.ContactAddressView.DistrictId = address.District;
            view.ContactAddressView.DistrictText = GetDistrictById(districtId).Name;
            view.ContactAddressView.Street = address.StreetAddress;

        }
        void SetFuturesAccountBankViewValues(BankInfoViewModel view, AccountBankInfo bankInfo)
        {
            //public string TypeText { get; set; }
            //public string CurrencyText { get; set; }
        }
        AccountBankInfo CreateBankInfoFromInput(BankInfoInputModel bankInfoInput)
        {
            string bankName = bankInfoInput.BankName;
            string branchName = bankInfoInput.BankBranch;
            int currencyId = bankInfoInput.CurrencyId;
            string number = bankInfoInput.AccountNumber;
            AccountBankInfo bankInfo = new AccountBankInfo(bankName, branchName, currencyId, number);
            SetCreateRecord(bankInfo);
            return bankInfo;
        }

        #endregion
        #endregion


        //FuturesAccount CreateFuturesAccount(FuturesAccount futuresAccount)
        //{
        //    if (futuresAccount.Owner == null) return null;
        //    var result = InsertAccount(futuresAccount);
        //    if (result == null) return null;

        //    if (futuresAccount.Owner.Users == null)
        //    {
        //        Person person = futuresAccount.Owner;
        //        List<string> roles = new List<string>() { "客戶" };
        //        string passWord = "";
        //        var user = CreateUserFromPerson(person, roles, passWord);
        //        if (user == null)
        //        {
        //            DeleteAccount(futuresAccount);
        //            return null;
        //        }
        //    }
        //    return result as FuturesAccount;
        //}

        //public List<StockAccount> GetStockAccountByReferral(int referralId)
        //{
        //    return memberRepository.GetStockAccountByReferral(referralId);
        //}
        //public List<StockAccount> GetStockAccountByTrader(int traderId)
        //{
        //    return memberRepository.GetStockAccountByTrader(traderId);
        //}
        //public List<StockAccount> GetStockAccountByOpenAE(int openAEId)
        //{
        //    return memberRepository.GetStockAccountByOpenAE(openAEId);
        //}
        //void SetAccountViewValues(AccountViewModel accountView, Account account)
        //{
        //    var company = account.CompanyBranch;
        //    string companyName = "";
        //    int companyId = 0;
        //    string branchName = "";
        //    int branchId = 0;
        //    if (company.ParentCompanyId == 0)
        //    {
        //        companyName = company.Name;
        //        companyId = company.CompanyId;
        //        branchId = 0;
        //        branchName = "";
        //    }
        //    else
        //    {
        //        companyId = company.ParentCompanyId;
        //        companyName = GetCompanyById(company.ParentCompanyId).Name;
        //        branchName = company.Name;
        //        branchId = company.CompanyId;
        //    }
        //    accountView.AccountId = account.AccountId;
        //    accountView.AccountNumber = account.AccountNumber;
        //    accountView.AccountType = Convert.ToInt32(account.AccountType);
        //    accountView.CompanyName = companyName;
        //    accountView.CompanyId = companyId;
        //    accountView.BranchId = branchId;
        //    accountView.BranchName = branchName;
        //    accountView.Status = Convert.ToInt32(account.Status);
        //    accountView.StatusText = Helper.GetAccountStatusText(account.Status);

        //    accountView.OpenDate = account.OpenDate;
        //    accountView.OpenAEId = account.OpenAE == null ? 0 : account.OpenAE.AEId;
        //    accountView.OpenAEName = account.OpenAE == null ? "" : account.OpenAE.Person.Name;

        //    accountView.AECodeId = account.AECodeId;
        //    accountView.AECodeText = Helper.GetAECodeText(account.AECode);

        //    accountView.PersonId = account.PersonId;
        //    accountView.OpenDateText = account.OpenDate.ToString();
        //    accountView.TraderId = account.TraderId == null ? 0 : (int)account.TraderId;
        //    accountView.TraderName = account.Trader == null ? "" : account.Trader.Name;
        //    accountView.ReferralId = account.ReferralId == null ? 0 : (int)account.ReferralId;
        //    accountView.ReferralName = account.Referral == null ? "" : account.Referral.Name;
        //    accountView.ServiceAEId = account.ServiceAEId;
        //    accountView.ServiceAEName = account.ServiceAE.Person.Name;

        //    accountView.OfficialTrader = Helper.GetIsOfficialTrader(account.GetCurrentTraderRecord());
        //}

        #endregion

        #region UserIdentity
        #region  Public Fuctions
        #region   Filter Fuctions
        public static G16ApplicationUser GetUserByLogin(UserInputModel userInput)
        {
            using (var memberRepository = new MemberRepository())
            {
                return memberRepository.GetUser(userInput.UserName, userInput.PassWord);
            }
        }
        public G16ApplicationUser GetUserByPerson(Person person)
        {
            var allUsers = GetAllUsers();
            return allUsers.Where(u => u.Person != null && u.Person.PersonId == person.PersonId).FirstOrDefault();

        }
        public List<string> GetAllRoleNames()
        {
            var roles = GetAllRoles();
            if (roles == null) return null;
            List<string> roleNames = new List<string>();
            foreach (var item in roles)
            {
                roleNames.Add(item.Name);
            }
            return roleNames;

        }
        public Identity GetPublicIdentityByName(string name)
        {
            var allIdentities = GetAllIdentities();
            return allIdentities.OfType<Identity>().Where(i => i.Name == name).FirstOrDefault();
        }
        public List<Identity> GetPublicIdentities()
        {
            var allIdentities = GetAllIdentities();
            return allIdentities.OfType<Identity>().ToList();
        }
        public List<CustomIdentity> GetCustomIdentities(int createPersonId)
        {
            var allIdentities = GetAllIdentities();
            return allIdentities.OfType<CustomIdentity>().ToList();
        }

        #endregion

        #region  Helper Fuctions
        public bool IsPersonHasIdentity(Person person, int identityId)
        {
            if (person.Identities == null) return false;
            if (person.Identities.Count == 0) return false;
            var identity = person.Identities.Where(i => i.IdentityId == identityId).FirstOrDefault();
            if (identity == null) return false;

            return true;

        }
        #endregion
        #region  Create , Update , Remove
        public G16ApplicationUser CreateUserFromCustomer(Person person, string passWord)
        {
            return CreateUserFromPerson(person, new List<string>() { "客戶" }, passWord);
        }
        G16ApplicationUser CreateUserFromPerson(Person person, List<string> roles, string passWord)
        {
            if (Helper.IsApplicationUser(person)) return null;

            var user = new G16ApplicationUser
            {
                UserName = person.ContactInfo.Email,
                Email = person.ContactInfo.Email,
                PhoneNumber = person.ContactInfo.Phone,
                Person = person
            };
            if (passWord == "")
            {
                passWord = person.ContactInfo.Phone;
            }
            user = CreateUser(user, passWord);
            if (user == null) return null;

            bool isSuccess = true;
            for (int i = 0; i < roles.Count; i++)
            {
                user = AddUserToRole(user, roles[i]);
                if (user == null)
                {
                    isSuccess = false;
                    break;
                }
            }

            return isSuccess ? user : null;

        }
        public Identity CreatePublicIdentity(string name)
        {
            var duplicateEntry = GetPublicIdentityByName(name);
            if (duplicateEntry != null) return null;
            var identity = new Identity
            {
                Name = name,
            };
            SetCreateRecord(identity);
            return memberRepository.InsertIdentity(identity);

        }
        public Person AddPersonToIdentity(int personId, int identityId)
        {
            var person = GetPersonById(personId);
            if (person == null) return null;
            var identity = GetIdentityById(identityId);
            if (identity == null) return null;

            return AddPersonToIdentity(person, identity);

        }
        Person AddPersonToIdentity(Person person, Identity identity)
        {
            if (person.Identities == null) person.Identities = new List<Identity>();
            if (person.Identities.Contains(identity)) return person;

            person.Identities.Add(identity);
            return UpdatePerson(person);
        }
        #endregion


        #endregion

        #region  memberRepository Fuctions
        #region UserManager Fuctions
        G16ApplicationUser CreateUser(G16ApplicationUser user, string passWord)
        {
            return memberRepository.InsertUser(user, passWord);
        }
        G16ApplicationUser UpdateUser(G16ApplicationUser user)
        {
            return memberRepository.UpdateUser(user);
        }
        G16ApplicationUser GetUser(string userName, string passWord)
        {
            return memberRepository.GetUser(userName, passWord);
        }
        G16ApplicationUser GetUserById(string userId)
        {
            return memberRepository.GetUserById(userId);
        }
        public IQueryable<G16ApplicationUser> GetAllUsers()
        {
            return memberRepository.GetAllUsers();
        }
        public G16ApplicationUser FindUserByUserName(string userName)
        {
            return memberRepository.FindUserByUserName(userName);
        }
        public bool IsUserInRole(string userId, string roleName)
        {
            return memberRepository.IsUserInRole(userId, roleName);
        }
        public G16ApplicationUser AddUserToRole(G16ApplicationUser user, string roleName)
        {
            bool hasTheRole = IsUserInRole(user.Id, roleName);
            if (hasTheRole) return user;
            return memberRepository.AddUserToRole(user, roleName);
        }
        public G16ApplicationUser RemoveUserFromRole(G16ApplicationUser user, string roleName)
        {
            bool hasTheRole = IsUserInRole(user.Id, roleName);
            if (!hasTheRole) return user;
            return memberRepository.RemoveUserFromRole(user, roleName);
        }
        public void SendMailToUser(string userId, string subject, string body)
        {
            memberRepository.SendMailToUser(userId, subject, body);
        }
        public string GenerateEmailConfirmationToken(string userId)
        {
            return memberRepository.GenerateEmailConfirmationToken(userId);
        }
        public bool ConfirmEmail(string userId, string tokenCode)
        {
            return memberRepository.ConfirmEmail(userId, tokenCode);
        }
        #endregion
        #region RoleManager Fuctions
        public IdentityRole CreateRole(string roleName)
        {
            return memberRepository.InsertRole(roleName);
        }
        public IdentityRole UpdateRole(IdentityRole role)
        {
            return memberRepository.UpdateRole(role);
        }
        public IQueryable<IdentityRole> GetAllRoles()
        {
            return memberRepository.GetAllRoles();
        }
        public IdentityRole GetRoleByName(string roleName)
        {
            return memberRepository.GetRollByName(roleName);
        }
        public bool IsRoleExsits(string roleName)
        {
            return memberRepository.IsRoleExsits(roleName);

        }

        #endregion
        #region Identity Fuctions
        public Identity CreateIdentity(Identity identity)
        {
            return memberRepository.InsertIdentity(identity);
        }
        public Identity UpdateIdentity(Identity identity)
        {
            return memberRepository.UpdateIdentity(identity);
        }
        public Identity GetIdentityById(int id)
        {
            return memberRepository.GetIdentityById(id);
        }
        public IQueryable<Identity> GetAllIdentities()
        {
            return memberRepository.GetAllIdentities();
        }
        //public CustomIdentity CreateCustomIdentity(string name, int createPersonId)
        //{
        //    var duplicateEntry = memberRepository.GetCustomIdentityByName(name, createPersonId);
        //    if (duplicateEntry != null) return null;
        //    var publicDuplicateEntry = memberRepository.GetPublicIdentityByName(name);
        //    if (publicDuplicateEntry != null) return null;

        //    var identity = new CustomIdentity
        //    {
        //        Name = name,
        //        CreatePersonId = createPersonId
        //    };
        //    SetCreateRecord(identity);
        //    var result = memberRepository.InsertIdentity(identity);
        //    if (result == null) return null;
        //    return result as CustomIdentity;
        //}
        #endregion
        #endregion


        #endregion

        #region AE
        #region  Public Fuctions

        #region   Filter Fuctions
        public IQueryable<AE> GetAEByKeyWord(int searchWay = 0, string keyWord = "")
        {
            // string[] status = new string[] { "全部資料", "合約狀態：有效", "合約狀態：無效"};
            var allAE = GetAllAEs();
            switch (searchWay)
            {
                case 0:  //全部資料
                    return allAE.Where(ae => ae.Person.Name.Contains(keyWord)).AsQueryable();

                case 1:  //合約狀態：有效
                    return allAE.Where(ae => ae.IsActive && ae.Person.Name.Contains(keyWord)).AsQueryable();

                case 2:  //合約狀態：無效
                    return allAE.Where(ae => !ae.IsActive && ae.Person.Name.Contains(keyWord)).AsQueryable();

                default:
                    return allAE.Where(ae => ae.Person.Name.Contains(keyWord)).AsQueryable();
            }
        }
        public List<AE> GetAEsByCompany(int companyId)
        {
            var allAEs = GetAllAEs();
            return allAEs.Where(a => a.CompanyId == companyId).ToList();
        }

        #endregion

        #region  Create , Update , Remove
        public AE CreateAE(AECreateInputModel input)
        {
            var ae = new AE()
            {
                IsActive = true,
                BeginDate = input.BeginDate,
                EndDate = input.EndDate
            };
            PersonSelectCreateInput personSelectCreateInput = input.PersonSelectCreateInput;
            if (personSelectCreateInput.PersonExsit)
            {
                int exsitPersonId = personSelectCreateInput.ExsitPersonId;
                var exsitPerson = GetPersonById(exsitPersonId);
                if (exsitPerson == null) return null;
                ae.Person = exsitPerson;
            }
            else
            {
                var personInput = personSelectCreateInput.NewPersonInput;
                var newPerson = new Person();
                SetPersonValuesFromInput(newPerson, personInput);
                SetCreateRecord(newPerson);

                var contactInfo = new PersonContactInfo();
                SetContactInfoValuesFromInput(contactInfo, personInput.ContactInfoInput);
                SetCreateRecord(contactInfo);

                newPerson.ContactInfo = contactInfo;
                ae.Person = newPerson;
            }

            //建立AEContract
            var aeContract = new AEContract()
            {
                BeginDate = ae.BeginDate,
                EndDate = ae.EndDate,
                IsActive = true,
            };
            SetCreateRecord(aeContract);
            ae.AEContractRecords = new List<AEContract>();
            ae.AEContractRecords.Add(aeContract);


            ae = CreateAE(ae);
            if (ae == null) return null;


            Person person = ae.Person;
            if (Helper.IsApplicationUser(person))
            {
                var user = person.Users.FirstOrDefault();
                if (user != null) AddUserToRole(user, "業務員");

            }
            else
            {
                //建立AE使用者帳號
                var user = CreateUserFromPerson(person, new List<string> { "業務員" }, "");
                if (user == null)
                {
                    //Do something...
                }
            }

            return ae;
        }
        //public AE CreateAE(AEInputModel input)
        //{
        //    var ae = new AE()
        //    {
        //        IsActive = true,
        //        BeginDate = input.BeginDate,
        //        EndDate = input.EndDate
        //    };
        //    var personInput = input.PersonInput;
        //    if (personInput.PersonId > 0)
        //    {
        //        var exsitPerson = GetPersonById(personInput.PersonId);
        //        if (exsitPerson == null) return null;
        //        ae.Person = exsitPerson;
        //    }
        //    else
        //    {
        //        var newPerson = new Person();
        //        SetPersonValuesFromInput(newPerson, personInput);
        //        SetCreateRecord(newPerson);

        //        var contactInfo = new PersonContactInfo();
        //        SetContactInfoValuesFromInput(contactInfo, personInput.ContactInfoInput);
        //        SetCreateRecord(contactInfo);

        //        newPerson.ContactInfo = contactInfo;
        //        ae.Person = newPerson;
        //    }

        //    //建立AEContract
        //    var aeContract = new AEContract()
        //    {
        //        BeginDate = ae.BeginDate,
        //        EndDate = ae.EndDate,
        //        IsActive = true,
        //    };
        //    SetCreateRecord(aeContract);
        //    ae.AEContractRecords = new List<AEContract>();
        //    ae.AEContractRecords.Add(aeContract);


        //    ae = CreateAE(ae);
        //    if (ae == null) return null;


        //    Person person = ae.Person;
        //    if (Helper.IsApplicationUser(person))
        //    {
        //        var user = person.Users.FirstOrDefault();
        //        if (user != null) AddUserToRole(user, "業務員");

        //    }
        //    else
        //    {
        //        //建立AE使用者帳號
        //        var user = CreateUserFromPerson(person, new List<string> { "業務員" }, "");
        //        if (user == null)
        //        {
        //            //Do something...
        //        }
        //    }

        //    return ae;

        //}
        public AE CreateAEContractRecord(AEContractInputModel input)
        {
            int AEId = input.AEId;
            var ae = GetAEById(AEId);
            if (ae == null) return null;
            if (ae.AEContractRecords == null)
            {
                ae.AEContractRecords = new List<AEContract>();
            }

            AEContract contract = new AEContract();
            SetAEContrctValuesFromInput(input, contract);
            contract.IsActive = true;
            SetCreateRecord(contract);

            ae.AEContractRecords.Add(contract);


            ae = UpdateAE(ae);

            if (ae == null) return null;

            UpdateAEContractStatus(ae);


            return ae;

        }
        public AE DesignateCompanyToAE(AECompanyInput input)
        {
            var ae = GetAEById(input.AEId);
            if (ae == null) return null;

            var companyAE = new CompanyAE();
            SetAECompanyValuesFromInput(input, companyAE);
            companyAE.IsActive = true;

            SetCreateRecord(companyAE);

            ae.CompanyRecords.Add(companyAE);
            ae.CompanyId = companyAE.CompanyId;
            SetEditRecord(ae);

            ae = UpdateAE(ae);
            if (ae == null) return null;
            return ae;
        }
        void UpdateAEContractStatus(AE ae)
        {
            var contractRecords = ae.AEContractRecords;
            if (contractRecords == null)
            {
                ae.IsActive = false;
                UpdateAE(ae);
                return;
            }

            var latestRecord = contractRecords.Where(r => !r.Removed && r.IsActive).FirstOrDefault();
            if (latestRecord == null)
            {
                ae.IsActive = false;
                UpdateAE(ae);
                return;
            }


            ae.IsActive = true;
            ae.BeginDate = latestRecord.BeginDate;
            ae.EndDate = latestRecord.EndDate;
            UpdateAE(ae);
            return;


        }

        void UpdateAECurrentCompany(AE ae)
        {
            var aeCompanyRecords = ae.CompanyRecords;
            if (aeCompanyRecords == null)
            {
                ae.Company = null;
                UpdateAE(ae);
                return;
            }
            var latestRecord = aeCompanyRecords.Where(r => !r.Removed && r.IsActive).FirstOrDefault();

            if (latestRecord == null)
            {
                ae.Company = null;
                UpdateAE(ae);
                return;
            }
            if (ae.Company == null)
            {
                ae.Company = latestRecord.Company;
                UpdateAE(ae);
                return;
            }
            if (ae.CompanyId != latestRecord.CompanyId)
            {
                ae.CompanyId = latestRecord.CompanyId;
                UpdateAE(ae);
                return;
            }

        }

        public AE UpdateAEContract(AEContractInputModel input)
        {
            int AEId = input.AEId;
            int contractId = input.Id;
            var ae = GetAEById(AEId);
            if (ae == null) return null;
            if (ae.AEContractRecords == null) return null;
            var contractRecords = ae.AEContractRecords;
            var contract = contractRecords.Where(c => c.Id == contractId).FirstOrDefault();
            if (contract == null) return null;

            SetAEContrctValuesFromInput(input, contract);

            ae = UpdateAE(ae);

            if (ae == null) return null;

            UpdateAEContractStatus(ae);


            return ae;

        }
        public bool RemoveAE(int AEId)
        {
            var ae = GetAEById(AEId);
            if (ae == null)
            {
                //do something
                return false;
            }
            ae.Removed = true;
            SetEditRecord(ae);
            ae = UpdateAE(ae);
            if (ae == null)
            {
                //do something
                return false;
            }

            return true;

        }
        public bool RemoveAEContractRecord(int aeId, int id)
        {
            var ae = GetAEById(aeId);
            if (ae == null) return false;
            if (ae.AEContractRecords == null) return false;

            var contract = ae.AEContractRecords.Where(c => c.Id == id).FirstOrDefault();
            if (contract == null) return false;

            contract.Removed = true;
            SetEditRecord(contract);

            ae = UpdateAE(ae);

            if (ae == null) return false;

            UpdateAEContractStatus(ae);


            return true;
        }

        #endregion

        #region  GetInput/View Models
        public AECreateInputModel GetAECreateInputModel()
        {

            var input = new AECreateInputModel()
            {
                PersonSelectCreateInput = new PersonSelectCreateInput()
            };
            input.PersonSelectCreateInput.NewPersonInput = GetPersonCreateInputModel();

            return input;
        }
        public AEViewModel GetAEViewModel(int id)
        {
            AE ae = GetAEById(id);
            if (ae == null) return null;

            AEViewModel view = new AEViewModel();
            SetAEViewValues(view, ae);

            return view;

        }
        public AEViewModel GetAEViewModel(int AEId, int personId)
        {
            AEViewModel view = GetAEViewModel(AEId);

            view.PersonView = GetPersonViewModel(personId, personId);

            return view;

        }
        public List<AEContractInputModel> GetAEContractRecordsView(int AEId)
        {
            var ae = GetAEById(AEId);
            if (ae == null) return null;
            if (ae.AEContractRecords == null) return null;

            var contractRecords = ae.AEContractRecords.Where(c => !c.Removed);
            if (contractRecords == null) return null;

            var listAEContractInput = new List<AEContractInputModel>();
            foreach (var contract in contractRecords)
            {
                var input = new AEContractInputModel();
                SetAEContractInputValues(input, contract);
                listAEContractInput.Add(input);
            }
            return listAEContractInput;

        }
        public AEContractInputModel GetAEContractInput(int AEId, int contractId)
        {

            AEContract contract = GetAEContractById(AEId, contractId);

            if (contract == null) return null;
            var input = new AEContractInputModel();
            SetAEContractInputValues(input, contract);

            return input;
        }

        public SearchInputModel GetAESearchInputModel()
        {
            var input = new SearchInputModel();
            input.SearchOptions = Helper.GetAESearchWayOptions();
            return input;
        }

        AEContract GetAEContractById(int AEId, int contractId)
        {
            var ae = GetAEById(AEId);
            if (ae == null) return null;
            if (ae.AEContractRecords == null) return null;
            var contractRecords = ae.AEContractRecords;
            var contract = contractRecords.Where(c => c.Id == contractId).FirstOrDefault();
            if (contract == null) return null;
            return contract;
        }

        #endregion

        #endregion

        #region Input & ViewModel
        #region Initialize Input Values

        #endregion
        #region Set Input/View Values
        void SetAEViewValues(AEViewModel view, AE ae)
        {
            if (view == null) view = new AEViewModel();
            view.AEId = ae.AEId;
            view.AEName = ae.Person.Name;
            view.BeginDate = ae.BeginDate;
            view.EndDate = ae.EndDate;
            view.CompanyId = ae.CompanyId;
            view.IsActive = ae.IsActive;

            if (ae.IsActive)
            {
                view.IsActiveText = "有效";
            }
            else
            {
                view.IsActiveText = "已無效";
            }

            if (view.CompanyId != null)
            {
                int companyId = Convert.ToInt32(view.CompanyId);
                view.CurrentCompanyText = GetCompanyFullName(companyId);

            }
        }
        void SetAEContractInputValues(AEContractInputModel input, AEContract contract)
        {
            input.AEId = contract.AEId;
            input.Id = contract.Id;
            input.IsActive = contract.IsActive;
            input.BeginDate = contract.BeginDate;
            input.EndDate = contract.EndDate;
            input.AENameText = GetPersonName(contract.AEId);
            input.StatusText = Helper.GetContractStatusText(contract.IsActive);
        }
        #endregion

        #region Set Values From Input
        void SetAEContrctValuesFromInput(AEContractInputModel input, AEContract contract)
        {
            contract.AEId = input.AEId;
            contract.IsActive = input.IsActive;
            contract.BeginDate = input.BeginDate;
            contract.EndDate = input.EndDate;
        }

        #endregion
        #endregion
        #region  memberRepository Fuctions
        AE CreateAE(AE AE)
        {
            SetCreateRecord(AE);
            return memberRepository.InsertAE(AE);
        }
        AE UpdateAE(AE AE)
        {
            SetEditRecord(AE);
            return memberRepository.UpdateAE(AE);
        }
        public AE GetAEById(int AEId)
        {
            return memberRepository.GetAEById(AEId);
        }

        public IQueryable<AE> GetAllAEs()
        {
            return memberRepository.GetAllAEs();
        }

        #endregion


        #endregion

        #region AECode
        #region Public Functions
        #region   Filter Fuctions
        public List<AECode> GetAECodesBySearch(int companyId, int searchWay = 0, int busiuessType = 0)
        {
            // string[] status = new string[] { "全部資料", "狀態：有效", "狀態：無效"};
            Company company = GetCompanyById(companyId);
            if (company == null) return null;
            if (company.AECodes == null) return null;
            var companyAECodes = company.AECodes.Where(c => !c.Removed);
            if (companyAECodes == null) return null;

            List<AECode> listAECode = new List<AECode>();
            IQueryable<AECode> queryableRecords;


            BusinessType type = (BusinessType)busiuessType;

            switch (searchWay)
            {
                case 0:  //全部資料
                    queryableRecords = companyAECodes.AsQueryable();
                    break;

                case 1:  //狀態：有效
                    queryableRecords = companyAECodes.Where(c => c.IsActive).AsQueryable();
                    break;
                case 2:  //狀態：無效
                    queryableRecords = companyAECodes.Where(c => !c.IsActive).AsQueryable();
                    break;
                default:
                    queryableRecords = companyAECodes.AsQueryable();
                    break;
            }

            listAECode = queryableRecords.Where(c => c.BusinessType == type).ToList();

            return listAECode;
        }


        #endregion

        #region  Create , Update , Read, Remove
        public AECode UpdateAECode(AECodeInputModel input)
        {
            int id = input.AECodeId;
            AECode entry = GetAECodeById(id);
            if (entry == null) return null;

            SetAECodeValuesFromInput(entry, input);
            SetEditRecord(entry);

            return UpdateAECode(entry);

        }
        public AECode UpdateAECodeAE(AECodeAEInputModel input)
        {
            int aeCodeId = input.AECodeId;
            int id = input.Id;
            AECode aeCode = GetAECodeById(aeCodeId);
            if (aeCode == null) return null;

            if (aeCode.AERecords == null) return null;

            AECodeAE entry = aeCode.AERecords.Where(r => r.Id == id).FirstOrDefault();
            if (entry == null) return null;


            SetAECodeAEValuesFromInput(entry, input);
            SetEditRecord(entry);

            return UpdateAECode(aeCode);

        }
        public List<AECodeAEViewModel> ReadAECodeAERecords(int aeCodeId)
        {
            AECode entry = GetAECodeById(aeCodeId);
            if (entry == null) return null;

            if (entry.AERecords == null) return null;
            var records = entry.AERecords.Where(r => !r.Removed).OrderByDescending(r => r.BeginDate);
            if (records == null) return null;

            List<AECodeAEViewModel> listAECodeAEViewModel = new List<AECodeAEViewModel>();
            foreach (var aeCodeAE in records)
            {
                AECodeAEViewModel view = new AECodeAEViewModel();
                SetAECodeAEInputValues(view, aeCodeAE);
                SetAECodeAEViewValues(view, aeCodeAE);

                listAECodeAEViewModel.Add(view);
            }

            return listAECodeAEViewModel;
        }

        public bool RemoveAECode(int aeCodeId)
        {
            AECode aeCode = GetAECodeById(aeCodeId);
            if (aeCode == null) return false;

            aeCode.Removed = true;

            aeCode = UpdateAECode(aeCode);
            if (aeCode == null) return false;
            return true;
        }
        public bool RemoveAECodeAE(int aeCodeId, int id)
        {
            AECode aeCode = GetAECodeById(aeCodeId);
            if (aeCode == null) return false;

            if (aeCode.AERecords == null) return false;

            AECodeAE entry = aeCode.AERecords.Where(r => r.Id == id).FirstOrDefault();
            if (entry == null) return false;

            entry.Removed = true;
            SetEditRecord(entry);

            aeCode = UpdateAECode(aeCode);
            if (aeCode == null) return false;
            return true;
        }
        #endregion


        #region Get Input & ViewModel
        public AECodeViewModel GetAECodeViewModel(AECode aeCode)
        {
            AECodeViewModel view = new AECodeViewModel();
            SetAECodeInputValues(view, aeCode);
            InitializeAECodeInput(view);
            SetAECodeViewValues(view, aeCode);
            return view;
        }
        public AECodeViewModel GetAECodeViewModel(int aeCodeId)
        {
            var aeCode = GetAECodeById(aeCodeId);
            if (aeCode == null) return null;

            return GetAECodeViewModel(aeCode);
        }
        public AECodeAEInputModel GetAECodeAEInputModel(int aeCodeId)
        {
            var aeCode = GetAECodeById(aeCodeId);
            if (aeCode == null) return null;

            return GetAECodeAEInputModel(aeCode);
        }
        public AECodeAEInputModel GetAECodeAEInputModel(AECode aeCode)
        {
            int companyId = aeCode.Company.CompanyId;
            var aeOptions = GetAEOptions(companyId);
            int aeId = 0;
            if (aeOptions != null) aeId = Convert.ToInt32(aeOptions.FirstOrDefault().Value);

            var input = new AECodeAEInputModel()
            {
                AECodeId = aeCode.AECodeId,
                AEId = aeId,
                BeginDate = System.DateTime.Now,
                AEOptions = aeOptions,
            };
            return input;
        }
        public AECodeAEInputModel GetAECodeAEInputModel(int aeCodeId, int id)
        {
            AECodeAE entry = GetAECodeAE(aeCodeId, id);
            if (entry == null) return null;

            AECodeAEInputModel input = new AECodeAEInputModel();
            SetAECodeAEInputValues(input, entry);
            InitializeAECodeAEInput(input);

            return input;
        }
        public AECodeAEViewModel GetAECodeAEViewModel(int aeCodeId, int id)
        {
            AECodeAE entry = GetAECodeAE(aeCodeId, id);
            if (entry == null) return null;

            return GetAECodeAEViewModel(entry);
        }
        AECodeAEViewModel GetAECodeAEViewModel(AECodeAE aeCodeAE)
        {
            AECodeAEViewModel view = new AECodeAEViewModel();
            SetAECodeAEInputValues(view, aeCodeAE);
            InitializeAECodeAEInput(view);
            SetAECodeAEViewValues(view, aeCodeAE);

            return view;
        }

        AECodeAE GetAECodeAE(int aeCodeId, int id)
        {
            var aeCode = GetAECodeById(aeCodeId);
            if (aeCode == null) return null;
            if (aeCode.AERecords == null) return null;

            AECodeAE entry = aeCode.AERecords.Where(r => r.Id == id).FirstOrDefault();
            if (entry == null) return null;
            return entry;
        }
        public AECodeViewModel GetAECodeCreateInputModel(int companyId)
        {
            if (companyId <= 0) return null;
            Company company = GetCompanyById(companyId);
            if (company == null) return null;

            var inputModel = new AECodeViewModel
            {
                IsActive = true,
                BusinessTypeOptions = Helper.GetBusinessTypeOptions(),
                AEOptions = GetAEOptions(companyId)
            };

            AECode aeCode = new AECode()
            {
                Company = company,
                CompanyId = companyId,
                IsActive = true
            };
            SetAECodeInputValues(inputModel, aeCode);
            SetAECodeViewValues(inputModel, aeCode);

            return inputModel;

        }
        public CompanyAECodesSearchInput GetAECodeSearchInputModel()
        {
            var searchInput = new CompanyAECodesSearchInput();
            searchInput.SearchOptions = new List<BaseOption>();
            searchInput.SearchOptions.Add(new BaseOption() { Text = "全部資料", Value = "0" });
            searchInput.SearchOptions.Add(new BaseOption() { Text = "狀態：有效", Value = "1" });
            searchInput.SearchOptions.Add(new BaseOption() { Text = "狀態：停用", Value = "2" });

            searchInput.BusinessTypeOptions = Helper.GetBusinessTypeOptions();

            return searchInput;
        }
        public DesignateAECodeToAEInput GetDesignateAECodeToAEInput(int aeCodeId)
        {
            var input = new DesignateAECodeToAEInput();
            AECode aeCode = GetAECodeById(aeCodeId);
            if (aeCode == null) return null;
            AECodeViewModel aeCodeViewModel = GetAECodeViewModel(aeCode);
            input.AECodeView = aeCodeViewModel;

            AECodeAE currentUserRecord = GetAECodeCurrentUserRecord(aeCode);
            if (currentUserRecord == null) input.CurrentUserInput = null;
            else
            {
                input.CurrentUserInput = GetAECodeAEViewModel(currentUserRecord);
            }

            input.NewUserInput = GetAECodeAEInputModel(aeCode);

            return input;
        }
        #endregion
        //public AECode CreateAECode(AECodeInputModel inputModel)
        //{
        //    var aeCode = new AECode
        //    {
        //        IsActive=true
        //    };
        //    SetAECodeValuesFromInput(aeCode, inputModel);

        //    aeCode = InsertAECode(aeCode);

        //    if (aeCode == null) return null;
        //    return aeCode;
        //}


        public AECode DesignateAECodeToAE(AECodeAEInputModel input)
        {
            var aeCode = GetAECodeById(input.AECodeId);
            if (aeCode == null) return null;

            var record = new AECodeAE()
            {
                AEId = input.AEId,
                BeginDate = input.BeginDate,
                IsActive = true,
            };

            SetCreateRecord(record);

            if (aeCode.AERecords == null) aeCode.AERecords = new List<AECodeAE>();
            aeCode.AERecords.Add(record);

            SetEditRecord(aeCode);
            aeCode = UpdateAECode(aeCode);

            return aeCode ?? null;

        }
        public AECode DesignateAECodeToAE(DesignateAECodeToAEInput input)
        {
            int aeCodeId = input.AECodeView.AECodeId;
            var aeCode = GetAECodeById(aeCodeId);
            if (aeCode == null) return null;

            //修改原使用者紀錄
            if (aeCode.AERecords != null && input.CurrentUserInput != null)
            {
                int aeCodeAEId = input.CurrentUserInput.Id;
                AECodeAE currentUserRecord = aeCode.AERecords.Where(r => r.Id == aeCodeAEId).FirstOrDefault();
                if (currentUserRecord != null)
                {
                    currentUserRecord.BeginDate = input.CurrentUserInput.BeginDate;
                    currentUserRecord.EndDate = input.CurrentUserInput.EndDate;

                    currentUserRecord.IsActive = false;
                    SetEditRecord(currentUserRecord);
                }
            }

            //新增使用者紀錄

            var newUserRecord = new AECodeAE()
            {
                AEId = input.NewUserInput.AEId,
                BeginDate = input.NewUserInput.BeginDate,
                IsActive = true,
            };

            SetCreateRecord(newUserRecord);

            if (aeCode.AERecords == null) aeCode.AERecords = new List<AECodeAE>();
            aeCode.AERecords.Add(newUserRecord);

            SetEditRecord(aeCode);
            aeCode = UpdateAECode(aeCode);

            return aeCode ?? null;

        }
        #endregion

        #region  memberRepository Fuctions
        AECode InsertAECode(AECode code)
        {
            SetCreateRecord(code);
            return memberRepository.InsertAECode(code);
        }
        AECode UpdateAECode(AECode code)
        {
            return memberRepository.UpdateAECode(code);
        }
        public AECode GetAECodeById(int codeId)
        {
            return memberRepository.GetAECodeById(codeId);
        }
        public IQueryable<AECode> GetAllAECodes()
        {
            return memberRepository.GetAllAECodes();
        }
        public List<AECode> GetAECodesByCompany(int companyId)
        {
            var allCodes = GetAllAECodes();
            return allCodes.Where(a => a.CompanyId == companyId).ToList();
        }

        public List<AECode> GetAECodesByCompanyByType(int companyId, BusinessType type)
        {
            var allCodes = GetAllAECodes();

            var codes = allCodes.Where(c => c.CompanyId == companyId && c.BusinessType == type).ToList();

            return codes;
        }
        #endregion

        #region Input & ViewModel
        #region Initialize Input Values
        void InitializeAECodeInput(AECodeInputModel inputModel)
        {
            inputModel.BusinessTypeOptions = Helper.GetBusinessTypeOptions();
            if (inputModel.CompanyId > 0)
            {
                inputModel.AEOptions = GetAEOptions(inputModel.CompanyId);
            }

        }
        void InitializeAECodeAEInput(AECodeAEInputModel inputModel)
        {
            var aeCode = GetAECodeById(inputModel.AECodeId);
            if (aeCode == null) return;
            var company = aeCode.Company;
            List<CompanyAE> companyAEList = GetCompanyAEByKeyWord(company.CompanyId);
            if (companyAEList == null) return;
            var AEs = (from record in companyAEList select record.AE).Distinct();
            inputModel.AEOptions = new List<BaseOption>();
            foreach (var ae in AEs)
            {
                BaseOption option = new BaseOption
                {
                    Value = ae.AEId.ToString(),
                    Text = ae.Person.Name
                };
                inputModel.AEOptions.Add(option);
            }

        }
        #endregion

        #region  Set Values From Input
        void SetAECodeValuesFromInput(AECode aeCode, AECodeInputModel input)
        {
            if (input.BranchId == 0) aeCode.CompanyId = input.CompanyId;
            else aeCode.CompanyId = input.BranchId;
            aeCode.IsActive = input.IsActive;
            aeCode.BusinessType = (BusinessType)input.BusinessTypeId;
            aeCode.Code = input.Code;
        }
        void SetAECodeAEValuesFromInput(AECodeAE entry, AECodeAEInputModel input)
        {
            entry.AECodeId = input.AECodeId;
            entry.AEId = input.AEId;
            entry.IsActive = input.IsActive;
            entry.BeginDate = input.BeginDate;
            entry.EndDate = input.EndDate;

        }
        #endregion

        #region  Set Input/View Values
        void SetAECodeInputValues(AECodeInputModel input, AECode aeCode)
        {
            input.AECodeId = aeCode.AECodeId;
            var company = aeCode.Company;
            int companyId = 0;
            int branchId = 0;
            if (company.ParentCompanyId == 0)
            {
                companyId = company.CompanyId;
                branchId = 0;
            }
            else
            {
                companyId = company.ParentCompanyId;
                branchId = company.CompanyId;
            }

            input.CompanyId = companyId;
            input.BranchId = branchId;
            input.Code = aeCode.Code;
            input.BusinessTypeId = (int)aeCode.BusinessType;
            input.IsActive = aeCode.IsActive;

            var currentAE = GetAECodeCurrentUser(aeCode);
            if (currentAE == null) return;

            input.AEId = currentAE.AEId;

        }
        void SetAECodeViewValues(AECodeViewModel view, AECode aeCode)
        {
            view.CompanyName = GetCompanyName(view.CompanyId);
            view.BranchName = GetCompanyName(view.BranchId);
            view.TypeName = Helper.GetBusinessTypeText(view.BusinessTypeId);
            if (view.IsActive) view.StatusName = "有效";
            else view.StatusName = "停用";


            if (view.AEId <= 0) return;
            view.AEName = GetPersonName(view.AEId);

        }

        void SetAECodeAEInputValues(AECodeAEInputModel input, AECodeAE aeCode)
        {
            input.Id = aeCode.Id;
            input.AECodeId = aeCode.AECodeId;
            input.AEId = aeCode.AEId;
            input.IsActive = aeCode.IsActive;
            input.BeginDate = aeCode.BeginDate;
            input.EndDate = aeCode.EndDate;

        }
        void SetAECodeAEViewValues(AECodeAEViewModel view, AECodeAE aeCode)
        {
            view.AEName = GetPersonName(aeCode.AEId);
            if (view.IsActive) view.StatusText = "使用中";
            else view.StatusText = "已停用";

        }
        #endregion


        #endregion

        #region  Helper Fuctions

        AE GetAECodeCurrentUser(AECode aeCode)
        {

            var activeRecord = GetAECodeCurrentUserRecord(aeCode);
            if (activeRecord == null) return null;
            return activeRecord.AE;
        }
        AECodeAE GetAECodeCurrentUserRecord(AECode aeCode)
        {
            if (aeCode.AERecords == null) return null;
            var activeRecord = aeCode.AERecords.Where(r => r.IsActive && !r.Removed).FirstOrDefault();
            if (activeRecord == null) return null;
            return activeRecord;
        }
        #endregion
        #endregion

        #region Company

        #region Public Functions

        #region   Filter Fuctions

        public List<Company> GetCompaniesByType(BusinessType type)
        {
            return GetAllCompanies().Where(c => c.CompanyType == type).ToList();
        }
        public List<Company> GetBranchsByCompany(int companyId)
        {
            return GetAllCompanies().
                            Where(b => b.ParentCompanyId == companyId).ToList();
        }
        public List<Company> GetParentCompanies()
        {
            return GetAllCompanies().Where(c => c.ParentCompanyId == 0).ToList();

        }
        public Company GetCompanyByName(string companyName)
        {
            return GetAllCompanies().Where(c => c.Name == companyName).FirstOrDefault();
        }
        public List<CompanyViewModel> GetCompanyBySearch(int searchOption, int parentCompany = -1)
        {
            var allCompany = GetAllCompanies();
            List<Company> listCompany = new List<Company>();
            IQueryable<Company> queryableRecords;
            switch (searchOption)
            {
                case 0:  //全部
                    queryableRecords = allCompany;
                    break;
                case 1://合約有效
                    queryableRecords = allCompany.Where(c => c.IsActive);
                    break;
                case 2://合約無效
                    queryableRecords = allCompany.Where(c => !c.IsActive);
                    break;
                default:
                    queryableRecords = allCompany;
                    break;
            }
            if (parentCompany == -1)
            {
                listCompany = queryableRecords.ToList();
            }
            else
            {
                listCompany = queryableRecords.Where(c => c.ParentCompanyId == parentCompany).ToList();
            }


            if (listCompany == null || listCompany.Count == 0)
                return null;

            List<CompanyViewModel> listCompanyViewModel = new List<CompanyViewModel>();
            foreach (var compamy in listCompany)
            {
                CompanyViewModel view = GetCompanyViewModel(compamy);
                listCompanyViewModel.Add(view);
            }
            return listCompanyViewModel;
        }


        public CompanyContract GetCompanyContractById(int companyId, int contractId)
        {
            var company = GetCompanyById(companyId);
            if (company == null) return null;
            if (company.CompanyContracts == null) return null;
            var contractRecords = company.CompanyContracts;
            var contract = contractRecords.Where(c => c.Id == contractId).FirstOrDefault();
            if (contract == null) return null;
            return contract;
        }
        #endregion

        #region  Create , Update , Remove

        public Company CreateCompany(CompanyInputModel input)
        {
            Company company = new Company();
            SetCompanyValuesFromInput(company, input);
            company.IsActive = true;

            if (!input.NoManager)
            {
                CompanyManeger manager = new CompanyManeger() { ContactInfo = new PersonContactInfo() };
                int personId = input.ManagerInput.PersonId;
                if (personId > 0)
                {
                    var person = GetPersonById(personId);
                    if (person == null) return null;
                    manager = person as CompanyManeger;
                }
                else
                {
                    SetPersonValuesFromInput(manager, input.ManagerInput);
                    SetContactInfoValuesFromInput(manager.ContactInfo, input.ManagerInput.ContactInfoInput);
                    SetCreateRecord(manager);
                    SetCreateRecord(manager.ContactInfo);
                }

                company.Manager = manager;
            }

            CompanyContract contract = new CompanyContract()
            {
                IsActive = true,
                BeginDate = company.BeginDate,
                EndDate = company.EndDate,
            };
            SetCreateRecord(contract);
            company.CompanyContracts = new List<CompanyContract>();
            company.CompanyContracts.Add(contract);

            company = CreateCompany(company);
            return company;
        }
        Company CreateBranch(Company company, int parentCompanyId)
        {
            var parentCompany = GetCompanyById(parentCompanyId);
            if (parentCompany == null) return null;
            company.ParentCompanyId = parentCompanyId;

            return CreateCompany(company);
        }
        public Company CreateAECodeFromCompany(AECodeInputModel inputModel)
        {
            AECode aeCode = new AECode();

            SetAECodeValuesFromInput(aeCode, inputModel);
            aeCode.IsActive = true;
            int companyId = inputModel.CompanyId;
            if (companyId <= 0) return null;
            var company = GetCompanyById(companyId);
            if (company == null) return null;

            var ae = GetAEById(inputModel.AEId);
            if (ae == null) return null;

            SetCreateRecord(aeCode);

            if (company.AECodes == null) company.AECodes = new List<AECode>();
            company.AECodes.Add(aeCode);

            company = UpdateCompany(company);

            if (company == null) return null;

            //create AECodeAE
            AECodeAE record = new AECodeAE()
            {
                Code = aeCode,
                IsActive = true,
                AE = ae
            };
            if (inputModel.BeginDate != null)
            {
                record.BeginDate = Convert.ToDateTime(inputModel.BeginDate);
            }
            SetCreateRecord(record);
            if (ae.AECodeRecords == null) ae.AECodeRecords = new List<AECodeAE>();
            ae.AECodeRecords.Add(record);
            ae = UpdateAE(ae);

            return company;
        }
        public Company CreateCompanyContractRecord(CompanyContractInputModel input)
        {
            int companyId = input.CompanyId;
            var company = GetCompanyById(companyId);
            if (company == null) return null;
            if (company.CompanyContracts == null)
            {
                company.CompanyContracts = new List<CompanyContract>();
            }

            CompanyContract contract = new CompanyContract();
            SetCompanyContrctValuesFromInput(contract, input);
            contract.IsActive = true;
            SetCreateRecord(contract);

            company.CompanyContracts.Add(contract);


            company = UpdateCompany(company);

            if (company == null) return null;

            UpdateCompanyContractStatus(company);


            return company;

        }
        public Company UpdateCompany(CompanyInputModel input)
        {
            Company company = GetCompanyById(input.CompanyId);
            if (company == null)
            {
                //do something
                return null;
            }
            SetCompanyValuesFromInput(company, input);
            SetEditRecord(company);

            company = UpdateCompany(company);
            return company;

        }
        public Company UpdateCompanyManager(CompanyInputModel input)
        {
            Company company = GetCompanyById(input.CompanyId);
            if (company == null)
            {
                //do something
                return null;
            }
            if (input.NoManager)
            {
                company.ManagerId = null;
                return UpdateCompany(company);
            }

            if (input.ManagerInput.IsPersonExist) //現有人員
            {
                company.ManagerId = input.ManagerId;
                return UpdateCompany(company);
            }
            //新增人員
            var personInput = input.ManagerInput;
            var newPerson = new CompanyManeger();
            SetPersonValuesFromInput(newPerson, personInput);
            SetCreateRecord(newPerson);

            var contactInfo = new PersonContactInfo();
            SetContactInfoValuesFromInput(contactInfo, personInput.ContactInfoInput);
            SetCreateRecord(contactInfo);

            newPerson.ContactInfo = contactInfo;
            company.Manager = newPerson;

            return UpdateCompany(company);

        }
        public Company UpdateCompanyContractRecord(CompanyContractInputModel input)
        {
            int companyId = input.CompanyId;
            var company = GetCompanyById(companyId);
            if (company == null) return null;
            if (company.CompanyContracts == null) return null;

            int contractId = input.Id;
            CompanyContract contract = company.CompanyContracts.Where(c => c.Id == contractId).FirstOrDefault();
            if (contract == null)
            {
                //do something
                return null;
            }

            SetCompanyContrctValuesFromInput(contract, input);

            SetEditRecord(contract);


            company = UpdateCompany(company);

            if (company == null) return null;

            UpdateCompanyContractStatus(company);

            return company;

        }
        void UpdateCompanyContractStatus(Company company)
        {
            var contractRecords = company.CompanyContracts;
            if (contractRecords == null)
            {
                company.IsActive = false;
                UpdateCompany(company);
                return;
            }

            var latestRecord = contractRecords.Where(r => !r.Removed && r.IsActive).FirstOrDefault();
            if (latestRecord == null)
            {
                company.IsActive = false;
                UpdateCompany(company);
                return;
            }


            company.IsActive = true;
            company.BeginDate = latestRecord.BeginDate;
            company.EndDate = latestRecord.EndDate;
            UpdateCompany(company);
            return;
        }

        public int RemoveCompany(int companyId)
        {
            var company = GetCompanyById(companyId);
            if (company == null)
            {
                //do something
                return 0;
            }
            company.Removed = true;
            company = UpdateCompany(company);
            if (company == null) return 0;
            return 1;
        }


        public int RemoveCompanyContract(int companyId, int contractId)
        {
            var company = GetCompanyById(companyId);
            if (company == null)
            {
                //do something
                return 0;
            }
            if (company.CompanyContracts == null)
            {
                //do something
                return 0;
            }
            var contract = company.CompanyContracts.Where(c => c.Id == contractId).FirstOrDefault();
            if (contract == null)
            {
                //do something
                return 0;
            }

            contract.Removed = true;
            company = UpdateCompany(company);
            if (company == null)
            {
                //do something
                return 0;
            }

            return 1;


        }

        #endregion

        #region  GetInput/View Models
        public CompanyViewModel GetCompanyViewModel(int companyId)
        {
            Company company = GetCompanyById(companyId);
            if (company == null) return null;
            CompanyViewModel view = new CompanyViewModel();
            InitializeCompanyInputModel(view);
            SetCompanyInputValues(company, view);
            SetCompanyViewValues(company, view);

            return view;
        }
        public CompanyInputModel GetCompanyCreateInputModel()
        {
            CompanyInputModel input = new CompanyInputModel()
            {
                ManagerInput = new PersonInputModel() { AllowSelect = true }
            };
            InitializeCompanyInputModel(input);

            input.ManagerInput = GetPersonCreateInputModel();
            return input;
        }
        public CompanyInputModel GetCompanyInputModel(int companyId)
        {
            var company = GetCompanyById(companyId);
            if (company == null)
            {
                //do something
                return null;
            }
            CompanyInputModel input = new CompanyInputModel();

            InitializeCompanyInputModel(input);

            SetCompanyInputValues(company, input);

            return input;
        }
        public CompanyContractInputModel GetCompanyContractCreateInput(int companyId)
        {

            var company = GetCompanyById(companyId);
            if (company == null)
            {
                //do something
                return null;
            }

            var inputModel = new CompanyContractInputModel();
            inputModel.CurrentMode = CurrentMode.Insert;
            inputModel.CompanyId = companyId;
            inputModel.CompanyNameText = GetCompanyFullName(company);
            inputModel.InputTitle = "新增公司合約記錄";

            return inputModel;
        }
        public CompanyContractInputModel GetCompanyContractInputModel(int companyId, int contractId)
        {
            var contract = GetCompanyContractById(companyId, contractId);
            if (contract == null)
            {
                //do something
                return null;
            }
            var inputModel = new CompanyContractInputModel();
            SetCompanyContractInputValues(contract, inputModel);
            inputModel.InputTitle = "修改公司合約記錄";

            return inputModel;

        }
        public List<CompanyContractInputModel> GetCompanyContractViewList(int companyId)
        {
            var company = GetCompanyById(companyId);
            if (company == null)
            {
                //do somthing
                return null;
            }
            if (company.CompanyContracts == null)
            {
                return null;
            }
            var contractRecords = company.CompanyContracts.Where(c => !c.Removed);
            if (contractRecords == null)
            {
                //do somthing
                return null;
            }
            var contractList = new List<CompanyContractInputModel>();
            foreach (var record in contractRecords)
            {
                var inputModel = new CompanyContractInputModel();
                SetCompanyContractInputValues(record, inputModel);
                contractList.Add(inputModel);
            }

            return contractList;

        }
        public List<AECompanyView> GetCompanyAERecords(int companyId)
        {
            var company = GetCompanyById(companyId);
            if (company == null)
            {
                //do somthing
                return null;
            }
            if (company.AERecords == null) return null;

            var recordViewList = new List<AECompanyView>();
            foreach (var record in company.AERecords)
            {
                AECompanyView companyView = new AECompanyView();

                SetAECompanyInputValues(companyView, record);
                SetAECompanyViewValues(companyView, record);
                recordViewList.Add(companyView);
            }

            return recordViewList;

        }

        CompanyViewModel GetCompanyViewModel(Company company)
        {
            CompanyViewModel view = new CompanyViewModel();

            SetCompanyInputValues(company, view);
            SetCompanyViewValues(company, view);

            return view;
        }
        public CompanySearchInput GetCompanySearchInput()
        {
            CompanySearchInput input = new CompanySearchInput();
            input.SearchOptions = Helper.GetCompanySearchWayOptions();

            input.ParentCompanyOptions = GetCompanyOptions();
            var option = new BaseOption() { Value = "-1", Text = "-------" };
            input.ParentCompanyOptions.Insert(0, option);
            input.ParentCompanyId = -1;
            return input;
        }

        #endregion


        #region  Helper Fuctions
        public string GetCompanyName(int companyId)
        {
            if (companyId <= 0) return "";
            var company = GetCompanyById(companyId);
            if (company == null) return "";
            return company.Name;
        }
        public string GetCompanyFullName(int companyId)
        {
            if (companyId <= 0) return "";
            var company = GetCompanyById(companyId);

            return GetCompanyFullName(company);
        }
        public string GetCompanyFullName(Company company)
        {
            if (company == null) return "";
            if (company.ParentCompanyId > 0)
            {
                string parentCompanyName = GetCompanyName(company.ParentCompanyId);
                return parentCompanyName + " " + company.Name;
            }

            return company.Name;
        }
        #endregion

        #endregion

        #region  memberRepository Fuctions
        Company CreateCompany(Company company)
        {
            SetCreateRecord(company);
            return memberRepository.InsertCompany(company);
        }
        Company UpdateCompany(Company company)
        {
            SetEditRecord(company);
            return memberRepository.UpdateCompany(company);
        }

        public Company GetCompanyById(int companyId)
        {
            return memberRepository.GetCompanyById(companyId);
        }
        public IQueryable<Company> GetAllCompanies()
        {
            return memberRepository.GetAllCompanies();
        }

        #endregion

        #region Input & ViewModel

        #region Initialize Input Values
        void InitializeCompanyInputModel(CompanyInputModel input)
        {
            input.ParentCompanyOptions = GetCompanyOptions();
            input.ParentCompanyOptions.Insert(0, new BaseOption("-------", "0"));
            input.CompanyTypeOptions = Helper.GetBusinessTypeOptions();
        }

        #endregion
        #region Set Input/View Values
        void SetCompanyInputValues(Company company, CompanyInputModel input)
        {
            input.CompanyId = company.CompanyId;
            input.ParentCompanyId = company.ParentCompanyId;

            input.Name = company.Name;
            input.DisplayOrder = company.DisplayOrder;
            input.BeginDate = company.BeginDate;
            input.EndDate = company.EndDate;
            input.CompanyTypeId = (int)company.CompanyType;

            input.ManagerId = company.ManagerId;
        }
        void SetCompanyViewValues(Company company, CompanyViewModel view)
        {
            view.ParentCompanyName = GetCompanyName(company.ParentCompanyId);
            view.FullName = view.ParentCompanyName + " " + view.Name;
            view.CompanyTypeName = Helper.GetBusinessTypeText(view.CompanyTypeId);
            view.StatusText = Helper.GetContractStatusText(company.IsActive);
        }

        void SetCompanyContractInputValues(CompanyContract contract, CompanyContractInputModel inputModel)
        {
            inputModel.Id = contract.Id;
            inputModel.CompanyId = contract.CompanyId;
            inputModel.CompanyNameText = GetCompanyFullName(contract.CompanyId);
            inputModel.CurrentMode = CurrentMode.Edit;
            inputModel.BeginDate = contract.BeginDate;
            inputModel.EndDate = contract.EndDate;
            inputModel.IsActive = contract.IsActive;
            inputModel.PS = contract.PS;
            inputModel.StatusText = Helper.GetContractStatusText(contract.IsActive);
            inputModel.InputTitle = "修改公司合約記錄";
        }
        #endregion
        #region Set Values From Input
        void SetCompanyValuesFromInput(Company company, CompanyInputModel input)
        {
            company.CompanyId = input.CompanyId;
            company.ParentCompanyId = input.ParentCompanyId;

            company.Name = input.Name;
            company.DisplayOrder = input.DisplayOrder;
            company.BeginDate = input.BeginDate;
            company.EndDate = input.EndDate;
            company.CompanyType = (BusinessType)input.CompanyTypeId;

        }
        void SetCompanyContrctValuesFromInput(CompanyContract contract, CompanyContractInputModel input)
        {
            contract.CompanyId = input.CompanyId;
            contract.IsActive = input.IsActive;
            contract.BeginDate = input.BeginDate;
            contract.EndDate = input.EndDate;
            contract.PS = input.PS;

        }
        #endregion
        #endregion
        #endregion

        #region CompanyAE

        #region Public Functions

        #region   Filter Fuctions
        public List<CompanyAE> GetCompanyAEByKeyWord(int companyId, int searchWay = 0, string keyWord = "")
        {
            // string[] status = new string[] { "全部資料", "在職中", "已離職"};
            var allRecords = GetAllCompanyAERecords();
            var companyRecords = allRecords.Where(r => r.CompanyId == companyId);
            switch (searchWay)
            {
                case 0:  //全部資料
                    return companyRecords.Where(record => record.AE.Person.Name.Contains(keyWord)).ToList();

                case 1:  //合約狀態：有效
                    return companyRecords.Where(record => record.IsActive && record.AE.Person.Name.Contains(keyWord)).ToList();

                case 2:  //合約狀態：無效
                    return companyRecords.Where(record => !record.IsActive && record.AE.Person.Name.Contains(keyWord)).ToList();

                default:
                    return companyRecords.Where(record => record.AE.Person.Name.Contains(keyWord)).ToList();
            }
        }
        #endregion

        #region  Create , Update , Remove
        public CompanyAE UpdateCompanyAE(AECompanyInput input)
        {
            var companyAE = GetCompanyAEById(input.Id);
            if (companyAE == null) return null;

            SetAECompanyValuesFromInput(input, companyAE);
            SetEditRecord(companyAE);

            companyAE = UpdateCompanyAE(companyAE);

            if (companyAE == null) return null;

            AE ae = companyAE.AE;

            UpdateAECurrentCompany(ae);

            return companyAE;
        }
        public bool RemoveCompanyAE(int id)
        {
            CompanyAE companyAE = GetCompanyAEById(id);
            if (companyAE == null) return false;

            companyAE.Removed = true;
            SetEditRecord(companyAE);
            companyAE = UpdateCompanyAE(companyAE);

            if (companyAE == null) return false;
            return true;
        }
        #endregion

        #region  GetInput/View Models
        public AECompanyInput GetAECompanyCreateInput(int AEId = 0)
        {
            AECompanyInput input = new AECompanyInput();
            input.CurrentMode = CurrentMode.Insert;
            InitializeAECompanyInput(input);
            if (AEId <= 0) return input;

            var ae = GetAEById(AEId);
            if (ae == null) return input;

            input.AEId = AEId;
            input.AENameText = ae.Person.Name;


            return input;
        }
        public AECompanyInput GetAECompanyInput(int id)
        {
            AECompanyInput input = new AECompanyInput();
            InitializeAECompanyInput(input);
            if (id <= 0) return input;

            var companyAE = GetCompanyAEById(id);
            if (companyAE == null) return input;
            SetAECompanyInputValues(input, companyAE);

            return input;
        }
        public List<AECompanyView> GetAECompanyRecordsView(int AEId)
        {
            var ae = GetAEById(AEId);
            if (ae == null) return null;

            if (ae.CompanyRecords == null) return null;

            var records = ae.CompanyRecords.Where(c => !c.Removed);
            if (records == null) return null;

            List<CompanyAE> listCompanyAE = records.ToList();
            var recordViewList = GetAECompanyRecordsView(listCompanyAE);

            return recordViewList;

        }
        public List<AECompanyView> GetAECompanyRecordsView(List<CompanyAE> listCompanyAE)
        {
            var recordViewList = new List<AECompanyView>();
            foreach (var record in listCompanyAE)
            {
                AECompanyView companyView = new AECompanyView();

                SetAECompanyInputValues(companyView, record);
                SetAECompanyViewValues(companyView, record);
                recordViewList.Add(companyView);
            }
            return recordViewList;
        }
        #endregion



        #endregion



        #region  memberRepository Fuctions
        CompanyAE InsertCompanyAE(CompanyAE companyAE)
        {
            return memberRepository.InsertCompanyAE(companyAE);
        }
        CompanyAE UpdateCompanyAE(CompanyAE companyAE)
        {
            return memberRepository.UpdateCompanyAE(companyAE);
        }
        public CompanyAE GetCompanyAEById(int id)
        {
            return memberRepository.GetCompanyAEById(id);
        }
        public IQueryable<CompanyAE> GetAllCompanyAERecords()
        {
            return memberRepository.GetAllCompanyAERecords();
        }

        #endregion

        #region Input & ViewModel
        #region Initialize Input Values
        void InitializeAECompanyInput(AECompanyInput inputModel)
        {
            var companyOptions = GetCompanyOptions();
            if (inputModel.CompanyId == 0)
            {
                inputModel.CompanyId = Convert.ToInt32(companyOptions.FirstOrDefault().Value);
            }

            var branchOptions = GetBranchOptions(inputModel.CompanyId);
            if (inputModel.BranchId == 0)
            {
                inputModel.BranchId = Convert.ToInt32(branchOptions.FirstOrDefault().Value);
            }

            inputModel.CompanyOptions = companyOptions;
            inputModel.BranchOptions = branchOptions;

            var listAE = GetAllAEs().ToList();
            var aeOptions = new List<BaseOption>();
            foreach (var ae in listAE)
            {
                aeOptions.Add(new BaseOption(ae.Person.Name, ae.AEId.ToString()));
            }
            inputModel.AEOptions = aeOptions;

        }
        #endregion
        #region Set Input Values
        void SetAECompanyInputValues(AECompanyInput inputModel, CompanyAE companyAE)
        {
            var company = companyAE.Company;
            int companyId = 0;
            int branchId = 0;
            if (company.ParentCompanyId == 0)
            {
                companyId = company.CompanyId;
                branchId = 0;
            }
            else
            {
                companyId = company.ParentCompanyId;
                branchId = company.CompanyId;
            }

            inputModel.Id = companyAE.Id;
            inputModel.CompanyId = companyId;
            inputModel.BranchId = branchId;
            inputModel.AEId = companyAE.AEId;
            inputModel.BeginDate = companyAE.BeginDate;
            inputModel.EndDate = companyAE.EndDate;
            inputModel.PersonalTEL = companyAE.PersonalTEL;
            inputModel.TELCode = companyAE.TELCode;

            inputModel.Title = companyAE.Title;
            inputModel.IsActive = companyAE.IsActive;
            inputModel.StaffNumber = companyAE.StaffNumber;
            inputModel.AENameText = GetPersonName(inputModel.AEId);

            inputModel.CompanyFullName = GetCompanyName(companyId) + " " + GetCompanyName(branchId);

        }
        void SetAECompanyViewValues(AECompanyView viewModel, CompanyAE companyAE)
        {
            if (viewModel == null) viewModel = new AECompanyView();
            if (companyAE == null) return;

            viewModel.CompanyText = GetCompanyFullName(Convert.ToInt32(viewModel.CompanyId));
            if (viewModel.IsActive) viewModel.StatusText = "就職中";
            else viewModel.StatusText = "已離職";

        }
        #endregion

        #region Set Values From Input
        void SetAECompanyValuesFromInput(AECompanyInput inputModel, CompanyAE companyAE)
        {
            int companyBranchId;
            if (inputModel.BranchId == 0) companyBranchId = inputModel.CompanyId;
            else companyBranchId = inputModel.BranchId;

            companyAE.CompanyId = companyBranchId;
            companyAE.AEId = inputModel.AEId;
            companyAE.BeginDate = inputModel.BeginDate;
            companyAE.EndDate = inputModel.EndDate;
            companyAE.PersonalTEL = inputModel.PersonalTEL;
            companyAE.StaffNumber = inputModel.StaffNumber;
            companyAE.TELCode = inputModel.TELCode;
            companyAE.Title = inputModel.Title;
            companyAE.IsActive = inputModel.IsActive;
        }



        #endregion
        #endregion
        #endregion

        #region TextTradeRecord
        public TextFuturesTradeRecord InsertTextFuturesTradeRecord(TextFuturesTradeRecord record)
        {
            return memberRepository.InsertTextFuturesTradeRecord(record);
        }
        public TextFuturesTradeRecord UpdateTextFuturesTradeRecord(TextFuturesTradeRecord record)
        {
            return memberRepository.UpdateTextFuturesTradeRecord(record);
        }

        public TextFuturesTradeRecord GetTextFuturesTradeRecordById(int id)
        {
            return memberRepository.GetTextFuturesTradeRecordById(id);
        }
        public IQueryable<TextFuturesTradeRecord> GetAllTextFuturesTradeRecords()
        {
            return memberRepository.GetAllTextFuturesTradeRecords();
        }
        public List<TextFuturesTradeRecord> GetTextFuturesTradeRecordByDate(DateTime date)
        {
            var allRecords = GetAllTextFuturesTradeRecords();
            return allRecords.Where(r => r.DateOfTrade == date).ToList();
        }
        public TextStockTradeRecord InsertTextStockTradeRecord(TextStockTradeRecord record)
        {
            return memberRepository.InsertTextStockTradeRecord(record);
        }
        public TextStockTradeRecord UpdateTextStockTradeRecord(TextStockTradeRecord record)
        {
            return memberRepository.UpdateTextStockTradeRecord(record);
        }
        public TextStockTradeRecord GetTextStockTradeRecordById(int id)
        {
            return memberRepository.GetTextStockTradeRecordById(id);
        }
        public IQueryable<TextStockTradeRecord> GetAllTextStockTradeRecords()
        {
            return memberRepository.GetAllTextStockTradeRecords();
        }


        #endregion

        #region Dispose
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    memberRepository.Dispose();
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

        public class Helper
        {
            public static string GetAccountStatusText(AccountStatus status)
            {

                int value = Convert.ToInt32(status);
                if (value == 0) return "已註銷";
                if (value == 1) return "有效";
                if (value == 2) return "靜止戶";

                return "狀態錯誤";
            }
            public static AccountStatus GetAccountStatusByInput(string input)
            {
                if (input == "Active") return AccountStatus.Active;
                if (input == "Canceled") return AccountStatus.Canceled;
                if (input == "Static") return AccountStatus.Static;

                return AccountStatus.Active;
            }
            public static string GetAECodeText(AECode aeCode)
            {
                string type = "期";
                string aeName = "";
                if (aeCode.BusinessType == BusinessType.Stock) type = "股";
                if (aeCode.BusinessType == BusinessType.Fund) type = "基";

                var ae = aeCode.GetCurrentAE();
                if (ae != null) aeName = ae.Person.Name;

                return String.Format("({0}){1} {2}", type, aeCode.Code, aeName);

            }
            public static bool GetIsOfficialTrader(AccountTrader traderRecord)
            {
                if (traderRecord == null) return false;
                return traderRecord.IsOfficial;

            }
            public static List<BaseOption> GetAccountStatusOptions()
            {
                var statusOptionList = new List<BaseOption>();
                string[] status = new string[] { "已註銷", "有效", "靜止戶" };
                for (int i = 0; i < status.Length; i++)
                {
                    var accountStatusOption = new BaseOption()
                    {
                        Text = status[i],
                        Value = i.ToString()
                    };
                    statusOptionList.Add(accountStatusOption);
                }
                return statusOptionList;
            }
            public static List<BaseOption> GetCompanySearchWayOptions()
            {
                var optionList = new List<BaseOption>();
                string[] status = new string[] { "全部資料", "合約狀態：有效", "合約狀態：無效" };
                for (int i = 0; i < status.Length; i++)
                {
                    var option = new BaseOption()
                    {
                        Text = status[i],
                        Value = i.ToString()
                    };
                    optionList.Add(option);
                }
                return optionList;
            }
            public static List<BaseOption> GetAESearchWayOptions()
            {
                return GetCompanySearchWayOptions();
            }
            public static List<BaseOption> GetPersonSearchWayOptions()
            {
                var optionList = new List<BaseOption>();
                string[] status = new string[] { "姓名", "手機", "Email", "帳號" };
                for (int i = 0; i < status.Length; i++)
                {
                    var option = new BaseOption()
                    {
                        Text = status[i],
                        Value = i.ToString()
                    };
                    optionList.Add(option);
                }
                return optionList;
            }
            public static List<BaseOption> GetAccountTypeOptions()
            {
                var optionList = new List<BaseOption>();
                string[] status = new string[] { "期貨帳戶", "股票帳戶", "基金帳戶" };

                for (int i = 0; i < status.Length; i++)
                {
                    var option = new BaseOption()
                    {
                        Text = status[i],
                        Value = i.ToString()
                    };
                    optionList.Add(option);
                }
                return optionList;
            }

            public static List<BaseOption> GetBusinessTypeOptions()
            {

                var optionList = new List<BaseOption>();
                string[] status = new string[] { "期貨", "股票", "基金" };

                for (int i = 0; i < status.Length; i++)
                {
                    var option = new BaseOption()
                    {
                        Text = status[i],
                        Value = i.ToString()
                    };
                    optionList.Add(option);
                }
                return optionList;
            }
            public static List<BaseOption> GetCurrencyOptions()
            {
                var optionList = new List<BaseOption>();
                string[] options = new string[] { "台幣", "綜合外幣", "美元", "港幣", "日圓", "歐元" };

                for (int i = 0; i < options.Length; i++)
                {
                    var option = new BaseOption()
                    {
                        Text = options[i],
                        Value = i.ToString()
                    };
                    optionList.Add(option);
                }
                return optionList;
            }
            public static string GetCurrencyText(int currencyId)
            {
                string returnValue;
                switch (currencyId)
                {
                    case 0:
                        returnValue = "台幣";
                        break;
                    case 1:
                        returnValue = "綜合外幣";
                        break;
                    case 2:
                        returnValue = "美元";
                        break;
                    case 3:
                        returnValue = "港幣";
                        break;
                    case 4:
                        returnValue = "日圓";
                        break;
                    case 5:
                        returnValue = "歐元";
                        break;
                    default:
                        returnValue = "";
                        break;

                }

                return returnValue;

            }
            public static List<BaseOption> GetBankInfoTypeOptions()
            {
                var optionList = new List<BaseOption>();
                string[] options = new string[] { "出金", "入金" };

                for (int i = 0; i < options.Length; i++)
                {
                    var option = new BaseOption()
                    {
                        Text = options[i],
                        Value = i.ToString()
                    };
                    optionList.Add(option);
                }
                return optionList;
            }
            public static string GetBankInfoTypeText(int typeId)
            {
                if (typeId == 0) return "出金";
                if (typeId == 1) return "入金";
                return "";

            }
            public static bool IsApplicationUser(Person person)
            {
                if (person.Users == null) return false;
                if (person.Users.Count == 0) return false;
                return true;
            }
            public static string GetBusinessTypeText(int businessTypeId)
            {
                // string[] status = new string[] { "期貨", "股票", "基金" };
                if (businessTypeId == 0) return "期貨";
                if (businessTypeId == 1) return "股票";
                if (businessTypeId == 2) return "基金";
                return "";
            }
            public static string GetContractStatusText(bool isActive)
            {

                if (isActive) return "有效";
                return "已無效";
            }

        }



    }//end class


}
