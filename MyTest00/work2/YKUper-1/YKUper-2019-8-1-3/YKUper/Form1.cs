using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yungku.Common.IOCard.DataDeal;
using System.IO.Ports;
using Yungku.Common.IOCardS1;
using Yungku.Common.IOCardS2;

namespace YKUper
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 数据实例
        /// </summary>
        public DataDeal data = new DataDeal();
        /// <summary>
        /// 串口实例
        /// </summary>
        public YKS1Card MainCom1 = new YKS1Card();

        /// <summary>
        /// 串口实例
        /// </summary>
        public YKS2Card MainCom2 = new YKS2Card();

        /// <summary>
        /// 窗体初始化
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体控件载入过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            tstbNetip.Text = "192.168.1.100";          //网络通讯-IP
            tstbNetport.Text = "4000";                   //网络通讯-port
            tstbNetime.Text = "300";                    //网络通讯-timeout
            tstbComtime.Text = "300";                    //串口通讯-timeout
            ComConf();
            UperConf();
        }


        //*****************************************************************************

        private void RecodeInfo(string info)
        {
            tbRecode.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff ") + " " + info + "\r\n");
        }

        /// <summary>
        /// 串口通讯选项配置
        /// </summary>
        private void ComConf()
        {
            CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到
            int[] BaudItem = { 9600, 115200 };
            for (int i = 0; i < BaudItem.Length; i++)
            {
                tscbCombaud.Items.Add(BaudItem[i]);  //配置选项
                tscbCombaud.SelectedIndex = i;    //配置索引序号      
            }
            tscbCombaud.SelectedItem = tscbCombaud.Items[1];    //默认为列表第二个变量 - 115200

            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                tscbComport.Items.Add(ports[i]);  //配置选项
                tscbComport.SelectedIndex = i;    //配置索引序号
            }
            tscbComport.SelectedItem = tscbComport.Items[3];    //默认为列表第1个变量

        }
        /// <summary>
        /// 上位机选项配置
        /// </summary>
        private void UperConf()
        {
            string[] UperItem = { "自动识别", "S1", "S2" };
            for (int i = 0; i < UperItem.Length; i++)
            {
                tscbVer.Items.Add(UperItem[i]);  //配置选项
                tscbVer.SelectedIndex = i;    //配置索引序号      
            }
            tscbVer.SelectedItem = tscbVer.Items[0];

        }

        /// <summary>
        /// 循环运行三次心跳查询
        /// </summary>
        private bool CheckHeartTimes()
        {
            int checktimes = 3;
            int ver = data.VerUper;
            int num = 0;
            if (ver == 0 || ver == 2)
            {

                MainCom2.Port = data.Comport;
                MainCom2.BaudRate = data.Baudrate;
                MainCom2.Timeout = data.Comtimeout;
                MainCom2.Open();         //打开串口

                num = 0;
                do
                {
                    if (MainCom2.IsExists())
                    {
                        data.VerUper = 2;
                        tscbVer.SelectedIndex = data.VerUper; //更新版本号
                        MainCom2.Close();
                        return true;
                    }
                    num++;
                } while (num <= checktimes);
                MainCom2.Close();
            }
            if (ver == 0 || ver == 1)
            {
                MainCom1.Port = data.Comport;
                MainCom1.Timeout = data.Comtimeout;
                MainCom1.Open();         //打开串口

                num = 0;
                do
                {
                    if (MainCom1.IsExists())
                    {
                        data.VerUper = 1;
                        tscbVer.SelectedIndex = data.VerUper; //更新版本号
                        MainCom1.Close();
                        return true;
                    }
                    num++;
                } while (num <= checktimes);
                MainCom1.Close();
            }

            return false;
        }


        //******************************************************************************

        private void timer1_Tick(object sender, EventArgs e)
        {
            data.SetLocation(data.GetLocation()+1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckHeartTimes())
            {
                RecodeInfo("上位机版本为：" + data.VerUper);
            }
            else { RecodeInfo("自动检测失败！"); }

            panel.Controls.Clear();//移除所有控件
            FormS2 formS2 = new FormS2();
            formS2.Owner = this;
            formS2.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            formS2.TopLevel = false; //指示子窗体非顶级窗体
            this.panel.Controls.Add(formS2);//将子窗体载入panel
            formS2.Show();
        }
        /// <summary>
        /// 同步更新网络ip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tstbNetip_TextChanged(object sender, EventArgs e)
        {
            data.NetIP = tstbNetip.Text;
        }

        /// <summary>
        /// 同步更新网络端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tstbNetport_TextChanged(object sender, EventArgs e)
        {
            data.Netport = Convert.ToInt32(tstbNetport.Text);
        }

        /// <summary>
        /// 同步更新网络超时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tstbNetime_TextChanged(object sender, EventArgs e)
        {
            data.Netimeout = Convert.ToInt32(tstbNetime.Text);
        }
        /// <summary>
        /// 同步更新串口号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbComport_TextChanged(object sender, EventArgs e)
        {
            data.Comport = tscbComport.SelectedIndex;
        }
        /// <summary>
        /// 同步更新串口波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbCombaud_TextChanged(object sender, EventArgs e)
        {
            data.Baudrate = Convert.ToInt32(tscbCombaud.Text);
        }
        /// <summary>
        /// 同步更新串口超时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tstbComtime_TextChanged(object sender, EventArgs e)
        {
            data.Comtimeout = Convert.ToInt32(tstbComtime.Text);
        }

        private void tscbVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.VerUper = tscbVer.SelectedIndex;

            if (data.VerUper == 0)          //自动检测
            {
                btnCheck.Enabled = true;
                btnCheck.Visible = true;
                //panel.Controls.Clear();   //移除所有控件
            }
            else if (data.VerUper == 1)     //S1卡
            {
                btnCheck.Enabled = false;
                btnCheck.Visible = false;
                //panel.Controls.Clear();   //移除所有控件
            }
            else if (data.VerUper == 2)     //S2卡
            {
                btnCheck.Enabled = false;
                btnCheck.Visible = false;
                //panel.Controls.Clear();   //移除所有控件
            }
        }
    }
}
