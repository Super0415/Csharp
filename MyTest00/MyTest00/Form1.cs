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

namespace MyTest00
{
    public partial class Form1 : Form
    {
        //结构体 IP端口
        struct EndPoint
        {
            public string Ip;
            public int Port;  
        }

        private TcpClient client;                       //创建一个客户端
        NetworkStream Stream;                           //创建一个数据流
        Socket socketWatch = null;                      //创建一个socket套接字
        private UInt16 HardConnectstate = 0;            //记录硬件端口连接状态
        private UInt16 SoftConnectstate = 0;            //记录socket连接状态
        public static byte[] data = new byte[1024];     //创建数据读取缓存区
        EndPoint endPoint = new EndPoint();             //保存IP信息

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox3.Height = 230;                 //数据接收区的高度
            this.textBox3.Width = 700;                  //数据接收区的宽度
            this.textBox1.Text = "192.168.1.100";       //设置IP显示默认值
        }

        //点击事件 - 网口连接状态判断
        private void button1_Click(object sender, EventArgs e)
        {
                                           //使用指定的地址族、套接字类型和协议初始化
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            endPoint.Ip = textBox1.Text.Trim();
            endPoint.Port = 4000;
            IPAddress serverIp = IPAddress.Parse(endPoint.Ip);  //服务端IP 将IP字符串转化为IPAddress实例  注：未填写IP地址，此处阻塞
            if (endPoint.Ip == string.Empty)     //确保不会误操作导致IP为空
            {
                this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                this.textBox3.AppendText("IP为空" + "\r\n");
                return;
            }
            GetTcpPOPnnections();               //确保物理层线路连接正常
            if (HardConnectstate != 0xFF)
            {
                if (SoftConnectstate == 0)
                {
                    this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                    this.textBox3.AppendText("网口打开链接" + "\r\n");
                    this.button1.Text = "已连接";
                    SoftConnectstate = 1;
                }
                else
                {
                    //client.Close();
                    this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                    this.textBox3.AppendText("网口断开链接" + "\r\n");
                    this.button1.Text = "未连接";
                    SoftConnectstate = 0;
                }
            }
            else
            {
                HardConnectstate = 0;
                SoftConnectstate = 0;
            }
        }

        //获取指定IP端口物理状态
        public void GetTcpPOPnnections()
        {
            Ping p = new Ping();
            PingReply reply = p.Send(this.textBox1.Text);   //进行 ping 连接测试，并返回连接状态
            if (reply.Status == IPStatus.Success)       //物理链路连接成功
            {
                HardConnectstate = 1;
            }
            else                                           //物理链路连接失败
            {
                this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                this.textBox3.AppendText("网口连接失败" + "\r\n");
                this.button1.Text = "未连接";
                HardConnectstate = 0xFF;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetTcpPOPnnections();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) timer1.Enabled = false;
            else timer1.Enabled = true;
        }
        //点击事件 - 发送网络通讯数据
        private void button4_Click(object sender, EventArgs e)
        {
            GetTcpPOPnnections();                                       //确保物理层线路连接正常
            if (HardConnectstate == 1 && SoftConnectstate == 1)
            {
                client = new TcpClient();                               //实例化客户端
                client.Connect(endPoint.Ip, endPoint.Port);             //端口需与服务端开启的端口一致，否则无法与服务端建立链接    注：未链接服务器此处阻塞
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

                        textBox3.AppendText("发送数据" + " " + sendData + " 发送长度");
                        textBox3.AppendText(buffer.Length + "\r\n");


                        int length = Stream.Read(data, 0, data.Length); //读取要接收的数据
                        string receiveMsg = Encoding.UTF8.GetString(data, 0, length);

                        textBox3.AppendText("接收长度" + " ");
                        textBox3.AppendText(length + " ");

                        textBox3.AppendText(DateTime.Now.ToString() + " ");
                        textBox3.AppendText(receiveMsg + "\r\n");

                        client.Close();
                    }
                    catch
                    {
                        textBox3.AppendText(DateTime.Now.ToString() + " ");
                        textBox3.AppendText("通讯异常" + "\r\n");
                    }
                }
            }
            else
            {
                textBox3.AppendText(DateTime.Now.ToString() + " ");
                textBox3.AppendText("网口连接异常" + "\r\n");
            }
        }
        //点击事件 - 串口连接（暂未写）
        private void button2_Click(object sender, EventArgs e)
        {

        }

    }
}
