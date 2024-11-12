using System.Xml.Linq;

namespace CRUD_System
{
    partial class UserMainControl
    {
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.TextBox txtPassword;
        public System.Windows.Forms.TextBox txtEmail;
        public System.Windows.Forms.TextBox txtAddress;
        public System.Windows.Forms.TextBox txtCity;
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtName = new TextBox();
            this.txtPassword = new TextBox();
            this.txtEmail = new TextBox();
            this.txtAddress = new TextBox();
            this.txtCity = new TextBox();
            this.btnCreateUser = new Button();
            this.btnSaveEditUserDetails = new Button();
            this.btnDeleteUser = new Button();
            this.txtSurname = new TextBox();
            this.txtAlias = new TextBox();
            this.txtZIPCode = new TextBox();
            this.txtPhonenumber = new TextBox();
            this.btnGeneratePSW = new Button();
            this.btnEditUserDetails = new Button();
            this.listBoxUser = new ListBox();
            this.comboBoxStatus = new ComboBox();
            this.btnChangePassword = new Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtName.Location = new Point(100, 104);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Name";
            this.txtName.Size = new Size(325, 26);
            this.txtName.TabIndex = 1;
            this.txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new Point(0, 0);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(100, 23);
            this.txtPassword.TabIndex = 0;
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtEmail.Location = new Point(100, 168);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "E-mail";
            this.txtEmail.Size = new Size(325, 26);
            this.txtEmail.TabIndex = 6;
            this.txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtAddress.Location = new Point(100, 136);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PlaceholderText = "Adress";
            this.txtAddress.Size = new Size(325, 26);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            this.txtCity.Enabled = false;
            this.txtCity.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtCity.Location = new Point(762, 136);
            this.txtCity.Name = "txtCity";
            this.txtCity.PlaceholderText = "City";
            this.txtCity.Size = new Size(325, 26);
            this.txtCity.TabIndex = 5;
            this.txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Location = new Point(0, 0);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new Size(75, 23);
            this.btnCreateUser.TabIndex = 0;
            // 
            // btnSaveEditUserDetails
            // 
            this.btnSaveEditUserDetails.BackColor = Color.LightGreen;
            this.btnSaveEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveEditUserDetails.Location = new Point(430, 200);
            this.btnSaveEditUserDetails.Name = "btnSaveEditUserDetails";
            this.btnSaveEditUserDetails.Size = new Size(160, 30);
            this.btnSaveEditUserDetails.TabIndex = 9;
            this.btnSaveEditUserDetails.Text = "Save Edit";
            this.btnSaveEditUserDetails.UseVisualStyleBackColor = false;
            this.btnSaveEditUserDetails.Visible = false;
            this.btnSaveEditUserDetails.Click += this.btnSaveEditUserDetails_Click;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new Point(0, 0);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new Size(75, 23);
            this.btnDeleteUser.TabIndex = 0;
            // 
            // txtSurname
            // 
            this.txtSurname.Enabled = false;
            this.txtSurname.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtSurname.Location = new Point(431, 104);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.PlaceholderText = "Surname";
            this.txtSurname.Size = new Size(325, 26);
            this.txtSurname.TabIndex = 2;
            this.txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            this.txtAlias.Enabled = false;
            this.txtAlias.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtAlias.Location = new Point(100, 72);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.PlaceholderText = "Alias";
            this.txtAlias.ReadOnly = true;
            this.txtAlias.Size = new Size(325, 26);
            this.txtAlias.TabIndex = 20;
            this.txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            this.txtZIPCode.Enabled = false;
            this.txtZIPCode.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtZIPCode.Location = new Point(431, 136);
            this.txtZIPCode.Name = "txtZIPCode";
            this.txtZIPCode.PlaceholderText = "ZIP Code";
            this.txtZIPCode.Size = new Size(325, 26);
            this.txtZIPCode.TabIndex = 4;
            this.txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhonenumber
            // 
            this.txtPhonenumber.Enabled = false;
            this.txtPhonenumber.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtPhonenumber.Location = new Point(431, 168);
            this.txtPhonenumber.Name = "txtPhonenumber";
            this.txtPhonenumber.PlaceholderText = "Phonenumber";
            this.txtPhonenumber.Size = new Size(325, 26);
            this.txtPhonenumber.TabIndex = 7;
            this.txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            // 
            // btnGeneratePSW
            // 
            this.btnGeneratePSW.Location = new Point(0, 0);
            this.btnGeneratePSW.Name = "btnGeneratePSW";
            this.btnGeneratePSW.Size = new Size(75, 23);
            this.btnGeneratePSW.TabIndex = 0;
            // 
            // btnEditUserDetails
            // 
            this.btnEditUserDetails.BackColor = SystemColors.ActiveCaption;
            this.btnEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnEditUserDetails.Location = new Point(275, 200);
            this.btnEditUserDetails.Name = "btnEditUserDetails";
            this.btnEditUserDetails.Size = new Size(150, 30);
            this.btnEditUserDetails.TabIndex = 8;
            this.btnEditUserDetails.Text = "Edit Details";
            this.btnEditUserDetails.UseVisualStyleBackColor = false;
            this.btnEditUserDetails.Click += this.btnEditUserDetails_Click;
            // 
            // listBoxUser
            // 
            this.listBoxUser.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.listBoxUser.FormattingEnabled = true;
            this.listBoxUser.ItemHeight = 18;
            this.listBoxUser.Location = new Point(100, 30);
            this.listBoxUser.Name = "listBoxUser";
            this.listBoxUser.Size = new Size(680, 22);
            this.listBoxUser.TabIndex = 27;
            this.listBoxUser.SelectedIndexChanged += this.ListBoxUser_SelectedIndexChanged;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new Point(786, 26);
            this.comboBoxStatus.MaxDropDownItems = 9;
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new Size(150, 26);
            this.comboBoxStatus.TabIndex = 28;
            this.comboBoxStatus.Text = "User Status";
            this.comboBoxStatus.SelectedIndexChanged += this.comboBoxStatus_SelectedIndexChanged;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = SystemColors.ActiveCaption;
            this.btnChangePassword.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnChangePassword.Location = new Point(596, 200);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new Size(160, 30);
            this.btnChangePassword.TabIndex = 10;
            this.btnChangePassword.Text = "Change PSW";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Visible = false;
            this.btnChangePassword.Click += this.ChangePassword_Click;
            // 
            // UserMainControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.btnEditUserDetails);
            this.Controls.Add(this.listBoxUser);
            this.Controls.Add(this.txtPhonenumber);
            this.Controls.Add(this.txtZIPCode);
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.btnSaveEditUserDetails);
            this.Name = "UserMainControl";
            this.Size = new Size(1192, 402);
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
        public ListBox listBoxUser;
        public ComboBox comboBoxStatus;
        public Button btnChangePassword;
    }
}
