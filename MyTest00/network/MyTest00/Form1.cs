﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Yungku.Common.IOCard.Net;
using Yungku.Common.IOCard.DataDeal;

namespace MyTest00
{

        public partial class Form1 : Form
    {

        //结构体 IP端口
        struct EndPoint
        {
            public string Ip;
            public string Port;  
        }
        //结构体 通讯状态
        public struct CommState
        {
            /// <summary>
            /// 记录硬件端口连接状态 0-未连接 1-连接
            /// </summary>
            public ushort NetHardCon;
            /// <summary>
            /// 记录socket连接状态 0-未连接 1-连接
            /// </summary>
            public ushort NetSoftCon;
            /// <summary>
            /// 记录串口硬连接状态 0-未连接 1-连接
            /// </summary>
            public ushort COMHardCon;
            /// <summary>
            ///记录串口通讯连接状态 0-未连接 1-连接
            /// </summary>
            public ushort COMSoftCon;                 
        }
        //结构体 电机控制状态
        public class EngineData
        {
            public string Name;                     //主板名称
            public int  SN;                     //串口信息
            public ushort boardIP;              //扩展卡编号
            /// <summary>
            /// 轴号
            /// </summary>
            public ushort Axle;               
            public int Distence;             //距离
            public double StartSpeed;           //起始速度
            public double RunSpeed;             //运行速度
            public double Acceleration;         //加速度
            public double Deceleration;         //减速度
            public int Location;             //当前位置
            public int Targetlocation;       //目标位置
            public int Direction;            //运动方向
            public int Reserve0;             //预留参数0
            public int Reserve1;             //预留参数1
            public int Reserve2;             //预留参数2
            public int Reserve3;             //预留参数3
            public int Reserve4;             //预留参数4

            public ushort StateCodingswitch;    //编码开关状态

            public ushort SetDirection;    //设置运动方向

            //回原点参数
            public int SecondSpeed;          //第二速度
            public int ReturnDirection;            //回原点运动方向
            //运动模式
            public int RunMode;                 //运动模式  0-点对点 1-连续 2-原点
            public int StopRunMode;             //停止运动模式 0-立即停 1-减速停
            //轴IO
            public ushort SignEnLimit;          //极限使能信号
            public ushort SignEnOrigin;         //原点使能信号
            public ushort SignReversalLimit;    //反转极限信号
            public ushort SignReversalOrigin;   //反转原点信号

            /// <summary>
            /// bit0-报警状态
            /// bit1:忙状态
            /// bit2:错误状态
            /// bit3:正极限信号
            /// bit4:原点信号
            /// bit5:负极限信号
            /// bit6:回原点状态中
            /// </summary>
            public int LightState;           //灯光-状态
            //主板或者扩展卡
            public ushort CardID;           //卡号 0-主板 1-扩展S1卡
            //主板-IO控制-输入
            public int MInput;         //主板输入

            //主板-IO控制-输出
            public int MOutput;        //主板输出

            //主板-PWM控制-输入输出控制
            public int PWMStatequery;    //PWM控制输入输出查询码
            public int PWMState;         //PWM控制输入输出状态


            //演示模式
            public ushort ShowMode;         //演示模式
            

            //通讯状态
            public ushort StatePOP;         //通讯状态 0-OK
            public ushort HeartCount;       //心跳计数
            public int CMDID;       //发送命令ID

        }
        /// <summary>
        /// 0：OK-正常   1：BUSY-正忙     2：ERROR-错误    3：Invalide paramters-无效数据
        /// </summary>
        string[] ReturnData = { "OK", "BUSY", "ERROR", "Invalide paramters!" };

        private TcpClient client;                               //实例化客户端;                               //创建一个客户端
        NetworkStream Stream;                                   //创建一个数据流
        Socket socketWatch = null;                              //创建一个socket套接字
        
        public static byte[] data = new byte[1024];             //创建数据读取缓存区
        EndPoint endPoint = new EndPoint();                     //保存IP信息        

        SerialPort com = new SerialPort();
        
        string ReceiBuff = null;

        /// <summary>
        /// 创建一个记录通讯状态的信息区
        /// </summary>
        public CommState commu = new CommState();   
        /// <summary>
        /// 运行数据集
        /// </summary>
        public static EngineData enginedata = new EngineData();
        
        public object syncRoot = new object();
       
      

        /// <summary>
        /// 记录灯光状态
        /// bit0-报警状态
        /// bit1:忙状态
        /// bit2:错误状态
        /// bit3:正极限信号
        /// bit4:原点信号
        /// bit5:负极限信号
        /// bit6:回原点状态中
        /// </summary>
        public static int LightState = 0;   

        public static Semaphore sema = new Semaphore(0, 1);

        YKS2CardNet YKS2net = new YKS2CardNet();
        Thread thread;//线程句柄-处理通讯过程
        DataDeal datadeal = new DataDeal();

        //****************************************** Myself Code ***********************************************
        // ************************* 网络通讯 *************************
        //网络通讯 - 配置IP与端口号
        void NetWorkInit()
        {
            //使用指定的地址族、套接字类型和协议初始化
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            endPoint.Ip = textBox1.Text.Trim();                 //从 textBox1 框中获取IP地址信息
            endPoint.Port = "4000";                             //默认设置端口号为4000
            IPAddress serverIp = IPAddress.Parse(endPoint.Ip);  //服务端IP 将IP字符串转化为IPAddress实例  注：未填写IP地址，此处阻塞

        }
        //事件API - 点击按钮，连接网络
        /// <summary>
        /// 
        /// </summary>
        void NetWorkEventClickToConnect()
        {
            if (endPoint.Ip == string.Empty)     //确保不会误操作导致IP为空
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " IP为空\r\n");
                return;
            }

            if (!YKS2net.NetWorkPing())
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 网口硬连接异常\r\n");
                return;
            }


            if (commu.NetHardCon == 0)
            {
                button1.Text = "已连接";
                timer2.Enabled = true;
                commu.NetHardCon = 1;
                datadeal.SetNetHard(1);
                if (YKS2net.IsExists())
                {
                    commu.NetSoftCon = 1;
                    datadeal.SetNetSoft(1);
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 网口软连接正常" + "\r\n");
                }
                else
                {
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 网口软连接异常" + "\r\n");
                }
            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 网口断开软链接" + "\r\n");
                button1.Text = "未连接";
                timer2.Enabled = false;
                commu.NetSoftCon = 0;
                commu.NetHardCon = 0;
                datadeal.SetNetHard(0);
                datadeal.SetNetSoft(0);
            }
        }

        //检测API - 获取指定IP端口物理状态
        public void NetWorkGetTcpPOPnnections()
        {
            //Ping p = new Ping();
            //PingReply reply = p.Send(this.textBox1.Text);   //进行 ping 连接测试，并返回连接状态
            //if (reply.Status == IPStatus.Success)       //物理链路连接成功
            //{
            //    communicationstate.NetWorkHardConnectstate = 1;
            //}
            //else                                           //物理链路连接失败
            //{
            //    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "网口连接失败" + "\r\n");
            //    button1.Text = "未连接";
            //    communicationstate.NetWorkHardConnectstate = 0;
            //    communicationstate.NetWorkSoftConnectstate = 0;
            //}
        }
        //显示接收数据 - 输出控件
        void NetWorkSendReceived(string sendata)
        {
            YKS2net.NetWorkSendReceived(sendata);
/*
            client = new TcpClient();                               //实例化客户端
            try
            {
                //client.Connect(endPoint.Ip, int.Parse(endPoint.Port));  //端口需与服务端开启的端口一致，否则无法与服务端建立链接    注：未链接服务器此处阻塞
                client.Connect("192.168.1.100", 4000);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("未连接服务器！", "提示", 0, MessageBoxIcon.Exclamation);
                MessageBox.Show(ex.Message);
                return;
            }
            Stream = client.GetStream();
            Stream.ReadTimeout = 100;                               //读取阻塞100ms
            Stream.WriteTimeout = 100;                              //写入阻塞100ms

            if (client != null && sendata.Trim() != string.Empty)                                     //确保客户端实例化成功
            {
                try
                {
                    string sendData = sendata.Trim() + "\r\n";         //获取要发送的数据
                    byte[] buffer = Encoding.UTF8.GetBytes(sendData); //将数据存入缓存
                    Stream.Write(buffer, 0, buffer.Length);

                    //textBox3.AppendText(DateTime.Now.ToString("\r\nyyyy/MM/dd HH:mm:ss fff") + "发送长度 " + buffer.Length + "发送数据：" + " " + sendData);

                    int length = Stream.Read(data, 0, data.Length); //读取要接收的数据
                    string receiveMsg = Encoding.UTF8.GetString(data, 0, length);

                    //NetWorkDealAcceptdata(receiveMsg);
                    Thread.Sleep(1);
                    //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "接收长度 " + length + "接收数据：" + receiveMsg + "\r\n");
                }
                catch
                {
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "通讯异常" + "\r\n");
                }
            }
            client.Close();
*/
        }


        public void NetWorkDealAcceptdata(string data)
        {
            //try
            //{
            //    string[] sArray = Regex.Split(data, "\n", RegexOptions.IgnoreCase);
            //    string source = sArray[0];
            //    string Target = sArray[1];
            //    //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "片切0：" + source + "片切1：" + Target + "\r\n");
            //    if (sArray.Length > 1)
            //    {
            //        if (DealReceiData(sArray[0], sArray[1]) != 0xFF)
            //        {
            //            WindosShowLight();
            //            WindosShowData();

            //            if (sArray.Length > 5)//提取版本号
            //            {
            //                NetIP = sArray[2];
            //                CardName = sArray[3];
            //                UDSV = sArray[4];
            //                SPT = sArray[5];
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "数据异常" + "\r\n");
            //}
        }

        // ************************* 串口通讯 *************************
        //串口通讯 - 配置-端口号、波特率
        public void COMInit()
        {
            
            int[] item = { 9600, 115200 };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            foreach (int a in item)
            {
                comboBox2.Items.Add(a.ToString());
            }
            comboBox2.SelectedItem = comboBox2.Items[1];    //默认为列表第二个变量 - 115200

            string[] ports = SerialPort.GetPortNames();
            if (ports.Length != 0)
            {
                comboBox1.Items.AddRange(ports);
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
            
        }

        //事件API - 点击按钮，连接串口
        public void COMEventClickToConnect(ComboBox Box1, ComboBox Box2, Button Btn1)
        {
            try
            {
                if (!com.IsOpen)    //判断串口是否打开
                {
                    com.PortName = Box1.SelectedItem.ToString();
                    com.BaudRate = Convert.ToInt32(Box2.SelectedItem.ToString());
                    com.Open();
                    com.DataReceived += COM_DataReceived;
                    commu.COMHardCon = 1;

                    com.WriteTimeout = 10;
                    com.ReadTimeout = 10;
                    com.ReceivedBytesThreshold = 1;

                    Btn1.Text = "关闭串口";
                    timer2.Enabled = true;
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "打开串口" + "\r\n");
                }
                else
                {
                    com.Close();
                    com.DataReceived -= COM_DataReceived;
                    commu.COMHardCon = 0;
                    Btn1.Text = "打开串口";
                    timer2.Enabled = false;
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "关闭串口" + "\r\n");
                }
            }
            catch
            {
                MessageBox.Show("串口打开失败！", "提示", 0, MessageBoxIcon.Exclamation);
            }
        }

        //显示接收数据 - 输出控件
        //public void COM_DataReceived(object sender, SerialDataReceivedEventArgs e)   //数据接收事件，读到数据的长度赋值给count，就申请一个byte类型的buff数组，s句柄来读数据
        //{
        //    int len = com.BytesToRead;
        //    byte[] bytes = new byte[len];
        //    com.Read(bytes, 0, len);
        //    string str = System.Text.Encoding.Default.GetString(bytes); //xx="中文";
        //    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "接收数据:" + str + "\r\n");
        //}


        /// <summary>
        /// 数据接收事件（不完善，暂时只能处理20ms间隔下的指令，只能发一组，处理一组）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        string NetIP = null;
        string CardName = null;
        string UDSV = null;
        string SPT = null;

        public void COM_DataReceived(object sender, SerialDataReceivedEventArgs e)   //数据接收事件，读到数据的长度赋值给count，就申请一个byte类型的buff数组，s句柄来读数据
        {
            int len = com.BytesToRead;
            byte[] bytes = new byte[len];
            com.Read(bytes, 0, len);
            string str = System.Text.Encoding.Default.GetString(bytes); //xx="中文";
            ReceiBuff += str;
        }

        //串口接收数据保存处理
        public void COM_DataReceivedSplit()      // "gb2312"  move 0,-1000,100,1000,100,100
        {
            string testzc = null;
            string[] strArray = null;
            do
            {
                if (ReceiBuff != null)
                {
                    strArray = ReceiBuff.Split(new Char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArray.Length > 1 && (DealReceiData(strArray[0], strArray[1]) != 0xFF))
                    {
                        WindosShowLight();
                        WindosShowData();

                        if (strArray.Length > 5)//提取版本号
                        {
                            NetIP = strArray[2];
                            CardName = strArray[3];
                            UDSV = strArray[4];
                            SPT = strArray[5];

                        }
                        testzc = ReceiBuff;
                        ReceiBuff = null;
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (i == 0)
                                continue;
                            else if (i == 1)
                                continue;
                            ReceiBuff += strArray[i] + '\n';
                        }
                    }
                    else if (strArray.Length > 2 && (DealReceiData(strArray[0] + strArray[1], strArray[2]) != 0xFF))
                    {
                        WindosShowLight();
                        WindosShowData();

                        if (strArray.Length > 5)//提取版本号
                        {
                            NetIP = strArray[2];
                            CardName = strArray[3];
                            UDSV = strArray[4];
                            SPT = strArray[5];
                        }
                        testzc = ReceiBuff;
                        ReceiBuff = null;
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (i == 0)
                                continue;
                            else if (i == 1)
                                continue;
                            else if (i == 2)
                                continue;
                            ReceiBuff += strArray[i] + '\n';
                        }
                    }
                    else if (strArray.Length > 2 && (DealReceiData(strArray[0], strArray[1] + strArray[2]) != 0xFF))
                    {
                        WindosShowLight();
                        WindosShowData();

                        if (strArray.Length > 5)//提取版本号
                        {
                            NetIP = strArray[2];
                            CardName = strArray[3];
                            UDSV = strArray[4];
                            SPT = strArray[5];
                        }
                        testzc = ReceiBuff;
                        ReceiBuff = null;
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (i == 0)
                                continue;
                            else if (i == 1)
                                continue;
                            else if (i == 2)
                                continue;
                            ReceiBuff += strArray[i] + '\n';
                        }
                    }
                    else
                    {
                        ReceiBuff = null;
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            if (i == 0)
                                continue;
                            ReceiBuff += strArray[i] + '\n';
                        }
                    }
                }
                else
                {
                    break;
                }
            } while (strArray.Length - 2 > 1);
        }



        /// <summary>
        /// 解析响应数据，解析完成，返回true，否则返回false
        /// </summary>
        /// <param name="Send"></param>
        /// <param name="Recei"></param>
        /// <returns></returns>
        public int DealReceiData(string Send, string Recei)
        {
            int CMDnum = 0;
            string[] SendData = Send.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] ReceiData = Recei.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            CMDnum = CMD.GetInfo_CmdNum(SendData[0]);   //获取指令序号
            //if(CMDnum == 12)    //获取版本信息
                //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "软件版本为：" + Recei + NetIP + CardName + UDSV + SPT + "\r\n");
            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "命令序号：" + CMDnum + "\r\n");

            if (CMDnum != 0xFF)
            {
                int num = CMD.Group_CmdReceivedData(ReceiData[0], CMDnum);
                if(num == 0) enginedata.HeartCount = 0;


                //for (int i = 0; i < ReceiData.Length; i++)
                //{
                //    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "接收数据" + i + ":" + ReceiData[i] + "\r\n");
                //}
                //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "处理结果:" + CMD.CMDStateCN[num] + "\r\n");
                //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "灯光:" + engineData.LightState + "\r\n");

            }

            return CMDnum;
        }
        //串口发送数据 - 输出控件
        public void COM_DataSend(string sendata, string encoding)      // "gb2312"  move 0,-1000,100,1000,100,100
        {
            sema.WaitOne(100);
            Encoding gb = System.Text.Encoding.GetEncoding(encoding);
            string str = sendata.Trim() + "\r\n";
            byte[] bytes = gb.GetBytes(str);
            com.Write(bytes, 0, bytes.Length);
            //textBox3.AppendText(DateTime.Now.ToString("\r\nyyyy/MM/dd HH:mm:ss fff") + "发送数据:" + str);
            sema.Release();
        }




        //********************** 组件配置 *************************************
        //配置运动模式
        public void Tool_Combobox3()   //数据接收事件，读到数据的长度赋值给count，如果是8位（节点内部编程规定好的），就申请一个byte类型的buff数组，s句柄来读数据
        {
            String[] item = { "点到点", "连续", "原点" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                comboBox3.Items.Add(item[i]);
                comboBox3.SelectedIndex = i;    //配置索引序号

            }
            comboBox3.SelectedItem = comboBox3.Items[1];    //默认为列表第二个变量 
        }
        void Tool_ChangeCardIDFont()
        {
            if (enginedata.CardID == 0)
            {
                enginedata.CardID = 1;  //切换到S1卡
                this.label28.Font = new Font("宋体", 9, FontStyle.Regular);
                this.label57.Font = new Font("宋体", 9, FontStyle.Bold);

                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "切换到S1卡：" + enginedata.CardID + "\r\n");

            }
            else
            {
                enginedata.CardID = 0;  //切换到S1卡
                this.label28.Font = new Font("宋体", 9, FontStyle.Bold);
                this.label57.Font = new Font("宋体", 9, FontStyle.Regular);
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "切换到主板：" + enginedata.CardID + "\r\n");
            }
        }
        /// <summary>
        /// 根据轴号改变字体粗体，区分显示，以及记录轴号
        /// </summary>
        void Tool_ChangeAxleFont(ushort num)
        {
            enginedata.Axle = num;
            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 轴号" + num + "\r\n");
            if (enginedata.Axle == 0)
                button5.Font = new Font("宋体", 9, FontStyle.Bold);
            else
                button5.Font = new Font("宋体", 9, FontStyle.Regular);
            if (enginedata.Axle == 1)
                button6.Font = new Font("宋体", 9, FontStyle.Bold);
            else
                button6.Font = new Font("宋体", 9, FontStyle.Regular);
            if (enginedata.Axle == 2)
                button7.Font = new Font("宋体", 9, FontStyle.Bold);
            else
                button7.Font = new Font("宋体", 9, FontStyle.Regular);

        }
        //获取输出口的值
        int GetterValue(int Value, ushort bit)
        {
            return Value >> bit & 1;
        }
        //配置输出口的值
        int SetterValue(int Value, ushort bit, ushort State)
        {
            if (State == 1) { Value &= (1 << bit) ^ (0xFFFF); }
            else { Value |= 1 << bit; }
            return Value;
        }

        /// <summary>
        /// 窗口灯光变化
        /// </summary>
        void WindosShowLight()
        {
            if ((enginedata.LightState >> 1 & 0x1) == 0) label17.BackColor = Color.FromArgb(28, 66, 28);    //灯光-忙
            else label17.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.LightState >> 3 & 0x1) == 0) label13.BackColor = Color.FromArgb(28, 66, 28);    //灯光-正极限
            else label13.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.LightState >> 4 & 0x1) == 0) label15.BackColor = Color.FromArgb(28, 66, 28);    //灯光-原点
            else label15.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.LightState >> 5 & 0x1) == 0) label14.BackColor = Color.FromArgb(28, 66, 28);    //灯光-负极限
            else label14.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.LightState >> 6 & 0x1) == 0) label16.BackColor = Color.FromArgb(28, 66, 28);    //灯光-回原点
            else label16.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.PWMState & 0x15) == 0) label18.BackColor = Color.FromArgb(28, 66, 28);    //灯光-脉冲
            else label18.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.PWMState & 0x2A) == 0) label26.BackColor = Color.FromArgb(28, 66, 28);    //灯光-方向
            else label26.BackColor = Color.FromArgb(44, 255, 44);
            //开关量输出
            if ((enginedata.MOutput >> 0 & 0x1) == 0) button15.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y0
            else button15.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MOutput >> 1 & 0x1) == 0) button17.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y1
            else button17.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MOutput >> 2 & 0x1) == 0) button21.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y2
            else button21.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MOutput >> 3 & 0x1) == 0) button19.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y3
            else button19.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MOutput >> 4 & 0x1) == 0) button23.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y4
            else button23.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MOutput >> 5 & 0x1) == 0) button25.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y5
            else button25.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MOutput >> 6 & 0x1) == 0) button27.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y6
            else button27.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MOutput >> 7 & 0x1) == 0) button29.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y7
            else button29.BackColor = Color.FromArgb(44, 255, 44);
            //开关量输入
            if ((enginedata.MInput >> 0 & 0x1) == 0) label5.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X0
            else label5.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MInput >> 1 & 0x1) == 0) label6.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X1
            else label6.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MInput >> 2 & 0x1) == 0) label7.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X2
            else label7.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MInput >> 3 & 0x1) == 0) label8.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X3
            else label8.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MInput >> 4 & 0x1) == 0) label9.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X4
            else label9.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MInput >> 5 & 0x1) == 0) label10.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X5
            else label10.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MInput >> 6 & 0x1) == 0) label11.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X6
            else label11.BackColor = Color.FromArgb(44, 255, 44);
            if ((enginedata.MInput >> 7 & 0x1) == 0) label12.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X7
            else label12.BackColor = Color.FromArgb(44, 255, 44);


        }



        void StateCheckHeart()
        {
            if(enginedata.HeartCount >10)
            {
                if (com.IsOpen)    //判断串口是否打开
                {
                    com.Close();
                    com.DataReceived -= COM_DataReceived;
                    commu.COMHardCon = 0;
                    button2.Text = "打开串口";
                    timer2.Enabled = false;
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "关闭串口" + "\r\n");
                    enginedata.HeartCount = 0;
                }

                if (commu.NetSoftCon == 1)
                {
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "网口断开链接" + "\r\n");
                    button1.Text = "未连接";
                    commu.NetSoftCon = 0;
                    commu.NetHardCon = 0;
                    enginedata.HeartCount = 0;
                }
            }

        }


        //为窗口2准备的函数接口 SignEnLimit;         //极限使能信号
        public string SetSignEnLimit{set{enginedata.SignEnLimit = ushort.Parse(value);}}
        //为窗口2准备的函数接口 SignEnOrigin;        //原点使能信号
        public string SetSignEnOrigin { set { enginedata.SignEnOrigin = ushort.Parse(value); } }
        //为窗口2准备的函数接口 SignReversalLimit;   //反转极限信号
        public string SetSignReversalLimit { set { enginedata.SignReversalLimit = ushort.Parse(value); } }
        //为窗口2准备的函数接口 SignReversalOrigin;  //反转原点信号
        public string SetSignReversalOrigin { set { enginedata.SignReversalOrigin = ushort.Parse(value); } }
        //为窗口2准备的函数接口 SignReversalOrigin;  //发送极限设置
        public void SetSignSendFirm()
        {
            enginedata.CMDID = (int)CMD.CMDTopNum.setlmt;
        }

        /// <summary>
        /// 处理循环命令以及数据显示
        /// </summary>
        void MsgProcess()
        {
            
            textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "开启线程，处理通讯数据！\r\n");
            while (true)
            {
                Thread.Sleep(30);
            }

        }

        //****************************************** Myself Code ***********************************************
       
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到

            textBox3.Height = 230;                  //数据接收区的高度
            textBox3.Width = 700;                   //数据接收区的宽度
            textBox1.Text = "192.168.1.100";        //设置IP显示默认值

            textBox4.Text = "0";                    //当前位置
            textBox5.Text = "1000";                 //距离
            textBox6.Text = "0.2";                  //加速度
            textBox7.Text = "100";                  //起始速度
            textBox8.Text = "10000";                //运行速度
            textBox9.Text = "1000";                 //第二速度
            textBox10.Text= "0";                    //目标位置

            enginedata.CMDID = (int)CMD.CMDTopNum.H;
            //端口显示控件 波特率显示控件
            COMInit();
            //控件配置-下拉列表3
            Tool_Combobox3();

           
            ThreadStart threadStart = new ThreadStart(datadeal.ThreadMain);//通过ThreadStart委托告诉子线程执行什么方法　　
            thread = new Thread(threadStart);
            thread.Start();//启动新线程      //关闭窗体，自动关闭线程

        }
        //点击事件 - 网口连接状态判断
        private void button1_Click(object sender, EventArgs e)
        {
            //网络通讯 - 配置IP与端口号
            NetWorkInit();
            YKS2net.NetWorkInit(textBox1.Text.Trim(),4000);
            NetWorkEventClickToConnect();
            
            textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "开启线程" + datadeal.GetCMDID() + "\r\n");

        }
        //点击事件 - 串口连接
        private void button2_Click(object sender, EventArgs e)
        {
            COMEventClickToConnect(comboBox1,comboBox2, button2);
        }
        //测试模式配置定时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if(communicationstate.NetWorkHardConnectstate == 1) NetWorkGetTcpPOPnnections();        //若物理连接初次导通，则之后开始检测

        }
        //测试模式是否开启定时器
        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                button3.Text = "开启心跳";
            }
            else
            {
                timer1.Enabled = true;
                timer2.Enabled = true;
                button3.Text = "关闭心跳";
            }
        }

        /// <summary>
        /// 测试模式-点击事件 - 发送网络通讯数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (YKS2net.IsExists())
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "检测到心跳！\r\n");

            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "未检测到心跳！\r\n");
            }


            //if (communicationstate.COMHardConnectstate == 1 && communicationstate.NetWorkHardConnectstate == 0)
            //{
            //COM_DataSend(textBox2.Text.Trim(), "utf-8");
            //}
            //else if (communicationstate.COMHardConnectstate == 0 && communicationstate.NetWorkHardConnectstate == 1)
            //{
            //NetWorkSendReceived(textBox2.Text);           //网络通讯发送接收数据处理
            //}
            //else if (communicationstate.COMHardConnectstate == 1 && communicationstate.NetWorkHardConnectstate == 1)
            //{
            //    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "网口通讯与串口通讯不能同时打开！\r\n");
            //}
            //else
            //{
            //    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "网口通讯与串口通讯未打开！\r\n");
            //}    
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }

        private void label28_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }

        private void label57_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }
        //(ushort Axle, double Distence, double StartSpeed, double RunSpeed, double Acceleration, double Deceleration)
        private void button9_Click(object sender, EventArgs e)
        {
            Form2 IForm = new Form2();
            IForm.Owner = this;
            IForm.Show();
            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 极限使能信号 " + engineData.SignEnLimit + "\r\n");
            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 原点使能信号 " + engineData.SignEnOrigin + "\r\n");
            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 反转极限信号 " + engineData.SignReversalLimit + "\r\n");
            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 反转原点信号 " + engineData.SignReversalOrigin + "\r\n");
            //String str = CMD.ControlCmd_AbsoluteMovement(engineData);
            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + str + "\r\n");

        }

        /// <summary>
        /// 事件- 鼠标离开控件，用于保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1MouseLeaveTool(object sender, EventArgs e)
        {
            enginedata.Location     = int.Parse(textBox4.Text.Trim());          //当前位置
            enginedata.Distence     = int.Parse(textBox5.Text.Trim());          //距离
            enginedata.Acceleration = double.Parse(textBox6.Text.Trim());          //加速度
            enginedata.StartSpeed   = int.Parse(textBox7.Text.Trim());          //起始速度
            enginedata.RunSpeed     = int.Parse(textBox8.Text.Trim());          //运行速度
            enginedata.SecondSpeed  = int.Parse(textBox9.Text.Trim());          //第二速度
            enginedata.Targetlocation = int.Parse(textBox10.Text.Trim());          //第二速度
            if (enginedata.Location>=0) enginedata.ReturnDirection = 0;
            else enginedata.ReturnDirection = 1;
            enginedata.RunMode = comboBox3.SelectedIndex;
        }
        /// <summary>
        /// 选择X轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Tool_ChangeAxleFont(0);
        }
        /// <summary>
        /// 选择Y轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            Tool_ChangeAxleFont(1);
        }

        /// <summary>
        /// 选择Z轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Tool_ChangeAxleFont(2);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }
     


        /// <summary>
        /// 开关量-输出口0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            if((enginedata.MOutput >> 0 & 0x1) ==0) enginedata.MOutput |= 1 << 0;
            else enginedata.MOutput &= (1 << 0) ^ (0xFFFF);

            enginedata.CMDID = (int)CMD.CMDTopNum.setout;
        }

        /// <summary>
        /// 开关量-输出口1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            if ((enginedata.MOutput >> 1 & 0x1) == 0) enginedata.MOutput |= 1 << 1;
            else enginedata.MOutput &= (1 << 1) ^ (0xFFFF);

            enginedata.CMDID = (int)CMD.CMDTopNum.setout;
        }

        /// <summary>
        /// 开关量-输出口2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            if ((enginedata.MOutput >> 2 & 0x1) == 0) enginedata.MOutput |= 1 << 2;
            else enginedata.MOutput &= (1 << 2) ^ (0xFFFF);

            enginedata.CMDID = (int)CMD.CMDTopNum.setout;
        }

        /// <summary>
        /// 开关量-输出口3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            if ((enginedata.MOutput >> 3 & 0x1) == 0) enginedata.MOutput |= 1 << 3;
            else enginedata.MOutput &= (1 << 3) ^ (0xFFFF);

            enginedata.CMDID = (int)CMD.CMDTopNum.setout;
        }

        /// <summary>
        /// 开关量-输出口4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            if ((enginedata.MOutput >> 4 & 0x1) == 0) enginedata.MOutput |= 1 << 4;
            else enginedata.MOutput &= (1 << 4) ^ (0xFFFF);

            enginedata.CMDID = (int)CMD.CMDTopNum.setout;
        }

        /// <summary>
        /// 开关量-输出口5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            if ((enginedata.MOutput >> 5 & 0x1) == 0) enginedata.MOutput |= 1 << 5;
            else enginedata.MOutput &= (1 << 5) ^ (0xFFFF);

            enginedata.CMDID = (int)CMD.CMDTopNum.setout;
        }

        /// <summary>
        /// 开关量-输出口6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            if ((enginedata.MOutput >> 6 & 0x1) == 0) enginedata.MOutput |= 1 << 6;
            else enginedata.MOutput &= (1 << 6) ^ (0xFFFF);

            enginedata.CMDID = (int)CMD.CMDTopNum.setout;
        }

        /// <summary>
        /// 开关量-输出口7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            if ((enginedata.MOutput >> 7 & 0x1) == 0) enginedata.MOutput |= 1 << 7;
            else enginedata.MOutput &= (1 << 7) ^ (0xFFFF);

            enginedata.CMDID = (int)CMD.CMDTopNum.setout;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            enginedata.SetDirection = 0;
            if (enginedata.RunMode == 0)  //点对点
                enginedata.CMDID = (int)CMD.CMDTopNum.moveto;
            else if (enginedata.RunMode == 1)  //连续
                enginedata.CMDID = (int)CMD.CMDTopNum.move;
            else if (enginedata.RunMode == 2)  //原点
                enginedata.CMDID = (int)CMD.CMDTopNum.home;

        }

        private void button11_Click(object sender, EventArgs e)
        {
                NetWorkSendReceived("move 0,-1000,100,1000,100,100");
            enginedata.SetDirection = 1;
            if(enginedata.RunMode==0)  //点对点
                enginedata.CMDID = (int)CMD.CMDTopNum.moveto;
            else if (enginedata.RunMode == 1)  //连续
                enginedata.CMDID = (int)CMD.CMDTopNum.move;
            else if (enginedata.RunMode == 2)  //原点
                enginedata.CMDID = (int)CMD.CMDTopNum.home;

        }

        /// <summary>
        /// 窗口数据变化
        /// </summary>
        void WindosShowData()
        {
            textBox4.Text = enginedata.Location.ToString();
        }
        /// <summary>
        /// 定时器：每10ms发送一次查询命令，查询常规数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        uint TimeState = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            //NetWorkSendReceived("getpos 0");
            //NetWorkSendReceived("move 0,-1000,100,1000,100,100");
            //enginedata.Location = YKS2net.GetPosition(enginedata.Axle);
            YKS2net.RltMove(0,1000,100,1000,100,100);
            textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 获取轴当前位置0：" + enginedata.Location + "\r\n");
            textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 获取轴当前位置1：" +datadeal.GetLocation() +"\r\n");
            textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 获取轴输出状态：" + datadeal.GetMOutput() + "\r\n");
            textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "线程变化值：" + datadeal.Gettimecount() + "\r\n"); 
            textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "心跳变化值：" + datadeal.GetHeartCount() + "\r\n"); 
            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "线程状态：" + thread.IsAlive +"\r\n");

            //WindosShowLight();
            //WindosShowData();


            //string CMDData = null;
            //CMDData = CMD.Group_CmdSendData(enginedata.CMDID);

            //COM_DataReceivedSplit();

            //if (commu.COMHardCon == 1 && commu.NetHardCon == 0)
            //{
            //    TimeState = 0;
            //    COM_DataSend(CMDData, "utf-8");
            //}
            //else if (commu.COMHardCon == 0 && commu.NetHardCon == 1)
            //{
            //    TimeState = 0;
            //    NetWorkSendReceived(CMDData);           //网络通讯发送接收数据处理
            //}
            //else if (TimeState == 0 && commu.COMHardCon == 1 && commu.NetHardCon == 1)
            //{
            //    TimeState++;
            //    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "网口通讯与串口通讯不能同时打开！\r\n");
            //}
            //else if (TimeState > 20 && commu.COMHardCon == 0 && commu.NetHardCon == 0)
            //{
            //    TimeState = 0;
            //    //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "网口通讯与串口通讯未打开！\r\n");
            //}


        }

        private void button12_Click(object sender, EventArgs e)
        {
            enginedata.StopRunMode = 1;
            enginedata.CMDID = (int)CMD.CMDTopNum.stop;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            enginedata.StopRunMode = 0;
            enginedata.CMDID = (int)CMD.CMDTopNum.stop;

            textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "线程变化值："+ datadeal.GetHeartCount() + "\r\n");
         
        }

        private void button14_Click(object sender, EventArgs e)
        {

            if(this.radioButton1.Checked == true)
                enginedata.SetDirection = 0;        //演示模式正方向
            else
                enginedata.SetDirection = 1;        //演示模式负方向

            if (enginedata.ShowMode == 0)
            {
                enginedata.ShowMode = 1;    //开始演示
                button14.Text = "停止";
            }
            else
            {
                enginedata.ShowMode = 0;   //停止演示
                button14.Text = "开始";

            }
                
        }


        private void FormClosing_Click(object sender, FormClosingEventArgs e)
        {
            //网络通讯 - 配置IP与端口号
            MessageBox.Show("This is the first thing I want know!");
            thread.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}

