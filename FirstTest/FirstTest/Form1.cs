﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace FirstTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.label1.BackColor == Color.Yellow) this.label1.BackColor = Color.Green;
            else this.label1.BackColor = Color.Yellow;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.label2.BackColor == Color.Yellow) this.label2.BackColor = Color.Green;
            else this.label2.BackColor = Color.Yellow;
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (this.label3.BackColor == Color.Yellow) this.label3.BackColor = Color.Green;
            else this.label3.BackColor = Color.Yellow;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (this.label4.BackColor == Color.Yellow) this.label4.BackColor = Color.Green;
            else this.label4.BackColor = Color.Yellow;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IPAddress serverIp = IPAddress.Parse("192.168.1.100"); //服务端IP 将IP字符串转化为IPAddress实例
            TcpClient client = new TcpClient(); //实例化客户端
            client.Connect(serverIp, 4000); //端口8888需与服务端开启的端口一致，否则无法与服务端建立链接
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


    }
}
