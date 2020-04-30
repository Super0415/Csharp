using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 十五子游戏
{
    public partial class Form1 : Form
    {
        int X = 4;
        int Y = 4;

        /// <summary>
        /// 十五子点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// 按钮自定义-十五子布局
        /// </summary>
        private void DefineButton()
        {
            int Top = 26, Left = 26,r = 60;
            int height = 50, width = 50;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button btn = new Button();
                    int temp = 4 * i + j +1;
                    btn.Top = i * r + Top;
                    btn.Left = j * r + Left;
                    btn.Height = height;
                    btn.Width = width;
                    btn.Tag = temp;
                    btn.Text = temp.ToString();
                    btn.Enabled = true;
                    btn.Visible = true;
                    btn.Click += new EventHandler(ButtonClick); 
                    this.Controls.Add(btn);
                }
            }
        }

 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //界面布局 - 15+1子
            DefineButton();

        }
    }
}
