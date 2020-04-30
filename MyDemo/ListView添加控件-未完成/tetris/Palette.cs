using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
namespace tetris
{
    class Palette
    {
        private int _width = 15;
        private int _height = 25;
        private Color[,] coorArr;
        private Color disapperColor;
        private Graphics gpPalette;
        private Graphics gpReady;
        private BlockGroup bGroup;
        private Block runBlock;
        private Block readyBlock;
        private int rectPix;
        private int _nowScore = 0;
        private System.Timers.Timer timerBlock;
        private int timeSpan = 800;

        public delegate void myDelegate(int n);
        public event myDelegate MyEventSender;

        public int NowScore
        {
            get { return _nowScore; }
        }
        public Palette(int x, int y, int pix, Color dColor, Graphics gp, Graphics gr)
        {
            _width = x;
            _height = y;
            coorArr = new Color[_width,_height];
            disapperColor = dColor;
            gpPalette = gp;
            gpReady = gr;
            rectPix = pix;
        }
         ~Palette()
        {
            if (gpPalette != null) gpPalette.Dispose();
            if (gpReady != null) gpReady.Dispose();
        }
        public void Start()
        {
            bGroup = new BlockGroup();
            runBlock = bGroup.GetABlock();
            runBlock.XPos = _width / 2;
            int y = 0;
            for (int i = 0; i < runBlock.Lengh; i++)
            {
                if (runBlock[i].Y > y)
                {
                    y = runBlock[i].Y;
                }
            }
            runBlock.YPos = y;
            gpPalette.Clear(disapperColor);
            runBlock.Paint(gpPalette);
            Thread.Sleep(10);
            readyBlock = bGroup.GetABlock();
            readyBlock.XPos = 2;
            readyBlock.YPos = 2;
            gpReady.Clear(disapperColor);
            readyBlock.Paint(gpReady);

            if(timerBlock == null)
            {
                timerBlock = new System.Timers.Timer(timeSpan);
                timerBlock.Elapsed += new System.Timers.ElapsedEventHandler(OntimeEvent);
                timerBlock.AutoReset = true;
                timerBlock.Start();
            }

        }

        private void OntimeEvent(object sender, ElapsedEventArgs e)
        {
            CheckAndOverBlock();
            Down();

            if (MyEventSender != null) MyEventSender(++_nowScore);
        }

        public bool Down()
        {
            int xPos = runBlock.XPos;
            int yPos = runBlock.YPos + 1;
            for (int i = 0; i < runBlock.Lengh; i++)
            {
                if (yPos - runBlock[i].Y > _height - 1)
                    return false;
                if (!coorArr[xPos + runBlock[i].X, yPos - runBlock[i].Y].IsEmpty)
                    return false;
            }

            runBlock.erase(gpPalette);
            runBlock.YPos++;
            runBlock.Paint(gpPalette);
            return true;
        }
        public bool Left()
        {
            int xPos = runBlock.XPos-1;
            int yPos = runBlock.YPos;
            for (int i = 0; i < runBlock.Lengh; i++)
            {
                if (xPos + runBlock[i].X <0 || yPos - runBlock[i].Y < 0)
                    return false;
                if (!coorArr[xPos + runBlock[i].X, yPos - runBlock[i].Y].IsEmpty)
                    return false;
            }

            runBlock.erase(gpPalette);
            runBlock.XPos--;
            runBlock.Paint(gpPalette);
            return true;
        }
        public bool Right()
        {
            int xPos = runBlock.XPos + 1;
            int yPos = runBlock.YPos;
            for (int i = 0; i < runBlock.Lengh; i++)
            {
                if (xPos + runBlock[i].X > _width - 1 || yPos - runBlock[i].Y <0)
                    return false;
                if (!coorArr[xPos + runBlock[i].X, yPos - runBlock[i].Y].IsEmpty)
                    return false;
            }

            runBlock.erase(gpPalette);
            runBlock.XPos++;
            runBlock.Paint(gpPalette);
            return true;
        }
        public bool Deasil()
        {
            
            for (int i = 0; i < runBlock.Lengh; i++)
            {
                int xPos = runBlock.XPos+runBlock[i].Y;
                int yPos = runBlock.YPos+runBlock[i].X;
                if ((xPos > _width - 1) || (xPos < 0))
                    return false;
                if ((yPos > _height - 1) || (yPos < 0))
                    return false;
                if (!coorArr[xPos, yPos].IsEmpty)
                    return false;
            }

            runBlock.erase(gpPalette);
            runBlock.DeasilRote();
            runBlock.Paint(gpPalette);
            return true;
        }
        public bool Contra()
        {
            
            for (int i = 0; i < runBlock.Lengh; i++)
            {
                int xPos = runBlock.XPos - runBlock[i].Y; ;
                int yPos = runBlock.YPos - runBlock[i].X; ;
                if ((xPos  > _width - 1) || (xPos  < 0))
                    return false;
                if ((yPos  > _height - 1) || (yPos  < 0))
                    return false;
                if (!coorArr[xPos, yPos].IsEmpty)
                    return false;
            }

            runBlock.erase(gpPalette);
            runBlock.ContraRote();
            runBlock.Paint(gpPalette);
            return true;
        }

        private void PaintBackground(Graphics gp)
        {
            gp.Clear(disapperColor);
            
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (!coorArr[j, i].IsEmpty)
                    {
                        SolidBrush sb = new SolidBrush(coorArr[j, i]);
                        gp.FillRectangle(sb, j * rectPix + 1, i * rectPix + 1, rectPix - 2, rectPix - 2);
                    }
                    
                }

            }
        }
        public void PaintPaletter(Graphics gp)  //重画整个画板
        {
            PaintBackground(gp);
            if (runBlock != null)
            {
                runBlock.Paint(gp);
            }
        }
        public void PaintReady(Graphics gp)     //重画下一砖块画板
        {
            if (readyBlock != null)
            {
                readyBlock.Paint(gp);
            }
        }

        public void CheckAndOverBlock()
        {
            bool over = false;
            for (int i = 0; i < runBlock.Lengh; i++)
            {
                int x = runBlock.XPos + runBlock[i].X;
                int y = runBlock.YPos - runBlock[i].Y;
                if (y == _height - 1)   //到下边界
                {
                    over = true;
                    break;
                }
                if (!coorArr[x,y+1].IsEmpty)
                {
                    over = true;
                    break;
                }
            }
            if (over)
            {
                for (int i = 0; i < runBlock.Lengh; i++) //记录将当前块
                {
                    coorArr[runBlock.XPos + runBlock[i].X, runBlock.YPos - runBlock[i].Y] = runBlock.BlockColor;
                }

                CheckAndDelFullRow();

                runBlock = readyBlock;
                runBlock.XPos = _width / 2;
                int y = 0;
                for (int i = 0; i < runBlock.Lengh; i++)
                {
                    if (runBlock[i].Y > y)
                    {
                        y = runBlock[i].Y;
                    }
                }
                runBlock.YPos = y;

                //检查新产生的砖块所占用的地方是否已经有砖块
                for (int i = 0; i < runBlock.Lengh; i++)
                {
                    if (!coorArr[runBlock.XPos + runBlock[i].X, runBlock.YPos - runBlock[i].Y].IsEmpty)
                    {
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;
                        gpPalette.DrawString("GAME OVER",
                            new Font("Arial Black", 25f),
                            new SolidBrush(Color.White),
                            new RectangleF(0, (_height * rectPix / 2 - 100), (_width * rectPix), 100), drawFormat);
                        //gpPalette.DrawString("OVER",
                        //    new Font("Arial Black", 25f),
                        //    new SolidBrush(Color.White),
                        //    new RectangleF(0, (_height * rectPix / 3 - 100), (_width * rectPix / 2), 50), drawFormat);
                        if (timerBlock != null)
                            timerBlock.Stop();
                        return;
                    }
                }

                runBlock.Paint(gpPalette);
                readyBlock = bGroup.GetABlock();
                readyBlock.XPos = 2;
                readyBlock.YPos = 2;
                gpReady.Clear(Color.Silver);
                readyBlock.Paint(gpReady);

               

            }
        }
        public void CheckAndDelFullRow()
        {
            int lowRow = runBlock.YPos - runBlock[0].Y;
            int highRow = lowRow;
            for (int i = 1; i < runBlock.Lengh; i++)
            {
                int y = runBlock.YPos - runBlock[i].Y;
                if (y < lowRow)
                    lowRow = y;              
                if (y > highRow)
                    highRow = y;
            }

            bool repaint = false;
            for (int i = lowRow; i <= highRow; i++)
            {
                bool rowFull = true;
                for (int j = 0; j < _width; j++)    //判断是否满行
                {
                    if (coorArr[j, i].IsEmpty)      //每个格子中有一个不在记录中
                    {
                        rowFull = false;
                        break;
                    }
                }
                if (rowFull)
                {
                    repaint = true;


                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < _width; j++)
                        {
                            coorArr[j, k] = coorArr[j,k-1];
                        }
                    }
                    for (int n = 0; n < _width; n++)
                    {
                        coorArr[n, 0] = Color.Empty;
                    }
                }
            }
            if (repaint)
            {
                PaintBackground(gpPalette);
            }

        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            if (timerBlock.Enabled == true)
                timerBlock.Enabled = false;
        }
        /// <summary>
        /// 恢复
        /// </summary>
        public void EndPause()
        {
            if (timerBlock.Enabled == false)
                timerBlock.Enabled = true;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            timerBlock.Close();
            gpPalette.Dispose();
            gpReady.Dispose();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Drop()
        {
            timerBlock.Stop();
            while (Down()) ;
            timerBlock.Start();
        }

    }
}



