using CryptoCore.Classes;
using CryptoFront.Forms;
using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoFront
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UserLookAndFeel.Default.SkinName = "Seven Classic";
            DevExpress.Utils.Paint.XPaint.ForceGDIPlusPaint(); // fixes antialiasing in fctb

            DBHelper.HackConnectionString = "ok";


            //Globals.CurrentUser = Globals.LoginUser("admin", "123");
            Application.Run(new LoginForm());
            
            
            
            //Application.Run(new MainForm());

            //Application.Run(new NewJobForm());

        }
    }
}
