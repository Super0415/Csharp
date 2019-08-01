using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yungku.Common.IOCard.DataDeal;

namespace YKUper
{
    public partial class FormS2 : Form
    {
        /// <summary>
        /// 数据实例
        /// </summary>
        DataDeal S2Data = new DataDeal();

        public FormS2()
        {
            InitializeComponent();
        }

        private void FormS2_Load(object sender, EventArgs e)
        {
            MainForm Form = (MainForm)this.Owner;//把Form2的父窗口指针赋给lForm1
            S2Data = Form.MainData;
            string ip = S2Data.NetIP;
            timer1.Enabled = true;
            TbNowPos.Text = S2Data.GetLocation().ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
