using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yungku.Common.CCASP24WST
{
    public class YK_CCASP24WST
    {
        public enum IOEnum
        {
            LED2 = 0,//通道1选择指示
            LED3,//通道2选择指示
            LED4,//通道3选择指示
            LED5,//通道4选择指示
            KEY1,//S3
            KEY2,//S4
            KEY3,//S5
            KEY4,//数字电位器上的按键
            LockFlag,//按键锁定标志位
            SW1,    //编码器/按钮 选择标志位
        };

        public enum EnableEnum
        {
            Enable1,
            Enable2,
            Enable3,
            Enable4,
        }

        private SerialPort sport = new SerialPort();
        private object syncRoot = new object();

        private int port = 1;
        /// <summary>
        /// 设置或获取通讯端口
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private int baudRate = 115200;
        /// <summary>
        /// 设置或获取波特率
        /// </summary>
        public int BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        private int timeout = 2000;
        /// <summary>
        /// 设置或获取读写超时时间
        /// 修改此值后必须重新调用Open才会生效
        /// </summary>
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        //private int boardId = 0;

        //public int BoardId
        //{
        //	get { return boardId; }
        //	set { boardId = value; }
        //}

        public YK_CCASP24WST()
        {
        }

        /// <summary>
        /// 打开控制卡
        /// </summary>
        public void Open()
        {
            lock (syncRoot)
            {
                if (sport.IsOpen)
                    sport.Close();

                sport.PortName = "COM" + Port.ToString();
                sport.BaudRate = BaudRate;
                sport.StopBits = StopBits.One;
                sport.DataBits = 8;

                sport.ReadTimeout = Timeout;
                sport.WriteTimeout = Timeout;
                sport.Open();
                sport.ReadExisting();
            }
        }

        /// <summary>
        /// 关闭卡
        /// </summary>
        public void Close()
        {
            lock (syncRoot)
            {
                sport.Close();
            }
        }

        private bool SendChar(char ch)
        {
            sport.Write(new char[] { ch }, 0, 1);
            char rch = (char)sport.ReadChar();
            return ch == rch;
        }

        private bool SendCommand(string cmd)
        {
            sport.WriteLine(cmd);
            sport.ReadLine();

            //以下为握手通讯方式，可靠性较高，但会通讯速度较低
            //sport.ReadExisting();
            //cmd += "\n";
            //for (int i = 0; i < cmd.Length; i++)
            //{
            //    bool sendChar = SendChar(cmd[i]);
            //    if (!sendChar)
            //        sendChar = SendChar(cmd[i]);
            //    if (!sendChar)
            //        return false;
            //}
            return true;
        }

        /// <summary>
        /// 执行一个命令并返回一个结果
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected string ExecuteCommand(string cmd)
        {
            lock (syncRoot)
            {
                if (SendCommand(cmd))
                {
                    string ret = sport.ReadLine();
                    return ret.Trim().ToUpper();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        protected int GetIntegerValue(string cmd)
        {
            string ret = ExecuteCommand(cmd);
            return int.Parse(ret);
        }

        protected bool ExecuteAndCheckOk(string cmd)
        {
            string ret = ExecuteCommand(cmd);
            return ret.Equals("OK");
        }

        /// <summary>
        /// 获取所有IO值
        /// </summary>
        /// <returns></returns>
        public int GetIO()
        {
            return GetIntegerValue("getio");
        }

        /// <summary>
        /// 获取所有通道的占空比
        /// </summary>
        /// <returns></returns>
        public int[] GetValue()
        {
            int[] vals = new int[4]{0,0,0,0};
            string ret = ExecuteCommand("getval");
            if (ret.IndexOf(',') > 0)
            {
                string[] retArr = ret.Split(',');
                vals[0] = int.Parse(retArr[0]);
                vals[1] = int.Parse(retArr[1]);
                vals[2] = int.Parse(retArr[2]);
                vals[3] = int.Parse(retArr[3]);
            }
            return vals;
        }


        /// <summary>
        /// 设置指定通道的占空比
        /// </summary>
        /// <param name="chNo"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetValue(int chNo, int val)
        {
            return ExecuteAndCheckOk("setval " + chNo.ToString() + "," + ((ushort)val).ToString());

        }

        public int[] GetEnables()
        {
            int[] vals = new int[4] { 0, 0, 0, 0 };
            string ret = ExecuteCommand("geten");
            if (ret.IndexOf(',') > 0)
            {
                string[] retArr = ret.Split(',');
                vals[0] = int.Parse(retArr[0]);
                vals[1] = int.Parse(retArr[1]);
                vals[2] = int.Parse(retArr[2]);
                vals[3] = int.Parse(retArr[3]);
            }
            return vals;
        }

        public bool SetEnable(int chNo, bool enable)
        {
            return ExecuteAndCheckOk("seten " + chNo.ToString() + "," + (enable ? "1" : "0"));

        }

        public bool IsExists()
        {
            if (!sport.IsOpen)
                return false;
            return ExecuteAndCheckOk("H");
        }
    }
}
