using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormWriteInfo : Form
    {
        /// <summary>
        /// 用户协议
        /// </summary>
        UserAgree myAgree = null;

        /// <summary>
        /// 记录窗体类型
        /// </summary>
        public string formType { get; set; } = string.Empty;
        /// <summary>
        /// 记录卡号
        /// </summary>
        string NowCardId = string.Empty;

        public FormWriteInfo()
        {
            InitializeComponent();
            myAgree = UserAgree.Instance;
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            DataTemplate();
        }

        /// <summary>
        /// 填充数据模板
        /// </summary>
        private void DataTemplate()
        {
            tbName.Text = "1";
            cbbSex.SelectedItem = "男";
            tbAge.Text = "2";
            tbBranch.Text = "3";
            tbPosition.Text = "4";
            tbLenel.Text = "5";
            tbJobNum.Text = "6";
            tbEquipNum.Text = "7";
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            MyPublic.TipClass myTip = new MyPublic.TipClass();
            myTip.Name = tbName.Text;
            myTip.Sex = cbbSex.SelectedItem.ToString();
            myTip.Age = tbAge.Text;
            myTip.Branch = tbBranch.Text;
            myTip.Position = tbPosition.Text;
            myTip.Level = tbLenel.Text;
            myTip.JopNum = tbJobNum.Text;
            myTip.EquipNum = tbEquipNum.Text;
            myTip.DataT = dateTimePicker1.Text;
            myAgree.SaveUSBTip(myTip);
            this.Hide();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void WriteInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void WriteInfo_VisibleChanged(object sender, EventArgs e)
        {
            this.Text = "信息添加 - "+formType;
        }


    }
}
