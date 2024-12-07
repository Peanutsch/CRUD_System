namespace CRUD_System
{
    partial class ShowReportForm
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
            /*
            rtxtDisplayReport = new RichTextBox();
            txtDate = new TextBox();
            txtSubject = new TextBox();
            txtCreatedBy = new TextBox();
            */
            btnCloseForm = new Button();

            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtCreatedBy = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.rtxtDisplayReport = new System.Windows.Forms.RichTextBox();

            SuspendLayout();
            // 
            // rtxtDisplayReport
            // 
            rtxtDisplayReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtxtDisplayReport.Location = new Point(19, 47);
            rtxtDisplayReport.Margin = new Padding(4, 4, 4, 4);
            rtxtDisplayReport.Name = "rtxtDisplayReport";
            rtxtDisplayReport.Size = new Size(508, 406);
            rtxtDisplayReport.TabIndex = 0;
            rtxtDisplayReport.Text = "";
            // 
            // txtDate
            // 
            txtDate.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDate.Location = new Point(19, 13);
            txtDate.Margin = new Padding(4, 4, 4, 4);
            txtDate.Name = "txtDate";
            txtDate.PlaceholderText = "Date";
            txtDate.Size = new Size(163, 26);
            txtDate.TabIndex = 1;
            txtDate.TextAlign = HorizontalAlignment.Center;
            // 
            // txtSubject
            // 
            txtSubject.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSubject.Location = new Point(190, 13);
            txtSubject.Margin = new Padding(4, 4, 4, 4);
            txtSubject.Name = "txtSubject";
            txtSubject.PlaceholderText = "Subject";
            txtSubject.Size = new Size(163, 26);
            txtSubject.TabIndex = 2;
            txtSubject.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCreatedBy
            // 
            txtCreatedBy.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCreatedBy.Location = new Point(362, 13);
            txtCreatedBy.Margin = new Padding(4, 4, 4, 4);
            txtCreatedBy.Name = "txtCreatedBy";
            txtCreatedBy.PlaceholderText = "CreatedBy";
            txtCreatedBy.Size = new Size(163, 26);
            txtCreatedBy.TabIndex = 3;
            txtCreatedBy.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCloseForm
            // 
            btnCloseForm.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCloseForm.Location = new Point(218, 474);
            btnCloseForm.Margin = new Padding(4, 4, 4, 4);
            btnCloseForm.Name = "btnCloseForm";
            btnCloseForm.Size = new Size(107, 28);
            btnCloseForm.TabIndex = 4;
            btnCloseForm.Text = "Close";
            btnCloseForm.UseVisualStyleBackColor = true;
            btnCloseForm.Click += btnCloseForm_Click;
            // 
            // ShowReportForm
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(571, 517);
            Controls.Add(btnCloseForm);
            Controls.Add(txtCreatedBy);
            Controls.Add(txtSubject);
            Controls.Add(txtDate);
            Controls.Add(rtxtDisplayReport);
            Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 4, 4, 4);
            Name = "ShowReportForm";
            Text = "Show Report";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public RichTextBox rtxtDisplayReport;
        public TextBox txtDate;
        public TextBox txtSubject;
        public TextBox txtCreatedBy;
        public Button btnCloseForm;
    }
}