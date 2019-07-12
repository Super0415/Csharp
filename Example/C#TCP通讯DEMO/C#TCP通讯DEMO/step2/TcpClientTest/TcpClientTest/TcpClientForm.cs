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

namespace TcpClientTest
{
    //TCP通讯测试客户端代码
    //yelg 20170824
    public partial class TcpClientForm : Form
    {
        //结构体 IP端口
        struct EndPoint
        {
            public string Ip;
            public string Port;
        }

        public TcpClientForm()
        {
            InitializeComponent();
        }

        private void TcpClientForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Connect()
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
            IPAddress serverIp = IPAddress.Parse(endPoint.Ip); //服务端IP 将IP字符串转化为IPAddress实例
            int serverPort = int.Parse(endPoint.Port); //服务端端口
            TcpClient client = new TcpClient(); //实例化客户端
            client.Connect(serverIp, serverPort); //端口8888需与服务端开启的端口一致，否则无法与服务端建立链接
            WriteLog("客户端开始连接服务端...");
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

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            Connect();
        }
    }
}
