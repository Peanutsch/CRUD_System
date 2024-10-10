namespace CRUD_System
{
    partial class AdminManagementControl
    {
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnUpdateUser;
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
            txtName = new TextBox();
            txtPassword = new TextBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            txtCity = new TextBox();
            listBoxUsers = new ListBox();
            btnAddUser = new Button();
            btnUpdateUser = new Button();
            btnDeleteUser = new Button();
            txtSurname = new TextBox();
            txtAlias = new TextBox();
            txtZIPCode = new TextBox();
            txtPhonenumber = new TextBox();
            btnGenPSW = new Button();
            txtAdmin = new TextBox();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(229, 255);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Name";
            txtName.Size = new Size(199, 23);
            txtName.TabIndex = 1;
            txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(681, 327);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password";
            txtPassword.Size = new Size(150, 23);
            txtPassword.TabIndex = 8;
            txtPassword.TextAlign = HorizontalAlignment.Center;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(24, 313);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "E-mail";
            txtEmail.Size = new Size(199, 23);
            txtEmail.TabIndex = 6;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(24, 284);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Adress";
            txtAddress.Size = new Size(199, 23);
            txtAddress.TabIndex = 3;
            txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(434, 284);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "City";
            txtCity.Size = new Size(199, 23);
            txtCity.TabIndex = 5;
            txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // listBoxUsers
            // 
            listBoxUsers.ItemHeight = 15;
            listBoxUsers.Location = new Point(24, 20);
            listBoxUsers.Name = "listBoxUsers";
            listBoxUsers.Size = new Size(963, 229);
            listBoxUsers.TabIndex = 0;
            listBoxUsers.SelectedIndexChanged += ListBoxUsers_SelectedIndexChanged;
            // 
            // btnAddUser
            // 
            btnAddUser.Location = new Point(837, 255);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(150, 30);
            btnAddUser.TabIndex = 11;
            btnAddUser.Text = "Add User";
            btnAddUser.Click += btnAddUser_Click;
            // 
            // btnUpdateUser
            // 
            btnUpdateUser.Location = new Point(681, 255);
            btnUpdateUser.Name = "btnUpdateUser";
            btnUpdateUser.Size = new Size(150, 30);
            btnUpdateUser.TabIndex = 10;
            btnUpdateUser.Text = "Update User";
            btnUpdateUser.Click += btnUpdateUser_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Location = new Point(837, 291);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(150, 30);
            btnDeleteUser.TabIndex = 9;
            btnDeleteUser.Text = "Delete User";
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(434, 255);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(199, 23);
            txtSurname.TabIndex = 2;
            txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            txtAlias.Enabled = false;
            txtAlias.Location = new Point(24, 255);
            txtAlias.Name = "txtAlias";
            txtAlias.PlaceholderText = "Alias";
            txtAlias.ReadOnly = true;
            txtAlias.Size = new Size(199, 23);
            txtAlias.TabIndex = 20;
            txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            txtZIPCode.Location = new Point(229, 284);
            txtZIPCode.Name = "txtZIPCode";
            txtZIPCode.PlaceholderText = "ZIP Code";
            txtZIPCode.Size = new Size(199, 23);
            txtZIPCode.TabIndex = 4;
            txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhonenumber
            // 
            txtPhonenumber.Location = new Point(229, 313);
            txtPhonenumber.Name = "txtPhonenumber";
            txtPhonenumber.PlaceholderText = "Phonenumber";
            txtPhonenumber.Size = new Size(199, 23);
            txtPhonenumber.TabIndex = 21;
            txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            // 
            // btnGenPSW
            // 
            btnGenPSW.Location = new Point(681, 291);
            btnGenPSW.Name = "btnGenPSW";
            btnGenPSW.Size = new Size(150, 30);
            btnGenPSW.TabIndex = 22;
            btnGenPSW.Text = "Generate PSW";
            btnGenPSW.Click += btnGenPSW_Click;
            // 
            // txtAdmin
            // 
            txtAdmin.BackColor = Color.FromArgb(128, 255, 128);
            txtAdmin.Enabled = false;
            txtAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAdmin.Location = new Point(434, 313);
            txtAdmin.Name = "txtAdmin";
            txtAdmin.Size = new Size(199, 26);
            txtAdmin.TabIndex = 24;
            txtAdmin.TabStop = false;
            txtAdmin.Text = "ADMIN";
            txtAdmin.TextAlign = HorizontalAlignment.Center;
            txtAdmin.Visible = false;
            // 
            // AdminManagementControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(txtAdmin);
            Controls.Add(btnGenPSW);
            Controls.Add(txtPhonenumber);
            Controls.Add(txtZIPCode);
            Controls.Add(txtAlias);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(txtCity);
            Controls.Add(listBoxUsers);
            Controls.Add(btnAddUser);
            Controls.Add(btnUpdateUser);
            Controls.Add(btnDeleteUser);
            Name = "AdminManagementControl";
            Size = new Size(1014, 396);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSurname;
        private TextBox txtAlias;
        private TextBox txtZIPCode;
        private TextBox txtPhonenumber;
        private Button btnGenPSW;
        private TextBox txtAdmin;
    }
}
