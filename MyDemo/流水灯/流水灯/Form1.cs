using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace 流水灯
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 定时器1触发计数
        /// </summary>
        int timer1C = 0; 
        /// <summary>
        /// 线程触发计数
        /// </summary>
        int thC = 0;     
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 演示流水灯,定时器200ms触发一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1C == 0) label1.BackColor = Color.Lime;
            else label1.BackColor = Color.Red;

            if (timer1C == 1) label2.BackColor = Color.Lime;
            else label2.BackColor = Color.Red;

            if (timer1C == 2) label3.BackColor = Color.Lime;
            else label3.BackColor = Color.Red;

            if (timer1C == 3) label4.BackColor = Color.Lime;
            else label4.BackColor = Color.Red;

            if (timer1C == 4) label5.BackColor = Color.Lime;
            else label5.BackColor = Color.Red;

            if (timer1C++ > 4)  //定时器1触发计数
            { timer1C = 0; }
           
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread th = new Thread(Thread_LampRun);
            th.Start();  // 只要使用Start方法，线程才会运行  
        }

        void Thread_LampRun()
        {
            while(true)
            {
                if (thC == 0) label6.BackColor = Color.Lime;
                else label6.BackColor = Color.Red;

                if (thC == 1) label7.BackColor = Color.Lime;
                else label7.BackColor = Color.Red;

                if (thC == 2) label8.BackColor = Color.Lime;
                else label8.BackColor = Color.Red;

                if (thC == 3) label9.BackColor = Color.Lime;
                else label9.BackColor = Color.Red;

                if (thC == 4) label10.BackColor = Color.Lime;
                else label10.BackColor = Color.Red;

                Thread.Sleep(500);
                if (thC++ > 4)  //线程触发计数
                { thC = 0; }
            }

        }
    }
}
