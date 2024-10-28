using System.Xml.Linq;

namespace CRUD_System
{
    partial class USERSMainControl
    {
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtAdmin;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Button btnSaveEditUserDetails;
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtName = new TextBox();
            txtPassword = new TextBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            txtCity = new TextBox();
            btnCreateUser = new Button();
            btnSaveEditUserDetails = new Button();
            btnDeleteUser = new Button();
            txtSurname = new TextBox();
            txtAlias = new TextBox();
            txtZIPCode = new TextBox();
            txtPhonenumber = new TextBox();
            btnGeneratePSW = new Button();
            btnEditUserDetails = new Button();
            txtAdmin = new TextBox();
            chkIsAdmin = new CheckBox();
            listBoxUsers = new ListBox();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Enabled = false;
            txtName.Location = new Point(24, 87);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Name";
            txtName.Size = new Size(199, 23);
            txtName.TabIndex = 1;
            txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(0, 0);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 0;
            // 
            // txtEmail
            // 
            txtEmail.Enabled = false;
            txtEmail.Location = new Point(24, 145);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "E-mail";
            txtEmail.Size = new Size(199, 23);
            txtEmail.TabIndex = 6;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            txtAddress.Enabled = false;
            txtAddress.Location = new Point(24, 116);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Adress";
            txtAddress.Size = new Size(199, 23);
            txtAddress.TabIndex = 3;
            txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            txtCity.Enabled = false;
            txtCity.Location = new Point(434, 116);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "City";
            txtCity.Size = new Size(199, 23);
            txtCity.TabIndex = 5;
            txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCreateUser
            // 
            btnCreateUser.Location = new Point(0, 0);
            btnCreateUser.Name = "btnCreateUser";
            btnCreateUser.Size = new Size(75, 23);
            btnCreateUser.TabIndex = 0;
            // 
            // btnSaveEditUserDetails
            // 
            btnSaveEditUserDetails.BackColor = Color.LightGreen;
            btnSaveEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveEditUserDetails.Location = new Point(483, 174);
            btnSaveEditUserDetails.Name = "btnSaveEditUserDetails";
            btnSaveEditUserDetails.Size = new Size(150, 30);
            btnSaveEditUserDetails.TabIndex = 10;
            btnSaveEditUserDetails.Text = "Save Edit";
            btnSaveEditUserDetails.UseVisualStyleBackColor = false;
            btnSaveEditUserDetails.Visible = false;
            btnSaveEditUserDetails.Click += btnSaveEditUserDetails_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Location = new Point(0, 0);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(75, 23);
            btnDeleteUser.TabIndex = 0;
            // 
            // txtSurname
            // 
            txtSurname.Enabled = false;
            txtSurname.Location = new Point(229, 87);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(199, 23);
            txtSurname.TabIndex = 2;
            txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            txtAlias.Enabled = false;
            txtAlias.Location = new Point(24, 58);
            txtAlias.Name = "txtAlias";
            txtAlias.PlaceholderText = "Alias";
            txtAlias.ReadOnly = true;
            txtAlias.Size = new Size(199, 23);
            txtAlias.TabIndex = 20;
            txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            txtZIPCode.Enabled = false;
            txtZIPCode.Location = new Point(229, 116);
            txtZIPCode.Name = "txtZIPCode";
            txtZIPCode.PlaceholderText = "ZIP Code";
            txtZIPCode.Size = new Size(199, 23);
            txtZIPCode.TabIndex = 4;
            txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhonenumber
            // 
            txtPhonenumber.Enabled = false;
            txtPhonenumber.Location = new Point(229, 145);
            txtPhonenumber.Name = "txtPhonenumber";
            txtPhonenumber.PlaceholderText = "Phonenumber";
            txtPhonenumber.Size = new Size(199, 23);
            txtPhonenumber.TabIndex = 21;
            txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            // 
            // btnGeneratePSW
            // 
            btnGeneratePSW.Location = new Point(0, 0);
            btnGeneratePSW.Name = "btnGeneratePSW";
            btnGeneratePSW.Size = new Size(75, 23);
            btnGeneratePSW.TabIndex = 0;
            // 
            // btnEditUserDetails
            // 
            btnEditUserDetails.BackColor = SystemColors.ActiveCaption;
            btnEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditUserDetails.Location = new Point(483, 174);
            btnEditUserDetails.Name = "btnEditUserDetails";
            btnEditUserDetails.Size = new Size(150, 30);
            btnEditUserDetails.TabIndex = 25;
            btnEditUserDetails.Text = "Edit Details";
            btnEditUserDetails.UseVisualStyleBackColor = false;
            btnEditUserDetails.Click += btnEditUserDetails_Click;
            // 
            // txtAdmin
            // 
            txtAdmin.BackColor = Color.LightGreen;
            txtAdmin.Enabled = false;
            txtAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAdmin.Location = new Point(434, 145);
            txtAdmin.Multiline = true;
            txtAdmin.Name = "txtAdmin";
            txtAdmin.Size = new Size(199, 23);
            txtAdmin.TabIndex = 26;
            txtAdmin.Text = "User is ADMIN";
            txtAdmin.TextAlign = HorizontalAlignment.Center;
            txtAdmin.Visible = false;
            // 
            // chkIsAdmin
            // 
            chkIsAdmin.Location = new Point(0, 0);
            chkIsAdmin.Name = "chkIsAdmin";
            chkIsAdmin.Size = new Size(104, 24);
            chkIsAdmin.TabIndex = 0;
            // 
            // listBoxUsers
            // 
            listBoxUsers.FormattingEnabled = true;
            listBoxUsers.ItemHeight = 15;
            listBoxUsers.Location = new Point(24, 3);
            listBoxUsers.Name = "listBoxUsers";
            listBoxUsers.Size = new Size(609, 49);
            listBoxUsers.TabIndex = 27;
            // 
            // USERSMainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btnEditUserDetails);
            Controls.Add(listBoxUsers);
            Controls.Add(txtAdmin);
            Controls.Add(txtPhonenumber);
            Controls.Add(txtZIPCode);
            Controls.Add(txtAlias);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(txtCity);
            Controls.Add(btnSaveEditUserDetails);
            Name = "USERSMainControl";
            Size = new Size(746, 317);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TextBox txtSurname;
        private TextBox txtAlias;
        private TextBox txtZIPCode;
        private TextBox txtPhonenumber;
        private Button btnGeneratePSW;
        private Button btnEditUserDetails;
        private CheckBox chkIsAdmin;
        private ListBox listBoxUsers;
    }
}
