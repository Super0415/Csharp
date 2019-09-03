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
        float[] Sensor1Volt = new float[10];
        float[] Sensor1Show = new float[10];
        float[] Sensor2Volt = new float[10];
        float[] Sensor2Show = new float[10];
        float[] Sensor3Volt = new float[10];
        float[] Sensor3Show = new float[10];
        float[] Sensor4Volt = new float[10];
        float[] Sensor4Show = new float[10];
        //String[] item = { "0：无曲线", "1：曲线1", "2：曲线2", "3：曲线3", "4：曲线4" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
        private int SenNum;
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
            userCurve1.SetCurveText(text);
        }

        public void ComboboxS(ComboBox box)
        {
            String[] item = { "0：无曲线", "1：曲线1", "2：曲线2", "3：曲线3", "4：曲线4" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < item.Length; i++)
            {
                box.Items.Add(item[i]);
                box.SelectedIndex = i;    //配置索引序号

            }
            box.SelectedItem = box.Items[0];    //默认为列表第二个变量 
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

        private void cbSenCh_TextChanged(object sender, EventArgs e)
        {
            SenNum = cbCurve.SelectedIndex;
            switch (SenNum)
            {
                case 0:
                    gbCurve.Text = "曲线 - 1 - 数据";
                    userCurve1.Title = "曲线 - 1 - 图";
                    break;
                case 1:
                    gbCurve.Text = "曲线 - 2 - 数据";
                    userCurve1.Title = "曲线 - 2 - 图";
                    break;
                case 2:
                    gbCurve.Text = "曲线 - 3 - 数据";
                    userCurve1.Title = "曲线 - 3 - 图";
                    break;
                case 3:
                    gbCurve.Text = "曲线 - 4 - 数据";
                    userCurve1.Title = "曲线 - 4 - 图";
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
            Sensor1Volt = lForm.GetSensorData(0);
            Sensor1Show = lForm.GetSensorData(1);
            Sensor2Volt = lForm.GetSensorData(2);
            Sensor2Show = lForm.GetSensorData(3);
            Sensor3Volt = lForm.GetSensorData(4);
            Sensor3Show = lForm.GetSensorData(5);
            Sensor4Volt = lForm.GetSensorData(6);
            Sensor4Show = lForm.GetSensorData(7);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            userCurve1.SetLeftCurve("A", Sensor1Volt);
        }
    }
}
