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
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner;
            form1.Recode_Info("窗口2打开成功！");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            form1.Recode_Info("窗口2 - close()成功！");
            //Close();       

            ListData test2 = ListData.GetInstance();
            test2.OnlySubformHide(this);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            form1.Recode_Info("窗口2 - 关闭成功！");

            ListData test2 = ListData.GetInstance();
            test2.OnlySubformHide(this);
        }


    }
}
