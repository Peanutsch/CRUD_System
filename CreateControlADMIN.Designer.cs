namespace CRUD_System
{
    partial class CreateControlADMIN
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
            this.chkIsAdmin.Location = new Point(438, 103);
            this.chkIsAdmin.Name = "chkIsAdmin";
            this.chkIsAdmin.Size = new Size(97, 22);
            this.chkIsAdmin.TabIndex = 43;
            this.chkIsAdmin.Text = "isAdmin";
            this.chkIsAdmin.TextAlign = ContentAlignment.MiddleCenter;
            this.chkIsAdmin.UseVisualStyleBackColor = true;
            this.chkIsAdmin.CheckedChanged += this.chkIsAdmin_CheckedChanged;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = SystemColors.ActiveCaption;
            this.btnCancel.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCancel.Location = new Point(303, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(104, 30);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += this.btnCancel_Click;
            // 
            // txtPhonenumber
            // 
            this.txtPhonenumber.Location = new Point(233, 103);
            this.txtPhonenumber.Name = "txtPhonenumber";
            this.txtPhonenumber.PlaceholderText = "Phonenumber";
            this.txtPhonenumber.Size = new Size(199, 23);
            this.txtPhonenumber.TabIndex = 39;
            this.txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            this.txtZIPCode.Location = new Point(233, 74);
            this.txtZIPCode.Name = "txtZIPCode";
            this.txtZIPCode.PlaceholderText = "ZIP Code";
            this.txtZIPCode.Size = new Size(199, 23);
            this.txtZIPCode.TabIndex = 31;
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
            this.txtAlias.TabIndex = 38;
            this.txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new Point(233, 45);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.PlaceholderText = "Surname";
            this.txtSurname.Size = new Size(199, 23);
            this.txtSurname.TabIndex = 29;
            this.txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtName
            // 
            this.txtName.Location = new Point(28, 45);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Name";
            this.txtName.Size = new Size(199, 23);
            this.txtName.TabIndex = 28;
            this.txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new Point(28, 103);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "E-mail";
            this.txtEmail.Size = new Size(199, 23);
            this.txtEmail.TabIndex = 33;
            this.txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new Point(28, 74);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PlaceholderText = "Adress";
            this.txtAddress.Size = new Size(199, 23);
            this.txtAddress.TabIndex = 30;
            this.txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            this.txtCity.Location = new Point(438, 74);
            this.txtCity.Name = "txtCity";
            this.txtCity.PlaceholderText = "City";
            this.txtCity.Size = new Size(199, 23);
            this.txtCity.TabIndex = 32;
            this.txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // btnSaveEdit
            // 
            this.btnSaveEdit.BackColor = Color.LightGreen;
            this.btnSaveEdit.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveEdit.Location = new Point(208, 145);
            this.btnSaveEdit.Name = "btnSaveEdit";
            this.btnSaveEdit.Size = new Size(89, 30);
            this.btnSaveEdit.TabIndex = 36;
            this.btnSaveEdit.Text = "Save";
            this.btnSaveEdit.UseVisualStyleBackColor = false;
            this.btnSaveEdit.Visible = true;
            this.btnSaveEdit.Click += this.btnSaveEdit_Click;
            // 
            // CreateControlADMIN
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
            this.Name = "CreateControlADMIN";
            this.Size = new Size(658, 198);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private CheckBox chkIsAdmin;
        private Button btnCancel;
        private TextBox txtPhonenumber;
        private TextBox txtZIPCode;
        private TextBox txtAlias;
        private TextBox txtSurname;
        private TextBox txtName;
        private TextBox txtEmail;
        private TextBox txtAddress;
        private TextBox txtCity;
        private Button btnSaveEdit;
    }
}
