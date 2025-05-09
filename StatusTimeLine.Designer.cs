namespace CRUD_System
{
    partial class StatusTimeLine
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
            this.listBoxStatusTimeLine = new ListBox();
            this.SuspendLayout();
            // 
            // listBoxStatusTimeLine
            // 
            this.listBoxStatusTimeLine.FormattingEnabled = true;
            this.listBoxStatusTimeLine.ItemHeight = 18;
            this.listBoxStatusTimeLine.Location = new Point(247, 133);
            this.listBoxStatusTimeLine.Name = "listBoxStatusTimeLine";
            this.listBoxStatusTimeLine.Size = new Size(758, 166);
            this.listBoxStatusTimeLine.TabIndex = 0;
            // 
            // StatusTimeLine
            // 
            this.AutoScaleDimensions = new SizeF(10F, 18F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.ClientSize = new Size(1143, 540);
            this.Controls.Add(this.listBoxStatusTimeLine);
            this.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Margin = new Padding(4, 4, 4, 4);
            this.Name = "StatusTimeLine";
            this.Text = "StatusTimeLine";
            this.ResumeLayout(false);
        }

        #endregion

        public ListBox listBoxStatusTimeLine;
    }
}