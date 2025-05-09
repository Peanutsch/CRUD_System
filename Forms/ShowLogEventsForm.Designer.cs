namespace CRUD_System
{
    partial class ShowLogEventsForm
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
            btnCloseForm = new Button();
            txtSelectedAlias = new TextBox();
            listBoxLogs = new ListBox();
            SuspendLayout();
            // 
            // btnCloseForm
            // 
            btnCloseForm.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCloseForm.Location = new Point(259, 501);
            btnCloseForm.Margin = new Padding(4);
            btnCloseForm.Name = "btnCloseForm";
            btnCloseForm.Size = new Size(107, 28);
            btnCloseForm.TabIndex = 4;
            btnCloseForm.Text = "Close";
            btnCloseForm.UseVisualStyleBackColor = true;
            btnCloseForm.Click += btnCloseForm_Click;
            // 
            // txtSelectedAlias
            // 
            txtSelectedAlias.Enabled = false;
            txtSelectedAlias.Location = new Point(26, 12);
            txtSelectedAlias.Name = "txtSelectedAlias";
            txtSelectedAlias.PlaceholderText = "SelectedAlias";
            txtSelectedAlias.Size = new Size(163, 26);
            txtSelectedAlias.TabIndex = 5;
            txtSelectedAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // listBoxLogs
            // 
            listBoxLogs.Font = new Font("Courier New", 9F, FontStyle.Bold);
            listBoxLogs.FormattingEnabled = true;
            listBoxLogs.HorizontalScrollbar = true;
            listBoxLogs.Location = new Point(26, 42);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(573, 452);
            listBoxLogs.TabIndex = 6;
            // 
            // ShowLogEventsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(629, 539);
            Controls.Add(listBoxLogs);
            Controls.Add(txtSelectedAlias);
            Controls.Add(btnCloseForm);
            Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            Name = "ShowLogEventsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Overview Log Events";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public Button btnCloseForm;
        public TextBox txtSelectedAlias;
        private ListBox listBoxLogs;
    }
}