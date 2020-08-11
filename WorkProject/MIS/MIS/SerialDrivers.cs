using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MIS
{
    class SerialDrivers
    {
        #region  数据定义

        /// <summary>
        /// 可使用存储块
        /// </summary>
        byte[] BlockNumber = new byte[45]
        {                       0x04,0x05,0x06,
            0x08,0x09,0x0A,     0x0C,0x0D,0x0E,
            0x10,0x11,0x12,     0x14,0x15,0x16,
            0x18,0x19,0x1A,     0x1C,0x1D,0x1E,
            0x20,0x21,0x22,     0x24,0x25,0x26,
            0x28,0x19,0x1A,     0x2C,0x2D,0x2E,
            0x30,0x31,0x32,     0x34,0x35,0x36,
            0x38,0x39,0x3A,     0x3C,0x3D,0x3E,
        };

        private SerialPort sport = new SerialPort();
        private object syncRoot = new object();
        Thread thread = null;
        Stopwatch stopwatch = null;
        string myPort = null;
        private List<byte> buffer = new List<byte>(4096);
        MyData mydata = null;

        private bool statePort = false;
        /// <summary>
        /// 获取通讯状态
        /// </summary>
        public bool StatePort
        {
            get { return statePort; }
        }
        private bool stateNFC = false;
        /// <summary>
        /// 获取NFC状态
        /// </summary>
        public bool StateNFC
        {
            get { return stateNFC; }
        }

        private string port = "COM3";
        /// <summary>
        /// 设置或获取通讯端口
        /// </summary>
        public string Port
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
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SerialDrivers()
        {
            thread = new Thread(CheckSerial);
            thread.IsBackground = true;
            thread.Start();
            stopwatch = new Stopwatch();

            mydata = MyData.Instance;

            System.Diagnostics.Debug.WriteLine("开始串口通讯!");
        }

        /// <summary>
        /// 断开通讯时自动搜索串口
        /// </summary>
        private void CheckSerial()
        {
            while (true)
            {
                if (statePort == true)
                {
                    if (stopwatch.ElapsedMilliseconds > 3000)
                    {
                        stopwatch.Restart();
                        statePort = false;
                    }
                    else if (stopwatch.ElapsedMilliseconds > 1000)
                    {
                        Thread.Sleep(500);
                        Port = myPort;
                        Open();
                        SendReadCmd(0);
                    }
                }
                else
                {
                    string[] ports = SerialPort.GetPortNames();
                    for (int i = 0; i < ports.Length; i++)
                    {
                        if (statePort == true)
                            break;
                        Port = ports[i];
                        Open();
                        SendReadCmd(0);
                        Thread.Sleep(500); //延时500ms
                    }

                }
            }
        }

        /// <summary>
        /// 打开控制卡
        /// </summary>
        private bool Open()
        {
            lock (syncRoot)
            {
                try
                {
                    if (sport.IsOpen)
                        sport.Close();

                    sport.PortName = Port;
                    sport.BaudRate = BaudRate;
                    sport.StopBits = StopBits.One;
                    sport.DataBits = 8;

                    sport.ReadTimeout = Timeout;
                    sport.WriteTimeout = Timeout;
                    sport.Open();
                    sport.ReadExisting();

                    sport.DataReceived += Sport_DataReceived;


                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 接收函数中断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = sport.BytesToRead;
                byte[] buf = new byte[n];
                sport.Read(buf, 0, n);
                //缓存数据           
                buffer.AddRange(buf);
                //验证数据完整
                if (buffer.Count == 23)
                {
                    //验证帧头帧尾
                    if (buffer[0] == 0xEB && buffer[1] == 0x91 && buffer[21] == 0xAA && buffer[22] == 0xBB)
                    {
                        if (CheckDataOut(buffer.ToArray()) == buffer[20])
                        {
                            AnalysisData(buffer.ToArray());
                            statePort = true;
                            myPort = Port;
                            stopwatch.Restart();
                        }
                    }
                    buffer.Clear();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("接收失败！");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 关闭卡
        /// </summary>
        private void Close()
        {
            lock (syncRoot)
            {
                sport.Close();
            }
        }

        /// <summary>
        /// 发送执行函数
        /// </summary>
        private void ExecuteCommand(Byte[] Tmsg)
        {
            lock (syncRoot)
            {
                try
                {
                    sport.Write(Tmsg, 0, Tmsg.Length);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("发送失败！");
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

        }


        /// <summary>
        /// 发送写卡指令
        /// </summary>
        /// <param name="n">区块号</param>
        /// <param name="data">数据</param>
        private void SendWriteCmd(int n, byte[] data)
        {
            byte[] msg = new byte[23];
            int len = 16 < data.Length ? 16 : data.Length;
            msg[0] = 0xEB;
            msg[1] = 0x90;
            msg[2] = 0x01;
            msg[3] = (byte)BlockNumber[n];

            for (int i = 0; i < len; i++)
            {
                msg[4 + i] = (byte)data[i];
            }
            msg[20] = CheckDataOut(msg);
            msg[21] = 0xAA;
            msg[22] = 0xBB;

            ExecuteCommand(msg);
        }
        /// <summary>
        /// 发送读卡指令        
        /// </summary>
        /// <param name="n">区块号</param>
        /// <param name="data">数据</param>
        private void SendReadCmd(int n)
        {
            byte[] msg = new byte[23];
            int len = 16;
            msg[0] = 0xEB;
            msg[1] = 0x90;
            msg[2] = 0x02;
            msg[3] = (byte)BlockNumber[n];

            for (int i = 0; i < len; i++)
            {
                msg[4 + i] = 0;
            }
            msg[20] = CheckDataOut(msg);
            msg[21] = 0xAA;
            msg[22] = 0xBB;

            ExecuteCommand(msg);
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        private byte CheckDataOut(byte[] buf)
        {
            int Sum = 0;
            for (int i = 2; i < 20; i++)
            {
                Sum += buf[i];
            }
            return (byte)(0x100 - (byte)Sum);
        }


        /// <summary>
        /// 当前通讯卡号
        /// </summary>
        private string nowCard = string.Empty;
        public string NowCard
        {
            get { return nowCard; }
        }
        /// <summary>
        /// 数据验证通过后，进行数据解析
        /// </summary>
        /// <param name="data"></param>
        private void AnalysisData(byte[] data)
        {
            
            switch (data[2]) //数据标识
            {
                case 0x00:      //无NFC卡
                    stateNFC = false;
                    mydata.ReflashCardState(nowCard, "FAIL");
                    mydata.ReflashCardActionState(nowCard, "无卡");
                    break;
                case 0x01:      //识别卡并校验成功
                    DistinguishCardAndCheckOK(data);
                    mydata.ReflashCardState(nowCard, "OK");
                    stateNFC = true;
                    break;
                case 0x02:      //识别卡但校验失败
                    DistinguishCardAndCheckFail(data);
                    mydata.ReflashCardState(nowCard, "FAIL");
                    stateNFC = true;
                    break;
                case 0x03:      //读卡成功，数据有效
                    stateNFC = true;
                    ReadCarkOK(data);
                    mydata.ReflashCardState(nowCard, "OK");
                    mydata.ReflashCardActionState(nowCard, "读卡成功");
                    break;
                case 0x04:      //读卡失败，数据无效
                    mydata.ReflashCardState(nowCard, "FAIL");
                    mydata.ReflashCardActionState(nowCard, "读卡失败");
                    stateNFC = false;
                    break;
                case 0x05:      //写卡成功，数据无效
                    mydata.ReflashCardActionState(nowCard, "写卡成功");
                    break;
                case 0x06:      //写卡失败，数据无效
                    mydata.ReflashCardActionState(nowCard, "写卡失败");
                    break;
                case 0x07:      //非法存储块操作
                    mydata.ReflashCardActionState(nowCard, "非法操作");
                    break;
                case 0xFF:      //硬件初始化失败
                    stateNFC = false;
                    mydata.ReflashCardActionState(nowCard, "初始化失败");
                    break;
                case 0xEE:      //硬件初始化成功
                    mydata.ReflashCardActionState(nowCard, "初始化成功");
                    break;

            }

        }

        /// <summary>
        /// 读卡成功，数据有效
        /// </summary>
        /// <param name="data"></param>
        private void ReadCarkOK(byte[] data)
        {
            if (mydata.IsRepeat(nowCard))
            {
                string da = string.Empty;
                byte[] buff = new byte[16];
                for (int i = 0; i < 16; i++)
                {
                    buff[i] = data[4+i];
                }
                string str = System.Text.Encoding.Default.GetString(buff);
                mydata.ReadUserInfo(nowCard, buff[3], str);
            }
        }

        /// <summary>
        /// 识别卡但校验失败
        /// </summary>
        /// <param name="data"></param>
        private void DistinguishCardAndCheckFail(byte[] data)
        {
            int num = data[3] << 24 | data[4] << 16 | data[5] << 8 | data[6];
            if (!mydata.IsRepeat(num.ToString()))
            {
                nowCard = num.ToString();
                mydata.Add(nowCard);
                
            }
        }

        /// <summary>
        /// 识别卡并校验成功
        /// </summary>
        /// <param name="data"></param>
        private void DistinguishCardAndCheckOK(byte[] data)
        {
            int num = data[3] << 24 | data[4] << 16 | data[5] << 8 | data[6];
            if (!mydata.IsRepeat(num.ToString()))
            {
                nowCard = num.ToString();
                mydata.Add(nowCard);                
            }
        }


    }
}
