using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;

using G16_2013.Models.MemberModel;
using G16_2013.Models.TradeModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Xml;

namespace G16_2013.DAL
{
    public class G16MemberEntitiesSeedData : DropCreateDatabaseAlways<G16MemberEntities>
    {
        XmlDocument cityDistrictDoc;
        public G16MemberEntitiesSeedData(XmlDocument doc)
        {
            cityDistrictDoc = doc;
        }
        protected override void Seed(G16MemberEntities context)
        {
            InsertCity(context);
            // InitializeIdentityForEF(context);
            base.Seed(context);
        }
        void InsertCity(G16MemberEntities context)
        {
            List<City> allCity = new List<City>();

            XmlNode root = cityDistrictDoc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                XmlElement element = (XmlElement)node;
                XmlAttribute attributeID = element.GetAttributeNode("ID");
                string id = attributeID.Value;
                XmlAttribute attributeName = element.GetAttributeNode("Name");
                string name = (attributeName.Value).Replace("臺", "台");
                XmlAttribute attributeTelCode = element.GetAttributeNode("TelCode");
                string telCode = attributeTelCode.Value;
                var city = new City
                {
                    Code = id,
                    Name = name,
                    TelCode = telCode
                };
                if (city.Districts == null) city.Districts = new List<District>();

                foreach (XmlNode item in node.ChildNodes)
                {
                    XmlElement itemElement = (XmlElement)item;
                    XmlAttribute districtNameAttribute = itemElement.GetAttributeNode("Name");
                    string districtName = (districtNameAttribute.Value).Replace("臺", "台");
                    XmlAttribute districtAttributeZip = itemElement.GetAttributeNode("Zip");
                    string districtZip = districtAttributeZip.Value;

                    District district = new District
                    {
                        Name = districtName,
                        ZipCode = districtZip
                    };

                    city.Districts.Add(district);
                }

                allCity.Add(city);
            }


            foreach (City c in allCity)
            {
                context.Cities.Add(c);
            }
            //City testCity = new City()
            //{
            //    Name="test",
            //    Code="testCode",
            //     TelCode="000",
            //};
            //context.Cities.Add(testCity);

            //context.Commit();

        }
    }
    public class DropCreateMemberDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<G16MemberContext>
    {
        protected override void Seed(G16MemberContext context)
        {
            // InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(G16MemberContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<G16ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<G16ApplicationRoleManager>();
            const string name = "admin@admin.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new G16ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
    public class DropCreateMemberDatabaseWithSeedData : DropCreateDatabaseAlways<G16MemberContext>
    {
        UserInputModel User { get; set; }


        G16ApplicationUserManager userManager;
        G16ApplicationRoleManager roleManager;

        public DropCreateMemberDatabaseWithSeedData(UserInputModel user)
        {
            User = user;
        }
        protected override void Seed(G16MemberContext context)
        {
            InitializeIdentity(context);
            //var userAdded = userManager.FindByName(UserName);
            //if (userAdded != null) UserId = userAdded.Id;
            base.Seed(context);

        }

        public void InitializeIdentity(G16MemberContext context)
        {
            userManager = new G16ApplicationUserManager(new UserStore<G16ApplicationUser>(context));

            roleManager = new G16ApplicationRoleManager(new RoleStore<IdentityRole>(context));

            string[] names = new string[] { "老闆", "業務員", "客戶" };

            foreach (string roleName in names)
            {

                var role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = new G16ApplicationUser()
            {
                Email = User.Email,
                UserName = User.UserName
            };


            var result = userManager.Create(user, User.PassWord);
            result = userManager.SetLockoutEnabled(user.Id, false);


            // Add user admin to Role Admin if not already added


            result = userManager.AddToRole(user.Id, "老闆");



        }
    }

    public class DropCreateTradeDatabaseWithSeedData : DropCreateDatabaseAlways<TradeContext>
    {
        protected override void Seed(TradeContext context)
        {
            InsertCompany(context);
            InsertContinent(context);
            InsertCurrency(context);
            InsertRedPointGroup(context);
            InsertTradeRecord(context);
            base.Seed(context);

        }
        static void InsertTradeRecord(TradeContext context)
        {
            var record = new TradeRecord
            {
                DateOfTrade = new DateTime(2014, 4, 6, 11, 15, 22, 667),
                DealTime = new TimeSpan(0, 10, 19, 32, 867)

            };
            context.TradeRecords.Add(record);
        }
        static void InsertCompany(TradeContext context)
        {
            var company = new TradeCompany
            {
                Name = "康和期貨",
                IsActive = true,
            };

            var company_A = new TradeCompany
            {
                Name = "康和證券",
                IsActive = true,
            };
            var systemGPM = new TradeSystem
            {
                Name = "全都賺",
                IsActive = true
            };
            var systemEFlash = new TradeSystem
            {
                Name = "e閃電",
                IsActive = true
            };


            company.TradeSystems = new List<TradeSystem>();
            company_A.TradeSystems = new List<TradeSystem>();
            company.TradeSystems.Add(systemGPM);
            company.TradeSystems.Add(systemEFlash);
            company_A.TradeSystems.Add(systemGPM);
            company_A.TradeSystems.Add(systemEFlash);

            context.TradeCompanies.Add(company);
            context.TradeCompanies.Add(company_A);

        }
        private static void InsertContinent(TradeContext context)
        {

            var asia = new Continent()
            {
                Name = "亞洲",
                DisplayOrder = 0
            };
            context.Continents.Add(asia);
            var us = new Continent()
            {
                Name = "美洲",
                DisplayOrder = 1
            };
            context.Continents.Add(us);
            var eu = new Continent()
            {
                Name = "歐洲",
                DisplayOrder = 2
            };
            context.Continents.Add(eu);


        }
        private static void InsertCurrency(TradeContext context)
        {
            int displayOrder = 0;
            var twd = new Currency()
            {
                Name = "台幣",
                Code = "TWD",
                DisplayOrder = displayOrder
            };
            context.Currencies.Add(twd);
            displayOrder++;

            var usd = new Currency()
            {
                Name = "美元",
                Code = "USD",
                DisplayOrder = displayOrder
            };
            context.Currencies.Add(usd);
            displayOrder++;

            var jpy = new Currency()
            {
                Name = "日圓",
                Code = "JPY",
                DisplayOrder = displayOrder
            };
            context.Currencies.Add(jpy);
            displayOrder++;

            var hkd = new Currency()
            {
                Name = "港幣",
                Code = "HKD",
                DisplayOrder = displayOrder
            };
            context.Currencies.Add(hkd);
            displayOrder++;

            var eud = new Currency()
            {
                Name = "歐元",
                Code = "EUD",
                DisplayOrder = displayOrder
            };
            context.Currencies.Add(eud);
            displayOrder++;

            var gbp = new Currency()
            {
                Name = "英鎊",
                Code = "GBP",
                DisplayOrder = displayOrder
            };
            context.Currencies.Add(gbp);
            displayOrder++;

            var sg = new Currency()
            {
                Name = "新加坡幣",
                Code = "SGD",
                DisplayOrder = displayOrder
            };
            context.Currencies.Add(sg);



        }

        static void InsertRedPointGroup(TradeContext context)
        {
            string[] names = new string[] { "大台", "小台", "選擇權", "國外期貨" };
            int[] values = new int[] { 5, 2, 1, 5 };

            for (int i = 0; i < names.Length; i++)
            {
                var group = new RedPointGroup
                {
                    Name = names[i],
                    RedPointValue = values[i]
                };
                context.RedPointGroups.Add(group);
            }

        }


    }
}
