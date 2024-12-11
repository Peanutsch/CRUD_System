﻿namespace CRUD_System
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
            txtName = new TextBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            txtCity = new TextBox();
            listBoxAdmin = new ListBox();
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
            btnChangePassword = new Button();
            btnForceLogOutUser = new Button();
            txtSearch = new TextBox();
            lblSearchTxt = new Label();
            lblPageNumber = new Label();
            btnNextPage = new Button();
            btnPreviousPage = new Button();
            txtAbsenceIllness = new TextBox();
            btnCallInSick = new Button();
            listViewFiles = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            lblReports = new Label();
            txtDateReport = new TextBox();
            txtAliasReport = new TextBox();
            comboBoxSubjectReport = new ComboBox();
            rtxReport = new RichTextBox();
            btnCreateReport = new Button();
            btnSaveReport = new Button();
            txtSubject = new TextBox();
            txtCreator = new TextBox();
            lblSelectedAlias = new Label();
            lblCreatedBy = new Label();
            lblCurrentDate = new Label();
            btnShowListBoxLogs = new Button();
            btnDeleteReport = new Button();
            chkIsTheOne = new CheckBox();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Enabled = false;
            txtName.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtName.Location = new Point(24, 320);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Name";
            txtName.Size = new Size(325, 26);
            txtName.TabIndex = 1;
            txtName.TextAlign = HorizontalAlignment.Center;
            txtName.KeyDown += TxtName_KeyDown;
            // 
            // txtEmail
            // 
            txtEmail.Enabled = false;
            txtEmail.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtEmail.Location = new Point(24, 384);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "E-mail";
            txtEmail.Size = new Size(443, 26);
            txtEmail.TabIndex = 6;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAddress
            // 
            txtAddress.Enabled = false;
            txtAddress.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtAddress.Location = new Point(24, 352);
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
            txtCity.Location = new Point(473, 352);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "City";
            txtCity.Size = new Size(218, 26);
            txtCity.TabIndex = 5;
            txtCity.TextAlign = HorizontalAlignment.Center;
            txtCity.KeyDown += TxtCity_KeyDown;
            // 
            // listBoxAdmin
            // 
            listBoxAdmin.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listBoxAdmin.HorizontalScrollbar = true;
            listBoxAdmin.ItemHeight = 15;
            listBoxAdmin.Location = new Point(24, 35);
            listBoxAdmin.Name = "listBoxAdmin";
            listBoxAdmin.Size = new Size(770, 244);
            listBoxAdmin.TabIndex = 0;
            listBoxAdmin.DrawItem += ListBoxAdmin_DrawItem;
            listBoxAdmin.SelectedIndexChanged += ListBoxAdmin_SelectedIndexChanged;
            // 
            // btnCreateUser
            // 
            btnCreateUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnCreateUser.Location = new Point(473, 2);
            btnCreateUser.Name = "btnCreateUser";
            btnCreateUser.Size = new Size(138, 30);
            btnCreateUser.TabIndex = 11;
            btnCreateUser.Text = "Create User";
            btnCreateUser.Click += btnCreateUser_Click;
            // 
            // btnSaveEditUserDetails
            // 
            btnSaveEditUserDetails.BackColor = Color.LightGreen;
            btnSaveEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveEditUserDetails.Location = new Point(168, 419);
            btnSaveEditUserDetails.Name = "btnSaveEditUserDetails";
            btnSaveEditUserDetails.Size = new Size(106, 30);
            btnSaveEditUserDetails.TabIndex = 9;
            btnSaveEditUserDetails.Text = "Save Edit";
            btnSaveEditUserDetails.UseVisualStyleBackColor = false;
            btnSaveEditUserDetails.Visible = false;
            btnSaveEditUserDetails.Click += btnSaveEditUserDetails_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.BackColor = SystemColors.ActiveCaption;
            btnDeleteUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnDeleteUser.Location = new Point(432, 419);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(150, 30);
            btnDeleteUser.TabIndex = 9;
            btnDeleteUser.Text = "Delete User";
            btnDeleteUser.UseVisualStyleBackColor = false;
            btnDeleteUser.Visible = false;
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // txtSurname
            // 
            txtSurname.Enabled = false;
            txtSurname.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtSurname.Location = new Point(355, 320);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(336, 26);
            txtSurname.TabIndex = 2;
            txtSurname.TextAlign = HorizontalAlignment.Center;
            txtSurname.KeyDown += TxtSurname_KeyDown;
            // 
            // txtAlias
            // 
            txtAlias.Enabled = false;
            txtAlias.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAlias.Location = new Point(24, 288);
            txtAlias.Name = "txtAlias";
            txtAlias.PlaceholderText = "Alias";
            txtAlias.ReadOnly = true;
            txtAlias.Size = new Size(97, 26);
            txtAlias.TabIndex = 20;
            txtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // txtZIPCode
            // 
            txtZIPCode.Enabled = false;
            txtZIPCode.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtZIPCode.Location = new Point(355, 352);
            txtZIPCode.Name = "txtZIPCode";
            txtZIPCode.PlaceholderText = "ZIP Code";
            txtZIPCode.Size = new Size(112, 26);
            txtZIPCode.TabIndex = 4;
            txtZIPCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhonenumber
            // 
            txtPhonenumber.Enabled = false;
            txtPhonenumber.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtPhonenumber.Location = new Point(473, 384);
            txtPhonenumber.Name = "txtPhonenumber";
            txtPhonenumber.PlaceholderText = "Phonenumber";
            txtPhonenumber.Size = new Size(218, 26);
            txtPhonenumber.TabIndex = 7;
            txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            txtPhonenumber.KeyPress += TxtPhonenumber_KeyPress;
            // 
            // btnGeneratePSW
            // 
            btnGeneratePSW.BackColor = SystemColors.ActiveCaption;
            btnGeneratePSW.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnGeneratePSW.Location = new Point(280, 419);
            btnGeneratePSW.Name = "btnGeneratePSW";
            btnGeneratePSW.Size = new Size(146, 30);
            btnGeneratePSW.TabIndex = 22;
            btnGeneratePSW.Text = "Gen. Password";
            btnGeneratePSW.UseVisualStyleBackColor = false;
            btnGeneratePSW.Visible = false;
            btnGeneratePSW.Click += btnGeneratePassword_Click;
            // 
            // btnEditUserDetails
            // 
            btnEditUserDetails.BackColor = SystemColors.ActiveCaption;
            btnEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditUserDetails.Location = new Point(24, 419);
            btnEditUserDetails.Name = "btnEditUserDetails";
            btnEditUserDetails.Size = new Size(138, 30);
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
            txtAdmin.Location = new Point(697, 320);
            txtAdmin.Multiline = true;
            txtAdmin.Name = "txtAdmin";
            txtAdmin.Size = new Size(97, 26);
            txtAdmin.TabIndex = 26;
            txtAdmin.Text = "Admin";
            txtAdmin.TextAlign = HorizontalAlignment.Center;
            txtAdmin.Visible = false;
            // 
            // chkIsAdmin
            // 
            chkIsAdmin.AutoSize = true;
            chkIsAdmin.Enabled = false;
            chkIsAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkIsAdmin.Location = new Point(127, 289);
            chkIsAdmin.Name = "chkIsAdmin";
            chkIsAdmin.Size = new Size(97, 22);
            chkIsAdmin.TabIndex = 8;
            chkIsAdmin.Text = "isAdmin";
            chkIsAdmin.TextAlign = ContentAlignment.MiddleRight;
            chkIsAdmin.UseVisualStyleBackColor = true;
            chkIsAdmin.Visible = false;
            chkIsAdmin.CheckedChanged += chkIsAdmin_CheckedChanged;
            // 
            // btnChangePassword
            // 
            btnChangePassword.BackColor = SystemColors.ActiveCaption;
            btnChangePassword.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChangePassword.Location = new Point(24, 3);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(200, 30);
            btnChangePassword.TabIndex = 31;
            btnChangePassword.Text = "Change own Password";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnForceLogOutUser
            // 
            btnForceLogOutUser.BackColor = Color.LightGreen;
            btnForceLogOutUser.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnForceLogOutUser.Location = new Point(355, 284);
            btnForceLogOutUser.Name = "btnForceLogOutUser";
            btnForceLogOutUser.Size = new Size(150, 30);
            btnForceLogOutUser.TabIndex = 32;
            btnForceLogOutUser.Text = "Force LogOut";
            btnForceLogOutUser.UseVisualStyleBackColor = false;
            btnForceLogOutUser.Visible = false;
            btnForceLogOutUser.Click += btnForceLogOutUser_Click;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSearch.Location = new Point(701, 6);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "SEARCH";
            txtSearch.Size = new Size(93, 26);
            txtSearch.TabIndex = 33;
            txtSearch.TextAlign = HorizontalAlignment.Center;
            txtSearch.TextChanged += txtAliasToSearch_TextChanged;
            // 
            // lblSearchTxt
            // 
            lblSearchTxt.AutoSize = true;
            lblSearchTxt.BackColor = SystemColors.ActiveCaption;
            lblSearchTxt.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSearchTxt.Location = new Point(617, 8);
            lblSearchTxt.Name = "lblSearchTxt";
            lblSearchTxt.Size = new Size(78, 18);
            lblSearchTxt.TabIndex = 34;
            lblSearchTxt.Text = "Search:";
            lblSearchTxt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPageNumber
            // 
            lblPageNumber.AutoSize = true;
            lblPageNumber.BackColor = SystemColors.ActiveCaption;
            lblPageNumber.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblPageNumber.Location = new Point(626, 288);
            lblPageNumber.Name = "lblPageNumber";
            lblPageNumber.Size = new Size(84, 16);
            lblPageNumber.TabIndex = 35;
            lblPageNumber.Text = "Page 1 of 2";
            // 
            // btnNextPage
            // 
            btnNextPage.BackColor = SystemColors.ActiveCaption;
            btnNextPage.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnNextPage.Location = new Point(716, 285);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(75, 23);
            btnNextPage.TabIndex = 36;
            btnNextPage.Text = "Next";
            btnNextPage.UseVisualStyleBackColor = false;
            btnNextPage.Click += btnNextPage_Click;
            // 
            // btnPreviousPage
            // 
            btnPreviousPage.BackColor = SystemColors.ActiveCaption;
            btnPreviousPage.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnPreviousPage.Location = new Point(545, 285);
            btnPreviousPage.Name = "btnPreviousPage";
            btnPreviousPage.Size = new Size(75, 23);
            btnPreviousPage.TabIndex = 37;
            btnPreviousPage.Text = "Prev";
            btnPreviousPage.UseVisualStyleBackColor = false;
            btnPreviousPage.Click += btnPreviousPage_Click;
            // 
            // txtAbsenceIllness
            // 
            txtAbsenceIllness.BackColor = Color.Violet;
            txtAbsenceIllness.Enabled = false;
            txtAbsenceIllness.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAbsenceIllness.Location = new Point(697, 352);
            txtAbsenceIllness.Multiline = true;
            txtAbsenceIllness.Name = "txtAbsenceIllness";
            txtAbsenceIllness.Size = new Size(97, 26);
            txtAbsenceIllness.TabIndex = 38;
            txtAbsenceIllness.Text = "Ab.ill";
            txtAbsenceIllness.TextAlign = HorizontalAlignment.Center;
            txtAbsenceIllness.Visible = false;
            // 
            // btnCallInSick
            // 
            btnCallInSick.BackColor = SystemColors.ActiveCaption;
            btnCallInSick.Enabled = false;
            btnCallInSick.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCallInSick.Location = new Point(331, 2);
            btnCallInSick.Name = "btnCallInSick";
            btnCallInSick.Size = new Size(136, 30);
            btnCallInSick.TabIndex = 40;
            btnCallInSick.Text = "Call in Sick";
            btnCallInSick.UseVisualStyleBackColor = false;
            btnCallInSick.Click += btnCallInSick_Click;
            // 
            // listViewFiles
            // 
            listViewFiles.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listViewFiles.Font = new Font("Courier New", 9F, FontStyle.Bold);
            listViewFiles.FullRowSelect = true;
            listViewFiles.Location = new Point(800, 36);
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(383, 377);
            listViewFiles.TabIndex = 47;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.View = View.Details;
            listViewFiles.SelectedIndexChanged += listViewFiles_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "File";
            columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Subject";
            columnHeader2.Width = 125;
            // 
            // lblReports
            // 
            lblReports.AutoSize = true;
            lblReports.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblReports.Location = new Point(800, 17);
            lblReports.Name = "lblReports";
            lblReports.Size = new Size(56, 16);
            lblReports.TabIndex = 48;
            lblReports.Text = "Reports";
            // 
            // txtDateReport
            // 
            txtDateReport.Enabled = false;
            txtDateReport.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtDateReport.Location = new Point(1337, 65);
            txtDateReport.Multiline = true;
            txtDateReport.Name = "txtDateReport";
            txtDateReport.PlaceholderText = "Date";
            txtDateReport.Size = new Size(140, 26);
            txtDateReport.TabIndex = 45;
            txtDateReport.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAliasReport
            // 
            txtAliasReport.Enabled = false;
            txtAliasReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAliasReport.Location = new Point(1189, 35);
            txtAliasReport.Name = "txtAliasReport";
            txtAliasReport.PlaceholderText = "Alias";
            txtAliasReport.ReadOnly = true;
            txtAliasReport.Size = new Size(140, 26);
            txtAliasReport.TabIndex = 44;
            txtAliasReport.TextAlign = HorizontalAlignment.Center;
            // 
            // comboBoxSubjectReport
            // 
            comboBoxSubjectReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBoxSubjectReport.FormattingEnabled = true;
            comboBoxSubjectReport.Items.AddRange(new object[] { "Update Details", "Note", "Report", "Evaluation", "Copy Messages", "Other" });
            comboBoxSubjectReport.Location = new Point(1189, 67);
            comboBoxSubjectReport.Name = "comboBoxSubjectReport";
            comboBoxSubjectReport.Size = new Size(140, 24);
            comboBoxSubjectReport.TabIndex = 46;
            comboBoxSubjectReport.Text = "Subject:";
            comboBoxSubjectReport.Visible = false;
            // 
            // rtxReport
            // 
            rtxReport.BackColor = Color.LightGray;
            rtxReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtxReport.Location = new Point(1189, 97);
            rtxReport.Name = "rtxReport";
            rtxReport.ReadOnly = true;
            rtxReport.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtxReport.Size = new Size(288, 316);
            rtxReport.TabIndex = 47;
            rtxReport.Text = "";
            // 
            // btnCreateReport
            // 
            btnCreateReport.BackColor = SystemColors.ActiveCaption;
            btnCreateReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCreateReport.Location = new Point(1189, 419);
            btnCreateReport.Name = "btnCreateReport";
            btnCreateReport.Size = new Size(140, 30);
            btnCreateReport.TabIndex = 48;
            btnCreateReport.Text = "Report";
            btnCreateReport.UseVisualStyleBackColor = false;
            btnCreateReport.Click += buttonMakeReport_Click;
            // 
            // btnSaveReport
            // 
            btnSaveReport.BackColor = Color.LightGreen;
            btnSaveReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveReport.Location = new Point(1337, 419);
            btnSaveReport.Name = "btnSaveReport";
            btnSaveReport.Size = new Size(140, 30);
            btnSaveReport.TabIndex = 49;
            btnSaveReport.Text = "Save Report";
            btnSaveReport.UseVisualStyleBackColor = false;
            btnSaveReport.Visible = false;
            btnSaveReport.Click += btnSaveReport_Click;
            // 
            // txtSubject
            // 
            txtSubject.Enabled = false;
            txtSubject.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtSubject.Location = new Point(1189, 65);
            txtSubject.Multiline = true;
            txtSubject.Name = "txtSubject";
            txtSubject.PlaceholderText = "Subject";
            txtSubject.Size = new Size(140, 26);
            txtSubject.TabIndex = 50;
            txtSubject.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCreator
            // 
            txtCreator.Enabled = false;
            txtCreator.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtCreator.Location = new Point(1337, 35);
            txtCreator.Multiline = true;
            txtCreator.Name = "txtCreator";
            txtCreator.PlaceholderText = "Creator";
            txtCreator.Size = new Size(140, 26);
            txtCreator.TabIndex = 51;
            txtCreator.TextAlign = HorizontalAlignment.Center;
            // 
            // lblSelectedAlias
            // 
            lblSelectedAlias.AutoSize = true;
            lblSelectedAlias.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblSelectedAlias.Location = new Point(1189, 16);
            lblSelectedAlias.Name = "lblSelectedAlias";
            lblSelectedAlias.Size = new Size(105, 16);
            lblSelectedAlias.TabIndex = 52;
            lblSelectedAlias.Text = "Selected Alias";
            // 
            // lblCreatedBy
            // 
            lblCreatedBy.AutoSize = true;
            lblCreatedBy.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblCreatedBy.Location = new Point(1337, 16);
            lblCreatedBy.Name = "lblCreatedBy";
            lblCreatedBy.Size = new Size(77, 16);
            lblCreatedBy.TabIndex = 53;
            lblCreatedBy.Text = "Created by";
            // 
            // lblCurrentDate
            // 
            lblCurrentDate.AutoSize = true;
            lblCurrentDate.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblCurrentDate.Location = new Point(1337, 46);
            lblCurrentDate.Name = "lblCurrentDate";
            lblCurrentDate.Size = new Size(91, 16);
            lblCurrentDate.TabIndex = 54;
            lblCurrentDate.Text = "Current Date";
            lblCurrentDate.Visible = false;
            // 
            // btnShowListBoxLogs
            // 
            btnShowListBoxLogs.BackColor = SystemColors.ActiveCaption;
            btnShowListBoxLogs.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnShowListBoxLogs.Location = new Point(800, 419);
            btnShowListBoxLogs.Name = "btnShowListBoxLogs";
            btnShowListBoxLogs.Size = new Size(140, 30);
            btnShowListBoxLogs.TabIndex = 58;
            btnShowListBoxLogs.Text = "ListBoxLogs";
            btnShowListBoxLogs.UseVisualStyleBackColor = false;
            btnShowListBoxLogs.Visible = false;
            btnShowListBoxLogs.Click += btnShowListBoxLogs_Click;
            // 
            // btnDeleteReport
            // 
            btnDeleteReport.BackColor = Color.Red;
            btnDeleteReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDeleteReport.Location = new Point(1043, 419);
            btnDeleteReport.Name = "btnDeleteReport";
            btnDeleteReport.Size = new Size(140, 30);
            btnDeleteReport.TabIndex = 59;
            btnDeleteReport.Text = "Delete File";
            btnDeleteReport.UseVisualStyleBackColor = false;
            btnDeleteReport.Visible = false;
            btnDeleteReport.Click += btnDeleteReport_Click;
            // 
            // chkIsTheOne
            // 
            chkIsTheOne.AutoSize = true;
            chkIsTheOne.CheckAlign = ContentAlignment.MiddleRight;
            chkIsTheOne.Enabled = false;
            chkIsTheOne.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkIsTheOne.Location = new Point(242, 289);
            chkIsTheOne.Name = "chkIsTheOne";
            chkIsTheOne.Size = new Size(107, 22);
            chkIsTheOne.TabIndex = 60;
            chkIsTheOne.Text = "isTheOne";
            chkIsTheOne.TextAlign = ContentAlignment.MiddleRight;
            chkIsTheOne.UseVisualStyleBackColor = true;
            chkIsTheOne.Checked = false;
            chkIsTheOne.Visible = false;
            chkIsTheOne.CheckedChanged += chkIsIsTheOne_CheckedChanged;
            // 
            // AdminMainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(chkIsTheOne);
            Controls.Add(btnDeleteReport);
            Controls.Add(btnShowListBoxLogs);
            Controls.Add(lblCurrentDate);
            Controls.Add(lblCreatedBy);
            Controls.Add(lblSelectedAlias);
            Controls.Add(btnCreateReport);
            Controls.Add(btnSaveReport);
            Controls.Add(txtAliasReport);
            Controls.Add(txtSubject);
            Controls.Add(rtxReport);
            Controls.Add(txtDateReport);
            Controls.Add(txtCreator);
            Controls.Add(lblReports);
            Controls.Add(listViewFiles);
            Controls.Add(comboBoxSubjectReport);
            Controls.Add(btnCallInSick);
            Controls.Add(txtAbsenceIllness);
            Controls.Add(btnPreviousPage);
            Controls.Add(btnNextPage);
            Controls.Add(lblPageNumber);
            Controls.Add(lblSearchTxt);
            Controls.Add(txtSearch);
            Controls.Add(btnForceLogOutUser);
            Controls.Add(btnCreateUser);
            Controls.Add(btnChangePassword);
            Controls.Add(chkIsAdmin);
            Controls.Add(txtAdmin);
            Controls.Add(btnEditUserDetails);
            Controls.Add(btnGeneratePSW);
            Controls.Add(txtPhonenumber);
            Controls.Add(txtZIPCode);
            Controls.Add(txtAlias);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(txtCity);
            Controls.Add(listBoxAdmin);
            Controls.Add(btnSaveEditUserDetails);
            Controls.Add(btnDeleteUser);
            Name = "AdminMainControl";
            Size = new Size(1566, 513);
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
        public ListView listViewFiles;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        public Label lblReports;
        public TextBox txtDateReport;
        public TextBox txtAliasReport;
        public ComboBox comboBoxSubjectReport;
        public RichTextBox rtxReport;
        public Button btnCreateReport;
        public Button btnSaveReport;
        public TextBox txtSubject;
        public TextBox txtCreator;
        public Label lblSelectedAlias;
        public Label lblCreatedBy;
        public Label lblCurrentDate;
        public Button btnShowListBoxLogs;
        public Button btnDeleteReport;
        public CheckBox chkIsTheOne;
    }
}
