namespace CRUD_System
{
    partial class MainFormUSERS
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
            textBoxUserName = new TextBox();
            buttonLOGOUT = new Button();
            labelUser = new Label();
            SuspendLayout();
            // 
            // textBoxUserName
            // 
            textBoxUserName.BackColor = SystemColors.ActiveCaption;
            textBoxUserName.Location = new Point(18, 12);
            textBoxUserName.Name = "textBoxUserName";
            textBoxUserName.ReadOnly = true;
            textBoxUserName.Size = new Size(100, 26);
            textBoxUserName.TabIndex = 0;
            textBoxUserName.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonLOGOUT
            // 
            buttonLOGOUT.BackColor = SystemColors.ActiveCaption;
            buttonLOGOUT.FlatStyle = FlatStyle.Popup;
            buttonLOGOUT.Location = new Point(198, 11);
            buttonLOGOUT.Name = "buttonLOGOUT";
            buttonLOGOUT.Size = new Size(100, 26);
            buttonLOGOUT.TabIndex = 1;
            buttonLOGOUT.Text = "Log Out";
            buttonLOGOUT.UseVisualStyleBackColor = false;
            buttonLOGOUT.Click += buttonLOGOUT_Click;
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.BackColor = SystemColors.Highlight;
            labelUser.Location = new Point(134, 15);
            labelUser.Name = "labelAdmin";
            labelUser.Size = new Size(48, 18);
            labelUser.TabIndex = 3;
            labelUser.Text = "ROLE";
            labelUser.TextAlign = ContentAlignment.TopCenter;
            // 
            // MainFormADMIN
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(2549, 1061);
            Controls.Add(labelUser);
            Controls.Add(buttonLOGOUT);
            Controls.Add(textBoxUserName);
            Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            Name = "MainFormADMIN";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main";
            WindowState = FormWindowState.Maximized;
            Load += MainFormUsers_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxUserName;
        private Button buttonLOGOUT;
        private Label labelUser;
    }
}