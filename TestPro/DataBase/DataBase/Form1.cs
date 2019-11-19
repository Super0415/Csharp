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
    public partial class Form1 : Form
    {
        Form2 form2 = null;
        ListData test1 = null;
        public Form1()
        {
            InitializeComponent();
            Recode_Info("打开窗口1");

            test1 = ListData.GetInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (form2 == null) return;
            Recode_Info("向窗口2发送：" + textBox1.Text);
            form2.Show_From_Form1();

        }

        /// <summary>
        /// 显示系统状态
        /// </summary>
        /// <param name="info"></param>
        public void Recode_Info(string info)
        {
            tb_Info.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "\r" + info + "\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form2 = new Form2(this);
            form2.Owner = this;
            test1.OnlySubformShow(form2);
        }
    }
}
