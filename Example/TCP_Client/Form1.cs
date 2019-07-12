using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCP_Client
{
    public partial class Form1 : Form
    {
        NetworkStream ns;
        TcpClient clientsocket;
        bool connected;
        Thread receive;

        public Form1()
        {
            InitializeComponent(); 
            this.Text = "�ͻ���";
            iptxt.Text = "127.0.0.1";
        }
        public delegate void Recdatadelegate(String str);
        private void showInfo(string s)
        {
            Invoke(new Recdatadelegate(rtbChatIn.AppendText), new object[] { s });
        }
        private void EstablishConnection()
        {
            sslbStatus.Text = "�������ӵ�������";

            try
            {
                string serverIP = iptxt.Text;
                int serverPort = Convert.ToInt32(porttxt.Text);
                clientsocket = new TcpClient(serverIP, serverPort);
                ns = clientsocket.GetStream();
                //sr = new StreamReader(ns);
                connected = true;
                sslbStatus.Text = "������";

            }
            catch (Exception)
            {
                MessageBox.Show("�������ӵ���������", "����",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                sslbStatus.Text = "�ѶϿ�����";
            }
        }
        private void RegisterWithServer()
        {
            try
            {
                string command = "*RS," + txtCarNO.Text + ",V1,000047,V,0000.0000,N,00000.0000,E,000.00,000,080804,FFFFFBFF#"; // string command = "CHAT|" + clientname + ": " + ChatOut.Text + "\r\n";
                Byte[] outbytes;
                if (rdoASCII.Checked)
                    outbytes = System.Text.Encoding.Default.GetBytes(command.ToCharArray());
                else
                    outbytes = System.Text.Encoding.Unicode.GetBytes(command.ToCharArray());


                ns.Write(outbytes, 0, outbytes.Length);
                //clientsocket.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void QuitChat()
        {
            if (connected)
            {
                try
                {
                    ns.Close();
                    clientsocket.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (receive != null && receive.IsAlive)
                    receive.Abort();
            }

            connected = false;

        }

        private void ReceiveChat()
        {
            bool keepalive = true;
            while (keepalive)
            {
                try
                {
                    Byte[] buffer = new Byte[1024];  // 2048???
                    int n = ns.Read(buffer, 0, buffer.Length);
                    byte[] getMsgbt = new byte[n];      //�����ݽ��յ����ֽ���Ŀ�������ֽ�����
                    Array.Copy(buffer, 0, getMsgbt, 0, n);
                    string serverInfo = "";
                    if (rdoASCII.Checked)
                        serverInfo = System.Text.Encoding.Default.GetString(getMsgbt);
                    else
                        serverInfo = System.Text.Encoding.Unicode.GetString(getMsgbt);
                    serverInfo.Trim();
                    showInfo("�յ�:"+serverInfo + "\r\n");
                    backToServer(serverInfo);
                }
                catch (Exception) { }
            }
        }
        //һ��V1���"*RS,1234567890,V1,HHmmss,S,latitude,D,longitude,G,speed,direction,DDMMYY,VehStatus#"
        //      ���ӣ�"*RS,1234567890,V1,000047,V,000.0000,N,0000.0000,E,00.00,   E,     080804,FFFFFBFF#";
        //����ȷ��V4��"*RS,1234567890,V4,CMD,VVVVVV,HHmmss,S,latitude,D,longitude,G,speed,direction,DDMMYY,VehStatus#";
        //      ���ӣ�"*RS,1234567890,V4,S4 ,9F,FF, 112233,S,000.0000,N,0000.0000,E,00.00,   E,     080104,FFFFFBFF#";
        private void backToServer(string recInfo)
        {
            if (!connected) return;
            string[] rec = recInfo.Split(new Char[] { ',' }); //*RS,YYYYYY,CMD,HHmmss,..Args...
            if (rec.Length < 4) return;
            string sBack = "";
            switch (rec[2])
            {
                case "S23":
                    //*RS,0000000001,S23,HHmmss,127.0.0.1,8800,redialTimes#
                    //*RS,0000000001,V4,S23,127.0.0.1:8800,HHmmss,083058,A,000.0000,N,0000.0000,E,00.00,19.60,080104,FFFFFBFF#
                    sBack = rec[0] + "," + rec[1] + ",V4," + rec[2] + "," + rec[4] + ":" + rec[5] + ",112233,083058,A,000.0000,N,0000.0000,E,00.00,19.60,080104,FFFFFBFF";
                    break;
                default:
                    //ϵͳ��������S4  //��������:  *RS,YYYYYY,S4,HHmmss,9F,FF#   //����ȷ��:  *RS,YYYYYY,V4,S4,9F,FF,HHmmss,....#

                    //       *RS      , 1234567890   ,V4,      S4          ,9F,FF,112233,S,000.0000,N,0000.0000,E,00.00,E,080104,FFFFFBFF#";
                    sBack = rec[0] + "," + rec[1] + ",V4," + rec[2] ;//+ " ,9F,FF,112233,S,000.0000,N,0000.0000,E,00.00,E,080104,FFFFFBFF#";
                    for (int i = 4; i < rec.Length - 1; i++) sBack += "," + rec[i];
                    sBack += "," + rec[rec.Length - 1].Substring(0,rec[rec.Length - 1].Length-1);
                    sBack += ",112233,S,000.0000,N,0000.0000,E,00.00,E,080104,FFFFFBFF#";
                    break;
            }

            if (sBack != "")
            {
                try
                {
                    Byte[] outbytes;
                    if (rdoASCII.Checked)
                        outbytes = System.Text.Encoding.Default.GetBytes(sBack.ToCharArray());
                    else
                        outbytes = System.Text.Encoding.Unicode.GetBytes(sBack.ToCharArray());
                    ns.Write(outbytes, 0, outbytes.Length);
                    showInfo("ȷ��:" + sBack + "\r\n");
                }
                catch { }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            EstablishConnection();
            RegisterWithServer();
            if (connected)
            {
                receive = new Thread(new ThreadStart(ReceiveChat));
                receive.Start();
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            QuitChat();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            QuitChat();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                try
                {
                    string command = "*RS," + txtCarNO.Text + ",V1,000047,V,0000.0000,N,00000.0000,E,000.00,W,080804,FFFFFBFF#";
                    Byte[] outbytes;
                    if (rdoASCII.Checked)
                        outbytes = System.Text.Encoding.Default.GetBytes(command.ToCharArray());
                    else
                        outbytes = System.Text.Encoding.Unicode.GetBytes(command.ToCharArray());


                    ns.Write(outbytes, 0, outbytes.Length);
                    //clientsocket.Close(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}