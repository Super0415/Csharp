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
    public partial class FormPol : Form
    {
        
        public FormPol()
        {
            InitializeComponent();
            Tool_FormPolCombobox();                 //配置下拉列表
        }

        private void FormPol_Load(object sender, EventArgs e)
        {
            FormGaugeP lForm1 = (FormGaugeP)this.Owner;//把Form2的父窗口指针赋给lForm1
            int Pol = lForm1.GetPolarity();
            cbPolY1.SelectedIndex = (Pol & 1 << 0) == 0 ? 0 : 1;       //输出口1极性
            cbPolY2.SelectedIndex = (Pol & 1 << 1) == 0 ? 0 : 1;       //输出口2极性
            cbPolY3.SelectedIndex = (Pol & 1 << 2) == 0 ? 0 : 1;       //输出口3极性
            cbPolY4.SelectedIndex = (Pol & 1 << 3) == 0 ? 0 : 1;       //输出口4极性
            cbPolLed1.SelectedIndex = (Pol & 1 << 4) == 0 ? 0 : 1;       //LED1极性
            cbPolLed2.SelectedIndex = (Pol & 1 << 5) == 0 ? 0 : 1;       //LED2极性
            cbPolLed3.SelectedIndex = (Pol & 1 << 6) == 0 ? 0 : 1;       //LED3极性
            cbPolLed4.SelectedIndex = (Pol & 1 << 7) == 0 ? 0 : 1;       //LED4极性
        }

        /// <summary>
        /// 配置下拉列表
        /// </summary>
        public void Tool_FormPolCombobox()   //数据接收事件，读到数据的长度赋值给count，如果是8位（节点内部编程规定好的），就申请一个byte类型的buff数组，s句柄来读数据
        {
            String[] item = { "0：低电平", "1：高电平" };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中
            for (int i = 0; i < 2; i++)
            {
                cbPolY1.Items.Add(item[i]);     //配置选项
                cbPolY1.SelectedIndex = i;      //配置索引序号

                cbPolY2.Items.Add(item[i]);
                cbPolY2.SelectedIndex = i;

                cbPolY3.Items.Add(item[i]);
                cbPolY3.SelectedIndex = i;

                cbPolY4.Items.Add(item[i]);
                cbPolY4.SelectedIndex = i;

                cbPolLed1.Items.Add(item[i]);
                cbPolLed1.SelectedIndex = i;

                cbPolLed2.Items.Add(item[i]);
                cbPolLed2.SelectedIndex = i;

                cbPolLed3.Items.Add(item[i]);
                cbPolLed3.SelectedIndex = i;

                cbPolLed4.Items.Add(item[i]);
                cbPolLed4.SelectedIndex = i;

            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btYes_Click(object sender, EventArgs e)
        {
            FormGaugeP lForm1 = (FormGaugeP)this.Owner;//把Form2的父窗口指针赋给lForm1
            int Pol = cbPolY1.SelectedIndex << 0 | cbPolY2.SelectedIndex << 1 | cbPolY3.SelectedIndex << 2 | cbPolY4.SelectedIndex << 3 |
                    cbPolLed1.SelectedIndex << 4 | cbPolLed2.SelectedIndex << 5 | cbPolLed3.SelectedIndex << 6 | cbPolLed4.SelectedIndex << 7 ;
            lForm1.SetPolarity(Pol);

            this.Close();
        }
    }
}
