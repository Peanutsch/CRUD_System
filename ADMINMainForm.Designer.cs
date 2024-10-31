namespace CRUD_System
{
    partial class ADMINMainForm
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
            labelUserName = new Label();
            UserControl = new ADMINMainControl();
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
            // labelUserName
            // 
            labelUserName.AutoSize = true;
            labelUserName.BackColor = Color.FromArgb(128, 255, 128);
            labelUserName.Location = new Point(124, 15);
            labelUserName.Name = "labelUserName";
            labelUserName.Size = new Size(58, 18);
            labelUserName.TabIndex = 3;
            labelUserName.Text = "ADMIN";
            labelUserName.TextAlign = ContentAlignment.TopCenter;
            //
            // UserControl
            // 
            UserControl.BorderStyle = BorderStyle.FixedSingle;
            UserControl.Location = new Point(18, 86);
            UserControl.Name = "adminManagementControl";
            UserControl.Size = new Size(1208, 400);
            UserControl.TabIndex = 4;
            // 
            // MainFormADMIN
            //
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1260, 512);
            Controls.Add(labelUserName);
            Controls.Add(buttonLOGOUT);
            Controls.Add(textBoxUserName);
            Controls.Add(UserControl);
            Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            Name = "MainFormADMIN";
            StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += this.MainFormADMIN_FormClosing;
            Text = "Display UserDetails Admin";
            Load += MainFormAdmin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxUserName;
        private Button buttonLOGOUT;
        private Label labelUserName;
        private ADMINMainControl UserControl;
    }
}