using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 右键菜单
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 清零ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void 绿色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Green;
        }

        private void 红色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Red;
        }

        private void 黄色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Yellow;
        }

        private void 黑色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Black;
        }
    }
}
