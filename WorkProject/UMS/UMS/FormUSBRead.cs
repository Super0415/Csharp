using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormUSBRead : Form
    {
        /// <summary>
        /// 定义委托
        /// </summary>
        public delegate void MyDelegate();

        /// <summary>
        /// 定义委托事件-读取USB中最新信息
        /// </summary>
        public event MyDelegate MyEventReadUSBNewTip;

        /// <summary>
        /// 定义委托事件-读取USB中所以信息
        /// </summary>
        public event MyDelegate MyEventReadUSBAllTip;

        private int tipSum = 0;
        public FormUSBRead()
        {
            InitializeComponent();
        }
        public void ReflashTipSum( string sum)
        {
            try
            {
                tipSum = Convert.ToInt32(sum);
            }
            catch
            {
                tipSum = -1;
            }
            lbUSBTipSum.Text = tipSum.ToString();
        }

        private void FormUSBRead_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 读取USB最新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (tipSum < 0)
            {
                MessageBox.Show("数据异常，请插拔USBkey！");
            }
            else if (tipSum == 0)
            {
                MessageBox.Show("USBkey中无数据，请先写入！");
            }
            else
            {
                if (MyEventReadUSBNewTip != null) MyEventReadUSBNewTip();//引发事件
            }
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tipSum < 0)
            {
                MessageBox.Show("数据异常，请插拔USBkey！");
            }
            else if (tipSum == 0)
            {
                MessageBox.Show("USBkey中无数据，请先写入！");
            }
            else
            {
                if (MyEventReadUSBAllTip != null) MyEventReadUSBAllTip();//引发事件
            }
            this.Hide();
        }
    }
}
