using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIS
{
    public partial class WriteInfo : Form
    {
        /// <summary>
        /// 用户协议
        /// </summary>
        UserAgree userAgree = null;
        /// <summary>
        /// 用户数据
        /// </summary>
        MyData mydata = null;
        Dictionary<string, MyData.DetailInfo> data = null;

        /// <summary>
        /// 记录窗体类型
        /// </summary>
        string FormType = string.Empty;
        /// <summary>
        /// 记录卡号
        /// </summary>
        string NowCardId = string.Empty;

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


        public WriteInfo(string nowCard, string formType)
        {
            InitializeComponent();
            userAgree = UserAgree.Instance;
            mydata = MyData.Instance;
            data = MyData.GetDictionary;
            NowCardId = nowCard;
            FormType = formType;
            cbbSex.SelectedItem = "男";
        }

        private void WriteInfo_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";

            if (data.ContainsKey(NowCardId))
            {
                MyData.DetailInfo buff = data[NowCardId];

                if (buff.actionstate != MyData.ActionStr.读卡成功)
                {
                    DataTemplate();
                    return;
                }
                tbName.Text = buff.userInfoR[MyData.Item.name];
                cbbSex.SelectedItem = buff.userInfoR[MyData.Item.sex] == null ? "男" : buff.userInfoR[MyData.Item.sex];
                tbAge.Text = buff.userInfoR[MyData.Item.age];
                tbBranch.Text = buff.userInfoR[MyData.Item.branch];
                tbPosition.Text = buff.userInfoR[MyData.Item.position];
                tbLenel.Text = buff.userInfoR[MyData.Item.level];
                tbJobNum.Text = buff.userInfoR[MyData.Item.jobNum];
                tbEquipNum.Text = buff.userInfoR[MyData.Item.equipNum];
                dateTimePicker1.Text = buff.userInfoR[MyData.Item.date];

            }
            else
            {
                DataTemplate();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 lForm1 = (Form1)this.Owner;//把Form2的父窗口指针赋给lForm1


            if (MessageBox.Show("确定写入？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
            {
                MyData.DetailInfo buff = data[NowCardId];
                data[NowCardId].userInfoW[MyData.Item.comType] = FormType;
                data[NowCardId].userInfoW[MyData.Item.name] = tbName.Text;
                data[NowCardId].userInfoW[MyData.Item.name] = tbName.Text;
                data[NowCardId].userInfoW[MyData.Item.sex] = cbbSex.SelectedItem.ToString();
                data[NowCardId].userInfoW[MyData.Item.age] = tbAge.Text;
                data[NowCardId].userInfoW[MyData.Item.branch] = tbBranch.Text;
                data[NowCardId].userInfoW[MyData.Item.position] = tbPosition.Text;
                data[NowCardId].userInfoW[MyData.Item.level] = tbLenel.Text;
                data[NowCardId].userInfoW[MyData.Item.jobNum] = tbJobNum.Text;
                data[NowCardId].userInfoW[MyData.Item.equipNum] = tbEquipNum.Text;
                data[NowCardId].userInfoW[MyData.Item.date] = dateTimePicker1.Text;
                data[NowCardId].userInfoW[MyData.Item.equipType] = FormType;
                data[NowCardId].actionstate = MyData.ActionStr.写卡中;

                userAgree.SendWriteNFCInfo(NowCardId);

                userAgree.SendReadNFCInfo(NowCardId);

                lForm1.addNewWriteItem();


                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
