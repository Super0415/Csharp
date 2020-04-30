using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    class BlockGroup
    {
        private InfoArr info;
        private Color disapperColor;
        private int rectPix;

        public BlockGroup()
        {
            BlockConfig config = new BlockConfig(); ;
            config.LoadFromXmlFile();
            info = new InfoArr();
            info = config.BlockInfo;
            disapperColor = config.BlockColor;
            rectPix = config.BlockCol;
        }
        public Block GetABlock()
        {
            Random rd = new Random();
            int keyOrder = rd.Next(0, info.Length);
            BitArray ba = info[keyOrder].ID;
            int struNum = 0;
            foreach (bool item in ba)
            {
                if (item)
                {
                    struNum++;
                }
            }
            Point[] structArr = new Point[struNum];
            int k = 0;
            for (int i = 0; i < ba.Length; i++)
            {
                if (ba[i])
                {
                    structArr[k].X = i / 5 - 2;
                    structArr[k].Y = 2 - i % 5;
                    k++;
                }
            }
            return new Block(structArr,info[keyOrder].BColor,disapperColor,rectPix);
        }

    }
}






