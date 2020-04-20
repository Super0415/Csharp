namespace AutoMessage
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbbCom = new System.Windows.Forms.ComboBox();
            this.cbbBaud = new System.Windows.Forms.ComboBox();
            this.lbCOM = new System.Windows.Forms.Label();
            this.lbBaud = new System.Windows.Forms.Label();
            this.tbRecode = new System.Windows.Forms.TextBox();
            this.pl1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIncrease = new System.Windows.Forms.Button();
            this.btnReduce = new System.Windows.Forms.Button();
            this.cbHex = new System.Windows.Forms.CheckBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btn14 = new System.Windows.Forms.Button();
            this.btn13 = new System.Windows.Forms.Button();
            this.btn12 = new System.Windows.Forms.Button();
            this.btn11 = new System.Windows.Forms.Button();
            this.btn10 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.cmsReName = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbSent1 = new System.Windows.Forms.TextBox();
            this.lbSent1 = new System.Windows.Forms.Label();
            this.tbReceive1 = new System.Windows.Forms.TextBox();
            this.lbReceive1 = new System.Windows.Forms.Label();
            this.pl1.SuspendLayout();
            this.cmsReName.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(382, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbbCom
            // 
            this.cbbCom.FormattingEnabled = true;
            this.cbbCom.Location = new System.Drawing.Point(47, 12);
            this.cbbCom.Name = "cbbCom";
            this.cbbCom.Size = new System.Drawing.Size(121, 20);
            this.cbbCom.TabIndex = 3;
            this.cbbCom.SelectedIndexChanged += new System.EventHandler(this.cbbCom_SelectedIndexChanged);
            // 
            // cbbBaud
            // 
            this.cbbBaud.FormattingEnabled = true;
            this.cbbBaud.Location = new System.Drawing.Point(235, 12);
            this.cbbBaud.Name = "cbbBaud";
            this.cbbBaud.Size = new System.Drawing.Size(121, 20);
            this.cbbBaud.TabIndex = 4;
            this.cbbBaud.SelectedIndexChanged += new System.EventHandler(this.cbbBaud_SelectedIndexChanged);
            // 
            // lbCOM
            // 
            this.lbCOM.AutoSize = true;
            this.lbCOM.Location = new System.Drawing.Point(12, 15);
            this.lbCOM.Name = "lbCOM";
            this.lbCOM.Size = new System.Drawing.Size(29, 12);
            this.lbCOM.TabIndex = 5;
            this.lbCOM.Text = "串口";
            // 
            // lbBaud
            // 
            this.lbBaud.AutoSize = true;
            this.lbBaud.Location = new System.Drawing.Point(188, 15);
            this.lbBaud.Name = "lbBaud";
            this.lbBaud.Size = new System.Drawing.Size(41, 12);
            this.lbBaud.TabIndex = 6;
            this.lbBaud.Text = "波特率";
            // 
            // tbRecode
            // 
            this.tbRecode.Location = new System.Drawing.Point(12, 39);
            this.tbRecode.Multiline = true;
            this.tbRecode.Name = "tbRecode";
            this.tbRecode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRecode.Size = new System.Drawing.Size(443, 155);
            this.tbRecode.TabIndex = 7;
            // 
            // pl1
            // 
            this.pl1.Controls.Add(this.label1);
            this.pl1.Controls.Add(this.btnIncrease);
            this.pl1.Controls.Add(this.btnReduce);
            this.pl1.Controls.Add(this.cbHex);
            this.pl1.Controls.Add(this.btnEnter);
            this.pl1.Controls.Add(this.btn14);
            this.pl1.Controls.Add(this.btn13);
            this.pl1.Controls.Add(this.btn12);
            this.pl1.Controls.Add(this.btn11);
            this.pl1.Controls.Add(this.btn10);
            this.pl1.Controls.Add(this.btn9);
            this.pl1.Controls.Add(this.btn8);
            this.pl1.Controls.Add(this.btn7);
            this.pl1.Controls.Add(this.btn6);
            this.pl1.Controls.Add(this.btn5);
            this.pl1.Controls.Add(this.btn4);
            this.pl1.Controls.Add(this.btn3);
            this.pl1.Controls.Add(this.btn2);
            this.pl1.Controls.Add(this.btn1);
            this.pl1.Controls.Add(this.tbSent1);
            this.pl1.Controls.Add(this.lbSent1);
            this.pl1.Controls.Add(this.tbReceive1);
            this.pl1.Controls.Add(this.lbReceive1);
            this.pl1.Location = new System.Drawing.Point(14, 200);
            this.pl1.Name = "pl1";
            this.pl1.Size = new System.Drawing.Size(441, 176);
            this.pl1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "接收次数：0";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnIncrease
            // 
            this.btnIncrease.Location = new System.Drawing.Point(245, 147);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(51, 23);
            this.btnIncrease.TabIndex = 21;
            this.btnIncrease.Tag = "";
            this.btnIncrease.Text = "+";
            this.btnIncrease.UseVisualStyleBackColor = true;
            this.btnIncrease.Click += new System.EventHandler(this.btnIncrease_Click);
            // 
            // btnReduce
            // 
            this.btnReduce.Location = new System.Drawing.Point(131, 147);
            this.btnReduce.Name = "btnReduce";
            this.btnReduce.Size = new System.Drawing.Size(51, 23);
            this.btnReduce.TabIndex = 20;
            this.btnReduce.Tag = "";
            this.btnReduce.Text = "-";
            this.btnReduce.UseVisualStyleBackColor = true;
            this.btnReduce.Click += new System.EventHandler(this.btnReduce_Click);
            // 
            // cbHex
            // 
            this.cbHex.AutoSize = true;
            this.cbHex.Location = new System.Drawing.Point(47, 12);
            this.cbHex.Name = "cbHex";
            this.cbHex.Size = new System.Drawing.Size(60, 16);
            this.cbHex.TabIndex = 19;
            this.cbHex.Text = "16进制";
            this.cbHex.UseVisualStyleBackColor = true;
            // 
            // btnEnter
            // 
            this.btnEnter.Enabled = false;
            this.btnEnter.Location = new System.Drawing.Point(387, 34);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(51, 48);
            this.btnEnter.TabIndex = 18;
            this.btnEnter.Tag = "";
            this.btnEnter.Text = "确认 修改";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btn14
            // 
            this.btn14.Location = new System.Drawing.Point(359, 118);
            this.btn14.Name = "btn14";
            this.btn14.Size = new System.Drawing.Size(51, 23);
            this.btn14.TabIndex = 17;
            this.btn14.Tag = "14";
            this.btn14.Text = "#14";
            this.btn14.UseVisualStyleBackColor = true;
            this.btn14.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn13
            // 
            this.btn13.Location = new System.Drawing.Point(302, 118);
            this.btn13.Name = "btn13";
            this.btn13.Size = new System.Drawing.Size(51, 23);
            this.btn13.TabIndex = 16;
            this.btn13.Tag = "13";
            this.btn13.Text = "#13";
            this.btn13.UseVisualStyleBackColor = true;
            this.btn13.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn12
            // 
            this.btn12.Location = new System.Drawing.Point(245, 118);
            this.btn12.Name = "btn12";
            this.btn12.Size = new System.Drawing.Size(51, 23);
            this.btn12.TabIndex = 15;
            this.btn12.Tag = "12";
            this.btn12.Text = "#12";
            this.btn12.UseVisualStyleBackColor = true;
            this.btn12.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn11
            // 
            this.btn11.Location = new System.Drawing.Point(188, 118);
            this.btn11.Name = "btn11";
            this.btn11.Size = new System.Drawing.Size(51, 23);
            this.btn11.TabIndex = 14;
            this.btn11.Tag = "11";
            this.btn11.Text = "#11";
            this.btn11.UseVisualStyleBackColor = true;
            this.btn11.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn10
            // 
            this.btn10.Location = new System.Drawing.Point(131, 118);
            this.btn10.Name = "btn10";
            this.btn10.Size = new System.Drawing.Size(51, 23);
            this.btn10.TabIndex = 13;
            this.btn10.Tag = "10";
            this.btn10.Text = "#10";
            this.btn10.UseVisualStyleBackColor = true;
            this.btn10.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn9
            // 
            this.btn9.Location = new System.Drawing.Point(74, 118);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(51, 23);
            this.btn9.TabIndex = 12;
            this.btn9.Tag = "9";
            this.btn9.Text = "#9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn8
            // 
            this.btn8.Location = new System.Drawing.Point(17, 118);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(51, 23);
            this.btn8.TabIndex = 11;
            this.btn8.Tag = "8";
            this.btn8.Text = "#8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn7
            // 
            this.btn7.Location = new System.Drawing.Point(359, 89);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(51, 23);
            this.btn7.TabIndex = 10;
            this.btn7.Tag = "7";
            this.btn7.Text = "#7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn6
            // 
            this.btn6.Location = new System.Drawing.Point(302, 89);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(51, 23);
            this.btn6.TabIndex = 9;
            this.btn6.Tag = "6";
            this.btn6.Text = "#6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn5
            // 
            this.btn5.Location = new System.Drawing.Point(245, 89);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(51, 23);
            this.btn5.TabIndex = 8;
            this.btn5.Tag = "5";
            this.btn5.Text = "#5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn4
            // 
            this.btn4.Location = new System.Drawing.Point(188, 89);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(51, 23);
            this.btn4.TabIndex = 7;
            this.btn4.Tag = "4";
            this.btn4.Text = "#4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn3
            // 
            this.btn3.Location = new System.Drawing.Point(131, 89);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(51, 23);
            this.btn3.TabIndex = 6;
            this.btn3.Tag = "3";
            this.btn3.Text = "#3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(74, 89);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(51, 23);
            this.btn2.TabIndex = 5;
            this.btn2.Tag = "2";
            this.btn2.Text = "#2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.SelectItem);
            // 
            // btn1
            // 
            this.btn1.ContextMenuStrip = this.cmsReName;
            this.btn1.Location = new System.Drawing.Point(17, 89);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(51, 23);
            this.btn1.TabIndex = 4;
            this.btn1.Tag = "1";
            this.btn1.Text = "#1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.SelectItem);
            // 
            // cmsReName
            // 
            this.cmsReName.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重命名ToolStripMenuItem});
            this.cmsReName.Name = "cmsReName";
            this.cmsReName.Size = new System.Drawing.Size(113, 26);
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.重命名ToolStripMenuItem.Text = "重命名";
            this.重命名ToolStripMenuItem.Click += new System.EventHandler(this.tsmiRename);
            // 
            // tbSent1
            // 
            this.tbSent1.Location = new System.Drawing.Point(50, 61);
            this.tbSent1.Name = "tbSent1";
            this.tbSent1.Size = new System.Drawing.Size(323, 21);
            this.tbSent1.TabIndex = 3;
            this.tbSent1.Enter += new System.EventHandler(this.Activa_Enter);
            // 
            // lbSent1
            // 
            this.lbSent1.AutoSize = true;
            this.lbSent1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lbSent1.Location = new System.Drawing.Point(3, 64);
            this.lbSent1.Name = "lbSent1";
            this.lbSent1.Size = new System.Drawing.Size(41, 12);
            this.lbSent1.TabIndex = 2;
            this.lbSent1.Text = "反馈：";
            // 
            // tbReceive1
            // 
            this.tbReceive1.Location = new System.Drawing.Point(50, 34);
            this.tbReceive1.Name = "tbReceive1";
            this.tbReceive1.Size = new System.Drawing.Size(323, 21);
            this.tbReceive1.TabIndex = 1;
            this.tbReceive1.Enter += new System.EventHandler(this.Activa_Enter);
            // 
            // lbReceive1
            // 
            this.lbReceive1.AutoSize = true;
            this.lbReceive1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lbReceive1.Location = new System.Drawing.Point(3, 37);
            this.lbReceive1.Name = "lbReceive1";
            this.lbReceive1.Size = new System.Drawing.Size(41, 12);
            this.lbReceive1.TabIndex = 0;
            this.lbReceive1.Text = "接收：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 378);
            this.Controls.Add(this.pl1);
            this.Controls.Add(this.tbRecode);
            this.Controls.Add(this.lbBaud);
            this.Controls.Add(this.lbCOM);
            this.Controls.Add(this.cbbBaud);
            this.Controls.Add(this.cbbCom);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pl1.ResumeLayout(false);
            this.pl1.PerformLayout();
            this.cmsReName.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbbCom;
        private System.Windows.Forms.ComboBox cbbBaud;
        private System.Windows.Forms.Label lbCOM;
        private System.Windows.Forms.Label lbBaud;
        private System.Windows.Forms.TextBox tbRecode;
        private System.Windows.Forms.Panel pl1;
        private System.Windows.Forms.TextBox tbReceive1;
        private System.Windows.Forms.Label lbReceive1;
        private System.Windows.Forms.TextBox tbSent1;
        private System.Windows.Forms.Label lbSent1;
        private System.Windows.Forms.Button btn14;
        private System.Windows.Forms.Button btn13;
        private System.Windows.Forms.Button btn12;
        private System.Windows.Forms.Button btn11;
        private System.Windows.Forms.Button btn10;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.CheckBox cbHex;
        private System.Windows.Forms.ContextMenuStrip cmsReName;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
        private System.Windows.Forms.Button btnIncrease;
        private System.Windows.Forms.Button btnReduce;
        private System.Windows.Forms.Label label1;
    }
}

