using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTest00
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();      
            Tool_Form2Combobox();       //配置下拉列表
        }
        
        /// <summary>
        /// 确定按键，保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 lForm1 = (Form1)this.Owner;//把Form2的父窗口指针赋给lForm1

            lForm1.SetSignEnLimit           = comboBox1.SelectedIndex.ToString();       //极限使能信号
            lForm1.SetSignEnOrigin          = comboBox2.SelectedIndex.ToString();       //原点使能信号
            lForm1.SetSignReversalLimit     = comboBox3.SelectedIndex.ToString();       //反转极限信号
            lForm1.SetSignReversalOrigin    = comboBox4.SelectedIndex.ToString();       //反转原点信号

            this.Close();
        }
        /// <summary>
        /// 取消按键，不保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 配置下拉列表
        /// </summary>
        public void Tool_Form2Combobox()   //数据接收事件，读到数据的长度赋值给count，如果是8位（节点内部编程规定好的），就申请一个byte类型的buff数组，s句柄来读数据
        {
            String[] item1 = { "0：不启用", "1：启用"};    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            String[] item2 = { "0：不启用", "1：启用"};
            String[] item3 = { "0：不反转", "1：反转"};
            String[] item4 = { "0：不反转", "1：反转"};
            for (int i = 0;i<2;i++)
            {
                comboBox1.Items.Add(item1[i]);  //配置选项
                comboBox1.SelectedIndex = i;    //配置索引序号

                comboBox2.Items.Add(item2[i]);
                comboBox2.SelectedIndex = i;

                comboBox3.Items.Add(item3[i]);
                comboBox3.SelectedIndex = i;

                comboBox4.Items.Add(item4[i]);
                comboBox4.SelectedIndex = i;

            }
            comboBox1.SelectedItem = comboBox1.Items[0];    //默认为列表第二个变量 
            comboBox2.SelectedItem = comboBox2.Items[0];    //默认为列表第二个变量 
            comboBox3.SelectedItem = comboBox3.Items[0];    //默认为列表第一个变量
            comboBox4.SelectedItem = comboBox4.Items[0];    //默认为列表第一个变量 
        }

        
    }
}
