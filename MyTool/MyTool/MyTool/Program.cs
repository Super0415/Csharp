using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTool
{
    static class Program
    {
        public static System.Threading.Mutex Run;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool noRun = false;
            Run = new System.Threading.Mutex(true, "MainForm", out noRun);
            //检测是否已经运行
            if (noRun)
            {//未运行
                Run.ReleaseMutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new MainForm());
            }
            else
            {//已经运行
             //MessageBox.Show("已经有一个实例正在运行!");
             //切换到已打开的实例
            }
        }
    }
}
