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
            btnCloseForm = new Button();
            txtDate = new TextBox();
            txtAliasCreator = new TextBox();
            txtSubject = new TextBox();
            rtxtDisplayReport = new RichTextBox();
            txtSelectedAlias = new TextBox();
            SuspendLayout();
            // 
            // btnCloseForm
            // 
            btnCloseForm.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCloseForm.Location = new Point(221, 505);
            btnCloseForm.Margin = new Padding(4);
            btnCloseForm.Name = "btnCloseForm";
            btnCloseForm.Size = new Size(107, 28);
            btnCloseForm.TabIndex = 4;
            btnCloseForm.Text = "Close";
            btnCloseForm.UseVisualStyleBackColor = true;
            btnCloseForm.Click += btnCloseForm_Click;
            // 
            // txtDate
            // 
            txtDate.Enabled = false;
            txtDate.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDate.Location = new Point(19, 57);
            txtDate.Margin = new Padding(4);
            txtDate.Name = "txtDate";
            txtDate.PlaceholderText = "Date";
            txtDate.Size = new Size(163, 26);
            txtDate.TabIndex = 1;
            txtDate.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAliasCreator
            // 
            txtAliasCreator.Enabled = false;
            txtAliasCreator.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAliasCreator.Location = new Point(361, 57);
            txtAliasCreator.Margin = new Padding(4);
            txtAliasCreator.Name = "txtAliasCreator";
            txtAliasCreator.PlaceholderText = "AliasCreator";
            txtAliasCreator.Size = new Size(163, 26);
            txtAliasCreator.TabIndex = 3;
            txtAliasCreator.TextAlign = HorizontalAlignment.Center;
            // 
            // txtSubject
            // 
            txtSubject.Enabled = false;
            txtSubject.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSubject.Location = new Point(190, 57);
            txtSubject.Margin = new Padding(4);
            txtSubject.Name = "txtSubject";
            txtSubject.PlaceholderText = "Subject";
            txtSubject.Size = new Size(163, 26);
            txtSubject.TabIndex = 2;
            txtSubject.TextAlign = HorizontalAlignment.Center;
            // 
            // rtxtDisplayReport
            // 
            //rtxtDisplayReport.Enabled = false;
            rtxtDisplayReport.ReadOnly = true;
            rtxtDisplayReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtxtDisplayReport.Location = new Point(19, 91);
            rtxtDisplayReport.Margin = new Padding(4);
            rtxtDisplayReport.Name = "rtxtDisplayReport";
            rtxtDisplayReport.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtxtDisplayReport.Size = new Size(508, 406);
            rtxtDisplayReport.TabIndex = 0;
            rtxtDisplayReport.Text = "";
            // 
            // txtSelectedAlias
            // 
            txtSelectedAlias.Enabled = false;
            txtSelectedAlias.Location = new Point(19, 24);
            txtSelectedAlias.Name = "txtSelectedAlias";
            txtSelectedAlias.PlaceholderText = "SelectedAlias";
            txtSelectedAlias.Size = new Size(163, 26);
            txtSelectedAlias.TabIndex = 5;
            txtSelectedAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // ShowReportForm
            // 
            AutoScaleDimensions = new SizeF(10F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(556, 539);
            Controls.Add(txtSelectedAlias);
            Controls.Add(btnCloseForm);
            Controls.Add(txtAliasCreator);
            Controls.Add(txtSubject);
            Controls.Add(txtDate);
            Controls.Add(rtxtDisplayReport);
            Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            Name = "ShowReportForm";
            Text = "Show Report";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public RichTextBox rtxtDisplayReport;
        public TextBox txtDate;
        public TextBox txtSubject;
        public TextBox txtAliasCreator;
        public Button btnCloseForm;
        private TextBox txtSelectedAlias;
    }
}