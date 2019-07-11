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

namespace MyTest00
{
    public partial class Form1 : Form
    {
        private TcpClient client;                   //创建一个客户端
        NetworkStream sendStream;                   //创建一个数据流
        private const int buffSize = 10240;         //设置数据流大小为1024
        private UInt16 HardConnectstate = 0;               //记录硬件端口连接状态
        private UInt16 SoftConnectstate = 0;               //记录socket连接状态
        //结构体 IP端口
        struct EndPoint
        {
            public string Ip;
            //public string Port;   //暂时固定4000端口
        }
        EndPoint endPoint = new EndPoint();

        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox3.Height = 100;
            this.textBox3.Width = 700;
            this.textBox1.Text = "192.168.1.100";
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //点击事件 - 网口连接状态判断
        private void button1_Click(object sender, EventArgs e)
        {
            endPoint.Ip = textBox1.Text.Trim();
            IPAddress serverIp = IPAddress.Parse(endPoint.Ip);  //服务端IP 将IP字符串转化为IPAddress实例  注：未填写IP地址，此处阻塞
            if (endPoint.Ip == string.Empty)     //确保不会误操作导致IP为空
            {
                //MessageBox.Show("IP不能为空", "登录提示", 0, MessageBoxIcon.Exclamation);
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
                    client.Close();
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

        //获取所有网络端口信息
        public void GetTcpConnections()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();  
            foreach (TcpConnectionInformation t in connections)
            {
                this.textBox3.AppendText("Local endpoint: " + t.LocalEndPoint.ToString() + "\r\n");
                this.textBox3.AppendText("Remote endpoint:" + t.RemoteEndPoint.ToString() + "\r\n");
            }
        }

        //获取指定IP端口物理状态
        public void GetTcpPOPnnections()
        {
            Ping p = new Ping();
            PingReply reply = p.Send(this.textBox1.Text);   //进行 ping 连接测试，并返回连接状态
            if (reply.Status == IPStatus.Success)
            {
                this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                this.textBox3.AppendText("物理链路连接成功" + "\r\n");
                HardConnectstate = 1;
            }
            else
            {
                this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                this.textBox3.AppendText("物理链路连接失败" + "\r\n");
                this.button1.Text = "未连接";
                HardConnectstate = 0xFF;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.textBox3.AppendText(DateTime.Now.ToString() + " ");
            //GetTcpConnections();
            GetTcpPOPnnections();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) timer1.Enabled = false;
            else timer1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (HardConnectstate == 1 && SoftConnectstate == 1)
            {
                client = new TcpClient(); //实例化客户端
                client.Connect(endPoint.Ip, 4000); //端口需与服务端开启的端口一致，否则无法与服务端建立链接    注：未链接服务器此处阻塞
                sendStream = client.GetStream();

                if (client != null)                             //确保连接有效
                {
                    if (textBox2.Text.Trim() == string.Empty)   //确保数据非空
                    {
                        return;
                    }
                    else
                    {
                        string sendData = textBox2.Text.Trim(); //要发送的数据
                        byte[] buffer = Encoding.Default.GetBytes(sendData); //将数据存入缓存
                        sendStream.Write(buffer, 0, buffer.Length);
                        client.Close();

                    }
                }
            }
            else
            {
                this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                this.textBox3.AppendText("网口连接异常" + "\r\n");
            }


        }
        //点击事件 - 串口连接（暂未写）
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
