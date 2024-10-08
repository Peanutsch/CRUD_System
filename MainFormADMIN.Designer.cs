namespace CRUD_System
{
    partial class MainFormADMIN
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
            this.textBoxUserName = new TextBox();
            this.buttonLOGOUT = new Button();
            this.labelAdmin = new Label();
            this.userManagementControl1 = new UserManagementControl();
            this.SuspendLayout();
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.BackColor = SystemColors.ActiveCaption;
            this.textBoxUserName.Location = new Point(18, 12);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.ReadOnly = true;
            this.textBoxUserName.Size = new Size(100, 26);
            this.textBoxUserName.TabIndex = 0;
            this.textBoxUserName.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonLOGOUT
            // 
            this.buttonLOGOUT.BackColor = SystemColors.ActiveCaption;
            this.buttonLOGOUT.FlatStyle = FlatStyle.Popup;
            this.buttonLOGOUT.Location = new Point(198, 11);
            this.buttonLOGOUT.Name = "buttonLOGOUT";
            this.buttonLOGOUT.Size = new Size(100, 26);
            this.buttonLOGOUT.TabIndex = 1;
            this.buttonLOGOUT.Text = "Log Out";
            this.buttonLOGOUT.UseVisualStyleBackColor = false;
            this.buttonLOGOUT.Click += this.buttonLOGOUT_Click;
            // 
            // labelAdmin
            // 
            this.labelAdmin.AutoSize = true;
            this.labelAdmin.BackColor = SystemColors.Highlight;
            this.labelAdmin.Location = new Point(134, 15);
            this.labelAdmin.Name = "labelAdmin";
            this.labelAdmin.Size = new Size(48, 18);
            this.labelAdmin.TabIndex = 3;
            this.labelAdmin.Text = "ROLE";
            this.labelAdmin.TextAlign = ContentAlignment.TopCenter;
            // 
            // userManagementControl1
            // 
            this.userManagementControl1.BorderStyle = BorderStyle.FixedSingle;
            this.userManagementControl1.Location = new Point(18, 86);
            this.userManagementControl1.Name = "userManagementControl1";
            this.userManagementControl1.Size = new Size(1208, 336);
            this.userManagementControl1.TabIndex = 4;
            // 
            // MainFormADMIN
            // 
            this.AutoScaleDimensions = new SizeF(10F, 18F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.ClientSize = new Size(1260, 512);
            this.Controls.Add(this.userManagementControl1);
            this.Controls.Add(this.labelAdmin);
            this.Controls.Add(this.buttonLOGOUT);
            this.Controls.Add(this.textBoxUserName);
            this.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Margin = new Padding(4);
            this.Name = "MainFormADMIN";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Main Admin";
            this.Load += this.MainFormAdmin_Load;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TextBox textBoxUserName;
        private Button buttonLOGOUT;
        private Label labelAdmin;
        private UserManagementControl userManagementControl1;
    }
}