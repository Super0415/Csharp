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
using Yungku.Common.IOCardS1;
using System.Threading;
namespace YKUper
{
    public partial class FormS1 : Form
    {
        /// <summary>
        /// 主窗口的句柄
        /// </summary>
        private MainForm lForm = null;
        /// <summary>
        /// 用于复制数据集
        /// </summary>
        private DataDeal S1data = null;
        /// <summary>
        /// 串口实例
        /// </summary>
        private YKS1Card Com = new YKS1Card();
        private Thread thread = null;                   //线程句柄-处理通讯过程
        private long ShowLoc = 0;        //记录演示时的位置
        private bool ShowEn = true;        //记录演示发送使能
        private int ShowNum = 0;        //记录演示发送次数
        public FormS1()
        {
            InitializeComponent();
            tbDIP.Text      = "0";                    //DIP
            tbLoca.Text     = "0";                 //当前位置
            tbDist.Text     = "1000";                  //距离
            tbSpdrun.Text   = "1000";                  //运行速度
            Comboboxn();
        }

        

        private void FormS1_Load(object sender, EventArgs e)
        {
            lForm = (MainForm)this.Owner;//把Form2的父窗口指针赋给lForm1
            lForm.MyEventCOMVerS1 += new MainForm.MyDelegate(Main_Connet);//监听MainForm窗体事件
            S1data = lForm.data;
            timerRefresh.Enabled = true;

            lForm.RecodeInfo("上位机版本为：S1上位机");
        }

        public void Comboboxn()
        {
            string[] item = { "相对", "连续", "原点" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                cbMode.Items.Add(item[i]);
                cbMode.SelectedIndex = i;    //配置索引序号

            }
            cbMode.SelectedItem = cbMode.Items[0];    //默认为列表第二个变量 
        }

        /// <summary>
        /// 主窗体打开串口事件
        /// </summary>
        void Main_Connet()
        {

            try
            {
                if (S1data.COMHardCon == 0)    //判断串口是否打开
                {
                    Com.Port = S1data.Comport;
                    Com.Timeout = S1data.Comtimeout;
                    Com.Open();         //打开串口


                    S1data.COMHardCon = 1;
                    lForm.RecodeInfo("打开串口");

                    thread = new Thread(thread_Refresh);
                    thread.Start();
                }
                else
                {
                    Com.Close();
                    S1data.COMHardCon = 0;
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

                if (S1data.COMHardCon == 1)      //串口数据处理
                {
                    if (Com.IsExists())
                    {
             
                        S1data.COMSoftCon = 1;
                        count = 0;
                    }
                    else
                    {
                        count++;
          
                        if (count > 5)
                        {
                            count = 0;
                            S1data.COMHardCon = 0;
                            S1data.COMSoftCon = 0;
                            lForm.RecodeInfo("心跳异常");
                            thread.Abort();            //避免重复新建线程
                        }
                    }

                    if (S1data.COMSoftCon == 1)     //软链接成功
                    {

                        if (S1data.GetFirstNum() == 0)    //读取固件信息
                        {
                            S1data.SetFirstNum(1);
                            //获取版本信息
                            S1data.SetVer_Info(Com.GetVerInfo());
                        }

                        int axis = S1data.Axis;
                        //获取轴位置
                        S1data.SetLocation(axis, Com.GetPosition());
                        //获取DIP
                        if(Com.GetDIP() != Com.ErrNum)S1data.DIP = Com.GetDIP();
                        //获取输入口状态
                        if (Com.GetInputs() != Com.ErrNum) S1data.MInput = Com.GetInputs();
                        //获取输出口状态
                        if (Com.GetOutputs() != Com.ErrNum) S1data.MOutput = Com.GetOutputs();
                   
                        if (S1data.GetShowMode(axis) == 1)
                        {
                            int dir = S1data.GetDire(axis);
                            int dis = S1data.GetDistence(axis);
                            long Loca = S1data.GetLocation(axis);

                            if (ShowLoc - Loca == 0 && ShowNum == 0)
                            {
                                ShowEn = true;
                                ShowNum++;
                            }
                            else if (Math.Abs(ShowLoc - Loca) == dis && ShowNum == 1)
                            {
                                ShowEn = true;
                                ShowNum--;
                            }
                            if (ShowEn)
                            {
                                ShowEn = false;
                                Com.SetMMmove(S1data.GetDire(axis), S1data.GetDistence(axis), S1data.GetRunSpd(axis));
                                if (dir == 0)
                                {
                                    S1data.SetDire(axis, 1);
                                }
                                else
                                {
                                    S1data.SetDire(axis, 0);
                                }
                            }
                        }
                    }
                }
            }
        }

        //int temp = 0;
        //bool RefreshData_Time(int value,int dir,int dis)
        //{
        //    int Nowtime = DateTime.Now.Millisecond; ;
        //    int startime = 0;
        //    if (startime == 0)
        //    {
        //        temp = value;
        //        startime = Nowtime;
        //    }
        //    if (Nowtime - startime > 300)
        //    {
        //        if (temp == value)
        //        {
                    
        //            return true;
        //        }
                    


        //    }

        //    return false;


        //}
        /// <summary>
        /// 时间刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            int axis = S1data.Axis;
            FromNature((((S1data.COMHardCon == 1) || (S1data.NetHardCon == 1)) && (S1data.GetShowMode(axis) == 0)) ? true : false);

            WindosShowData();
            WindosShowLight();
        }
        /// <summary>
        /// 窗口数据变化
        /// </summary>
        void WindosShowData()
        {
            int temp = 0;
            int axis = S1data.Axis;
            tbDIP.Text = S1data.DIP.ToString();
            tbLoca.Text = S1data.GetLocation(axis).ToString();

            S1data.SetRunMode(axis, cbMode.SelectedIndex);                       //设置运动模式

            temp = Convert.ToInt32(tbDist.Text);
            S1data.SetDistence(axis, temp);

            temp = Convert.ToInt32(tbSpdrun.Text);
            S1data.SetRunSpd(axis, temp);
        }
        /// <summary>
        /// 窗口灯光变化
        /// </summary>
        void WindosShowLight()
        {
            int MInput = S1data.MInput;
            int MOutput = S1data.MOutput;
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

        }

        /// <summary>
        /// 关闭此窗口时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormS1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRefresh.Enabled = false;
        }

        /// <summary>
        /// 输出口控制
        /// </summary>
        /// <param name="outnum"></param>
        private void Config_Out(int outnum)
        {
            int MOutput = S1data.MOutput;
            if ((MOutput >> outnum & 0x1) == 0) MOutput |= 1 << outnum;
            else MOutput &= (1 << outnum) ^ (0xFFFF);

            Com.SetOutputs((byte)MOutput); 
        }
        /// <summary>
        /// Y0控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedY0_Click(object sender, EventArgs e)
        {
            Config_Out(0);
        }
        /// <summary>
        /// Y1控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedY1_Click(object sender, EventArgs e)
        {
            Config_Out(1);
        }
        /// <summary>
        /// Y2控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedY2_Click(object sender, EventArgs e)
        {
            Config_Out(2);
        }
        /// <summary>
        /// Y3控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedY3_Click(object sender, EventArgs e)
        {
            Config_Out(3);
        }
        /// <summary>
        /// Y4控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedY4_Click(object sender, EventArgs e)
        {
            Config_Out(4);
        }
        /// <summary>
        /// Y5控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedY5_Click(object sender, EventArgs e)
        {
            Config_Out(5);
        }
        /// <summary>
        /// Y6控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedY6_Click(object sender, EventArgs e)
        {
            Config_Out(6);
        }
        /// <summary>
        /// Y7控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedY7_Click(object sender, EventArgs e)
        {
            Config_Out(7);
        }

        /// <summary>
        /// 向左运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            button_Turn_Click(1);
        }
        /// <summary>
        /// 向右运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            button_Turn_Click(0);
        }
        private void button_Turn_Click(int dir)
        {
            int axisNo = S1data.Axis;           
            int dis = S1data.GetDistence(axisNo);
            int vel = S1data.GetRunSpd(axisNo);
            int MOutput = S1data.MOutput;
            S1data.SetDire(axisNo, dir);  //记录移动方向

            if ((S1data.COMSoftCon == 1)&&((MOutput & 0x1) == 0))
            {
                if (S1data.GetRunMode(axisNo) == 0)  //相对
                    Com.SetMMmove(dir, dis, vel);
                else if (S1data.GetRunMode(axisNo) == 1)  //连续
                    Com.SetMMmove(dir, 32767, vel);
                else if (S1data.GetRunMode(axisNo) == 2)  //原点
                    Com.SetMMHome(dir, dis, vel);
            }
            else
            {
                lForm.RecodeInfo("执行错误!");
            }

        }

        //}
        /// <summary>
        /// 停止运动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!Com.EmgStop())
            {
                lForm.RecodeInfo("执行错误!");
            }
        }

        /// <summary>
        /// 自定义菜单-清零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiClear_Click(object sender, EventArgs e)
        {
            Com.Clean();
        }

        /// <summary>
        /// 演示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBegin_Click(object sender, EventArgs e)
        {
            int axis = S1data.Axis;
            if (this.rbPosi.Checked == true)
                S1data.SetDire(axis, 0);        //演示模式正方向
            else
                S1data.SetDire(axis, 1);        //演示模式正方向

            if (S1data.GetShowMode(axis) == 0)
            {
                S1data.SetShowMode(axis,1);    //开始演示
                btnBegin.Text = "停止";
                ShowLoc = S1data.GetLocation(axis);
                btnLeft.Enabled = false; //向左键
                btnRight.Enabled = false; //向右键
                btnStop.Enabled = false; //立即停
            }
            else
            {
                S1data.SetShowMode(axis,0);   //停止演示
                btnBegin.Text = "开始";
                ShowNum = 0;
                btnLeft.Enabled = true; //向左键
                btnRight.Enabled = true; //向右键
                btnStop.Enabled = true; //立即停
            }
        }

        public void FromNature(bool able)
        {
            btnLedY0.Enabled = able; //Y0
            btnLedY1.Enabled = able; //Y1
            btnLedY2.Enabled = able; //Y2
            btnLedY3.Enabled = able; //Y3
            btnLedY4.Enabled = able; //Y4
            btnLedY5.Enabled = able; //Y5
            btnLedY6.Enabled = able; //Y6
            btnLedY7.Enabled = able; //Y7

            btnLeft.Enabled = able; //向左键
            btnRight.Enabled = able; //向右键
            btnStop.Enabled = able; //立即停
            btnBegin.Enabled = ((S1data.COMHardCon == 1) || (S1data.NetHardCon == 1)) ? true : false; //演习键
        }
    }
}
