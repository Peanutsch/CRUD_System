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
            ADMINCreateControl createControlADMIN = new ADMINCreateControl();

            this.focusButton = new Button();
            this.SuspendLayout();
            // 
            // focusButton
            // 
            this.focusButton.Location = new Point(1046, 304);
            this.focusButton.Name = "focusButton";
            this.focusButton.Size = new Size(75, 23);
            this.focusButton.TabIndex = 0;
            this.focusButton.Visible = false;
            this.focusButton.Enabled = false;
            // 
            // createControlADMIN
            // 
            createControlADMIN.Dock = DockStyle.Fill;
            createControlADMIN.BackColor = SystemColors.ActiveCaption;
            createControlADMIN.Location = new Point(-1, 1);
            //createControlADMIN.Name = "Create New User";
            createControlADMIN.Size = new Size(1136, 340);
            createControlADMIN.TabIndex = 0;
            // 
            // CreateFormADMIN
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1133, 339);
            this.Controls.Add(this.focusButton);
            this.Controls.Add(createControlADMIN);
            this.Name = "CreateFormADMIN";
            this.Text = "Create New User";
            this.Load += this.Form_Load;
            this.ResumeLayout(false);
        }

        #endregion

        private Button focusButton;
    }
}