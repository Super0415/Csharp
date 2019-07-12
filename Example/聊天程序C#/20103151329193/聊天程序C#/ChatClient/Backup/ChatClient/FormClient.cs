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

namespace ChatClient
{
    public partial class FormClient : Form
    {
        private bool isExit = false;
        private delegate void SetListBoxCallback(string str);
        private SetListBoxCallback setListBoxCallback;
        private TcpClient client;
        private NetworkStream networkStream;
        private BinaryReader br;
        private BinaryWriter bw;
        public FormClient()
        {
            InitializeComponent();
            listBoxStatus.HorizontalScrollbar = true;
            setListBoxCallback = new SetListBoxCallback(SetListBox);
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //实际使用时要将Dns.GetHostName()改为服务器域名
                client = new TcpClient(Dns.GetHostName(), 51888);
                SetListBox(string.Format("本机EndPoint：{0}", client.Client.LocalEndPoint));
                SetListBox("与服务器建立连接成功");
            }
            catch
            {
                SetListBox("与服务器连接失败");
                return;
            }
            buttonConnect.Enabled = false;
            //获取网络流
            networkStream = client.GetStream();
            //将网络流作为二进制读写对象，使用UTF8编码
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            SendString("Login");
            Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
            threadReceive.Start();
        }
        private void ReceiveData()
        {
            while (isExit == false)
            {
                string receiveString = null;
                try
                {
                    //从网络流中读出字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = br.ReadString();
                }
                catch
                {
                    //底层套接字不存在时会出现异常
                    SetListBox("接收数据失败");
                }
                if (receiveString == null)
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("与服务器失去联系！");
                    }
                    break;
                }
                SetListBox("收到：" + receiveString);
            }
            Application.Exit();
        }

        private string ReadString(int dataLength)
        {
            string receiveString;
            try
            {
                //从网络流中读出字符串
                //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                receiveString = br.ReadString();
                return receiveString;
            }
            catch
            {
                //底层套接字不存在时会出现异常
                SetListBox("接收数据失败");
                return null;
            }
        }

        private void SendString(string str)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(str);
                bw.Flush();
                SetListBox(string.Format("发送：{0}", str));
            }
            catch
            {
                SetListBox("发送失败!");
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

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendString("Talk," + textBoxSend.Text);
            textBoxSend.Clear();
        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            //未与服务器连接前client为null
            if (client != null)
            {
                SendString("Logout");
                isExit = true;
                br.Close();
                bw.Close();
                client.Close();
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