namespace CRUD_System
{
    partial class ShowLogsForm
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
            this.btnCloseForm = new Button();
            this.txtSelectedAlias = new TextBox();
            this.listBoxLogs = new ListBox();
            this.SuspendLayout();
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCloseForm.Location = new Point(223, 503);
            this.btnCloseForm.Margin = new Padding(4);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new Size(107, 28);
            this.btnCloseForm.TabIndex = 4;
            this.btnCloseForm.Text = "Close";
            this.btnCloseForm.UseVisualStyleBackColor = true;
            this.btnCloseForm.Click += this.btnCloseForm_Click;
            // 
            // txtSelectedAlias
            // 
            this.txtSelectedAlias.Enabled = false;
            this.txtSelectedAlias.Location = new Point(26, 12);
            this.txtSelectedAlias.Name = "txtSelectedAlias";
            this.txtSelectedAlias.PlaceholderText = "SelectedAlias";
            this.txtSelectedAlias.Size = new Size(163, 26);
            this.txtSelectedAlias.TabIndex = 5;
            this.txtSelectedAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.HorizontalScrollbar = true;
            this.listBoxLogs.Location = new Point(26, 42);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new Size(507, 452);
            this.listBoxLogs.TabIndex = 6;
            // 
            // ShowLogsForm
            // 
            this.AutoScaleDimensions = new SizeF(10F, 18F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.ClientSize = new Size(556, 539);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.txtSelectedAlias);
            this.Controls.Add(this.btnCloseForm);
            this.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Margin = new Padding(4);
            this.Name = "ShowLogsForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Log Display";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        public Button btnCloseForm;
        public TextBox txtSelectedAlias;
        private ListBox listBoxLogs;
    }
}