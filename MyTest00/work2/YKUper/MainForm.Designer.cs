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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iP地址ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TstbNetip = new System.Windows.Forms.ToolStripTextBox();
            this.端口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TstbNetport = new System.Windows.Forms.ToolStripTextBox();
            this.读写超时ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TstbNetimeout = new System.Windows.Forms.ToolStripTextBox();
            this.串口配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.端口ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.TscbComport = new System.Windows.Forms.ToolStripComboBox();
            this.波特率ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TscbCombaud = new System.Windows.Forms.ToolStripComboBox();
            this.读写超时ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.TstbComTimeout = new System.Windows.Forms.ToolStripTextBox();
            this.下位机识别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TscbCheck = new System.Windows.Forms.ToolStripComboBox();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIVers = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnComt = new System.Windows.Forms.Button();
            this.BtnNett = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.BtnCheck = new System.Windows.Forms.Button();
            this.TbRecode = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(688, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.网络配置ToolStripMenuItem,
            this.串口配置ToolStripMenuItem,
            this.下位机识别ToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // 网络配置ToolStripMenuItem
            // 
            this.网络配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iP地址ToolStripMenuItem,
            this.端口ToolStripMenuItem,
            this.读写超时ToolStripMenuItem});
            this.网络配置ToolStripMenuItem.Name = "网络配置ToolStripMenuItem";
            this.网络配置ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.网络配置ToolStripMenuItem.Text = "网络配置";
            // 
            // iP地址ToolStripMenuItem
            // 
            this.iP地址ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TstbNetip});
            this.iP地址ToolStripMenuItem.Name = "iP地址ToolStripMenuItem";
            this.iP地址ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.iP地址ToolStripMenuItem.Text = "IP地址";
            // 
            // TstbNetip
            // 
            this.TstbNetip.Name = "TstbNetip";
            this.TstbNetip.Size = new System.Drawing.Size(100, 23);
            this.TstbNetip.TextChanged += new System.EventHandler(this.TstbNetip_TextChanged);
            // 
            // 端口ToolStripMenuItem
            // 
            this.端口ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TstbNetport});
            this.端口ToolStripMenuItem.Name = "端口ToolStripMenuItem";
            this.端口ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.端口ToolStripMenuItem.Text = "端口";
            // 
            // TstbNetport
            // 
            this.TstbNetport.Name = "TstbNetport";
            this.TstbNetport.Size = new System.Drawing.Size(121, 23);
            this.TstbNetport.TextChanged += new System.EventHandler(this.TstbNetport_TextChanged);
            // 
            // 读写超时ToolStripMenuItem
            // 
            this.读写超时ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TstbNetimeout});
            this.读写超时ToolStripMenuItem.Name = "读写超时ToolStripMenuItem";
            this.读写超时ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.读写超时ToolStripMenuItem.Text = "读写超时";
            // 
            // TstbNetimeout
            // 
            this.TstbNetimeout.Name = "TstbNetimeout";
            this.TstbNetimeout.Size = new System.Drawing.Size(100, 23);
            this.TstbNetimeout.TextChanged += new System.EventHandler(this.TstbNetimeout_TextChanged);
            // 
            // 串口配置ToolStripMenuItem
            // 
            this.串口配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.端口ToolStripMenuItem1,
            this.波特率ToolStripMenuItem,
            this.读写超时ToolStripMenuItem1});
            this.串口配置ToolStripMenuItem.Name = "串口配置ToolStripMenuItem";
            this.串口配置ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.串口配置ToolStripMenuItem.Text = "串口配置";
            // 
            // 端口ToolStripMenuItem1
            // 
            this.端口ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TscbComport});
            this.端口ToolStripMenuItem1.Name = "端口ToolStripMenuItem1";
            this.端口ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.端口ToolStripMenuItem1.Text = "端口";
            // 
            // TscbComport
            // 
            this.TscbComport.Name = "TscbComport";
            this.TscbComport.Size = new System.Drawing.Size(121, 25);
            this.TscbComport.SelectedIndexChanged += new System.EventHandler(this.TscbComport_SelectedIndexChanged);
            // 
            // 波特率ToolStripMenuItem
            // 
            this.波特率ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TscbCombaud});
            this.波特率ToolStripMenuItem.Name = "波特率ToolStripMenuItem";
            this.波特率ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.波特率ToolStripMenuItem.Text = "波特率";
            // 
            // TscbCombaud
            // 
            this.TscbCombaud.Name = "TscbCombaud";
            this.TscbCombaud.Size = new System.Drawing.Size(121, 25);
            this.TscbCombaud.SelectedIndexChanged += new System.EventHandler(this.TscbCombaud_SelectedIndexChanged);
            // 
            // 读写超时ToolStripMenuItem1
            // 
            this.读写超时ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TstbComTimeout});
            this.读写超时ToolStripMenuItem1.Name = "读写超时ToolStripMenuItem1";
            this.读写超时ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.读写超时ToolStripMenuItem1.Text = "读写超时";
            // 
            // TstbComTimeout
            // 
            this.TstbComTimeout.Name = "TstbComTimeout";
            this.TstbComTimeout.Size = new System.Drawing.Size(100, 23);
            this.TstbComTimeout.TextChanged += new System.EventHandler(this.TstbComTimeout_TextChanged);
            // 
            // 下位机识别ToolStripMenuItem
            // 
            this.下位机识别ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TscbCheck});
            this.下位机识别ToolStripMenuItem.Name = "下位机识别ToolStripMenuItem";
            this.下位机识别ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.下位机识别ToolStripMenuItem.Text = "下位机识别";
            // 
            // TscbCheck
            // 
            this.TscbCheck.Name = "TscbCheck";
            this.TscbCheck.Size = new System.Drawing.Size(121, 25);
            this.TscbCheck.SelectedIndexChanged += new System.EventHandler(this.TscbCheck_SelectedIndexChanged);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIVers});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // TSMIVers
            // 
            this.TSMIVers.Name = "TSMIVers";
            this.TSMIVers.Size = new System.Drawing.Size(124, 22);
            this.TSMIVers.Text = "产品信息";
            // 
            // BtnComt
            // 
            this.BtnComt.Location = new System.Drawing.Point(398, 0);
            this.BtnComt.Name = "BtnComt";
            this.BtnComt.Size = new System.Drawing.Size(88, 23);
            this.BtnComt.TabIndex = 6;
            this.BtnComt.Text = "串口未连接";
            this.BtnComt.UseVisualStyleBackColor = true;
            this.BtnComt.Click += new System.EventHandler(this.BtnComt_Click);
            // 
            // BtnNett
            // 
            this.BtnNett.BackColor = System.Drawing.SystemColors.Control;
            this.BtnNett.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnNett.Location = new System.Drawing.Point(498, 0);
            this.BtnNett.Name = "BtnNett";
            this.BtnNett.Size = new System.Drawing.Size(88, 23);
            this.BtnNett.TabIndex = 7;
            this.BtnNett.Text = "网口未连接";
            this.BtnNett.UseVisualStyleBackColor = false;
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(4, 29);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(680, 330);
            this.panel.TabIndex = 8;
            // 
            // BtnCheck
            // 
            this.BtnCheck.BackColor = System.Drawing.SystemColors.Control;
            this.BtnCheck.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCheck.Location = new System.Drawing.Point(598, 0);
            this.BtnCheck.Name = "BtnCheck";
            this.BtnCheck.Size = new System.Drawing.Size(88, 23);
            this.BtnCheck.TabIndex = 10;
            this.BtnCheck.Text = "检测版本";
            this.BtnCheck.UseVisualStyleBackColor = false;
            this.BtnCheck.Click += new System.EventHandler(this.BtnCheck_Click);
            // 
            // TbRecode
            // 
            this.TbRecode.Location = new System.Drawing.Point(4, 363);
            this.TbRecode.Margin = new System.Windows.Forms.Padding(0);
            this.TbRecode.MaximumSize = new System.Drawing.Size(677, 90);
            this.TbRecode.MinimumSize = new System.Drawing.Size(677, 90);
            this.TbRecode.Multiline = true;
            this.TbRecode.Name = "TbRecode";
            this.TbRecode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TbRecode.Size = new System.Drawing.Size(677, 90);
            this.TbRecode.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 457);
            this.Controls.Add(this.TbRecode);
            this.Controls.Add(this.BtnCheck);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.BtnNett);
            this.Controls.Add(this.BtnComt);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "YKUper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网络配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 串口配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iP地址ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 端口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 读写超时ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 端口ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 波特率ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 读写超时ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox TstbNetimeout;
        private System.Windows.Forms.ToolStripComboBox TscbComport;
        private System.Windows.Forms.ToolStripComboBox TscbCombaud;
        private System.Windows.Forms.ToolStripTextBox TstbComTimeout;
        private System.Windows.Forms.ToolStripMenuItem TSMIVers;
        private System.Windows.Forms.Button BtnComt;
        private System.Windows.Forms.Button BtnNett;
        private System.Windows.Forms.ToolStripTextBox TstbNetip;
        private System.Windows.Forms.ToolStripTextBox TstbNetport;
        private System.Windows.Forms.ToolStripMenuItem 下位机识别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox TscbCheck;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button BtnCheck;
        private System.Windows.Forms.TextBox TbRecode;
    }
}

