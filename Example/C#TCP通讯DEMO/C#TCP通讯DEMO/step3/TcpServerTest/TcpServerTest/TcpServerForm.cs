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
using System.Threading;

namespace TcpServerTest
{
    //TCP通讯测试服务端代码
    //yelg 20170824
    public partial class TcpServerForm : Form
    {
        private TcpClient client;
        private TcpListener server;
        private const int buffSize = 10240;
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

        private void btn_Listen_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Listen);
            EndPoint endPoint = new EndPoint();
            endPoint.Ip = textBox_Ip.Text.Trim();
            endPoint.Port = textBox_Port.Text.Trim();
            thread.Start(endPoint);
            //Listen();
        }

        private void Listen(object obj)
        {
            EndPoint endPoint = (EndPoint)obj;

            if (endPoint.Ip == string.Empty)
            {
                WriteLog("IP为空，请先输入IP！");
                return;
            }
            if (endPoint.Port == string.Empty)
            {
                WriteLog("端口为空，请先输入端口！");
                return;
            }
            IPAddress serverIp = IPAddress.Parse(endPoint.Ip);
            int port = int.Parse(endPoint.Port);
            server = new TcpListener(serverIp, port); //设置服务端IP、端口

            try
            {
                server.Start(); //启动侦听
                WriteLog("服务端开启侦听...");
                //接收客户端发起的连接请求 这是一个同步方法，在没有接收到客户端的连接请求
                //它后面的代码不会执行，线程将阻塞
                client = server.AcceptTcpClient();
                WriteLog("有客户端请求连接，连接已建立！");
            }
            catch (Exception e)
            {
                WriteLog("服务端开启侦听出现异常：" + e.Message);
                return;
            }
            
            NetworkStream receiveStream = client.GetStream();

            do
            {
                try
                {
                    byte[] buffer = new byte[buffSize]; //数据接收缓冲区
                    int msgSize = 0;
                    lock (receiveStream)
                    {
                        //从缓冲区读取客户端发送的数据
                        msgSize = receiveStream.Read(buffer, 0, buffSize);
                    }
                    //判断客户端是否发送数据
                    if (msgSize == 0)
                    {
                        return;
                    }
                    else
                    {
                        string clientMsg = Encoding.Default.GetString(buffer, 0, buffSize);
                        WriteLog("客户端：" + clientMsg);
                    }
                }
                catch (Exception e)
                {
                    WriteLog("接收客户端消息出现异常：" + e.Message + "。连接被迫关闭！");
                    break;
                }
            }
            while (true);
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
    }
}
