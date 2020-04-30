using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 代码创建多按钮灯
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button btn = new Button();
                    btn.Top = 35 * i;
                    btn.Left = 35 * j;
                    btn.Width = 30;
                    btn.Height = 30;
                    btn.Text = (i * 5 + j).ToString();
                    btn.Visible = true;
                    btn.Enabled = true;
                    btn.BackColor = Color.Green;
                    this.Controls.Add(btn);
                }
            }
        }

        int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Controls.Count == 25)  //统计添加的控件数，满25个，即可刷新
            {
                for (int i = 0; i < 25; i++)
                {
                    if (i == count)
                        this.Controls[i].BackColor = Color.Red;
                    else this.Controls[i].BackColor = Color.Green;
                }
                if (count < 24) count++;
                else count = 0;
            }
        }
    }
}
