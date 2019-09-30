using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void PLCCheck_Click(object sender, EventArgs e)
        {
            PLCSumCheck IForm = new PLCSumCheck();
            IForm.Owner = this;
            //IForm.Show();
            IForm.StartPosition = FormStartPosition.CenterScreen;
            OnlySubform(IForm);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }




        //创建全局变量，用来存放已show出的窗体对象
        List<Form> AllForms = new List<Form>();
        /// <summary>
        /// 显示唯一的窗体
        /// </summary>
        /// <param name="Form"></param>
        private void OnlySubform(Form Form)
        {
            //判断窗体是否已经弹出，默认false
            bool hasform = false;
            //遍历所有窗体对象
            foreach (Form f in AllForms)
            {
                //判断弹出的窗体是否重复
                if (f.Name == Form.Name)
                {
                    //重复，修改为true
                    hasform = true;
                    f.WindowState = FormWindowState.Normal;
                    //获取焦点
                    f.Focus();
                }
            }
            if (hasform)
            {
                Form.Hide();
            }
            else
            {
                //添加到所有窗体中
                AllForms.Add(Form);
                //并打开该窗体
                Form.Show();
            }
        }
        /// <summary>
        /// 关闭时清除窗体信息
        /// </summary>
        /// <param name="Form"></param>
        private void CleanTheform(Form Form)
        {
            //遍历所有窗体对象
            foreach (Form f in AllForms)
            {
                //判断弹出的窗体是否重复
                if (f.Name == Form.Name)
                {
                    AllForms.Remove(Form);
                }
            }

        }

    }
}
