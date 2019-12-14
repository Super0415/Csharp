using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 屏保
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile(Application.StartupPath+ @"\5.jpg");

        }

        private void MyFun_Exit(object sender, EventArgs e)
        {
            this.Close();
        }
        int num;
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (num == 0)
            {
                this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\5.jpg");
                num++;
            }
            else
            {
                this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\4.jpg");
                num = 0;
            }
        }
    }
}
