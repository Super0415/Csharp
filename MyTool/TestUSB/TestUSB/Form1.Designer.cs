namespace TestUSB
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
            this.cbbUSBdevice = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbUSBInfo = new System.Windows.Forms.TextBox();
            this.cmsUSBInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清空文本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbPVN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchUSB = new System.Windows.Forms.Button();
            this.tbVID = new System.Windows.Forms.TextBox();
            this.tbPID = new System.Windows.Forms.TextBox();
            this.lbVID = new System.Windows.Forms.Label();
            this.lbPID = new System.Windows.Forms.Label();
            this.lbSelect = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbHex = new System.Windows.Forms.CheckBox();
            this.gbSend = new System.Windows.Forms.GroupBox();
            this.btnSent = new System.Windows.Forms.Button();
            this.tbSend = new System.Windows.Forms.TextBox();
            this.gbReceive = new System.Windows.Forms.GroupBox();
            this.tbReceive = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmsReceiveInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.cmsUSBInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbSend.SuspendLayout();
            this.gbReceive.SuspendLayout();
            this.cmsReceiveInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbUSBdevice
            // 
            this.cbbUSBdevice.FormattingEnabled = true;
            this.cbbUSBdevice.Location = new System.Drawing.Point(74, 25);
            this.cbbUSBdevice.Name = "cbbUSBdevice";
            this.cbbUSBdevice.Size = new System.Drawing.Size(121, 20);
            this.cbbUSBdevice.TabIndex = 0;
            this.cbbUSBdevice.SelectedIndexChanged += new System.EventHandler(this.cbbUSB_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbUSBInfo);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 446);
            this.panel1.TabIndex = 1;
            // 
            // tbUSBInfo
            // 
            this.tbUSBInfo.ContextMenuStrip = this.cmsUSBInfo;
            this.tbUSBInfo.Location = new System.Drawing.Point(3, 226);
            this.tbUSBInfo.Multiline = true;
            this.tbUSBInfo.Name = "tbUSBInfo";
            this.tbUSBInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbUSBInfo.Size = new System.Drawing.Size(214, 217);
            this.tbUSBInfo.TabIndex = 1;
            // 
            // cmsUSBInfo
            // 
            this.cmsUSBInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空文本ToolStripMenuItem});
            this.cmsUSBInfo.Name = "cmsTest";
            this.cmsUSBInfo.Size = new System.Drawing.Size(125, 26);
            // 
            // 清空文本ToolStripMenuItem
            // 
            this.清空文本ToolStripMenuItem.Name = "清空文本ToolStripMenuItem";
            this.清空文本ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.清空文本ToolStripMenuItem.Text = "清空文本";
            this.清空文本ToolStripMenuItem.Click += new System.EventHandler(this.USBInfoClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.tbPVN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSearchUSB);
            this.groupBox1.Controls.Add(this.tbVID);
            this.groupBox1.Controls.Add(this.tbPID);
            this.groupBox1.Controls.Add(this.lbVID);
            this.groupBox1.Controls.Add(this.lbPID);
            this.groupBox1.Controls.Add(this.lbSelect);
            this.groupBox1.Controls.Add(this.cbbUSBdevice);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 217);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USB 配置";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(95, 186);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "打开";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbPVN
            // 
            this.tbPVN.Location = new System.Drawing.Point(74, 121);
            this.tbPVN.Name = "tbPVN";
            this.tbPVN.ReadOnly = true;
            this.tbPVN.Size = new System.Drawing.Size(121, 21);
            this.tbPVN.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "PVN";
            // 
            // btnSearchUSB
            // 
            this.btnSearchUSB.Location = new System.Drawing.Point(95, 155);
            this.btnSearchUSB.Name = "btnSearchUSB";
            this.btnSearchUSB.Size = new System.Drawing.Size(75, 23);
            this.btnSearchUSB.TabIndex = 6;
            this.btnSearchUSB.Text = "搜索";
            this.btnSearchUSB.UseVisualStyleBackColor = true;
            this.btnSearchUSB.Click += new System.EventHandler(this.btnSearchUSB_Click);
            // 
            // tbVID
            // 
            this.tbVID.Location = new System.Drawing.Point(74, 91);
            this.tbVID.Name = "tbVID";
            this.tbVID.ReadOnly = true;
            this.tbVID.Size = new System.Drawing.Size(121, 21);
            this.tbVID.TabIndex = 5;
            // 
            // tbPID
            // 
            this.tbPID.Location = new System.Drawing.Point(74, 61);
            this.tbPID.Name = "tbPID";
            this.tbPID.ReadOnly = true;
            this.tbPID.Size = new System.Drawing.Size(121, 21);
            this.tbPID.TabIndex = 4;
            // 
            // lbVID
            // 
            this.lbVID.AutoSize = true;
            this.lbVID.Location = new System.Drawing.Point(19, 94);
            this.lbVID.Name = "lbVID";
            this.lbVID.Size = new System.Drawing.Size(23, 12);
            this.lbVID.TabIndex = 3;
            this.lbVID.Text = "VID";
            // 
            // lbPID
            // 
            this.lbPID.AutoSize = true;
            this.lbPID.Location = new System.Drawing.Point(19, 64);
            this.lbPID.Name = "lbPID";
            this.lbPID.Size = new System.Drawing.Size(23, 12);
            this.lbPID.TabIndex = 2;
            this.lbPID.Text = "PID";
            // 
            // lbSelect
            // 
            this.lbSelect.AutoSize = true;
            this.lbSelect.Location = new System.Drawing.Point(9, 28);
            this.lbSelect.Name = "lbSelect";
            this.lbSelect.Size = new System.Drawing.Size(41, 12);
            this.lbSelect.TabIndex = 1;
            this.lbSelect.Text = "USB 口";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbHex);
            this.panel2.Controls.Add(this.gbSend);
            this.panel2.Controls.Add(this.gbReceive);
            this.panel2.Location = new System.Drawing.Point(221, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(564, 446);
            this.panel2.TabIndex = 2;
            // 
            // cbHex
            // 
            this.cbHex.AutoSize = true;
            this.cbHex.Location = new System.Drawing.Point(453, 23);
            this.cbHex.Name = "cbHex";
            this.cbHex.Size = new System.Drawing.Size(60, 16);
            this.cbHex.TabIndex = 5;
            this.cbHex.Text = "16进制";
            this.cbHex.UseVisualStyleBackColor = true;
            // 
            // gbSend
            // 
            this.gbSend.Controls.Add(this.btnSent);
            this.gbSend.Controls.Add(this.tbSend);
            this.gbSend.Location = new System.Drawing.Point(3, 288);
            this.gbSend.Name = "gbSend";
            this.gbSend.Size = new System.Drawing.Size(428, 155);
            this.gbSend.TabIndex = 4;
            this.gbSend.TabStop = false;
            this.gbSend.Text = "发送区";
            // 
            // btnSent
            // 
            this.btnSent.Location = new System.Drawing.Point(347, 126);
            this.btnSent.Name = "btnSent";
            this.btnSent.Size = new System.Drawing.Size(75, 23);
            this.btnSent.TabIndex = 10;
            this.btnSent.Text = "发送";
            this.btnSent.UseVisualStyleBackColor = true;
            this.btnSent.Click += new System.EventHandler(this.btnSent_Click);
            // 
            // tbSend
            // 
            this.tbSend.Location = new System.Drawing.Point(6, 20);
            this.tbSend.Multiline = true;
            this.tbSend.Name = "tbSend";
            this.tbSend.Size = new System.Drawing.Size(416, 100);
            this.tbSend.TabIndex = 2;
            // 
            // gbReceive
            // 
            this.gbReceive.Controls.Add(this.tbReceive);
            this.gbReceive.Location = new System.Drawing.Point(3, 3);
            this.gbReceive.Name = "gbReceive";
            this.gbReceive.Size = new System.Drawing.Size(428, 279);
            this.gbReceive.TabIndex = 3;
            this.gbReceive.TabStop = false;
            this.gbReceive.Text = "接收区";
            // 
            // tbReceive
            // 
            this.tbReceive.ContextMenuStrip = this.cmsReceiveInfo;
            this.tbReceive.Location = new System.Drawing.Point(6, 20);
            this.tbReceive.Multiline = true;
            this.tbReceive.Name = "tbReceive";
            this.tbReceive.ReadOnly = true;
            this.tbReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbReceive.Size = new System.Drawing.Size(416, 253);
            this.tbReceive.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmsReceiveInfo
            // 
            this.cmsReceiveInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空ToolStripMenuItem});
            this.cmsReceiveInfo.Name = "cmsReceiveInfo";
            this.cmsReceiveInfo.Size = new System.Drawing.Size(101, 26);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.ReceiveClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 448);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "HID - USB";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmsUSBInfo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbSend.ResumeLayout(false);
            this.gbSend.PerformLayout();
            this.gbReceive.ResumeLayout(false);
            this.gbReceive.PerformLayout();
            this.cmsReceiveInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbUSBdevice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbVID;
        private System.Windows.Forms.TextBox tbPID;
        private System.Windows.Forms.Label lbVID;
        private System.Windows.Forms.Label lbPID;
        private System.Windows.Forms.Label lbSelect;
        private System.Windows.Forms.TextBox tbUSBInfo;
        private System.Windows.Forms.Button btnSearchUSB;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbPVN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbSend;
        private System.Windows.Forms.Button btnSent;
        private System.Windows.Forms.TextBox tbSend;
        private System.Windows.Forms.GroupBox gbReceive;
        private System.Windows.Forms.TextBox tbReceive;
        private System.Windows.Forms.CheckBox cbHex;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip cmsUSBInfo;
        private System.Windows.Forms.ToolStripMenuItem 清空文本ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsReceiveInfo;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
    }
}

