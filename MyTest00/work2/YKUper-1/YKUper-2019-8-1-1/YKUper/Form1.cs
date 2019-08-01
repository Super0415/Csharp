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
    public partial class MainForm : Form
    {
        public DataDeal data = new DataDeal();
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel.Controls.Clear();//移除所有控件
            FormS2 forms2 = new FormS2();
            forms2.Owner = this;
            forms2.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            forms2.TopLevel = false; //指示子窗体非顶级窗体
            this.panel.Controls.Add(forms2);//将子窗体载入panel
            forms2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            data.SetAxle(data.GetAxle()+1);
        }
    }
}
