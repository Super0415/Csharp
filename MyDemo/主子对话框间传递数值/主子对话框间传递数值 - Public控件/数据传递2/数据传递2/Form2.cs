using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 数据传递2
{
    public partial class Form2 : Form
    {
        //实例主窗体
        Form1 pall = new Form1();

        public Form2(Form1 f1)
        {
            InitializeComponent();
            pall = f1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = pall.textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pall.textBox1.Text = textBox1.Text;
        }
    }
}
