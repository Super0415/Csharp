using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 多语言切换_简单版中英切换
{
    public partial class Form1 : Form
    {
        protected int language = 0;
        languages lang = new languages();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            language++;
            if (language > 2) language = 0;

            this.Text = lang.FormTest[language];
            label1.Text = lang.Lb1Test[language];
            button1.Text = lang.Btn1Test[language];
        }
    }

    /// <summary>
    /// 新建语言类
    /// </summary>
    public class languages
    {
        public string[] FormTest = { "中文窗体", "English Form" , "한글" };
        public string[] Lb1Test = { "中文", "English", "한글" };
        public string[] Btn1Test = { "语言", "Language", "언어" };
    }
}
