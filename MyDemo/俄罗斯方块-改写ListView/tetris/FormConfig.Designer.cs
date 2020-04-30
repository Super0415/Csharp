namespace tetris
{
    partial class FormConfig
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbBlockBlackColor = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbBlockColNum = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbBlockNumY = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbBlockNumX = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbContra = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbToRight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbToUp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDeasil = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbToDown = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbToleft = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnUpdateBlock = new System.Windows.Forms.Button();
            this.btnClearBlock = new System.Windows.Forms.Button();
            this.btnDelBlock = new System.Windows.Forms.Button();
            this.btnAddBlock = new System.Windows.Forms.Button();
            this.lsvBlockSet = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbColor = new System.Windows.Forms.Label();
            this.lbMode = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(674, 447);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(666, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "参数配置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(229, 328);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(148, 328);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbBlockBlackColor);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tbBlockColNum);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbBlockNumY);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tbBlockNumX);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(229, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 316);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "环境设置";
            // 
            // lbBlockBlackColor
            // 
            this.lbBlockBlackColor.BackColor = System.Drawing.Color.Transparent;
            this.lbBlockBlackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBlockBlackColor.Location = new System.Drawing.Point(95, 208);
            this.lbBlockBlackColor.Name = "lbBlockBlackColor";
            this.lbBlockBlackColor.Size = new System.Drawing.Size(100, 23);
            this.lbBlockBlackColor.TabIndex = 19;
            this.lbBlockBlackColor.Click += new System.EventHandler(this.lbBlockBlackColor_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 214);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "背景颜色";
            // 
            // tbBlockColNum
            // 
            this.tbBlockColNum.Location = new System.Drawing.Point(95, 146);
            this.tbBlockColNum.Name = "tbBlockColNum";
            this.tbBlockColNum.Size = new System.Drawing.Size(100, 21);
            this.tbBlockColNum.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "格子像素";
            // 
            // tbBlockNumY
            // 
            this.tbBlockNumY.Location = new System.Drawing.Point(95, 84);
            this.tbBlockNumY.Name = "tbBlockNumY";
            this.tbBlockNumY.Size = new System.Drawing.Size(100, 21);
            this.tbBlockNumY.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "垂直格子数";
            // 
            // tbBlockNumX
            // 
            this.tbBlockNumX.Location = new System.Drawing.Point(95, 32);
            this.tbBlockNumX.Name = "tbBlockNumX";
            this.tbBlockNumX.Size = new System.Drawing.Size(100, 21);
            this.tbBlockNumX.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "水平格子数";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbContra);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbToRight);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbToUp);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbDeasil);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbToDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbToleft);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 316);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "键盘设置";
            // 
            // tbContra
            // 
            this.tbContra.Location = new System.Drawing.Point(84, 236);
            this.tbContra.Name = "tbContra";
            this.tbContra.ReadOnly = true;
            this.tbContra.Size = new System.Drawing.Size(100, 21);
            this.tbContra.TabIndex = 11;
            this.tbContra.Tag = "";
            this.tbContra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbContra_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 239);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "逆时针旋转";
            // 
            // tbToRight
            // 
            this.tbToRight.Location = new System.Drawing.Point(84, 69);
            this.tbToRight.Name = "tbToRight";
            this.tbToRight.ReadOnly = true;
            this.tbToRight.Size = new System.Drawing.Size(100, 21);
            this.tbToRight.TabIndex = 9;
            this.tbToRight.Tag = "";
            this.tbToRight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbContra_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "向右";
            // 
            // tbToUp
            // 
            this.tbToUp.Location = new System.Drawing.Point(84, 108);
            this.tbToUp.Name = "tbToUp";
            this.tbToUp.ReadOnly = true;
            this.tbToUp.Size = new System.Drawing.Size(100, 21);
            this.tbToUp.TabIndex = 7;
            this.tbToUp.Tag = "";
            this.tbToUp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbContra_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "下落";
            // 
            // tbDeasil
            // 
            this.tbDeasil.Location = new System.Drawing.Point(84, 192);
            this.tbDeasil.Name = "tbDeasil";
            this.tbDeasil.ReadOnly = true;
            this.tbDeasil.Size = new System.Drawing.Size(100, 21);
            this.tbDeasil.TabIndex = 5;
            this.tbDeasil.Tag = "";
            this.tbDeasil.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbContra_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "顺时针旋转";
            // 
            // tbToDown
            // 
            this.tbToDown.Location = new System.Drawing.Point(84, 152);
            this.tbToDown.Name = "tbToDown";
            this.tbToDown.ReadOnly = true;
            this.tbToDown.Size = new System.Drawing.Size(100, 21);
            this.tbToDown.TabIndex = 3;
            this.tbToDown.Tag = "";
            this.tbToDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbContra_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "向下";
            // 
            // tbToleft
            // 
            this.tbToleft.Location = new System.Drawing.Point(84, 32);
            this.tbToleft.Name = "tbToleft";
            this.tbToleft.ReadOnly = true;
            this.tbToleft.Size = new System.Drawing.Size(100, 21);
            this.tbToleft.TabIndex = 1;
            this.tbToleft.Tag = "";
            this.tbToleft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbContra_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "向左";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnUpdateBlock);
            this.tabPage2.Controls.Add(this.btnClearBlock);
            this.tabPage2.Controls.Add(this.btnDelBlock);
            this.tabPage2.Controls.Add(this.btnAddBlock);
            this.tabPage2.Controls.Add(this.lsvBlockSet);
            this.tabPage2.Controls.Add(this.lbColor);
            this.tabPage2.Controls.Add(this.lbMode);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(666, 421);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "砖块样式配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnUpdateBlock
            // 
            this.btnUpdateBlock.Location = new System.Drawing.Point(8, 275);
            this.btnUpdateBlock.Name = "btnUpdateBlock";
            this.btnUpdateBlock.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateBlock.TabIndex = 6;
            this.btnUpdateBlock.Text = "修改";
            this.btnUpdateBlock.UseVisualStyleBackColor = true;
            this.btnUpdateBlock.Click += new System.EventHandler(this.btnUpdateBlock_Click);
            // 
            // btnClearBlock
            // 
            this.btnClearBlock.Location = new System.Drawing.Point(89, 275);
            this.btnClearBlock.Name = "btnClearBlock";
            this.btnClearBlock.Size = new System.Drawing.Size(75, 23);
            this.btnClearBlock.TabIndex = 5;
            this.btnClearBlock.Text = "清空";
            this.btnClearBlock.UseVisualStyleBackColor = true;
            this.btnClearBlock.Click += new System.EventHandler(this.btnClearBlock_Click);
            // 
            // btnDelBlock
            // 
            this.btnDelBlock.Location = new System.Drawing.Point(89, 235);
            this.btnDelBlock.Name = "btnDelBlock";
            this.btnDelBlock.Size = new System.Drawing.Size(75, 23);
            this.btnDelBlock.TabIndex = 4;
            this.btnDelBlock.Text = "删除";
            this.btnDelBlock.UseVisualStyleBackColor = true;
            this.btnDelBlock.Click += new System.EventHandler(this.btnDelBlock_Click);
            // 
            // btnAddBlock
            // 
            this.btnAddBlock.Location = new System.Drawing.Point(8, 235);
            this.btnAddBlock.Name = "btnAddBlock";
            this.btnAddBlock.Size = new System.Drawing.Size(75, 23);
            this.btnAddBlock.TabIndex = 3;
            this.btnAddBlock.Text = "添加";
            this.btnAddBlock.UseVisualStyleBackColor = true;
            this.btnAddBlock.Click += new System.EventHandler(this.btnAddBlock_Click);
            // 
            // lsvBlockSet
            // 
            this.lsvBlockSet.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lsvBlockSet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lsvBlockSet.Font = new System.Drawing.Font("宋体", 24F);
            this.lsvBlockSet.FullRowSelect = true;
            this.lsvBlockSet.GridLines = true;
            this.lsvBlockSet.Location = new System.Drawing.Point(177, 1);
            this.lsvBlockSet.Name = "lsvBlockSet";
            this.lsvBlockSet.OwnerDraw = true;
            this.lsvBlockSet.Size = new System.Drawing.Size(481, 417);
            this.lsvBlockSet.TabIndex = 2;
            this.lsvBlockSet.UseCompatibleStateImageBehavior = false;
            this.lsvBlockSet.View = System.Windows.Forms.View.Details;
            this.lsvBlockSet.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lsvBlockSet_DrawColumnHeader);
            this.lsvBlockSet.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lsvBlockSet_DrawItem);
            this.lsvBlockSet.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lsvBlockSet_DrawSubItem);
            this.lsvBlockSet.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lsvBlockSet_ItemSelectionChanged);
            this.lsvBlockSet.SelectedIndexChanged += new System.EventHandler(this.lsvBlockSet_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "编码";
            this.columnHeader1.Width = 190;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "颜色";
            this.columnHeader2.Width = 217;
            // 
            // lbColor
            // 
            this.lbColor.BackColor = System.Drawing.Color.Gray;
            this.lbColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbColor.Location = new System.Drawing.Point(8, 179);
            this.lbColor.Name = "lbColor";
            this.lbColor.Size = new System.Drawing.Size(142, 23);
            this.lbColor.TabIndex = 1;
            this.lbColor.Click += new System.EventHandler(this.lbColor_Click);
            // 
            // lbMode
            // 
            this.lbMode.BackColor = System.Drawing.Color.Silver;
            this.lbMode.Location = new System.Drawing.Point(6, 0);
            this.lbMode.Name = "lbMode";
            this.lbMode.Size = new System.Drawing.Size(165, 165);
            this.lbMode.TabIndex = 0;
            this.lbMode.Paint += new System.Windows.Forms.PaintEventHandler(this.lbMode_Paint);
            this.lbMode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMode_MouseClick);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 447);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormConfig";
            this.Text = "配置窗体";
            this.Load += new System.EventHandler(this.Config_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lbMode;
        private System.Windows.Forms.Label lbColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ListView lsvBlockSet;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnAddBlock;
        private System.Windows.Forms.Button btnDelBlock;
        private System.Windows.Forms.Button btnClearBlock;
        private System.Windows.Forms.Button btnUpdateBlock;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbContra;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbToRight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbToUp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDeasil;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbToDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbToleft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbBlockColNum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbBlockNumY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbBlockNumX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbBlockBlackColor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}

