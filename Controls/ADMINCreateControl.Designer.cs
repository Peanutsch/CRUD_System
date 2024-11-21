namespace CRUD_System
{
    partial class AdminCreateControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkIsAdmin = new CheckBox();
            this.btnCancel = new Button();
            this.txtPhonenumber = new TextBox();
            this.txtZIPCode = new TextBox();
            this.txtAlias = new TextBox();
            this.txtSurname = new TextBox();
            this.txtName = new TextBox();
            this.txtEmail = new TextBox();
            this.txtAddress = new TextBox();
            this.txtCity = new TextBox();
            this.btnSaveEdit = new Button();
            this.SuspendLayout();
            // 
            // chkIsAdmin
            // 
            this.chkIsAdmin.AutoSize = true;
            this.chkIsAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.chkIsAdmin.Location = new Point(494, 137);
            this.chkIsAdmin.Name = "chkIsAdmin";
            this.chkIsAdmin.Size = new Size(97, 22);
            this.chkIsAdmin.TabIndex = 7;
            this.chkIsAdmin.Text = "isAdmin";
            this.chkIsAdmin.TextAlign = ContentAlignment.MiddleCenter;
            this.chkIsAdmin.UseVisualStyleBackColor = true;
            this.chkIsAdmin.CheckedChanged += this.chkIsAdmin_CheckedChanged;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = SystemColors.ActiveCaption;
            this.btnCancel.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCancel.Location = new Point(384, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(104, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += this.btnCancel_Click;
            // 
            // txtPhonenumber
            // 
            this.txtPhonenumber.Location = new Point(384, 103);
            this.txtPhonenumber.Name = "txtPhonenumber";
            this.txtPhonenumber.PlaceholderText = "Phonenumber";
            this.txtPhonenumber.Size = new Size(350, 23);
            this.txtPhonenumber.TabIndex = 6;
            this.txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            this.txtZIPCode.Location = new Point(384, 74);
            this.txtZIPCode.Name = "txtZIPCode";
            this.txtZIPCode.PlaceholderText = "ZIP Code";
            this.txtZIPCode.Size = new Size(85, 23);
            this.txtZIPCode.TabIndex = 3;
            this.txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            this.txtAlias.Enabled = false;
            this.txtAlias.Location = new Point(28, 16);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.PlaceholderText = "Alias";
            this.txtAlias.ReadOnly = true;
            this.txtAlias.Size = new Size(199, 23);
            this.txtAlias.TabIndex = 100;
            this.txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new Point(384, 45);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.PlaceholderText = "Surname";
            this.txtSurname.Size = new Size(350, 23);
            this.txtSurname.TabIndex = 1;
            this.txtSurname.TextAlign = HorizontalAlignment.Center;
            this.txtSurname.TextChanged += this.TxtAlias_TextChanged;
            // 
            // txtName
            // 
            this.txtName.Location = new Point(28, 45);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Name";
            this.txtName.Size = new Size(350, 23);
            this.txtName.TabIndex = 0;
            this.txtName.TextAlign = HorizontalAlignment.Center;
            this.txtName.TextChanged += this.TxtAlias_TextChanged;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new Point(28, 103);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "E-mail";
            this.txtEmail.Size = new Size(350, 23);
            this.txtEmail.TabIndex = 5;
            this.txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new Point(28, 74);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PlaceholderText = "Adress";
            this.txtAddress.Size = new Size(350, 23);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            this.txtCity.Location = new Point(475, 74);
            this.txtCity.Name = "txtCity";
            this.txtCity.PlaceholderText = "City";
            this.txtCity.Size = new Size(259, 23);
            this.txtCity.TabIndex = 4;
            this.txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // btnSaveEdit
            // 
            this.btnSaveEdit.BackColor = Color.LightGreen;
            this.btnSaveEdit.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveEdit.Location = new Point(289, 132);
            this.btnSaveEdit.Name = "btnSaveEdit";
            this.btnSaveEdit.Size = new Size(89, 30);
            this.btnSaveEdit.TabIndex = 8;
            this.btnSaveEdit.Text = "Save";
            this.btnSaveEdit.UseVisualStyleBackColor = false;
            this.btnSaveEdit.Click += this.btnSaveEdit_Click;
            // 
            // AdminCreateControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.Controls.Add(this.chkIsAdmin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtPhonenumber);
            this.Controls.Add(this.txtZIPCode);
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.btnSaveEdit);
            this.Name = "AdminCreateControl";
            this.Size = new Size(896, 220);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        public CheckBox chkIsAdmin;
        public Button btnCancel;
        public TextBox txtPhonenumber;
        public TextBox txtZIPCode;
        public TextBox txtAlias;
        public TextBox txtSurname;
        public TextBox txtName;
        public TextBox txtEmail;
        public TextBox txtAddress;
        public TextBox txtCity;
        public Button btnSaveEdit;
    }
}
