namespace YKUper
{
    partial class MainForm
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
            this.panel = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnCheck = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iP地址ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tstbNetip = new System.Windows.Forms.ToolStripTextBox();
            this.tsmiNetport = new System.Windows.Forms.ToolStripMenuItem();
            this.tstbNetport = new System.Windows.Forms.ToolStripTextBox();
            this.读写超时ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tstbNetime = new System.Windows.Forms.ToolStripTextBox();
            this.串口配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.端口ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tscbComport = new System.Windows.Forms.ToolStripComboBox();
            this.波特率ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tscbCombaud = new System.Windows.Forms.ToolStripComboBox();
            this.读写超时ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tstbComtime = new System.Windows.Forms.ToolStripTextBox();
            this.下位机版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tscbVer = new System.Windows.Forms.ToolStripComboBox();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.产品信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnComt = new System.Windows.Forms.Button();
            this.btnNett = new System.Windows.Forms.Button();
            this.tbRecode = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(4, 27);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(680, 330);
            this.panel.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(598, 0);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(88, 23);
            this.btnCheck.TabIndex = 1;
            this.btnCheck.Text = "检测版本";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(688, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.网络配置ToolStripMenuItem,
            this.串口配置ToolStripMenuItem,
            this.下位机版本ToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // 网络配置ToolStripMenuItem
            // 
            this.网络配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iP地址ToolStripMenuItem,
            this.tsmiNetport,
            this.读写超时ToolStripMenuItem});
            this.网络配置ToolStripMenuItem.Name = "网络配置ToolStripMenuItem";
            this.网络配置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.网络配置ToolStripMenuItem.Text = "网络配置";
            // 
            // iP地址ToolStripMenuItem
            // 
            this.iP地址ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstbNetip});
            this.iP地址ToolStripMenuItem.Name = "iP地址ToolStripMenuItem";
            this.iP地址ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.iP地址ToolStripMenuItem.Text = "IP地址";
            // 
            // tstbNetip
            // 
            this.tstbNetip.Name = "tstbNetip";
            this.tstbNetip.Size = new System.Drawing.Size(100, 23);
            this.tstbNetip.TextChanged += new System.EventHandler(this.tstbNetip_TextChanged);
            // 
            // tsmiNetport
            // 
            this.tsmiNetport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstbNetport});
            this.tsmiNetport.Name = "tsmiNetport";
            this.tsmiNetport.Size = new System.Drawing.Size(124, 22);
            this.tsmiNetport.Text = "端口";
            // 
            // tstbNetport
            // 
            this.tstbNetport.Name = "tstbNetport";
            this.tstbNetport.Size = new System.Drawing.Size(100, 23);
            this.tstbNetport.TextChanged += new System.EventHandler(this.tstbNetport_TextChanged);
            // 
            // 读写超时ToolStripMenuItem
            // 
            this.读写超时ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstbNetime});
            this.读写超时ToolStripMenuItem.Name = "读写超时ToolStripMenuItem";
            this.读写超时ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.读写超时ToolStripMenuItem.Text = "读写超时";
            // 
            // tstbNetime
            // 
            this.tstbNetime.Name = "tstbNetime";
            this.tstbNetime.Size = new System.Drawing.Size(100, 23);
            this.tstbNetime.TextChanged += new System.EventHandler(this.tstbNetime_TextChanged);
            // 
            // 串口配置ToolStripMenuItem
            // 
            this.串口配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.端口ToolStripMenuItem1,
            this.波特率ToolStripMenuItem,
            this.读写超时ToolStripMenuItem1});
            this.串口配置ToolStripMenuItem.Name = "串口配置ToolStripMenuItem";
            this.串口配置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.串口配置ToolStripMenuItem.Text = "串口配置";
            // 
            // 端口ToolStripMenuItem1
            // 
            this.端口ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscbComport});
            this.端口ToolStripMenuItem1.Name = "端口ToolStripMenuItem1";
            this.端口ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.端口ToolStripMenuItem1.Text = "端口";
            // 
            // tscbComport
            // 
            this.tscbComport.Name = "tscbComport";
            this.tscbComport.Size = new System.Drawing.Size(121, 25);
            this.tscbComport.TextChanged += new System.EventHandler(this.tscbComport_TextChanged);
            // 
            // 波特率ToolStripMenuItem
            // 
            this.波特率ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscbCombaud});
            this.波特率ToolStripMenuItem.Name = "波特率ToolStripMenuItem";
            this.波特率ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.波特率ToolStripMenuItem.Text = "波特率";
            // 
            // tscbCombaud
            // 
            this.tscbCombaud.Name = "tscbCombaud";
            this.tscbCombaud.Size = new System.Drawing.Size(121, 25);
            this.tscbCombaud.TextChanged += new System.EventHandler(this.tscbCombaud_TextChanged);
            // 
            // 读写超时ToolStripMenuItem1
            // 
            this.读写超时ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstbComtime});
            this.读写超时ToolStripMenuItem1.Name = "读写超时ToolStripMenuItem1";
            this.读写超时ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.读写超时ToolStripMenuItem1.Text = "读写超时";
            // 
            // tstbComtime
            // 
            this.tstbComtime.Name = "tstbComtime";
            this.tstbComtime.Size = new System.Drawing.Size(100, 23);
            this.tstbComtime.TextChanged += new System.EventHandler(this.tstbComtime_TextChanged);
            // 
            // 下位机版本ToolStripMenuItem
            // 
            this.下位机版本ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscbVer});
            this.下位机版本ToolStripMenuItem.Name = "下位机版本ToolStripMenuItem";
            this.下位机版本ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.下位机版本ToolStripMenuItem.Text = "下位机版本";
            // 
            // tscbVer
            // 
            this.tscbVer.Name = "tscbVer";
            this.tscbVer.Size = new System.Drawing.Size(121, 25);
            this.tscbVer.SelectedIndexChanged += new System.EventHandler(this.tscbVer_SelectedIndexChanged);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.产品信息ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 产品信息ToolStripMenuItem
            // 
            this.产品信息ToolStripMenuItem.Name = "产品信息ToolStripMenuItem";
            this.产品信息ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.产品信息ToolStripMenuItem.Text = "产品信息";
            // 
            // btnComt
            // 
            this.btnComt.Location = new System.Drawing.Point(398, 0);
            this.btnComt.Name = "btnComt";
            this.btnComt.Size = new System.Drawing.Size(88, 23);
            this.btnComt.TabIndex = 3;
            this.btnComt.Text = "串口未连接";
            this.btnComt.UseVisualStyleBackColor = true;
            // 
            // btnNett
            // 
            this.btnNett.Location = new System.Drawing.Point(498, 0);
            this.btnNett.Name = "btnNett";
            this.btnNett.Size = new System.Drawing.Size(88, 23);
            this.btnNett.TabIndex = 4;
            this.btnNett.Text = "网口未连接";
            this.btnNett.UseVisualStyleBackColor = true;
            // 
            // tbRecode
            // 
            this.tbRecode.Location = new System.Drawing.Point(4, 363);
            this.tbRecode.Multiline = true;
            this.tbRecode.Name = "tbRecode";
            this.tbRecode.Size = new System.Drawing.Size(677, 90);
            this.tbRecode.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 457);
            this.Controls.Add(this.tbRecode);
            this.Controls.Add(this.btnNett);
            this.Controls.Add(this.btnComt);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网络配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 串口配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下位机版本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iP地址ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiNetport;
        private System.Windows.Forms.ToolStripMenuItem 读写超时ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox tstbNetip;
        private System.Windows.Forms.ToolStripTextBox tstbNetport;
        private System.Windows.Forms.ToolStripTextBox tstbNetime;
        private System.Windows.Forms.ToolStripMenuItem 端口ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox tscbComport;
        private System.Windows.Forms.ToolStripMenuItem 波特率ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox tscbCombaud;
        private System.Windows.Forms.ToolStripMenuItem 读写超时ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox tstbComtime;
        private System.Windows.Forms.ToolStripComboBox tscbVer;
        private System.Windows.Forms.ToolStripMenuItem 产品信息ToolStripMenuItem;
        private System.Windows.Forms.Button btnComt;
        private System.Windows.Forms.Button btnNett;
        private System.Windows.Forms.TextBox tbRecode;
    }
}

