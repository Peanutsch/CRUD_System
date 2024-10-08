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
            this.txtName = new TextBox();
            this.txtPassword = new TextBox();
            this.chkIsAdmin = new CheckBox();
            this.txtEmail = new TextBox();
            this.txtAddress = new TextBox();
            this.txtCity = new TextBox();
            this.listBoxUsers = new ListBox();
            this.btnAddUser = new Button();
            this.btnUpdateUser = new Button();
            this.btnDeleteUser = new Button();
            this.txtSurname = new TextBox();
            this.txtAlias = new TextBox();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new Point(133, 20);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Name";
            this.txtName.Size = new Size(150, 23);
            this.txtName.TabIndex = 0;
            this.txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new Point(133, 106);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PlaceholderText = "Password";
            this.txtPassword.Size = new Size(150, 23);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TextAlign = HorizontalAlignment.Center;
            // 
            // chkIsAdmin
            // 
            this.chkIsAdmin.Location = new Point(133, 135);
            this.chkIsAdmin.Name = "chkIsAdmin";
            this.chkIsAdmin.Size = new Size(80, 24);
            this.chkIsAdmin.TabIndex = 2;
            this.chkIsAdmin.Text = "Is Admin";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new Point(133, 165);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "E-mail";
            this.txtEmail.Size = new Size(150, 23);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new Point(133, 194);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PlaceholderText = "Adress";
            this.txtAddress.Size = new Size(150, 23);
            this.txtAddress.TabIndex = 4;
            this.txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            this.txtCity.Location = new Point(133, 223);
            this.txtCity.Name = "txtCity";
            this.txtCity.PlaceholderText = "City";
            this.txtCity.Size = new Size(150, 23);
            this.txtCity.TabIndex = 5;
            this.txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.ItemHeight = 15;
            this.listBoxUsers.Location = new Point(324, 20);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new Size(462, 199);
            this.listBoxUsers.TabIndex = 6;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new Point(636, 223);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new Size(150, 30);
            this.btnAddUser.TabIndex = 7;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.Click += this.btnAddUser_Click;
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.Location = new Point(480, 223);
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Size = new Size(150, 30);
            this.btnUpdateUser.TabIndex = 8;
            this.btnUpdateUser.Text = "Update User";
            this.btnUpdateUser.Click += this.btnUpdateUser_Click;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new Point(324, 223);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new Size(150, 30);
            this.btnDeleteUser.TabIndex = 9;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.Click += this.btnDeleteUser_Click;
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new Point(133, 78);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.PlaceholderText = "Surname";
            this.txtSurname.Size = new Size(150, 23);
            this.txtSurname.TabIndex = 10;
            this.txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new Point(133, 49);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.PlaceholderText = "Alias";
            this.txtAlias.ReadOnly = true;
            this.txtAlias.Size = new Size(150, 23);
            this.txtAlias.TabIndex = 11;
            this.txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // UserManagementControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkIsAdmin);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.btnUpdateUser);
            this.Controls.Add(this.btnDeleteUser);
            this.Name = "UserManagementControl";
            this.Size = new Size(863, 390);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TextBox txtSurname;
        private TextBox txtAlias;
    }
}
