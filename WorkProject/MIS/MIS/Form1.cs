using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIS
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 用户协议
        /// </summary>
        UserAgree userAgree = null;
        /// <summary>
        /// 用户数据
        /// </summary>
        Dictionary<string, MyData.DetailInfo> data = null;



        public Form1()
        {
            InitializeComponent();
            userAgree = UserAgree.Instance;
            data = MyData.GetDictionary;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;

        }

        /// <summary>
        /// 表格中添加新读取的信息
        /// </summary>
        private void addNewReadItem(string card)
        {
            
            if (data.Keys.Contains(card))    //包含此数据
            {
                MyData.DetailInfo buff = data[card];
                if (buff.actionstate == MyData.ActionStr.读卡完成)
                {
                    int index = dataGridView1.Rows.Add();//获取新的一行
                    dataGridView1.Rows[index].Cells[0].Value = index + 1;
                    dataGridView1.Rows[index].Cells[1].Value = buff.userInfoR[MyData.Item.comType] + buff.actionstate;
                    dataGridView1.Rows[index].Cells[2].Value = buff.userInfoR[MyData.Item.name];
                    dataGridView1.Rows[index].Cells[3].Value = buff.userInfoR[MyData.Item.sex];
                    dataGridView1.Rows[index].Cells[4].Value = buff.userInfoR[MyData.Item.age];
                    dataGridView1.Rows[index].Cells[5].Value = buff.userInfoR[MyData.Item.branch];
                    dataGridView1.Rows[index].Cells[6].Value = buff.userInfoR[MyData.Item.position];
                    dataGridView1.Rows[index].Cells[7].Value = buff.userInfoR[MyData.Item.level];
                    dataGridView1.Rows[index].Cells[8].Value = buff.userInfoR[MyData.Item.jobNum];
                    dataGridView1.Rows[index].Cells[9].Value = buff.userInfoR[MyData.Item.date];
                    dataGridView1.Rows[index].Cells[10].Value = buff.userInfoR[MyData.Item.equipType];
                    dataGridView1.Rows[index].Cells[11].Value = buff.userInfoR[MyData.Item.equipNum];
                }
                else
                {
                    MessageBox.Show("读卡失败！");
                }
            }

        }

        /// <summary>
        /// 实时刷新状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerReflash_Tick(object sender, EventArgs e)
        {
            btnHard.BackColor = userAgree.GetStatePort() ? Color.Green : Color.Red;
            btnNFC.BackColor = userAgree.GetStateNFC() ? Color.Green : Color.Red;
        }

        /// <summary>
        /// NFC写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWrite_Click(object sender, EventArgs e)
        {
            string NowNFCCard = userAgree.GetNowNFCCard();
            WriteInfo iForm = new WriteInfo(NowNFCCard, MyData.ProductType.NFC.ToString());
            iForm.Owner = this;
            iForm.Show();
        }


        /// <summary>
        /// 表格中添加新写入的信息
        /// </summary>
        public void addNewWriteItem()
        {
            string NowNFCCard = userAgree.GetNowNFCCard();
            if (data.Keys.Contains(NowNFCCard))    //包含此数据
            {
                MyData.DetailInfo buff = data[NowNFCCard];
                int index = dataGridView1.Rows.Add();//获取新的一行
                dataGridView1.Rows[index].Cells[0].Value = index + 1;
                dataGridView1.Rows[index].Cells[1].Value = buff.userInfoW[MyData.Item.comType] + (buff.actionstate == MyData.ActionStr.读卡完成 ? MyData.ActionStr.写卡成功 : MyData.ActionStr.写卡失败);
                dataGridView1.Rows[index].Cells[2].Value = buff.userInfoW[MyData.Item.name];
                dataGridView1.Rows[index].Cells[3].Value = buff.userInfoW[MyData.Item.sex];
                dataGridView1.Rows[index].Cells[4].Value = buff.userInfoW[MyData.Item.age];
                dataGridView1.Rows[index].Cells[5].Value = buff.userInfoW[MyData.Item.branch];
                dataGridView1.Rows[index].Cells[6].Value = buff.userInfoW[MyData.Item.position];
                dataGridView1.Rows[index].Cells[7].Value = buff.userInfoW[MyData.Item.level];
                dataGridView1.Rows[index].Cells[8].Value = buff.userInfoW[MyData.Item.jobNum];
                dataGridView1.Rows[index].Cells[9].Value = buff.userInfoW[MyData.Item.date];
                dataGridView1.Rows[index].Cells[10].Value = buff.userInfoW[MyData.Item.equipType];
                dataGridView1.Rows[index].Cells[11].Value = buff.userInfoW[MyData.Item.equipNum];

            }

        }

        /// <summary>
        /// USB写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string NowNFCCard = userAgree.GetNowNFCCard();
            WriteInfo iForm = new WriteInfo(NowNFCCard, MyData.ProductType.USBkey.ToString());
            iForm.Owner = this;
            iForm.Show();
        }

        /// <summary>
        /// NFC手动读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            string NowNFCCard = userAgree.GetNowNFCCard();
            userAgree.SendReadNFCInfo(NowNFCCard);
            Thread.Sleep(1000);
            addNewReadItem(NowNFCCard);
        }

        private void 通讯计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCheck iForm = new FormCheck();
            iForm.Owner = this;
            iForm.Show();
        }
    }
}
