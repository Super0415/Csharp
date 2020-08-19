namespace WindowsFormsApplication1
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clnNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnComType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnJobNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnEquip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnEquipNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbControl = new System.Windows.Forms.GroupBox();
            this.btnUSBRead = new System.Windows.Forms.Button();
            this.btnUSBWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUSB = new System.Windows.Forms.Button();
            this.btnNFC = new System.Windows.Forms.Button();
            this.btnHard = new System.Windows.Forms.Button();
            this.timerReflash = new System.Windows.Forms.Timer(this.components);
            this.btnUSBClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnNum,
            this.clnComType,
            this.clnName,
            this.clnSex,
            this.clnAge,
            this.clnBranch,
            this.clnPosition,
            this.clnLevel,
            this.clnJobNum,
            this.clnDate,
            this.clnEquip,
            this.clnEquipNum});
            this.dataGridView1.Location = new System.Drawing.Point(3, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(775, 369);
            this.dataGridView1.TabIndex = 5;
            // 
            // clnNum
            // 
            this.clnNum.HeaderText = "序号";
            this.clnNum.Name = "clnNum";
            this.clnNum.ReadOnly = true;
            this.clnNum.Width = 53;
            // 
            // clnComType
            // 
            this.clnComType.HeaderText = "通讯类型";
            this.clnComType.Name = "clnComType";
            this.clnComType.ReadOnly = true;
            this.clnComType.Width = 77;
            // 
            // clnName
            // 
            this.clnName.HeaderText = "姓名";
            this.clnName.Name = "clnName";
            this.clnName.Width = 53;
            // 
            // clnSex
            // 
            this.clnSex.HeaderText = "性别";
            this.clnSex.Name = "clnSex";
            this.clnSex.Width = 53;
            // 
            // clnAge
            // 
            this.clnAge.HeaderText = "年龄";
            this.clnAge.Name = "clnAge";
            this.clnAge.Width = 53;
            // 
            // clnBranch
            // 
            this.clnBranch.HeaderText = "部门";
            this.clnBranch.Name = "clnBranch";
            this.clnBranch.Width = 53;
            // 
            // clnPosition
            // 
            this.clnPosition.HeaderText = "职位";
            this.clnPosition.Name = "clnPosition";
            this.clnPosition.Width = 53;
            // 
            // clnLevel
            // 
            this.clnLevel.HeaderText = "权限";
            this.clnLevel.Name = "clnLevel";
            this.clnLevel.Width = 53;
            // 
            // clnJobNum
            // 
            this.clnJobNum.HeaderText = "工号";
            this.clnJobNum.Name = "clnJobNum";
            this.clnJobNum.Width = 53;
            // 
            // clnDate
            // 
            this.clnDate.HeaderText = "登记日期";
            this.clnDate.Name = "clnDate";
            this.clnDate.Width = 77;
            // 
            // clnEquip
            // 
            this.clnEquip.HeaderText = "设备类型";
            this.clnEquip.Name = "clnEquip";
            this.clnEquip.Width = 77;
            // 
            // clnEquipNum
            // 
            this.clnEquipNum.HeaderText = "设备编号";
            this.clnEquipNum.Name = "clnEquipNum";
            this.clnEquipNum.Width = 77;
            // 
            // gbControl
            // 
            this.gbControl.Controls.Add(this.btnUSBClear);
            this.gbControl.Controls.Add(this.btnUSBRead);
            this.gbControl.Controls.Add(this.btnUSBWrite);
            this.gbControl.Controls.Add(this.btnRead);
            this.gbControl.Controls.Add(this.btnWrite);
            this.gbControl.Controls.Add(this.panel1);
            this.gbControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbControl.Location = new System.Drawing.Point(781, 0);
            this.gbControl.Name = "gbControl";
            this.gbControl.Size = new System.Drawing.Size(157, 400);
            this.gbControl.TabIndex = 7;
            this.gbControl.TabStop = false;
            this.gbControl.Text = "控制面板";
            // 
            // btnUSBRead
            // 
            this.btnUSBRead.Location = new System.Drawing.Point(41, 225);
            this.btnUSBRead.Name = "btnUSBRead";
            this.btnUSBRead.Size = new System.Drawing.Size(75, 23);
            this.btnUSBRead.TabIndex = 9;
            this.btnUSBRead.Text = "读取USBkey";
            this.btnUSBRead.UseVisualStyleBackColor = true;
            this.btnUSBRead.Click += new System.EventHandler(this.btnUSBRead_Click);
            // 
            // btnUSBWrite
            // 
            this.btnUSBWrite.Location = new System.Drawing.Point(41, 196);
            this.btnUSBWrite.Name = "btnUSBWrite";
            this.btnUSBWrite.Size = new System.Drawing.Size(75, 23);
            this.btnUSBWrite.TabIndex = 8;
            this.btnUSBWrite.Text = "写入USBkey";
            this.btnUSBWrite.UseVisualStyleBackColor = true;
            this.btnUSBWrite.Click += new System.EventHandler(this.btnUSBWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(41, 143);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 7;
            this.btnRead.Text = "读取NFC";
            this.btnRead.UseVisualStyleBackColor = true;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(41, 114);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 6;
            this.btnWrite.Text = "写入NFC";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
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
            // btnUSB
            // 
            this.btnUSB.Location = new System.Drawing.Point(0, 5);
            this.btnUSB.Name = "btnUSB";
            this.btnUSB.Size = new System.Drawing.Size(30, 30);
            this.btnUSB.TabIndex = 0;
            this.btnUSB.UseVisualStyleBackColor = true;
            this.btnUSB.Click += new System.EventHandler(this.btnUSB_Click);
            // 
            // btnNFC
            // 
            this.btnNFC.Location = new System.Drawing.Point(100, 5);
            this.btnNFC.Name = "btnNFC";
            this.btnNFC.Size = new System.Drawing.Size(30, 30);
            this.btnNFC.TabIndex = 2;
            this.btnNFC.UseVisualStyleBackColor = true;
            // 
            // btnHard
            // 
            this.btnHard.Location = new System.Drawing.Point(50, 5);
            this.btnHard.Name = "btnHard";
            this.btnHard.Size = new System.Drawing.Size(30, 30);
            this.btnHard.TabIndex = 1;
            this.btnHard.UseVisualStyleBackColor = true;
            // 
            // timerReflash
            // 
            this.timerReflash.Enabled = true;
            this.timerReflash.Tick += new System.EventHandler(this.timerReflash_Tick);
            // 
            // btnUSBClear
            // 
            this.btnUSBClear.Location = new System.Drawing.Point(41, 254);
            this.btnUSBClear.Name = "btnUSBClear";
            this.btnUSBClear.Size = new System.Drawing.Size(75, 23);
            this.btnUSBClear.TabIndex = 10;
            this.btnUSBClear.Text = "清空USBkey";
            this.btnUSBClear.UseVisualStyleBackColor = true;
            this.btnUSBClear.Click += new System.EventHandler(this.btnUSBClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 400);
            this.Controls.Add(this.gbControl);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbControl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnComType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnJobNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnEquip;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnEquipNum;
        private System.Windows.Forms.GroupBox gbControl;
        private System.Windows.Forms.Button btnUSBRead;
        private System.Windows.Forms.Button btnUSBWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUSB;
        private System.Windows.Forms.Button btnNFC;
        private System.Windows.Forms.Button btnHard;
        private System.Windows.Forms.Timer timerReflash;
        private System.Windows.Forms.Button btnUSBClear;
    }
}

