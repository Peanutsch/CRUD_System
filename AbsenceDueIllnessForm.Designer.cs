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
            txtDayCallInSick = new TextBox();
            txtRecovery = new TextBox();
            btnCallInSick = new Button();
            btnRecovery = new Button();
            btnCancel = new Button();
            btnApply = new Button();
            txtAlias = new TextBox();
            txtTotalDays = new TextBox();
            comboBoxProcent = new ComboBox();
            SuspendLayout();
            // 
            // txtDayCallInSick
            // 
            txtDayCallInSick.Enabled = false;
            txtDayCallInSick.ForeColor = SystemColors.WindowText;
            txtDayCallInSick.Location = new Point(69, 119);
            txtDayCallInSick.Name = "txtDayCallInSick";
            txtDayCallInSick.PlaceholderText = "Day 1";
            txtDayCallInSick.Size = new Size(141, 26);
            txtDayCallInSick.TabIndex = 1;
            txtDayCallInSick.TextAlign = HorizontalAlignment.Center;
            // 
            // txtRecovery
            // 
            txtRecovery.Enabled = false;
            txtRecovery.ForeColor = SystemColors.WindowText;
            txtRecovery.Location = new Point(232, 119);
            txtRecovery.Name = "txtRecovery";
            txtRecovery.PlaceholderText = "Recovered";
            txtRecovery.Size = new Size(141, 26);
            txtRecovery.TabIndex = 2;
            txtRecovery.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCallInSick
            // 
            btnCallInSick.BackColor = SystemColors.ActiveCaption;
            btnCallInSick.Location = new Point(69, 83);
            btnCallInSick.Name = "btnCallInSick";
            btnCallInSick.Size = new Size(141, 30);
            btnCallInSick.TabIndex = 4;
            btnCallInSick.Text = "Call in Sick";
            btnCallInSick.UseVisualStyleBackColor = false;
            // 
            // btnRecovery
            // 
            btnRecovery.BackColor = SystemColors.ActiveCaption;
            btnRecovery.Location = new Point(232, 83);
            btnRecovery.Name = "btnRecovery";
            btnRecovery.Size = new Size(141, 30);
            btnRecovery.TabIndex = 5;
            btnRecovery.Text = "Recovery";
            btnRecovery.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(232, 153);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(81, 30);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(129, 153);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(81, 30);
            btnApply.TabIndex = 7;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // txtAlias
            // 
            txtAlias.Enabled = false;
            txtAlias.Location = new Point(69, 26);
            txtAlias.Name = "txtAlias";
            txtAlias.PlaceholderText = "Alias";
            txtAlias.Size = new Size(100, 26);
            txtAlias.TabIndex = 8;
            txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtTotalDays
            // 
            txtTotalDays.Enabled = false;
            txtTotalDays.ForeColor = SystemColors.WindowText;
            txtTotalDays.Location = new Point(541, 119);
            txtTotalDays.Name = "txtTotalDays";
            txtTotalDays.PlaceholderText = "TotalDays";
            txtTotalDays.Size = new Size(141, 26);
            txtTotalDays.TabIndex = 9;
            txtTotalDays.TextAlign = HorizontalAlignment.Center;
            // 
            // comboBoxProcent
            // 
            comboBoxProcent.Font = new Font("Courier New", 12F, FontStyle.Bold);
            comboBoxProcent.FormattingEnabled = true;
            comboBoxProcent.Items.AddRange(new object[] { "100%", "75%", "50%", "25%", "0% Recovered" });
            comboBoxProcent.Location = new Point(379, 119);
            comboBoxProcent.Name = "comboBoxProcent";
            comboBoxProcent.Size = new Size(137, 26);
            comboBoxProcent.TabIndex = 10;
            // 
            // AbsenceDueIllnessForm
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(824, 461);
            Controls.Add(comboBoxProcent);
            Controls.Add(txtTotalDays);
            Controls.Add(txtAlias);
            Controls.Add(btnApply);
            Controls.Add(btnCancel);
            Controls.Add(btnRecovery);
            Controls.Add(btnCallInSick);
            Controls.Add(txtRecovery);
            Controls.Add(txtDayCallInSick);
            Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "AbsenceDueIllnessForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Absence due Illness";
            Load += Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public TextBox txtDayCallInSick;
        public TextBox txtRecovery;
        public Button btnCallInSick;
        public Button btnRecovery;
        public Button btnCancel;
        public Button btnApply;
        public TextBox txtAlias;
        public TextBox txtTotalDays;
        private ComboBox comboBoxProcent;
    }
}