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
    public partial class FormCheck : Form
    {
        public FormCheck()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] msg = GetByteArray(tbSend.Text);
            msg[20] = CheckDataOut(msg);

            string result = string.Empty;
            if (msg != null)
            {
                for (int i = 0; i < msg.Length; i++)
                {
                    result += msg[i].ToString("X2") + " ";
                }
            }

            tbRec.Text = result;


        }

        public static byte[] GetByteArray(string shex)
        {
            string[] ssArray = shex.Trim().Split(' ');
            List<byte> bytList = new List<byte>();
            foreach (var s in ssArray)
            {                //将十六进制的字符串转换成数值  
                bytList.Add(Convert.ToByte(s, 16));
            }    //返回字节数组          
            return bytList.ToArray();
        }

        private byte CheckDataOut(byte[] buf)
        {
            int Sum = 0;
            for (int i = 2; i < 20; i++)
            {
                Sum += buf[i];
            }
            return (byte)(0x100 - (byte)Sum);
        }
    }
}
