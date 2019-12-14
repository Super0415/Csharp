using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 委托传递数值
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// 定义委托
        /// </summary>
        public delegate void MyForm2Delegate(string data);

        /// <summary>
        /// 发送整型数据
        /// </summary>
        public event MyForm2Delegate MyEventForm2SentINTData;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner;//把Form2的父窗口指针赋给lForm1
            form1.MyEventForm1SentINTData += Form1_MyEventForm1SentINTData;
        }

        private void Form1_MyEventForm1SentINTData(string data)
        {
            //throw new NotImplementedException();
            textBox1.Text = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyEventForm2SentINTData(textBox2.Text);
        }
    }
}
