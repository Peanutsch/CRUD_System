namespace CRUD_System
{
    partial class UserManagementControl
    {
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkIsAdmin;
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
            chkIsAdmin = new CheckBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            txtCity = new TextBox();
            listBoxUsers = new ListBox();
            btnAddUser = new Button();
            btnUpdateUser = new Button();
            btnDeleteUser = new Button();
            txtSurname = new TextBox();
            txtAlias = new TextBox();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(133, 20);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Name";
            txtName.Size = new Size(150, 23);
            txtName.TabIndex = 0;
            txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(133, 106);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password";
            txtPassword.Size = new Size(150, 23);
            txtPassword.TabIndex = 3;
            txtPassword.TextAlign = HorizontalAlignment.Center;
            // 
            // chkIsAdmin
            // 
            chkIsAdmin.Location = new Point(133, 135);
            chkIsAdmin.Name = "chkIsAdmin";
            chkIsAdmin.Size = new Size(80, 24);
            chkIsAdmin.TabIndex = 4;
            chkIsAdmin.Text = "Is Admin";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(133, 165);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "E-mail";
            txtEmail.Size = new Size(150, 23);
            txtEmail.TabIndex = 5;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(133, 194);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Adress";
            txtAddress.Size = new Size(150, 23);
            txtAddress.TabIndex = 6;
            txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(133, 223);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "City";
            txtCity.Size = new Size(150, 23);
            txtCity.TabIndex = 7;
            txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // listBoxUsers
            // 
            listBoxUsers.ItemHeight = 15;
            listBoxUsers.Location = new Point(324, 20);
            listBoxUsers.Name = "listBoxUsers";
            listBoxUsers.Size = new Size(462, 199);
            listBoxUsers.TabIndex = 8;
            // 
            // btnAddUser
            // 
            btnAddUser.Location = new Point(636, 223);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(150, 30);
            btnAddUser.TabIndex = 11;
            btnAddUser.Text = "Add User";
            btnAddUser.Click += btnAddUser_Click;
            // 
            // btnUpdateUser
            // 
            btnUpdateUser.Location = new Point(480, 223);
            btnUpdateUser.Name = "btnUpdateUser";
            btnUpdateUser.Size = new Size(150, 30);
            btnUpdateUser.TabIndex = 10;
            btnUpdateUser.Text = "Update User";
            btnUpdateUser.Click += btnUpdateUser_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Location = new Point(324, 223);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(150, 30);
            btnDeleteUser.TabIndex = 9;
            btnDeleteUser.Text = "Delete User";
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(133, 78);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(150, 23);
            txtSurname.TabIndex = 2;
            txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            txtAlias.Location = new Point(133, 49);
            txtAlias.Name = "txtAlias";
            txtAlias.PlaceholderText = "Alias";
            txtAlias.ReadOnly = true;
            txtAlias.Size = new Size(150, 23);
            txtAlias.TabIndex = 11;
            txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // UserManagementControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(txtAlias);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Controls.Add(txtPassword);
            Controls.Add(chkIsAdmin);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(txtCity);
            Controls.Add(listBoxUsers);
            Controls.Add(btnAddUser);
            Controls.Add(btnUpdateUser);
            Controls.Add(btnDeleteUser);
            Name = "UserManagementControl";
            Size = new Size(863, 390);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSurname;
        private TextBox txtAlias;
    }
}
