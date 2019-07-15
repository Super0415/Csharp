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
            public UInt16 NetWorkHardConnectstate;              //记录硬件端口连接状态
            public UInt16 NetWorkSoftConnectstate;              //记录socket连接状态
            public UInt16 COMHardConnectstate;                  //记录硬件串口连接状态
            public UInt16 COMSoftConnectstate;                  //记录串口通讯连接状态
        }

        private TcpClient client;                               //创建一个客户端
        NetworkStream Stream;                                   //创建一个数据流
        Socket socketWatch = null;                              //创建一个socket套接字
        
        public static byte[] data = new byte[1024];             //创建数据读取缓存区
        EndPoint endPoint = new EndPoint();                     //保存IP信息        

        SerialPort com = new SerialPort();
        CommunicationState communicationstate = new CommunicationState();   //创建一个记录通讯状态的信息区

        public Form1()
        {
            InitializeComponent();

            this.textBox3.Height = 230;                 //数据接收区的高度
            this.textBox3.Width = 700;                  //数据接收区的宽度
            this.textBox1.Text = "192.168.1.100";       //设置IP显示默认值

            //网络通讯 - 配置IP与端口号
            NetWorkInit();
            //端口显示控件 波特率显示控件
            COMInit(comboBox2, comboBox1);

        }
        //点击事件 - 网口连接状态判断
        private void button1_Click(object sender, EventArgs e)
        {
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
            NetWorkGetTcpPOPnnections();
        }
        //测试模式是否开启定时器
        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) timer1.Enabled = false;
            else timer1.Enabled = true;
        }
        //点击事件 - 发送网络通讯数据
        private void button4_Click(object sender, EventArgs e)
        {
            if (communicationstate.COMHardConnectstate == 1 && communicationstate.NetWorkHardConnectstate == 0)
            {
                Encoding gb = System.Text.Encoding.GetEncoding("gb2312");
                byte[] bytes = gb.GetBytes(textBox2.Text);
                com.Write(bytes, 0, bytes.Length);
            }
            else if (communicationstate.COMHardConnectstate == 0 && communicationstate.NetWorkHardConnectstate == 1)
            {
                //网络通讯接收数据处理
                NetWorkSendReceived();
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
            if(communicationstate.NetWorkHardConnectstate == 0) NetWorkGetTcpPOPnnections();               //确保物理层线路连接正常
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
                textBox3.AppendText(DateTime.Now.ToString() + " ");
                textBox3.AppendText("网口连接失败" + "\r\n");
                button1.Text = "未连接";
                communicationstate.NetWorkHardConnectstate = 0;
                communicationstate.NetWorkSoftConnectstate = 0;
            }
        }
        //显示接收数据 - 输出控件
        void NetWorkSendReceived()
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

            if (client != null && textBox2.Text.Trim() != string.Empty)                                     //确保客户端实例化成功
            {
                try
                {
                    string sendData = textBox2.Text.Trim() + "\n";         //获取要发送的数据
                    byte[] buffer = Encoding.UTF8.GetBytes(sendData); //将数据存入缓存
                    Stream.Write(buffer, 0, buffer.Length);

                    textBox3.AppendText(DateTime.Now.ToString() + "发送长度 " + buffer.Length  + "发送数据：" + " " + sendData + "\r\n");

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
        void COMInit(ComboBox Box1, ComboBox Box2)
        {
            Control.CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到
            int[] item = { 9600, 115200 };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            foreach (int a in item)
            {
                Box1.Items.Add(a.ToString());
            }
            Box1.SelectedItem = Box1.Items[1];    //默认为列表第二个变量 - 115200

            string[] ports = SerialPort.GetPortNames();
            Box2.Items.AddRange(ports);
            Box2.SelectedItem = Box2.Items[0];
        }

        //事件API - 点击按钮，连接串口
        void COMEventClickToConnect(ComboBox Box1,ComboBox Box2, Button Btn1)
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
        void COM_DataReceived(object sender, SerialDataReceivedEventArgs e)   //数据接收事件，读到数据的长度赋值给count，如果是8位（节点内部编程规定好的），就申请一个byte类型的buff数组，s句柄来读数据
        {
            int len = com.BytesToRead;
            byte[] bytes = new byte[len];
            com.Read(bytes, 0, len);
            string str = System.Text.Encoding.Default.GetString(bytes); //xx="中文";
            textBox3.AppendText(DateTime.Now.ToString() + "接收数据:" + str + "\r\n");
        }




    }
}
