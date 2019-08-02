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

namespace YKUper
{
    public partial class FormS1 : Form
    {
        /// <summary>
        /// 用于复制数据集
        /// </summary>
        DataDeal S1data = null;
        public FormS1()
        {
            InitializeComponent();
        }

        

        private void FormS1_Load(object sender, EventArgs e)
        {
            MainForm lForm1 = (MainForm)this.Owner;//把Form2的父窗口指针赋给lForm1
            S1data = lForm1.data;
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int axis = S1data.Axis;
            label2.Text = DateTime.Now.ToString();
            label3.Text = S1data.GetLocation(axis).ToString();
        }

        private void FormS1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
