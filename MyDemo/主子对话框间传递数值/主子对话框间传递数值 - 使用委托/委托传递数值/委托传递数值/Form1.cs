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
    public partial class Form1 : Form
    {
        /// <summary>
        /// 定义委托
        /// </summary>
        public delegate void MyForm1Delegate(string data);

        /// <summary>
        /// 发送整型数据
        /// </summary>
        public event MyForm1Delegate MyEventForm1SentINTData;

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 新建窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Owner = this;
            form2.Show();
            form2.MyEventForm2SentINTData += Form2_MyEventForm2SentINTData; ;
        }
        /// <summary>
        /// 委托-接收子窗体数据
        /// </summary>
        /// <param name="data"></param>
        private void Form2_MyEventForm2SentINTData(string data)
        {
            textBox1.Text = data;
        }
        /// <summary>
        /// 发送数据给子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MyEventForm1SentINTData(textBox2.Text);
        }
    }
}
