namespace TcpClientTest
{
    partial class TcpClientForm
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
            this.rTxtBox_Msg = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rTxtBox_Msg
            // 
            this.rTxtBox_Msg.BackColor = System.Drawing.SystemColors.Info;
            this.rTxtBox_Msg.Location = new System.Drawing.Point(90, 100);
            this.rTxtBox_Msg.Name = "rTxtBox_Msg";
            this.rTxtBox_Msg.Size = new System.Drawing.Size(400, 200);
            this.rTxtBox_Msg.TabIndex = 0;
            this.rTxtBox_Msg.Text = "";
            // 
            // TcpClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.rTxtBox_Msg);
            this.Name = "TcpClientForm";
            this.Text = "TCP客户端";
            this.Load += new System.EventHandler(this.TcpClientForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rTxtBox_Msg;
    }
}

