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
using Yungku.Common.IOCardS1;
using Yungku.Common.IOCardS2;
using Yungku.Common.IOCard.DataDeal;

namespace YKUper
{
    public partial class MainForm : Form
    {


        /// <summary>
        /// 串口实例
        /// </summary>
        public YKS1Card MainCom1 = new YKS1Card();

        /// <summary>
        /// 串口实例
        /// </summary>
        public YKS2Card MainCom2 = new YKS2Card();
        /// <summary>
        /// 数据实例
        /// </summary>
        public DataDeal MainData = new DataDeal();

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
            TstbNetip.Text = "192.168.1.100";          //网络通讯-IP
            TstbNetport.Text = "4000";                   //网络通讯-port
            TstbNetimeout.Text = "300";                    //网络通讯-timeout
            TstbComTimeout.Text = "300";                    //串口通讯-timeout
            ComConf();
            UperConf();

            panel.Controls.Clear();//移除所有控件
            FormS2 formS2 = new FormS2();
            formS2.Owner = this;
            formS2.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            formS2.TopLevel = false; //指示子窗体非顶级窗体
            panel.Controls.Add(formS2);//将子窗体载入panel
            formS2.Show();
        }

        //****************************************************************************

        private void RecodeInfo(string info)
        {
            TbRecode.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff ") + " " + info + "\r\n");
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
                TscbCombaud.Items.Add(BaudItem[i]);  //配置选项
                TscbCombaud.SelectedIndex = i;    //配置索引序号      
            }
            TscbCombaud.SelectedItem = TscbCombaud.Items[1];    //默认为列表第二个变量 - 115200

            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                TscbComport.Items.Add(ports[i]);  //配置选项
                TscbComport.SelectedIndex = i;    //配置索引序号
            }
            TscbComport.SelectedItem = TscbComport.Items[3];    //默认为列表第1个变量

        }

        /// <summary>
        /// 上位机选项配置
        /// </summary>
        private void UperConf()
        {
            string[] UperItem = { "自动识别", "S1", "S2" };
            for (int i = 0; i < UperItem.Length; i++)
            {
                TscbCheck.Items.Add(UperItem[i]);  //配置选项
                TscbCheck.SelectedIndex = i;    //配置索引序号      
            }
            TscbCheck.SelectedItem = TscbCheck.Items[0];  

        }

        /// <summary>
        /// 循环运行三次心跳查询
        /// </summary>
        private bool CheckHeartTimes()
        {
            int checktimes = 3;
            int ver = MainData.VerUper;
            int num = 0;
            if (ver == 0 || ver == 2)
            {

                MainCom2.Port = MainData.Comport;
                MainCom2.BaudRate = MainData.Baudrate;
                MainCom2.Timeout = MainData.Comtimeout;
                MainCom2.Open();         //打开串口

                num = 0;
                do
                {
                    if (MainCom2.IsExists())
                    {
                        MainData.VerUper = 2;
                        TscbCheck.SelectedIndex = MainData.VerUper; //更新版本号
                        MainCom2.Close();
                        return true;
                    }
                    num++;
                } while (num <= checktimes);
                MainCom2.Close();
            }
            if (ver == 0 || ver == 1)
            {
                MainCom1.Port = MainData.Comport;
                MainCom1.Timeout = MainData.Comtimeout;
                MainCom1.Open();         //打开串口

                num = 0;
                do
                {
                    if (MainCom1.IsExists())
                    {
                        MainData.VerUper = 1;
                        TscbCheck.SelectedIndex = MainData.VerUper; //更新版本号
                        MainCom1.Close();
                        return true;
                    }
                    num++;
                } while (num <= checktimes);
                MainCom1.Close();
            }
            
            return false;
        }

        //****************************************************************************

        /// <summary>
        /// 同步更新网络ip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TstbNetip_TextChanged(object sender, EventArgs e)
        {
            MainData.NetIP = TstbNetip.Text;
        }
        /// <summary>
        /// 同步更新网络端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TstbNetport_TextChanged(object sender, EventArgs e)
        {
            MainData.Netport = Convert.ToInt32(TstbNetport.Text);
        }
        /// <summary>
        /// 同步更新网络超时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TstbNetimeout_TextChanged(object sender, EventArgs e)
        {
            MainData.Netimeout = Convert.ToInt32(TstbNetimeout.Text);
        }

        /// <summary>
        /// 同步更新串口号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TscbComport_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainData.Comport = TscbComport.SelectedIndex;
        }

        /// <summary>
        /// 同步更新串口波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TscbCombaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainData.Baudrate = Convert.ToInt32(TscbCombaud.Text);
        }

        /// <summary>
        /// 同步更新串口超时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TstbComTimeout_TextChanged(object sender, EventArgs e)
        {
            MainData.Comtimeout = Convert.ToInt32(TstbComTimeout.Text);
        }

        /// <summary>
        /// 检测下位机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TscbCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到
            MainData.VerUper = TscbCheck.SelectedIndex;

            if (MainData.VerUper == 0)          //自动检测
            {
                BtnCheck.Enabled = true;
                BtnCheck.Visible = true;
                panel.Controls.Clear();   //移除所有控件
            }
            else if (MainData.VerUper == 1)     //S1卡
            {
                BtnCheck.Enabled = false;
                BtnCheck.Visible = false;
                panel.Controls.Clear();   //移除所有控件
            }
            else if (MainData.VerUper == 2)     //S2卡
            {
                BtnCheck.Enabled = false;
                BtnCheck.Visible = false;
                panel.Controls.Clear();   //移除所有控件
            }
                

            

        }

        /// <summary>
        /// 串口连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnComt_Click(object sender, EventArgs e)
        {
            if (MainData.VerUper == 0)  //未检测到版本
            {
                RecodeInfo("请检查上位机配置！");
            }
        }

        /// <summary>
        /// 下位机检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCheck_Click(object sender, EventArgs e)
        {
            if (CheckHeartTimes())
            {
                RecodeInfo("上位机版本为：" + MainData.VerUper);
            }
            else { RecodeInfo("自动检测失败！"); }
                
            
        }
    }


}
