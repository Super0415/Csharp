using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TcpServerTest
{
    //TCP通讯测试服务端代码
    //yelg 20170824
    public partial class TcpServerForm : Form
    {
        //结构体 IP端口
        struct EndPoint
        {
            public string Ip;
            public string Port;
        }

        public TcpServerForm()
        {
            InitializeComponent();
        }

        private void TcpServerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Listen()
        {
            EndPoint endPoint = new EndPoint();
            endPoint.Ip = textBox_Ip.Text.Trim();
            endPoint.Port = textBox_Port.Text.Trim();
            if (endPoint.Ip == string.Empty)
            {
                return;
            }
            if (endPoint.Port == string.Empty)
            {
                return;
            }
            IPAddress serverIp = IPAddress.Parse(endPoint.Ip);
            int port = int.Parse(endPoint.Port);
            TcpListener server = new TcpListener(serverIp, port); //设置服务端IP、端口
            server.Start(); //启动侦听
            WriteLog("服务端开启侦听...");
            //接收客户端发起的连接请求 这是一个同步方法，在没有接收到客户端的连接请求
            //它后面的代码不会执行，线程将阻塞
            TcpClient client = server.AcceptTcpClient();
            WriteLog("有客户端请求连接，连接已建立！");
        }

        //界面输出日志
        private delegate void writeLog(string log);
        private void WriteLog(string log)
        {
            if (this.rTxtBox_Msg.InvokeRequired)
            {
                writeLog writeLogDelegate = new writeLog(WriteLog);
                this.Invoke(writeLogDelegate, new object[] { log });
            }
            else
            {
                rTxtBox_Msg.AppendText(log + "\n");
                rTxtBox_Msg.ScrollToCaret();
            }
        }

        private void btn_Listen_Click(object sender, EventArgs e)
        {
            Listen();
        }
    }
}
