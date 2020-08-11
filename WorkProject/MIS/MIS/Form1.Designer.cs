namespace MIS
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btnUSB = new System.Windows.Forms.Button();
            this.btnHard = new System.Windows.Forms.Button();
            this.btnNFC = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbControl = new System.Windows.Forms.GroupBox();
            this.timerReflash = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clnEquipNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnEquip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnJobNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnComType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUSB
            // 
            this.btnUSB.Location = new System.Drawing.Point(0, 5);
            this.btnUSB.Name = "btnUSB";
            this.btnUSB.Size = new System.Drawing.Size(30, 30);
            this.btnUSB.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnUSB, "USB连接指示");
            this.btnUSB.UseVisualStyleBackColor = true;
            // 
            // btnHard
            // 
            this.btnHard.Location = new System.Drawing.Point(50, 5);
            this.btnHard.Name = "btnHard";
            this.btnHard.Size = new System.Drawing.Size(30, 30);
            this.btnHard.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnHard, "硬件连接指示");
            this.btnHard.UseVisualStyleBackColor = true;
            // 
            // btnNFC
            // 
            this.btnNFC.Location = new System.Drawing.Point(100, 5);
            this.btnNFC.Name = "btnNFC";
            this.btnNFC.Size = new System.Drawing.Size(30, 30);
            this.btnNFC.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnNFC, "NFC连接指示");
            this.btnNFC.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(943, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUSB);
            this.panel1.Controls.Add(this.btnNFC);
            this.panel1.Controls.Add(this.btnHard);
            this.panel1.Location = new System.Drawing.Point(6, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 40);
            this.panel1.TabIndex = 4;
            // 
            // gbControl
            // 
            this.gbControl.Controls.Add(this.button3);
            this.gbControl.Controls.Add(this.button2);
            this.gbControl.Controls.Add(this.button1);
            this.gbControl.Controls.Add(this.panel1);
            this.gbControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbControl.Location = new System.Drawing.Point(786, 25);
            this.gbControl.Name = "gbControl";
            this.gbControl.Size = new System.Drawing.Size(157, 391);
            this.gbControl.TabIndex = 6;
            this.gbControl.TabStop = false;
            this.gbControl.Text = "控制面板";
            // 
            // timerReflash
            // 
            this.timerReflash.Enabled = true;
            this.timerReflash.Tick += new System.EventHandler(this.timerReflash_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "修改";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 182);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "写入";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(41, 241);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "读取";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnNum,
            this.clnComType,
            this.clnName,
            this.clnSex,
            this.clnAge,
            this.clnBranch,
            this.clnPosition,
            this.clnLevel,
            this.clnJobNum,
            this.clnDate,
            this.clnEquip,
            this.clnEquipNum});
            this.dataGridView1.Location = new System.Drawing.Point(6, 11);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(776, 369);
            this.dataGridView1.TabIndex = 0;
            // 
            // clnEquipNum
            // 
            this.clnEquipNum.HeaderText = "设备编号";
            this.clnEquipNum.Name = "clnEquipNum";
            this.clnEquipNum.Width = 77;
            // 
            // clnEquip
            // 
            this.clnEquip.HeaderText = "设备类型";
            this.clnEquip.Name = "clnEquip";
            this.clnEquip.Width = 77;
            // 
            // clnDate
            // 
            this.clnDate.HeaderText = "登记日期";
            this.clnDate.Name = "clnDate";
            this.clnDate.Width = 77;
            // 
            // clnJobNum
            // 
            this.clnJobNum.HeaderText = "工号";
            this.clnJobNum.Name = "clnJobNum";
            this.clnJobNum.Width = 53;
            // 
            // clnLevel
            // 
            this.clnLevel.HeaderText = "权限";
            this.clnLevel.Name = "clnLevel";
            this.clnLevel.Width = 53;
            // 
            // clnPosition
            // 
            this.clnPosition.HeaderText = "职位";
            this.clnPosition.Name = "clnPosition";
            this.clnPosition.Width = 53;
            // 
            // clnBranch
            // 
            this.clnBranch.HeaderText = "部门";
            this.clnBranch.Name = "clnBranch";
            this.clnBranch.Width = 53;
            // 
            // clnAge
            // 
            this.clnAge.HeaderText = "年龄";
            this.clnAge.Name = "clnAge";
            this.clnAge.Width = 53;
            // 
            // clnSex
            // 
            this.clnSex.HeaderText = "性别";
            this.clnSex.Name = "clnSex";
            this.clnSex.Width = 53;
            // 
            // clnName
            // 
            this.clnName.HeaderText = "姓名";
            this.clnName.Name = "clnName";
            this.clnName.Width = 53;
            // 
            // clnComType
            // 
            this.clnComType.HeaderText = "通讯类型";
            this.clnComType.Name = "clnComType";
            this.clnComType.ReadOnly = true;
            this.clnComType.Width = 77;
            // 
            // clnNum
            // 
            this.clnNum.HeaderText = "序号";
            this.clnNum.Name = "clnNum";
            this.clnNum.ReadOnly = true;
            this.clnNum.Width = 53;
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.dataGridView1);
            this.gbInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbInfo.Location = new System.Drawing.Point(0, 25);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(786, 391);
            this.gbInfo.TabIndex = 5;
            this.gbInfo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 416);
            this.Controls.Add(this.gbControl);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "人员信息管理系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUSB;
        private System.Windows.Forms.Button btnHard;
        private System.Windows.Forms.Button btnNFC;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbControl;
        private System.Windows.Forms.Timer timerReflash;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnComType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnJobNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnEquip;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnEquipNum;
        private System.Windows.Forms.GroupBox gbInfo;
    }
}

