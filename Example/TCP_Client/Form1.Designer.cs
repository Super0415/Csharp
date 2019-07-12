namespace TCP_Client
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sslbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.rtbChatIn = new System.Windows.Forms.RichTextBox();
            this.txtCarNO = new System.Windows.Forms.TextBox();
            this.rdoASCII = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.iptxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.porttxt = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lbcar = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 515);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(868, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sslbStatus
            // 
            this.sslbStatus.Name = "sslbStatus";
            this.sslbStatus.Size = new System.Drawing.Size(131, 17);
            this.sslbStatus.Text = "toolStripStatusLabel1";
            // 
            // rtbChatIn
            // 
            this.rtbChatIn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.rtbChatIn.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtbChatIn.Location = new System.Drawing.Point(0, 0);
            this.rtbChatIn.Name = "rtbChatIn";
            this.rtbChatIn.Size = new System.Drawing.Size(627, 515);
            this.rtbChatIn.TabIndex = 3;
            this.rtbChatIn.Text = "";
            // 
            // txtCarNO
            // 
            this.txtCarNO.Location = new System.Drawing.Point(727, 105);
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.Size = new System.Drawing.Size(104, 21);
            this.txtCarNO.TabIndex = 44;
            this.txtCarNO.Text = "0000000001";
            // 
            // rdoASCII
            // 
            this.rdoASCII.AutoSize = true;
            this.rdoASCII.Checked = true;
            this.rdoASCII.Location = new System.Drawing.Point(740, 132);
            this.rdoASCII.Name = "rdoASCII";
            this.rdoASCII.Size = new System.Drawing.Size(53, 16);
            this.rdoASCII.TabIndex = 43;
            this.rdoASCII.TabStop = true;
            this.rdoASCII.Text = "ASCII";
            this.rdoASCII.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(667, 132);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(65, 16);
            this.radioButton1.TabIndex = 42;
            this.radioButton1.Text = "Unicode";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(652, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "IP地址:";
            // 
            // iptxt
            // 
            this.iptxt.BackColor = System.Drawing.SystemColors.Info;
            this.iptxt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.iptxt.Location = new System.Drawing.Point(707, 12);
            this.iptxt.Name = "iptxt";
            this.iptxt.Size = new System.Drawing.Size(95, 21);
            this.iptxt.TabIndex = 40;
            this.iptxt.Text = "61.191.52.134";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(652, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "端口号:";
            // 
            // porttxt
            // 
            this.porttxt.BackColor = System.Drawing.SystemColors.Info;
            this.porttxt.Location = new System.Drawing.Point(707, 39);
            this.porttxt.Name = "porttxt";
            this.porttxt.Size = new System.Drawing.Size(60, 21);
            this.porttxt.TabIndex = 41;
            this.porttxt.Text = "8800";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(655, 66);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(71, 28);
            this.btnConnect.TabIndex = 37;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(667, 154);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(78, 30);
            this.btnSend.TabIndex = 36;
            this.btnSend.Text = "Send_V1";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(740, 66);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(71, 29);
            this.btnDisconnect.TabIndex = 35;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lbcar
            // 
            this.lbcar.AutoSize = true;
            this.lbcar.Location = new System.Drawing.Point(651, 109);
            this.lbcar.Name = "lbcar";
            this.lbcar.Size = new System.Drawing.Size(77, 12);
            this.lbcar.TabIndex = 45;
            this.lbcar.Text = "终端车牌号：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 537);
            this.Controls.Add(this.txtCarNO);
            this.Controls.Add(this.rdoASCII);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iptxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.porttxt);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.lbcar);
            this.Controls.Add(this.rtbChatIn);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sslbStatus;
        private System.Windows.Forms.RichTextBox rtbChatIn;
        private System.Windows.Forms.TextBox txtCarNO;
        private System.Windows.Forms.RadioButton rdoASCII;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox iptxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox porttxt;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label lbcar;
    }
}

