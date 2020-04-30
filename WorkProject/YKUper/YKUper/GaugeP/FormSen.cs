using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Drawing;

namespace YKUper
{
    public partial class FormSen : Form
    {
        int[] Sensor1Volt = new int[10];
        int[] Sensor1Show = new int[10];
        int[] Sensor2Volt = new int[10];
        int[] Sensor2Show = new int[10];
        int[] Sensor3Volt = new int[10];
        int[] Sensor3Show = new int[10];
        int[] Sensor4Volt = new int[10];
        int[] Sensor4Show = new int[10];
        int[] SensorItems = new int[4];
        int[] SensorCalib = new int[4];
        int SensorSel = new int();
        //String[] item = { "0：无曲线", "1：曲线1", "2：曲线2", "3：曲线3", "4：曲线4" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
        //private int SenNum;
        public FormSen()
        {
            InitializeComponent();

            Comboboxn(cbCurve);
            ComboboxS(cbSenCh1);
            ComboboxS(cbSenCh2);
            ComboboxS(cbSenCh3);
            ComboboxS(cbSenCh4);

            string[] text = new string[]
            {
                "0.5","1","1.5","2","2.5","3","3.5","4","4.5","5"
            };
            //userCurve1.SetCurveText(text);
        }

        public void ComboboxS(ComboBox box)
        {
            String[] item = { "0：无曲线", "1：曲线1", "2：曲线2", "3：曲线3", "4：曲线4" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                box.Items.Add(item[i]);
                box.SelectedIndex = i;    //配置索引序号

            }
            box.SelectedItem = box.Items[0];    //默认
        }
        public void Comboboxn(ComboBox box)
        {
            string[] item = { "曲线1", "曲线2", "曲线3", "曲线4" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                box.Items.Add(item[i]);
                box.SelectedIndex = i;    //配置索引序号

            }
            box.SelectedItem = box.Items[0];    //默认为列表第二个变量 
        }

        private void FormSen_Load(object sender, EventArgs e)
        {
            //userCurve1.SetLeftCurve("A", GetRandomValueByCount(10, 0, 100), Color.DodgerBlue);

            

        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 获取指定范围的，指定个数的随机数数组
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private float[] GetRandomValueByCount(int count, float min, float max)
        {
            float[] data = new float[count];
            Random random = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (float)random.NextDouble() * (max - min) + min;
            }
            return data;
        }

        private void cbSenCh_TextChanged(int SenNum)
        {
            //SenNum = cbCurve.SelectedIndex;
            switch (SenNum)
            {
                case 0:
                    gbCurve.Text = "曲线 - 1 - 数据";
                    //userCurve1.Title = "曲线 - 1 - 图";
                    break;
                case 1:
                    gbCurve.Text = "曲线 - 2 - 数据";
                    //userCurve1.Title = "曲线 - 2 - 图";
                    break;
                case 2:
                    gbCurve.Text = "曲线 - 3 - 数据";
                    //userCurve1.Title = "曲线 - 3 - 图";
                    break;
                case 3:
                    gbCurve.Text = "曲线 - 4 - 数据";
                    //userCurve1.Title = "曲线 - 4 - 图";
                    break;
                default: break;

            }

        }

        /// <summary>
        /// 数据读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDRead_Click(object sender, EventArgs e)
        {
            FormGaugeP lForm = (FormGaugeP)this.Owner;//把Form2的父窗口指针赋给lForm1
            SensorCalib = lForm.GetSensorCalib();
            SensorItems = lForm.GetSensorItems();
            int len = SensorItems.Length;
            Sensor1Volt = lForm.GetSensorData(0);
            Sensor1Show = lForm.GetSensorData(1);
            Sensor2Volt = lForm.GetSensorData(2);
            Sensor2Show = lForm.GetSensorData(3);
            Sensor3Volt = lForm.GetSensorData(4);
            Sensor3Show = lForm.GetSensorData(5);
            Sensor4Volt = lForm.GetSensorData(6);
            Sensor4Show = lForm.GetSensorData(7);
            Curve_Selected_Fresh(SensorSel);

            cbSenCh1.SelectedIndex = SensorItems[0];
            cbSenCh2.SelectedIndex = SensorItems[1];
            cbSenCh3.SelectedIndex = SensorItems[2];
            cbSenCh4.SelectedIndex = SensorItems[3];

            tbSenCalib1.Text = SensorCalib[0].ToString();
            tbSenCalib2.Text = SensorCalib[1].ToString();
            tbSenCalib3.Text = SensorCalib[2].ToString();
            tbSenCalib4.Text = SensorCalib[3].ToString();

            btDWrite.Enabled = true;    //
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float[] temp = new float[10];
            for (int i = 0; i < 10; i++)
            {
                temp[i] = Sensor1Volt[i];
            }
            //userCurve1.SetLeftCurve("A", temp);
        }

        /// <summary>
        /// 切换传感器时，刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCurve_SelectedIndexChanged(object sender, EventArgs e)
        {
            SensorSel = cbCurve.SelectedIndex;  //获取索引，确定传感器刷新数据
            Curve_Selected_Fresh(SensorSel);
            cbSenCh_TextChanged(SensorSel);
        }

        /// <summary>
        /// 刷新指定传感器的数据
        /// </summary>
        /// <param name="sel"></param>
        void Curve_Selected_Fresh(int sel)
        {
            int[] tempV = new int[10];
            int[] tempS = new int[10];

            switch (sel)
            {
                case 0:
                    tempV = Sensor1Volt;
                    tempS = Sensor1Show;
                    break;
                case 1:
                    tempV = Sensor2Volt;
                    tempS = Sensor2Show;
                    break;
                case 2:
                    tempV = Sensor3Volt;
                    tempS = Sensor3Show;
                    break;
                case 3:
                    tempV = Sensor4Volt;
                    tempS = Sensor4Show;
                    break;
            }

            tbVolt0.Text = tempV[0].ToString();
            tbVolt1.Text = tempV[1].ToString();
            tbVolt2.Text = tempV[2].ToString();
            tbVolt3.Text = tempV[3].ToString();
            tbVolt4.Text = tempV[4].ToString();
            tbVolt5.Text = tempV[5].ToString();
            tbVolt6.Text = tempV[6].ToString();
            tbVolt7.Text = tempV[7].ToString();
            tbVolt8.Text = tempV[8].ToString();
            tbVolt9.Text = tempV[9].ToString();

            tbPress0.Text = ((Int16)tempS[0]).ToString();
            tbPress1.Text = ((Int16)tempS[1]).ToString();
            tbPress2.Text = ((Int16)tempS[2]).ToString();
            tbPress3.Text = ((Int16)tempS[3]).ToString();
            tbPress4.Text = ((Int16)tempS[4]).ToString();
            tbPress5.Text = ((Int16)tempS[5]).ToString();
            tbPress6.Text = ((Int16)tempS[6]).ToString();
            tbPress7.Text = ((Int16)tempS[7]).ToString();
            tbPress8.Text = ((Int16)tempS[8]).ToString();
            tbPress9.Text = ((Int16)tempS[9]).ToString();
        }

        private void btDWrite_Click(object sender, EventArgs e)
        {
            FormGaugeP lForm = (FormGaugeP)this.Owner;//把Form2的父窗口指针赋给lForm1

            lForm.SetSensorCalib(SensorCalib);
            SensorItems[0] = cbSenCh1.SelectedIndex;
            SensorItems[1] = cbSenCh2.SelectedIndex;
            SensorItems[2] = cbSenCh3.SelectedIndex;
            SensorItems[3] = cbSenCh4.SelectedIndex;
            lForm.SetSensorItems(SensorItems);
            lForm.SetSensorData(0, Sensor1Volt, Sensor1Show);
            lForm.SetSensorData(1, Sensor2Volt, Sensor2Show);
            lForm.SetSensorData(2, Sensor3Volt, Sensor3Show);
            lForm.SetSensorData(3, Sensor4Volt, Sensor4Show);

        }
        void Voltn_Fresh(int sel, int num, int Volt)
        {
            int tempV = Volt;
            switch (sel)
            {
                case 0:
                    Sensor1Volt[num] = tempV;
                    break;
                case 1:
                    Sensor2Volt[num] = tempV;
                    break;
                case 2:
                    Sensor3Volt[num] = tempV;
                    break;
                case 3:
                    Sensor4Volt[num] = tempV;
                    break;
            }
        }
        void Shown_Fresh(int sel, int num, int Show)
        {
            int tempS = Show;
            switch (sel)
            {
                case 0:
                    Sensor1Show[num] = tempS;
                    break;
                case 1:
                    Sensor2Show[num] = tempS;
                    break;
                case 2:
                    Sensor3Show[num] = tempS;
                    break;
                case 3:
                    Sensor4Show[num] = tempS;
                    break;
            }
        }
        private void tbVolt0_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt0.Text);
            Voltn_Fresh(SensorSel,0, temp);
        }

        private void tbVolt1_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt1.Text);
            Voltn_Fresh(SensorSel, 1, temp);
        }

        private void tbVolt2_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt2.Text);
            Voltn_Fresh(SensorSel, 2, temp);
        }

        private void tbVolt3_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt3.Text);
            Voltn_Fresh(SensorSel, 3, temp);
        }

        private void tbVolt4_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt4.Text);
            Voltn_Fresh(SensorSel, 4, temp);
        }

        private void tbVolt5_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt5.Text);
            Voltn_Fresh(SensorSel, 5, temp);
        }

        private void tbVolt6_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt6.Text);
            Voltn_Fresh(SensorSel, 6, temp);
        }

        private void tbVolt7_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt7.Text);
            Voltn_Fresh(SensorSel, 7, temp);
        }

        private void tbVolt8_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt8.Text);
            Voltn_Fresh(SensorSel, 8, temp);
        }

        private void tbVolt9_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbVolt9.Text);
            Voltn_Fresh(SensorSel, 9, temp);
        }

        private void tbPress0_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress0.Text);
            Shown_Fresh(SensorSel, 0, temp);
        }

        private void tbPress1_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress1.Text);
            Shown_Fresh(SensorSel, 1, temp);
        }

        private void tbPress2_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress2.Text);
            Shown_Fresh(SensorSel, 2, temp);
        }

        private void tbPress3_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress3.Text);
            Shown_Fresh(SensorSel, 3, temp);
        }

        private void tbPress4_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress4.Text);
            Shown_Fresh(SensorSel, 4, temp);
        }

        private void tbPress5_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress5.Text);
            Shown_Fresh(SensorSel,5, temp);
        }

        private void tbPress6_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress6.Text);
            Shown_Fresh(SensorSel, 6, temp);
        }

        private void tbPress7_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress7.Text);
            Shown_Fresh(SensorSel, 7, temp);
        }

        private void tbPress8_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress8.Text);
            Shown_Fresh(SensorSel, 8, temp);
        }

        private void tbPress9_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbPress9.Text);
            Shown_Fresh(SensorSel, 9, temp);
        }

        private void tbSenCalib1_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenCalib1.Text);
            SensorCalib[0] = temp;
        }

        private void tbSenCalib2_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenCalib2.Text);
            SensorCalib[1] = temp;
        }

        private void tbSenCalib3_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenCalib3.Text);
            SensorCalib[2] = temp;
        }

        private void tbSenCalib4_TextChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(tbSenCalib4.Text);
            SensorCalib[3] = temp;
        }
    }
}
