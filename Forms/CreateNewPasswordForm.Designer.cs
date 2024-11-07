namespace CRUD_System
{
    partial class CreateNewPasswordForm
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
            this.checkBoxTogglePSW = new CheckBox();
            this.inputChangePSW = new TextBox();
            this.lblPassword = new Label();
            this.btnApplyPSW = new Button();
            this.inputConfirmPSW = new TextBox();
            this.btnCancel = new Button();
            this.SuspendLayout();
            // 
            // checkBoxTogglePSW
            // 
            this.checkBoxTogglePSW.AutoSize = true;
            this.checkBoxTogglePSW.CheckAlign = ContentAlignment.MiddleRight;
            this.checkBoxTogglePSW.Location = new Point(296, 152);
            this.checkBoxTogglePSW.Name = "checkBoxTogglePSW";
            this.checkBoxTogglePSW.Size = new Size(157, 22);
            this.checkBoxTogglePSW.TabIndex = 2;
            this.checkBoxTogglePSW.Text = "Show Password";
            this.checkBoxTogglePSW.UseVisualStyleBackColor = true;
            this.checkBoxTogglePSW.CheckedChanged += this.checkBoxTogglePSW_CheckedChanged;
            // 
            // inputChangePSW
            // 
            this.inputChangePSW.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.inputChangePSW.Location = new Point(13, 76);
            this.inputChangePSW.Margin = new Padding(4);
            this.inputChangePSW.MaxLength = 32676;
            this.inputChangePSW.Name = "inputChangePSW";
            this.inputChangePSW.PasswordChar = '*';
            this.inputChangePSW.PlaceholderText = "ENTER NEW PASSWORD";
            this.inputChangePSW.Size = new Size(441, 26);
            this.inputChangePSW.TabIndex = 0;
            this.inputChangePSW.TextAlign = HorizontalAlignment.Center;
            this.inputChangePSW.TextChanged += this.inputChangePSW1_TextChanged;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(44, 12);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.RightToLeft = RightToLeft.No;
            this.lblPassword.Size = new Size(138, 18);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "LABEL PSW TXT";
            // 
            // btnApplyPSW
            // 
            this.btnApplyPSW.Enabled = false;
            this.btnApplyPSW.Location = new Point(140, 180);
            this.btnApplyPSW.Name = "btnApplyPSW";
            this.btnApplyPSW.Size = new Size(85, 33);
            this.btnApplyPSW.TabIndex = 3;
            this.btnApplyPSW.Text = "Apply";
            this.btnApplyPSW.UseVisualStyleBackColor = true;
            this.btnApplyPSW.Click += this.btnApplyPSW_Click;
            // 
            // inputConfirmPSW
            // 
            this.inputConfirmPSW.Enabled = false;
            this.inputConfirmPSW.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.inputConfirmPSW.Location = new Point(12, 119);
            this.inputConfirmPSW.Margin = new Padding(4);
            this.inputConfirmPSW.MaxLength = 32676;
            this.inputConfirmPSW.Name = "inputConfirmPSW";
            this.inputConfirmPSW.PasswordChar = '*';
            this.inputConfirmPSW.PlaceholderText = "CONFIRM PASSWORD";
            this.inputConfirmPSW.Size = new Size(441, 26);
            this.inputConfirmPSW.TabIndex = 1;
            this.inputConfirmPSW.TextAlign = HorizontalAlignment.Center;
            this.inputConfirmPSW.TextChanged += this.inputConfirmPSW_TextChanged;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(240, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(85, 33);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += this.btnCancel_Click;
            // 
            // USERS_PSW_Form
            // 
            this.AutoScaleDimensions = new SizeF(10F, 18F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.ClientSize = new Size(467, 225);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.inputConfirmPSW);
            this.Controls.Add(this.btnApplyPSW);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.checkBoxTogglePSW);
            this.Controls.Add(this.inputChangePSW);
            this.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Margin = new Padding(4);
            this.Name = "USERS_PSW_Form";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.Load += this.Form_Load;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private CheckBox checkBoxTogglePSW;
        private TextBox inputChangePSW;
        private Label lblPassword;
        private Button btnApplyPSW;
        private TextBox inputConfirmPSW;
        private Button btnCancel;
    }
}