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
using System.IO;

namespace TcpClientTest
{
    //TCP通讯测试客户端代码
    //yelg 20170824
    public partial class TcpClientForm : Form
    {
        private TcpClient client;
        NetworkStream sendStream;
        private const int buffSize = 10240;
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

        //连接服务端
        private void Connect()
        {
            EndPoint endPoint = new EndPoint();
            endPoint.Ip = textBox_Ip.Text.Trim();
            endPoint.Port = textBox_Port.Text.Trim();
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
            IPAddress serverIp = IPAddress.Parse(endPoint.Ip); //服务端IP 将IP字符串转化为IPAddress实例
            int serverPort = int.Parse(endPoint.Port); //服务端端口
            client = new TcpClient(); //实例化客户端
            try
            {

                if (client.Connected)
                {
                    WriteLog("客户端已经连接服务端，请勿重复连接！");
                    WriteLogToFile("客户端已经连接服务端，请勿重复连接！");
                    return;
                }
                client.Connect(serverIp, serverPort); //端口8888需与服务端开启的端口一致，否则无法与服务端建立链接
                WriteLog("客户端开始连接服务端...");
                WriteLogToFile("客户端开始连接服务端...");
                sendStream = client.GetStream();
                if (client.Connected)
                {
                    //客户端连接成功，设置心跳间隔并启动心跳
                    timer_HeartBeat.Interval = 5000;
                    timer_HeartBeat.Start();
                    WriteLog("客户端连接服务端成功！");
                    WriteLogToFile("客户端连接服务端成功！");
                }
            }
            catch (Exception e)
            {
                WriteLog("客户端连接服务端出现异常：" + e.Message);
                WriteLogToFile("客户端连接服务端出现异常：" + e.Message);
            }
            
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

        //发起连接按钮 点击事件
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            Connect();
        }


        //发送数据按钮 点击事件
        private void btn_SendData_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                if (textBox_SendData.Text.Trim() == string.Empty)
                {
                    return;
                }
                else
                {
                    string sendData = textBox_SendData.Text.Trim(); //要发送的数据
                    byte[] buffer = Encoding.Default.GetBytes(sendData); //将数据存入缓存
                    sendStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        //心跳方法 发送心跳数据
        private void HeartBeat()
        {
            try
            {
                string heartData = "WSXT";
                byte[] buffer = Encoding.Default.GetBytes(heartData);
                sendStream.Write(buffer, 0, buffer.Length);
            }
            catch (IOException e)
            {
                WriteLog("与服务端的连接出现异常：" + e.Message);
                WriteLogToFile("与服务端的连接出现异常：" + e.Message);
            }
            
        }

        //关闭连接 点击事件
        private void btn_Close_Click(object sender, EventArgs e)
        {
            timer_HeartBeat.Stop(); //关闭心跳定时器
            client.Close();
            WriteLog("客户端已断开与服务端的连接！");
            WriteLogToFile("客户端已断开与服务端的连接！");
        }

        //输出日志到文件
        private void WriteLogToFile(String log)
        {
            try
            {
                string logPath = Path.GetDirectoryName(Application.ExecutablePath);
                string fileName = "log." + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + log;
                //File.AppendAllText(logPath + "/logs/" + fileName, log, UTF8Encoding.UTF8);
                if (!Directory.Exists(logPath + "/logs"))
                {
                    //目录logs不存在，则创建该目录
                    DirectoryInfo dircetoryInfo = new DirectoryInfo(logPath + "/logs");
                    dircetoryInfo.Create();
                }
                else
                {
                    //目录logs存在，输出日志
                    StreamWriter sw = File.AppendText(logPath + "/logs/" + fileName);
                    sw.WriteLine(log);
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("输出日志出现异常：" + e.Message);
            }
        }

        private void bgWorker_ServerStatusCheck_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bgWorker_ServerStatusCheck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        //发送心跳数据定时器
        private void timer_HeartBeat_Tick(object sender, EventArgs e)
        {
            HeartBeat();
            WriteLog("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]客户端发送心跳数据！");
        }
    }
}
