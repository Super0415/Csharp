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
using Yungku.Common.IOCardS2;
using System.Threading;
namespace YKUper
{
    public partial class FormS2 : Form
    {
        /// <summary>
        /// 主窗口的句柄
        /// </summary>
        private MainForm lForm = null;
        /// <summary>
        /// 用于复制数据集
        /// </summary>
        private DataDeal S2data = null;
        /// <summary>
        /// 串口实例
        /// </summary>
        private YKS2Card Com = new YKS2Card();
        private Thread thread = null;                   //线程句柄-处理通讯过程

        /// <summary>
        /// 初始化S2窗口
        /// </summary>
        public FormS2()
        {
            InitializeComponent();

            //X轴数据初始化
            tbXLoca.Text        = "0";                    //当前位置
            tbXDist.Text        = "1000";                 //距离
            tbXAss.Text         = "0.2";                  //加速度
            tbXSpdstart.Text    = "100";                  //起始速度
            tbXSpdrun.Text      = "10000";                //运行速度
            tbXSpdsec.Text      = "1000";                 //第二速度
            tbXTarg.Text        = "0";                    //目标位置

            //Y轴数据初始化
            tbYLoca.Text        = "0";                    //当前位置
            tbYDist.Text        = "1000";                 //距离
            tbYAss.Text         = "0.2";                  //加速度
            tbYSpdstart.Text    = "100";                  //起始速度
            tbYSpdrun.Text      = "10000";                //运行速度
            tbYSpdsec.Text      = "1000";                 //第二速度
            tbYTarg.Text        = "0";                    //目标位置

            //Z轴数据初始化
            tbZLoca.Text        = "0";                    //当前位置
            tbZDist.Text        = "1000";                 //距离
            tbZAss.Text         = "0.2";                  //加速度
            tbZSpdstart.Text    = "100";                  //起始速度
            tbZSpdrun.Text      = "10000";                //运行速度
            tbZSpdsec.Text      = "1000";                 //第二速度
            tbZTarg.Text        = "0";                    //目标位置
        }

        /// <summary>
        /// 窗口加载过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Load(object sender, EventArgs e)
        {
            lForm = (MainForm)this.Owner;//把Form2的父窗口指针赋给lForm1
            lForm.MyEventCOMVer2 += new MainForm.MyDelegate(Main_Connet);//监听MainForm窗体事件
            S2data = lForm.data;
            timerRefresh.Enabled = true;
        }
        /// <summary>
        /// 同步更新轴号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            S2data.Axis = tcMain.SelectedIndex;        //选项卡索引
        }

        /// <summary>
        /// 主窗体打开串口事件
        /// </summary>
        void Main_Connet()
        {

            try
            {
                if (S2data.COMHardCon == 0)    //判断串口是否打开
                {
                    Com.Port = S2data.Comport;
                    Com.BaudRate = S2data.Baudrate;
                    Com.Timeout = S2data.Comtimeout;
                    Com.Open();         //打开串口


                    S2data.COMHardCon = 1;
                    lForm.RecodeInfo("打开串口");

                    thread = new Thread(thread_Refresh);
                    thread.Start();
                }
                else
                {
                    Com.Close();
                    S2data.COMHardCon = 0;
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
        /// 窗体关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormS2_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRefresh.Enabled = false;
        }

        /// <summary>
        /// 配置窗体控件使能
        /// </summary>
        /// <param name="able"></param>
        public void FromNature(bool able)
        {
            btnLedY0.Enabled    = able; //Y0
            btnLedY1.Enabled    = able; //Y1
            btnLedY2.Enabled    = able; //Y2
            btnLedY3.Enabled    = able; //Y3
            btnLedY4.Enabled    = able; //Y4
            btnLedY5.Enabled    = able; //Y5
            btnLedY6.Enabled    = able; //Y6
            btnLedY7.Enabled    = able; //Y7
            //X轴
            btnXConf.Enabled    = able;  //极限设置
            btnXLeft.Enabled    = able; //向左键
            btnXRight.Enabled   = able; //向右键
            btnXDestop.Enabled  = able; //减速停
            btnXEmstop.Enabled  = able; //立即停
            btnXBegin.Enabled   = able; //演习键
            //Y轴
            btnYConf.Enabled    = able;  //极限设置
            btnYLeft.Enabled    = able; //向左键
            btnYRight.Enabled   = able; //向右键
            btnYDestop.Enabled  = able; //减速停
            btnYEmstop.Enabled  = able; //立即停
            btnYBegin.Enabled   = able; //演习键
            //Z轴
            btnZConf.Enabled    = able;  //极限设置
            btnZLeft.Enabled    = able; //向左键
            btnZRight.Enabled   = able; //向右键
            btnZDestop.Enabled  = able; //减速停
            btnZEmstop.Enabled  = able; //立即停
            btnZBegin.Enabled   = able; //演习键
        }

        /// <summary>
        /// 窗口数据变化
        /// </summary>
        void WindosShowData()
        {
            int temp = 0;
            double temd = 0;
            int axis = S2data.Axis;
            if (axis == 0)
                tbXLoca.Text = S2data.GetLocation(axis).ToString();
            else if (axis == 1)
                tbYLoca.Text = S2data.GetLocation(axis).ToString();
            else
                tbZLoca.Text = S2data.GetLocation(axis).ToString();


            if(axis == 0)       temp = Convert.ToInt32(tbXDist.Text);
            else if (axis == 1) temp = Convert.ToInt32(tbYDist.Text);
            else if (axis == 2) temp = Convert.ToInt32(tbZDist.Text);
            S2data.SetDistence(axis, temp);

            if (axis == 0) temp = Convert.ToInt32(tbXTarg.Text);
            else if (axis == 1) temp = Convert.ToInt32(tbYTarg.Text);
            else if (axis == 2) temp = Convert.ToInt32(tbZTarg.Text);
            S2data.SetTargloca(axis, temp);

            if (axis == 0) temp = Convert.ToInt32(tbXSpdstart.Text);
            else if (axis == 1) temp = Convert.ToInt32(tbYSpdstart.Text);
            else if (axis == 2) temp = Convert.ToInt32(tbZSpdstart.Text);
            S2data.SetStartSpd(axis, temp);

            if (axis == 0) temp = Convert.ToInt32(tbXSpdrun.Text);
            else if (axis == 1) temp = Convert.ToInt32(tbYSpdrun.Text);
            else if (axis == 2) temp = Convert.ToInt32(tbZSpdrun.Text);
            S2data.SetRunSpd(axis, temp);

            if (axis == 0) temp = Convert.ToInt32(tbXSpdsec.Text);
            else if (axis == 1) temp = Convert.ToInt32(tbYSpdsec.Text);
            else if (axis == 2) temp = Convert.ToInt32(tbZSpdsec.Text);
            S2data.SetSecSpd(axis, temp);

            if (axis == 0) temd = double.Parse(tbXAss.Text);
            else if (axis == 1) temd = double.Parse(tbYAss.Text);
            else if (axis == 2) temd = double.Parse(tbZAss.Text);
            S2data.SetAcce(axis, temd);

        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            int axis = S2data.Axis;
            label1.Text = S2data.GetLocation(axis).ToString();
            FromNature(S2data.COMHardCon == 0 ? false : true);

            WindosShowData();
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
               
                if(S2data.COMHardCon == 1)      //串口数据处理
                {
                    if (Com.IsExists())
                    {
                        label3.Text = "心跳起搏";
                        S2data.COMSoftCon = 1;
                        count = 0;
                    }
                    else
                    {
                        count++;
                        label3.Text = "心跳异常";
                        if (count > 5)
                        {
                            count = 0;
                            S2data.COMHardCon = 0;
                            S2data.COMSoftCon = 0;                           
                            lForm.RecodeInfo("心跳异常");
                            thread.Abort();            //避免重复新建线程
                        }
                    }

                    if (S2data.COMSoftCon == 1)     //软链接成功
                    {
                        int axis = S2data.Axis;
                        //获取轴位置
                        S2data.SetLocation(axis, Com.GetPosition(axis));


                    }


                }

                
            }
            
        }



        /// <summary>
        /// 按钮-X轴向左
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXLeft_Click(object sender, EventArgs e)
        {
            button_ToLeft_Click();
        }
        private void button_ToLeft_Click()
        {


            int axis = S2data.Axis;
            int pos = S2data.GetTargloca(axis);
            int startVel = S2data.GetStartSpd(axis);
            int vel = S2data.GetRunSpd(axis);
            double acc = S2data.GetAcce(axis);
            double dec = S2data.GetDece(axis);
            int dist = S2data.GetDire(axis) > 0 ? S2data.GetDistence(axis) : (-S2data.GetDistence(axis));
            int homeDir = S2data.GetReturnDire(axis);
            int homeSVel = S2data.GetSecSpd(axis);
            int homeMode = 0;   //预留参数
            int offset = 0;     //预留参数

            S2data.SetDirection(axis, 0);

            if ((S2data.GetNetSoft() == 1) && !Com.IsBusy(axis))
            {
                if (S2data.GetRunMode() == 0)  //点对点
                    Com.AbsMove(axis, pos, startVel, vel, acc, dec);
                else if (S2data.GetRunMode() == 1)  //连续
                    Com.RltMove(axis, -2147483647, startVel, vel, acc, dec);
                else if (S2data.GetRunMode() == 2)  //原点
                    Com.Home(axis, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else if ((S2data.COMSoftCon == 1) && !Com.IsBusy(axis))
            {
                if (S2data.GetRunMode() == 0)  //点对点
                    Com.AbsMove(axis, pos, startVel, vel, acc, dec);
                else if (S2data.GetRunMode() == 1)  //连续
                    Com.RltMove(axis, -2147483647, startVel, vel, acc, dec);
                else if (S2data.GetRunMode() == 2)  //原点
                    Com.Home(axis, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else
            {
                lForm.RecodeInfo("执行错误!");
            }

        }
    }
}
