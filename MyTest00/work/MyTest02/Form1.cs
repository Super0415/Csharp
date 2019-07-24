using System;
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
using Yungku.Common.IOCard;


namespace MyTest00
{

    public partial class Form1 : Form
    {
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
        public struct EngineData
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

        
        public static byte[] data = new byte[1024];             //创建数据读取缓存区       

        SerialPort com = new SerialPort();
        string ReceiBuff = null;

        /// <summary>
        /// 运行数据集
        /// </summary>
        public static EngineData engineData = new EngineData();
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



        //**********
        /// <summary>
        /// 创建一个记录通讯状态的信息区
        /// </summary>
        public CommState commu = new CommState();

        YKS2CardNet YKS2net = new YKS2CardNet();
        DataDeal datadeal = new DataDeal();
        Thread thread = null;//线程句柄-处理通讯过程
        Thread thread2 = null; //创建线程

        YKS2Card YKS2Com = new YKS2Card();
        //***********

        //****************************************** Myself Code ***********************************************
        // ************************* 网络通讯 *************************
        void NetWorkEventToConnect()
        {
            if (YKS2net.IP == string.Empty)     //确保不会误操作导致IP为空
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " IP为空\r\n");
                return;
            }

            if (!YKS2net.NetWorkPing())
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 网口硬连接异常\r\n");
                return;
            }


            if (datadeal.GetNetHard() == 0)
            {
                button1.Text = "已连接";
                FromNature(true);
                datadeal.SetNetHard(1);
                if (YKS2net.IsExists())
                {
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
                if (datadeal.GetCOMHard() == 0)
                {
                    FromNature(false);
                }

                datadeal.SetNetHard(0);
                datadeal.SetNetSoft(0);
            }
        }
 

        //检测API - 获取指定IP端口物理状态
        //public void NetWorkGetTcpPOPnnections()
        //{
        //    Ping p = new Ping();
        //    PingReply reply = p.Send(this.textBox1.Text);   //进行 ping 连接测试，并返回连接状态
        //    if (reply.Status == IPStatus.Success)       //物理链路连接成功
        //    {
        //        commu.NetWorkHardConnectstate = 1;
        //    }
        //    else                                           //物理链路连接失败
        //    {
        //        textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "网口连接失败" + "\r\n");
        //        button1.Text = "未连接";
        //        commu.NetWorkHardConnectstate = 0;
        //        commu.NetWorkSoftConnectstate = 0;
        //    }
        //}



        public void NetWorkDealAcceptdata(string data)
        {
            try
            {
                string[] sArray = Regex.Split(data, "\n", RegexOptions.IgnoreCase);
                string source = sArray[0];
                string Target = sArray[1];
                //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "片切0：" + source + "片切1：" + Target + "\r\n");
                if (sArray.Length > 1)
                {
                    if (DealReceiData(sArray[0], sArray[1]) != 0xFF)
                    {
                        WindosShowLight();
                        WindosShowData();

                        if (sArray.Length > 5)//提取版本号
                        {
                            NetIP = sArray[2];
                            CardName = sArray[3];
                            UDSV = sArray[4];
                            SPT = sArray[5];
                        }
                    }
                }
            }
            catch
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "数据异常" + "\r\n");
            }
        }

        // ************************* 串口通讯 *************************
        //串口通讯 - 配置-端口号、波特率
        public void COMInit()
        {
           CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到
            int[] item = { 9600, 115200 };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                comboBox2.Items.Add(item[i]);  //配置选项
                comboBox2.SelectedIndex = i;    //配置索引序号
            }
            comboBox2.SelectedItem = comboBox2.Items[1];    //默认为列表第二个变量 - 115200

            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                comboBox1.Items.Add(ports[i]);  //配置选项
                comboBox1.SelectedIndex = i;    //配置索引序号
            }
            comboBox1.SelectedItem = comboBox1.Items[0];    //默认为列表第1个变量
        }
        void SerialSendReceived()
        {
            while (true)
            {
                Thread.Sleep(30);
                //心跳检测
                if (!YKS2Com.IsExists())
                {
                    datadeal.SetHeartCount(datadeal.GetHeartCount());
                    if(datadeal.GetHeartCount() > 30) datadeal.SetCOMSoft(0);
                }
                else
                {
                    datadeal.SetCOMSoft(1);
                }

                //获取轴位置
                datadeal.SetLocation(YKS2Com.GetPosition(datadeal.GetAxle()));
                //获取轴IO
                datadeal.SetPWMIOState(YKS2Com.GetAIO());
                //获取主板输入端口值 
                datadeal.SetMInput(YKS2Com.GetInputs());
                //获取主板输出端口值
                datadeal.SetMOutput(YKS2Com.GetOutputs());
                //获取轴状态
                datadeal.SetPWMState(YKS2Com.GetAxisStatus(datadeal.GetAxle()));
                if (datadeal.GetShowMode() == 1)
                {
                    if ((datadeal.GetPWMState() >> 1 & 0x1) == 0)
                    {
                        int Dis = datadeal.GetDirection() > 0 ? datadeal.GetDistence() : -datadeal.GetDistence();
                        YKS2net.RltMove(datadeal.GetAxle(), Dis, datadeal.GetStartSpeed(), datadeal.GetRunSpeed(), datadeal.GetAcceleration(), datadeal.GetDeceleration());
                        if (datadeal.GetDirection() == 0)
                        {
                            datadeal.SetDirection(1);
                        }
                        else
                        {
                            datadeal.SetDirection(0);
                        }
                    }
                }
            }

        } 
        /// <summary>
        /// 配置属性
        /// </summary>
        /// <param name="able"></param>
        public void FromNature(bool able)
        {
            if (able)
            {
                timer2.Enabled = true;
                button9.Enabled = true;  //极限设置
                button10.Enabled = true; //向左键
                button11.Enabled = true; //向右键
                button12.Enabled = true; //减速停
                button13.Enabled = true; //立即停
                button14.Enabled = true; //演习键
            }
            else
            {
                timer2.Enabled = false;
                button9.Enabled = false;  //极限设置
                button10.Enabled = false; //向左键
                button11.Enabled = false; //向右键
                button12.Enabled = false; //减速停
                button13.Enabled = false; //立即停
                button14.Enabled = false; //演习键

                datadeal.SetShowMode(0);
                button14.Text = "开始";
            }

        }
        //事件API - 点击按钮，连接串口 (comboBox1,comboBox2, button2)
        public void COMEventConnect()
        {
            try
            {
                if (datadeal.GetCOMHard() == 0)    //判断串口是否打开
                {

                    YKS2Com.Port = (int)(comboBox1.SelectedIndex);
                    YKS2Com.BaudRate = Convert.ToInt32(comboBox2.SelectedItem.ToString());
                    YKS2Com.Timeout = 1000;
                    YKS2Com.Open();

                    button2.Text = "关闭串口";
                    FromNature(true);
                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "打开串口" + "\r\n");
                    datadeal.SetCOMHard(1);

                    thread2 = new Thread(SerialSendReceived);
                    thread2.Start();
                }
                else
                {
                    if (datadeal.GetNetHard() == 0)
                    {
                        FromNature(false);
                    }
                    YKS2Com.Close();
                    button2.Text = "打开串口";

                    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "关闭串口" + "\r\n");
                    datadeal.SetCOMHard(0);
                    thread2.Abort();

                }
            }
            catch
            {
                YKS2Com.Close();
                MessageBox.Show("串口打开失败！", "提示", 0, MessageBoxIcon.Exclamation);
            }
        }
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
                if(num == 0) engineData.HeartCount = 0;


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
            if (engineData.CardID == 0)
            {
                datadeal.SetCardID(1);  //切换到S1卡
                this.label28.Font = new Font("宋体", 9, FontStyle.Regular);
                this.label57.Font = new Font("宋体", 9, FontStyle.Bold);

                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "切换到S1卡：" + engineData.CardID + "\r\n");

            }
            else
            {
                datadeal.SetCardID(0);  //切换到S1卡
                this.label28.Font = new Font("宋体", 9, FontStyle.Bold);
                this.label57.Font = new Font("宋体", 9, FontStyle.Regular);
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "切换到主板：" + engineData.CardID + "\r\n");
            }
        }
        /// <summary>
        /// 根据轴号改变字体粗体，区分显示，以及记录轴号
        /// </summary>
        void Tool_ChangeAxleFont(ushort num)
        {
            datadeal.SetAxle(num);
            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 轴号" + num + "\r\n");
            if (engineData.Axle == 0)
                button5.Font = new Font("宋体", 9, FontStyle.Bold);
            else
                button5.Font = new Font("宋体", 9, FontStyle.Regular);
            if (engineData.Axle == 1)
                button6.Font = new Font("宋体", 9, FontStyle.Bold);
            else
                button6.Font = new Font("宋体", 9, FontStyle.Regular);
            if (engineData.Axle == 2)
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


        void StateCheckHeart()
        {
            //if(engineData.HeartCount >10)
            //{
            //    if (com.IsOpen)    //判断串口是否打开
            //    {
            //        com.Close();
            //        com.DataReceived -= COM_DataReceived;
            //        commu.COMHardCon = 0; 
            //        button2.Text = "打开串口";
            //        timer2.Enabled = false;
            //        textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "关闭串口" + "\r\n");
            //        engineData.HeartCount = 0;
            //    }

            //    if (commu.NetWorkSoftConnectstate == 1)
            //    {
            //        textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "网口断开链接" + "\r\n");
            //        button1.Text = "未连接";
            //        commu.NetWorkSoftConnectstate = 0;
            //        commu.NetWorkHardConnectstate = 0;
            //        engineData.HeartCount = 0;
            //    }
            //}

        }


        //为窗口2准备的函数接口 SignEnLimit;         //极限使能信号
        //public string SetSignEnLimit{set{datadeal.SignEnLimit = ushort.Parse(value);}}
        public int SetSignEnLimit { set { datadeal.SetSignEnLimit(value); } }
        //为窗口2准备的函数接口 SignEnOrigin;        //原点使能信号
        //public string SetSignEnOrigin { set { engineData.SignEnOrigin = ushort.Parse(value); } }
        public int SetSignEnOrigin { set { datadeal.SetSignEnOrigin(value); } }
        //为窗口2准备的函数接口 SignReversalLimit;   //反转极限信号
        //public string SetSignReversalLimit { set { engineData.SignReversalLimit = ushort.Parse(value); } }
        public int SetSignReversalLimit { set { datadeal.SetSignReLimit(value); } }
        //为窗口2准备的函数接口 SignReversalOrigin;  //反转原点信号
        //public string SetSignReversalOrigin { set { engineData.SignReversalOrigin = ushort.Parse(value); } }
        public int SetSignReversalOrigin { set { datadeal.SetSignReOrigin(value); } }
        //为窗口2准备的函数接口 SignReversalOrigin;  //发送极限设置
        public void SetSignSendFirm()
        {
            YKS2net.SetLimits(datadeal.GetAxle(), datadeal.GetSignEnLimit(), datadeal.GetSignEnOrigin(), datadeal.GetSignReLimit(), datadeal.GetSignReOrigin());
        }
        //****************************************** Myself Code ***********************************************


        /// <summary>
        /// 窗体程序 - OK
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            ThreadStart threadStart = new ThreadStart(datadeal.ThreadMain);//通过ThreadStart委托告诉子线程执行什么方法　　
            thread = new Thread(threadStart);
            thread.Start();//启动新线程      //关闭窗体，自动关闭线程


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

            //engineData.CMDID = (int)CMD.CMDTopNum.H;
            //端口显示控件 波特率显示控件
            COMInit();
            //控件配置-下拉列表3
            Tool_Combobox3();
            FromNature(false);

        }
        //点击事件 - 网口连接状态判断
        private void button1_Click(object sender, EventArgs e)
        {
            //网络通讯 - 配置IP与端口号
            YKS2net.NetWorkInit(textBox1.Text.Trim(), 4000);
            NetWorkEventToConnect();
        }
        //点击事件 - 串口连接
        private void button2_Click(object sender, EventArgs e)
        {
            COMEventConnect();
        }
        //测试模式配置定时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if(commu.NetWorkHardConnectstate == 1) NetWorkGetTcpPOPnnections();        //若物理连接初次导通，则之后开始检测

        }

        /// <summary>
        /// 主板/扩展板切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }
        /// <summary>
        /// 主板/扩展板切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label28_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }
        /// <summary>
        /// 主板/扩展板切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label57_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }
        /// <summary>
        /// 极限设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            Form2 IForm = new Form2();
            IForm.Owner = this;
            IForm.Show();
        }

        /// <summary>
        /// 事件- 鼠标离开控件，用于保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1MouseLeaveTool(object sender, EventArgs e)
        {
            datadeal.SetDistence(int.Parse(textBox5.Text.Trim()));              //距离
            datadeal.SetTargetlocation(int.Parse(textBox10.Text.Trim()));              //目标位置
            datadeal.SetStartSpeed(int.Parse(textBox7.Text.Trim()));              //起始速度
            datadeal.SetRunSpeed(int.Parse(textBox8.Text.Trim()));              //运行速度
            datadeal.SetSecondSpeed(int.Parse(textBox9.Text.Trim()));              //第二速度
            datadeal.SetAcceleration(double.Parse(textBox6.Text.Trim()));              //加速度

            if (datadeal.GetLocation() >= 0) datadeal.SetReturnDirection(0);    //设置回原点方向
            else datadeal.SetReturnDirection(1);

            datadeal.SetRunMode(comboBox3.SelectedIndex);                       //设置运动模式
        }
        /// <summary>
        /// 选择X轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Tool_ChangeAxleFont(0);
            datadeal.SetAxle(0);
        }
        /// <summary>
        /// 选择Y轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            Tool_ChangeAxleFont(1);
            datadeal.SetAxle(1);
        }

        /// <summary>
        /// 选择Z轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Tool_ChangeAxleFont(2);
            datadeal.SetAxle(2);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// 配置开关量输出
        /// </summary>
        /// <param name="outnum"></param>
        private void Config_Out(int outnum)
        {
            int MOutput = datadeal.GetMOutput();

            if ((MOutput >> outnum & 0x1) == 0) MOutput |= 1 << outnum;
            else MOutput &= (1 << outnum) ^ (0xFFFF);

            YKS2net.SetOutputs((byte)MOutput);
        }
        /// <summary>
        /// 开关量-输出口0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            Config_Out(0);
        }
        /// <summary>
        /// 开关量-输出口1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            Config_Out(1);
        }

        /// <summary>
        /// 开关量-输出口2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            Config_Out(2);
        }

        /// <summary>
        /// 开关量-输出口3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            Config_Out(3);
        }

        /// <summary>
        /// 开关量-输出口4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            Config_Out(4);
        }

        /// <summary>
        /// 开关量-输出口5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            Config_Out(5);
        }

        /// <summary>
        /// 开关量-输出口6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            Config_Out(6);
        }

        /// <summary>
        /// 开关量-输出口7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            Config_Out(7);
        }

        /// <summary>
        /// 向左运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            datadeal.SetDirection(0);

            int axisNo = datadeal.GetAxle();
            int pos = datadeal.GetTargetlocation();
            int startVel = datadeal.GetStartSpeed();
            int vel = datadeal.GetRunSpeed();
            double acc = datadeal.GetAcceleration();
            double dec = datadeal.GetDeceleration();
            int dist = datadeal.GetDirection() > 0 ? datadeal.GetDistence() : (-datadeal.GetDistence());
            int homeDir = datadeal.GetReturnDirection();
            int homeSVel = datadeal.GetSecondSpeed();
            int homeMode = 0;   //预留参数
            int offset = 0;     //预留参数
            if ((datadeal.GetNetSoft() == 1) && !YKS2net.IsBusy(axisNo))
            {
                if (datadeal.GetRunMode() == 0)  //点对点
                    YKS2net.AbsMove(axisNo, pos, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 1)  //连续
                    YKS2net.RltMove(axisNo, dist, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 2)  //原点
                    YKS2net.Home(axisNo, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else if ((datadeal.GetCOMSoft() == 1) && !YKS2Com.IsBusy(axisNo))
            {
                if (datadeal.GetRunMode() == 0)  //点对点
                    YKS2Com.AbsMove(axisNo, pos, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 1)  //连续
                    YKS2Com.RltMove(axisNo, dist, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 2)  //原点
                    YKS2Com.Home(axisNo, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 执行错误！\r\n");
            }

        }

        /// <summary>
        /// 向右运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            datadeal.SetDirection(1);

            int axisNo = datadeal.GetAxle();
            int pos = datadeal.GetTargetlocation();
            int startVel = datadeal.GetStartSpeed();
            int vel = datadeal.GetRunSpeed();
            double acc = datadeal.GetAcceleration();
            double dec = datadeal.GetDeceleration();
            int dist = datadeal.GetDistence();
            int homeDir = datadeal.GetReturnDirection();
            int homeSVel = datadeal.GetSecondSpeed();
            int homeMode = 0;   //预留参数
            int offset = 0;     //预留参数
            if (!YKS2net.IsBusy(axisNo))
            {
                if (datadeal.GetRunMode() == 0)  //点对点
                    YKS2net.AbsMove(axisNo, pos, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 1)  //连续
                    YKS2net.RltMove(axisNo, dist, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 2)  //原点
                    YKS2net.Home(axisNo, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else if ((datadeal.GetCOMSoft() == 1) && !YKS2Com.IsBusy(axisNo))
            {
                if (datadeal.GetRunMode() == 0)  //点对点
                    YKS2Com.AbsMove(axisNo, pos, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 1)  //连续
                    YKS2Com.RltMove(axisNo, dist, startVel, vel, acc, dec);
                else if (datadeal.GetRunMode() == 2)  //原点
                    YKS2Com.Home(axisNo, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " 执行错误！\r\n");
            }

        }

        /// <summary>
        /// 窗口灯光变化
        /// </summary>
        void WindosShowLight()
        {
            int LightState = datadeal.GetPWMState();
            int LightIO = datadeal.GetPWMIOState();
            int MInput = datadeal.GetMInput();
            int MOutput = datadeal.GetMOutput();
            if ((LightState >> 1 & 0x1) == 0) label17.BackColor = Color.FromArgb(28, 66, 28);    //灯光-忙
            else label17.BackColor = Color.FromArgb(44, 255, 44);
            if ((LightState >> 3 & 0x1) == 0) label13.BackColor = Color.FromArgb(28, 66, 28);    //灯光-正极限
            else label13.BackColor = Color.FromArgb(44, 255, 44);
            if ((LightState >> 4 & 0x1) == 0) label15.BackColor = Color.FromArgb(28, 66, 28);    //灯光-原点
            else label15.BackColor = Color.FromArgb(44, 255, 44);
            if ((LightState >> 5 & 0x1) == 0) label14.BackColor = Color.FromArgb(28, 66, 28);    //灯光-负极限
            else label14.BackColor = Color.FromArgb(44, 255, 44);
            if ((LightState >> 6 & 0x1) == 0) label16.BackColor = Color.FromArgb(28, 66, 28);    //灯光-回原点
            else label16.BackColor = Color.FromArgb(44, 255, 44);
            if ((LightIO & 0x15) == 0) label18.BackColor = Color.FromArgb(28, 66, 28);    //灯光-脉冲
            else label18.BackColor = Color.FromArgb(44, 255, 44);
            if ((LightIO & 0x2A) == 0) label26.BackColor = Color.FromArgb(28, 66, 28);    //灯光-方向
            else label26.BackColor = Color.FromArgb(44, 255, 44);
            //开关量输出
            if ((MOutput >> 0 & 0x1) == 0) button15.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y0
            else button15.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 1 & 0x1) == 0) button17.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y1
            else button17.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 2 & 0x1) == 0) button21.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y2
            else button21.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 3 & 0x1) == 0) button19.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y3
            else button19.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 4 & 0x1) == 0) button23.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y4
            else button23.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 5 & 0x1) == 0) button25.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y5
            else button25.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 6 & 0x1) == 0) button27.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y6
            else button27.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 7 & 0x1) == 0) button29.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y7
            else button29.BackColor = Color.FromArgb(44, 255, 44);
            //开关量输入
            if ((MInput >> 0 & 0x1) == 0) label5.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X0
            else label5.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 1 & 0x1) == 0) label6.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X1
            else label6.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 2 & 0x1) == 0) label7.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X2
            else label7.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 3 & 0x1) == 0) label8.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X3
            else label8.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 4 & 0x1) == 0) label9.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X4
            else label9.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 5 & 0x1) == 0) label10.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X5
            else label10.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 6 & 0x1) == 0) label11.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X6
            else label11.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 7 & 0x1) == 0) label12.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X7
            else label12.BackColor = Color.FromArgb(44, 255, 44);
        }
        /// <summary>
        /// 窗口数据变化
        /// </summary>
        void WindosShowData()
        {
            textBox4.Text = datadeal.GetLocation().ToString();
        }
        /// <summary>
        /// 定时器：每10ms发送一次查询命令，查询常规数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        uint TimeState = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            //datadeal.SetLocation(YKS2Com.GetPosition(datadeal.GetAxle()));
  
            //if (!YKS2Com.IsExists())
            //{
            //    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "心跳断开！\r\n");
            //}
            //else
            //{
            //    textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + "心跳开启！\r\n");
            //}

            //textBox3.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + " " + " 串口计数：" + datadeal.GetHeartCount() + "\r\n");
            
            WindosShowLight();
            WindosShowData();
        }

        /// <summary>
        /// 减速停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            datadeal.SetStopRunMode(1);
            int axisNo = datadeal.GetAxle();

            if ((datadeal.GetNetSoft() == 1) && !YKS2net.IsBusy(axisNo))
            {
                YKS2net.Stop(axisNo);
            }
            else if ((datadeal.GetCOMSoft() == 1) && !YKS2Com.IsBusy(axisNo))
            {
                YKS2Com.Stop(axisNo);
            }
        }

        /// <summary>
        /// 立即停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            datadeal.SetStopRunMode(0);
            int axisNo = datadeal.GetAxle();
            if ((datadeal.GetNetSoft() == 1) && !YKS2net.IsBusy(axisNo))
            {
                YKS2net.Stop(axisNo);
            }
            else if ((datadeal.GetCOMSoft() == 1) && !YKS2Com.IsBusy(axisNo))
            {
                YKS2Com.Stop(axisNo);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {

            if(this.radioButton1.Checked == true)
                datadeal.SetDirection(1);        //演示模式正方向
            else
                datadeal.SetDirection(0);        //演示模式负方向

            if (datadeal.GetShowMode() == 0)
            {
                datadeal.SetShowMode(1);    //开始演示
                button14.Text = "停止";
            }
            else
            {
                datadeal.SetShowMode(0);   //停止演示
                button14.Text = "开始";
            }
                
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //退出线程
            thread.Abort();
            thread2.Abort();
        }
    }
}


