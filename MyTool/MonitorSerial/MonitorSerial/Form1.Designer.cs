namespace MonitorSerial
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
            this.lbBaud = new System.Windows.Forms.Label();
            this.lbComU = new System.Windows.Forms.Label();
            this.cbbBaud = new System.Windows.Forms.ComboBox();
            this.cbbComU = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lbComD = new System.Windows.Forms.Label();
            this.cbbComD = new System.Windows.Forms.ComboBox();
            this.tbRecode = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbTimout = new System.Windows.Forms.Label();
            this.tbTimout = new System.Windows.Forms.TextBox();
            this.cbCheckSum = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbBaud
            // 
            this.lbBaud.AutoSize = true;
            this.lbBaud.Location = new System.Drawing.Point(262, 15);
            this.lbBaud.Name = "lbBaud";
            this.lbBaud.Size = new System.Drawing.Size(41, 12);
            this.lbBaud.TabIndex = 11;
            this.lbBaud.Text = "波特率";
            // 
            // lbComU
            // 
            this.lbComU.AutoSize = true;
            this.lbComU.Location = new System.Drawing.Point(11, 15);
            this.lbComU.Name = "lbComU";
            this.lbComU.Size = new System.Drawing.Size(29, 12);
            this.lbComU.TabIndex = 10;
            this.lbComU.Text = "对上";
            // 
            // cbbBaud
            // 
            this.cbbBaud.FormattingEnabled = true;
            this.cbbBaud.Location = new System.Drawing.Point(309, 12);
            this.cbbBaud.Name = "cbbBaud";
            this.cbbBaud.Size = new System.Drawing.Size(65, 20);
            this.cbbBaud.TabIndex = 9;
            this.cbbBaud.SelectedIndexChanged += new System.EventHandler(this.cbbBaud_SelectedIndexChanged);
            // 
            // cbbComU
            // 
            this.cbbComU.FormattingEnabled = true;
            this.cbbComU.Location = new System.Drawing.Point(46, 12);
            this.cbbComU.Name = "cbbComU";
            this.cbbComU.Size = new System.Drawing.Size(74, 20);
            this.cbbComU.TabIndex = 8;
            this.cbbComU.SelectedIndexChanged += new System.EventHandler(this.cbbComU_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(546, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lbComD
            // 
            this.lbComD.AutoSize = true;
            this.lbComD.Location = new System.Drawing.Point(136, 15);
            this.lbComD.Name = "lbComD";
            this.lbComD.Size = new System.Drawing.Size(29, 12);
            this.lbComD.TabIndex = 13;
            this.lbComD.Text = "对下";
            // 
            // cbbComD
            // 
            this.cbbComD.FormattingEnabled = true;
            this.cbbComD.Location = new System.Drawing.Point(171, 12);
            this.cbbComD.Name = "cbbComD";
            this.cbbComD.Size = new System.Drawing.Size(73, 20);
            this.cbbComD.TabIndex = 12;
            this.cbbComD.SelectedIndexChanged += new System.EventHandler(this.cbbComD_SelectedIndexChanged);
            // 
            // tbRecode
            // 
            this.tbRecode.Location = new System.Drawing.Point(12, 39);
            this.tbRecode.Multiline = true;
            this.tbRecode.Name = "tbRecode";
            this.tbRecode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRecode.Size = new System.Drawing.Size(609, 374);
            this.tbRecode.TabIndex = 14;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(627, 50);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbTimout
            // 
            this.lbTimout.AutoSize = true;
            this.lbTimout.Location = new System.Drawing.Point(389, 15);
            this.lbTimout.Name = "lbTimout";
            this.lbTimout.Size = new System.Drawing.Size(101, 12);
            this.lbTimout.TabIndex = 16;
            this.lbTimout.Text = "超时          ms";
            // 
            // tbTimout
            // 
            this.tbTimout.Location = new System.Drawing.Point(420, 12);
            this.tbTimout.Name = "tbTimout";
            this.tbTimout.Size = new System.Drawing.Size(53, 21);
            this.tbTimout.TabIndex = 17;
            this.tbTimout.Leave += new System.EventHandler(this.tbTimout_Leave);
            // 
            // cbCheckSum
            // 
            this.cbCheckSum.AutoSize = true;
            this.cbCheckSum.Location = new System.Drawing.Point(627, 93);
            this.cbCheckSum.Name = "cbCheckSum";
            this.cbCheckSum.Size = new System.Drawing.Size(78, 16);
            this.cbCheckSum.TabIndex = 18;
            this.cbCheckSum.Text = "PLC和校验";
            this.cbCheckSum.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 425);
            this.Controls.Add(this.cbCheckSum);
            this.Controls.Add(this.tbTimout);
            this.Controls.Add(this.lbTimout);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tbRecode);
            this.Controls.Add(this.lbComD);
            this.Controls.Add(this.cbbComD);
            this.Controls.Add(this.lbBaud);
            this.Controls.Add(this.lbComU);
            this.Controls.Add(this.cbbBaud);
            this.Controls.Add(this.cbbComU);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbBaud;
        private System.Windows.Forms.Label lbComU;
        private System.Windows.Forms.ComboBox cbbBaud;
        private System.Windows.Forms.ComboBox cbbComU;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lbComD;
        private System.Windows.Forms.ComboBox cbbComD;
        private System.Windows.Forms.TextBox tbRecode;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lbTimout;
        private System.Windows.Forms.TextBox tbTimout;
        private System.Windows.Forms.CheckBox cbCheckSum;
    }
}

