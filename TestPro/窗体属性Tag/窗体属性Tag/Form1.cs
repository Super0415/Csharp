using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 窗体属性Tag
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxn_ShowData(object sender, EventArgs e)
        {
            string myTag = Convert.ToString((sender as Control).Tag);
            string myText = (sender as Control).Text;
            textBox16.AppendText("文本控件TestBox["+ myTag + "]显示：" + myText + "\r\n");
        }
    }

}
