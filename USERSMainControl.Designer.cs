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
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Button btnSaveEditUserDetails;
        private System.Windows.Forms.Button btnDeleteUser;

        private ADMINMainControl mainControlAdmin = new ADMINMainControl();

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
            textBox1 = new TextBox();
            btnChangePassword = new Button();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Enabled = false;
            txtName.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtName.Location = new Point(100, 101);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Name";
            txtName.Size = new Size(199, 26);
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
            txtEmail.Location = new Point(100, 162);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "E-mail";
            txtEmail.Size = new Size(199, 26);
            txtEmail.TabIndex = 6;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            txtAddress.Enabled = false;
            txtAddress.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtAddress.Location = new Point(100, 133);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Adress";
            txtAddress.Size = new Size(199, 26);
            txtAddress.TabIndex = 3;
            txtAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCity
            // 
            txtCity.Enabled = false;
            txtCity.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtCity.Location = new Point(510, 133);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "City";
            txtCity.Size = new Size(199, 26);
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
            btnSaveEditUserDetails.Location = new Point(821, 105);
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
            txtSurname.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtSurname.Location = new Point(305, 101);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(199, 26);
            txtSurname.TabIndex = 2;
            txtSurname.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAlias
            // 
            txtAlias.Enabled = false;
            txtAlias.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtAlias.Location = new Point(100, 69);
            txtAlias.Name = "txtAlias";
            txtAlias.PlaceholderText = "Alias";
            txtAlias.ReadOnly = true;
            txtAlias.Size = new Size(199, 26);
            txtAlias.TabIndex = 20;
            txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            txtZIPCode.Enabled = false;
            txtZIPCode.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtZIPCode.Location = new Point(305, 133);
            txtZIPCode.Name = "txtZIPCode";
            txtZIPCode.PlaceholderText = "ZIP Code";
            txtZIPCode.Size = new Size(199, 26);
            txtZIPCode.TabIndex = 4;
            txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhonenumber
            // 
            txtPhonenumber.Enabled = false;
            txtPhonenumber.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtPhonenumber.Location = new Point(305, 162);
            txtPhonenumber.Name = "txtPhonenumber";
            txtPhonenumber.PlaceholderText = "Phonenumber";
            txtPhonenumber.Size = new Size(199, 26);
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
            btnEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnEditUserDetails.Location = new Point(821, 69);
            btnEditUserDetails.Name = "btnEditUserDetails";
            btnEditUserDetails.Size = new Size(150, 30);
            btnEditUserDetails.TabIndex = 25;
            btnEditUserDetails.Text = "Edit Details";
            btnEditUserDetails.UseVisualStyleBackColor = false;
            btnEditUserDetails.Click += btnEditUserDetails_Click;
            // 
            // listBoxUser
            // 
            listBoxUser.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listBoxUser.FormattingEnabled = true;
            listBoxUser.ItemHeight = 18;
            listBoxUser.Location = new Point(100, 30);
            listBoxUser.Name = "listBoxUser";
            listBoxUser.Size = new Size(680, 22);
            listBoxUser.TabIndex = 27;
            listBoxUser.SelectedIndexChanged += ListBoxUser_SelectedIndexChanged;
            // 
            // comboBoxStatus
            // 
            comboBoxStatus.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBoxStatus.FormattingEnabled = true;
            comboBoxStatus.Location = new Point(821, 30);
            comboBoxStatus.Name = "comboBoxStatus";
            comboBoxStatus.Size = new Size(150, 26);
            comboBoxStatus.TabIndex = 28;
            comboBoxStatus.Text = "User Status";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(977, 144);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Password";
            textBox1.Size = new Size(150, 26);
            textBox1.TabIndex = 29;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // ChangePassword
            // 
            btnChangePassword.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChangePassword.Location = new Point(821, 141);
            btnChangePassword.Name = "button1";
            btnChangePassword.Size = new Size(150, 30);
            btnChangePassword.TabIndex = 30;
            btnChangePassword.Text = "Change PSW";
            btnChangePassword.UseVisualStyleBackColor = true;
            btnChangePassword.Enabled = false;
            btnChangePassword.Click += ChangePassword_Click;
            // 
            // USERSMainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btnChangePassword);
            Controls.Add(textBox1);
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
            Name = "USERSMainControl";
            Size = new Size(1192, 317);
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
        private ListBox listBoxUser;
        private ComboBox comboBoxStatus;
        private TextBox textBox1;
        private Button btnChangePassword;
    }
}
