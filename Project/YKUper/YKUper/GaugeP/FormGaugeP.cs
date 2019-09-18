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
            GPdata.Comaddr = Convert.ToInt32(tbAddr.Text);     //记录默认的地址
        }

        public void Comboboxn(ComboBox box)
        {
            string[] item = { "无动作", "低限制", "高限制", "全部限制" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                box.Items.Add(item[i]);
                box.SelectedIndex = i;    //配置索引序号

            }
            box.SelectedItem = box.Items[0];    //默认
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
                    tbAddr.ReadOnly = true;
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
            int count = 0;
            while (true)
            {
                Thread.Sleep(50);
                int addr = GPdata.Comaddr;
                while (WRABLE) ;
                WRABLE = true;
                if (Com.IsExists(addr))
                {

                    GPdata.COMSoftCon = 1;
                    count = 0;
                }
                else
                {
                    count++;

                    if (count > 5)
                    {
                        count = 0;
                        GPdata.COMHardCon = 0;
                        GPdata.COMSoftCon = 0;
                        lForm.RecodeInfo("心跳异常");
                        Com.Close();
                        threadGP.Abort();            //避免重复新建线程
                    }
                }
                WRABLE = false;
                if (GPdata.COMSoftCon == 1)      //串口数据处理
                {
                    while (WRABLE) ;
                    WRABLE = true;

                    if (GPdata.GetFirstNum() == 0)    //读取固件信息
                    {
                        GPdata.SetFirstNum(1);
                        //获取版本信息
                        GPdata.SetVer_Info(Com.GetVerInfo(addr));
                    }

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

            if (GPdata.COMSoftCon == 0)
            {
                btWriteP.Enabled = false;
                btSave.Enabled = false;
                btReset.Enabled = false;
                tbAddr.ReadOnly = false;
            }
            //float[] Volt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //float[] Show = {10,20,30,40,50,60,70,80,90,100 };
            //GPdata.Sensor1Volt = Volt;
            //GPdata.Sensor1Show = Show;
        }

        /// <summary>
        /// 窗口数据变化
        /// </summary>
        void WindosShowData()
        {
            int temp = 0;
            tbSensor1.Text = ((Int16)GPdata.SensorP1).ToString();
            tbSensor2.Text = ((Int16)GPdata.SensorP2).ToString();
            tbSensor3.Text = ((Int16)GPdata.SensorP3).ToString();
            tbSensor4.Text = ((Int16)GPdata.SensorP4).ToString();

            temp = cbSenAct1.SelectedIndex;
            GPdata.GPSenAct1 = temp;
            temp = cbSenAct2.SelectedIndex;
            GPdata.GPSenAct2 = temp;
            temp = cbSenAct3.SelectedIndex;
            GPdata.GPSenAct3 = temp;
            temp = cbSenAct4.SelectedIndex;
            GPdata.GPSenAct4 = temp;
      
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
            //while (WRABLE) ;
            WRABLE = true;
            int[] Data = Com.GetFlashDBP(addr);
            WRABLE = false;
            if (Com.MODState == 1)
            {
                lForm.RecodeInfo("读取参数成功");
                btWriteP.Enabled    = true;
                btSave.Enabled      = true;
                btReset.Enabled     = true;
            }
            else
            {
                lForm.RecodeInfo("读取参数失败");
            }
            

            GPdata.GPSenAct1 = Data[0];
            GPdata.GPSenDelay1 = Data[1];
            GPdata.GPSenMin1 = Data[2];
            GPdata.GPSenMax1 = Data[3];
            GPdata.GPSenAct2 = Data[4];
            GPdata.GPSenDelay2 = Data[5];
            GPdata.GPSenMin2 = Data[6];
            GPdata.GPSenMax2 = Data[7];
            GPdata.GPSenAct3 = Data[8];
            GPdata.GPSenDelay3 = Data[9];
            GPdata.GPSenMin3 = Data[10];
            GPdata.GPSenMax3 = Data[11];
            GPdata.GPSenAct4 = Data[12];
            GPdata.GPSenDelay4 = Data[13];
            GPdata.GPSenMin4 = Data[14];
            GPdata.GPSenMax4 = Data[15];

            try
            {
                cbSenAct1.SelectedIndex = GPdata.GPSenAct1;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
            tbSendelay1.Text = GPdata.GPSenDelay1.ToString();
            tbSenMin1.Text = GPdata.GPSenMin1.ToString();
            tbSenMax1.Text = GPdata.GPSenMax1.ToString();

            try
            {
                cbSenAct2.SelectedIndex = GPdata.GPSenAct2;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
    
            tbSendelay2.Text = GPdata.GPSenDelay2.ToString();
            tbSenMin2.Text = GPdata.GPSenMin2.ToString();
            tbSenMax2.Text = GPdata.GPSenMax2.ToString();

            try
            {
                cbSenAct3.SelectedIndex = GPdata.GPSenAct3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
            tbSendelay3.Text = GPdata.GPSenDelay3.ToString();
            tbSenMin3.Text = GPdata.GPSenMin3.ToString();
            tbSenMax3.Text = GPdata.GPSenMax3.ToString();

            try
            {
                cbSenAct4.SelectedIndex = GPdata.GPSenAct4;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tbSendelay4.Text = GPdata.GPSenDelay4.ToString();
            tbSenMin4.Text = GPdata.GPSenMin4.ToString();
            tbSenMax4.Text = GPdata.GPSenMax4.ToString();

        }

        private void btWriteP_Click(object sender, EventArgs e)
        {
            int addr = GPdata.Comaddr;
            int[] data = new int[16];

            data[0] = GPdata.GPSenAct1;
            data[1] = GPdata.GPSenDelay1;
            data[2] = GPdata.GPSenMin1;
            data[3] = GPdata.GPSenMax1;

            data[4] = GPdata.GPSenAct2;
            data[5] = GPdata.GPSenDelay2;
            data[6] = GPdata.GPSenMin2;
            data[7] = GPdata.GPSenMax2;

            data[8] = GPdata.GPSenAct3;
            data[9] = GPdata.GPSenDelay3;
            data[10] = GPdata.GPSenMin3;
            data[11] = GPdata.GPSenMax3;

            data[12] = GPdata.GPSenAct4;
            data[13] = GPdata.GPSenDelay4;
            data[14] = GPdata.GPSenMin4;
            data[15] = GPdata.GPSenMax4;
            //while (WRABLE) ;
            WRABLE = true;
            if (Com.SetFlashDBP(addr, data, 16))
            {
                lForm.RecodeInfo("写入参数成功");
            }
            else lForm.RecodeInfo("写入参数失败");
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
                lForm.RecodeInfo("端口极性配置写入成功");
            }
            else lForm.RecodeInfo("端口极性配置写入失败");

        }
        /// <summary>
        /// 设置传感器配置
        /// </summary>
        public void SetSensorItems(int[] items)
        {
            int addr = GPdata.Comaddr;
            if (Com.SetSensorItems(addr, items))
            {
                lForm.RecodeInfo("传感器配置写入成功");
            }
            else lForm.RecodeInfo("传感器配置写入失败");

        }
        /// <summary>
        /// 设置传感器校准值
        /// </summary>
        public void SetSensorCalib(int[] items)
        {
            int addr = GPdata.Comaddr;
            if (!Com.SetSensorCalib(addr, items))
            {
                lForm.RecodeInfo("传感器校准值写入失败");
            }
        }
        /// <summary>
        /// 设置传感器曲线数据
        /// </summary>
        public void SetSensorData(int num ,int[] itemV, int[] itemS)
        {
            int addr = GPdata.Comaddr;
            bool temb = false;
            switch (num)
            {
                case 0:
                    if (Com.SetSensor1Volt(addr, itemV))
                    {
                        temb = Com.SetSensor1Show(addr, itemS);
                    }
                    break;
                case 1:
                    if (Com.SetSensor2Volt(addr, itemV))
                    {
                        temb = Com.SetSensor2Show(addr, itemS);
                    }
                    break;
                case 2:
                    if (Com.SetSensor3Volt(addr, itemV))
                    {
                        temb = Com.SetSensor3Show(addr, itemS);
                    }
                    break;
                case 3:
                    if (Com.SetSensor4Volt(addr, itemV))
                    {
                        temb = Com.SetSensor4Show(addr, itemS);
                    }
                    break;


            }
            if (!temb)
            {
                lForm.RecodeInfo("传感器" + num + "数据写入失败");
            }

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
        /// <summary>
        /// 获取固件内部4个传感器的选项
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int[] GetSensorItems()
        {
            int addr = GPdata.Comaddr;
            int[] temp = Com.GetSensorItems(addr);
            GPdata.GPSen1Selec = temp[0];
            GPdata.GPSen2Selec = temp[1];
            GPdata.GPSen3Selec = temp[2];
            GPdata.GPSen4Selec = temp[3];
            return temp;
        }
        /// <summary>
        /// 获取固件内部4个传感器的校准值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int[] GetSensorCalib()
        {
            int addr = GPdata.Comaddr;
            int[] temp = Com.GetSensorCalib(addr);
            GPdata.GPSen1Calib = temp[0];
            GPdata.GPSen2Calib = temp[1];
            GPdata.GPSen3Calib = temp[2];
            GPdata.GPSen4Calib = temp[3];
            return temp;
        }
        /// <summary>
        /// 获取指定的传感器电压值或显示值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int[] GetSensorData(int index)
        {
            int[] temp = new int[10];
            int addr = GPdata.Comaddr;
            switch (index)
            {
                case 0:
                    GPdata.Sensor1Volt = Com.GetSensor1Volt(addr);
                    temp = GPdata.Sensor1Volt;
                    break;
                case 1:
                    GPdata.Sensor1Show = Com.GetSensor1Show(addr);
                    temp = GPdata.Sensor1Show;
                    break;
                case 2:
                    GPdata.Sensor2Volt = Com.GetSensor2Volt(addr);
                    temp = GPdata.Sensor2Volt;
                    break;
                case 3:
                    GPdata.Sensor2Show = Com.GetSensor2Show(addr);
                    temp = GPdata.Sensor2Show;
                    break;
                case 4:
                    GPdata.Sensor3Volt = Com.GetSensor3Volt(addr);
                    temp = GPdata.Sensor3Volt;
                    break;
                case 5:
                    GPdata.Sensor3Show = Com.GetSensor3Show(addr);
                    temp = GPdata.Sensor3Show;
                    break;
                case 6:
                    GPdata.Sensor4Volt = Com.GetSensor4Volt(addr);
                    temp = GPdata.Sensor4Volt;
                    break;
                case 7:
                    GPdata.Sensor4Show = Com.GetSensor4Show(addr);
                    temp = GPdata.Sensor4Show;
                    break;
                default:break;

            }
            return temp;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //while (WRABLE) ;
            WRABLE = true;
            int addr = GPdata.Comaddr;
            WRABLE = false;
            if (Com.SetSaveData(addr))
            {
                lForm.RecodeInfo("数据保存成功");
            }
            else lForm.RecodeInfo("数据保存失败");

        }

        private void btReset_Click(object sender, EventArgs e)
        {
            if ((int)MessageBox.Show("请确定恢复出厂设置", "提示", MessageBoxButtons.OKCancel) == 1)
            {
                //while (WRABLE) ;
                WRABLE = true;
                int addr = GPdata.Comaddr;
                WRABLE = false;
                if (Com.SetReset(addr))
                {
                    lForm.RecodeInfo("恢复出厂设置成功");
                }
                else lForm.RecodeInfo("恢复出厂设置失败");
            }
        }

        private void tbAddr_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbAddr.Text);        //记录通讯地址
            if (temp <= 255 && temp >= 0)
            {
                GPdata.Comaddr = temp;
            }
            else tbAddr.Text = GPdata.Comaddr.ToString();
        }

        private void tbSendelay1_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSendelay1.Text);
            GPdata.GPSenDelay1 = temp;
        }

        private void tbSenMin1_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenMin1.Text);
            GPdata.GPSenMin1 = temp;
        }

        private void tbSenMax1_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenMax1.Text);
            GPdata.GPSenMax1 = temp;
        }

        private void tbSendelay2_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSendelay2.Text);
            GPdata.GPSenDelay2 = temp;
        }

        private void tbSenMin2_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenMin2.Text);
            GPdata.GPSenMin2 = temp;
        }

        private void tbSenMax2_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenMax2.Text);
            GPdata.GPSenMax2 = temp;
        }

        private void tbSendelay3_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSendelay3.Text);
            GPdata.GPSenDelay3 = temp;
        }

        private void tbSenMin3_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenMin3.Text);
            GPdata.GPSenMin3 = temp;
        }

        private void tbSenMax3_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenMax3.Text);
            GPdata.GPSenMax3 = temp;
        }

        private void tbSendelay4_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSendelay4.Text);
            GPdata.GPSenDelay4 = temp;
        }
        private void tbSenMin4_Leave(object sender, EventArgs e)
        {
            int temp = 0;
            temp = Convert.ToInt32(tbSenMin4.Text);
            GPdata.GPSenMin4 = temp;
        }

        private void tbSenMax4_Leave(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenMax4.Text);
            GPdata.GPSenMax4 = temp;
        }


    }
}
