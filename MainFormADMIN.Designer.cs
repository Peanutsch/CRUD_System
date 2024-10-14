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
            textBoxUserName = new TextBox();
            buttonLOGOUT = new Button();
            labelAdmin = new Label();
            adminManagementControl = new AdminManagementControl();
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
            buttonLOGOUT.Location = new Point(188, 11);
            buttonLOGOUT.Name = "buttonLOGOUT";
            buttonLOGOUT.Size = new Size(100, 26);
            buttonLOGOUT.TabIndex = 1;
            buttonLOGOUT.Text = "Log Out";
            buttonLOGOUT.UseVisualStyleBackColor = false;
            buttonLOGOUT.Click += buttonLOGOUT_Click;
            // 
            // labelAdmin
            // 
            labelAdmin.AutoSize = true;
            labelAdmin.BackColor = Color.FromArgb(128, 255, 128);
            labelAdmin.Location = new Point(124, 15);
            labelAdmin.Name = "labelAdmin";
            labelAdmin.Size = new Size(58, 18);
            labelAdmin.TabIndex = 3;
            labelAdmin.Text = "ADMIN";
            labelAdmin.TextAlign = ContentAlignment.TopCenter;
            //
            // adminManagementControl
            // 
            adminManagementControl.BorderStyle = BorderStyle.FixedSingle;
            adminManagementControl.Location = new Point(18, 86);
            adminManagementControl.Name = "adminManagementControl";
            adminManagementControl.Size = new Size(1208, 400);
            adminManagementControl.TabIndex = 4;
            // 
            // MainFormADMIN
            //
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1260, 512);
            Controls.Add(labelAdmin);
            Controls.Add(buttonLOGOUT);
            Controls.Add(textBoxUserName);
            Controls.Add(adminManagementControl);
            Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            Name = "MainFormADMIN";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main Admin";
            Load += MainFormAdmin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxUserName;
        private Button buttonLOGOUT;
        private Label labelAdmin;
        private AdminManagementControl adminManagementControl;
    }
}