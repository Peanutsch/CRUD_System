namespace CRUD_System
{
    partial class AbsenceDueIllnessForm
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
            this.txtDayCallInSick = new TextBox();
            this.txtRecovery = new TextBox();
            this.btnCallInSick = new Button();
            this.btnRecovery = new Button();
            this.btnCancel = new Button();
            this.btnApply = new Button();
            this.txtAlias = new TextBox();
            this.SuspendLayout();
            // 
            // txtDayCallInSick
            // 
            this.txtDayCallInSick.Enabled = false;
            this.txtDayCallInSick.ForeColor = SystemColors.WindowText;
            this.txtDayCallInSick.Location = new Point(69, 121);
            this.txtDayCallInSick.Name = "txtDayCallInSick";
            this.txtDayCallInSick.PlaceholderText = "DD-MM-YYYY";
            this.txtDayCallInSick.Size = new Size(141, 26);
            this.txtDayCallInSick.TabIndex = 1;
            this.txtDayCallInSick.TextAlign = HorizontalAlignment.Center;
            // 
            // txtRecovery
            // 
            this.txtRecovery.Enabled = false;
            this.txtRecovery.ForeColor = SystemColors.WindowText;
            this.txtRecovery.Location = new Point(232, 119);
            this.txtRecovery.Name = "txtRecovery";
            this.txtRecovery.PlaceholderText = "DD-MM-YYYY";
            this.txtRecovery.Size = new Size(141, 26);
            this.txtRecovery.TabIndex = 2;
            this.txtRecovery.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCallInSick
            // 
            this.btnCallInSick.BackColor = SystemColors.ActiveCaption;
            this.btnCallInSick.Location = new Point(69, 83);
            this.btnCallInSick.Name = "btnCallInSick";
            this.btnCallInSick.Size = new Size(141, 30);
            this.btnCallInSick.TabIndex = 4;
            this.btnCallInSick.Text = "Call in Sick";
            this.btnCallInSick.UseVisualStyleBackColor = false;
            // 
            // btnRecovery
            // 
            this.btnRecovery.BackColor = SystemColors.ActiveCaption;
            this.btnRecovery.Location = new Point(232, 83);
            this.btnRecovery.Name = "btnRecovery";
            this.btnRecovery.Size = new Size(141, 30);
            this.btnRecovery.TabIndex = 5;
            this.btnRecovery.Text = "Recovery";
            this.btnRecovery.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(232, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(81, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += this.btnCancel_Click;
            // 
            // btnApply
            // 
            this.btnApply.Location = new Point(129, 153);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(81, 30);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // txtAlias
            // 
            this.txtAlias.Enabled = false;
            this.txtAlias.Location = new Point(69, 26);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.PlaceholderText = "Alias";
            this.txtAlias.Size = new Size(100, 26);
            this.txtAlias.TabIndex = 8;
            this.txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // AbsenceDueIllnessForm
            // 
            this.AutoScaleDimensions = new SizeF(10F, 18F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.ClientSize = new Size(448, 215);
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRecovery);
            this.Controls.Add(this.btnCallInSick);
            this.Controls.Add(this.txtRecovery);
            this.Controls.Add(this.txtDayCallInSick);
            Load += this.Form_Load;
            this.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Margin = new Padding(4);
            this.Name = "AbsenceDueIllnessForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Absence due Illness";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        public TextBox txtDayCallInSick;
        public TextBox txtRecovery;
        public Button btnCallInSick;
        public Button btnRecovery;
        public Button btnCancel;
        public Button btnApply;
        public TextBox txtAlias;
    }
}