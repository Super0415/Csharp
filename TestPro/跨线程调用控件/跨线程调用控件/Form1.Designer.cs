namespace 跨线程调用控件
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.开启线程 = new System.Windows.Forms.Button();
            this.关闭线程 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 59);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(360, 400);
            this.textBox1.TabIndex = 0;
            // 
            // 开启线程
            // 
            this.开启线程.Location = new System.Drawing.Point(62, 21);
            this.开启线程.Name = "开启线程";
            this.开启线程.Size = new System.Drawing.Size(75, 23);
            this.开启线程.TabIndex = 1;
            this.开启线程.Text = "开启线程";
            this.开启线程.UseVisualStyleBackColor = true;
            this.开启线程.Click += new System.EventHandler(this.开启线程_Click);
            // 
            // 关闭线程
            // 
            this.关闭线程.Location = new System.Drawing.Point(206, 21);
            this.关闭线程.Name = "关闭线程";
            this.关闭线程.Size = new System.Drawing.Size(75, 23);
            this.关闭线程.TabIndex = 2;
            this.关闭线程.Text = "关闭线程";
            this.关闭线程.UseVisualStyleBackColor = true;
            this.关闭线程.Click += new System.EventHandler(this.关闭线程_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 462);
            this.Controls.Add(this.关闭线程);
            this.Controls.Add(this.开启线程);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button 开启线程;
        private System.Windows.Forms.Button 关闭线程;
        private System.Windows.Forms.TextBox textBox1;
    }
}

