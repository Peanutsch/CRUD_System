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
            inputChangePSW2 = new TextBox();
            checkBoxTogglePSW2 = new CheckBox();
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
            // 
            // inputChangePSW2
            // 
            inputChangePSW2.Enabled = false;
            inputChangePSW2.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            inputChangePSW2.Location = new Point(13, 130);
            inputChangePSW2.Margin = new Padding(4);
            inputChangePSW2.MaxLength = 32676;
            inputChangePSW2.Name = "inputChangePSW2";
            inputChangePSW2.PasswordChar = '*';
            inputChangePSW2.PlaceholderText = "PASSWORD";
            inputChangePSW2.Size = new Size(441, 26);
            inputChangePSW2.TabIndex = 7;
            inputChangePSW2.TextAlign = HorizontalAlignment.Center;
            // 
            // checkBoxTogglePSW2
            // 
            checkBoxTogglePSW2.AutoSize = true;
            checkBoxTogglePSW2.Location = new Point(13, 163);
            checkBoxTogglePSW2.Name = "checkBoxTogglePSW2";
            checkBoxTogglePSW2.Size = new Size(157, 22);
            checkBoxTogglePSW2.TabIndex = 8;
            checkBoxTogglePSW2.Text = "Show Password";
            checkBoxTogglePSW2.UseVisualStyleBackColor = true;
            // 
            // USERS_PSW_Form
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(467, 225);
            Controls.Add(checkBoxTogglePSW2);
            Controls.Add(inputChangePSW2);
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
        private TextBox inputChangePSW2;
        private CheckBox checkBoxTogglePSW2;
    }
}