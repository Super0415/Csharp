using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        FormUSBRead formUSBRead = null;
        FormWriteInfo writeInfo = null;

        /// <summary>
        /// 用户协议
        /// </summary>
        UserAgree myAgree = null;

        public Form1()
        {
            InitializeComponent();
            formUSBRead = new FormUSBRead();
            formUSBRead.MyEventReadUSBNewTip += FormUSBRead_MyEventReadUSBNewTip;
            formUSBRead.MyEventReadUSBAllTip += FormUSBRead_MyEventReadUSBAllTip;
            writeInfo = new FormWriteInfo();
            myAgree = UserAgree.Instance;         
        }

        /// <summary>
        /// 读取USBkey中存储的所有数据
        /// </summary>
        private void FormUSBRead_MyEventReadUSBAllTip()
        {
            MessageBox.Show("读取所有数据");
        }

        /// <summary>
        /// 读取USBkey中存储的最新一条数据
        /// </summary>
        private void FormUSBRead_MyEventReadUSBNewTip()
        {
            MyPublic.TipClass myTip = new MyPublic.TipClass();
            int addr = myAgree.GetUSBTipSum();
            myAgree.GetPointTip(addr,out myTip);
            MessageBox.Show("读取最新一条数据");
        }

        private void timerReflash_Tick(object sender, EventArgs e)
        {
            btnUSB.BackColor = myAgree.USBConnectState ? Color.Green : Color.Red;
            btnHard.BackColor = myAgree.HardConnectState ? Color.Green : Color.Red;
            btnNFC.BackColor = myAgree.NFCConnectState ? Color.Green : Color.Red;
        }

        /// <summary>
        /// 连接USB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUSB_Click(object sender, EventArgs e)
        {
            myAgree.HandOpenUSBkey();
        }

        private void btnUSBRead_Click(object sender, EventArgs e)
        {
            formUSBRead.ReflashTipSum(myAgree.GetUSBTipSum().ToString());
            formUSBRead.Show();
        }

        private void btnUSBWrite_Click(object sender, EventArgs e)
        {
            writeInfo.formType = "USB";
            writeInfo.Show();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            writeInfo.formType = "NFC";
            writeInfo.Show();
        }

        private void btnUSBClear_Click(object sender, EventArgs e)
        {
            if (myAgree.ClearUSBTipSum())
            {
                MessageBox.Show("USB中记录已清零！");
            }
            else
            {
                MessageBox.Show("请重新操作，或插拔USBkey！");
            }
        }
    }
}
