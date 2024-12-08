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
            listBoxLogs = new ListBox();
            lblLoggings = new Label();
            txtAliasNotes = new TextBox();
            txtCurrentDateReport = new TextBox();
            panelNotes = new Panel();
            buttonSaveReport = new Button();
            buttonEmptyReport = new Button();
            rtxNewReport = new RichTextBox();
            comboBoxSubjectReport = new ComboBox();
            listViewFiles = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            lblReports = new Label();
            panelNotes.SuspendLayout();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Enabled = false;
            txtName.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtName.Location = new Point(24, 302);
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
            txtEmail.Location = new Point(24, 366);
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
            txtAddress.Location = new Point(24, 334);
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
            txtCity.Location = new Point(473, 334);
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
            listBoxAdmin.ItemHeight = 15;
            listBoxAdmin.Location = new Point(24, 35);
            listBoxAdmin.Name = "listBoxAdmin";
            listBoxAdmin.Size = new Size(770, 229);
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
            btnSaveEditUserDetails.Location = new Point(168, 409);
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
            btnDeleteUser.Location = new Point(432, 409);
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
            txtSurname.Location = new Point(355, 302);
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
            txtAlias.Location = new Point(24, 270);
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
            txtZIPCode.Location = new Point(355, 334);
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
            txtPhonenumber.Location = new Point(473, 366);
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
            btnGeneratePSW.Location = new Point(280, 409);
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
            btnEditUserDetails.Location = new Point(24, 409);
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
            txtAdmin.Location = new Point(127, 270);
            txtAdmin.Multiline = true;
            txtAdmin.Name = "txtAdmin";
            txtAdmin.Size = new Size(97, 23);
            txtAdmin.TabIndex = 26;
            txtAdmin.Text = "Admin";
            txtAdmin.TextAlign = HorizontalAlignment.Center;
            txtAdmin.Visible = false;
            // 
            // chkIsAdmin
            // 
            chkIsAdmin.AutoSize = true;
            chkIsAdmin.CheckAlign = ContentAlignment.MiddleRight;
            chkIsAdmin.Enabled = false;
            chkIsAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkIsAdmin.Location = new Point(588, 414);
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
            btnForceLogOutUser.BackColor = SystemColors.ActiveCaption;
            btnForceLogOutUser.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnForceLogOutUser.Location = new Point(333, 266);
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
            lblPageNumber.Location = new Point(629, 270);
            lblPageNumber.Name = "lblPageNumber";
            lblPageNumber.Size = new Size(84, 16);
            lblPageNumber.TabIndex = 35;
            lblPageNumber.Text = "Page 1 of 2";
            // 
            // btnNextPage
            // 
            btnNextPage.BackColor = SystemColors.ActiveCaption;
            btnNextPage.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnNextPage.Location = new Point(719, 267);
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
            btnPreviousPage.Location = new Point(548, 267);
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
            txtAbsenceIllness.Location = new Point(230, 270);
            txtAbsenceIllness.Multiline = true;
            txtAbsenceIllness.Name = "txtAbsenceIllness";
            txtAbsenceIllness.Size = new Size(97, 23);
            txtAbsenceIllness.TabIndex = 38;
            txtAbsenceIllness.Text = "Ab.ill";
            txtAbsenceIllness.TextAlign = HorizontalAlignment.Center;
            txtAbsenceIllness.Visible = false;
            // 
            // btnCallInSick
            // 
            btnCallInSick.BackColor = SystemColors.ActiveCaption;
            btnCallInSick.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCallInSick.Location = new Point(331, 2);
            btnCallInSick.Name = "btnCallInSick";
            btnCallInSick.Size = new Size(136, 30);
            btnCallInSick.TabIndex = 40;
            btnCallInSick.Text = "Call in Sick";
            btnCallInSick.UseVisualStyleBackColor = false;
            btnCallInSick.Click += btnCallInSick_Click;
            // 
            // listBoxLogs
            // 
            listBoxLogs.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listBoxLogs.FormattingEnabled = true;
            listBoxLogs.HorizontalScrollbar = true;
            listBoxLogs.Location = new Point(800, 35);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(383, 116);
            listBoxLogs.TabIndex = 41;
            listBoxLogs.SelectedIndexChanged += listBoxLogs_SelectedIndexChanged;
            // 
            // lblLoggings
            // 
            lblLoggings.AutoSize = true;
            lblLoggings.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblLoggings.Location = new Point(800, 17);
            lblLoggings.Name = "lblLoggings";
            lblLoggings.Size = new Size(63, 16);
            lblLoggings.TabIndex = 42;
            lblLoggings.Text = "Loggings";
            // 
            // txtAliasNotes
            // 
            txtAliasNotes.Enabled = false;
            txtAliasNotes.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAliasNotes.Location = new Point(5, 6);
            txtAliasNotes.Name = "txtAliasNotes";
            txtAliasNotes.PlaceholderText = "Alias";
            txtAliasNotes.ReadOnly = true;
            txtAliasNotes.Size = new Size(97, 26);
            txtAliasNotes.TabIndex = 44;
            txtAliasNotes.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCurrentDateReport
            // 
            txtCurrentDateReport.Enabled = false;
            txtCurrentDateReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCurrentDateReport.Location = new Point(151, 38);
            txtCurrentDateReport.Multiline = true;
            txtCurrentDateReport.Name = "txtCurrentDateReport";
            txtCurrentDateReport.Size = new Size(140, 23);
            txtCurrentDateReport.TabIndex = 45;
            txtCurrentDateReport.TextAlign = HorizontalAlignment.Center;
            // 
            // panelNotes
            // 
            panelNotes.BackColor = SystemColors.GradientActiveCaption;
            panelNotes.Controls.Add(buttonSaveReport);
            panelNotes.Controls.Add(buttonEmptyReport);
            panelNotes.Controls.Add(rtxNewReport);
            panelNotes.Controls.Add(comboBoxSubjectReport);
            panelNotes.Controls.Add(txtAliasNotes);
            panelNotes.Controls.Add(txtCurrentDateReport);
            panelNotes.Location = new Point(1189, 35);
            panelNotes.Name = "panelNotes";
            panelNotes.Size = new Size(301, 412);
            panelNotes.TabIndex = 46;
            // 
            // buttonSaveReport
            // 
            buttonSaveReport.BackColor = SystemColors.ActiveCaption;
            buttonSaveReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonSaveReport.Location = new Point(151, 381);
            buttonSaveReport.Name = "buttonSaveReport";
            buttonSaveReport.Size = new Size(140, 23);
            buttonSaveReport.TabIndex = 49;
            buttonSaveReport.Text = "Save Report";
            buttonSaveReport.UseVisualStyleBackColor = false;
            buttonSaveReport.Click += buttonSaveReport_Click;
            // 
            // buttonEmptyReport
            // 
            buttonEmptyReport.BackColor = SystemColors.ActiveCaption;
            buttonEmptyReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonEmptyReport.Location = new Point(5, 381);
            buttonEmptyReport.Name = "buttonEmptyReport";
            buttonEmptyReport.Size = new Size(140, 23);
            buttonEmptyReport.TabIndex = 48;
            buttonEmptyReport.Text = "Empty Report";
            buttonEmptyReport.UseVisualStyleBackColor = false;
            buttonEmptyReport.Click += buttonEmptyReport_Click;
            // 
            // rtxNewReport
            // 
            rtxNewReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtxNewReport.Location = new Point(5, 65);
            rtxNewReport.Name = "rtxNewReport";
            rtxNewReport.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtxNewReport.Size = new Size(288, 313);
            rtxNewReport.TabIndex = 47;
            rtxNewReport.Text = "";
            // 
            // comboBoxSubjectReport
            // 
            comboBoxSubjectReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBoxSubjectReport.FormattingEnabled = true;
            comboBoxSubjectReport.Items.AddRange(new object[] { "Report", "Memo", "Evaluation", "Other" });
            comboBoxSubjectReport.Location = new Point(5, 38);
            comboBoxSubjectReport.Name = "comboBoxSubjectReport";
            comboBoxSubjectReport.Size = new Size(140, 24);
            comboBoxSubjectReport.TabIndex = 46;
            comboBoxSubjectReport.Text = "Subject:";
            // 
            // listViewFiles
            // 
            listViewFiles.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listViewFiles.Font = new Font("Courier New", 9F, FontStyle.Bold);
            listViewFiles.FullRowSelect = true;
            listViewFiles.Location = new Point(800, 170);
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(383, 243);
            listViewFiles.TabIndex = 47;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.View = View.Details;
            listViewFiles.DoubleClick += listViewFiles_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "File";
            columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Date";
            columnHeader2.Width = 125;
            // 
            // lblReports
            // 
            lblReports.AutoSize = true;
            lblReports.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblReports.Location = new Point(800, 154);
            lblReports.Name = "lblReports";
            lblReports.Size = new Size(56, 16);
            lblReports.TabIndex = 48;
            lblReports.Text = "Reports";
            // 
            // AdminMainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblReports);
            Controls.Add(listViewFiles);
            Controls.Add(panelNotes);
            Controls.Add(lblLoggings);
            Controls.Add(listBoxLogs);
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
            Size = new Size(1566, 450);
            panelNotes.ResumeLayout(false);
            panelNotes.PerformLayout();
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
        public ListBox listBoxLogs;
        public Label lblLoggings;
        public TextBox txtAliasNotes;
        public TextBox txtCurrentDateReport;
        public Panel panelNotes;
        public ComboBox comboBoxSubjectReport;
        public Button buttonSaveReport;
        public Button buttonEmptyReport;
        public RichTextBox rtxNewReport;
        public ListView listViewFiles;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        public Label lblReports;
    }
}
