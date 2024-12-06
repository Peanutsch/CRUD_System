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
            listBoxUser = new ListBox();
            comboBoxStatus = new ComboBox();
            btnChangePassword = new Button();
            txtStatusIndicator = new TextBox();
            listBoxLogs = new ListBox();
            lblLoggings = new Label();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Enabled = false;
            txtName.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtName.Location = new Point(28, 98);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Name";
            txtName.Size = new Size(325, 26);
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
            txtEmail.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtEmail.Location = new Point(28, 162);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "E-mail";
            txtEmail.Size = new Size(325, 26);
            txtEmail.TabIndex = 6;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            txtAddress.Enabled = false;
            txtAddress.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtAddress.Location = new Point(28, 130);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Adress";
            txtAddress.Size = new Size(325, 26);
            txtAddress.TabIndex = 3;
            txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            txtCity.Enabled = false;
            txtCity.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtCity.Location = new Point(471, 130);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "City";
            txtCity.Size = new Size(237, 26);
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
            btnSaveEditUserDetails.Location = new Point(359, 194);
            btnSaveEditUserDetails.Name = "btnSaveEditUserDetails";
            btnSaveEditUserDetails.Size = new Size(160, 32);
            btnSaveEditUserDetails.TabIndex = 9;
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
            txtSurname.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtSurname.Location = new Point(359, 98);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(349, 26);
            txtSurname.TabIndex = 2;
            txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            txtAlias.Enabled = false;
            txtAlias.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtAlias.Location = new Point(28, 66);
            txtAlias.Name = "txtAlias";
            txtAlias.PlaceholderText = "Alias";
            txtAlias.ReadOnly = true;
            txtAlias.Size = new Size(325, 26);
            txtAlias.TabIndex = 20;
            txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            txtZIPCode.Enabled = false;
            txtZIPCode.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtZIPCode.Location = new Point(359, 130);
            txtZIPCode.Name = "txtZIPCode";
            txtZIPCode.PlaceholderText = "ZIP Code";
            txtZIPCode.Size = new Size(106, 26);
            txtZIPCode.TabIndex = 4;
            txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhonenumber
            // 
            txtPhonenumber.Enabled = false;
            txtPhonenumber.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtPhonenumber.Location = new Point(359, 162);
            txtPhonenumber.Name = "txtPhonenumber";
            txtPhonenumber.PlaceholderText = "Phonenumber";
            txtPhonenumber.Size = new Size(349, 26);
            txtPhonenumber.TabIndex = 7;
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
            btnEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnEditUserDetails.Location = new Point(203, 194);
            btnEditUserDetails.Name = "btnEditUserDetails";
            btnEditUserDetails.Size = new Size(150, 32);
            btnEditUserDetails.TabIndex = 8;
            btnEditUserDetails.Text = "Edit Details";
            btnEditUserDetails.UseVisualStyleBackColor = false;
            btnEditUserDetails.Click += btnEditUserDetails_Click;
            // 
            // listBoxUser
            // 
            listBoxUser.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listBoxUser.FormattingEnabled = true;
            listBoxUser.ItemHeight = 18;
            listBoxUser.Location = new Point(28, 20);
            listBoxUser.Name = "listBoxUser";
            listBoxUser.Size = new Size(680, 22);
            listBoxUser.TabIndex = 27;
            listBoxUser.SelectedIndexChanged += ListBoxUser_SelectedIndexChanged;
            // 
            // comboBoxStatus
            // 
            comboBoxStatus.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBoxStatus.FormattingEnabled = true;
            comboBoxStatus.Items.AddRange(new object[] { "Online", "Active", "Away", "Break" });
            comboBoxStatus.Location = new Point(969, 20);
            comboBoxStatus.MaxDropDownItems = 9;
            comboBoxStatus.Name = "comboBoxStatus";
            comboBoxStatus.Size = new Size(83, 26);
            comboBoxStatus.TabIndex = 28;
            comboBoxStatus.Text = "Status";
            comboBoxStatus.SelectedIndexChanged += comboBoxStatus_SelectedIndexChanged;
            // 
            // btnChangePassword
            // 
            btnChangePassword.BackColor = SystemColors.ActiveCaption;
            btnChangePassword.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChangePassword.Location = new Point(525, 194);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(160, 32);
            btnChangePassword.TabIndex = 10;
            btnChangePassword.Text = "Change PSW";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Visible = false;
            btnChangePassword.Click += ChangePassword_Click;
            // 
            // txtStatusIndicator
            // 
            txtStatusIndicator.BackColor = Color.Blue;
            txtStatusIndicator.Location = new Point(1058, 20);
            txtStatusIndicator.Multiline = true;
            txtStatusIndicator.Name = "txtStatusIndicator";
            txtStatusIndicator.Size = new Size(16, 26);
            txtStatusIndicator.TabIndex = 30;
            // 
            // listBoxLogs
            // 
            listBoxLogs.FormattingEnabled = true;
            listBoxLogs.Location = new Point(714, 72);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(360, 116);
            listBoxLogs.TabIndex = 31;
            // 
            // lblLoggings
            // 
            lblLoggings.AutoSize = true;
            lblLoggings.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblLoggings.Location = new Point(714, 53);
            lblLoggings.Name = "lblLoggings";
            lblLoggings.Size = new Size(63, 16);
            lblLoggings.TabIndex = 43;
            lblLoggings.Text = "Loggings";
            // 
            // UserMainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblLoggings);
            Controls.Add(listBoxLogs);
            Controls.Add(txtStatusIndicator);
            Controls.Add(btnChangePassword);
            Controls.Add(comboBoxStatus);
            Controls.Add(btnEditUserDetails);
            Controls.Add(listBoxUser);
            Controls.Add(txtPhonenumber);
            Controls.Add(txtZIPCode);
            Controls.Add(txtAlias);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(txtCity);
            Controls.Add(btnSaveEditUserDetails);
            Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Name = "UserMainControl";
            Size = new Size(1116, 429);
            ResumeLayout(false);
            PerformLayout();
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
        public TextBox txtStatusIndicator;
        public ListBox listBoxLogs;
        private Label lblLoggings;
    }
}
