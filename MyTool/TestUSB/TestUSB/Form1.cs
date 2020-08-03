using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestUSB
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// USB 驱动
        /// </summary>
        USBDriver usb;

        /// <summary>
        /// 选中的 USB 序号
        /// </summary>
        int USBSelect = -1;

        /// <summary>
        /// 自动获取USB接收数据信息
        /// </summary>
        private Thread thread_R = null;
        private CancellationTokenSource cancelSource_R = null;
        /// <summary>
        /// 接收数据总长度
        /// </summary>
        ulong recvLen = 0;

        /// <summary>
        /// 推送信息到接收界面显示
        /// </summary>
        /// <param name="info"></param>
        public void RecordReceiveInfo(string info)
        {
            if (tbReceive.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) =>
                {
                    //this.tbReceive.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff \r\n") + info + "\r\n");
                    this.tbReceive.AppendText(info);
                    this.tbReceive.ScrollToCaret();
                };
                this.tbReceive.Invoke(actionDelegate, info);
            }
            else
            {
                //tbReceive.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff \r\n") + info + "\r\n");
                tbReceive.AppendText(string.Format(info));
                this.tbReceive.ScrollToCaret();
            }
        }

        /// <summary>
        /// 推送信息到主界面显示
        /// </summary>
        /// <param name="info"></param>
        public void RecordUSBInfo(string info)
        {
            if (tbUSBInfo.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) =>
                {
                    this.tbUSBInfo.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff \r\n") + info + "\r\n");
                    this.tbUSBInfo.ScrollToCaret();
                };
                this.tbUSBInfo.Invoke(actionDelegate, info);
            }
            else
            {
                tbUSBInfo.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff \r\n") + info + "\r\n");
                this.tbUSBInfo.ScrollToCaret();
            }
        }
        Dictionary<int, string> Rebuff;
        public Form1()
        {
            InitializeComponent();
            usb = new USBDriver();
            Rebuff = usb.RecvBuffer;
        }

        private void btnSearchUSB_Click(object sender, EventArgs e)
        {
            usb.Refresh();
            RecordUSBInfo("搜索USB");
            RefreshUSBList();

        }

        /// <summary>
        /// 刷新USB口下拉列表
        /// </summary>
        /// <param name="box"></param>
        private void RefreshUSBList()
        {
            int USBSum = usb.Count;
            if (USBSum == 0)
            {
                cbbUSBdevice.Items.Clear();
                RecordUSBInfo("无USB接入");
            }
            else
            {
                cbbUSBdevice.Items.Clear();
                for (int i = 0; i < USBSum; i++)
                {
                    string strDisplay = i.ToString() + "  " + usb.List[i].pname;
                    cbbUSBdevice.Items.Add(strDisplay);
                    cbbUSBdevice.SelectedIndex = i;    //配置索引序号
                }
                RecordUSBInfo( string.Format("存在 {0} 个USB !",USBSum));
            }

        }

        private void cbbUSB_SelectedIndexChanged(object sender, EventArgs e)
        {
            USBSelect = cbbUSBdevice.SelectedIndex;

            if (usb != null && usb.List.Count != 0)
            {
                tbPID.Text = usb.List[USBSelect].pid.ToString("X4");
                tbVID.Text = usb.List[USBSelect].vid.ToString("X4");
                tbPVN.Text = usb.List[USBSelect].pvn.ToString("X4");
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (USBSelect < 0 || cbbUSBdevice.SelectedIndex < 0)
            {
                btnConnect.Text = "打开";
                return;
            }

            if (btnConnect.Text == "打开")
            {
                if (usb.Connect(USBSelect))
                {
                    btnConnect.Text = "关闭";

                    cancelSource_R = new CancellationTokenSource();
                    thread_R = new Thread(GetUSBReceive);
                    thread_R.IsBackground = true;
                    thread_R.Start();

                    cbbUSBdevice.Enabled = false;
                    RecordUSBInfo(string.Format("{0} 成功连接", usb.List[USBSelect].pname));

                    timer1.Start();
                }
            }
            else
            {
                if (usb.Close(USBSelect))
                {
                    btnConnect.Text = "打开";
                    cbbUSBdevice.Enabled = true;
                    RecordUSBInfo(string.Format("{0} 成功关闭", usb.List[USBSelect].pname));

                    cancelSource_R.Cancel();
                    timer1.Stop();
                }
            }
        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            DataSend();
        }

        private void DataSend()
        {
            if (USBSelect < 0)
            {
                RecordUSBInfo(string.Format("未打开USB连接"));
                return;
            }
            string sbuff = tbSend.Text;
            if (cbHex.Checked)
            { }
            else
            {
                byte[] sData = Encoding.Default.GetBytes(sbuff);
                //byte[] buff = new byte[64];
                if (sData.Length / 64 == 0)
                {
                    usb.Send(USBSelect, sData);
                }
                else
                {
                    int tempX = sData.Length / 64;
                    int tempY = sData.Length % 64;
                    for (int i = 0; i <= tempX; i++)
                    {
                        byte[] buff = sData.Skip(i * 64).Take(i == tempX?tempY:64).ToArray();
                        usb.Send(USBSelect, buff);
                        Thread.Sleep(10);
                    }

                }
               
                
            }
        }

        private void GetUSBReceive()
        {
            while (!cancelSource_R.IsCancellationRequested)
            {
                usb.Recv(USBSelect);
                if (recvLen == usb.RecvLen)
                {
                    continue;
                }
                recvLen = usb.RecvLen;
                RecordUSBInfo(string.Format("已接收" + recvLen * 64 + "个字节"));
                while (ReadNum < Rebuff.Count)
                {
                    RecordReceiveInfo(Rebuff[ReadNum]);
                    ReadNum++;
                }
            }
            cancelSource_R.Dispose();
        }



        byte[] RecvBuffer = new byte[64];
        
        List<byte> l_recvbuffer = new List<byte>();
        byte[] RevData = new byte[1000];

        int ReadNum = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //usb.Recv(USBSelect);
            //if (recvLen == usb.RecvLen)
            //{
            //    return;
            //}
            //recvLen = usb.RecvLen;
            //RecordUSBInfo(string.Format("已接收" + recvLen * 64 + "个字节"));
            //while (ReadNum < Rebuff.Count)
            //{
            //    RecordReceiveInfo(Rebuff[ReadNum]);
            //    ReadNum++;
            //}

        }


        private void USBInfoClear_Click(object sender, EventArgs e)
        {
            tbUSBInfo.Clear();
        }
        private void ReceiveClear_Click(object sender, EventArgs e)
        {
            tbReceive.Clear();
        }
    }
}
