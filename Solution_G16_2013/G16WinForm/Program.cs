using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using G16_2013.DAL;
using G16_2013.Models.MemberModel;

namespace G16WinForm
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<G16MemberContext>());
            string user = "stephenchoe";
           // Database.SetInitializer(new DropCreateMemberDatabaseWithSeedData(user));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new G16MemberForm());
           
        }
    }
}
