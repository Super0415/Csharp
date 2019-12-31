using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 浏览器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tbUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                //1: 获取文本框的地址
                string str = tbUrl.Text.Trim();
                //2: 将地址转换成Uri对象
                Uri webUri = new Uri(str);
                //3: 设置WebBrowser的Url属性
                wbMine.Url = webUri;
            }
        }
    }
}
