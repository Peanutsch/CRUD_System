namespace CRUD_System
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginUserNameBox = new TextBox();
            loginUserPSWBox = new TextBox();
            loginButton = new Button();
            checkBoxTogglePSW = new CheckBox();
            SuspendLayout();
            // 
            // loginUserNameBox
            // 
            loginUserNameBox.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loginUserNameBox.Location = new Point(13, 56);
            loginUserNameBox.Margin = new Padding(4);
            loginUserNameBox.MaxLength = 20;
            loginUserNameBox.Name = "loginUserNameBox";
            loginUserNameBox.PlaceholderText = "Username";
            loginUserNameBox.Size = new Size(441, 26);
            loginUserNameBox.TabIndex = 1;
            loginUserNameBox.TextAlign = HorizontalAlignment.Center;
            loginUserNameBox.TextChanged += LoginUserNameBox_TextChanged_1;
            // 
            // loginUserPSWBox
            // 
            loginUserPSWBox.Enabled = false;
            loginUserPSWBox.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loginUserPSWBox.Location = new Point(13, 90);
            loginUserPSWBox.Margin = new Padding(4);
            loginUserPSWBox.MaxLength = 32676;
            loginUserPSWBox.Name = "loginUserPSWBox";
            loginUserPSWBox.PasswordChar = '*';
            loginUserPSWBox.PlaceholderText = "PASSWORD";
            loginUserPSWBox.Size = new Size(441, 26);
            loginUserPSWBox.TabIndex = 2;
            loginUserPSWBox.TextAlign = HorizontalAlignment.Center;
            loginUserPSWBox.TextChanged += LoginUserPSWBox_TextChanged;
            // 
            // loginButton
            // 
            loginButton.BackColor = SystemColors.ControlLightLight;
            loginButton.Enabled = false;
            loginButton.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loginButton.Location = new Point(162, 156);
            loginButton.Margin = new Padding(4);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(150, 56);
            loginButton.TabIndex = 3;
            loginButton.Text = "Log In";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += LoginButton_Click;
            // 
            // checkBoxTogglePSW
            // 
            checkBoxTogglePSW.AutoSize = true;
            checkBoxTogglePSW.Location = new Point(12, 123);
            checkBoxTogglePSW.Name = "checkBoxTogglePSW";
            checkBoxTogglePSW.Size = new Size(157, 22);
            checkBoxTogglePSW.TabIndex = 4;
            checkBoxTogglePSW.Text = "Show Password";
            checkBoxTogglePSW.UseVisualStyleBackColor = true;
            checkBoxTogglePSW.CheckedChanged += checkBoxTogglePSW_CheckedChanged;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(467, 225);
            Controls.Add(checkBoxTogglePSW);
            Controls.Add(loginButton);
            Controls.Add(loginUserPSWBox);
            Controls.Add(loginUserNameBox);
            Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CRUD Log In";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button loginButton;
        private TextBox loginUserNameBox;
        private TextBox loginUserPSWBox;
        private CheckBox checkBoxTogglePSW;
    }
}
