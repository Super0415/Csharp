using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;       //正则
using System.Windows.Forms;
using System.Threading;

/// <summary>
/// 基于LWIP协议编制，查询一次，connect连接一次
/// </summary>
namespace Yungku.Common.IOCard.Net
{
    public class YKS2CardNet
    {
        enum AxisStatus
        {
            Alm = 0,    //报警状态
            Busy,       //忙状态
            Err,        //错误状态
            Lmp,        //正极限信号
            Org,        //原点信号
            Lmn,        //负极限信号
            Homing,     //回原点状态中
            LmtEn,      //是否使用极限信号
            OrgEn,      //是否使用原点信号
            LmtRvs,     //是否反转极限信号
            OrgRvs,     //是否反转原点信号
        };

        private object syncRoot = new object();
        private TcpClient client = null;                                        //创建一个客户端
        private NetworkStream Stream = null;                                    //创建一个数据流
        private Socket socketWatch = null;                                      //创建一个socket套接字

        private string ip = "192.168.1.100";
        /// <summary>
        /// 设置或获取通讯IP
        /// </summary>
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        private int port = 4000;
        /// <summary>
        /// 设置或获取通讯IP
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        private int timeout = 300;
        /// <summary>
        /// 读取阻塞、写入阻塞
        /// </summary>
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        /// <summary>
        /// 指定协议栈、服务器IP
        /// </summary>
        public void NetWorkInit(string tip, int tport,int time)
        {
            ip = tip;                 //从 textBox1 框中获取IP地址信息
            port = tport;                             //默认设置端口号为4000
            timeout = time;         //超时
            //使用指定的地址族、套接字类型和协议初始化
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress serverIp = IPAddress.Parse(ip);  //服务端IP 将IP字符串转化为IPAddress实例  注：未填写IP地址，此处阻塞
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public bool Open()
        {
            lock (syncRoot)
            {

                client = new TcpClient();                               //实例化客户端
                try
                {
                    client.Connect(ip, port);
                    int num = 100000;
                    while (num-- > 0) ;
                    Stream = client.GetStream();
                    Stream.ReadTimeout = timeout;                               //读取阻塞ms
                    Stream.WriteTimeout = timeout;                              //写入阻塞ms
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Ping测试
        /// </summary>
        public bool NetWorkPing()
        {
            Ping p = new Ping();
            PingReply reply = p.Send(ip);   //进行 ping 连接测试，并返回连接状态
            if (reply.Status == IPStatus.Success)       //物理链路连接成功
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 关闭卡
        /// </summary>
        public void Close()
        {
            lock (syncRoot)
            {
                client.Close();
            }
        }

        /// <summary>
        /// 执行一个命令并返回一个结果
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <returns></returns>
        protected string ExecuteCommand(string cmd)
        {
            lock (syncRoot)
            {
             
                if (Open())
                {
                    try
                    {
                        string sendata = cmd.Trim()+ "\r\n";
                        byte[] Sentbuff = Encoding.UTF8.GetBytes(sendata);              //将数据存入缓存
                        Stream.Write(Sentbuff, 0, Sentbuff.Length);                 //写入

                        byte[] Recbuff = new byte[1024];                            //创建数据读取缓存区
                        int length = Stream.Read(Recbuff, 0, Recbuff.Length);       //读取要接收的数据
                        string msg = Encoding.UTF8.GetString(Recbuff, 0, length);
                        string[] cmdmsg = Regex.Split(msg, "\n", RegexOptions.IgnoreCase);
                        Thread.Sleep(1);
                        if (cmdmsg[0].Equals(cmd))
                        {
                            Close();
                            return cmdmsg[1];
                        }
                    }
                    catch
                    {
                        Close();
                        return string.Empty;
                    }
                    
                }
                Close();
                return string.Empty;

            }
        }
        /// <summary>
        /// 查询版本信息
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <returns></returns>
        protected string ExecuteVer(string cmd)
        {
            lock (syncRoot)
            {

                if (Open())
                {
                    try
                    {
                        string sendata = cmd.Trim() + "\r\n";
                        byte[] Sentbuff = Encoding.UTF8.GetBytes(sendata);              //将数据存入缓存
                        Stream.Write(Sentbuff, 0, Sentbuff.Length);                 //写入

                        byte[] Recbuff = new byte[1024];                            //创建数据读取缓存区
                        int length = Stream.Read(Recbuff, 0, Recbuff.Length);       //读取要接收的数据
                        string msg = Encoding.UTF8.GetString(Recbuff, 0, length);
                        string[] cmdmsg = Regex.Split(msg, "\n", RegexOptions.IgnoreCase);
                        Thread.Sleep(1);
                        if (cmdmsg[0].Equals(cmd))
                        {
                            Close();
                            return cmdmsg[1] + "\n" + cmdmsg[2] + "\n" + cmdmsg[3] + "\n" + cmdmsg[4] + "\n" + cmdmsg[5];
                        }
                    }
                    catch
                    {
                        Close();
                        return string.Empty;
                    }

                }
                Close();
                return string.Empty;

            }
        }
        

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected int GetIntegerValue(string cmd)
        {
            string ret = ExecuteCommand(cmd);
            if(!ret.Equals("")) return int.Parse(ret);
            return 0;
        }
        /// <summary>
        /// 获取返回字符串
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected string GetString(string cmd)
        {
            return ExecuteCommand(cmd);
        }
        /// <summary>
        /// 获取返回字符串
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected string GetVerString(string cmd)
        {
            return ExecuteVer(cmd);
        }

        protected bool ExecuteAndCheckOk(string cmd)
        {
            string ret = ExecuteCommand(cmd);
            return ret.Equals("OK");
        }

        public void EnumCard()
        {
            lock (syncRoot)
            {
                //sport.WriteLine("EnumIP");
                //string ret = sport.ReadLine();
            }
        }

        /// <summary>
        /// 获取轴IO
        /// </summary>
        /// <returns></returns>
        public int GetAIO()
        {
            return GetIntegerValue("getaio 15");
        }

        public bool SetAIO(int chanel, bool val)
        {
            return ExecuteAndCheckOk("setaio " + chanel.ToString() + "," + (val ? "1" : "0"));
        }

        public int GetInputs()
        {
            return GetIntegerValue("getin");
        }

        public int GetInputsEx()
        {
            return GetIntegerValue("getinex");
        }

        public int GetOutputs()
        {
            return GetIntegerValue("getout");
        }

        public int GetOutputsEx()
        {
            return GetIntegerValue("getoutex");
        }

        /// <summary>
        /// 获取编码开关状态
        /// </summary>
        /// <returns></returns>
        public int GetDipSwitch()
        {
            return GetIntegerValue("getds");
        }

        public int GetDipSwitchEx()
        {
            return GetIntegerValue("getdsex");
        }

        public bool SetOutputs(byte val)
        {
            return ExecuteAndCheckOk("setout " + val.ToString());
        }

        public bool SetOutput(int chanel, bool val)
        {
            return ExecuteAndCheckOk("setout " + chanel.ToString() + "," + (val ? "1" : "0"));
        }

        public bool SetOutputEx(int chanel, bool val)
        {
            return ExecuteAndCheckOk("setoutex " + chanel.ToString() + "," + (val ? "1" : "0"));
        }

        /// <summary>
        /// 获取轴状态
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public int GetAxisStatus(int axisNo)
        {
            return GetIntegerValue("getst " + axisNo.ToString());
        }

        public bool IsBusy(int axisNo)
        {
            int status = GetAxisStatus(axisNo);

            return (status & (0x01 << (int)AxisStatus.Busy)) > 0
                || (status & (0x01 << (int)AxisStatus.Homing)) > 0;
        }

        public int GetPosition(int axisNo)
        {
            return GetIntegerValue("getpos " + axisNo.ToString());
        }

        /// <summary>
        /// 获取主板名称
        /// </summary>
        /// <returns></returns>
        public string GetCardName()
        {
            return GetString("getname");
        }
        /// <summary>
        /// 获取主板编号
        /// </summary>
        /// <returns></returns>
        public int GetSN()
        {
            return GetIntegerValue("getsn");
        } 
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns></returns>
        public string GetVer_Info()
        {
            return GetVerString("getver");
        }
        public bool SetPosition(int axisNo, int pos)
        {
            return ExecuteAndCheckOk("setpos " + axisNo.ToString() + "," + pos.ToString());
        }

        public bool RltMove(int axisNo, int dist, int startVel = 100,
            int vel = 2000, double acc = 0.2, double dec = 0.2)
        {
            int accTimeToPules = (int)((vel - startVel) / (acc * 1000));
            string cmd = string.Format("move {0},{1},{2},{3},{4},{5}",
                axisNo, dist, startVel, vel, accTimeToPules, accTimeToPules);

            return ExecuteAndCheckOk(cmd);
        }

        /// <summary>
        /// 绝对移动
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="pos"></param>
        /// <param name="startVel"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        public bool AbsMove(int axisNo, int pos, int startVel = 100,
            int vel = 2000, double acc = 0.2, double dec = 0.2)
        {
            int accTimeToPules = (int)((vel - startVel) / (acc * 1000));
            string cmd = string.Format("moveto {0},{1},{2},{3},{4},{5}",
                axisNo, pos, startVel, vel, accTimeToPules, accTimeToPules);

            return ExecuteAndCheckOk(cmd);
        }

        public bool Home(int axisNo, int startVel = 100, int homeDir = 0, int homeSVel = 200,
            int vel = 2000, double acc = 0.2, double dec = 0.2,
            int homeMode = 0, int offset = 0)
        {
            int accTimeToPules = (int)((vel - startVel) / (acc * 1000));
            string cmd = string.Format("home {0},{1},{2},{3},{4},{5},{6},{7},{8}",
                axisNo, startVel, vel, accTimeToPules, accTimeToPules,
                homeMode, homeDir, homeSVel, offset);

            return ExecuteAndCheckOk(cmd);
        }

        /// <summary>
        /// 减速停
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool Stop(int axisNo)
        {
            return ExecuteAndCheckOk("stop " + axisNo.ToString() + ",1");
        }

        /// <summary>
        /// 立即停
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool EmgStop(int axisNo)
        {
            return ExecuteAndCheckOk("stop " + axisNo.ToString() + ",0");
        }

        public bool SetLimits(int axisNo, bool lmtEn, bool orgEn,
            bool lmtReverse, bool orgReverse)
        {
            string cmd = "setlmt " + axisNo.ToString() + "," + (lmtEn ? "1" : "0")
                 + "," + (orgEn ? "1" : "0")
                 + "," + (lmtReverse ? "1" : "0")
                 + "," + (orgReverse ? "1" : "0");

            return ExecuteAndCheckOk(cmd);
        }

        /// <summary>
        /// 心跳检测
        /// </summary>
        /// <returns></returns>
        public bool IsExists()
        {
            if (!NetWorkPing())
                return false;
            return ExecuteAndCheckOk("H");
        }

    }
}
