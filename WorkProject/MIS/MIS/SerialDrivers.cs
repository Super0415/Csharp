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

        private SerialPort sport = new SerialPort();
        private object syncRoot = new object();
        Thread thread = null;
        Stopwatch stopwatch = null;
        string myPort = null;
        bool statePort = false;
        

        private List<byte> buffer = new List<byte>(4096);


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
                    if(stopwatch.ElapsedMilliseconds > 3000)
                    {
                        Port = myPort;
                        Open();
                        SendReadCmd();
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
                        SendReadCmd();
                        Thread.Sleep(1000); //延时1000ms
                    }

                }
                

                Thread.Sleep(10); //延时10ms
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
                {
                    if (buffer.Count == 23)
                    {
                        //验证帧头帧尾
                        if (buffer[0] == 0xEB && buffer[1] == 0x91 && buffer[21] == 0xAA && buffer[22] == 0xBB)
                        {
                            if (CheckDataOut(buffer.ToArray()) == buffer[20])
                            {
                                statePort = true;
                                myPort = Port;
                                stopwatch.Restart();
                            }
                        }
                        
                        buffer.Clear();
                    }
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
        /// 发送写卡指令        
        /// </summary>
        private void SendReadCmd()
        {
            byte[] msg = new byte[23];
            msg[0] = 0xEB;
            msg[1] = 0x90;
            for (int i = 0; i < 16; i++)
            {
                msg[2 + i] = (byte)i;
            }
            msg[20] = CheckDataOut(msg);
            msg[21] = 0xAA;
            msg[22] = 0xBB;

            ExecuteCommand(msg);
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
                    stopwatch.Restart();
                    sport.Write(Tmsg, 0, Tmsg.Length);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("发送失败！");
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

        }



        byte CheckDataOut(byte[] buf)
        {
            int Sum = 0;
            for (int i = 2; i < 20; i++)
            {
                Sum += buf[i];
            }
            return (byte)(0x100 - (byte)Sum);
        }




    }
}
