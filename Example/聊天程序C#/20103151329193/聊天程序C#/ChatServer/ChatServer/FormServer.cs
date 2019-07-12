using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//添加的命名空间引用
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
namespace ChatServer
{
    public partial class FormServer : Form
    {
        //连接的用户

        System.Collections.Generic.List<User> userList = new List<User>();
        TcpListener listener;
        private delegate void SetListBoxCallback(string str);
        private SetListBoxCallback setListBoxCallback;
        private delegate void SetComboBoxCallback(User user);
        private SetComboBoxCallback setComboBoxCallback;
        //使用的本机IP地址
        IPAddress localAddress;
        //监听端口
        private int port = 51888;
        private TcpListener myListener;

        
        public FormServer()
        {
            InitializeComponent();
            listBoxStatus.HorizontalScrollbar = true;
            setListBoxCallback = new SetListBoxCallback(SetListBox);
            setComboBoxCallback = new SetComboBoxCallback(AddComboBoxitem);
            IPAddress[] addrIP = Dns.GetHostAddresses(Dns.GetHostName());
            localAddress = addrIP[0];
            buttonStop.Enabled = false;
        }
        
        //【开始监听】按钮的Click事件
        private void buttonStart_Click(object sender, EventArgs e)
        {
            myListener = new TcpListener(localAddress, port);
            myListener.Start();
            SetListBox(string.Format("开始在{0}:{1}监听客户连接", localAddress, port));
            //创建一个线程监听客户端连接请求
            ThreadStart ts = new ThreadStart(ListenClientConnect);
            Thread myThread = new Thread(ts);
            myThread.Start();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }
        //接收客户端连接
        private void ListenClientConnect()
        {
            while (true)
            {
                TcpClient newClient = null;
                try
                {
                    //等待用户进入
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    //当单击“停止监听”或者退出此窗体时AcceptTcpClient()会产生异常
                    //因此可以利用此异常退出循环
                    break;
                }
                //每接受一个客户端连接,就创建一个对应的线程循环接收该客户端发来的信息
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveData);
                Thread threadReceive = new Thread(pts);
                User user = new User(newClient);
                threadReceive.Start(user);
                userList.Add(user);
                AddComboBoxitem(user);
                SetListBox(string.Format("[{0}]进入", newClient.Client.RemoteEndPoint));
                SetListBox(string.Format("当前连接用户数：{0}", userList.Count));
            }
        }

        //接收、处理客户端信息，每客户1个线程，参数用于区分是哪个客户
        private void ReceiveData(object obj)
        {
            User user = (User)obj;
            TcpClient client = user.client;
            //是否正常退出接收线程
            bool normalExit = false;
            //用于控制是否退出循环
            bool exitWhile = false;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    //从网络流中读出字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = user.br.ReadString();
                }
                catch
                {
                    //底层套接字不存在时会出现异常
                    SetListBox("接收数据失败");
                }
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        //如果停止了监听，Connected为false
                        if (client.Connected == true)
                        {
                            SetListBox(string.Format(
                                "与[{0}]失去联系，已终止接收该用户信息", client.Client.RemoteEndPoint));
                        }
                    }
                    break;
                }
                SetListBox(string.Format("来自[{0}]：{1}", user.client.Client.RemoteEndPoint, receiveString));
                string[] splitString = receiveString.Split(',');
                string sendString = "";
                switch (splitString[0])
                {
                    case "Login":
                        //格式：Login
                            sendString = "Hello，我是服务器，你好!";
                            SendToClient(user, sendString);
                        break;
                    case "Logout":
                        //格式：Logout
                        SetListBox(string.Format("[{0}]退出", user.client.Client.RemoteEndPoint));
                        normalExit = true;
                        exitWhile = true;
                        break;
                    case "Talk":
                        //格式：Talk,对话内容
                        SetListBox(string.Format("[{0}]说：{1}",client.Client.RemoteEndPoint,
                            receiveString.Substring(splitString[0].Length + 1)));
                        break;
                    default:
                        SetListBox("什么意思啊：" + receiveString);
                        break;
                }
            }
            userList.Remove(user);
            client.Close();
            SetListBox(string.Format("当前连接用户数：{0}", userList.Count));
        }

        private void SendToClient(User user, string str)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                user.bw.Write(str);
                user.bw.Flush();
                SetListBox(string.Format("向[{0}]发送：{1}", user.client.Client.RemoteEndPoint, str));
            }
            catch
            {
                SetListBox(string.Format("向[{0}]发送信息失败", user.client.Client.RemoteEndPoint));
            }
        }
        
        private void AddComboBoxitem(User user)
        {
            if (comboBoxReceiver.InvokeRequired == true)
            {
                this.Invoke(setComboBoxCallback, user);
            }
            else
            {
                comboBoxReceiver.Items.Add(user.client.Client.RemoteEndPoint);
            }
        }
       
        private void SetListBox(string str)
        {
            if (listBoxStatus.InvokeRequired == true)
            {
                this.Invoke(setListBoxCallback, str);
            }
            else
            {
                listBoxStatus.Items.Add(str);
                listBoxStatus.SelectedIndex = listBoxStatus.Items.Count - 1;
                listBoxStatus.ClearSelected();
            }
        }
        
        //【停止监听】按钮的Click事件
        private void buttonStop_Click(object sender, EventArgs e)
        {
            SetListBox(string.Format("目前连接用户数：{0}", userList.Count));
            SetListBox("开始停止服务，并依次使用户退出!");
            for (int i = 0; i < userList.Count; i++)
            {
                comboBoxReceiver.Items.Remove(userList[i].client.Client.RemoteEndPoint);
                userList[i].br.Close();
                userList[i].bw.Close();
                userList[i].client.Close();
            }
            //通过停止监听让myListener.AcceptTcpClient()产生异常退出监听线程
            myListener.Stop();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        
        //【发送】按钮的Click事件
        private void buttonSend_Click(object sender, EventArgs e)
        {
            int index = comboBoxReceiver.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("请先选择接收方，然后再单击［发送］");
            }
            else
            {
                User user = (User)userList[index];
                SendToClient(user, textBoxSend.Text);
                textBoxSend.Clear();
            }
        }
 
        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //未单击开始监听就直接退出时，myListener为null
            if (myListener != null)
            {
                buttonStop_Click(null, null);
            }
        }

        private void textBoxSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonSend_Click(null, null);
            }
        }
    }
}