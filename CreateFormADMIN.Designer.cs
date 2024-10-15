using System.Windows.Forms;

namespace CRUD_System
{
    partial class CreateFormADMIN
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
            editUserDetailsControl1 = new CreateControlADMIN(this);
            SuspendLayout();
            // 
            // editUserDetailsControl1
            // 
            editUserDetailsControl1.Dock = DockStyle.Fill;
            editUserDetailsControl1.BackColor = SystemColors.ActiveCaption;
            editUserDetailsControl1.Location = new Point(-1, 1);
            editUserDetailsControl1.Name = "editUserDetailsControl1";
            editUserDetailsControl1.Size = new Size(1136, 340);
            editUserDetailsControl1.TabIndex = 0;
            // 
            // EditFormADMIN
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1133, 339);
            Controls.Add(editUserDetailsControl1);
            //userControl.Dock = DockStyle.Fill; // UserControl fills whole Form
            Name = "EditFormADMIN";
            Text = "Edit UserDetails ADMIN";
            ResumeLayout(false);
        }
        
        #endregion

        private CreateControlADMIN editUserDetailsControl1;
    }
}