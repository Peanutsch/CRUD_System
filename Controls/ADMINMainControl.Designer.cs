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
            this.txtSearch = new TextBox();
            this.lblSearchTxt = new Label();
            this.lblPageNumber = new Label();
            this.btnNextPage = new Button();
            this.btnPreviousPage = new Button();
            this.txtAbsenceIllness = new TextBox();
            this.btnCallInSick = new Button();
            this.listBoxLogs = new ListBox();
            this.lblLoggings = new Label();
            this.listViewFiles = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.lblReports = new Label();
            this.txtCurrentDateReport = new TextBox();
            this.txtAliasNotes = new TextBox();
            this.comboBoxSubjectReport = new ComboBox();
            this.rtxNewReport = new RichTextBox();
            this.buttonEmptyReport = new Button();
            this.buttonSaveReport = new Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtName.Location = new Point(24, 302);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Name";
            this.txtName.Size = new Size(325, 26);
            this.txtName.TabIndex = 1;
            this.txtName.TextAlign = HorizontalAlignment.Center;
            this.txtName.KeyDown += this.TxtName_KeyDown;
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtEmail.Location = new Point(24, 366);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "E-mail";
            this.txtEmail.Size = new Size(443, 26);
            this.txtEmail.TabIndex = 6;
            this.txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtAddress.Location = new Point(24, 334);
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
            this.txtCity.Location = new Point(473, 334);
            this.txtCity.Name = "txtCity";
            this.txtCity.PlaceholderText = "City";
            this.txtCity.Size = new Size(218, 26);
            this.txtCity.TabIndex = 5;
            this.txtCity.TextAlign = HorizontalAlignment.Center;
            this.txtCity.KeyDown += this.TxtCity_KeyDown;
            // 
            // listBoxAdmin
            // 
            this.listBoxAdmin.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.listBoxAdmin.ItemHeight = 15;
            this.listBoxAdmin.Location = new Point(24, 35);
            this.listBoxAdmin.Name = "listBoxAdmin";
            this.listBoxAdmin.Size = new Size(770, 229);
            this.listBoxAdmin.TabIndex = 0;
            this.listBoxAdmin.DrawItem += this.ListBoxAdmin_DrawItem;
            this.listBoxAdmin.SelectedIndexChanged += this.ListBoxAdmin_SelectedIndexChanged;
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnCreateUser.Location = new Point(473, 2);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new Size(138, 30);
            this.btnCreateUser.TabIndex = 11;
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.Click += this.btnCreateUser_Click;
            // 
            // btnSaveEditUserDetails
            // 
            this.btnSaveEditUserDetails.BackColor = Color.LightGreen;
            this.btnSaveEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveEditUserDetails.Location = new Point(168, 409);
            this.btnSaveEditUserDetails.Name = "btnSaveEditUserDetails";
            this.btnSaveEditUserDetails.Size = new Size(106, 30);
            this.btnSaveEditUserDetails.TabIndex = 9;
            this.btnSaveEditUserDetails.Text = "Save Edit";
            this.btnSaveEditUserDetails.UseVisualStyleBackColor = false;
            this.btnSaveEditUserDetails.Visible = false;
            this.btnSaveEditUserDetails.Click += this.btnSaveEditUserDetails_Click;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.BackColor = SystemColors.ActiveCaption;
            this.btnDeleteUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnDeleteUser.Location = new Point(432, 409);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new Size(150, 30);
            this.btnDeleteUser.TabIndex = 9;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.UseVisualStyleBackColor = false;
            this.btnDeleteUser.Visible = false;
            this.btnDeleteUser.Click += this.btnDeleteUser_Click;
            // 
            // txtSurname
            // 
            this.txtSurname.Enabled = false;
            this.txtSurname.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtSurname.Location = new Point(355, 302);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.PlaceholderText = "Surname";
            this.txtSurname.Size = new Size(336, 26);
            this.txtSurname.TabIndex = 2;
            this.txtSurname.TextAlign = HorizontalAlignment.Center;
            this.txtSurname.KeyDown += this.TxtSurname_KeyDown;
            // 
            // txtAlias
            // 
            this.txtAlias.Enabled = false;
            this.txtAlias.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtAlias.Location = new Point(24, 270);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.PlaceholderText = "Alias";
            this.txtAlias.ReadOnly = true;
            this.txtAlias.Size = new Size(97, 26);
            this.txtAlias.TabIndex = 20;
            this.txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            this.txtZIPCode.Enabled = false;
            this.txtZIPCode.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtZIPCode.Location = new Point(355, 334);
            this.txtZIPCode.Name = "txtZIPCode";
            this.txtZIPCode.PlaceholderText = "ZIP Code";
            this.txtZIPCode.Size = new Size(112, 26);
            this.txtZIPCode.TabIndex = 4;
            this.txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhonenumber
            // 
            this.txtPhonenumber.Enabled = false;
            this.txtPhonenumber.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtPhonenumber.Location = new Point(473, 366);
            this.txtPhonenumber.Name = "txtPhonenumber";
            this.txtPhonenumber.PlaceholderText = "Phonenumber";
            this.txtPhonenumber.Size = new Size(218, 26);
            this.txtPhonenumber.TabIndex = 7;
            this.txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            this.txtPhonenumber.KeyPress += this.TxtPhonenumber_KeyPress;
            // 
            // btnGeneratePSW
            // 
            this.btnGeneratePSW.BackColor = SystemColors.ActiveCaption;
            this.btnGeneratePSW.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnGeneratePSW.Location = new Point(280, 409);
            this.btnGeneratePSW.Name = "btnGeneratePSW";
            this.btnGeneratePSW.Size = new Size(146, 30);
            this.btnGeneratePSW.TabIndex = 22;
            this.btnGeneratePSW.Text = "Gen. Password";
            this.btnGeneratePSW.UseVisualStyleBackColor = false;
            this.btnGeneratePSW.Visible = false;
            this.btnGeneratePSW.Click += this.btnGeneratePassword_Click;
            // 
            // btnEditUserDetails
            // 
            this.btnEditUserDetails.BackColor = SystemColors.ActiveCaption;
            this.btnEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnEditUserDetails.Location = new Point(24, 409);
            this.btnEditUserDetails.Name = "btnEditUserDetails";
            this.btnEditUserDetails.Size = new Size(138, 30);
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
            this.txtAdmin.Location = new Point(127, 270);
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
            this.chkIsAdmin.Location = new Point(588, 414);
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
            this.btnForceLogOutUser.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnForceLogOutUser.Location = new Point(333, 266);
            this.btnForceLogOutUser.Name = "btnForceLogOutUser";
            this.btnForceLogOutUser.Size = new Size(150, 30);
            this.btnForceLogOutUser.TabIndex = 32;
            this.btnForceLogOutUser.Text = "Force LogOut";
            this.btnForceLogOutUser.UseVisualStyleBackColor = false;
            this.btnForceLogOutUser.Visible = false;
            this.btnForceLogOutUser.Click += this.btnForceLogOutUser_Click;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtSearch.Location = new Point(701, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "SEARCH";
            this.txtSearch.Size = new Size(93, 26);
            this.txtSearch.TabIndex = 33;
            this.txtSearch.TextAlign = HorizontalAlignment.Center;
            this.txtSearch.TextChanged += this.txtAliasToSearch_TextChanged;
            // 
            // lblSearchTxt
            // 
            this.lblSearchTxt.AutoSize = true;
            this.lblSearchTxt.BackColor = SystemColors.ActiveCaption;
            this.lblSearchTxt.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblSearchTxt.Location = new Point(617, 8);
            this.lblSearchTxt.Name = "lblSearchTxt";
            this.lblSearchTxt.Size = new Size(78, 18);
            this.lblSearchTxt.TabIndex = 34;
            this.lblSearchTxt.Text = "Search:";
            this.lblSearchTxt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPageNumber
            // 
            this.lblPageNumber.AutoSize = true;
            this.lblPageNumber.BackColor = SystemColors.ActiveCaption;
            this.lblPageNumber.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.lblPageNumber.Location = new Point(629, 270);
            this.lblPageNumber.Name = "lblPageNumber";
            this.lblPageNumber.Size = new Size(84, 16);
            this.lblPageNumber.TabIndex = 35;
            this.lblPageNumber.Text = "Page 1 of 2";
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackColor = SystemColors.ActiveCaption;
            this.btnNextPage.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnNextPage.Location = new Point(719, 267);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new Size(75, 23);
            this.btnNextPage.TabIndex = 36;
            this.btnNextPage.Text = "Next";
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += this.btnNextPage_Click;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.BackColor = SystemColors.ActiveCaption;
            this.btnPreviousPage.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnPreviousPage.Location = new Point(548, 267);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new Size(75, 23);
            this.btnPreviousPage.TabIndex = 37;
            this.btnPreviousPage.Text = "Prev";
            this.btnPreviousPage.UseVisualStyleBackColor = false;
            this.btnPreviousPage.Click += this.btnPreviousPage_Click;
            // 
            // txtAbsenceIllness
            // 
            this.txtAbsenceIllness.BackColor = Color.Violet;
            this.txtAbsenceIllness.Enabled = false;
            this.txtAbsenceIllness.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtAbsenceIllness.Location = new Point(230, 270);
            this.txtAbsenceIllness.Multiline = true;
            this.txtAbsenceIllness.Name = "txtAbsenceIllness";
            this.txtAbsenceIllness.Size = new Size(97, 23);
            this.txtAbsenceIllness.TabIndex = 38;
            this.txtAbsenceIllness.Text = "Ab.ill";
            this.txtAbsenceIllness.TextAlign = HorizontalAlignment.Center;
            this.txtAbsenceIllness.Visible = false;
            // 
            // btnCallInSick
            // 
            this.btnCallInSick.BackColor = SystemColors.ActiveCaption;
            this.btnCallInSick.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCallInSick.Location = new Point(331, 2);
            this.btnCallInSick.Name = "btnCallInSick";
            this.btnCallInSick.Size = new Size(136, 30);
            this.btnCallInSick.TabIndex = 40;
            this.btnCallInSick.Text = "Call in Sick";
            this.btnCallInSick.UseVisualStyleBackColor = false;
            this.btnCallInSick.Click += this.btnCallInSick_Click;
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.HorizontalScrollbar = true;
            this.listBoxLogs.Location = new Point(800, 35);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new Size(383, 116);
            this.listBoxLogs.TabIndex = 41;
            this.listBoxLogs.SelectedIndexChanged += this.listBoxLogs_SelectedIndexChanged;
            // 
            // lblLoggings
            // 
            this.lblLoggings.AutoSize = true;
            this.lblLoggings.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.lblLoggings.Location = new Point(800, 17);
            this.lblLoggings.Name = "lblLoggings";
            this.lblLoggings.Size = new Size(63, 16);
            this.lblLoggings.TabIndex = 42;
            this.lblLoggings.Text = "Loggings";
            // 
            // listViewFiles
            // 
            this.listViewFiles.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listViewFiles.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.Location = new Point(800, 170);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new Size(383, 243);
            this.listViewFiles.TabIndex = 47;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = View.Details;
            this.listViewFiles.SelectedIndexChanged += this.listViewFiles_SelectedIndexChanged;
            this.listViewFiles.DoubleClick += this.listViewFiles_DoubleClick;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File";
            this.columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Subject";
            this.columnHeader2.Width = 125;
            // 
            // lblReports
            // 
            this.lblReports.AutoSize = true;
            this.lblReports.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.lblReports.Location = new Point(800, 154);
            this.lblReports.Name = "lblReports";
            this.lblReports.Size = new Size(56, 16);
            this.lblReports.TabIndex = 48;
            this.lblReports.Text = "Reports";
            // 
            // txtCurrentDateReport
            // 
            this.txtCurrentDateReport.Enabled = false;
            this.txtCurrentDateReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtCurrentDateReport.Location = new Point(1335, 67);
            this.txtCurrentDateReport.Multiline = true;
            this.txtCurrentDateReport.Name = "txtCurrentDateReport";
            this.txtCurrentDateReport.Size = new Size(140, 23);
            this.txtCurrentDateReport.TabIndex = 45;
            this.txtCurrentDateReport.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAliasNotes
            // 
            this.txtAliasNotes.Enabled = false;
            this.txtAliasNotes.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtAliasNotes.Location = new Point(1189, 35);
            this.txtAliasNotes.Name = "txtAliasNotes";
            this.txtAliasNotes.PlaceholderText = "Alias";
            this.txtAliasNotes.ReadOnly = true;
            this.txtAliasNotes.Size = new Size(97, 26);
            this.txtAliasNotes.TabIndex = 44;
            this.txtAliasNotes.TextAlign = HorizontalAlignment.Center;
            // 
            // comboBoxSubjectReport
            // 
            this.comboBoxSubjectReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.comboBoxSubjectReport.FormattingEnabled = true;
            this.comboBoxSubjectReport.Items.AddRange(new object[] { "Report", "Memo", "Evaluation", "Other" });
            this.comboBoxSubjectReport.Location = new Point(1189, 67);
            this.comboBoxSubjectReport.Name = "comboBoxSubjectReport";
            this.comboBoxSubjectReport.Size = new Size(140, 24);
            this.comboBoxSubjectReport.TabIndex = 46;
            this.comboBoxSubjectReport.Text = "Subject:";
            // 
            // rtxNewReport
            // 
            this.rtxNewReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.rtxNewReport.Location = new Point(1189, 97);
            this.rtxNewReport.Name = "rtxNewReport";
            this.rtxNewReport.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.rtxNewReport.Size = new Size(288, 316);
            this.rtxNewReport.TabIndex = 47;
            this.rtxNewReport.Text = "";
            // 
            // buttonEmptyReport
            // 
            this.buttonEmptyReport.BackColor = SystemColors.ActiveCaption;
            this.buttonEmptyReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.buttonEmptyReport.Location = new Point(1189, 419);
            this.buttonEmptyReport.Name = "buttonEmptyReport";
            this.buttonEmptyReport.Size = new Size(140, 23);
            this.buttonEmptyReport.TabIndex = 48;
            this.buttonEmptyReport.Text = "Empty Report";
            this.buttonEmptyReport.UseVisualStyleBackColor = false;
            this.buttonEmptyReport.Click += this.buttonEmptyReport_Click;
            // 
            // buttonSaveReport
            // 
            this.buttonSaveReport.BackColor = SystemColors.ActiveCaption;
            this.buttonSaveReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.buttonSaveReport.Location = new Point(1337, 419);
            this.buttonSaveReport.Name = "buttonSaveReport";
            this.buttonSaveReport.Size = new Size(140, 23);
            this.buttonSaveReport.TabIndex = 49;
            this.buttonSaveReport.Text = "Save Report";
            this.buttonSaveReport.UseVisualStyleBackColor = false;
            this.buttonSaveReport.Click += this.buttonSaveReport_Click;
            // 
            // AdminMainControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.rtxNewReport);
            this.Controls.Add(this.buttonSaveReport);
            this.Controls.Add(this.txtCurrentDateReport);
            this.Controls.Add(this.comboBoxSubjectReport);
            this.Controls.Add(this.lblReports);
            this.Controls.Add(this.buttonEmptyReport);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.lblLoggings);
            this.Controls.Add(this.txtAliasNotes);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.btnCallInSick);
            this.Controls.Add(this.txtAbsenceIllness);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.lblPageNumber);
            this.Controls.Add(this.lblSearchTxt);
            this.Controls.Add(this.txtSearch);
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
            this.Size = new Size(1566, 450);
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
        public TextBox txtSearch;
        public Label lblSearchTxt;
        public Label lblPageNumber;
        public Button btnNextPage;
        public Button btnPreviousPage;
        public TextBox txtAbsenceIllness;
        public Button btnCallInSick;
        public ListBox listBoxLogs;
        public Label lblLoggings;
        public ListView listViewFiles;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        public Label lblReports;
        public TextBox txtCurrentDateReport;
        public TextBox txtAliasNotes;
        public ComboBox comboBoxSubjectReport;
        public RichTextBox rtxNewReport;
        public Button buttonEmptyReport;
        public Button buttonSaveReport;
    }
}
