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
    }
}
