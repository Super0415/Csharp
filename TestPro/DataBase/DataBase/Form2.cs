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
        public Form2(Form1 f)
        {
            InitializeComponent();
            mForm = f;
            test2 = ListData.GetInstance();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner;
            form1.Recode_Info("窗口2打开成功！");
            label2.TextChanged += Label2_Changed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1 form1 = (Form1)Owner;
            //form1.Recode_Info("窗口2 - close()成功！");       

            //ListData test2 = ListData.GetInstance();
            //test2.OnlySubformHide(this);

            ListData test2 = ListData.GetInstance();
            tbform1.Text = test2.Getline;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            form1.Recode_Info("窗口2 - 关闭成功！");

            ListData test2 = ListData.GetInstance();
            test2.OnlySubformHide(this);
        }
        public void Show_From_Form1()
        {
            Form1 form1 = (Form1)Owner;

            Show_Data(test2.Getline);
        }

        public void Show_Data(string info)
        {
            
            this.label2.Text = test2.Getline;
            //textBox1.Text = test2.Getline;
            //textBox1.AppendText(test2.Getline);
            //this.tbform1.Text = info;
            this.label2.Text = test2.Getline;
            

            Form1 form1 = (Form1)Owner;
            form1.Recode_Info("窗口2 - 接收到数据：" + info);
            this.label2.Refresh();
            this.tbform1.Refresh();
        }

        private void Label2_Changed(object sender, EventArgs e)
        {
            this.tbform1.Text += "测试";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.label2.Text = test2.Getline;
            this.tbform1.Text += "测试";
        }
    }
}
