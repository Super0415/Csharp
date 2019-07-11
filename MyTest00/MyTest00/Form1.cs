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
        private int Connectstate = 0;               //记录端口连接状态

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

        //点击事件 - 网口连接
        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress serverIp = IPAddress.Parse(this.textBox1.Text);  //服务端IP 将IP字符串转化为IPAddress实例  注：未填写IP地址，此处阻塞
            TcpClient client = new TcpClient(); //实例化客户端
            
            if (Connectstate == 0)
            {
                client.Connect(serverIp, 4000); //端口8888需与服务端开启的端口一致，否则无法与服务端建立链接    注：未链接服务器此处阻塞
                this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                this.textBox3.AppendText("网口打开链接" + "\r\n");
                this.button1.Text = "已连接";
                Connectstate = 1;
            }
            else
            {
                client.Close();
                this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                this.textBox3.AppendText("网口断开链接" + "\r\n");
                this.button1.Text = "未连接";
                Connectstate = 0;
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
                Connectstate = 1;
            }
            else
            {
                this.textBox3.AppendText(DateTime.Now.ToString() + " ");
                this.textBox3.AppendText("物理链路连接失败" + "\r\n");
                this.button1.Text = "未连接";
                Connectstate = 0;
            };
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

        }
        //点击事件 - 串口连接（暂未写）
        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
