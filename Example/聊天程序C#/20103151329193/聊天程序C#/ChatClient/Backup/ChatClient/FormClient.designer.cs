namespace ChatClient
{
    partial class FormClient
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
            this.listBoxStatus = new System.Windows.Forms.ListBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxStatus
            // 
            this.listBoxStatus.FormattingEnabled = true;
            this.listBoxStatus.ItemHeight = 12;
            this.listBoxStatus.Location = new System.Drawing.Point(19, 20);
            this.listBoxStatus.Name = "listBoxStatus";
            this.listBoxStatus.Size = new System.Drawing.Size(360, 100);
            this.listBoxStatus.TabIndex = 0;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(306, 13);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(105, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "连接服务器";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(315, 21);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxSend);
            this.groupBox2.Controls.Add(this.buttonSend);
            this.groupBox2.Location = new System.Drawing.Point(15, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 55);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送信息";
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(19, 21);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(282, 21);
            this.textBoxSend.TabIndex = 2;
            this.textBoxSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSend_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxStatus);
            this.groupBox1.Location = new System.Drawing.Point(15, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 131);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态信息";
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 252);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormClient";
            this.Text = "FormClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClient_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxStatus;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxSend;
    }
}

