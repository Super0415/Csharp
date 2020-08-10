namespace usbtest
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btn_connect = new System.Windows.Forms.Button();
			this.btn_search = new System.Windows.Forms.Button();
			this.cb_devices = new System.Windows.Forms.ComboBox();
			this.tb_vid = new System.Windows.Forms.TextBox();
			this.tb_pid = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lbl_status = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.btn_clearCount = new System.Windows.Forms.Button();
			this.btn_clearInfo = new System.Windows.Forms.Button();
			this.tb_info = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.btn_stopRecv = new System.Windows.Forms.Button();
			this.tb_recv = new System.Windows.Forms.TextBox();
			this.btn_clearRecv = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.pBar1 = new System.Windows.Forms.ProgressBar();
			this.tb_send = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tb_ms = new System.Windows.Forms.TextBox();
			this.btn_sends = new System.Windows.Forms.Button();
			this.btn_send = new System.Windows.Forms.Button();
			this.btn_clearSend = new System.Windows.Forms.Button();
			this.timerSends = new System.Windows.Forms.Timer(this.components);
			this.timerRecv = new System.Windows.Forms.Timer(this.components);
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.pictureBox1);
			this.groupBox3.Controls.Add(this.btn_connect);
			this.groupBox3.Controls.Add(this.btn_search);
			this.groupBox3.Controls.Add(this.cb_devices);
			this.groupBox3.Controls.Add(this.tb_vid);
			this.groupBox3.Controls.Add(this.tb_pid);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.lbl_status);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.groupBox3.Location = new System.Drawing.Point(12, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(236, 273);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "USB配置";
			// 
			// pictureBox1
			// 
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(49, 189);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// btn_connect
			// 
			this.btn_connect.ForeColor = System.Drawing.Color.Black;
			this.btn_connect.Location = new System.Drawing.Point(106, 222);
			this.btn_connect.Name = "btn_connect";
			this.btn_connect.Size = new System.Drawing.Size(75, 23);
			this.btn_connect.TabIndex = 6;
			this.btn_connect.Text = "打开";
			this.btn_connect.UseVisualStyleBackColor = true;
			this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
			// 
			// btn_search
			// 
			this.btn_search.Location = new System.Drawing.Point(106, 184);
			this.btn_search.Name = "btn_search";
			this.btn_search.Size = new System.Drawing.Size(75, 23);
			this.btn_search.TabIndex = 1;
			this.btn_search.Text = "搜索";
			this.btn_search.UseVisualStyleBackColor = true;
			this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
			// 
			// cb_devices
			// 
			this.cb_devices.FormattingEnabled = true;
			this.cb_devices.Location = new System.Drawing.Point(49, 68);
			this.cb_devices.Name = "cb_devices";
			this.cb_devices.Size = new System.Drawing.Size(132, 20);
			this.cb_devices.TabIndex = 5;
			this.cb_devices.SelectedIndexChanged += new System.EventHandler(this.cb_devices_SelectedIndexChanged);
			// 
			// tb_vid
			// 
			this.tb_vid.Location = new System.Drawing.Point(81, 145);
			this.tb_vid.Name = "tb_vid";
			this.tb_vid.ReadOnly = true;
			this.tb_vid.Size = new System.Drawing.Size(100, 21);
			this.tb_vid.TabIndex = 4;
			// 
			// tb_pid
			// 
			this.tb_pid.Location = new System.Drawing.Point(81, 112);
			this.tb_pid.Name = "tb_pid";
			this.tb_pid.ReadOnly = true;
			this.tb_pid.Size = new System.Drawing.Size(100, 21);
			this.tb_pid.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(47, 148);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "VID:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(47, 117);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "PID:";
			// 
			// lbl_status
			// 
			this.lbl_status.AutoSize = true;
			this.lbl_status.Location = new System.Drawing.Point(47, 37);
			this.lbl_status.Name = "lbl_status";
			this.lbl_status.Size = new System.Drawing.Size(71, 12);
			this.lbl_status.TabIndex = 0;
			this.lbl_status.Text = "初始化中...";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.checkBox2);
			this.groupBox4.Controls.Add(this.btn_clearCount);
			this.groupBox4.Controls.Add(this.btn_clearInfo);
			this.groupBox4.Controls.Add(this.tb_info);
			this.groupBox4.Location = new System.Drawing.Point(12, 290);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(236, 254);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "信息";
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(6, 229);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(48, 16);
			this.checkBox2.TabIndex = 8;
			this.checkBox2.Text = "停止";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// btn_clearCount
			// 
			this.btn_clearCount.Location = new System.Drawing.Point(74, 224);
			this.btn_clearCount.Name = "btn_clearCount";
			this.btn_clearCount.Size = new System.Drawing.Size(75, 23);
			this.btn_clearCount.TabIndex = 7;
			this.btn_clearCount.Text = "重新计数";
			this.btn_clearCount.UseVisualStyleBackColor = true;
			this.btn_clearCount.Click += new System.EventHandler(this.btn_clearCount_Click);
			// 
			// btn_clearInfo
			// 
			this.btn_clearInfo.Location = new System.Drawing.Point(155, 225);
			this.btn_clearInfo.Name = "btn_clearInfo";
			this.btn_clearInfo.Size = new System.Drawing.Size(75, 23);
			this.btn_clearInfo.TabIndex = 6;
			this.btn_clearInfo.Text = "清空";
			this.btn_clearInfo.UseVisualStyleBackColor = true;
			this.btn_clearInfo.Click += new System.EventHandler(this.btn_clearInfo_Click);
			// 
			// tb_info
			// 
			this.tb_info.AllowDrop = true;
			this.tb_info.Location = new System.Drawing.Point(6, 20);
			this.tb_info.Multiline = true;
			this.tb_info.Name = "tb_info";
			this.tb_info.ReadOnly = true;
			this.tb_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tb_info.Size = new System.Drawing.Size(224, 198);
			this.tb_info.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.checkBox1);
			this.groupBox2.Controls.Add(this.btn_stopRecv);
			this.groupBox2.Controls.Add(this.tb_recv);
			this.groupBox2.Controls.Add(this.btn_clearRecv);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(263, 14);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(640, 271);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "接收数据(提示：双击保存数据)";
			// 
			// checkBox1
			// 
			this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(385, 247);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 16);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "循环显示";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.Click += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// btn_stopRecv
			// 
			this.btn_stopRecv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_stopRecv.Location = new System.Drawing.Point(468, 243);
			this.btn_stopRecv.Name = "btn_stopRecv";
			this.btn_stopRecv.Size = new System.Drawing.Size(75, 23);
			this.btn_stopRecv.TabIndex = 4;
			this.btn_stopRecv.Text = "停止接收";
			this.btn_stopRecv.UseVisualStyleBackColor = true;
			this.btn_stopRecv.Click += new System.EventHandler(this.btn_stopRecv_Click);
			// 
			// tb_recv
			// 
			this.tb_recv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_recv.Location = new System.Drawing.Point(16, 20);
			this.tb_recv.Multiline = true;
			this.tb_recv.Name = "tb_recv";
			this.tb_recv.ReadOnly = true;
			this.tb_recv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tb_recv.Size = new System.Drawing.Size(608, 217);
			this.tb_recv.TabIndex = 1;
			this.tb_recv.DoubleClick += new System.EventHandler(this.tb_recv_DoubleClick);
			// 
			// btn_clearRecv
			// 
			this.btn_clearRecv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_clearRecv.Location = new System.Drawing.Point(549, 243);
			this.btn_clearRecv.Name = "btn_clearRecv";
			this.btn_clearRecv.Size = new System.Drawing.Size(75, 23);
			this.btn_clearRecv.TabIndex = 0;
			this.btn_clearRecv.Text = "清空";
			this.btn_clearRecv.UseVisualStyleBackColor = true;
			this.btn_clearRecv.Click += new System.EventHandler(this.btn_clearRecv_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.pBar1);
			this.groupBox1.Controls.Add(this.tb_send);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tb_ms);
			this.groupBox1.Controls.Add(this.btn_sends);
			this.groupBox1.Controls.Add(this.btn_send);
			this.groupBox1.Controls.Add(this.btn_clearSend);
			this.groupBox1.Location = new System.Drawing.Point(263, 291);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(640, 255);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "发送数据（提示：双击发送图片或文件）";
			// 
			// pBar1
			// 
			this.pBar1.Location = new System.Drawing.Point(303, 233);
			this.pBar1.Name = "pBar1";
			this.pBar1.Size = new System.Drawing.Size(180, 10);
			this.pBar1.TabIndex = 6;
			this.pBar1.Visible = false;
			// 
			// tb_send
			// 
			this.tb_send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_send.Location = new System.Drawing.Point(16, 21);
			this.tb_send.Multiline = true;
			this.tb_send.Name = "tb_send";
			this.tb_send.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tb_send.Size = new System.Drawing.Size(608, 198);
			this.tb_send.TabIndex = 2;
			this.tb_send.Text = "00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 " +
    "27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53" +
    " 54 55 56 57 58 59 60 61 62 63 ";
			this.tb_send.DoubleClick += new System.EventHandler(this.tb_send_DoubleClick);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(245, 233);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "ms";
			// 
			// tb_ms
			// 
			this.tb_ms.Location = new System.Drawing.Point(182, 227);
			this.tb_ms.Name = "tb_ms";
			this.tb_ms.Size = new System.Drawing.Size(61, 21);
			this.tb_ms.TabIndex = 4;
			this.tb_ms.Text = "1000";
			// 
			// btn_sends
			// 
			this.btn_sends.Location = new System.Drawing.Point(97, 226);
			this.btn_sends.Name = "btn_sends";
			this.btn_sends.Size = new System.Drawing.Size(75, 23);
			this.btn_sends.TabIndex = 3;
			this.btn_sends.Text = "周期发送";
			this.btn_sends.UseVisualStyleBackColor = true;
			this.btn_sends.Click += new System.EventHandler(this.btn_sends_Click);
			// 
			// btn_send
			// 
			this.btn_send.Location = new System.Drawing.Point(16, 225);
			this.btn_send.Name = "btn_send";
			this.btn_send.Size = new System.Drawing.Size(75, 23);
			this.btn_send.TabIndex = 2;
			this.btn_send.Text = "发送";
			this.btn_send.UseVisualStyleBackColor = true;
			this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
			// 
			// btn_clearSend
			// 
			this.btn_clearSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_clearSend.Location = new System.Drawing.Point(549, 226);
			this.btn_clearSend.Name = "btn_clearSend";
			this.btn_clearSend.Size = new System.Drawing.Size(75, 23);
			this.btn_clearSend.TabIndex = 1;
			this.btn_clearSend.Text = "清空";
			this.btn_clearSend.UseVisualStyleBackColor = true;
			this.btn_clearSend.Click += new System.EventHandler(this.btn_clearSend_Click);
			// 
			// timerSends
			// 
			this.timerSends.Tick += new System.EventHandler(this.timerSends_Tick);
			// 
			// timerRecv
			// 
			this.timerRecv.Tick += new System.EventHandler(this.timerRecv_Tick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(920, 550);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Name = "Form1";
			this.Text = "USB调试助手";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btn_connect;
		private System.Windows.Forms.Button btn_search;
		private System.Windows.Forms.ComboBox cb_devices;
		private System.Windows.Forms.TextBox tb_vid;
		private System.Windows.Forms.TextBox tb_pid;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lbl_status;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btn_clearCount;
		private System.Windows.Forms.Button btn_clearInfo;
		private System.Windows.Forms.TextBox tb_info;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button btn_stopRecv;
		private System.Windows.Forms.TextBox tb_recv;
		private System.Windows.Forms.Button btn_clearRecv;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ProgressBar pBar1;
		private System.Windows.Forms.TextBox tb_send;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tb_ms;
		private System.Windows.Forms.Button btn_sends;
		private System.Windows.Forms.Button btn_send;
		private System.Windows.Forms.Button btn_clearSend;
		private System.Windows.Forms.Timer timerSends;
		private System.Windows.Forms.Timer timerRecv;
		private System.Windows.Forms.CheckBox checkBox2;
	}
}

