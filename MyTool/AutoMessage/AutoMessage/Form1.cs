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
using System.Threading;
using System.IO;
namespace AutoMessage
{
    public partial class Form1 : Form
    {
        public struct MsgData
        {
            public string cmd;
            public string feedback;
        }

        private SerialPort COM = new SerialPort();
        private Thread thread = null;                   //线程句柄-处理通讯过程
        private MsgData[] MainData = new MsgData[255];

        /// <summary>
        /// 指令序号
        /// </summary>
        int index = 0;

        /// <summary>
        /// 接收次数
        /// </summary>
        int ReNum = 0;

        /// <summary>
        /// 串口通讯选项配置
        /// </summary>
        private void ComConf(ComboBox combox, ComboBox baudbox)
        {
            CheckForIllegalCrossThreadCalls = false;    //防止跨线程访问出错，好多地方会用到
            combox.Items.Clear();                       //清空选项内容
            baudbox.Items.Clear();
            int[] BaudItem = { 9600,19200, 115200};
            for (int i = 0; i < BaudItem.Length; i++)
            {
                baudbox.Items.Add(BaudItem[i]);         //配置选项
                baudbox.SelectedIndex = i;              //配置索引序号      
            }
            baudbox.SelectedItem = baudbox.Items[1];    //默认为列表第二个变量 - 115200

            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                combox.Items.Add(ports[i]);             //配置选项
                combox.SelectedIndex = i;               //配置索引序号
            }
            combox.SelectedItem = combox.Items[0];      //默认为列表第1个变量
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
        private void thread_Refresh()
        {
            byte[] bytes = new byte[255];
            byte count = 0;
            string Rebuff = "";
            string Stbuff = "";
            while (true)
            {
                try
                {
                    bytes[count] = (byte)COM.ReadByte();
                    count++;
                }
                catch
                {
                    if(count != 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            Rebuff += Convert.ToString(bytes[i] / 0x10, 16);      //10进制转为16进制
                            Rebuff += Convert.ToString(bytes[i] % 0x10, 16);      //10进制转为16进制
                            if(i != (count-1)) Rebuff += " ";      //10进制转为16进制
                        }

                        RecodeInfo("\r\n接收数据：" + Rebuff.ToUpper());
                        ReNum++;
                        label1.Text = "接收次数：" + ReNum.ToString();
                        Stbuff = "未指定此响应指令";
                        for (int i = 0;i < MainData.Length; i++)
                        {
                            if (Rebuff.ToUpper().Equals(MainData[i].cmd.ToUpper()))
                            {
                                Stbuff = MainData[i].feedback;
                                Byte[] StData = strToToHexByte(Stbuff);
                                try
                                {
                                    COM.Write(StData, 0, StData.Length);
                                }
                                catch
                                {
                                    RecodeInfo("\r\n发送失败！");
                                }
                                
                                break;
                            }
                        }
                        

                        RecodeInfo("\r\n发送数据：" + Stbuff.ToUpper());

                        count = 0;
                        Array.Clear(bytes, 0, bytes.Length);
                        Rebuff = "";
                    }
                   
                }


            }
        }


        public Form1()
        {
            InitializeComponent();
            ComConf(cbbCom, cbbBaud);
            btnConnect.Text = "未连接";
            cbHex.Checked = true;              //默认选择16进制数据
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text.Equals("已连接"))
            {
                COM.Close();
                btnConnect.Text = "未连接";
                RecodeInfo("串口未连接");
                thread.Abort();
            }
            else
            {
                try
                {
                    COM.StopBits = StopBits.One;
                    COM.DataBits = 8;

                    COM.ReadTimeout = 3;
                    COM.WriteTimeout = 3;
                    COM.Open();
                    COM.ReadExisting();
                    //设置数据接收事件
                    COM.ReceivedBytesThreshold = 1;

                    thread = new Thread(thread_Refresh);
                    thread.Start();

                    btnConnect.Text = "已连接";
                    RecodeInfo("串口已连接");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        /// <summary>
        /// 设置端口号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM.PortName = (string)cbbCom.SelectedItem;
        }

        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            COM.BaudRate = (int)cbbBaud.SelectedItem;
        }


        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (index != 0)
            {
                MainData[index-1].cmd = tbReceive1.Text;
                MainData[index-1].feedback = tbSent1.Text;

                using (StreamWriter sw = new StreamWriter("CMD.txt"))
                {
                    foreach (MsgData s in MainData)
                    {
                        sw.WriteLine(s.cmd);
                        sw.WriteLine(s.feedback);
                    }
                }

            }
            else
            {
                MessageBox.Show("请先选择指令序号！","提示");
            }
            btnEnter.Enabled = false;
        }
        /// <summary>
        /// 根据按钮序号，存储需反馈指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectItem(object sender, EventArgs e)
        {
            index = int.Parse((sender as Control).Tag.ToString());
            lbReceive1.Text = "接收"+ index + "：";
            lbSent1.Text    = "发送"+ index + "：";
            tbReceive1.Text = MainData[index-1].cmd;
            tbSent1.Text = MainData[index-1].feedback;
            btnEnter.Enabled = true;
        }

        private void Activa_Enter(object sender, EventArgs e)
        {
            if (index == 0)
            {
                MessageBox.Show("请先选择指令序号！", "提示");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName = "CMD.txt";
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = "C:\\";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }

                this.Name = fileName;



                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader m_streamReader = new StreamReader(fs);

                string line;
                for (int i = 0; i < MainData.Length; i++)
                {
                    line = m_streamReader.ReadLine();
                    MainData[i].cmd = line;
                    line = m_streamReader.ReadLine();
                    MainData[i].feedback = line;
                }

                fs.Close();

            }
            catch (Exception ei)
            {
                MessageBox.Show(ei.Message, "提示");
            }
        }

        private void tsmiRename(object sender, EventArgs e)
        {
            Button button = (Button)sender;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            COM.Close();
        }

        private void btnReduce_Click(object sender, EventArgs e)
        {
            if (index > 1)
            {
                index--;
                lbReceive1.Text = "接收" + index + "：";
                lbSent1.Text = "发送" + index + "：";
                tbReceive1.Text = MainData[index - 1].cmd;
                tbSent1.Text = MainData[index - 1].feedback;
                btnEnter.Enabled = true;
            }
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            index++;
            lbReceive1.Text = "接收" + index + "：";
            lbSent1.Text = "发送" + index + "：";
            tbReceive1.Text = MainData[index - 1].cmd;
            tbSent1.Text = MainData[index - 1].feedback;
            btnEnter.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0); //关闭主窗体时，关闭所有线程
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ReNum = 0;
            label1.Text = "接收次数：" + ReNum.ToString();
        }
    }
}
