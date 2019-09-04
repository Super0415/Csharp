using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOOL_combobox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitTool_comboboxItemWeek(comboBox1);
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// API-调用作为下拉列表选项，显示周星期
        /// </summary>
        /// <param name="box"></param>
        private void InitTool_comboboxItemWeek(ComboBox box)
        {
            string[] Item = {"星期天","星期一","星期二", "星期三", "星期四", "星期五", "星期六" };
            for (int i = 0; i < Item.Length; i++)
            {
                box.Items.Add(Item[i]);     //添加选项
                box.SelectedIndex = i;      //添加索引序号
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox1.SelectedItem.ToString();
        }
    }
}
