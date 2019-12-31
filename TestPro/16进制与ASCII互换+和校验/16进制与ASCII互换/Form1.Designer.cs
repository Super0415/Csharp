namespace _16进制与ASCII互换
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
            this.tbASCII = new System.Windows.Forms.TextBox();
            this.tbHex = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbASCII = new System.Windows.Forms.Label();
            this.lbHex = new System.Windows.Forms.Label();
            this.lbDec = new System.Windows.Forms.Label();
            this.tbDec = new System.Windows.Forms.TextBox();
            this.tbPLC = new System.Windows.Forms.TextBox();
            this.lbPLCSum = new System.Windows.Forms.Label();
            this.btnPLCSum = new System.Windows.Forms.Button();
            this.tbPLCSum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbASCII
            // 
            this.tbASCII.Location = new System.Drawing.Point(12, 35);
            this.tbASCII.Multiline = true;
            this.tbASCII.Name = "tbASCII";
            this.tbASCII.Size = new System.Drawing.Size(159, 121);
            this.tbASCII.TabIndex = 0;
            // 
            // tbHex
            // 
            this.tbHex.Location = new System.Drawing.Point(291, 35);
            this.tbHex.Multiline = true;
            this.tbHex.Name = "tbHex";
            this.tbHex.Size = new System.Drawing.Size(159, 121);
            this.tbHex.TabIndex = 1;
            this.tbHex.Leave += new System.EventHandler(this.tbHexToDec__TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "---->";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(191, 108);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "<----";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbASCII
            // 
            this.lbASCII.AutoSize = true;
            this.lbASCII.Location = new System.Drawing.Point(12, 13);
            this.lbASCII.Name = "lbASCII";
            this.lbASCII.Size = new System.Drawing.Size(35, 12);
            this.lbASCII.TabIndex = 4;
            this.lbASCII.Text = "ASCII";
            // 
            // lbHex
            // 
            this.lbHex.AutoSize = true;
            this.lbHex.Location = new System.Drawing.Point(289, 13);
            this.lbHex.Name = "lbHex";
            this.lbHex.Size = new System.Drawing.Size(53, 12);
            this.lbHex.TabIndex = 5;
            this.lbHex.Text = "十六进制";
            // 
            // lbDec
            // 
            this.lbDec.AutoSize = true;
            this.lbDec.Location = new System.Drawing.Point(476, 13);
            this.lbDec.Name = "lbDec";
            this.lbDec.Size = new System.Drawing.Size(41, 12);
            this.lbDec.TabIndex = 6;
            this.lbDec.Text = "十进制";
            // 
            // tbDec
            // 
            this.tbDec.Location = new System.Drawing.Point(478, 35);
            this.tbDec.Multiline = true;
            this.tbDec.Name = "tbDec";
            this.tbDec.Size = new System.Drawing.Size(159, 121);
            this.tbDec.TabIndex = 7;
            this.tbDec.Leave += new System.EventHandler(this.tbDecToHex__TextChanged);
            // 
            // tbPLC
            // 
            this.tbPLC.Location = new System.Drawing.Point(12, 190);
            this.tbPLC.Multiline = true;
            this.tbPLC.Name = "tbPLC";
            this.tbPLC.Size = new System.Drawing.Size(280, 48);
            this.tbPLC.TabIndex = 8;
            // 
            // lbPLCSum
            // 
            this.lbPLCSum.AutoSize = true;
            this.lbPLCSum.Location = new System.Drawing.Point(12, 175);
            this.lbPLCSum.Name = "lbPLCSum";
            this.lbPLCSum.Size = new System.Drawing.Size(119, 12);
            this.lbPLCSum.TabIndex = 9;
            this.lbPLCSum.Text = "PLC和校验（包含02）";
            // 
            // btnPLCSum
            // 
            this.btnPLCSum.Location = new System.Drawing.Point(315, 202);
            this.btnPLCSum.Name = "btnPLCSum";
            this.btnPLCSum.Size = new System.Drawing.Size(75, 23);
            this.btnPLCSum.TabIndex = 10;
            this.btnPLCSum.Text = "---->";
            this.btnPLCSum.UseVisualStyleBackColor = true;
            this.btnPLCSum.Click += new System.EventHandler(this.btnPLCSum_Click);
            // 
            // tbPLCSum
            // 
            this.tbPLCSum.Location = new System.Drawing.Point(430, 190);
            this.tbPLCSum.Multiline = true;
            this.tbPLCSum.Name = "tbPLCSum";
            this.tbPLCSum.Size = new System.Drawing.Size(147, 48);
            this.tbPLCSum.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 250);
            this.Controls.Add(this.tbPLCSum);
            this.Controls.Add(this.btnPLCSum);
            this.Controls.Add(this.lbPLCSum);
            this.Controls.Add(this.tbPLC);
            this.Controls.Add(this.tbDec);
            this.Controls.Add(this.lbDec);
            this.Controls.Add(this.lbHex);
            this.Controls.Add(this.lbASCII);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbHex);
            this.Controls.Add(this.tbASCII);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbASCII;
        private System.Windows.Forms.TextBox tbHex;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lbASCII;
        private System.Windows.Forms.Label lbHex;
        private System.Windows.Forms.Label lbDec;
        private System.Windows.Forms.TextBox tbDec;
        private System.Windows.Forms.TextBox tbPLC;
        private System.Windows.Forms.Label lbPLCSum;
        private System.Windows.Forms.Button btnPLCSum;
        private System.Windows.Forms.TextBox tbPLCSum;
    }
}

