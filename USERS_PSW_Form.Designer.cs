namespace CRUD_System
{
    partial class USERS_PSW_Form
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
            checkBoxTogglePSW1 = new CheckBox();
            inputChangePSW1 = new TextBox();
            lblPassword = new Label();
            btnEnterPSW = new Button();
            SuspendLayout();
            // 
            // checkBoxTogglePSW1
            // 
            checkBoxTogglePSW1.AutoSize = true;
            checkBoxTogglePSW1.Location = new Point(13, 101);
            checkBoxTogglePSW1.Name = "checkBoxTogglePSW1";
            checkBoxTogglePSW1.Size = new Size(157, 22);
            checkBoxTogglePSW1.TabIndex = 6;
            checkBoxTogglePSW1.Text = "Show Password";
            checkBoxTogglePSW1.UseVisualStyleBackColor = true;
            checkBoxTogglePSW1.CheckedChanged += checkBoxTogglePSW_CheckedChanged;
            // 
            // inputChangePSW1
            // 
            inputChangePSW1.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            inputChangePSW1.Location = new Point(13, 68);
            inputChangePSW1.Margin = new Padding(4);
            inputChangePSW1.MaxLength = 32676;
            inputChangePSW1.Name = "inputChangePSW1";
            inputChangePSW1.PasswordChar = '*';
            inputChangePSW1.PlaceholderText = "PASSWORD";
            inputChangePSW1.Size = new Size(441, 26);
            inputChangePSW1.TabIndex = 5;
            inputChangePSW1.TextAlign = HorizontalAlignment.Center;
            //inputChangePSW1.TextChanged += inputChangePSW1_TextChanged;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(44, 12);
            lblPassword.Name = "lblPassword";
            lblPassword.RightToLeft = RightToLeft.No;
            lblPassword.Size = new Size(78, 18);
            lblPassword.TabIndex = 9;
            lblPassword.Text = "PSW TXT";
            // 
            // btnEnterPSW
            // 
            btnEnterPSW.Location = new Point(187, 149);
            btnEnterPSW.Name = "btnEnterPSW";
            btnEnterPSW.Size = new Size(85, 33);
            btnEnterPSW.TabIndex = 10;
            btnEnterPSW.Text = "Enter";
            btnEnterPSW.UseVisualStyleBackColor = true;
            btnEnterPSW.Click += btnEnterPSW_Click;
            // 
            // USERS_PSW_Form
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(467, 225);
            Controls.Add(btnEnterPSW);
            Controls.Add(lblPassword);
            Controls.Add(checkBoxTogglePSW1);
            Controls.Add(inputChangePSW1);
            Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4);
            Name = "USERS_PSW_Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Change Password";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBoxTogglePSW1;
        private TextBox inputChangePSW1;
        private Label lblPassword;
        private Button btnEnterPSW;
    }
}