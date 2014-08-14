using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using G16_2013.DAL;
using G16_2013.Models.MemberModel;
using System.Xml;

namespace G16_2013.BLL
{
    public class G16MemberDataBaseInitialHelper : IDisposable
    {
        MemberBL memberBL;
        XmlDocument cityXmlDocument { get; set; }

        G16ApplicationUser user { get; set; }
        
        public G16MemberDataBaseInitialHelper(G16ApplicationUser user, XmlDocument cityXmlDocument)
        {
            memberBL = new MemberBL(user);
            this.cityXmlDocument = cityXmlDocument;
            this.user = user;
        }
       

        public void InitialMemberDB()
        {
            InsertCity(cityXmlDocument);
            InsertIdentities("客戶");
            InsertIdentities("操盤人");
            InsertIdentities("介紹人");
           

            return;

           
        }

        void InsertCity(XmlDocument cityXmlDocument)
        {
            List<City> allCity = new List<City>();

            XmlNode root = cityXmlDocument.DocumentElement;

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

            using (var context = new G16MemberContext())
            {
                foreach (City c in allCity)
                {
                    context.Cities.Add(c);
                }
                context.SaveChanges();
            }
        }
      
        void InsertIdentities(string name)
        {
            memberBL.CreatePublicIdentity(name);
            //string[] names = new string[] { "操盤人", "介紹人", };
           
        }
        void InsertCustomIdentities(string name,int personId)
        {
            memberBL.CreatePublicIdentity(name);

        }
       
    
        #region Dispose
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue == false)
            {
                if (disposing)
                {
                    memberBL.Dispose();
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
