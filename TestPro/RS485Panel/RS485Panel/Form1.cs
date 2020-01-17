using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;

namespace RS485Panel
{
    public partial class Form1 : Form
    {
        #region  接口定义
        /// <summary>
        /// 自定义485协议对象
        /// </summary>
        CustomRS485 Com = new CustomRS485();


        /// <summary>
        /// 处理串口通讯，需要自动发送部分
        /// </summary>
        private Thread thread = null;
        #endregion

        #region 数据定义
        /// <summary>
        /// 串口项内容
        /// </summary>
        int ComPort = 0;
        /// <summary>
        /// 串口波特率
        /// </summary>
        int ComRate = 9600;
        /// <summary>
        /// 从机地址
        /// </summary>
        int ComAddr = 1;
        /// <summary>
        /// 通讯状态：false-未通讯 true-连接通讯
        /// </summary>
        bool ComState = false;
        /// <summary>
        /// 波特率项
        /// </summary>
        int[] BaudItem = { 9600, 115200 };

        /// <summary>
        /// 固定输入口状态
        /// </summary>
        byte State_FixedInput = 0;
        /// <summary>
        /// 固定输出口状态
        /// </summary>
        byte State_FixedOutput = 0;

        /// <summary>
        /// 可调口类型
        /// </summary>
        byte Type_IO = 0;
        byte Type_IOState = 0;

        #endregion







        /// <summary>
        /// 串口下拉列表
        /// </summary>
        /// <param name="box"></param>
        private void comConf_ReflashPort(ComboBox box)
        {
            box.Items.Clear();
            box.Items.Add("刷新");  //配置选项
            box.SelectedIndex = 0;    //配置索引序号
            string[] ports = SerialPort.GetPortNames();
            for (int i = 1; i <= ports.Length; i++)
            {
                box.Items.Add(ports[i-1]);  //配置选项
                box.SelectedIndex = i;    //配置索引序号
            }
        }

        /// <summary>
        /// 波特率下拉列表
        /// </summary>
        /// <param name="box"></param>
        private void comConf_BaudRate(ComboBox box)
        {          
            for (int i = 0; i < BaudItem.Length; i++)
            {
                box.Items.Add(BaudItem[i]);  //配置选项
                box.SelectedIndex = i;    //配置索引序号
            }
        }

        /// <summary>
        /// 串口选择，关闭下拉列表时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbCom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbCom.SelectedIndex == 0)
            {
                comConf_ReflashPort(cbbCom);
                cbbCom.SelectedIndex = ComPort;
            }
            else
            {
                ComPort = cbbCom.SelectedIndex;

            }
        }

        /// <summary>
        /// 波特率更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbRate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComRate = BaudItem[cbbRate.SelectedIndex];
        }

        /// <summary>
        /// 从机地址更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAddr_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ComAddr = Convert.ToInt32(tbAddr.Text);
            }
            catch
            {
                tbAddr.Text = ComAddr.ToString();
            }


        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="info"></param>
        public void RecodeInfo(string info)
        {
            tbRecode.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff ") + " " + info + "\r\n");
        }


        public Form1()
        {
            InitializeComponent();
            comConf_ReflashPort(cbbCom);
            cbbCom.SelectedIndex = ComPort;
            comConf_BaudRate(cbbRate);
            cbbRate.SelectedIndex = 0;
            tbAddr.Text = "1";
        }

        /// <summary>
        /// 点击串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ComState)
                {
                    string str = (string)cbbCom.SelectedItem;
                    string[] sArray = Regex.Split(str, "COM", RegexOptions.IgnoreCase);
                    Com.Port = Convert.ToInt32(sArray[1]);
                    Com.BaudRate = ComRate;
                    Com.Open();         //打开串口
                    ComState = true;
                    btnOpen.Text = "关闭";

                    thread = new Thread(thread_AutoDeal);
                    thread.Start();
                    timer1.Enabled = true;
                }
                else
                {
                    Com.Close();         //关闭串口
                    timer1.Enabled = false;
                    if(thread != null) thread.Abort();
                    ComState = false;
                    btnOpen.Text = "打开";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        /// <summary>
        /// 线程-处理自动检测任务
        /// </summary>
        private void thread_AutoDeal()
        {
            Byte[] Rmsg = new byte[7];
            while (true)
            {
                Thread.Sleep(30);

                Rmsg = Com.Query_AllFixdInput(ComAddr,0xFF);    //查固定输入
                if (Rmsg != null && Rmsg[5] == 0)
                {
                    State_FixedInput = Rmsg[4];
                }
                Rmsg = Com.Query_AllFixdOutput(ComAddr,0xFF);   //查固定输出???
                if (Rmsg != null && Rmsg[5] == 0)
                {
                    State_FixedOutput = Rmsg[4];
                }
                Rmsg = Com.Query_TypeIO(ComAddr, 0xFF);         //查可调类型
                if (Rmsg != null && Rmsg[5] == 0)
                {
                    Type_IO = Rmsg[4];
                }
                Rmsg = Com.Query_TypeIO_State(ComAddr, 0xFF);   //查可调输入输出状态
                if (Rmsg != null && Rmsg[5] == 0)
                {
                    Type_IOState = Rmsg[4];
                }

            }
        }

        /// <summary>
        /// 退出关闭所有线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0); //关闭主窗体时，关闭所有线程
        }

        Color MyGray = Color.FromArgb(255, 105, 105, 105);
        /// <summary>
        /// 刷新显示数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbLedFixedX0.BackColor = (State_FixedInput & 1 << 0) == 0 ? MyGray : Color.Green;
            lbLedFixedX1.BackColor = (State_FixedInput & 1 << 1) == 0 ? MyGray : Color.Green;
            lbLedFixedX2.BackColor = (State_FixedInput & 1 << 2) == 0 ? MyGray : Color.Green;
            lbLedFixedX3.BackColor = (State_FixedInput & 1 << 3) == 0 ? MyGray : Color.Green;
            lbLedFixedX4.BackColor = (State_FixedInput & 1 << 4) == 0 ? MyGray : Color.Green;
            lbLedFixedX5.BackColor = (State_FixedInput & 1 << 5) == 0 ? MyGray : Color.Green;
            lbLedFixedX6.BackColor = (State_FixedInput & 1 << 6) == 0 ? MyGray : Color.Green;
            lbLedFixedX7.BackColor = (State_FixedInput & 1 << 7) == 0 ? MyGray : Color.Green;

            lbLedFixedY0.BackColor = (State_FixedOutput & 1 << 0) == 0 ? MyGray : Color.Green;
            lbLedFixedY1.BackColor = (State_FixedOutput & 1 << 1) == 0 ? MyGray : Color.Green;
            lbLedFixedY2.BackColor = (State_FixedOutput & 1 << 2) == 0 ? MyGray : Color.Green;
            lbLedFixedY3.BackColor = (State_FixedOutput & 1 << 3) == 0 ? MyGray : Color.Green;
            lbLedFixedY4.BackColor = (State_FixedOutput & 1 << 4) == 0 ? MyGray : Color.Green;
            lbLedFixedY5.BackColor = (State_FixedOutput & 1 << 5) == 0 ? MyGray : Color.Green;
            lbLedFixedY6.BackColor = (State_FixedOutput & 1 << 6) == 0 ? MyGray : Color.Green;
            lbLedFixedY7.BackColor = (State_FixedOutput & 1 << 7) == 0 ? MyGray : Color.Green;

            btnTypeSet0.Text = (Type_IO & 1 << 0) == 0 ? "X0" : "Y0";
            btnTypeSet1.Text = (Type_IO & 1 << 1) == 0 ? "X1" : "Y1";
            btnTypeSet2.Text = (Type_IO & 1 << 2) == 0 ? "X2" : "Y2";
            btnTypeSet3.Text = (Type_IO & 1 << 3) == 0 ? "X3" : "Y3";
            btnTypeSet4.Text = (Type_IO & 1 << 4) == 0 ? "X4" : "Y4";
            btnTypeSet5.Text = (Type_IO & 1 << 5) == 0 ? "X5" : "Y5";
            btnTypeSet6.Text = (Type_IO & 1 << 6) == 0 ? "X6" : "Y6";
            btnTypeSet7.Text = (Type_IO & 1 << 7) == 0 ? "X7" : "Y7";

            lbTypeSet0.BackColor = (Type_IOState >> 0 & 0x01) == 0 ? Color.Green : MyGray;
            lbTypeSet1.BackColor = (Type_IOState >> 1 & 0x01) == 0 ? Color.Green : MyGray;
            lbTypeSet2.BackColor = (Type_IOState >> 2 & 0x01) == 0 ? Color.Green : MyGray;
            lbTypeSet3.BackColor = (Type_IOState >> 3 & 0x01) == 0 ? Color.Green : MyGray;
            lbTypeSet4.BackColor = (Type_IOState >> 4 & 0x01) == 0 ? Color.Green : MyGray;
            lbTypeSet5.BackColor = (Type_IOState >> 5 & 0x01) == 0 ? Color.Green : MyGray;
            lbTypeSet6.BackColor = (Type_IOState >> 6 & 0x01) == 0 ? Color.Green : MyGray;
            lbTypeSet7.BackColor = (Type_IOState >> 7 & 0x01) == 0 ? Color.Green : MyGray;
        }

        /// <summary>
        /// 设置固定输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbLedFixedYn_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            Com.Set_AllFixdOutput(ComAddr, 1 << index, ((State_FixedOutput & 1 << index) == 0 ? 1 : 0) << index);
            RecodeInfo("设置固定输出口" + index.ToString());
        }

        /// <summary>
        /// 设置可调输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbLedSetYn_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            if (((Type_IO >> index) & 0x01) == 1)
            {
                Com.Set_AllSetOutput(ComAddr, 1 << index, ((Type_IOState & 1 << index) == 0 ? 1 : 0) << index);
                RecodeInfo("设置可调输出口"+ index.ToString());
            }
            else
            {
                RecodeInfo("无法改变输入口状态！");
            }
           

            
        }

        private void tbRecode_DoubleClick(object sender, EventArgs e)
        {
            long[] info = Com.GetCheckInfo();
            RecodeInfo("总时间(ms)：" + info[0] + " 总次数：" + info[1] + " 单次耗时(ms)："+info[2]);
        }
    }
}
