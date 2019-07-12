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
        public TcpClientForm()
        {
            InitializeComponent();
        }

        private void TcpClientForm_Load(object sender, EventArgs e)
        {
            IPAddress serverIp = IPAddress.Parse("127.0.0.1"); //服务端IP 将IP字符串转化为IPAddress实例
            TcpClient client = new TcpClient(); //实例化客户端
            client.Connect(serverIp, 8888); //端口8888需与服务端开启的端口一致，否则无法与服务端建立链接
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
