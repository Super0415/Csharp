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
using System.IO;

namespace TcpServerTest
{
    //TCP通讯测试服务端代码
    //yelg 20170824
    public partial class TcpServerForm : Form
    {
        private TcpClient client;
        private TcpListener server;
        private const int buffSize = 10240;
        private static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);
        //结构体 IP端口
        struct EndPoint
        {
            public string Ip;
            public string Port;
        }

        Thread threadWatch = null;   //创建监听客户端连接请求的线程
        Thread threadReceive = null; //创建监听客户端发送数据的线程
        Socket socketWatch = null;

        public TcpServerForm()
        {
            InitializeComponent();
        }

        private void TcpServerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_Listen_Click(object sender, EventArgs e)
        {
            //使用指定的地址族、套接字类型和协议初始化
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            IPAddress address = IPAddress.Parse(textBox_Ip.Text.Trim());
            int port = int.Parse(textBox_Port.Text.Trim());
            IPEndPoint endPoint = new IPEndPoint(address, port);
            if (textBox_Ip.Text.Trim() == string.Empty)
            {
                WriteLog("IP为空，请先输入IP！");
                return;
            }
            if (textBox_Port.Text.Trim() == string.Empty)
            {
                WriteLog("端口为空，请先输入端口！");
                return;
            }
            try
            {
                socketWatch.Bind(endPoint);
            }
            catch (Exception ex)
            {
                WriteLog("绑定IP、端口出现异常！");
                WriteLogToFile("绑定IP、端口出现异常：" + ex.Message);
            }
            socketWatch.Listen(10);                          //设置监听队列的长度
            threadWatch = new Thread(WatchClientConnecting); //创建负责监听客户端连接请求的线程
            threadWatch.IsBackground = true;                 //设置线程为后台线程
            threadWatch.Start();
            WriteLog("服务端开始监听客户端的连接请求！");
        }

        private void WatchClientConnecting()
        {
            while (true)
            {
                //开始监听客户端的连接请求，Accept()为同步方法，会阻塞当前线程
                Socket socketConnection = socketWatch.Accept();
                WriteLog("客户端[" + socketConnection.RemoteEndPoint.ToString() + "]连接服务端成功！");
                WriteLogToFile("客户端[" + socketConnection.RemoteEndPoint.ToString() + "]连接服务端成功！");

                threadReceive = new Thread(ReceiveData); //创建负责监听客户端发送数据的线程
                threadReceive.IsBackground = true;       //设置线程为后台线程
                threadReceive.Start(socketConnection);
                WriteLog("服务端开始监听客户端发送的数据！");
            }
        }

        //接收客户端发送的数据
        private void ReceiveData(object socketConnection)
        {
            //Socket socketClient = socketConnection as Socket;
            Socket socketClient = (Socket)socketConnection;
            while (true)
            {
                byte[] dataCache = new byte[1024 * 1024 * 2]; //定义一个2M的缓存区
                int length = -1;
                try
                {
                    length = socketClient.Receive(dataCache); //接收数据，并返回数据长度
                    if (length > 0)
                    {
                        string data = Encoding.Default.GetString(dataCache, 0, length);
                        if (data.Trim().StartsWith("WSXT"))
                        {
                            WriteLog("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]接收到客户端发送的心跳数据！");
                        }
                        else
                        {
                            WriteLog("客户端:" + data);
                        }
                    }
                }
                catch (SocketException se)
                {
                    WriteLog("接收客户端发送的数据出现异常！");
                    WriteLogToFile("接收客户端发送的数据出现异常：" + se.Message);
                }
            }
        }

        /*
        private void btn_Listen_Click(object sender, EventArgs e)
        {
            //EndPoint endPoint = (EndPoint)obj;
            //EndPoint endPoint = new EndPoint();
            //endPoint.Ip = textBox_Ip.Text.Trim();
            //endPoint.Port = textBox_Port.Text.Trim();
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
            //IPAddress serverIp = IPAddress.Parse(endPoint.Ip);
            //int port = int.Parse(endPoint.Port);
            //TcpListener listener = new TcpListener(serverIp, port); //设置服务端IP、端口
            //listener.Start();
            //WriteLog("服务端开启侦听...");
            //WriteLogToFile("服务端开启侦听...");
            //DoBeginAcceptTcpClient(listener);

            Thread thread = new Thread(Listen);
            
            thread.Start(endPoint);
            //Listen();
        }
        */

        private void DoBeginAcceptTcpClient(TcpListener listener)
        {
            tcpClientConnected.Reset();
            WriteLog("等待客户端连接...");
            WriteLogToFile("等待客户端连接...");
            listener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), listener);
            tcpClientConnected.WaitOne();
        }

        private void DoAcceptTcpClientCallback(IAsyncResult iar)
        {
            TcpListener listener = (TcpListener)iar.AsyncState;
            TcpClient tcpClient = listener.EndAcceptTcpClient(iar);
            WriteLog("已有客户端连接成功！");
            WriteLogToFile("已有客户端连接成功！");
            tcpClientConnected.Set();
        }

        private void Listen(object obj)
        {
            EndPoint endPoint = (EndPoint)obj;

            //if (endPoint.Ip == string.Empty)
            //{
            //    WriteLog("IP为空，请先输入IP！");
            //    return;
            //}
            //if (endPoint.Port == string.Empty)
            //{
            //    WriteLog("端口为空，请先输入端口！");
            //    return;
            //}
            IPAddress serverIp = IPAddress.Parse(endPoint.Ip);
            int port = int.Parse(endPoint.Port);
            server = new TcpListener(serverIp, port); //设置服务端IP、端口

            try
            {
                server.Start(); //启动侦听
                WriteLog("服务端开启侦听...");
                WriteLogToFile("服务端开启侦听...");
                //接收客户端发起的连接请求 这是一个同步方法，在没有接收到客户端的连接请求
                //它后面的代码不会执行，线程将阻塞
                client = server.AcceptTcpClient();
                WriteLog("有客户端请求连接，连接已建立！");
                WriteLogToFile("有客户端请求连接，连接已建立！");
            }
            catch (Exception e)
            {
                WriteLog("服务端开启侦听出现异常：" + e.Message);
                WriteLogToFile("服务端开启侦听出现异常：" + e.Message);
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
                        WriteLogToFile("客户端：" + clientMsg);
                    }
                }
                catch (Exception e)
                {
                    WriteLog("接收客户端消息出现异常：" + e.Message + "。连接被迫关闭！");
                    WriteLogToFile("接收客户端消息出现异常：" + e.Message + "。连接被迫关闭！");
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
    }
}
