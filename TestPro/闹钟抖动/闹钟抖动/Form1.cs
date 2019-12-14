using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 闹钟抖动
{
    public partial class Form1 : Form
    {
        private DateTime SetAlarm = new DateTime();
        private int RecordX = 0;
        private int RecordY = 0;
        private int RecordCoordinate = 10;  //抖动幅度
        private bool StartAlarm = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\backpic.jpg");
        }

        /// <summary>
        /// 窗体移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            MyHandle_RecordbCoordinate();
        }
        /// <summary>
        /// 记录坐标
        /// </summary>
        private void MyHandle_RecordbCoordinate()
        {
            lbCoordinate.Text = "(X,Y):" + this.Location.X.ToString() + "," + this.Location.Y.ToString();
        }
        /// <summary>
        /// 闹钟开始窗体抖动
        /// </summary>
        private void MyHandle_AlarmJitter_ON()
        {
            Point p = new Point(0, this.Location.Y);
            p.X = this.Location.X > RecordX ? RecordX - RecordCoordinate : RecordX + RecordCoordinate;
            this.Location = p;
        }
        /// <summary>
        /// 事件-闹钟抖动消除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Event_AlarmJitter_OFF(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 任务：每0.5s刷新一次显示，达到闹钟时开始闹钟任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            
            if (SetAlarm.ToString("yyyy/MM/dd HH:mm:ss") == DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
            {
                StartAlarm = true;
                this.Click += Event_AlarmJitter_OFF;

            }
            if (StartAlarm)
            {
                MyHandle_AlarmJitter_ON();
                label2.Text = SetAlarm.ToString("yyyy/MM/dd HH:mm:ss");
            }
                  
        }

        /// <summary>
        /// 任务：1-记录闹钟时间 2-记录最初的坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            SetAlarm = dateTimePicker1.Value;
            timer1.Interval = 100;  //每100ms刷新一次计时中断

            label2.Text = SetAlarm.ToString("yyyy/MM/dd HH:mm:ss");

            RecordX = this.Location.X;
            RecordY = this.Location.Y;          
        }


    }
}
