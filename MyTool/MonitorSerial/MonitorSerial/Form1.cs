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



namespace MonitorSerial
{
    public partial class Form1 : Form
    {
        private SerialPort COMU = new SerialPort();
        private SerialPort COMD = new SerialPort();
        /// <summary>
        /// 线程句柄-处理通讯过程
        /// </summary>
        private Thread threadU = null;
        private Thread threadD = null;
        /// <summary>
        /// 通讯超时设置
        /// </summary>
        int timout;
        //private MsgData MainData = new MsgData();
        /// <summary>
        /// 串口通讯选项配置
        /// </summary>
        private void ComConf(ComboBox combox1, ComboBox combox2, ComboBox baudbox)
        {
            CheckForIllegalCrossThreadCalls = false;    //防止跨线程访问出错，好多地方会用到
            combox1.Items.Clear();                       //清空选项内容
            combox2.Items.Clear();                       //清空选项内容
            baudbox.Items.Clear();
            int[] BaudItem = { 9600, 19200, 115200 };
            for (int i = 0; i < BaudItem.Length; i++)
            {
                baudbox.Items.Add(BaudItem[i]);         //配置选项
                baudbox.SelectedIndex = i;              //配置索引序号      
            }
            baudbox.SelectedItem = baudbox.Items[0];    //默认为列表第二个变量 - 115200

            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                combox1.Items.Add(ports[i]);             //配置选项
                combox1.SelectedIndex = i;               //配置索引序号
            }
            combox1.SelectedItem = combox1.Items[0];      //默认为列表第1个变量

            for (int i = 0; i < ports.Length; i++)
            {
                combox2.Items.Add(ports[i]);             //配置选项
                combox2.SelectedIndex = i;               //配置索引序号
            }
            combox2.SelectedItem = combox2.Items[0];      //默认为列表第1个变量
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="info"></param>
        public void RecodeInfo(string info)
        {
            tbRecode.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff ") + " " + info + "\r\n");
        }


        /// <summary>
        /// PLC用求和检验
        /// </summary>
        /// <param name="bytes">字节组</param>
        /// <param name="start">起始位置</param>
        /// <param name="end">结束位置</param>
        public bool CheckSumForPLCMD(byte[] bytes,int start,int end)
        {
            byte PLC_STX = 0x02;
            byte PLC_ETX = 0x03;
            byte[] PLC_Single = { 0x05, 0x06, 0x15 };
            int len = end - start + 1;
            int Sum = 0;
            char[] MyASCII = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            foreach (byte sin in PLC_Single)
            {
                if (sin == bytes[start]) return true;
            }
            if (bytes[start] == PLC_STX && bytes[end-3] == PLC_ETX)
            {
                for (int i = 1; i < len-3; i++) //去除首-报文开始指令  去除尾-和校验结果
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

        public bool CheckSumForPLCMD(byte[] bytes, int start)
        {
            byte PLC_STX = 0x02;
            byte PLC_ETX = 0x03;
            byte[] PLC_Single = { 0x05, 0x06, 0x15 };
            int len = bytes.Length - start;
            int Sum = 0;
            int Sign = 0;
            char[] MyASCII = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            foreach (byte sin in PLC_Single)
            {
                if (sin == bytes[start]) return true;
            }
            if (bytes[start] == PLC_STX)
            {
                for (int i = 1; i < len - 3; i++) //去除首-报文开始指令  去除尾-和校验结果
                {
                    Sum += bytes[i];
                    if (bytes[i] == PLC_ETX)
                    {
                        Sign = i;
                        break;
                    }

                }
                Sum &= 0xFF;

                int SumFirst = (int)Convert.ToInt32(MyASCII[Sum / 0x10]);

                int SumSecond = (int)Convert.ToInt32(MyASCII[Sum % 0x10]);

                if (SumFirst == bytes[Sign + 1] && SumSecond == bytes[Sign + 2]) return true;
            }
            return false;
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text.Equals("已连接"))
            {
                COMU.Close();
                COMD.Close();
                btnConnect.Text = "未连接";
                RecodeInfo("串口未连接");
                threadU.Abort();
                threadD.Abort();
            }
            else
            {
                try
                {
                    COMU.StopBits = StopBits.One;
                    COMU.DataBits = 7;
                    COMU.Parity = Parity.Even;

                    COMU.ReadTimeout = timout;
                    COMU.WriteTimeout = timout * 4;
                    if (COMU.IsOpen) COMU.Close();
                    COMU.Open();
                    COMU.ReadExisting();
                    //设置数据接收事件
                    COMU.ReceivedBytesThreshold = 1;

                    threadU = new Thread(thread_U_D);
                    threadU.Start();


                    COMD.StopBits = StopBits.One;
                    COMD.DataBits = 7;
                    COMD.Parity = Parity.Even;

                    COMD.ReadTimeout = timout ;
                    COMD.WriteTimeout = timout * 4;
                    if (COMD.IsOpen) COMD.Close();
                    COMD.Open();
                    COMD.ReadExisting();
                    //设置数据接收事件
                    COMD.ReceivedBytesThreshold = 1;

                    threadD = new Thread(thread_D_U);
                    threadD.Start();

                    btnConnect.Text = "已连接";
                    RecodeInfo("串口已连接");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    btnConnect.Text = "未连接";
                }
            }
        }
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 接收上位机数据，转发给下位机数据
        /// </summary>
        private void thread_U_D()
        {
            byte[] bytes = new byte[1000];
            int count = 0;
            string Rebuff = "";
            while (true)
            {
                ReadCOMU:
                while (true)
                {
                    try
                    {
                        bytes[count] = (byte)COMU.ReadByte();
                        count++;
                    }
                    catch
                    {
                        break;
                    }
                }
                if ((cbCheckSum.Checked == true) && (count > 5) && (CheckSumForPLCMD(bytes, 0, count) == false))
                    goto ReadCOMU;

                if (count != 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        Rebuff += Convert.ToString(bytes[i] / 0x10, 16);      //10进制转为16进制
                        Rebuff += Convert.ToString(bytes[i] % 0x10, 16);      //10进制转为16进制
                        if (i != (count - 1)) Rebuff += " ";      //10进制转为16进制
                    }

                    RecodeInfo("\r\n上方接收数据：" + Rebuff.ToUpper());

                    try
                    {
                        COMD.Write(bytes, 0, count);
                    }
                    catch /*(Exception ex)*/
                    {
                        RecodeInfo("发送U失败！");
                    }
                }
                count = 0;
                Array.Clear(bytes, 0, bytes.Length);
                Rebuff = "";
            }
        }


        /// <summary>
        /// 接收下位机数据，转发给上位机数据
        /// </summary>
        private void thread_D_U()
        {
            byte[] bytes1 = new byte[1000];
            int count1 = 0;
            string Rebuff1 = "";
            while (true)
            {
                ReadCode:
                while (true)
                {
                    try
                    {
                        bytes1[count1] = (byte)COMD.ReadByte();
                        count1++;
                    }
                    catch /*(Exception ex)*/
                    {
                        //MessageBox.Show(ex.Message);
                        break;
                    }
                }
                if ((cbCheckSum.Checked == true) && (count1 > 5) && (CheckSumForPLCMD(bytes1, 0, count1) == false))
                    goto ReadCode;

                if (count1 != 0)
                {
                    for (int i = 0; i < count1; i++)
                    {
                        Rebuff1 += Convert.ToString(bytes1[i] / 0x10, 16);      //10进制转为16进制
                        Rebuff1 += Convert.ToString(bytes1[i] % 0x10, 16);      //10进制转为16进制
                        if (i != (count1 - 1)) Rebuff1 += " ";      //10进制转为16进制
                    }

                    RecodeInfo("\r\n下方接收U数据：" + Rebuff1.ToUpper());
                    try
                    {
                        COMU.Write(bytes1, 0, count1);
                        //AbleState = false;
                    }
                    catch
                    {
                        //MessageBox.Show("向上发送失败！", "提示");
                        RecodeInfo("发送D失败！");
                    }

                }

                count1 = 0;
                Array.Clear(bytes1, 0, bytes1.Length);
                Rebuff1 = "";
            }

        }

        public Form1()
        {
            InitializeComponent();
            btnConnect.Text = "未连接";
            ComConf(cbbComU,cbbComD,cbbBaud);
            tbTimout.Text = "100";
            timout = 100;
            cbCheckSum.Checked = true;
        }

        private void cbbComU_SelectedIndexChanged(object sender, EventArgs e)
        {
            COMU.PortName = (string)cbbComU.SelectedItem;
        }

        private void cbbComD_SelectedIndexChanged(object sender, EventArgs e)
        {
            COMD.PortName = (string)cbbComD.SelectedItem;
        }

        private void cbbBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            COMU.BaudRate = (int)cbbBaud.SelectedItem;
            COMD.BaudRate = COMU.BaudRate;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            COMU.Close();
            COMD.Close();
            System.Environment.Exit(0); //关闭主窗体时，关闭所有线程         
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbRecode.Clear();
        }

        private void tbTimout_Leave(object sender, EventArgs e)
        {
            timout = Convert.ToInt32(tbTimout.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}




















//bytes1[count1] = (byte)COMD.ReadByte();
//count1++;
//if (bytes1[0] == 0x02)
//{
//    if (count1 < 5) continue;
//    if (count1 > bytes1.Length - 2) break;

//    for (int i = 1; i < count1 - 2; i++)
//    {
//        Sum += bytes1[i];
//    }
//    byte num1 = (byte)(Sum / 0x10 + 30);
//    byte num2 = (byte)(Sum % 0x10 + 30);
//    if (num1 == bytes1[count1 - 2] && num2 == bytes1[count1 - 1]) break;
//}
//else break;














