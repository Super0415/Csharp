using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tetris
{
    public partial class FormConfig : Form
    {
        public class ModeConfig
        {
            /// <summary>
            /// 砖块颜色
            /// </summary>
            public Color BlockColor;
            /// <summary>
            /// 横线数量
            /// </summary>
            public int LineNum_X;
            /// <summary>
            /// 宽度
            /// </summary>
            public int Width;
            /// <summary>
            /// 竖线数量
            /// </summary>
            public int LineNum_Y;
            /// <summary>
            /// 高度
            /// </summary>
            public int Height;

            /// <summary>
            /// 横间距
            /// </summary>
            /// <returns></returns>
            public int GetXDistance()
            {
                return Height / (LineNum_Y + 1);
            }
            /// <summary>
            /// 竖间距
            /// </summary>
            /// <returns></returns>
            public int GetYDistance()
            {
                return Width / (LineNum_X + 1);
            }

            public bool[,] States;
        }
        public ModeConfig mc = new ModeConfig();
        public ModeConfig mclb = new ModeConfig();

        private BlockConfig config = new BlockConfig();



        public FormConfig()
        {
            InitializeComponent();
        }

        ArrayList Test = new ArrayList();
        private void Config_Load(object sender, EventArgs e)
        {
            mc.LineNum_X = 4;                                  //横线数量
            mc.Width = lbMode.Width;   //宽度
            mc.LineNum_Y = 4;                                  //竖线数量
            mc.Height = lbMode.Height;   //高度
            mc.States = new bool[(mc.LineNum_X + 1), (mc.LineNum_Y + 1)];   //每个砖块状态
            mc.BlockColor = Color.Gray;

            config.LoadFromXmlFile();   //读取
            InfoArr info = config.BlockInfo;

            ListViewItem myItem = new ListViewItem();
            for (int i = 0; i < info.Length; i++)
            {
                myItem = lsvBlockSet.Items.Add(info[i].GetIdStr());
                myItem.SubItems.Add(info[i].GetColorStr());

            }

            tbToUp.Text = ((Keys)config.UpKey).ToString();
            tbToUp.Tag = config.UpKey;
            tbToDown.Text = ((Keys)config.DownKey).ToString();
            tbToDown.Tag = config.DownKey;
            tbToleft.Text = ((Keys)config.LeftKey).ToString();
            tbToleft.Tag = config.LeftKey;
            tbToRight.Text = ((Keys)config.RightKey).ToString();
            tbToRight.Tag = config.RightKey;
            tbDeasil.Text = ((Keys)config.DeasilKey).ToString();
            tbDeasil.Tag = config.DeasilKey;
            tbContra.Text = ((Keys)config.ContraKey).ToString();
            tbContra.Tag = config.ContraKey;

            tbBlockNumX.Text = config.BlockNumX.ToString();
            tbBlockNumY.Text = config.BlockNumY.ToString();
            tbBlockColNum.Text = config.BlockCol.ToString();
            lbBlockBlackColor.BackColor = config.BlockColor;

        }

        /// <summary>
        /// 砖块样式填充(165*165)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMode_Paint(object sender, PaintEventArgs e)
        {
            Graphics mp = e.Graphics;
            mp.Clear(Color.Silver);
            Pen p = new Pen(Color.White);
            for (int i = 0; i <= mc.LineNum_X; i++)         //画横线
            {
                mp.DrawLine(p, 0, (mc.GetXDistance() * i), 165, (mc.GetXDistance() * i));
            }
            for (int i = 0; i <= mc.LineNum_Y; i++)         //画竖线
            {
                mp.DrawLine(p, (mc.GetYDistance() * i), 0, (mc.GetYDistance() * i), 165);
            }
            lbMode_FillRectangle();
        }

        /// <summary>
        /// 鼠标点击事件 - 根据鼠标地址确定对应区间状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMode_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)  //确保是鼠标左键点击
            {
                return;
            }
            int mPos_X = 0, mPos_Y = 0;
            mPos_X = e.X / (mc.GetXDistance());
            mPos_Y = e.Y / (mc.GetYDistance());
            mc.States[mPos_X, mPos_Y] = !(mc.States[mPos_X, mPos_Y]);   //选中砖块状态取反
            lbMode_FillRectangle();
        }

        /// <summary>
        /// 砖块填充颜色
        /// </summary>
        private void lbMode_FillRectangle()
        {
            Graphics mp = lbMode.CreateGraphics();
            for (int i = 0; i <= mc.LineNum_X; i++)
            {
                for (int j = 0; j <= mc.LineNum_X; j++)
                {
                    bool b = mc.States[i, j];
                    int pX = mc.GetXDistance() * i + 1;
                    int pY = mc.GetYDistance() * j + 1;
                    int pPX = mc.GetXDistance() - 1;
                    int pPY = mc.GetYDistance() - 1;
                    Color DefCol = mc.BlockColor;

                    SolidBrush s = new SolidBrush(b ? DefCol : Color.Silver);
                    mp.FillRectangle(s, pX, pY, pPX, pPY);

                }
            }
            mp.Dispose();
        }

        private void lbColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            mc.BlockColor = colorDialog1.Color;
            lbColor.BackColor = colorDialog1.Color;
            lbMode_FillRectangle();
        }

        private void btnAddBlock_Click(object sender, EventArgs e)
        {
            bool isEmpty = false;
            foreach (bool item in mc.States)
            {
                if (item)
                {
                    isEmpty = true;
                    break;
                }
            }
            if (!isEmpty)   //图形为空
            {
                MessageBox.Show("图形为空，请重新绘制！", "提示窗体", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder sb = new StringBuilder(25);
            foreach (bool item in mc.States)
            {
                sb.Append(item ? "1" : "0");
            }

            string blockstr = sb.ToString();
            foreach (ListViewItem item in lsvBlockSet.Items)
            {
                if (item.SubItems[0].Text.ToString().Equals(blockstr))
                {
                    MessageBox.Show("图形重复，请重新绘制！", "提示窗体", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            int i = lsvBlockSet.Items.Count;
            ListViewItem info = new ListViewItem();
            info = lsvBlockSet.Items.Add(blockstr);
            info.SubItems.Add(Convert.ToString(mc.BlockColor.ToArgb()));
            info.Font = new Font(lsvBlockSet.Font.Name, 36);                //通过字体大小改变行高
        }

        private void lsvBlockSet_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                mc.BlockColor = Color.FromArgb(int.Parse(e.Item.SubItems[1].Text));
                lbColor.BackColor = mc.BlockColor;
                string str = e.Item.SubItems[0].Text;
                for (int i = 0; i < str.Length; i++)
                {
                    mc.States[i / (mc.LineNum_X + 1), i % (mc.LineNum_Y + 1)] = (str[i] == '1') ? true : false;
                }
                lbMode_FillRectangle();
            }
        }

        private void btnDelBlock_Click(object sender, EventArgs e)
        {
            if (lsvBlockSet.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中右框图形，再进行删除操作！", "提示窗体", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lsvBlockSet.Items.Remove(lsvBlockSet.SelectedItems[0]);
            btnClearBlock.PerformClick();                                   //触发清空按钮

        }

        private void btnClearBlock_Click(object sender, EventArgs e)
        {
            mc.States = new bool[(mc.LineNum_X + 1), (mc.LineNum_Y + 1)];   //每个砖块状态
            lbMode_FillRectangle();
        }

        private void btnUpdateBlock_Click(object sender, EventArgs e)
        {
            if (lsvBlockSet.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中右框图形，再进行删除操作！", "提示窗体", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool isEmpty = false;
            foreach (bool item in mc.States)
            {
                if (item)
                {
                    isEmpty = true;
                    break;
                }
            }
            if (!isEmpty)   //图形为空
            {
                MessageBox.Show("图形为空，请重新绘制！", "提示窗体", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder sb = new StringBuilder(25);
            foreach (bool item in mc.States)
            {
                sb.Append(item ? "1" : "0");
            }
            lsvBlockSet.SelectedItems[0].SubItems[0].Text = sb.ToString();
            lsvBlockSet.SelectedItems[0].SubItems[1].Text = mc.BlockColor.ToArgb().ToString();

        }

        private void tbContra_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue >= 33 && e.KeyValue <= 36) || (e.KeyValue >= 45 && e.KeyValue <= 46) || (e.KeyValue >= 48 && e.KeyValue <= 57)
                || (e.KeyValue >= 65 && e.KeyValue <= 90) || (e.KeyValue >= 96 && e.KeyValue <= 107) || (e.KeyValue >= 109 && e.KeyValue <= 111)
                || (e.KeyValue >= 186 && e.KeyValue <= 192) || (e.KeyValue >= 219 && e.KeyValue <= 222))
            {
                foreach (Control item in groupBox1.Controls)
                {
                    Control temp = item as TextBox;
                    if (temp != null && ((TextBox)temp).Text != "")
                    {
                        if (((int)((TextBox)temp).Tag) == e.KeyValue)
                        {
                            ((TextBox)temp).Text = "";
                            ((TextBox)temp).Tag = Keys.None;
                        }
                    }
                }
                ((TextBox)sender).Text = e.KeyCode.ToString();
                ((TextBox)sender).Tag = (Keys)e.KeyValue;
            }
        }

        private void lbBlockBlackColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            lbBlockBlackColor.BackColor = colorDialog1.Color;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InfoArr info = new InfoArr();
            foreach (ListViewItem item in lsvBlockSet.Items)
            {
                info.Add(item.SubItems[0].Text, item.SubItems[1].Text);
            }

            config.BlockInfo = info;
            config.UpKey = (Keys)tbToUp.Tag;
            config.DownKey = (Keys)tbToDown.Tag;
            config.LeftKey = (Keys)tbToleft.Tag;
            config.RightKey = (Keys)tbToRight.Tag;
            config.DeasilKey = (Keys)tbDeasil.Tag;
            config.ContraKey = (Keys)tbContra.Tag;

            config.BlockNumX = int.Parse(tbBlockNumX.Text);
            config.BlockNumY = int.Parse(tbBlockNumY.Text);
            config.BlockCol = int.Parse(tbBlockColNum.Text);
            config.BlockColor = lbBlockBlackColor.BackColor;
            config.SaveToXmlFile();
        }

        private void lsvBlockSet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listViewEx1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as TabControl).SelectedIndex == 1)
            {
                for (int i = 0; i < Test.Count; i++)
                {
                    //mclb_FillRectangle(i);

                    this.Invoke(new System.Action(() =>
                    {
                        //label1.Text = n.ToString();
                        //mclb_FillRectangle(i);
                    }));

                }

            }
        }

        private void lsvBlockSet_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lsvBlockSet_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Rectangle rect = e.Bounds;
            int length = Math.Min(rect.Width, rect.Height);
            int gridLen = length / 5;
            Rectangle r = new Rectangle(rect.X + (rect.Width - length) / 2, rect.Y + (rect.Height - length) / 2, length, length);
            if (e.ColumnIndex == 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), r);
                //Pen p = new Pen(Color.White);
                //for (int i = 0; i < 4; i++)       //划线？
                //{
                //    e.Graphics.DrawLine(p, r.Left, r.Top + (i + 1) * gridLen, r.Right, r.Top + (i + 1) * gridLen);
                //    e.Graphics.DrawLine(p, r.Left + (i + 1) * gridLen, r.Top, r.Left + (i + 1) * gridLen, r.Bottom);
                //}
                string str = e.Item.SubItems[e.ColumnIndex].Text;
                string text = e.Item.SubItems[1].Text;
                Color clr = Color.FromArgb(int.Parse(text));
                for (int i = 0; i < str.Length; i++)
                {
                    bool val = (str[i] == '1') ? true : false;
                    if (val)
                    {
                        Rectangle gridRect = new Rectangle(r.Left + (i / 5) * gridLen, r.Top + (i % 5) * gridLen, gridLen, gridLen);
                        e.Graphics.FillRectangle(new SolidBrush(clr), gridRect);
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                string text = e.Item.SubItems[e.ColumnIndex].Text;
                Color clr = Color.FromArgb(int.Parse(text));
                e.Graphics.FillRectangle(new SolidBrush(clr), r);
            }

        }

        private void lsvBlockSet_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawFocusRectangle();
            if (e.Item.Focused)
                e.Graphics.FillRectangle(new SolidBrush(Color.Blue), e.Item.Bounds);
        }

    }
}





