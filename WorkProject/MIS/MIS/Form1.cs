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

            for (int i = 0; i < 6; i++)
            {
                try
                {
                    int index = dataGridView1.Rows.Add();//获取新的一行

                    this.dataGridView1.Rows[index].Cells[0].Value = "第一列";
                    this.dataGridView1.Rows[index].Cells[1].Value = "输出形式为字符串";
                    this.dataGridView1.Rows[index].Cells[2].Value = "第三列";
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            dataGridView1.Rows[0].ReadOnly= true;
            dataGridView1.Rows[1].ReadOnly = true;
            dataGridView1.Rows[2].ReadOnly = true;

        }

        private void timerReflash_Tick(object sender, EventArgs e)
        {
            btnHard.BackColor = userAgree.GetStatePort() ? Color.Green : Color.Red;
            btnNFC.BackColor = userAgree.GetStateNFC() ? Color.Green : Color.Red;

            int Count = data.Count;

        }
    }
}
