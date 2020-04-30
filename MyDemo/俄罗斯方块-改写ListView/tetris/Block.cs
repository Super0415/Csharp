using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class Block
    {
        protected Point[] structArr;
        protected int _xPos;
        protected int _yPos;
        protected Color _blockColor;        
        protected Color _disapperColor;
        protected int _rectPix;

        public Block(Point[] sa,Color bColor,Color dColor,int pix)
        {
            _blockColor = bColor;
            _disapperColor = dColor;
            _rectPix = pix;
            structArr = sa;
        }
        public Point this[int index]
        {
            get
            {
                return structArr[index];
            }
        }
        public int Lengh
        {
            get
            {
                return structArr.Length;
            }
        }
        #region 成员变量属性
        public int XPos
        {
            get
            {
                return _xPos;
            }
            set
            {
                _xPos = value;
            }
        }
        public int YPos
        {
            get
            {
                return _yPos;
            }
            set
            {
                _yPos = value;
            }
        }
        public Color BlockColor
        {
            get
            {
                return _blockColor;
            }
            set
            {
                _blockColor = value;
            }
        }
        public Color DisapperColor
        {
            get
            {
                return _disapperColor;
            }
            set
            {
                _disapperColor = value;
            }
        }
        public int RectPix
        {
            get
            {
                return _rectPix;
            }
            set
            {
                _rectPix = value;
            }
        }
        #endregion

        public void DeasilRote()
        {
            int temp;
            for (int i = 0; i < structArr.Length; i++)
            {
                temp = structArr[i].X;
                structArr[i].X = structArr[i].Y;
                structArr[i].Y = -temp;
            }
        }
        public void ContraRote()
        {
            int temp;
            for (int i = 0; i < structArr.Length; i++)
            {
                temp = structArr[i].X;
                structArr[i].X = -structArr[i].Y;
                structArr[i].Y = temp;
            }
        }
        private Rectangle PointToRect(Point p)
        {
            return new Rectangle((_xPos + p.X) * _rectPix + 1,(_yPos - p.Y) * _rectPix + 1, _rectPix - 2, _rectPix - 2 );
        }
        public virtual void Paint(Graphics gp)
        {
            SolidBrush sb = new SolidBrush(_blockColor);
            foreach (Point item in structArr)
            {
                lock (gp)
                {
                    gp.FillRectangle(sb, PointToRect(item));
                }
            }
        }
        public void erase(Graphics gp)
        {
            SolidBrush sb = new SolidBrush(_disapperColor);
            foreach (Point item in structArr)
            {
                lock (gp)
                {
                    gp.FillRectangle(sb, PointToRect(item));
                }
            }
        }

    }
}





