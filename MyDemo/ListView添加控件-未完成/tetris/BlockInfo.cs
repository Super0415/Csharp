using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class BlockInfo
    {
        private BitArray _id;
        private Color _bColor;
        public BlockInfo(BitArray id, Color bColor)
        {
            _id = id;
            _bColor = bColor;
        } 
        public BitArray ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public Color BColor
        {
            get
            {
                return _bColor;
            }
            set
            {
                _bColor = value;
            }
        }
        public string GetIdStr()
        {
            StringBuilder sb = new StringBuilder(25);
            foreach (bool item in _id)
            {
                sb.Append(item ? "1" : "0");
            }
            return sb.ToString();
        }
        public string GetColorStr()
        {
            return Convert.ToString(_bColor.ToArgb());
        }


    }
}
