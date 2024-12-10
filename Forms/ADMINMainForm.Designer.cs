namespace CRUD_System
{
    partial class AdminMainForm
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
            labelAlias = new Label();
            UserControl = new AdminMainControl();
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
            buttonLOGOUT.FlatStyle = FlatStyle.Standard;
            buttonLOGOUT.Location = new Point(188, 11);
            buttonLOGOUT.Name = "buttonLOGOUT";
            buttonLOGOUT.Size = new Size(100, 26);
            buttonLOGOUT.TabIndex = 1;
            buttonLOGOUT.Text = "log Out";
            buttonLOGOUT.UseVisualStyleBackColor = false;
            buttonLOGOUT.Click += buttonLOGOUT_Click;
            // 
            // labelAlias
            // 
            labelAlias.AutoSize = true;
            labelAlias.BackColor = Color.FromArgb(128, 255, 128);
            labelAlias.Location = new Point(124, 15);
            labelAlias.Name = "labelUserName";
            labelAlias.Size = new Size(58, 18);
            labelAlias.TabIndex = 3;
            labelAlias.Text = "Admin";
            labelAlias.TextAlign = ContentAlignment.TopCenter;
            //
            // UserControl
            // 
            UserControl.BorderStyle = BorderStyle.FixedSingle;
            UserControl.Location = new Point(18, 55);
            UserControl.Name = "adminManagementControl";
            UserControl.Size = new Size(1500, 460);
            UserControl.TabIndex = 4;
            UserControl.Load += adminControl.UserControl_Load;
            // 
            // MainFormADMIN
            //
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1550, 550);
            Controls.Add(labelAlias);
            Controls.Add(buttonLOGOUT);
            Controls.Add(textBoxUserName);
            Controls.Add(UserControl);
            Font = new Font("Courier New", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            Name = "MainFormADMIN";
            Text = "Display UserDetails Admin";
            StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += this.MainFormADMIN_FormClosing;
            Load += MainFormAdmin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TextBox textBoxUserName;
        public Button buttonLOGOUT;
        public Label labelAlias;
        public AdminMainControl UserControl;

        
    }
}