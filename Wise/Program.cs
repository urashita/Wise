/*----------------------------------------*/
/*                                        */
/* Program.cs                             */
/*                                        */
/* WISE (C) Shoji Urashita                */
/*----------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Wise
{
    static class Program
    {
        /// <summary>
        /// Entry point of Main Application
        /// </summary>
        /// 
        [STAThread]
        static void Main(string[] args)
        {
            if (System.Diagnostics.Process.GetProcessesByName(
                System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {       
                MessageBox.Show("Already running.");
                return;
            }

            if (args.Length > 0)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                new CultureInfo(args[0], false);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form f1 = new MainForm();
            Application.Run(f1);
        }

    }
}
