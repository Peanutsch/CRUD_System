namespace CRUD_System
{
    partial class AdminMainControl
    {
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.TextBox txtEmail;
        public System.Windows.Forms.TextBox txtAddress;
        public System.Windows.Forms.TextBox txtCity;
        public System.Windows.Forms.TextBox txtAdmin;
        public System.Windows.Forms.ListBox listBoxAdmin;
        public System.Windows.Forms.Button btnCreateUser;
        public System.Windows.Forms.Button btnSaveEditUserDetails;
        public System.Windows.Forms.Button btnDeleteUser;

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
            this.txtEmail = new TextBox();
            this.txtAddress = new TextBox();
            this.txtCity = new TextBox();
            this.listBoxAdmin = new ListBox();
            this.btnCreateUser = new Button();
            this.btnSaveEditUserDetails = new Button();
            this.btnDeleteUser = new Button();
            this.txtSurname = new TextBox();
            this.txtAlias = new TextBox();
            this.txtZIPCode = new TextBox();
            this.txtPhonenumber = new TextBox();
            this.btnGeneratePSW = new Button();
            this.btnEditUserDetails = new Button();
            this.txtAdmin = new TextBox();
            this.chkIsAdmin = new CheckBox();
            this.btnChangePassword = new Button();
            this.btnForceLogOutUser = new Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new Point(283, 269);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Name";
            this.txtName.Size = new Size(325, 23);
            this.txtName.TabIndex = 1;
            this.txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Location = new Point(283, 326);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "E-mail";
            this.txtEmail.Size = new Size(443, 23);
            this.txtEmail.TabIndex = 6;
            this.txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new Point(283, 298);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PlaceholderText = "Adress";
            this.txtAddress.Size = new Size(325, 23);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            this.txtCity.Enabled = false;
            this.txtCity.Location = new Point(732, 297);
            this.txtCity.Name = "txtCity";
            this.txtCity.PlaceholderText = "City";
            this.txtCity.Size = new Size(207, 23);
            this.txtCity.TabIndex = 5;
            this.txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // listBoxAdmin
            // 
            this.listBoxAdmin.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxAdmin.ItemHeight = 15;
            this.listBoxAdmin.Location = new Point(24, 35);
            this.listBoxAdmin.Name = "listBoxAdmin";
            this.listBoxAdmin.Size = new Size(915, 229);
            this.listBoxAdmin.TabIndex = 0;
            this.listBoxAdmin.DrawItem += this.ListBoxAdmin_DrawItem;
            this.listBoxAdmin.SelectedIndexChanged += this.ListBoxAdmin_SelectedIndexChanged;
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnCreateUser.Location = new Point(583, 355);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new Size(200, 30);
            this.btnCreateUser.TabIndex = 11;
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.Click += this.btnCreateUser_Click;
            // 
            // btnSaveEditUserDetails
            // 
            this.btnSaveEditUserDetails.BackColor = Color.LightGreen;
            this.btnSaveEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveEditUserDetails.Location = new Point(24, 298);
            this.btnSaveEditUserDetails.Name = "btnSaveEditUserDetails";
            this.btnSaveEditUserDetails.Size = new Size(150, 30);
            this.btnSaveEditUserDetails.TabIndex = 9;
            this.btnSaveEditUserDetails.Text = "Save Edit";
            this.btnSaveEditUserDetails.UseVisualStyleBackColor = false;
            this.btnSaveEditUserDetails.Visible = false;
            this.btnSaveEditUserDetails.Click += this.btnSaveEditUserDetails_Click;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnDeleteUser.Location = new Point(789, 355);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new Size(150, 30);
            this.btnDeleteUser.TabIndex = 9;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.Click += this.btnDeleteUser_Click;
            // 
            // txtSurname
            // 
            this.txtSurname.Enabled = false;
            this.txtSurname.Location = new Point(614, 269);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.PlaceholderText = "Surname";
            this.txtSurname.Size = new Size(325, 23);
            this.txtSurname.TabIndex = 2;
            this.txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            this.txtAlias.Enabled = false;
            this.txtAlias.Location = new Point(180, 270);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.PlaceholderText = "Alias";
            this.txtAlias.ReadOnly = true;
            this.txtAlias.Size = new Size(97, 23);
            this.txtAlias.TabIndex = 20;
            this.txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            this.txtZIPCode.Enabled = false;
            this.txtZIPCode.Location = new Point(614, 297);
            this.txtZIPCode.Name = "txtZIPCode";
            this.txtZIPCode.PlaceholderText = "ZIP Code";
            this.txtZIPCode.Size = new Size(112, 23);
            this.txtZIPCode.TabIndex = 4;
            this.txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhonenumber
            // 
            this.txtPhonenumber.Enabled = false;
            this.txtPhonenumber.Location = new Point(732, 326);
            this.txtPhonenumber.Name = "txtPhonenumber";
            this.txtPhonenumber.PlaceholderText = "Phonenumber";
            this.txtPhonenumber.Size = new Size(207, 23);
            this.txtPhonenumber.TabIndex = 7;
            this.txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            // 
            // btnGeneratePSW
            // 
            this.btnGeneratePSW.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnGeneratePSW.Location = new Point(427, 355);
            this.btnGeneratePSW.Name = "btnGeneratePSW";
            this.btnGeneratePSW.Size = new Size(150, 30);
            this.btnGeneratePSW.TabIndex = 22;
            this.btnGeneratePSW.Text = "Gen. Password";
            this.btnGeneratePSW.Click += this.btnGeneratePassword_Click;
            // 
            // btnEditUserDetails
            // 
            this.btnEditUserDetails.BackColor = SystemColors.ActiveCaption;
            this.btnEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnEditUserDetails.Location = new Point(24, 269);
            this.btnEditUserDetails.Name = "btnEditUserDetails";
            this.btnEditUserDetails.Size = new Size(150, 30);
            this.btnEditUserDetails.TabIndex = 25;
            this.btnEditUserDetails.Text = "Edit Details";
            this.btnEditUserDetails.UseVisualStyleBackColor = false;
            this.btnEditUserDetails.Click += this.btnEditUserDetails_Click;
            // 
            // txtAdmin
            // 
            this.txtAdmin.BackColor = Color.LightGreen;
            this.txtAdmin.Enabled = false;
            this.txtAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtAdmin.Location = new Point(180, 298);
            this.txtAdmin.Multiline = true;
            this.txtAdmin.Name = "txtAdmin";
            this.txtAdmin.Size = new Size(97, 23);
            this.txtAdmin.TabIndex = 26;
            this.txtAdmin.Text = "Admin";
            this.txtAdmin.TextAlign = HorizontalAlignment.Center;
            this.txtAdmin.Visible = false;
            // 
            // chkIsAdmin
            // 
            this.chkIsAdmin.AutoSize = true;
            this.chkIsAdmin.CheckAlign = ContentAlignment.MiddleRight;
            this.chkIsAdmin.Enabled = false;
            this.chkIsAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.chkIsAdmin.Location = new Point(180, 327);
            this.chkIsAdmin.Name = "chkIsAdmin";
            this.chkIsAdmin.Size = new Size(97, 22);
            this.chkIsAdmin.TabIndex = 8;
            this.chkIsAdmin.Text = "isAdmin";
            this.chkIsAdmin.TextAlign = ContentAlignment.MiddleRight;
            this.chkIsAdmin.UseVisualStyleBackColor = true;
            this.chkIsAdmin.Visible = false;
            this.chkIsAdmin.CheckedChanged += this.chkIsAdmin_CheckedChanged;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = SystemColors.ActiveCaption;
            this.btnChangePassword.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnChangePassword.Location = new Point(24, 3);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new Size(200, 30);
            this.btnChangePassword.TabIndex = 31;
            this.btnChangePassword.Text = "Change own Password";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += this.btnChangePassword_Click;
            // 
            // btnForceLogOutUser
            // 
            this.btnForceLogOutUser.BackColor = SystemColors.ActiveCaption;
            this.btnForceLogOutUser.Enabled = false;
            this.btnForceLogOutUser.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnForceLogOutUser.Location = new Point(283, 355);
            this.btnForceLogOutUser.Name = "btnForceLogOutUser";
            this.btnForceLogOutUser.Size = new Size(138, 30);
            this.btnForceLogOutUser.TabIndex = 32;
            this.btnForceLogOutUser.Text = "Force LogOut";
            this.btnForceLogOutUser.UseVisualStyleBackColor = false;
            this.btnForceLogOutUser.Visible = false;
            this.btnForceLogOutUser.Click += this.btnForceLogOutUser_Click;
            // 
            // AdminMainControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.btnForceLogOutUser);
            this.Controls.Add(this.btnCreateUser);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.chkIsAdmin);
            this.Controls.Add(this.txtAdmin);
            this.Controls.Add(this.btnEditUserDetails);
            this.Controls.Add(this.btnGeneratePSW);
            this.Controls.Add(this.txtPhonenumber);
            this.Controls.Add(this.txtZIPCode);
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.listBoxAdmin);
            this.Controls.Add(this.btnSaveEditUserDetails);
            this.Controls.Add(this.btnDeleteUser);
            this.Name = "AdminMainControl";
            this.Size = new Size(968, 391);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        public TextBox txtSurname;
        public TextBox txtAlias;
        public TextBox txtZIPCode;
        public TextBox txtPhonenumber;
        public Button btnGeneratePSW;
        public Button btnEditUserDetails;
        public CheckBox chkIsAdmin;
        public Button btnChangePassword;
        public Button btnForceLogOutUser;
    }
}
