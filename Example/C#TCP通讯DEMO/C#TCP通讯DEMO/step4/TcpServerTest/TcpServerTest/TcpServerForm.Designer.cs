﻿namespace TcpServerTest
{
    partial class TcpServerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Ip = new System.Windows.Forms.TextBox();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.btn_Listen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rTxtBox_Msg
            // 
            this.rTxtBox_Msg.BackColor = System.Drawing.SystemColors.Info;
            this.rTxtBox_Msg.Location = new System.Drawing.Point(90, 90);
            this.rTxtBox_Msg.Name = "rTxtBox_Msg";
            this.rTxtBox_Msg.Size = new System.Drawing.Size(400, 200);
            this.rTxtBox_Msg.TabIndex = 0;
            this.rTxtBox_Msg.Text = "";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(90, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(300, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_Ip
            // 
            this.textBox_Ip.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Ip.Location = new System.Drawing.Point(126, 40);
            this.textBox_Ip.Multiline = true;
            this.textBox_Ip.Name = "textBox_Ip";
            this.textBox_Ip.Size = new System.Drawing.Size(160, 26);
            this.textBox_Ip.TabIndex = 3;
            // 
            // textBox_Port
            // 
            this.textBox_Port.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Port.Location = new System.Drawing.Point(339, 40);
            this.textBox_Port.Multiline = true;
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(60, 26);
            this.textBox_Port.TabIndex = 4;
            // 
            // btn_Listen
            // 
            this.btn_Listen.BackColor = System.Drawing.Color.Coral;
            this.btn_Listen.Location = new System.Drawing.Point(411, 40);
            this.btn_Listen.Name = "btn_Listen";
            this.btn_Listen.Size = new System.Drawing.Size(75, 26);
            this.btn_Listen.TabIndex = 5;
            this.btn_Listen.Text = "启动监听";
            this.btn_Listen.UseVisualStyleBackColor = false;
            this.btn_Listen.Click += new System.EventHandler(this.btn_Listen_Click);
            // 
            // TcpServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.btn_Listen);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.textBox_Ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rTxtBox_Msg);
            this.Name = "TcpServerForm";
            this.Text = "TCP服务端";
            this.Load += new System.EventHandler(this.TcpServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rTxtBox_Msg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Ip;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Button btn_Listen;
    }
}

