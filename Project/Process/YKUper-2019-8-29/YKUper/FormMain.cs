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
using Yungku.Common.IOCard.Net;
using System.Threading;



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
        /// 网口实例
        /// </summary>
        private YKS2CardNet MainNet2 = new YKS2CardNet();
        /// <summary>
        /// 创建窗体S1的句柄
        /// </summary>
        private FormS1 formS1 = null;

        /// <summary>
        /// 创建窗体S2的句柄
        /// </summary>
        private FormS2 formS2 = null;

        /// <summary>
        /// 创建窗体S2的句柄
        /// </summary>
        private FormGaugeP formGaugeP = null;

        ///// <summary>
        ///// 线程句柄-处理通讯过程
        ///// </summary>
        //private Thread thread = null;                  

        /// <summary>
        /// 定义委托
        /// </summary>
        public delegate void MyDelegate();
        /// <summary>
        /// 定义委托事件-版本S1的串口打开
        /// </summary>
        public event MyDelegate MyEventCOMVerS1;
        /// <summary>
        /// 定义委托事件-版本S2的串口打开
        /// </summary>
        public event MyDelegate MyEventCOMVerS2;
        /// <summary>
        /// 定义委托事件-版本S2的网口打开
        /// </summary>
        public event MyDelegate MyEventNetVerS2;
        /// <summary>
        /// 定义委托事件-负压表的串口打开
        /// </summary>
        public event MyDelegate MyEventCOMVerGaugeP;
        /// <summary>
        /// 窗体初始化
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            tstbNetip.Text = "192.168.1.100";          //网络通讯-IP
            tstbNetport.Text = "4000";                   //网络通讯-port
            tstbNetime.Text = "300";                    //网络通讯-timeout
            tstbComtime.Text = "300";                    //串口通讯-timeout
            ComConf();
            UperConf();
        }      
        /// <summary>
        /// 窗体控件载入过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {    
        }
        /// <summary>
        /// 主窗体关闭过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0); //关闭主窗体时，关闭所有线程
        }
        //*****************************************************************************

        public void RecodeInfo(string info)
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
            tscbComport.SelectedItem = tscbComport.Items[0];    //默认为列表第1个变量

        }
        /// <summary>
        /// 上位机选项配置
        /// </summary>
        private void UperConf()
        {
            string[] UperItem = { "自动识别", "S1", "S2","负压表" };
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
        private bool CheckComHeartTimes()
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

        /// <summary>
        /// 循环运行三次心跳查询
        /// </summary>
        private bool CheckNetHeartTimes()
        {
            int checktimes = 1;
            int ver = data.VerUper;
            int num = 0;
            if (ver == 0 || ver == 2)
            {
                MainNet2.NetWorkInit(data.NetIP, data.Netport, data.Netimeout);
                num = 0;
                do
                {
                    if (MainNet2.IsExists())
                    {
                        data.VerUper = 2;
                        tscbVer.SelectedIndex = data.VerUper; //更新版本号
                        MainNet2.Close();
                        return true;
                    }
                    num++;
                } while (num <= checktimes);

            }
            return false;
        }


        //******************************************************************************
        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckComHeartTimes()|| CheckNetHeartTimes())
            {
            }
            else { RecodeInfo("自动检测失败！"); }


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
            string str = tscbComport.SelectedItem.ToString().Split('M')[1];

            data.Comport = Convert.ToInt32(str);
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
     
        /// <summary>
        /// 切换上位机版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbVer_SelectedIndexChanged(object sender, EventArgs e)
        {           
            data.VerUper = tscbVer.SelectedIndex;

            if (data.VerUper == 0)          //自动检测
            {
                btnCheck.Enabled = true;
                btnCheck.Visible = true;
                if(formS1 != null) formS1.Hide();  //Dispose Hide
                if(formS2 != null) formS2.Hide();
                if (formGaugeP != null) formGaugeP.Hide();

            }
            else if (data.VerUper == 1)     //S1卡
            {
                btnCheck.Enabled = false;
                btnCheck.Visible = false;

                if (formS2 != null) formS2.Hide();
                if (formGaugeP != null) formGaugeP.Hide();

                panel.Controls.Clear();//移除所有控件
                if (formS1 == null) formS1 = new FormS1();
                formS1.Owner = this;    //标注父窗体
                formS1.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                formS1.TopLevel = false; //指示子窗体非顶级窗体
                panel.Controls.Add(formS1);//将子窗体载入panel
                formS1.Show();
                
            }
            else if (data.VerUper == 2)     //S2卡
            {         
                btnCheck.Enabled = false;
                btnCheck.Visible = false;

                if (formS1 != null) formS1.Hide();
                if (formGaugeP != null) formGaugeP.Hide();

                panel.Controls.Clear();//移除所有控件
                if (formS2 == null)
                    formS2 = new FormS2();      //避免重复创建窗口，只创建一次，之后使用Hide()与Show()实现窗体的变化
                formS2.Owner = this;        //标注父窗体
                formS2.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                formS2.TopLevel = false; //指示子窗体非顶级窗体
                panel.Controls.Add(formS2);//将子窗体载入panel
                formS2.Show();

            }
            else if (data.VerUper == 3)     //负压表
            {
                btnCheck.Enabled = false;
                btnCheck.Visible = false;

                if (formS1 != null) formS1.Hide();
                if (formS2 != null) formS2.Hide();

                panel.Controls.Clear();//移除所有控件
                if (formGaugeP == null)
                    formGaugeP = new FormGaugeP();      //避免重复创建窗口，只创建一次，之后使用Hide()与Show()实现窗体的变化
                formGaugeP.Owner = this;        //标注父窗体
                formGaugeP.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                formGaugeP.TopLevel = false; //指示子窗体非顶级窗体
                panel.Controls.Add(formGaugeP);//将子窗体载入panel
                formGaugeP.Show();

            }
        }

        /// <summary>
        /// 串口连接状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComt_Click(object sender, EventArgs e)
        {
            int ver = data.VerUper;

            if (ver == 2)
            {
                if (MyEventCOMVerS2 != null) MyEventCOMVerS2();//引发事件
            }
            else if (ver == 1)
            {
                if (MyEventCOMVerS1 != null) MyEventCOMVerS1();//引发事件
            }
            else if (ver == 3)
            {
                if (MyEventCOMVerGaugeP != null) MyEventCOMVerGaugeP();//引发事件             
            }

            if (data.COMHardCon == 0)
            {
                btnComt.Text = "串口未连接";
                tscbVer.Enabled = true;
            }
            else
            {
                btnComt.Text = "串口已连接";
                tscbVer.Enabled = false;
            }
        }
        /// <summary>
        /// 网口连接状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNett_Click(object sender, EventArgs e)
        {
            int ver = data.VerUper;

            if (ver == 2)
            {
                if (MyEventNetVerS2 != null) MyEventNetVerS2();//引发事件
            }

            if (data.NetHardCon == 0)
            {
                btnNett.Text = "网口未连接";
                tscbVer.Enabled = true;
            }
            else
            {
                btnNett.Text = "网口已连接";
                tscbVer.Enabled = false;
            }
        }
        /// <summary>
        /// 显示版本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiVers_Click(object sender, EventArgs e)
        {
            string Ver_Co = "Shenzhen Yungku Precision Fisture Co.,Ltd.";
            string Ver_Web = "www.yungku.com";
            string Ver_Card = "未知";
            string Ver_UD = "未知";
            string Ver_SPT = "15221283134@163.com";
            string Name = "未知";
            int sn = 0;

            if (data.VerUper == 2)
            {
                string[] info = data.GetVer_Info().Split('\n');    //版本信息-公司
                Ver_Co = info[0];    //版本信息-公司
                Ver_Web = info[1];    //版本信息-网址
                Ver_Card = info[2];    //版本信息-主板名
                Ver_UD = info[3];       //版本信息-版本号
                Ver_SPT = info[4];      //版本信息-技术支持
                Name = data.GetName();
                sn = data.GetSN();
            }
            else if (data.VerUper == 1)
            {
                string[] info = data.GetVer_Info().Split('\n');    //版本信息-公司
                Ver_Co = info[0];    //版本信息-公司
                Ver_Web = info[1];    //版本信息-网址
                Ver_UD = info[2];       //版本信息-版本号
                Ver_SPT = info[3];      //版本信息-技术支持
                Ver_Card = "S1 IO";
                Name = "S1";
            }
            
            

            MessageBox.Show("公司名称：" + Ver_Co + "\n公司网址：" + Ver_Web + "\n产品名称：" + Ver_Card + "\n固件版本：" + Ver_UD + "\n硬件版本：" + Name + "\n产品编号：" + sn + "\n技术支持：" + Ver_SPT, "产品信息", 0);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Tick(object sender, EventArgs e)
        {
            if (data.COMHardCon == 0 && data.NetHardCon == 0)
            {
                tscbVer.Enabled = true;
            }
            if (data.COMHardCon == 0)
                btnComt.Text = "串口未连接";
            if (data.NetHardCon == 0)
                btnNett.Text = "网口未连接";

            if (data.VerUper == 0)
            {
                this.Text = "Card Tool";
            }
            else if (data.VerUper == 1)
            {
                this.Text = "S1 Card Tool V2.0";
            }
            else if (data.VerUper == 2)
            {
                this.Text = "S2 Card Tool V2.0";
            }
        }

        private void tsmiCOMname_MouseDown(object sender, MouseEventArgs e)
        {
            tscbComport.Items.Clear();
            tscbCombaud.Items.Clear();
            ComConf();
        }

    }
}
