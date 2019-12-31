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
        private StringBuilder builderU = new StringBuilder();
        /// <summary>
        /// 向下串口
        /// </summary>
        private SerialPort COMD = new SerialPort();
        private StringBuilder builderD = new StringBuilder();

        /// <summary>
        /// 通讯超时设置
        /// </summary>
        int timout = 0;
        /// <summary>
        /// 判断串口是否处于有效监听状态
        /// </summary>
        bool Listening = false;

        private object syncRoot = new object();

        public Form1()
        {
            InitializeComponent();
            tbTimout.Text = "100";
        }
        /// <summary>
        /// 窗体加载中，配置下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            lock (syncRoot)
            {
                tbRecode.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff ") + " " + info + "\r\n");
            }
            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            #region 断开连接串口
            if (btnConnect.Text.Equals("已连接"))
            {
                COMU.Close();
                COMD.Close();
                btnConnect.Text = "未连接";
                RecodeInfo("串口断开连接");
            }
            #endregion
            #region 开始连接串口
            else
            {
                btnConnect.Text = "已连接";
                RecodeInfo("串口已连接");
                try
                {
                    if (COMU.IsOpen)
                        COMU.Close();
                    COMU.Open();
                    COMU.DataReceived += COMUDataReceived;

                    if (COMD.IsOpen)
                        COMD.Close();
                    COMD.Open();
                    COMD.DataReceived += COMDataReceived;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    btnConnect.Text = "未连接";
                }
            }
            #endregion
        }

        /// <summary>
        /// 记录串口超时超时变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTimout_TextChanged(object sender, EventArgs e)
        {
            if (tbTimout.Text != null)
            {
                timout = Convert.ToInt32(tbTimout.Text);
                COMU.ReadTimeout = timout;
                COMU.WriteTimeout = timout;
                COMD.ReadTimeout = timout;
                COMD.ReadTimeout = timout;
            }        
        }

        /// <summary>
        /// 记录波特率选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbBaud_SelectedValueChanged(object sender, EventArgs e)
        {
            COMU.BaudRate = (int)cbbBaud.SelectedItem;
            COMD.BaudRate = COMU.BaudRate;
        }

        /// <summary>
        /// 记录向下端口选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbComD_SelectedIndexChanged(object sender, EventArgs e)
        {
            COMD.PortName = (string)cbbComD.SelectedItem;
        }

        /// <summary>
        /// 记录向上端口选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbComU_SelectedIndexChanged(object sender, EventArgs e)
        {
            COMU.PortName = (string)cbbComU.SelectedItem;
        }


        List<byte> listU = new List<byte>();
        /// <summary>
        /// 接收完整的上层数据并发送到下层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void COMUDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string UpToDownBuff = "";
            if (!COMU.IsOpen) return;
            if (!COMD.IsOpen) return;
            try
            {
                Listening = true;//设置标记，说明我已经开始处理数据，一会儿要使用系统UI的。
                int n = COMU.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据

                COMU.Read(buf, 0, n);//读取缓冲数据
                builderU.Clear();//清除字符串构造器的内容

                listU.AddRange(buf);
                byte[] ListBuff = new byte[listU.LongCount()];
                listU.CopyTo(ListBuff);
                if (cbPLCSum.Checked)
                {
                    if (!CheckSumForPLCMD(ListBuff, 0, ListBuff.Length))
                    {
                        return;
                    }
                }


                COMD.Write(ListBuff, 0, ListBuff.Length);
                //判断是否是显示为16进制
                if (cbHexView.Checked)
                {
                    //依次的拼接出16进制字符串
                    foreach (byte b in ListBuff)
                    {
                        builderU.Append(b.ToString("X2") + " ");
                    }
                }
                else
                {
                    //直接按ASCII规则转换成字符串
                    builderU.Append(Encoding.ASCII.GetString(ListBuff));
                }

                UpToDownBuff = builderU.ToString();
                listU.Clear();
                RecodeInfo("向下发送:" + UpToDownBuff);

            }
            finally
            {
                Listening = false;//我用完了，ui可以关闭串口了。
            }
        }

        List<byte> listD = new List<byte>();
        /// <summary>
        /// 接收完整的下层数据并发送到上层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void COMDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string DownToUpBuff = "";
            if (!COMU.IsOpen) return;
            if (!COMD.IsOpen) return;
            try
            {
                Listening = true;//设置标记，说明我已经开始处理数据，一会儿要使用系统UI的。
                int n = COMD.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据

                COMD.Read(buf, 0, n);//读取缓冲数据
                builderD.Clear();//清除字符串构造器的内容

                listD.AddRange(buf);
                byte[] ListBuff = new byte[listD.LongCount()];
                listD.CopyTo(ListBuff);
                if (cbPLCSum.Checked)
                {
                    if (!CheckSumForPLCMD(ListBuff, 0, ListBuff.Length))
                    {
                        return;
                    }
                }


                COMU.Write(ListBuff, 0, ListBuff.Length);
                //判断是否是显示为16进制
                if (cbHexView.Checked)
                {
                    //依次的拼接出16进制字符串
                    foreach (byte b in ListBuff)
                    {
                        builderD.Append(b.ToString("X2") + " ");
                    }
                }
                else
                {
                    //直接按ASCII规则转换成字符串
                    builderD.Append(Encoding.ASCII.GetString(ListBuff));
                }

                DownToUpBuff = builderD.ToString();
                listD.Clear();
                RecodeInfo("向上发送:" + DownToUpBuff);

            }
            finally
            {
                Listening = false;//我用完了，ui可以关闭串口了。
            }
        }


        /// <summary>
        /// PLC用求和检验
        /// </summary>
        /// <param name="bytes">字节组</param>
        /// <param name="start">起始位置</param>
        /// <param name="end">结束位置</param>
        public bool CheckSumForPLCMD(byte[] bytes, int start, int end)
        {
            byte PLC_STX = 0x02;
            byte PLC_ETX = 0x03;
            byte[] PLC_Single = { 0x05, 0x06, 0x15 };
            int len = end - start + 1;
            int Sum = 0;
            char[] MyASCII = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            foreach (byte sin in PLC_Single)
            {
                if (sin == bytes[start]) return true;      //特殊报文
            }
            if (end < 5) return false;      //数据过短

            if (bytes[start] == PLC_STX && bytes[end - 3] == PLC_ETX)
            {
                for (int i = 1; i < len - 3; i++) //去除首-报文开始指令  去除尾-和校验结果
                {
                    Sum += bytes[i];
                }
                Sum &= 0xFF;

                int SumFirst = (int)Convert.ToInt32(MyASCII[Sum / 0x10]);

                int SumSecond = (int)Convert.ToInt32(MyASCII[Sum % 0x10]);

                if (SumFirst == bytes[end - 2] && SumSecond == bytes[end - 1]) return true;
            }
            return false;
        }

        /// <summary>
        /// 清空记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbRecode.Clear();
            builderU.Clear();
            builderD.Clear();
            listU.Clear();
            listD.Clear();
        }
    }
}
