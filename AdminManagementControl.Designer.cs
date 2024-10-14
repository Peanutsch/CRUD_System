namespace CRUD_System
{
    partial class AdminManagementControl
    {
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtAdmin;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Button btnSaveEdit;
        private System.Windows.Forms.Button btnDeleteUser;

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

        private void InitializeComponent()
        {
            this.txtName = new TextBox();
            this.txtPassword = new TextBox();
            this.txtEmail = new TextBox();
            this.txtAddress = new TextBox();
            this.txtCity = new TextBox();
            this.listBoxUsers = new ListBox();
            this.btnCreateUser = new Button();
            this.btnSaveEdit = new Button();
            this.btnDeleteUser = new Button();
            this.txtSurname = new TextBox();
            this.txtAlias = new TextBox();
            this.txtZIPCode = new TextBox();
            this.txtPhonenumber = new TextBox();
            this.btnGenPSW = new Button();
            this.btnEdit = new Button();
            this.txtAdmin = new TextBox();
            this.chkIsAdmin = new CheckBox();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new Point(229, 255);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Name";
            this.txtName.Size = new Size(199, 23);
            this.txtName.TabIndex = 1;
            this.txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new Point(837, 368);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PlaceholderText = "Password";
            this.txtPassword.Size = new Size(150, 23);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.TextAlign = HorizontalAlignment.Center;
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Location = new Point(24, 313);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "E-mail";
            this.txtEmail.Size = new Size(199, 23);
            this.txtEmail.TabIndex = 6;
            this.txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new Point(24, 284);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PlaceholderText = "Adress";
            this.txtAddress.Size = new Size(199, 23);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            this.txtCity.Enabled = false;
            this.txtCity.Location = new Point(434, 284);
            this.txtCity.Name = "txtCity";
            this.txtCity.PlaceholderText = "City";
            this.txtCity.Size = new Size(199, 23);
            this.txtCity.TabIndex = 5;
            this.txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.ItemHeight = 15;
            this.listBoxUsers.Location = new Point(24, 20);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new Size(963, 229);
            this.listBoxUsers.TabIndex = 0;
            this.listBoxUsers.SelectedIndexChanged += this.ListBoxUsers_SelectedIndexChanged;
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnCreateUser.Location = new Point(681, 291);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new Size(150, 30);
            this.btnCreateUser.TabIndex = 11;
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.Click += this.btnAddUser_Click;
            // 
            // btnSaveEdit
            // 
            this.btnSaveEdit.BackColor = Color.LightGreen;
            this.btnSaveEdit.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveEdit.Location = new Point(837, 255);
            this.btnSaveEdit.Name = "btnSaveEdit";
            this.btnSaveEdit.Size = new Size(150, 30);
            this.btnSaveEdit.TabIndex = 10;
            this.btnSaveEdit.Text = "Save Edit";
            this.btnSaveEdit.UseVisualStyleBackColor = false;
            this.btnSaveEdit.Visible = false;
            this.btnSaveEdit.Click += this.btnSaveEdit_Click;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnDeleteUser.Location = new Point(681, 327);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new Size(150, 30);
            this.btnDeleteUser.TabIndex = 9;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.Click += this.btnDeleteUser_Click;
            // 
            // txtSurname
            // 
            this.txtSurname.Enabled = false;
            this.txtSurname.Location = new Point(434, 255);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.PlaceholderText = "Surname";
            this.txtSurname.Size = new Size(199, 23);
            this.txtSurname.TabIndex = 2;
            this.txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            this.txtAlias.Enabled = false;
            this.txtAlias.Location = new Point(24, 255);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.PlaceholderText = "Alias";
            this.txtAlias.ReadOnly = true;
            this.txtAlias.Size = new Size(199, 23);
            this.txtAlias.TabIndex = 20;
            this.txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            this.txtZIPCode.Enabled = false;
            this.txtZIPCode.Location = new Point(229, 284);
            this.txtZIPCode.Name = "txtZIPCode";
            this.txtZIPCode.PlaceholderText = "ZIP Code";
            this.txtZIPCode.Size = new Size(199, 23);
            this.txtZIPCode.TabIndex = 4;
            this.txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhonenumber
            // 
            this.txtPhonenumber.Enabled = false;
            this.txtPhonenumber.Location = new Point(229, 313);
            this.txtPhonenumber.Name = "txtPhonenumber";
            this.txtPhonenumber.PlaceholderText = "Phonenumber";
            this.txtPhonenumber.Size = new Size(199, 23);
            this.txtPhonenumber.TabIndex = 21;
            this.txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            // 
            // btnGenPSW
            // 
            this.btnGenPSW.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnGenPSW.Location = new Point(681, 363);
            this.btnGenPSW.Name = "btnGenPSW";
            this.btnGenPSW.Size = new Size(150, 30);
            this.btnGenPSW.TabIndex = 22;
            this.btnGenPSW.Text = "Send New PSW";
            this.btnGenPSW.Click += this.btnGenPSW_Click;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = SystemColors.ActiveCaption;
            this.btnEdit.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnEdit.Location = new Point(681, 255);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(150, 30);
            this.btnEdit.TabIndex = 25;
            this.btnEdit.Text = "Edit User";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += this.btnEdit_Click;
            // 
            // txtAdmin
            // 
            this.txtAdmin.BackColor = Color.LightGreen;
            this.txtAdmin.Enabled = false;
            this.txtAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtAdmin.Location = new Point(434, 313);
            this.txtAdmin.Multiline = true;
            this.txtAdmin.Name = "txtAdmin";
            this.txtAdmin.Size = new Size(199, 23);
            this.txtAdmin.TabIndex = 26;
            this.txtAdmin.Text = "User is ADMIN";
            this.txtAdmin.TextAlign = HorizontalAlignment.Center;
            this.txtAdmin.Visible = false;
            // 
            // chkIsAdmin
            // 
            this.chkIsAdmin.AutoSize = true;
            this.chkIsAdmin.CheckAlign = ContentAlignment.MiddleRight;
            this.chkIsAdmin.Enabled = false;
            this.chkIsAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.chkIsAdmin.Location = new Point(536, 342);
            this.chkIsAdmin.Name = "chkIsAdmin";
            this.chkIsAdmin.Size = new Size(97, 22);
            this.chkIsAdmin.TabIndex = 27;
            this.chkIsAdmin.Text = "isAdmin";
            this.chkIsAdmin.UseVisualStyleBackColor = true;
            this.chkIsAdmin.Visible = false;
            this.chkIsAdmin.CheckedChanged += this.chkIsAdmin_CheckedChanged;
            // 
            // AdminManagementControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.chkIsAdmin);
            this.Controls.Add(this.txtAdmin);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnGenPSW);
            this.Controls.Add(this.txtPhonenumber);
            this.Controls.Add(this.txtZIPCode);
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.btnCreateUser);
            this.Controls.Add(this.btnSaveEdit);
            this.Controls.Add(this.btnDeleteUser);
            this.Name = "AdminManagementControl";
            this.Size = new Size(1014, 410);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private TextBox txtSurname;
        private TextBox txtAlias;
        private TextBox txtZIPCode;
        private TextBox txtPhonenumber;
        private Button btnGenPSW;
        private Button btnEdit;
        private CheckBox chkIsAdmin;
    }
}
