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
            this.loginUserNameBox = new TextBox();
            this.loginUserPSWBox = new TextBox();
            this.loginButton = new Button();
            this.checkBoxTogglePSW = new CheckBox();
            this.SuspendLayout();
            // 
            // loginUserNameBox
            // 
            this.loginUserNameBox.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.loginUserNameBox.Location = new Point(13, 56);
            this.loginUserNameBox.Margin = new Padding(4);
            this.loginUserNameBox.MaxLength = 20;
            this.loginUserNameBox.Name = "loginUserNameBox";
            this.loginUserNameBox.PlaceholderText = "Username";
            this.loginUserNameBox.Size = new Size(441, 26);
            this.loginUserNameBox.TabIndex = 0;
            this.loginUserNameBox.TextAlign = HorizontalAlignment.Center;
            this.loginUserNameBox.TextChanged += this.LoginUserNameBox_TextChanged_1;
            // 
            // loginUserPSWBox
            // 
            this.loginUserPSWBox.Enabled = false;
            this.loginUserPSWBox.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.loginUserPSWBox.Location = new Point(13, 90);
            this.loginUserPSWBox.Margin = new Padding(4);
            this.loginUserPSWBox.MaxLength = 32676;
            this.loginUserPSWBox.Name = "loginUserPSWBox";
            this.loginUserPSWBox.PasswordChar = '*';
            this.loginUserPSWBox.PlaceholderText = "PASSWORD";
            this.loginUserPSWBox.Size = new Size(441, 26);
            this.loginUserPSWBox.TabIndex = 1;
            this.loginUserPSWBox.TextAlign = HorizontalAlignment.Center;
            this.loginUserPSWBox.TextChanged += this.LoginUserPSWBox_TextChanged;
            // 
            // loginButton
            // 
            this.loginButton.BackColor = SystemColors.ControlLightLight;
            this.loginButton.Enabled = false;
            this.loginButton.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.loginButton.Location = new Point(162, 156);
            this.loginButton.Margin = new Padding(4);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new Size(150, 56);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Log In";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += this.LoginButton_Click;
            // 
            // checkBoxTogglePSW
            // 
            this.checkBoxTogglePSW.AutoSize = true;
            this.checkBoxTogglePSW.Location = new Point(12, 123);
            this.checkBoxTogglePSW.Name = "checkBoxTogglePSW";
            this.checkBoxTogglePSW.Size = new Size(157, 22);
            this.checkBoxTogglePSW.TabIndex = 5;
            this.checkBoxTogglePSW.Text = "Show Password";
            this.checkBoxTogglePSW.UseVisualStyleBackColor = true;
            this.checkBoxTogglePSW.CheckedChanged += this.checkBoxTogglePSW_CheckedChanged;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new SizeF(10F, 18F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.ClientSize = new Size(467, 225);
            this.Controls.Add(this.checkBoxTogglePSW);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.loginUserPSWBox);
            this.Controls.Add(this.loginUserNameBox);
            this.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Margin = new Padding(4);
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "CRUD Log In";
            this.Load += this.LoginForm_Load;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private Button loginButton;
        private TextBox loginUserNameBox;
        private TextBox loginUserPSWBox;
        private CheckBox checkBoxTogglePSW;
    }
}
