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
        /// <summary>
        /// 线程句柄-处理通讯过程
        /// </summary>
        private Thread thread = null;                   
        /// <summary>
        /// 刷新
        /// </summary>
        private bool Refresh = true;   
            
        public FormYKUCC52()
        {
            InitializeComponent();           
        }

        private void FormYKUCC52_Load(object sender, EventArgs e)
        {          
            lForm = (MainForm)this.Owner;//把Form2的父窗口指针赋给lForm1
            lForm.MyEventCOMVerYTUCC52 += new MainForm.MyDelegate(Main_Connet);//监听MainForm窗体事件
            UCdata = lForm.data;
            tFresh.Enabled = true;
            
            lForm.RecodeInfo("上位机版本为：调光器上位机");
            ToolTipShow(toolTip1, lbSW1, "测试效果");

            int[] temp = new int[4] { 0, 0, 0, 0 };
            UCdata.ucPWMstate = temp;
            UCdata.ucDutyCycle = temp;
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

                        UCdata.ucDutyCycle = Com.GetValue();      //获取通道占空比
                        UCdata.ucIOState = Com.GetIO();           //获取IO状态，LED、KEY、跳线
                        UCdata.ucPWMstate = Com.GetEnables();   //获取通道使能状态

                        if (Refresh)
                        {
                            tbCh1.Value = UCdata.ucDutyCycle[0];
                            tbCh2.Value = UCdata.ucDutyCycle[1];
                            tbCh3.Value = UCdata.ucDutyCycle[2];
                            tbCh4.Value = UCdata.ucDutyCycle[3];
                        }

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

        private void tFresh_Tick(object sender, EventArgs e)
        {


            lblCh1Val.Text = UCdata.ucDutyCycle[0].ToString();
            lblCh2Val.Text = UCdata.ucDutyCycle[1].ToString();
            lblCh3Val.Text = UCdata.ucDutyCycle[2].ToString();
            lblCh4Val.Text = UCdata.ucDutyCycle[3].ToString();

            btnEnable1.BackColor = (UCdata.ucPWMstate[0] == 0) ? Color.DarkRed : Color.Red;
            btnEnable2.BackColor = (UCdata.ucPWMstate[1] == 0) ? Color.DarkRed : Color.Red;
            btnEnable3.BackColor = (UCdata.ucPWMstate[2] == 0) ? Color.DarkRed : Color.Red;
            btnEnable4.BackColor = (UCdata.ucPWMstate[3] == 0) ? Color.DarkRed : Color.Red;

            lblCh1Led.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.LED5)) != 0) ? Color.Lime : Color.Black;
            lblCh2Led.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.LED4)) != 0) ? Color.Lime : Color.Black;
            lblCh3Led.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.LED3)) != 0) ? Color.Lime : Color.Black;
            lblCh4Led.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.LED2)) != 0) ? Color.Lime : Color.Black;

            lbLedS3.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.KEY1)) != 0) ? Color.Lime : Color.Black;
            lbLedS4.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.KEY2)) != 0) ? Color.Lime : Color.Black;
            lbLedS5.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.KEY3)) != 0) ? Color.Lime : Color.Black;
            lbLedS2.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.KEY4)) != 0) ? Color.Lime : Color.Black;

            lbLedLock.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.LockFlag)) != 0) ? Color.Lime : Color.Black;
            lbLedSW1.BackColor = ((UCdata.ucIOState & (1 << (int)YK_CCASP24WST.IOEnum.SW1)) != 0) ? Color.Lime : Color.Black;
 
        }


        private void btnIncCh_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            int[] vals = Com.GetValue();
            Com.SetValue(index, vals[index] + 10);
        }

        private void btnDecCh_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            int[] vals = Com.GetValue();
            Com.SetValue(index, vals[index] - 10);
        }
        private void btnIncCh2_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            int[] vals = Com.GetValue();
            Com.SetValue(index, vals[index] + 10);
        }
        private void btnDecCh2_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            int[] vals = Com.GetValue();
            Com.SetValue(index, vals[index] - 10);
        }

        private void btnIncCh3_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            int[] vals = Com.GetValue();
            Com.SetValue(index, vals[index] + 10);
        }

        private void btnDecCh3_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            int[] vals = Com.GetValue();
            Com.SetValue(index, vals[index] - 10);
        }

        private void btnIncCh4_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            int[] vals = Com.GetValue();
            Com.SetValue(index, vals[index] + 10);
        }

        private void btnDecCh4_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Control).Tag.ToString());
            int[] vals = Com.GetValue();
            Com.SetValue(index, vals[index] - 10);
        }

        private void lbEnable_Click(object sender, EventArgs e)
        {
            string index = (sender as Label).Tag as string;
            int ch = Int32.Parse(index);
            Com.SetEnable(ch, (sender as Label).BackColor == Color.DarkRed);
        }

        private void tbCh_MouseDown(object sender, MouseEventArgs e)
        {
            Refresh = false;
        }

        private void tbCh_MouseUp(object sender, MouseEventArgs e)
        {
            TrackBar[] tbn = new TrackBar[] { tbCh1, tbCh2, tbCh3, tbCh4 };
            string index = (sender as Control).Tag as string;
            int ch = Int32.Parse(index);
            Com.SetValue(ch, tbn[ch].Value);
            Refresh = true;
        }

    }
}
