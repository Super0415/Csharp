using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 操作DOS指令
{
    class Program
    {
        /// <summary>
        /// 指令集
        /// </summary>
        static List<string> SelfDOS = new List<string>();
        static void Main(string[] args)
        {
            SelfDOS.Add("倒计时1h");
            SelfDOS.Add("取消定时关机");

            Console.Write("操作指令-->");
            string cmd = Console.ReadLine();
            Console.WriteLine("输入指令-->" + cmd);
            Console.WriteLine(MySelfExecute(cmd, 10));

            Console.Write("操作指令-->");
            string cmd1 = Console.ReadLine();
            Console.WriteLine("输入指令-->" + cmd1);
            Console.WriteLine(MySelfExecute(cmd1, 10));

            Console.Read();
        }

        
        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="cmd">DOS指令</param>
        /// <param name="timeout">指令执行超时限制(0：无限等待)</param>
        static string MySelfExecute(string cmd, int timeout)
        {
            string output = null;
            if (cmd != null && !cmd.Equals(""))
            {
                foreach (string str in SelfDOS)
                {
                    if (cmd.Equals(str))
                    {
                        SelfExecute(cmd, timeout);
                        return "执行完成";
                    }
                }
                output = MyExecute(cmd, timeout);
            }
            return output;
        }

        /// <summary>
        /// 执行DOS指令
        /// </summary>
        /// <param name="cmd">DOS指令</param>
        /// <param name="timeout">指令执行超时限制(0：无限等待)</param>
        static string MyExecute(string cmd,int timeout)
        {

            //Console.Write("操作指令-->");
            //string Mycmd = Console.ReadLine();
            //Console.WriteLine("输入指令-->" + Mycmd);
            //Console.WriteLine(MyExecute(Mycmd, 10));

            string output = null;
            if (cmd != null && !cmd.Equals(""))
            {
                Process p = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                startInfo.Arguments = "/C " + cmd;//“/C”表示执行完命令后马上退出  
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动 
                startInfo.RedirectStandardInput = false;//不重定向输入  
                startInfo.RedirectStandardOutput = true; //重定向输出  
                startInfo.CreateNoWindow = true;//不创建窗口  
                p.StartInfo = startInfo;
                try
                {
                    if (p.Start())//开始进程  
                    {
                        if (timeout == 0)
                            p.WaitForExit();//这里无限等待进程结束                       
                        else
                            p.WaitForExit(timeout); //等待进程结束，等待时间为指定的毫秒                         
                        output = p.StandardOutput.ReadToEnd();//读取进程的输出  
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);//捕获异常，输出异常信息
                }
                finally
                {
                    if (p != null)  p.Close();
                }
            }
            return output;
        }

        /// <summary>
        /// 自定义bat文件
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        static string SelfExecute(string cmd,int timeout)
        {

            //Console.Write("输入指令-->");
            //string Selfcmd = Console.ReadLine();
            //Console.WriteLine("输入指令-->" + Selfcmd);
            //Console.WriteLine(SelfExecute(Selfcmd, 10));

            string output = null;
            if (cmd != null && !cmd.Equals(""))
            {
                Process p = null;
                try
                {
                    string targetDir = string.Format(@"C:\Users\Fengzc\Desktop\关机配置\");//这是bat存放的目录
                    p = new Process();
                    p.StartInfo.WorkingDirectory = targetDir;
                    if (cmd.Equals("倒计时1h"))
                    {
                        p.StartInfo.FileName = "倒计时1h.bat";//bat文件名称
                    }
                    else if (cmd.Equals("取消定时关机"))
                    {
                        p.StartInfo.FileName = "取消定时关机.bat";//bat文件名称
                    }

                    p.StartInfo.Arguments = string.Format("10");//this is argument
                                                                //proc.StartInfo.CreateNoWindow = true;
                                                                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行
                    p.Start();
                    p.WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
                }
            }
            return output;
        }
        
    }

}
