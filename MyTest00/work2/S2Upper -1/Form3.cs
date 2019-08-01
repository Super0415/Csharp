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

namespace S2Upper
{
    public partial class Form3 : Form
    {
        DataDeal datadeal2 = null;
        public Form3()
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
            lForm1.SetSignEnLimit           = comboBox1.SelectedIndex;       //极限使能信号
            lForm1.SetSignEnOrigin          = comboBox2.SelectedIndex;       //原点使能信号
            lForm1.SetSignReversalLimit     = comboBox3.SelectedIndex;       //反转极限信号
            lForm1.SetSignReversalOrigin    = comboBox4.SelectedIndex;       //反转原点信号
            lForm1.SetSignSendFirm();
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
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 lForm1 = (Form1)this.Owner;//把Form2的父窗口指针赋给lForm1
            comboBox1.SelectedIndex = lForm1.SetSignEnLimit;       //极限使能信号
            comboBox2.SelectedIndex = lForm1.SetSignEnOrigin;       //原点使能信号
            comboBox3.SelectedIndex = lForm1.SetSignReversalLimit;       //反转极限信号
            comboBox4.SelectedIndex = lForm1.SetSignReversalOrigin;       //反转原点信号

            datadeal2 = lForm1.datadeal;
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = datadeal2.GetLocation().ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
