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
using Yungku.Common.GaugeP;
using System.Threading;
namespace YKUper
{
    public partial class FormGaugeP : Form
    {
        /// <summary>
        /// 主窗口的句柄
        /// </summary>
        private MainForm lForm = null;
        /// <summary>
        /// 用于复制数据集
        /// </summary>
        public DataDeal GPdata = null;
        /// <summary>
        /// 串口实例
        /// </summary>
        private YKGaugeP Com = new YKGaugeP();
        /// <summary>
        /// 处理串口通讯
        /// </summary>
        private Thread threadGP = null;

        private bool WRABLE = false;

        public FormGaugeP()
        {
            InitializeComponent();
            tbAddr.Text = "1";
            tbbaud.Text = "115200";
            Comboboxn(cbSenAct1);
            tbSendelay1.Text = "0";
            tbSenMin1.Text = "0";
            tbSenMax1.Text = "0";
            Comboboxn(cbSenAct2);
            tbSendelay2.Text = "0";
            tbSenMin2.Text = "0";
            tbSenMax2.Text = "0";
            Comboboxn(cbSenAct3);
            tbSendelay3.Text = "0";
            tbSenMin3.Text = "0";
            tbSenMax3.Text = "0";
            Comboboxn(cbSenAct4);
            tbSendelay4.Text = "0";
            tbSenMin4.Text = "0";
            tbSenMax4.Text = "0";
        }

        private void FormGaugeP_Load(object sender, EventArgs e)
        {
            lForm = (MainForm)this.Owner;//把FormGaugeP的父窗口指针赋给lForm1
            lForm.MyEventCOMVerGaugeP += new MainForm.MyDelegate(GP_Connet);//监听MainForm窗体事件
            GPdata = lForm.data;
            timerRefresh.Enabled = true;

            lForm.RecodeInfo("上位机版本为：负压表上位机");
        }

        public void Comboboxn(ComboBox box)
        {
            string[] item = { "无动作", "低限制", "高限制","全部限制" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                box.Items.Add(item[i]);
                box.SelectedIndex = i;    //配置索引序号

            }
            box.SelectedItem = box.Items[0];    //默认为列表第二个变量 
        }

        /// <summary>
        /// 主窗体打开串口事件
        /// </summary>
        private void GP_Connet()
        {
            try
            {
                if (GPdata.COMHardCon == 0)    //判断串口是否打开
                {
                    Com.Port = GPdata.Comport;
                    Com.BaudRate = GPdata.Baudrate;
                    Com.Timeout = GPdata.Comtimeout;
                    Com.Open();         //打开串口

                    GPdata.COMHardCon = 1;
                    lForm.RecodeInfo("打开串口");

                    threadGP = new Thread(threadGP_Refresh);
                    threadGP.Start();
                    WRABLE = false;
                }
                else
                {
                    Com.Close();
                    GPdata.COMHardCon = 0;
                    GPdata.COMSoftCon = 0;
                    lForm.RecodeInfo("关闭串口");
                    threadGP.Abort();
                    WRABLE = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 线程主体，循环发送串口指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void threadGP_Refresh()
        {
            while (true)
            {
                Thread.Sleep(200);
                if (GPdata.COMHardCon == 1)      //串口数据处理
                {
                    while (WRABLE) ;
                    WRABLE = true;
                    int addr = GPdata.Comaddr;
                    GPdata.GPOutput = Com.GetOutputs(addr);
                    GPdata.GPLed = Com.GetLeds(addr);
                    GPdata.GPPolarity = Com.GetOutputPol(addr);
                    GPdata.GPState = Com.GetSTATE0(addr);
                    int[] sensorPV = Com.GetSensors(addr);
                    GPdata.SensorP1 = sensorPV[0];
                    GPdata.SensorP2 = sensorPV[1];
                    GPdata.SensorP3 = sensorPV[2];
                    GPdata.SensorP4 = sensorPV[3];
                    WRABLE = false;
                }

            }


        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRefresh_Tick(object sender, EventArgs e)
        {           
            WindosShowData();
            WindosShowLED();
            label1.Text = GPdata.GPPolarity.ToString();


            float[] Volt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            float[] Show = {10,20,30,40,50,60,70,80,90,100 };
            GPdata.Sensor1Volt = Volt;
            GPdata.Sensor1Show = Show;
        }

        /// <summary>
        /// 窗口数据变化
        /// </summary>
        void WindosShowData()
        {
            int temp = 0;
            tbSensor1.Text = GPdata.SensorP1.ToString();
            tbSensor2.Text = GPdata.SensorP2.ToString();
            tbSensor3.Text = GPdata.SensorP3.ToString();
            tbSensor4.Text = GPdata.SensorP4.ToString();



            temp = Convert.ToInt32(tbAddr.Text);        //记录通讯地址
            GPdata.Comaddr = temp;

            //temp = Convert.ToInt32(tbbaud.Text);
            //GPdata.Baudrate = temp;


            //GPdata.GPNumberBurn = Data[5];      //485使能

            //temp = Convert.ToInt32(tbbaud.Text);
            //GPdata.Baudrate = temp;

            temp = cbSenAct1.SelectedIndex;
            GPdata.GPSenAct1 = temp;
            temp = Convert.ToInt32(tbSendelay1.Text);
            GPdata.GPSenDelay1 = temp;
            temp = Convert.ToInt32(tbSenMin1.Text);
            GPdata.GPSenMin1 = temp;
            temp = Convert.ToInt32(tbSenMax1.Text);
            GPdata.GPSenMax1 = temp;

            temp = cbSenAct2.SelectedIndex;
            GPdata.GPSenAct2 = temp;
            temp = Convert.ToInt32(tbSendelay2.Text);
            GPdata.GPSenDelay2 = temp;
            temp = Convert.ToInt32(tbSenMin2.Text);
            GPdata.GPSenMin2 = temp;
            temp = Convert.ToInt32(tbSenMax2.Text);
            GPdata.GPSenMax2 = temp;

            temp = cbSenAct3.SelectedIndex;
            GPdata.GPSenAct3 = temp;
            temp = Convert.ToInt32(tbSendelay3.Text);
            GPdata.GPSenDelay3 = temp;
            temp = Convert.ToInt32(tbSenMin3.Text);
            GPdata.GPSenMin3 = temp;
            temp = Convert.ToInt32(tbSenMax3.Text);
            GPdata.GPSenMax3 = temp;

            temp = cbSenAct4.SelectedIndex;
            GPdata.GPSenAct4 = temp;
            temp = Convert.ToInt32(tbSendelay4.Text);
            GPdata.GPSenDelay4 = temp;
            temp = Convert.ToInt32(tbSenMin4.Text);
            GPdata.GPSenMin4 = temp;
            temp = Convert.ToInt32(tbSenMax4.Text);
            GPdata.GPSenMax4 = temp;
        }

        /// <summary>
        /// 窗口灯光变化
        /// </summary>
        void WindosShowLED()
        {
            int Led = GPdata.GPLed;
            int Output = GPdata.GPOutput;
            //开关量输出
            if ((Output >> 0 & 0x1) == 0)
                btnOut1.BackColor = Color.FromArgb(28, 66, 28);    //开关1
            else
                btnOut1.BackColor = Color.FromArgb(44, 255, 44);
            if ((Output >> 1 & 0x1) == 0)
                btnOut2.BackColor = Color.FromArgb(28, 66, 28);    //开关2
            else
                btnOut2.BackColor = Color.FromArgb(44, 255, 44);
            if ((Output >> 2 & 0x1) == 0)
                btnOut3.BackColor = Color.FromArgb(28, 66, 28);    //开关3
            else
                btnOut3.BackColor = Color.FromArgb(44, 255, 44);
            if ((Output >> 3 & 0x1) == 0)
                btnOut4.BackColor = Color.FromArgb(28, 66, 28);    //开关4
            else
                btnOut4.BackColor = Color.FromArgb(44, 255, 44);

            //开关量输出
            if ((Led >> 0 & 0x1) == 0)
                btnLed1.BackColor = Color.FromArgb(28, 66, 28);    //灯光1
            else
                btnLed1.BackColor = Color.FromArgb(44, 255, 44);
            if ((Led >> 1 & 0x1) == 0)
                btnLed2.BackColor = Color.FromArgb(28, 66, 28);    //灯光2
            else
                btnLed2.BackColor = Color.FromArgb(44, 255, 44);
            if ((Led >> 2 & 0x1) == 0)
                btnLed3.BackColor = Color.FromArgb(28, 66, 28);    //灯光3
            else
                btnLed3.BackColor = Color.FromArgb(44, 255, 44);
            if ((Led >> 3 & 0x1) == 0)
                btnLed4.BackColor = Color.FromArgb(28, 66, 28);    //灯光4
            else
                btnLed4.BackColor = Color.FromArgb(44, 255, 44);
        }

        private void FormGaugeP_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRefresh.Enabled = false;
        }

        /// <summary>
        /// 读取 参数数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btReadP_Click(object sender, EventArgs e)
        {
            int addr = GPdata.Comaddr;
            while (WRABLE) ;
            WRABLE = true;
            int[] Data = Com.GetFlashDBP(addr);
            WRABLE = false;

            GPdata.Baudrate = Data[0];
            GPdata.GPSenAct1 = Data[1];
            GPdata.GPSenDelay1 = Data[2];
            GPdata.GPSenMin1 = Data[3];
            GPdata.GPSenMax1 = Data[4];
            GPdata.GPSenAct2 = Data[5];
            GPdata.GPSenDelay2 = Data[6];
            GPdata.GPSenMin2 = Data[7];
            GPdata.GPSenMax2 = Data[8];
            GPdata.GPSenAct3 = Data[9];
            GPdata.GPSenDelay3 = Data[10];
            GPdata.GPSenMin3 = Data[11];
            GPdata.GPSenMax3 = Data[12];
            GPdata.GPSenAct4 = Data[13];
            GPdata.GPSenDelay4 = Data[14];
            GPdata.GPSenMin4 = Data[15];
            GPdata.GPSenMax4 = Data[16];

            cbSenAct1.SelectedIndex = GPdata.GPSenAct1;
            tbSendelay1.Text = GPdata.GPSenDelay1.ToString();
            tbSenMin1.Text = GPdata.GPSenMin1.ToString();
            tbSenMax1.Text = GPdata.GPSenMax1.ToString();

            cbSenAct2.SelectedIndex = GPdata.GPSenAct2;
            tbSendelay2.Text = GPdata.GPSenDelay2.ToString();
            tbSenMin2.Text = GPdata.GPSenMin2.ToString();
            tbSenMax2.Text = GPdata.GPSenMax2.ToString();

            cbSenAct3.SelectedIndex = GPdata.GPSenAct3;
            tbSendelay3.Text = GPdata.GPSenDelay3.ToString();
            tbSenMin3.Text = GPdata.GPSenMin3.ToString();
            tbSenMax3.Text = GPdata.GPSenMax3.ToString();

            cbSenAct4.SelectedIndex = GPdata.GPSenAct4;
            tbSendelay4.Text = GPdata.GPSenDelay4.ToString();
            tbSenMin4.Text = GPdata.GPSenMin4.ToString();
            tbSenMax4.Text = GPdata.GPSenMax4.ToString();

        }

        private void btWriteP_Click(object sender, EventArgs e)
        {
            int addr = GPdata.Comaddr;
            int[] data = new int[18];

            //data[0] = GPdata.GPSen1Selec;
            //data[1] = GPdata.GPSen2Selec;
            //data[2] = GPdata.GPSen3Selec;
            //data[3] = GPdata.GPSen4Selec;
            data[0] = 1;       //485使能 ，默认允许设置
            data[1] = 0;// GPdata.Baudrate;

            data[2]     = GPdata.GPSenAct1;
            data[3]     = GPdata.GPSenDelay1;
            data[4]     = GPdata.GPSenMin1;
            data[5]    = GPdata.GPSenMax1;

            data[6] = GPdata.GPSenAct2;
            data[7] = GPdata.GPSenDelay2;
            data[8] = GPdata.GPSenMin2;
            data[9] = GPdata.GPSenMax2;

            data[10] = GPdata.GPSenAct3;
            data[11] = GPdata.GPSenDelay3;
            data[12] = GPdata.GPSenMin3;
            data[13] = GPdata.GPSenMax3;

            data[14] = GPdata.GPSenAct4;
            data[15] = GPdata.GPSenDelay4;
            data[16] = GPdata.GPSenMin4;
            data[17] = GPdata.GPSenMax4;
            while (WRABLE) ;
            WRABLE = true;
            if (Com.SetFlashDBP(addr, data, 18))
            {
                lForm.RecodeInfo("写入成功");
            }
            else lForm.RecodeInfo("写入失败");
            WRABLE = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormPol IForm = new FormPol();
            IForm.Owner = this;
            IForm.Show();
        }


        /// <summary>
        /// 获取输出口极性、LED极性
        /// </summary>
        public int GetPolarity()
        {
            return GPdata.GPPolarity;
        }
        /// <summary>
        /// 获取输出口极性、LED极性
        /// </summary>
        public void SetPolarity(int Pol)
        {
            int addr = GPdata.Comaddr;
            if (Com.SetOutputPol(addr, Pol))
            {
                lForm.RecodeInfo("写入成功");
            }
            else lForm.RecodeInfo("写入失败");

        }

        /// <summary>
        /// 跳转传感器界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            FormSen IForm = new FormSen();
            IForm.Owner = this;
            IForm.Show();
        }
        public float[] GetSensorData(int index)
        {
            float[] temp = new float[10];
            switch (index)
            {
                case 0:
                    temp = GPdata.Sensor1Volt;
                    break;
                case 1:
                    temp = GPdata.Sensor1Show;
                    break;
                case 2:
                    temp = GPdata.Sensor2Volt;
                    break;
                case 3:
                    temp = GPdata.Sensor2Show;
                    break;
                case 4:
                    temp = GPdata.Sensor3Volt;
                    break;
                case 5:
                    temp = GPdata.Sensor3Show;
                    break;
                case 6:
                    temp = GPdata.Sensor4Volt;
                    break;
                case 7:
                    temp = GPdata.Sensor4Show;
                    break;
                default:break;

            }
            return temp;
        }
    }
}
