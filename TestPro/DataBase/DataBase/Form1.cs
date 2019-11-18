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

        ListData test1 = null;
        public Form1()
        {
            InitializeComponent();
            Recode_Info("打开窗口1");

            test1 = ListData.GetInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Owner = this;
            test1.OnlySubformShow(form2);
            Recode_Info("发送按钮尝试打开窗口2");

        }

        /// <summary>
        /// 显示系统状态
        /// </summary>
        /// <param name="info"></param>
        public void Recode_Info(string info)
        {
            tb_Info.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff") + "\r" + info + "\r\n");
        }
    }
}
