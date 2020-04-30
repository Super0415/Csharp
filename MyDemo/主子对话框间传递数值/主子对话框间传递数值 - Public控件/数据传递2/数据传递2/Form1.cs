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
    public partial class Form1 : Form
    {
        Form2 Sub = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Sub == null) Sub = new Form2(this);
            Sub.Show();                       //显示子对话框内容
            Sub.textBox1.Text = textBox1.Text;
        }
    }
}
