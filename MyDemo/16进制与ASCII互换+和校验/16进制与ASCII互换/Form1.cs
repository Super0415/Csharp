using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16进制与ASCII互换
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ASCII To Hex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            tbbuffDec = null;
            tbbuffHex = null;
            string STR_ASCII = tbASCII.Text.Trim();
            string[] SingleASCII = STR_ASCII.Split(' ');
            string buffDec = null;
            string buffHex = null;
            for (int i = 0; i < SingleASCII.Length; i++)
            {
                char[] ch = SingleASCII[i].ToCharArray();
                for (int j = 0; j < ch.Length; j++)
                {
                    int num = Convert.ToInt32(ch[j]);
                    buffDec += num.ToString()+" ";
                    buffHex += Convert.ToString(num, 16)+" ";
                }
            }
            tbDec.Text = buffDec;
            tbHex.Text = buffHex;
        }

        /// <summary>
        /// Hex To ASCII 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            tbbuffDec = null;
            tbbuffHex = null;
            string STR_Dec = tbDec.Text.Trim();
            string[] SingleDec = STR_Dec.Split(' ');
            string buffASCII = null;
            for (int i = 0; i < SingleDec.Length; i++)
            {
                char ch =  Convert.ToChar(Convert.ToInt32(SingleDec[i]));

                buffASCII += ch.ToString();
                //if (i != (SingleDec.Length - 1)) buffASCII += " ";
            }

            tbASCII.Text = buffASCII;
        }

        string tbbuffDec = null;
        private void tbHexToDec__TextChanged(object sender, EventArgs e)
        {
            if(tbHex.Text!=null)
            {
                string STR_Hex = tbHex.Text.Trim();
                string[] SingleHex = STR_Hex.Split(' ');
                
                for (int i = 0; i < SingleHex.Length; i++)
                {
                    int num_Dec = Convert.ToInt32(SingleHex[i], 16);

                    tbbuffDec += num_Dec.ToString();
                    if (i != (SingleHex.Length - 1)) tbbuffDec += " ";
                }
                tbDec.Text = tbbuffDec;
            }
        }

        string tbbuffHex = null;
        private void tbDecToHex__TextChanged(object sender, EventArgs e)
        {
            if (tbDec.Text != null)
            {
                string STR_Dec = tbDec.Text.Trim();
                string[] SingleDec = STR_Dec.Split(' ');
                
                for (int i = 0; i < SingleDec.Length; i++)
                {
                    tbbuffHex += Convert.ToString(Convert.ToInt32(SingleDec[i]), 16);
                    if (i != (SingleDec.Length - 1)) tbbuffHex += " ";
                }
                tbHex.Text = tbbuffHex;
            }
        }

        private void btnPLCSum_Click(object sender, EventArgs e)
        {
            char[] ASCII = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F',};
            string PLC_Info = tbPLC.Text.Trim();
            string[] SinglePLC = PLC_Info.Split(' ');
            int SumI = 0;
            for (int i = 1; i < SinglePLC.Length; i++)
            {
                int num_Dec = Convert.ToInt32(SinglePLC[i], 16);
                SumI += num_Dec;
            }
            int First = SumI % 16;
            int Second = (SumI-First) / 16 % 16;

            tbPLCSum.Text = Convert.ToString(Convert.ToInt32(ASCII[Second]), 16) + " " + Convert.ToString(Convert.ToInt32(ASCII[First]), 16);
        }

        private void btnXOR_Click(object sender, EventArgs e)
        {
            byte[] info = new byte[7];
            string PLC_Info = tbPLC.Text.Trim();
            string[] SinglePLC = PLC_Info.Split(' ');
            for (int i = 0; i < SinglePLC.Length; i++)
            {
                info[i] = Convert.ToByte(SinglePLC[i],16);
            }
            info[6] = Get_Crc(info,6);
            tbPLCSum.Text = Convert.ToString(info[6], 16).ToUpper();
        }

        /// <summary>
        /// 异或和校验码
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        byte Get_Crc(byte[] buf, byte len)
        {
            byte crc = 0;
            byte i = 0;
            for (i = 0; i < len; i++)
            {
                crc ^= buf[i];
            }
            return crc;
        }
    }
}
