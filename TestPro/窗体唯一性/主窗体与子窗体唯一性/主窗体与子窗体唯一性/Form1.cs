using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 主窗体与子窗体唯一性
{
    public partial class Form1 : Form
    {
        //创建全局变量，用来存放已show出的窗体对象
        List<Form> AllForms = new List<Form>();

        public Form1()
        {
            InitializeComponent();         
            this.StartPosition = FormStartPosition.CenterScreen;                        //居中显示
        }

        private void buttonA_Click(object sender, EventArgs e)
        {
            FormA IForm = new FormA();
            IForm.Owner = this;
            IForm.Show();

        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            FormB IForm = new FormB();
            IForm.Owner = this;
            IForm.StartPosition = FormStartPosition.CenterScreen;                       //居中显示
            OnlySubform(IForm);
        }

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

    }
}
