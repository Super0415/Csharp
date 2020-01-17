using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;   //测试响应时间

namespace RS485Panel
{
    class CustomRS485
    {
        enum CMD
        {
            //状态
            /// <summary>
            /// 查状态
            /// </summary>
            GetState = 0,
            /// <summary>
            /// 设置输出
            /// </summary>
            SetState = 1,
            /// <summary>
            /// 查类型
            /// </summary>
            GetType = 2,
            //IO类型
            /// <summary>
            /// 固定输入
            /// </summary>
            IO_X = 0,
            /// <summary>
            /// 固定输出
            /// </summary>
            IO_Y = 1,
            /// <summary>
            /// 可调?类型
            /// </summary>
            IO_SetType = 2,

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

        private int timeout = 200;
        /// <summary>
        /// 设置或获取读写超时时间
        /// 修改此值后必须重新调用Open才会生效
        /// </summary>
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        private long RecordTotalTime = 0;
        private long RecordTotalNum = 0;

        public long[] GetCheckInfo()
        {
            long[] info = new long[3];
            info[0] = RecordTotalTime;
            info[1] = RecordTotalNum;
            if (RecordTotalNum != 0)
            {
                info[2] = RecordTotalTime / RecordTotalNum;
            }
            
            return info;
        }

        /// <summary>
        /// 打开控制卡
        /// </summary>
        public bool Open()
        {
            lock (syncRoot)
            {
                try
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
                    return true;
                }
                catch
                {
                    return false;
                }
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





        /// <summary>
        /// 发送接收
        /// </summary>
        /// <param name="Tmsg"></param>
        /// <returns></returns>
        private Byte[] ExecuteCommand(Byte[] Tmsg)
        {
            Byte[] Rmsg = new byte[7];
            lock (syncRoot)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start(); //  开始监视代码运行时间
                try
                {
                    sport.Write(Tmsg, 0, Tmsg.Length);
                    

                    for (int i = 0; i < 7; i++)
                    {
                        Rmsg[i] = (byte)sport.ReadByte();
                    }
                    int Test = Get_Crc(Rmsg, 6);
                    if (Get_Crc(Rmsg, 6) == Rmsg[6])
                    {
                        stopwatch.Stop();
                        RecordTotalTime += stopwatch.ElapsedMilliseconds;
                        RecordTotalNum++;
                        return Rmsg;
                    }
                    else
                    {
                        stopwatch.Stop();
                        RecordTotalTime += stopwatch.ElapsedMilliseconds;
                        RecordTotalNum++;
                        return null;
                    }
                }
                catch
                {
                    stopwatch.Stop();
                    RecordTotalTime += stopwatch.ElapsedMilliseconds;
                    RecordTotalNum++;
                    return null;
                }

            }
        }



        /// <summary>
        /// 查询固定输入状态
        /// </summary>
        public byte[] Query_AllFixdInput(int Addr,int Pins)
        {
            byte[] Tmsg = new byte[7];
            byte[] Rmsg = new byte[7];
            Tmsg[0] = (byte)Addr;
            Tmsg[1] = (byte)CMD.GetState;
            Tmsg[2] = (byte)CMD.IO_X; 
           Tmsg[3] = (byte)Pins;
            Tmsg[4] = 0x00;
            Tmsg[5] = 0x00;
            Tmsg[6] = Get_Crc(Tmsg, 6);
            Rmsg = ExecuteCommand(Tmsg);

            return Rmsg;

        }

        /// <summary>
        /// 查询固定输出状态 
        /// </summary>
        public byte[] Query_AllFixdOutput(int Addr, int Pins)
        {
            byte[] Tmsg = new byte[7];
            byte[] Rmsg = new byte[7];
            Tmsg[0] = (byte)Addr;
            Tmsg[1] = (byte)CMD.GetState;
            Tmsg[2] = (byte)CMD.IO_Y;
            Tmsg[3] = (byte)Pins;
            Tmsg[4] = 0x00;
            Tmsg[5] = 0x00;
            Tmsg[6] = Get_Crc(Tmsg, 6);
            Rmsg = ExecuteCommand(Tmsg);
            return Rmsg;
        }
        /// <summary>
        /// 设置固定输出状态 
        /// </summary>
        public bool Set_AllFixdOutput(int Addr, int Pins,int PinState)
        {
            byte[] Tmsg = new byte[7];
            byte[] Rmsg = new byte[7];
            Tmsg[0] = (byte)Addr;
            Tmsg[1] = (byte)CMD.SetState;
            Tmsg[2] = (byte)CMD.IO_Y;
            Tmsg[3] = (byte)Pins;
            Tmsg[4] = (byte)PinState;
            Tmsg[5] = 0x00;
            Tmsg[6] = Get_Crc(Tmsg, 6);
            Rmsg = ExecuteCommand(Tmsg);
            if (Rmsg != null && Rmsg[5] == 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 查询可调端口类型
        /// </summary>
        public byte[] Query_TypeIO(int Addr, int Pins)
        {
            byte[] Tmsg = new byte[7];
            byte[] Rmsg = new byte[7];
            Tmsg[0] = (byte)Addr;
            Tmsg[1] = (byte)CMD.GetType;
            Tmsg[2] = (byte)CMD.IO_SetType;
            Tmsg[3] = (byte)Pins;
            Tmsg[4] = 0x00;
            Tmsg[5] = 0x00;
            Tmsg[6] = Get_Crc(Tmsg, 6);
            Rmsg = ExecuteCommand(Tmsg);
            return Rmsg;
        }

        /// <summary>
        /// 查询可调端口输入型状态
        /// </summary>
        public byte[] Query_TypeIO_State(int Addr, int Pins)
        {
            byte[] Tmsg = new byte[7];
            byte[] Rmsg = new byte[7];
            Tmsg[0] = (byte)Addr;
            Tmsg[1] = (byte)CMD.GetState;
            Tmsg[2] = (byte)CMD.IO_SetType;
            Tmsg[3] = (byte)Pins;
            Tmsg[4] = 0x00;
            Tmsg[5] = 0x00;
            Tmsg[6] = Get_Crc(Tmsg, 6);
            Rmsg = ExecuteCommand(Tmsg);
            return Rmsg;
        }

        /// <summary>
        /// 设置可调输出状态 
        /// </summary>
        public bool Set_AllSetOutput(int Addr, int Pins, int PinState)
        {
            byte[] Tmsg = new byte[7];
            byte[] Rmsg = new byte[7];
            Tmsg[0] = (byte)Addr;
            Tmsg[1] = (byte)CMD.SetState;
            Tmsg[2] = (byte)CMD.IO_SetType;
            Tmsg[3] = (byte)Pins;
            Tmsg[4] = (byte)PinState;
            Tmsg[5] = 0x00;
            Tmsg[6] = Get_Crc(Tmsg, 6);
            Rmsg = ExecuteCommand(Tmsg);
            if (Rmsg != null && Rmsg[5] == 0)
                return true;
            else return false;
        }

        /// <summary>
        /// 异或和校验码
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        byte Get_Crc(byte[] buf, byte len)
        {
            byte crc = 0;
            byte i = 0;
            for (i = 0; i < len; i++)
            {
                crc ^= buf[i];
            }
            return crc;
        }
    }
}
