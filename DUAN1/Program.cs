using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUAN1
{
    public class Connecstring
    {
        public static string connectionString = @"Data Source= DESKTOP-VJM0K6K\SQLEXPRESS;Initial Catalog=workshop1;User ID=sa;Password=123456;TrustServerCertificate=True";

    }
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
      

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
