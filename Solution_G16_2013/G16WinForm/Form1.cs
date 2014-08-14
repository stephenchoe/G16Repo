using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G16_2013.BLL;
using G16_2013.Models.MemberModel;
using G16_2013.BLL.TradeReport;

namespace G16WinForm
{
  
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test();
            label1.Text = "";
            //string str = "1,3,5";
            //string[] strArr = str.Split(new char[]{','});
            //foreach (string s in strArr)
            //{
            //    label1.Text += s + "-";
            //}
            Bind();
         

           
        }
        void Bind()
        {
            //string filePath = @"c:\tw20130411.txt";
            //DateTime reportDate=new DateTime(2013,4,11);
            //ConcordsTradeReportResolver resolver = 
            //    new ConcordsTradeReportResolver(BusinessType.Futures, reportDate, filePath);
            //List<TextFuturesTradeRecord> listRecords = resolver.ResolveTradeReport();
            //dataGridView1.DataSource = listRecords;
           

        }

        void TestString()
        {
            string input = "8800.0000C";
            string[] splitLine = input.Split(new string[] { "." }, StringSplitOptions.None);
            double i=Convert.ToDouble(splitLine[0]);
            label1.Text = i.ToString();
            //if (input.EndsWith("C"))
            //{
            //    label1.Text = "C";
            //}
            //else if (input.EndsWith("P"))
            //{
            //    label1.Text = "P";
            //}
            //else
            //{
            //    label1.Text = "0";
            //}
            
        }

      

        void Test()
        {
            string filePath = @"c:\tw20130411.txt";
            string line = "";

            string savePath = @"c:\TestWrite20130415.txt";
            List<string> linesToWrite = new List<string>();

            using (System.IO.StreamReader file = new System.IO.StreamReader(filePath, Encoding.Default))
            {
                while ((line = file.ReadLine()) != null)
                {
                    

                    //string[] splitLine = line.Split(new char[] { ' ' });
                    string[] splitLine = line.Split(new string[] { " " },StringSplitOptions.RemoveEmptyEntries);
                   // List<string> columns = new List<string>();
                    List<string>  columns = splitLine.ToList();
                    //foreach (string s in splitLine)
                    //{
                    //    if (s != "")
                    //    {
                    //        columns.Add(s);
                    //    }
                    //}

                    if (columns.Count == 20 || columns.Count == 21)
                    {
                        string lineText = "";
                        if (columns.Count == 20)
                        {
                            columns.Insert(13, "N");
                        }
                        for (int i = 0; i < columns.Count; i++)
                        {
                            lineText += "(" + i.ToString() + ")" + columns[i] ;
                        }
                        linesToWrite.Add(lineText);
                    }

                }
            }

            System.IO.File.WriteAllLines(savePath, linesToWrite);

            label1.Text = "Done!";
        }

        void GetPerson()
        {
            using (MemberBL memberBL = new MemberBL())
            {
                var person = memberBL.GetPersonById(1);
                if (person == null)
                {
                    label1.Text = "null";
                    return;
                }
                label1.Text = person.Name + "," + person.PersonId.ToString();
            }
        }

        void AddPerson()
        {
            var person = new Person()
            {
                Name = "testPersonName",
                //UserName = "testUserName",
                Gender = true,
                TWID = "C100001110",
                Birthday = new DateTime(1980, 3, 4)
            };
            var contactInfo = new PersonContactInfo()
            {
                TEL = "02-23389090",
                Phone = "0935678904",
                Email = "stephen@yahoo.com.tw",

                Address = new Address()
                {
                    StreetAddress = "信義路三段70號5樓",
                    //City = "台北市",
                    //ZipCode = "106",
                    //District = "大安區"
                }
            };
            person.ContactInfo = contactInfo;

            using (MemberBL memberBL = new MemberBL())
            {
                var personAdded = memberBL.InsertPerson(person);
                if (personAdded == null)
                {
                    label1.Text = "null";
                    return;
                }
                label1.Text = person.Name + "," + person.PersonId.ToString();
            }

        }

        void UpdatePerson()
        {
            var person = new Person
            {
                PersonId = 4,
                Name = "testtestPersonNamee",
               // UserName = "testUserNametestPersonName",
                Gender = true,
                TWID = "C10000111000",
                Birthday = new DateTime(1980, 3, 4)

            };


            using (MemberBL memberBL = new MemberBL())
            {
                var returnPerson = memberBL.UpdatePerson(person);
                label1.Text = returnPerson.Name + "," + returnPerson.PersonId.ToString();
            }
        }

        void DeletePerson()
        {
            //using (MemberBL memberBL = new MemberBL())
            //{
            //    int isDeleted = memberBL.DeletePerson(3);
            //    label1.Text = isDeleted.ToString();
            //}
        }


        //public string Name { get; set; }
        //public string TWID { get; set; }
        //public string UserName { get; set; }
        //public ushort Gender { get; set; }
        //public DateTime Birthday { get; set; }
        //public PersonContactInfo ContactInfo { get; set; }

    }

    public abstract class AbsClass
    {
        public string Name { get; set; }
    }

    public class ParentClass : AbsClass
    {
        public ParentClass()
        {
            Name = "ParentClassName";
        }

        public virtual string Show()
        {
            return Name;
        }
        public virtual void ReName()
        {
            Name = "damn";
        }
    }

    public class DevClass : ParentClass
    {
        public override string Show()
        {

            return "shit";
            //  return base.Show();
        }
        public override void ReName()
        {
         
           Name = "DevClass";
           base.ReName();
        }

    }
}
