﻿namespace CRUD_System
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
            chkIsAdmin = new CheckBox();
            btnCancel = new Button();
            txtPhonenumber = new TextBox();
            txtZIPCode = new TextBox();
            txtAlias = new TextBox();
            txtSurname = new TextBox();
            txtName = new TextBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            txtCity = new TextBox();
            btnSaveEdit = new Button();
            SuspendLayout();
            // 
            // chkIsAdmin
            // 
            chkIsAdmin.AutoSize = true;
            chkIsAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkIsAdmin.Location = new Point(438, 103);
            chkIsAdmin.Name = "chkIsAdmin";
            chkIsAdmin.Size = new Size(97, 22);
            chkIsAdmin.TabIndex = 7;
            chkIsAdmin.Text = "isAdmin";
            chkIsAdmin.TextAlign = ContentAlignment.MiddleCenter;
            chkIsAdmin.UseVisualStyleBackColor = true;
            chkIsAdmin.CheckedChanged += chkIsAdmin_CheckedChanged;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = SystemColors.ActiveCaption;
            btnCancel.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(303, 145);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(104, 30);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtPhonenumber
            // 
            txtPhonenumber.Location = new Point(233, 103);
            txtPhonenumber.Name = "txtPhonenumber";
            txtPhonenumber.PlaceholderText = "Phonenumber";
            txtPhonenumber.Size = new Size(199, 23);
            txtPhonenumber.TabIndex = 6;
            txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            txtZIPCode.Location = new Point(233, 74);
            txtZIPCode.Name = "txtZIPCode";
            txtZIPCode.PlaceholderText = "ZIP Code";
            txtZIPCode.Size = new Size(199, 23);
            txtZIPCode.TabIndex = 3;
            txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            txtAlias.Enabled = false;
            txtAlias.Location = new Point(28, 16);
            txtAlias.Name = "txtAlias";
            txtAlias.PlaceholderText = "Alias";
            txtAlias.ReadOnly = true;
            txtAlias.Size = new Size(199, 23);
            txtAlias.TabIndex = 100;
            txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(233, 45);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(199, 23);
            txtSurname.TabIndex = 1;
            txtSurname.TextAlign = HorizontalAlignment.Center;
            txtSurname.TextChanged += TxtAlias_TextChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(28, 45);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Name";
            txtName.Size = new Size(199, 23);
            txtName.TabIndex = 0;
            txtName.TextAlign = HorizontalAlignment.Center;
            txtName.TextChanged += TxtAlias_TextChanged;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(28, 103);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "E-mail";
            txtEmail.Size = new Size(199, 23);
            txtEmail.TabIndex = 5;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(28, 74);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Adress";
            txtAddress.Size = new Size(199, 23);
            txtAddress.TabIndex = 2;
            txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(438, 74);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "City";
            txtCity.Size = new Size(199, 23);
            txtCity.TabIndex = 4;
            txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // btnSaveEdit
            // 
            btnSaveEdit.BackColor = Color.LightGreen;
            btnSaveEdit.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveEdit.Location = new Point(208, 145);
            btnSaveEdit.Name = "btnSaveEdit";
            btnSaveEdit.Size = new Size(89, 30);
            btnSaveEdit.TabIndex = 8;
            btnSaveEdit.Text = "Save";
            btnSaveEdit.UseVisualStyleBackColor = false;
            btnSaveEdit.Click += btnSaveEdit_Click;
            // 
            // CreateControlADMIN
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            Controls.Add(chkIsAdmin);
            Controls.Add(btnCancel);
            Controls.Add(txtPhonenumber);
            Controls.Add(txtZIPCode);
            Controls.Add(txtAlias);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(txtCity);
            Controls.Add(btnSaveEdit);
            Name = "CreateControlADMIN";
            Size = new Size(658, 198);
            ResumeLayout(false);
            PerformLayout();
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
