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
using Yungku.Common.IOCard.Net;
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
        public DataDeal S2data = null;
        /// <summary>
        /// 串口实例
        /// </summary>
        private YKS2Card Com = new YKS2Card();
        /// <summary>
        /// 网口实例
        /// </summary>
        private YKS2CardNet Net = new YKS2CardNet();
        /// <summary>
        /// 处理串口通讯
        /// </summary>
        private Thread threadCom = null;                  
        /// <summary>
        /// 处理网口通讯
        /// </summary>
        private Thread threadNet = null;                 

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
            Comboboxn(cbXMode);
            Comboboxn(cbYMode);
            Comboboxn(cbZMode);
        }

        /// <summary>
        /// 窗口加载过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Load(object sender, EventArgs e)
        {
            lForm = (MainForm)this.Owner;//把Form2的父窗口指针赋给lForm1
            lForm.MyEventCOMVerS2 += new MainForm.MyDelegate(Com_Connet);//监听MainForm窗体事件
            lForm.MyEventNetVerS2 += new MainForm.MyDelegate(Net_Connet);//监听MainForm窗体事件
            S2data = lForm.data;
            timerRefresh.Enabled = true;
        }
        public void Comboboxn(ComboBox box)
        {
            string[] item = { "点对点", "连续", "原点" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                box.Items.Add(item[i]);
                box.SelectedIndex = i;    //配置索引序号

            }
            box.SelectedItem = box.Items[0];    //默认为列表第二个变量 
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
        void Com_Connet()
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

                    threadCom = new Thread(threadCom_Refresh);
                    threadCom.Start();
                }
                else
                {
                    Com.Close();
                    S2data.COMHardCon = 0;
                    S2data.COMSoftCon = 0;
                    lForm.RecodeInfo("关闭串口");
                    threadCom.Abort();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 主窗体打开网口事件
        /// </summary>
        void Net_Connet()
        {
            Net.NetWorkInit(S2data.NetIP, S2data.Netport, S2data.Netimeout);
            if (!Net.NetWorkPing())
            {
                lForm.RecodeInfo("网口连接异常");
                S2data.NetHardCon = 0;
                S2data.NetSoftCon = 0;
                return;
            }
            if (S2data.NetHardCon == 0)
            {
                if (Net.IsExists())
                {
                    S2data.NetHardCon = 1;
                    lForm.RecodeInfo("网口连接正常");

                    threadNet = new Thread(threadNet_Refresh);
                    threadNet.Start();
                }
                else
                {
                    lForm.RecodeInfo("网口连接异常");
                }
            }
            else
            {
                lForm.RecodeInfo("网口断开连接");
                S2data.NetHardCon = 0;
                S2data.NetSoftCon = 0;
                threadNet.Abort();
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
            //Y轴
            btnYConf.Enabled    = able;  //极限设置
            btnYLeft.Enabled    = able; //向左键
            btnYRight.Enabled   = able; //向右键
            btnYDestop.Enabled  = able; //减速停
            btnYEmstop.Enabled  = able; //立即停
            //Z轴
            btnZConf.Enabled    = able;  //极限设置
            btnZLeft.Enabled    = able; //向左键
            btnZRight.Enabled   = able; //向右键
            btnZDestop.Enabled  = able; //减速停
            btnZEmstop.Enabled  = able; //立即停

            btnXBegin.Enabled = ((S2data.COMHardCon == 1) || (S2data.NetHardCon == 1)) ? true : false; //演习键
            btnYBegin.Enabled = ((S2data.COMHardCon == 1) || (S2data.NetHardCon == 1)) ? true : false; //演习键
            btnZBegin.Enabled = ((S2data.COMHardCon == 1) || (S2data.NetHardCon == 1)) ? true : false; //演习键

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

            S2data.SetRunMode(0, cbXMode.SelectedIndex);
            S2data.SetRunMode(1, cbYMode.SelectedIndex);
            S2data.SetRunMode(2, cbZMode.SelectedIndex);

        }
        /// <summary>
        /// 窗口灯光变化
        /// </summary>
        void WindosShowLED()
        {
            int MInput = S2data.MInput;
            int MOutput = S2data.MOutput;
            int axis = S2data.Axis;
            //开关量输出
            if ((MOutput >> 0 & 0x1) == 0) btnLedY0.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y0
            else btnLedY0.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 1 & 0x1) == 0) btnLedY1.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y1
            else btnLedY1.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 2 & 0x1) == 0) btnLedY2.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y2
            else btnLedY2.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 3 & 0x1) == 0) btnLedY3.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y3
            else btnLedY3.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 4 & 0x1) == 0) btnLedY4.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y4
            else btnLedY4.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 5 & 0x1) == 0) btnLedY5.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y5
            else btnLedY5.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 6 & 0x1) == 0) btnLedY6.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y6
            else btnLedY6.BackColor = Color.FromArgb(44, 255, 44);
            if ((MOutput >> 7 & 0x1) == 0) btnLedY7.BackColor = Color.FromArgb(28, 66, 28);    //灯光-Y7
            else btnLedY7.BackColor = Color.FromArgb(44, 255, 44);
            //开关量输入
            if ((MInput >> 0 & 0x1) == 0) lbLedX0.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X0
            else lbLedX0.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 1 & 0x1) == 0) lbLedX1.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X1
            else lbLedX1.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 2 & 0x1) == 0) lbLedX2.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X2
            else lbLedX2.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 3 & 0x1) == 0) lbLedX3.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X3
            else lbLedX3.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 4 & 0x1) == 0) lbLedX4.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X4
            else lbLedX4.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 5 & 0x1) == 0) lbLedX5.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X5
            else lbLedX5.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 6 & 0x1) == 0) lbLedX6.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X6
            else lbLedX6.BackColor = Color.FromArgb(44, 255, 44);
            if ((MInput >> 7 & 0x1) == 0) lbLedX7.BackColor = Color.FromArgb(28, 66, 28);    //灯光-X7
            else lbLedX7.BackColor = Color.FromArgb(44, 255, 44);

            AxleShowLight(S2data.GetPWMState(axis), S2data.GetPWMIOState(axis));

        }
        void ShowLight(int State, Label Lab)
        {
            if (State == 0) Lab.BackColor = Color.FromArgb(28, 66, 28);    //灯光-暗
            else Lab.BackColor = Color.FromArgb(44, 255, 44);
        }
        void AxleShowLight(int PWMSt, int IOSt)
        {
            S2data.SetSignEnLimit((PWMSt >> 7) & 0x1);
            S2data.SetSignEnOrigin((PWMSt >> 8) & 0x1);
            S2data.SetSignReLimit((PWMSt >> 9) & 0x1);
            S2data.SetSignReOrigin((PWMSt >> 10) & 0x1);


            if (S2data.Axis == 0)
            {
                ShowLight((PWMSt >> 1 & 0x1), lbXLedbusy);    //灯光-忙
                ShowLight((PWMSt >> 3 & 0x1), lbXLedlimtp);    //灯光-正极限
                ShowLight((PWMSt >> 4 & 0x1), lbXLedhome);    //灯光-原点
                ShowLight((PWMSt >> 5 & 0x1), lbXLedlimtn);    //灯光-负极限
                ShowLight((PWMSt >> 6 & 0x1), lbXLedretu);    //灯光-回原点
                ShowLight((IOSt & 0x1), lbXLedpuls);           //灯光-脉冲
                ShowLight((IOSt & 0x2), lbXLeddire);           //灯光-方向

                ShowLight((PWMSt >> 7 & 0x1), lbXLedlimte);    //灯光-极限使能
                ShowLight((PWMSt >> 8 & 0x1), lbXLedhe);    //灯光-原点使能
                ShowLight((PWMSt >> 9 & 0x1), lbXLedlimti);    //灯光-反极限使能
                ShowLight((PWMSt >> 10 & 0x1),lbXLedhi);    //灯光-反原点使能
            }
            else if (S2data.Axis == 1)
            {
                ShowLight((PWMSt >> 1 & 0x1), lbYLedbusy);    //灯光-忙
                ShowLight((PWMSt >> 3 & 0x1), lbYLedlimtp);    //灯光-正极限
                ShowLight((PWMSt >> 4 & 0x1), lbYLedhome);    //灯光-原点
                ShowLight((PWMSt >> 5 & 0x1), lbYLedlimtn);    //灯光-负极限
                ShowLight((PWMSt >> 6 & 0x1), lbYLedretu);    //灯光-回原点
                ShowLight((IOSt & 0x1), lbYLedpuls);           //灯光-脉冲
                ShowLight((IOSt & 0x2), lbYLeddire);           //灯光-方向

                ShowLight((PWMSt >> 7 & 0x1), lbYLedlimte);    //灯光-极限使能
                ShowLight((PWMSt >> 8 & 0x1), lbYLedhe);    //灯光-原点使能
                ShowLight((PWMSt >> 9 & 0x1), lbYLedlimti);    //灯光-反极限使能
                ShowLight((PWMSt >> 10 & 0x1),lbYLedhi);    //灯光-反原点使能
            }
            else
            {
                ShowLight((PWMSt >> 1 & 0x1), lbZLedbusy);    //灯光-忙
                ShowLight((PWMSt >> 3 & 0x1), lbZLedlimtp);    //灯光-正极限
                ShowLight((PWMSt >> 4 & 0x1), lbZLedhome);    //灯光-原点
                ShowLight((PWMSt >> 5 & 0x1), lbZLedlimtn);    //灯光-负极限
                ShowLight((PWMSt >> 6 & 0x1), lbZLedretu);    //灯光-回原点
                ShowLight((IOSt & 0x1), lbZLedpuls);           //灯光-脉冲
                ShowLight((IOSt & 0x2), lbZLeddire);           //灯光-方向

                ShowLight((PWMSt >> 7 & 0x1), lbZLedlimte);    //灯光-极限使能
                ShowLight((PWMSt >> 8 & 0x1), lbZLedhe);    //灯光-原点使能
                ShowLight((PWMSt >> 9 & 0x1), lbZLedlimti);    //灯光-反极限使能
                ShowLight((PWMSt >> 10 & 0x1),lbZLedhi);    //灯光-反原点使能
            }



        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            int axis = S2data.Axis;
            //label1.Text = S2data.GetLocation(axis).ToString();
            FromNature((((S2data.COMHardCon == 1) || (S2data.NetHardCon == 1)) && (S2data.GetShowMode(axis) == 0)) ? true : false);

            WindosShowData();
            WindosShowLED();
        }


        /// <summary>
        /// 线程主体，循环发送串口指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threadCom_Refresh()
        {
            int count = 0;
            while (true)
            {
                Thread.Sleep(30);
               
                if(S2data.COMHardCon == 1)      //串口数据处理
                {
                    if (Com.IsExists())
                    {
                        //label3.Text = "心跳起搏";
                        S2data.COMSoftCon = 1;
                        count = 0;
                    }
                    else
                    {
                        count++;
                        //label3.Text = "心跳异常";
                        if (count > 2)
                        {
                            count = 0;
                            S2data.COMHardCon = 0;
                            S2data.COMSoftCon = 0;                           
                            lForm.RecodeInfo("串口心跳异常");
                            threadCom.Abort();            //避免重复新建线程
                        }
                    }

                    if (S2data.COMSoftCon == 1 && S2data.NetHardCon == 0)     //软链接成功
                    {

                        if (S2data.GetFirstNum() == 0)    //读取固件信息
                        {
                            S2data.SetFirstNum(1);

                            //获取主板名称
                            S2data.SetName(Com.GetCardName());
                            //获取固件编号
                            S2data.SetSN(Com.GetSN());
                            //获取编码开关状态
                            S2data.DIP = Com.GetDipSwitch();
                            //获取版本信息
                            S2data.SetVer_Info(Com.GetVerInfo());
                        }

                        int axis = S2data.Axis;
                        //获取轴位置
                        S2data.SetLocation(axis, Com.GetPosition(axis));
                        //获取轴IO
                        S2data.SetPWMIOState(axis,Com.GetAIO());

                        if (S2data.GetCardID() == 1)
                        {
                            //获取主板输入端口值 
                            S2data.MInput = Com.GetInputsEx();
                            //获取主板输出端口值
                            S2data.MOutput = Com.GetOutputsEx();
                        }
                        else
                        {
                            //获取主板输入端口值 
                            S2data.MInput = Com.GetInputs();
                            //获取主板输出端口值
                            S2data.MOutput = Com.GetOutputs();
                        }

                        //获取轴状态
                        S2data.SetPWMState(axis,Com.GetAxisStatus(axis));
                        if (S2data.GetShowMode(axis) == 1)
                        {
                            if ((S2data.GetPWMState(axis) >> 1 & 0x1) == 0)
                            {
                                int Dis = S2data.GetDire(axis) > 0 ? S2data.GetDistence(axis) : -S2data.GetDistence(axis);
                                Com.RltMove(S2data.Axis, Dis, S2data.GetStartSpd(axis), S2data.GetRunSpd(axis), S2data.GetAcce(axis), S2data.GetDece(axis));
                                if (S2data.GetDire(axis) == 0)
                                {
                                    S2data.SetDire(axis,1);
                                }
                                else
                                {
                                    S2data.SetDire(axis,0);
                                }
                            }
                        }

                    }


                }

                
            }
            
        }
        /// <summary>
        /// 线程主体，循环发送网口指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threadNet_Refresh()
        {
            int count = 0;
            while (true)
            {
                Thread.Sleep(30);

                if (S2data.NetHardCon == 1)      //串口数据处理
                {
                    if (Net.IsExists())
                    {
                        S2data.NetSoftCon = 1;
                        count = 0;
                    }
                    else
                    {
                        count++;
                        if (count > 1)
                        {
                            count = 0;
                            S2data.NetHardCon = 0;
                            S2data.NetSoftCon = 0;
                            lForm.RecodeInfo("网口心跳异常");
                            threadNet.Abort();            //避免重复新建线程
                        }
                    }

                    if (S2data.NetSoftCon == 1)     //软链接成功
                    {

                        if (S2data.GetFirstNum() == 0)    //读取固件信息
                        {
                            S2data.SetFirstNum(1);

                            //获取主板名称
                            S2data.SetName(Net.GetCardName());
                            //获取固件编号
                            S2data.SetSN(Net.GetSN());
                            //获取编码开关状态
                            S2data.DIP = Net.GetDipSwitch();
                            //获取版本信息
                            S2data.SetVer_Info(Net.GetVerInfo());
                        }

                        int axis = S2data.Axis;
                        //获取轴位置
                        S2data.SetLocation(axis, Net.GetPosition(axis));
                        //获取轴IO
                        S2data.SetPWMIOState(axis, Net.GetAIO());

                        if (S2data.GetCardID() == 1)
                        {
                            //获取主板输入端口值 
                            S2data.MInput = Net.GetInputsEx();
                            //获取主板输出端口值
                            S2data.MOutput = Net.GetOutputsEx();
                        }
                        else
                        {
                            //获取主板输入端口值 
                            S2data.MInput = Net.GetInputs();
                            //获取主板输出端口值
                            S2data.MOutput = Net.GetOutputs();
                        }

                        //获取轴状态
                        S2data.SetPWMState(axis, Net.GetAxisStatus(axis));
                        if (S2data.GetShowMode(axis) == 1)
                        {
                            if ((S2data.GetPWMState(axis) >> 1 & 0x1) == 0)
                            {
                                int Dis = S2data.GetDire(axis) > 0 ? S2data.GetDistence(axis) : -S2data.GetDistence(axis);
                                Net.RltMove(S2data.Axis, Dis, S2data.GetStartSpd(axis), S2data.GetRunSpd(axis), S2data.GetAcce(axis), S2data.GetDece(axis));
                                if (S2data.GetDire(axis) == 0)
                                {
                                    S2data.SetDire(axis, 1);
                                }
                                else
                                {
                                    S2data.SetDire(axis, 0);
                                }
                            }
                        }

                    }


                }


            }

        }

        /// <summary>
        /// 配置开关量输出
        /// </summary>
        /// <param name="outnum"></param>
        private void Config_Out(int outnum)
        {
            int MOutput = S2data.MOutput;
            int id = S2data.GetCardID();

            if ((MOutput >> outnum & 0x1) == 0) MOutput |= 1 << outnum;
            else MOutput &= (1 << outnum) ^ (0xFFFF);

            if ((S2data.NetSoftCon == 1))
            {
                if (S2data.GetCardID() == 1)
                {
                    Net.SetOutputEx(outnum, (MOutput >> outnum & 0x1) == 0 ? false : true);
                }
                else Net.SetOutputs((byte)MOutput);

            }
            else if ((S2data.COMHardCon == 1))
            {
                if (S2data.GetCardID() == 1)
                {
                    Com.SetOutputEx(outnum, (MOutput >> outnum & 0x1) == 0 ? false : true);
                }
                else Com.SetOutputs((byte)MOutput);
            }

        }
        /// <summary>
        /// 输出口Y0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedY0_Click(object sender, EventArgs e)
        {
            Config_Out(0);
        }

        private void btnLedY1_Click(object sender, EventArgs e)
        {
            Config_Out(1);
        }

        private void btnLedY2_Click(object sender, EventArgs e)
        {
            Config_Out(2);
        }

        private void btnLedY3_Click(object sender, EventArgs e)
        {
            Config_Out(3);
        }

        private void btnLedY4_Click(object sender, EventArgs e)
        {
            Config_Out(4);
        }

        private void btnLedY5_Click(object sender, EventArgs e)
        {
            Config_Out(5);
        }

        private void btnLedY6_Click(object sender, EventArgs e)
        {
            Config_Out(6);
        }

        private void btnLedY7_Click(object sender, EventArgs e)
        {
            Config_Out(7);
        }


        private void button_Turn_Click(int dir)
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

            S2data.SetDire(axis, dir);

            if ((S2data.NetSoftCon == 1) && !Net.IsBusy(axis))
            {
                if (S2data.GetRunMode(axis) == 0)  //点对点
                    Net.AbsMove(axis, pos, startVel, vel, acc, dec);
                else if (S2data.GetRunMode(axis) == 1)  //连续
                    Net.RltMove(axis, dir == 0 ? -2147483647 : 2147483647, startVel, vel, acc, dec);
                else if (S2data.GetRunMode(axis) == 2)  //原点
                    Net.Home(axis, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else if ((S2data.COMSoftCon == 1) && !Com.IsBusy(axis))
            {
                if (S2data.GetRunMode(axis) == 0)  //点对点
                    Com.AbsMove(axis, pos, startVel, vel, acc, dec);
                else if (S2data.GetRunMode(axis) == 1)  //连续
                    Com.RltMove(axis, dir == 0 ? -2147483647 : 2147483647, startVel, vel, acc, dec);
                else if (S2data.GetRunMode(axis) == 2)  //原点
                    Com.Home(axis, startVel, homeDir, homeSVel, vel, acc, dec, homeMode, offset);
            }
            else
            {
                lForm.RecodeInfo("执行错误!");
            }

        }
        /// <summary>
        /// 按钮-X轴向左
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXLeft_Click(object sender, EventArgs e)
        {
            button_Turn_Click(0);
        }
        private void btnXRight_Click(object sender, EventArgs e)
        {
            button_Turn_Click(1);
        }

        private void btnYLeft_Click(object sender, EventArgs e)
        {
            button_Turn_Click(0);
        }

        private void btnYRight_Click(object sender, EventArgs e)
        {
            button_Turn_Click(1);
        }

        private void btnZLeft_Click(object sender, EventArgs e)
        {
            button_Turn_Click(0);
        }

        private void btnZRight_Click(object sender, EventArgs e)
        {
            button_Turn_Click(1);
        }

        private void button_Stop_Click()
        {
            S2data.SetStopRunMode(1);
            int axisNo = S2data.Axis;

            if ((S2data.NetSoftCon == 1) /*&& !Net.IsBusy(axisNo)*/)
            {
                Net.Stop(axisNo);
            }
            else if ((S2data.COMSoftCon == 1)/* && !Com.IsBusy(axisNo)*/)
            {
                Com.Stop(axisNo);
            }
        }
        private void button1_EmgStop_Click()
        {
            S2data.SetStopRunMode(0);
            int axisNo = S2data.Axis;
            if ((S2data.NetSoftCon == 1) /*&& !Net.IsBusy(axisNo)*/)
            {
                Net.EmgStop(axisNo);
            }
            else if ((S2data.COMSoftCon == 1) /*&& !YKS2Com.IsBusy(axisNo)*/)
            {
                Com.EmgStop(axisNo);
            }
        }
        private void btnXDestop_Click(object sender, EventArgs e)
        {
            button_Stop_Click();
        }

        private void btnXEmstop_Click(object sender, EventArgs e)
        {
            button1_EmgStop_Click();
        }

        private void btnYDestop_Click(object sender, EventArgs e)
        {
            button_Stop_Click();
        }

        private void btnYEmstop_Click(object sender, EventArgs e)
        {
            button1_EmgStop_Click();
        }

        private void btnZDestop_Click(object sender, EventArgs e)
        {
            button_Stop_Click();
        }

        private void btnZEmstop_Click(object sender, EventArgs e)
        {
            button1_EmgStop_Click();
        }

        //为窗口2准备的函数接口 SignEnLimit;         //极限使能信号
        public int SetSignEnLimit { set { S2data.SetSignEnLimit(value); } get { return S2data.GetSignEnLimit() ? 1 : 0; } }
        //为窗口2准备的函数接口 SignEnOrigin;        //原点使能信号
        public int SetSignEnOrigin { set { S2data.SetSignEnOrigin(value); } get { return S2data.GetSignEnOrigin() ? 1 : 0; } }
        //为窗口2准备的函数接口 SignReversalLimit;   //反转极限信号
        public int SetSignReversalLimit { set { S2data.SetSignReLimit(value); } get { return S2data.GetSignReLimit() ? 1 : 0; } }
        //为窗口2准备的函数接口 SignReversalOrigin;  //反转原点信号
        public int SetSignReversalOrigin { set { S2data.SetSignReOrigin(value); } get { return S2data.GetSignReOrigin() ? 1 : 0; } }
        //为窗口2准备的函数接口 SignReversalOrigin;  //发送极限设置
        public void SetSignSendFirm()
        {
            if ((S2data.NetSoftCon == 1))
            {
                Net.SetLimits(S2data.Axis, S2data.GetSignEnLimit(), S2data.GetSignEnOrigin(), S2data.GetSignReLimit(), S2data.GetSignReOrigin());
            }
            else if ((S2data.COMSoftCon == 1))
            {
                Com.SetLimits(S2data.Axis, S2data.GetSignEnLimit(), S2data.GetSignEnOrigin(), S2data.GetSignReLimit(), S2data.GetSignReOrigin());
            }
               
        }


        /// <summary>
        /// 极限配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXConf_Click(object sender, EventArgs e)
        {
            ConfLimt IForm = new ConfLimt();
            IForm.Owner = this;
            IForm.Show();
        }
        private void btnYConf_Click(object sender, EventArgs e)
        {
            ConfLimt IForm = new ConfLimt();
            IForm.Owner = this;
            IForm.Show();
        }
        private void btnZConf_Click(object sender, EventArgs e)
        {
            ConfLimt IForm = new ConfLimt();
            IForm.Owner = this;
            IForm.Show();
        }

        private void ShowModeEvent(int axis)
        {
            if (this.rbXPosi.Checked == true)
                S2data.SetDire(0, 1);        //演示模式正方向
            else
                S2data.SetDire(0, 0);        //演示模式负方向

            if (this.rbYPosi.Checked == true)
                S2data.SetDire(1, 1);        //演示模式正方向
            else
                S2data.SetDire(1, 0);        //演示模式负方向

            if (this.rbZPosi.Checked == true)
                S2data.SetDire(2, 1);        //演示模式正方向
            else
                S2data.SetDire(2, 0);        //演示模式负方向



            if (S2data.GetShowMode(axis) == 0) 
            {
                if (axis == 0)
                {
                    S2data.SetShowMode(axis, 1);    //开始演示
                    btnXBegin.Text = "停止";
                    btnXConf.Enabled = false;  //极限设置
                    btnXLeft.Enabled = false; //向左键
                    btnXRight.Enabled = false; //向右键
                    btnXDestop.Enabled = false; //减速停
                    btnXEmstop.Enabled = false; //立即停

                }
                else if (axis == 1)
                {
                    S2data.SetShowMode(axis, 1);    //开始演示
                    btnYBegin.Text = "停止";
                    btnYConf.Enabled = false;  //极限设置
                    btnYLeft.Enabled = false; //向左键
                    btnYRight.Enabled = false; //向右键
                    btnYDestop.Enabled = false; //减速停
                    btnYEmstop.Enabled = false; //立即停
                }
                else
                {
                    S2data.SetShowMode(axis, 1);    //开始演示
                    btnZBegin.Text = "停止";
                    btnZConf.Enabled = false;  //极限设置
                    btnZLeft.Enabled = false; //向左键
                    btnZRight.Enabled = false; //向右键
                    btnZDestop.Enabled = false; //减速停
                    btnZEmstop.Enabled = false; //立即停
                }
            }
            else
            {
                if (axis == 0)
                {
                    S2data.SetShowMode(axis, 0);    //开始演示
                    btnXBegin.Text = "开始";
                    btnXConf.Enabled = true;  //极限设置
                    btnXLeft.Enabled = true; //向左键
                    btnXRight.Enabled = true; //向右键
                    btnXDestop.Enabled = true; //减速停
                    btnXEmstop.Enabled = true; //立即停
                }
                else if (axis == 1)
                {
                    S2data.SetShowMode(axis, 0);    //开始演示
                    btnYBegin.Text = "开始";
                    btnYConf.Enabled = true;  //极限设置
                    btnYLeft.Enabled = true; //向左键
                    btnYRight.Enabled = true; //向右键
                    btnYDestop.Enabled = true; //减速停
                    btnYEmstop.Enabled = true; //立即停
                }
                else
                {
                    S2data.SetShowMode(axis, 0);    //开始演示
                    btnZBegin.Text = "开始";
                    btnZConf.Enabled = true;  //极限设置
                    btnZLeft.Enabled = true; //向左键
                    btnZRight.Enabled = true; //向右键
                    btnZDestop.Enabled = true; //减速停
                    btnZEmstop.Enabled = true; //立即停
                }
            }
        }
        private void btnXBegin_Click(object sender, EventArgs e)
        {
            ShowModeEvent(0);
        }

        private void btnYBegin_Click(object sender, EventArgs e)
        {
            ShowModeEvent(1);
        }

        private void btnZBegin_Click(object sender, EventArgs e)
        {
            ShowModeEvent(2);
        }

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            int axis = S2data.Axis;
            if ((S2data.NetHardCon == 1) && !Net.IsBusy(axis))
            {
                Net.SetPosition(axis, 0);
            }
            else if ((S2data.COMHardCon == 1) && !Com.IsBusy(axis))
            {
                Com.SetPosition(axis, 0);
            }
        }
        /// <summary>
        /// 切换主卡/扩展卡
        /// </summary>
        void Tool_ChangeCardIDFont()
        {
            if (S2data.GetCardID() == 0)
            {
                S2data.SetCardID(1);  //切换到S1卡
                this.lbIOmain.Font = new Font("宋体", 9, FontStyle.Regular);
                this.lbIOextern.Font = new Font("宋体", 9, FontStyle.Bold);
                lForm.RecodeInfo("切换到S1卡!");
            }
            else
            {
                S2data.SetCardID(0);  //切换到S1卡
                this.lbIOmain.Font = new Font("宋体", 9, FontStyle.Bold);
                this.lbIOextern.Font = new Font("宋体", 9, FontStyle.Regular); 
                lForm.RecodeInfo("切换到主板!");
            }
            S2data.MInput = 0;
            S2data.MOutput = 0;
        }
        private void lbIOmain_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }

        private void lbIOextern_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }

        private void btnIO_Click(object sender, EventArgs e)
        {
            Tool_ChangeCardIDFont();
        }
    }
}
