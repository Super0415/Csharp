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
        struct CommunicationState
        {
            public ushort NetWorkHardConnectstate;              //记录硬件端口连接状态
            public ushort NetWorkSoftConnectstate;              //记录socket连接状态
            public ushort COMHardConnectstate;                  //记录硬件串口连接状态
            //public UInt16 COMSoftConnectstate;                  //记录串口通讯连接状态
        }
        //结构体 电机控制状态
        struct EngineData
        {
            public int Axle;                    //轴号
            public double Distence;             //距离
            public double StartSpeed;           //起始速度
            public double RunSpeed;             //运行速度
            public double Acceleration;         //加速度
            public double Location;             //当前位置
            //回原点参数
            public double SecondSpeed;          //第二速度
            //运动模式
            public int RunMode;                 //运动模式
            //轴IO
            public ushort SignEnLimit;          //极限使能信号
            public ushort SignEnOrigin;         //原点使能信号
            public ushort SignReversalLimit;    //反转极限信号
            public ushort SignReversalOrigin;   //反转原点信号
            //主板或者扩展卡
            public ushort CardID;           //卡号 0-主板 1-扩展S1卡
            //主板-IO控制-输入
            public ushort MInput00;         //主板输入X0
            public ushort MInput01;         //主板输入X1
            public ushort MInput02;         //主板输入X2
            public ushort MInput03;         //主板输入X3
            public ushort MInput04;         //主板输入X4
            public ushort MInput05;         //主板输入X5
            public ushort MInput06;         //主板输入X6
            public ushort MInput07;         //主板输入X7
            //主板-IO控制-输出
            public ushort MOutput00;        //主板输出Y0
            public ushort MOutput01;        //主板输出Y1
            public ushort MOutput02;        //主板输出Y2
            public ushort MOutput03;        //主板输出Y3
            public ushort MOutput04;        //主板输出Y4
            public ushort MOutput05;        //主板输出Y5
            public ushort MOutput06;        //主板输出Y6
            public ushort MOutput07;        //主板输出Y7
            //演示模式
            public ushort ShowMode;         //演示模式


        }

        private TcpClient client;                               //创建一个客户端
        NetworkStream Stream;                                   //创建一个数据流
        Socket socketWatch = null;                              //创建一个socket套接字
        
        public static byte[] data = new byte[1024];             //创建数据读取缓存区
        EndPoint endPoint = new EndPoint();                     //保存IP信息        

        SerialPort com = new SerialPort();
        CommunicationState communicationstate = new CommunicationState();   //创建一个记录通讯状态的信息区

        EngineData engineData = new EngineData();

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
        void NetWorkEventClickToConnect()
        {
            if (endPoint.Ip == string.Empty)     //确保不会误操作导致IP为空
            {
                textBox3.AppendText(DateTime.Now.ToString() + " ");
                textBox3.AppendText("IP为空" + "\r\n");
                return;
            }
            if (communicationstate.NetWorkHardConnectstate == 0) NetWorkGetTcpPOPnnections();               //确保物理层线路连接正常
            if (communicationstate.NetWorkHardConnectstate != 0)
            {
                if (communicationstate.NetWorkSoftConnectstate == 0)
                {
                    textBox3.AppendText(DateTime.Now.ToString() + "网口打开链接" + "\r\n");
                    button1.Text = "已连接";
                    communicationstate.NetWorkSoftConnectstate = 1;
                    communicationstate.NetWorkHardConnectstate = 1;
                }
                else
                {
                    textBox3.AppendText(DateTime.Now.ToString() + "网口断开链接" + "\r\n");
                    button1.Text = "未连接";
                    communicationstate.NetWorkSoftConnectstate = 0;
                    communicationstate.NetWorkHardConnectstate = 0;
                }
            }
            else
            {
                communicationstate.NetWorkHardConnectstate = 0;
                communicationstate.NetWorkSoftConnectstate = 0;
            }
        }

        //检测API - 获取指定IP端口物理状态
        public void NetWorkGetTcpPOPnnections()
        {
            Ping p = new Ping();
            PingReply reply = p.Send(this.textBox1.Text);   //进行 ping 连接测试，并返回连接状态
            if (reply.Status == IPStatus.Success)       //物理链路连接成功
            {
                communicationstate.NetWorkHardConnectstate = 1;
            }
            else                                           //物理链路连接失败
            {
                textBox3.AppendText(DateTime.Now.ToString() + "网口连接失败" + "\r\n");
                button1.Text = "未连接";
                communicationstate.NetWorkHardConnectstate = 0;
                communicationstate.NetWorkSoftConnectstate = 0;
            }
        }
        //显示接收数据 - 输出控件
        void NetWorkSendReceived(String sendata)
        {
            client = new TcpClient();                               //实例化客户端
            try
            {
                client.Connect(endPoint.Ip, int.Parse(endPoint.Port));  //端口需与服务端开启的端口一致，否则无法与服务端建立链接    注：未链接服务器此处阻塞
            }
            catch
            {
                MessageBox.Show("未连接服务器！", "提示", 0, MessageBoxIcon.Exclamation);
                return;
            }
            Stream = client.GetStream();
            Stream.ReadTimeout = 100;                               //读取阻塞100ms
            Stream.WriteTimeout = 100;                              //写入阻塞100ms

            if (client != null && sendata.Trim() != string.Empty)                                     //确保客户端实例化成功
            {
                try
                {
                    string sendData = sendata.Trim() + "\n";         //获取要发送的数据
                    byte[] buffer = Encoding.UTF8.GetBytes(sendData); //将数据存入缓存
                    Stream.Write(buffer, 0, buffer.Length);

                    textBox3.AppendText(DateTime.Now.ToString() + "发送长度 " + buffer.Length + "发送数据：" + " " + sendData + "\r\n");

                    int length = Stream.Read(data, 0, data.Length); //读取要接收的数据
                    string receiveMsg = Encoding.UTF8.GetString(data, 0, length);

                    textBox3.AppendText(DateTime.Now.ToString() + "接收长度 " + length + "接收数据：" + receiveMsg + "\r\n");
                }
                catch
                {
                    textBox3.AppendText(DateTime.Now.ToString() + " " + "通讯异常" + "\r\n");
                }
            }
            client.Close();
        }

        // ************************* 串口通讯 *************************
        //串口通讯 - 配置-端口号、波特率
        public void COMInit()
        {
            Control.CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到
            int[] item = { 9600, 115200 };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            foreach (int a in item)
            {
                comboBox2.Items.Add(a.ToString());
            }
            comboBox2.SelectedItem = comboBox2.Items[1];    //默认为列表第二个变量 - 115200

            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            comboBox1.SelectedItem = comboBox1.Items[0];
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
                    communicationstate.COMHardConnectstate = 1;
                    Btn1.Text = "关闭串口";
                    textBox3.AppendText(DateTime.Now.ToString() + " " + "打开串口" + "\r\n");
                }
                else
                {
                    com.Close();
                    com.DataReceived -= COM_DataReceived;
                    communicationstate.COMHardConnectstate = 0;
                    Btn1.Text = "打开串口";
                    textBox3.AppendText(DateTime.Now.ToString() + " " + "关闭串口" + "\r\n");
                }
            }
            catch
            {
                MessageBox.Show("串口被占用！", "提示", 0, MessageBoxIcon.Exclamation);
            }
        }

        //显示接收数据 - 输出控件
        public void COM_DataReceived(object sender, SerialDataReceivedEventArgs e)   //数据接收事件，读到数据的长度赋值给count，就申请一个byte类型的buff数组，s句柄来读数据
        {

            int len = com.BytesToRead;
            byte[] bytes = new byte[len];
            com.Read(bytes, 0, len);
            string str = System.Text.Encoding.Default.GetString(bytes); //xx="中文";
            textBox3.AppendText(DateTime.Now.ToString() + "接收数据:" + str + "\r\n");
        }
        //串口发送数据 - 输出控件
        public void COM_DataSend(String sendata, String encoding)      // "gb2312"  move 0,-1000,100,1000,100,100
        {
            Encoding gb = System.Text.Encoding.GetEncoding(encoding);
            String str = sendata.Trim() + "\r\n";
            byte[] bytes = gb.GetBytes(str);
            com.Write(bytes, 0, bytes.Length);
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
                engineData.CardID = 1;  //切换到S1卡
                this.label28.Font = new Font("宋体", 9, FontStyle.Regular);
                this.label57.Font = new Font("宋体", 9, FontStyle.Bold);

                textBox3.AppendText(DateTime.Now.ToString() + " " + "切换到S1卡：" + engineData.CardID + "\r\n");

            }
            else
            {
                engineData.CardID = 0;  //切换到S1卡
                this.label28.Font = new Font("宋体", 9, FontStyle.Bold);
                this.label57.Font = new Font("宋体", 9, FontStyle.Regular);
                textBox3.AppendText(DateTime.Now.ToString() + " " + "切换到主板：" + engineData.CardID + "\r\n");
            }
        }
        /// <summary>
        /// 根据轴号改变字体粗体，区分显示，以及记录轴号
        /// </summary>
        void Tool_ChangeAxleFont(int num)
        {
            engineData.Axle = num;
            textBox3.AppendText(DateTime.Now.ToString() + " " + num + "\r\n");
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

            //TESTZC
            textBox3.AppendText(DateTime.Now.ToString() + " " + " 运动模式 " + engineData.RunMode + "\r\n"); 
            textBox3.AppendText(DateTime.Now.ToString() + " " + " 演示模式 " + engineData.ShowMode + "\r\n");
        }

        //为窗口2准备的函数接口 SignEnLimit;         //极限使能信号
        public string SetSignEnLimit{set{engineData.SignEnLimit = ushort.Parse(value);}}
        //为窗口2准备的函数接口 SignEnOrigin;        //原点使能信号
        public string SetSignEnOrigin { set { engineData.SignEnOrigin = ushort.Parse(value); } }
        //为窗口2准备的函数接口 SignReversalLimit;   //反转极限信号
        public string SetSignReversalLimit { set { engineData.SignReversalLimit = ushort.Parse(value); } }
        //为窗口2准备的函数接口 SignReversalOrigin;  //反转原点信号
        public string SetSignReversalOrigin { set { engineData.SignReversalOrigin = ushort.Parse(value); } }
        //****************************************** Myself Code ***********************************************


        public Form1()
        {
            InitializeComponent();

            textBox3.Height = 230;                  //数据接收区的高度
            textBox3.Width = 700;                   //数据接收区的宽度
            textBox1.Text = "192.168.1.100";        //设置IP显示默认值

            textBox4.Text = "0";                    //当前位置
            textBox5.Text = "1000";                 //距离
            textBox6.Text = "0.2";                  //加速度
            textBox7.Text = "100";                  //起始速度
            textBox8.Text = "10000";                //运行速度
            textBox9.Text = "1000";                 //第二速度


            //端口显示控件 波特率显示控件
            COMInit();
            //控件配置-下拉列表3
            Tool_Combobox3();

        }
        //点击事件 - 网口连接状态判断
        private void button1_Click(object sender, EventArgs e)
        {
            //网络通讯 - 配置IP与端口号
            NetWorkInit();
            NetWorkEventClickToConnect();
        }
        //点击事件 - 串口连接
        private void button2_Click(object sender, EventArgs e)
        {
            COMEventClickToConnect(comboBox1,comboBox2, button2);
        }
        //测试模式配置定时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(communicationstate.NetWorkHardConnectstate == 1) NetWorkGetTcpPOPnnections();        //若物理连接初次导通，则之后开始检测

        }
        //测试模式是否开启定时器
        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) timer1.Enabled = false;
            else timer1.Enabled = true;
        }

        /// <summary>
        /// 测试模式-点击事件 - 发送网络通讯数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (communicationstate.COMHardConnectstate == 1 && communicationstate.NetWorkHardConnectstate == 0)
            {
                Encoding gb = System.Text.Encoding.GetEncoding("gb2312");
                String str = textBox2.Text.Trim() + "\r\n";
                byte[] bytes = gb.GetBytes(str);
                com.Write(bytes, 0, bytes.Length);
            }
            else if (communicationstate.COMHardConnectstate == 0 && communicationstate.NetWorkHardConnectstate == 1)
            {
                NetWorkSendReceived(textBox2.Text);           //网络通讯发送接收数据处理
            }
            else if (communicationstate.COMHardConnectstate == 1 && communicationstate.NetWorkHardConnectstate == 1)
            {
                textBox3.AppendText(DateTime.Now.ToString() + " " + "网口通讯与串口通讯不能同时打开！\r\n");
            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString() + " " + "网口通讯与串口通讯未打开！\r\n");
            }    
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

        private void button9_Click(object sender, EventArgs e)
        {
            Form2 IForm = new Form2();
            IForm.Owner = this;
            IForm.Show();
            textBox3.AppendText(DateTime.Now.ToString() + " 极限使能信号 " + engineData.SignEnLimit + "\r\n");
            textBox3.AppendText(DateTime.Now.ToString() + " 原点使能信号 " + engineData.SignEnOrigin + "\r\n");
            textBox3.AppendText(DateTime.Now.ToString() + " 反转极限信号 " + engineData.SignReversalLimit + "\r\n");
            textBox3.AppendText(DateTime.Now.ToString() + " 反转原点信号 " + engineData.SignReversalOrigin + "\r\n");
        }

        /// <summary>
        /// 事件- 鼠标离开控件，用于保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1MouseLeaveTool(object sender, EventArgs e)
        {
            //textBox3.AppendText(DateTime.Now.ToString() + " " + "离开控件\r\n");
          
            engineData.Location     = double.Parse(textBox4.Text.Trim());          //当前位置
            engineData.Distence     = double.Parse(textBox5.Text.Trim());          //距离
            engineData.Acceleration = double.Parse(textBox6.Text.Trim());          //加速度
            engineData.StartSpeed   = double.Parse(textBox7.Text.Trim());          //起始速度
            engineData.RunSpeed     = double.Parse(textBox8.Text.Trim());          //运行速度
            engineData.SecondSpeed  = double.Parse(textBox9.Text.Trim());          //第二速度

            engineData.RunMode = comboBox3.SelectedIndex;

            

            //for(int i = 0;i< str.Length;i++)
            //{
            //    if (char.IsNumber())
            //    {

            //    }

            //}
            ////string s = "123.123";
            //if (Regex.IsMatch(textBox6.Text.Trim(), @"^\d+\.\d+$"))
            //{
            //    textBox3.AppendText(DateTime.Now.ToString() + " " + "浮点数\r\n");
            //}
            //else
            //    textBox3.AppendText(DateTime.Now.ToString() + " " + "不是浮点数\r\n");
        }
        /// <summary>
        /// X轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Tool_ChangeAxleFont(0);
        }
        /// <summary>
        /// Y轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            Tool_ChangeAxleFont(1);
        }

        /// <summary>
        /// Z轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Tool_ChangeAxleFont(2);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            engineData.ShowMode = 0;        //演示模式正方向
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            engineData.ShowMode = 1;        //演示模式反方向
        }

        /// <summary>
        /// 开关量-输出口0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            if (engineData.MOutput00 == 0) engineData.MOutput00 = 1;
            else engineData.MOutput00 = 0;

            if (engineData.MOutput00 == 0) button15.BackColor = Color.FromArgb(28,66,28);
            else button15.BackColor = Color.FromArgb(44,255,44);
        }

        /// <summary>
        /// 开关量-输出口1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            if (engineData.MOutput01 == 0) engineData.MOutput01 = 1;
            else engineData.MOutput01 = 0;

            if (engineData.MOutput01 == 0) button17.BackColor = Color.FromArgb(28, 66, 28);
            else button17.BackColor = Color.FromArgb(44, 255, 44);
        }

        /// <summary>
        /// 开关量-输出口2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            if (engineData.MOutput02 == 0) engineData.MOutput02 = 1;
            else engineData.MOutput02 = 0;

            if (engineData.MOutput02 == 0) button21.BackColor = Color.FromArgb(28, 66, 28);
            else button21.BackColor = Color.FromArgb(44, 255, 44);
        }

        /// <summary>
        /// 开关量-输出口3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            if (engineData.MOutput03 == 0) engineData.MOutput03 = 1;
            else engineData.MOutput03 = 0;

            if (engineData.MOutput03 == 0) button19.BackColor = Color.FromArgb(28, 66, 28);
            else button19.BackColor = Color.FromArgb(44, 255, 44);
        }

        /// <summary>
        /// 开关量-输出口4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            if (engineData.MOutput04 == 0) engineData.MOutput04 = 1;
            else engineData.MOutput04 = 0;

            if (engineData.MOutput04 == 0) button23.BackColor = Color.FromArgb(28, 66, 28);
            else button23.BackColor = Color.FromArgb(44, 255, 44);
        }

        /// <summary>
        /// 开关量-输出口5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            if (engineData.MOutput05 == 0) engineData.MOutput05 = 1;
            else engineData.MOutput05 = 0;

            if (engineData.MOutput05 == 0) button25.BackColor = Color.FromArgb(28, 66, 28);
            else button25.BackColor = Color.FromArgb(44, 255, 44);
        }

        /// <summary>
        /// 开关量-输出口6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            if (engineData.MOutput06 == 0) engineData.MOutput06 = 1;
            else engineData.MOutput06 = 0;

            if (engineData.MOutput06 == 0) button27.BackColor = Color.FromArgb(28, 66, 28);
            else button27.BackColor = Color.FromArgb(44, 255, 44);
        }

        /// <summary>
        /// 开关量-输出口7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            if (engineData.MOutput07 == 0) engineData.MOutput07 = 1;
            else engineData.MOutput07 = 0;

            if (engineData.MOutput07 == 0) button29.BackColor = Color.FromArgb(28, 66, 28);
            else button29.BackColor = Color.FromArgb(44, 255, 44);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (communicationstate.COMHardConnectstate == 1 && communicationstate.NetWorkHardConnectstate == 0)
            {
                COM_DataSend("move 0,-1000,100,1000,100,100", "gb2312" );
            }
            else if (communicationstate.COMHardConnectstate == 0 && communicationstate.NetWorkHardConnectstate == 1)
            {
                NetWorkSendReceived(textBox2.Text);           //网络通讯发送接收数据处理
            }
            else if (communicationstate.COMHardConnectstate == 1 && communicationstate.NetWorkHardConnectstate == 1)
            {
                textBox3.AppendText(DateTime.Now.ToString() + " " + "网口通讯与串口通讯不能同时打开！\r\n");
            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString() + " " + "网口通讯与串口通讯未打开！\r\n");
            }
        }
    }
}


