namespace InfoManage
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
            this.lbConnectNFC = new System.Windows.Forms.Label();
            this.lbConnectUSB = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbConnectNFC
            // 
            this.lbConnectNFC.BackColor = System.Drawing.Color.Maroon;
            this.lbConnectNFC.Location = new System.Drawing.Point(0, 0);
            this.lbConnectNFC.Name = "lbConnectNFC";
            this.lbConnectNFC.Size = new System.Drawing.Size(31, 29);
            this.lbConnectNFC.TabIndex = 0;
            this.toolTip1.SetToolTip(this.lbConnectNFC, "硬件连接指示");
            // 
            // lbConnectUSB
            // 
            this.lbConnectUSB.BackColor = System.Drawing.Color.Maroon;
            this.lbConnectUSB.Location = new System.Drawing.Point(62, 0);
            this.lbConnectUSB.Name = "lbConnectUSB";
            this.lbConnectUSB.Size = new System.Drawing.Size(31, 29);
            this.lbConnectUSB.TabIndex = 1;
            this.toolTip1.SetToolTip(this.lbConnectUSB, "USB连接指示");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbConnectNFC);
            this.panel1.Controls.Add(this.lbConnectUSB);
            this.panel1.Location = new System.Drawing.Point(669, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 31);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(125, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 29);
            this.label1.TabIndex = 2;
            this.toolTip1.SetToolTip(this.label1, "NFC连接指示");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单ToolStripMenuItem,
            this.tsmiOpen,
            this.tsmiSave,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(184, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(44, 21);
            this.tsmiOpen.Text = "打开";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(44, 21);
            this.tsmiSave.Text = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 539);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "信息管理系统";
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbConnectNFC;
        private System.Windows.Forms.Label lbConnectUSB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
    }
}

