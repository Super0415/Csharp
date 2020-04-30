namespace tetris
{
    partial class FrmTetris
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbRun = new System.Windows.Forms.PictureBox();
            this.lbReady = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbScore = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btndeasil = new System.Windows.Forms.Button();
            this.btnCondtra = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbRun)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbRun
            // 
            this.pbRun.BackColor = System.Drawing.Color.Silver;
            this.pbRun.Location = new System.Drawing.Point(5, 0);
            this.pbRun.Name = "pbRun";
            this.pbRun.Size = new System.Drawing.Size(200, 300);
            this.pbRun.TabIndex = 0;
            this.pbRun.TabStop = false;
            this.pbRun.Paint += new System.Windows.Forms.PaintEventHandler(this.pbRun_Paint);
            // 
            // lbReady
            // 
            this.lbReady.BackColor = System.Drawing.Color.Silver;
            this.lbReady.Location = new System.Drawing.Point(18, 12);
            this.lbReady.Name = "lbReady";
            this.lbReady.Size = new System.Drawing.Size(100, 100);
            this.lbReady.TabIndex = 1;
            this.lbReady.Paint += new System.Windows.Forms.PaintEventHandler(this.lbReady_Paint);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(31, 144);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbScore);
            this.panel1.Controls.Add(this.btnSet);
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.lbReady);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(211, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 365);
            this.panel1.TabIndex = 3;
            // 
            // lbScore
            // 
            this.lbScore.AutoSize = true;
            this.lbScore.Location = new System.Drawing.Point(18, 288);
            this.lbScore.Name = "lbScore";
            this.lbScore.Size = new System.Drawing.Size(47, 12);
            this.lbScore.TabIndex = 5;
            this.lbScore.Text = "总分：0";
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(32, 239);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 4;
            this.btnSet.Text = "设置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(32, 191);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 3;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btndeasil
            // 
            this.btndeasil.Location = new System.Drawing.Point(103, 131);
            this.btndeasil.Name = "btndeasil";
            this.btndeasil.Size = new System.Drawing.Size(49, 23);
            this.btndeasil.TabIndex = 7;
            this.btndeasil.Text = "顺时针";
            this.btndeasil.UseVisualStyleBackColor = true;
            this.btndeasil.Click += new System.EventHandler(this.btndeasil_Click);
            // 
            // btnCondtra
            // 
            this.btnCondtra.Location = new System.Drawing.Point(27, 131);
            this.btnCondtra.Name = "btnCondtra";
            this.btnCondtra.Size = new System.Drawing.Size(54, 23);
            this.btnCondtra.TabIndex = 6;
            this.btnCondtra.Text = "逆时针";
            this.btnCondtra.UseVisualStyleBackColor = true;
            this.btnCondtra.Click += new System.EventHandler(this.btnCondtra_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(103, 50);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(38, 23);
            this.btnRight.TabIndex = 5;
            this.btnRight.Text = "向右";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(43, 50);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(38, 23);
            this.btnLeft.TabIndex = 4;
            this.btnLeft.Text = "向左";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(54, 90);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 3;
            this.btnDown.Text = "向下";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(54, 185);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 8;
            this.btnCheck.Text = "检查";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmTetris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 365);
            this.Controls.Add(this.pbRun);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btndeasil);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnCondtra);
            this.Controls.Add(this.btnRight);
            this.KeyPreview = true;
            this.Name = "FrmTetris";
            this.Text = "俄罗斯方块";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTetris_FormClosing);
            this.Load += new System.EventHandler(this.FrmTetris_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmTetris_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbRun)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbRun;
        private System.Windows.Forms.Label lbReady;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btndeasil;
        private System.Windows.Forms.Button btnCondtra;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Label lbScore;
        private System.Windows.Forms.Timer timer1;
    }
}