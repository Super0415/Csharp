using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yungku.Common.CCASP24WST;
using Yungku.Common.IOCard.DataDeal;
using System.Threading;

namespace YKUper
{
    public partial class FormYKUCC52 : Form
    {
        /// <summary>
        /// 主窗口的句柄
        /// </summary>
        private MainForm lForm = null;
        /// <summary>
        /// 用于复制数据集
        /// </summary>
        private DataDeal UCdata = null;
        /// <summary>
        /// 串口实例
        /// </summary>
        private YK_CCASP24WST Com = new YK_CCASP24WST();
        private Thread thread = null;                   //线程句柄-处理通讯过程
        public FormYKUCC52()
        {
            InitializeComponent();
        }

        private void FormYKUCC52_Load(object sender, EventArgs e)
        {
            ToolTipShow(toolTip1,lbSW1,"测试效果");
            lForm = (MainForm)this.Owner;//把Form2的父窗口指针赋给lForm1
            lForm.MyEventCOMVerYTUCC52 += new MainForm.MyDelegate(Main_Connet);//监听MainForm窗体事件
            UCdata = lForm.data;
            //timerRefresh.Enabled = true;

            lForm.RecodeInfo("上位机版本为：调光器上位机");
        }
        /// <summary>
        /// 主窗体打开串口事件
        /// </summary>
        void Main_Connet()
        {

            try
            {
                if (UCdata.COMHardCon == 0)    //判断串口是否打开
                {
                    Com.Port = UCdata.Comport;
                    Com.Timeout = UCdata.Comtimeout;
                    Com.Open();         //打开串口


                    UCdata.COMHardCon = 1;
                    lForm.RecodeInfo("打开串口");

                    thread = new Thread(thread_Refresh);
                    thread.Start();
                }
                else
                {
                    Com.Close();
                    UCdata.COMHardCon = 0;
                    lForm.RecodeInfo("关闭串口");
                    thread.Abort();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 线程主体，循环发送指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_Refresh()
        {
            int count = 0;
            while (true)
            {
                Thread.Sleep(30);

                if (UCdata.COMHardCon == 1)      //串口数据处理
                {
                    if (Com.IsExists())
                    {

                        UCdata.COMSoftCon = 1;
                        count = 0;
                    }
                    else
                    {
                        count++;

                        if (count > 5)
                        {
                            count = 0;
                            UCdata.COMHardCon = 0;
                            UCdata.COMSoftCon = 0;
                            lForm.RecodeInfo("心跳异常");
                            thread.Abort();            //避免重复新建线程
                        }
                    }

                        if (UCdata.COMSoftCon == 1)     //软链接成功
                        {

                        //        if (S1data.GetFirstNum() == 0)    //读取固件信息
                        //        {
                        //            S1data.SetFirstNum(1);
                        //            //获取版本信息
                        //            S1data.SetVer_Info(Com.GetVerInfo());
                        //        }

                        UCdata.ucDutyCycle = Com.GetValue();      //获取通道占空比
                        UCdata.ucIOState = Com.GetIO();           //获取IO状态，LED、KEY、跳线

                        UCdata.ucPWMstate = Com.GetEnables();   //获取通道使能状态

                        //        int axis = S1data.Axis;
                        //        //获取轴位置
                        //        S1data.SetLocation(axis, Com.GetPosition());
                        //        //获取DIP
                        //        if (Com.GetDIP() != Com.ErrNum) S1data.DIP = Com.GetDIP();
                        //        //获取输入口状态
                        //        if (Com.GetInputs() != Com.ErrNum) S1data.MInput = Com.GetInputs();
                        //        //获取输出口状态
                        //        if (Com.GetOutputs() != Com.ErrNum) S1data.MOutput = Com.GetOutputs();

                        //        if (S1data.GetShowMode(axis) == 1)
                        //        {
                        //            int dir = S1data.GetDire(axis);
                        //            int dis = S1data.GetDistence(axis);
                        //            long Loca = S1data.GetLocation(axis);

                        //            if (ShowLoc - Loca == 0 && ShowNum == 0)
                        //            {
                        //                ShowEn = true;
                        //                ShowNum++;
                        //            }
                        //            else if (Math.Abs(ShowLoc - Loca) == dis && ShowNum == 1)
                        //            {
                        //                ShowEn = true;
                        //                ShowNum--;
                        //            }
                        //            if (ShowEn)
                        //            {
                        //                ShowEn = false;
                        //                Com.SetMMmove(S1data.GetDire(axis), S1data.GetDistence(axis), S1data.GetRunSpd(axis));
                        //                if (dir == 0)
                        //                {
                        //                    S1data.SetDire(axis, 1);
                        //                }
                        //                else
                        //                {
                        //                    S1data.SetDire(axis, 0);
                        //                }
                        //            }
                        //        }
                    }
                }
            }
        }

        /// <summary>
        /// 动态提示
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="Con"></param>
        /// <param name="words"></param>
        private void ToolTipShow(ToolTip tip,Control Con, string words)
        {
            tip.SetToolTip(Con, words);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
