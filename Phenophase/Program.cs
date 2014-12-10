using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
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
            
            /*----------Single Instance-------------------*/
            bool ok;
            System.Threading.Mutex m = new System.Threading.Mutex(true, "phenology", out ok);

            if (!ok)
            {
                MessageBox.Show("Another instance is already running.");
                return;
            }
            //----------------------------------------------
            Application.Run(new FrmMain());
            
            //-------------------------------------------
            GC.KeepAlive(m);    
        }
    }
}
