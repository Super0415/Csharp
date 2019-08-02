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
    public partial class FormS2 : Form
    {
        /// <summary>
        /// 用于复制数据集
        /// </summary>
        DataDeal S2data = null;
        public FormS2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MainForm lForm1 = (MainForm)this.Owner;//把Form2的父窗口指针赋给lForm1
            S2data = lForm1.data;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = S2data.GetLocation().ToString();
        }
    }
}
