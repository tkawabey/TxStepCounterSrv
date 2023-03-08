namespace TxStepCounterSrv
{
    partial class MainForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.grpbxLog = new System.Windows.Forms.GroupBox();
            this.listBxLog = new System.Windows.Forms.ListBox();
            this.txtMyServerIP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textPortNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textStep = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textStatus = new System.Windows.Forms.TextBox();
            this.grpbxLog.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(255, 15);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // grpbxLog
            // 
            this.grpbxLog.Controls.Add(this.listBxLog);
            this.grpbxLog.Location = new System.Drawing.Point(14, 95);
            this.grpbxLog.Name = "grpbxLog";
            this.grpbxLog.Size = new System.Drawing.Size(343, 111);
            this.grpbxLog.TabIndex = 1;
            this.grpbxLog.TabStop = false;
            this.grpbxLog.Text = "Log";
            // 
            // listBxLog
            // 
            this.listBxLog.FormattingEnabled = true;
            this.listBxLog.ItemHeight = 12;
            this.listBxLog.Location = new System.Drawing.Point(6, 30);
            this.listBxLog.Name = "listBxLog";
            this.listBxLog.Size = new System.Drawing.Size(322, 64);
            this.listBxLog.TabIndex = 0;
            // 
            // txtMyServerIP
            // 
            this.txtMyServerIP.Location = new System.Drawing.Point(50, 18);
            this.txtMyServerIP.Name = "txtMyServerIP";
            this.txtMyServerIP.ReadOnly = true;
            this.txtMyServerIP.Size = new System.Drawing.Size(114, 19);
            this.txtMyServerIP.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMyServerIP);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textPortNumber);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Location = new System.Drawing.Point(12, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(345, 46);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "このPCの情報";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP Addr:";
            // 
            // textPortNumber
            // 
            this.textPortNumber.Location = new System.Drawing.Point(197, 16);
            this.textPortNumber.Name = "textPortNumber";
            this.textPortNumber.Size = new System.Drawing.Size(52, 19);
            this.textPortNumber.TabIndex = 1;
            this.textPortNumber.Text = "5001";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port :";
            // 
            // textStep
            // 
            this.textStep.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textStep.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textStep.ForeColor = System.Drawing.Color.White;
            this.textStep.Location = new System.Drawing.Point(46, 55);
            this.textStep.Name = "textStep";
            this.textStep.ReadOnly = true;
            this.textStep.Size = new System.Drawing.Size(105, 34);
            this.textStep.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Step";
            // 
            // textStatus
            // 
            this.textStatus.BackColor = System.Drawing.Color.White;
            this.textStatus.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textStatus.ForeColor = System.Drawing.Color.Black;
            this.textStatus.Location = new System.Drawing.Point(157, 55);
            this.textStatus.Name = "textStatus";
            this.textStatus.Size = new System.Drawing.Size(185, 34);
            this.textStatus.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 218);
            this.Controls.Add(this.textStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textStep);
            this.Controls.Add(this.grpbxLog);
            this.Controls.Add(this.groupBox2);
            this.Name = "MainForm";
            this.Text = "Step CounterServer";
            this.grpbxLog.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox grpbxLog;
        private System.Windows.Forms.ListBox listBxLog;
        private System.Windows.Forms.TextBox txtMyServerIP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textPortNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textStatus;
    }
}

