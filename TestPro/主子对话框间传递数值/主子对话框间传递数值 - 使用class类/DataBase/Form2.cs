using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestDataBase;

namespace DataBase
{
    public partial class Form2 : Form
    {
        ListData test2 = null;
        Form mForm = new Form();
        public Form2()
        {
            InitializeComponent();
            test2 = ListData.GetInstance();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner;
            form1.Recode_Info("窗口2打开成功！");
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            form1.Recode_Info("窗口2 - 关闭成功！");

            ListData test2 = ListData.GetInstance();
            test2.OnlySubformClose(this);
        }
        public void Show_From_Form1()
        {
            tbform1.Text = test2.Getline1;
            Form1 form1 = (Form1)Owner;
            form1.Recode_Info("窗口2 - 接收到数据：" + test2.Getline1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            form1.Recode_Info("窗口2 - Hide()成功！");

            ListData test2 = ListData.GetInstance();
            test2.OnlySubformHide(this);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            test2.Getline2 = tbToForm1.Text;
            Form1 form1 = (Form1)Owner;
            form1.Recode_Info("窗口2向窗口1发送数据：" + test2.Getline2);
            form1.Show_Data();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            this.tbform1.Text += "测试";
        }
    }
}
