
namespace BusInformationManagementProgram_Input
{
    partial class Form1
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
            this.lblEnterBusNumber = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.cmbBxBusNumber = new System.Windows.Forms.ComboBox();
            this.Time = new System.Windows.Forms.Label();
            this.currentTime = new System.Windows.Forms.Label();
            this.date = new System.Windows.Forms.Label();
            this.currentDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblEnterBusNumber
            // 
            this.lblEnterBusNumber.AutoSize = true;
            this.lblEnterBusNumber.Location = new System.Drawing.Point(33, 62);
            this.lblEnterBusNumber.Name = "lblEnterBusNumber";
            this.lblEnterBusNumber.Size = new System.Drawing.Size(93, 13);
            this.lblEnterBusNumber.TabIndex = 1;
            this.lblEnterBusNumber.Text = "Enter Bus Number";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(182, 290);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Check In";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // cmbBxBusNumber
            // 
            this.cmbBxBusNumber.FormattingEnabled = true;
            this.cmbBxBusNumber.Location = new System.Drawing.Point(136, 59);
            this.cmbBxBusNumber.Name = "cmbBxBusNumber";
            this.cmbBxBusNumber.Size = new System.Drawing.Size(121, 21);
            this.cmbBxBusNumber.TabIndex = 3;
            this.cmbBxBusNumber.SelectedIndexChanged += new System.EventHandler(this.cmbBxBusNumber_SelectedIndexChanged);
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.Location = new System.Drawing.Point(36, 100);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(70, 13);
            this.Time.TabIndex = 4;
            this.Time.Text = "Current Time:";
            // 
            // currentTime
            // 
            this.currentTime.AutoSize = true;
            this.currentTime.Location = new System.Drawing.Point(113, 100);
            this.currentTime.Name = "currentTime";
            this.currentTime.Size = new System.Drawing.Size(34, 13);
            this.currentTime.TabIndex = 5;
            this.currentTime.Text = "00:00";
            // 
            // date
            // 
            this.date.AutoSize = true;
            this.date.Location = new System.Drawing.Point(39, 133);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(30, 13);
            this.date.TabIndex = 6;
            this.date.Text = "Date";
            // 
            // currentDate
            // 
            this.currentDate.AutoSize = true;
            this.currentDate.Location = new System.Drawing.Point(70, 133);
            this.currentDate.Name = "currentDate";
            this.currentDate.Size = new System.Drawing.Size(35, 13);
            this.currentDate.TabIndex = 7;
            this.currentDate.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 356);
            this.Controls.Add(this.currentDate);
            this.Controls.Add(this.date);
            this.Controls.Add(this.currentTime);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.cmbBxBusNumber);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.lblEnterBusNumber);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblEnterBusNumber;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ComboBox cmbBxBusNumber;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Label currentTime;
        private System.Windows.Forms.Label date;
        private System.Windows.Forms.Label currentDate;
    }
}

