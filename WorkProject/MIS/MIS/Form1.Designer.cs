namespace MIS
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
            this.btnUSB = new System.Windows.Forms.Button();
            this.btnHard = new System.Windows.Forms.Button();
            this.btnNFC = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.gbControl = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUSB
            // 
            this.btnUSB.Location = new System.Drawing.Point(0, 5);
            this.btnUSB.Name = "btnUSB";
            this.btnUSB.Size = new System.Drawing.Size(30, 30);
            this.btnUSB.TabIndex = 0;
            this.btnUSB.UseVisualStyleBackColor = true;
            // 
            // btnHard
            // 
            this.btnHard.Location = new System.Drawing.Point(50, 5);
            this.btnHard.Name = "btnHard";
            this.btnHard.Size = new System.Drawing.Size(30, 30);
            this.btnHard.TabIndex = 1;
            this.btnHard.UseVisualStyleBackColor = true;
            // 
            // btnNFC
            // 
            this.btnNFC.Location = new System.Drawing.Point(100, 5);
            this.btnNFC.Name = "btnNFC";
            this.btnNFC.Size = new System.Drawing.Size(30, 30);
            this.btnNFC.TabIndex = 2;
            this.btnNFC.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(771, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUSB);
            this.panel1.Controls.Add(this.btnNFC);
            this.panel1.Controls.Add(this.btnHard);
            this.panel1.Location = new System.Drawing.Point(6, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 40);
            this.panel1.TabIndex = 4;
            // 
            // gbInfo
            // 
            this.gbInfo.Location = new System.Drawing.Point(2, 28);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(608, 386);
            this.gbInfo.TabIndex = 5;
            this.gbInfo.TabStop = false;
            // 
            // gbControl
            // 
            this.gbControl.Controls.Add(this.panel1);
            this.gbControl.Location = new System.Drawing.Point(614, 28);
            this.gbControl.Name = "gbControl";
            this.gbControl.Size = new System.Drawing.Size(157, 386);
            this.gbControl.TabIndex = 6;
            this.gbControl.TabStop = false;
            this.gbControl.Text = "控制面板";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 416);
            this.Controls.Add(this.gbControl);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "人员信息管理系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUSB;
        private System.Windows.Forms.Button btnHard;
        private System.Windows.Forms.Button btnNFC;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.GroupBox gbControl;
    }
}

