using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 添加动态标签
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTipShow(toolTip1, button1,          "我是button1");
            ToolTipShow(toolTip1, checkBox1,        "我是checkBox1");
            ToolTipShow(toolTip1, label1,           "我是label1");
            ToolTipShow(toolTip1, dateTimePicker1,  "我是dateTimePicker1");
            ToolTipShow(toolTip1, trackBar1,        "我是trackBar1");
            ToolTipShow(toolTip1, comboBox1,        "我是comboBox1");
            ToolTipShow(toolTip1, groupBox1,        "我是groupBox1");
            ToolTipShow(toolTip1, textBox1,         "我是textBox1");
            ToolTipShow(toolTip1, menuStrip1,       "我是menuStrip1"); 
            //ToolTipShow(toolTip9, toolStripComboBox1, "我是toolStripComboBox1");    //菜单下级菜单无法被选中
        }

        /// <summary>
        /// 标签控件
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="Con"></param>
        /// <param name="words"></param>
        private void ToolTipShow(ToolTip tip, Control Con, string words)
        {
            tip.SetToolTip(Con, words);
        }
    }
}
