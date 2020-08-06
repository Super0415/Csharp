using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoManage
{
    public partial class Form1 : Form
    {
        string fName = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "表格(*.)|*.xlsx;*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                fName = fileDialog.FileName;                
                this.Text = "信息管理系统 (" + fName + ")";
            }
        }
    }
}
