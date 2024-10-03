namespace CRUD_LoginSystem
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
            this.SuspendLayout();
            // 
            // loginUserNameBox
            // 
            this.loginUserNameBox.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.loginUserNameBox.Location = new Point(82, 58);
            this.loginUserNameBox.MaxLength = 20;
            this.loginUserNameBox.Name = "loginUserNameBox";
            this.loginUserNameBox.PlaceholderText = "Login Name";
            this.loginUserNameBox.Size = new Size(310, 26);
            this.loginUserNameBox.TabIndex = 0;
            this.loginUserNameBox.TextAlign = HorizontalAlignment.Center;
            // 
            // loginUserPSWBox
            // 
            this.loginUserPSWBox.Enabled = false;
            this.loginUserPSWBox.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.loginUserPSWBox.Location = new Point(82, 101);
            this.loginUserPSWBox.MaxLength = 32676;
            this.loginUserPSWBox.Name = "loginUserPSWBox";
            this.loginUserPSWBox.PasswordChar = '*';
            this.loginUserPSWBox.PlaceholderText = "PASSWORD";
            this.loginUserPSWBox.Size = new Size(310, 26);
            this.loginUserPSWBox.TabIndex = 1;
            this.loginUserPSWBox.TextAlign = HorizontalAlignment.Center;
            // 
            // loginButton
            // 
            this.loginButton.BackColor = SystemColors.ControlLightLight;
            this.loginButton.Enabled = false;
            this.loginButton.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.loginButton.Location = new Point(183, 133);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new Size(105, 47);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Log In";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += this.loginButton_Click;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.loginUserPSWBox);
            this.Controls.Add(this.loginUserNameBox);
            this.Name = "LoginForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TextBox loginUserNameBox;
        private TextBox loginUserPSWBox;
        private Button loginButton;
    }
}
