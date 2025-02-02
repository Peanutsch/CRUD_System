using System.Windows.Forms;

namespace CRUD_System
{
    partial class ADMINCreateForm
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
            createControlADMIN = new AdminCreateControl();
            focusButton = new Button();
            SuspendLayout();
            // 
            // createControlADMIN
            // 
            createControlADMIN.BackColor = SystemColors.ActiveCaption;
            createControlADMIN.Dock = DockStyle.Fill;
            createControlADMIN.Location = new Point(0, 0);
            createControlADMIN.Name = "createControlADMIN";
            createControlADMIN.Size = new Size(1133, 339);
            createControlADMIN.TabIndex = 0;
            // 
            // focusButton
            // 
            focusButton.Enabled = false;
            focusButton.Location = new Point(1046, 304);
            focusButton.Name = "focusButton";
            focusButton.Size = new Size(75, 23);
            focusButton.TabIndex = 0;
            focusButton.Visible = false;
            // 
            // ADMINCreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1133, 339);
            Controls.Add(focusButton);
            Controls.Add(createControlADMIN);
            Name = "ADMINCreateForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create New User";
            Load += Form_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button focusButton;
        private AdminCreateControl createControlADMIN;
    }
}