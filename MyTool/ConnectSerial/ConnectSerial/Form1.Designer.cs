namespace ConnectSerial
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
            this.tbTimout = new System.Windows.Forms.TextBox();
            this.lbTimout = new System.Windows.Forms.Label();
            this.lbComD = new System.Windows.Forms.Label();
            this.cbbComD = new System.Windows.Forms.ComboBox();
            this.lbBaud = new System.Windows.Forms.Label();
            this.lbComU = new System.Windows.Forms.Label();
            this.cbbBaud = new System.Windows.Forms.ComboBox();
            this.cbbComU = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbRecode = new System.Windows.Forms.TextBox();
            this.cbHexView = new System.Windows.Forms.CheckBox();
            this.cbPLCSum = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbTimout
            // 
            this.tbTimout.Location = new System.Drawing.Point(412, 6);
            this.tbTimout.Name = "tbTimout";
            this.tbTimout.Size = new System.Drawing.Size(53, 21);
            this.tbTimout.TabIndex = 26;
            this.tbTimout.TextChanged += new System.EventHandler(this.tbTimout_TextChanged);
            // 
            // lbTimout
            // 
            this.lbTimout.AutoSize = true;
            this.lbTimout.Location = new System.Drawing.Point(381, 9);
            this.lbTimout.Name = "lbTimout";
            this.lbTimout.Size = new System.Drawing.Size(101, 12);
            this.lbTimout.TabIndex = 25;
            this.lbTimout.Text = "超时          ms";
            // 
            // lbComD
            // 
            this.lbComD.AutoSize = true;
            this.lbComD.Location = new System.Drawing.Point(128, 9);
            this.lbComD.Name = "lbComD";
            this.lbComD.Size = new System.Drawing.Size(29, 12);
            this.lbComD.TabIndex = 24;
            this.lbComD.Text = "对下";
            // 
            // cbbComD
            // 
            this.cbbComD.FormattingEnabled = true;
            this.cbbComD.Location = new System.Drawing.Point(163, 6);
            this.cbbComD.Name = "cbbComD";
            this.cbbComD.Size = new System.Drawing.Size(73, 20);
            this.cbbComD.TabIndex = 23;
            this.cbbComD.SelectedIndexChanged += new System.EventHandler(this.cbbComD_SelectedIndexChanged);
            // 
            // lbBaud
            // 
            this.lbBaud.AutoSize = true;
            this.lbBaud.Location = new System.Drawing.Point(254, 9);
            this.lbBaud.Name = "lbBaud";
            this.lbBaud.Size = new System.Drawing.Size(41, 12);
            this.lbBaud.TabIndex = 22;
            this.lbBaud.Text = "波特率";
            // 
            // lbComU
            // 
            this.lbComU.AutoSize = true;
            this.lbComU.Location = new System.Drawing.Point(3, 9);
            this.lbComU.Name = "lbComU";
            this.lbComU.Size = new System.Drawing.Size(29, 12);
            this.lbComU.TabIndex = 21;
            this.lbComU.Text = "对上";
            // 
            // cbbBaud
            // 
            this.cbbBaud.FormattingEnabled = true;
            this.cbbBaud.Location = new System.Drawing.Point(301, 6);
            this.cbbBaud.Name = "cbbBaud";
            this.cbbBaud.Size = new System.Drawing.Size(65, 20);
            this.cbbBaud.TabIndex = 20;
            this.cbbBaud.SelectedValueChanged += new System.EventHandler(this.cbbBaud_SelectedValueChanged);
            // 
            // cbbComU
            // 
            this.cbbComU.FormattingEnabled = true;
            this.cbbComU.Location = new System.Drawing.Point(38, 6);
            this.cbbComU.Name = "cbbComU";
            this.cbbComU.Size = new System.Drawing.Size(74, 20);
            this.cbbComU.TabIndex = 19;
            this.cbbComU.SelectedIndexChanged += new System.EventHandler(this.cbbComU_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(513, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 18;
            this.btnConnect.Text = "未连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbRecode
            // 
            this.tbRecode.Location = new System.Drawing.Point(4, 33);
            this.tbRecode.Multiline = true;
            this.tbRecode.Name = "tbRecode";
            this.tbRecode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRecode.Size = new System.Drawing.Size(609, 374);
            this.tbRecode.TabIndex = 27;
            // 
            // cbHexView
            // 
            this.cbHexView.AutoSize = true;
            this.cbHexView.Location = new System.Drawing.Point(620, 51);
            this.cbHexView.Name = "cbHexView";
            this.cbHexView.Size = new System.Drawing.Size(72, 16);
            this.cbHexView.TabIndex = 28;
            this.cbHexView.Text = "十六进制";
            this.cbHexView.UseVisualStyleBackColor = true;
            // 
            // cbPLCSum
            // 
            this.cbPLCSum.AutoSize = true;
            this.cbPLCSum.Location = new System.Drawing.Point(619, 73);
            this.cbPLCSum.Name = "cbPLCSum";
            this.cbPLCSum.Size = new System.Drawing.Size(66, 16);
            this.cbPLCSum.TabIndex = 29;
            this.cbPLCSum.Text = "PLC校验";
            this.cbPLCSum.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(610, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 30;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 415);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cbPLCSum);
            this.Controls.Add(this.cbHexView);
            this.Controls.Add(this.tbRecode);
            this.Controls.Add(this.tbTimout);
            this.Controls.Add(this.lbTimout);
            this.Controls.Add(this.lbComD);
            this.Controls.Add(this.cbbComD);
            this.Controls.Add(this.lbBaud);
            this.Controls.Add(this.lbComU);
            this.Controls.Add(this.cbbBaud);
            this.Controls.Add(this.cbbComU);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbTimout;
        private System.Windows.Forms.Label lbTimout;
        private System.Windows.Forms.Label lbComD;
        private System.Windows.Forms.ComboBox cbbComD;
        private System.Windows.Forms.Label lbBaud;
        private System.Windows.Forms.Label lbComU;
        private System.Windows.Forms.ComboBox cbbBaud;
        private System.Windows.Forms.ComboBox cbbComU;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbRecode;
        private System.Windows.Forms.CheckBox cbHexView;
        private System.Windows.Forms.CheckBox cbPLCSum;
        private System.Windows.Forms.Button btnClear;
    }
}

