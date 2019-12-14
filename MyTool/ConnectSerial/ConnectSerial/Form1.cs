using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

namespace ConnectSerial
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 向上串口
        /// </summary>
        private SerialPort COMU = new SerialPort();
        /// <summary>
        /// 向下串口
        /// </summary>
        private SerialPort COMD = new SerialPort();
        /// <summary>
        /// 通讯超时设置
        /// </summary>
        int timout;

        public Form1()
        {
            InitializeComponent();
            tbTimout.Text = "100";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;    //防止跨线程访问出错，好多地方会用到
            cbbBaud.Items.Clear();                       //清空选项内容
            int[] BaudItem = { 9600, 19200, 115200 };
            for (int i = 0; i < BaudItem.Length; i++)
            {
                cbbBaud.Items.Add(BaudItem[i]);         //配置选项
                cbbBaud.SelectedIndex = i;              //配置索引序号      
            }
            cbbBaud.SelectedItem = cbbBaud.Items[0];    //默认为列表第二个变量 - 115200

            cbbComU.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                cbbComU.Items.Add(ports[i]);             //配置选项
                cbbComU.SelectedIndex = i;               //配置索引序号
            }
            cbbComU.SelectedItem = cbbComU.Items[0];      //默认为列表第1个变量

            cbbComD.Items.Clear();
            for (int i = 0; i < ports.Length; i++)
            {
                cbbComD.Items.Add(ports[i]);             //配置选项
                cbbComD.SelectedIndex = i;               //配置索引序号
            }
            cbbComD.SelectedItem = cbbComD.Items[0];      //默认为列表第1个变量
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="info"></param>
        public void RecodeInfo(string info)
        {
            tbRecode.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff ") + " " + info + "\r\n");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            if (btnConnect.Text.Equals("已连接"))
            {
                COMU.Close();
                COMD.Close();
                btnConnect.Text = "未连接";
                RecodeInfo("串口断开连接");
            }
            else
            {
                btnConnect.Text = "已连接";
                RecodeInfo("串口已连接");

                COMU.ReadTimeout = timout;
                COMU.WriteTimeout = timout;

                COMD.ReadTimeout = timout;
                COMD.WriteTimeout = timout;

                if (COMU.IsOpen)
                    COMU.Close();
                COMU.Open();
                COMU.DataReceived += COMUDataReceived();

                //    try
                //    {
                //        COMU.StopBits = StopBits.One;
                //        COMU.DataBits = 8;
                //        COMU.Parity = Parity.None;

                //        COMU.ReadTimeout = timout;
                //        COMU.WriteTimeout = timout;
                //        if (COMU.IsOpen) COMU.Close();
                //        COMU.Open();
                //        COMU.ReadExisting();
                //        //设置数据接收事件
                //        COMU.ReceivedBytesThreshold = 1;

                //        threadU = new Thread(thread_U_D);
                //        threadU.Start();


                //        COMD.StopBits = StopBits.One;
                //        COMD.DataBits = 7;
                //        COMD.Parity = Parity.Even;

                //        COMD.ReadTimeout = timout;
                //        COMD.WriteTimeout = timout * 4;
                //        if (COMD.IsOpen) COMD.Close();
                //        COMD.Open();
                //        COMD.ReadExisting();
                //        //设置数据接收事件
                //        COMD.ReceivedBytesThreshold = 1;

                //        threadD = new Thread(thread_D_U);
                //        threadD.Start();


                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
            }




        }

        /// <summary>
        /// 超时变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTimout_TextChanged(object sender, EventArgs e)
        {
            if (tbTimout.Text != null)
            {
                timout = Convert.ToInt32(tbTimout.Text);
            }
            
        }

        private void cbbBaud_SelectedValueChanged(object sender, EventArgs e)
        {
            COMU.BaudRate = (int)cbbBaud.SelectedItem;
            COMD.BaudRate = COMU.BaudRate;
        }

        private void cbbComD_SelectedIndexChanged(object sender, EventArgs e)
        {
            COMD.PortName = (string)cbbComD.SelectedItem;
        }

        private void cbbComU_SelectedIndexChanged(object sender, EventArgs e)
        {
            COMU.PortName = (string)cbbComU.SelectedItem;
        }

        void COMUDataReceived()
        {

        }
    }
}
