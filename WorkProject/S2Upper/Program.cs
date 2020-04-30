using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace S2Upper
{

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process[] tProcess = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (tProcess.Length > 1)
                Application.Exit();
            else
                Application.Run(new Form1());
        }
    }
}
