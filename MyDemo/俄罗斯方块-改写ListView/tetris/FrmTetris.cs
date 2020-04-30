using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tetris
{
    public partial class FrmTetris : Form
    {
        public FrmTetris()
        {
            InitializeComponent();
        }
        private Palette p;
        private Keys _upKey;            //上
        private Keys _downKey;          //下
        private Keys _leftKey;          //左
        private Keys _rightKey;         //右
        private Keys _deasilKey;        //顺时针
        private Keys _contraKey;        //逆时针
        private int _blockNumX;         //水平格子数
        private int _blockNumY;         //垂直格子数
        private int _blockColnum;          //格子像素
        private Color _blockColor;          //背景颜色

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (p != null)
                p.Close();
        
            p = new Palette(_blockNumX, _blockNumY, _blockColnum, _blockColor, Graphics.FromHwnd(pbRun.Handle), Graphics.FromHwnd(lbReady.Handle));
            //p.MyEventSender += new Palette.myDelegate(MyEventScort);
            p.Start();
        }

        //private void MyEventScort(int n)
        //{
        //    this.Invoke(new System.Action(()=> {
        //        //label1.Text = n.ToString();
        //    }));
        //}

        private void btnDown_Click(object sender, EventArgs e)
        {
            p.Down();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            p.Left();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            p.Right();
        }

        private void btnCondtra_Click(object sender, EventArgs e)
        {
            p.Contra();
        }

        private void btndeasil_Click(object sender, EventArgs e)
        {
            p.Deasil();
        }

        private void pbRun_Paint(object sender, PaintEventArgs e)
        {
            if (p != null)
            {
                p.PaintPaletter(e.Graphics);
            }
        }

        private void lbReady_Paint(object sender, PaintEventArgs e)
        {
            if (p != null)
            {
                p.PaintReady(e.Graphics);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            p.CheckAndOverBlock();
        }

        private void FrmTetris_Load(object sender, EventArgs e)
        {
            BlockConfig config = new BlockConfig();
            config.LoadFromXmlFile();
            _upKey = config.UpKey;
            _downKey = config.DownKey;
            _leftKey = config.LeftKey;
            _rightKey = config.RightKey;
            _deasilKey = config.DeasilKey;
            _contraKey = config.ContraKey;
            _blockNumX = config.BlockNumX;
            _blockNumY = config.BlockNumY;
            _blockColnum = config.BlockCol;
            _blockColor = config.BlockColor;

            this.Width = _blockNumX * _blockColnum + 165;
            this.Height = _blockNumY * _blockColnum + 55;
            pbRun.Width = _blockNumX * _blockColnum;
            pbRun.Height = _blockNumY * _blockColnum;

        }

        private void FrmTetris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 32)
            {
                e.Handled = true;
            }
            if (e.KeyCode == _downKey)
            {
                p.Down();
            }
            else if (e.KeyCode == _upKey)
            {
                p.Drop();
            }
            else if (e.KeyCode == _leftKey)
            {
                p.Left();
            }
            else if (e.KeyCode == _rightKey)
            {
                p.Right();
            }
            else if (e.KeyCode == _deasilKey)
            {
                p.Deasil();
            }
            else if (e.KeyCode == _contraKey)
            {
                p.Contra();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (p == null)
                return;
            if (btnPause.Text == "暂停")
            {
                p.Pause();
                btnPause.Text = "继续";
            }
            else
            {
                p.EndPause();
                btnPause.Text = "暂停";
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "暂停")
            {
                btnPause.PerformClick();
            }
            using (FormConfig formConfig = new FormConfig())
            {
                formConfig.ShowDialog();
            }
        }

        private void FrmTetris_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (p != null)
                p.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (p != null)
                lbScore.Text = "总分：" + p.NowScore.ToString();
        }
    }
}
