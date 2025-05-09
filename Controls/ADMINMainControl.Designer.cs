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
            listViewReports = new ListView();
            fileColumn = new ColumnHeader();
            createdColumn = new ColumnHeader();
            subjectColumn = new ColumnHeader();
            reportLBLReports = new Label();
            reportTxtDate = new TextBox();
            reportTxtAlias = new TextBox();
            comboBoxSubjectReport = new ComboBox();
            reportRichTxReport = new RichTextBox();
            btnCreateReport = new Button();
            btnSaveReport = new Button();
            reportTxtSubject = new TextBox();
            reportTxtCreator = new TextBox();
            reportLBLSelectedAlias = new Label();
            reportLBLCreatedBy = new Label();
            reportLBLCurrentDate = new Label();
            btnShowListBoxLogEvents = new Button();
            btnDeleteFileReport = new Button();
            chkIsTheOne = new CheckBox();
            btnUploadFile = new Button();
            btnShowLogsStatus = new Button();
            lblOverview = new Label();
            lblActions = new Label();
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
            btnCreateUser.BackColor = SystemColors.ActiveCaption;
            btnCreateUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnCreateUser.Location = new Point(800, 123);
            btnCreateUser.Name = "btnCreateUser";
            btnCreateUser.Size = new Size(135, 30);
            btnCreateUser.TabIndex = 11;
            btnCreateUser.Text = "Create User";
            btnCreateUser.UseVisualStyleBackColor = false;
            btnCreateUser.Click += btnCreateUser_Click;
            // 
            // btnSaveEditUserDetails
            // 
            btnSaveEditUserDetails.BackColor = Color.LightGreen;
            btnSaveEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveEditUserDetails.Location = new Point(186, 419);
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
            btnDeleteUser.BackColor = Color.Red;
            btnDeleteUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnDeleteUser.Location = new Point(298, 419);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(135, 30);
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
            txtPhonenumber.KeyDown += TxtPhonenumber_KeyDown;
            // 
            // btnGeneratePSW
            // 
            btnGeneratePSW.BackColor = SystemColors.ActiveCaption;
            btnGeneratePSW.Enabled = false;
            btnGeneratePSW.Font = new Font("Courier New", 11F, FontStyle.Bold);
            btnGeneratePSW.Location = new Point(800, 231);
            btnGeneratePSW.Name = "btnGeneratePSW";
            btnGeneratePSW.Size = new Size(137, 30);
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
            btnEditUserDetails.Size = new Size(156, 30);
            btnEditUserDetails.TabIndex = 25;
            btnEditUserDetails.Text = "Unlock Details";
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
            chkIsAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkIsAdmin.Location = new Point(127, 289);
            chkIsAdmin.Name = "chkIsAdmin";
            chkIsAdmin.Size = new Size(97, 22);
            chkIsAdmin.TabIndex = 8;
            chkIsAdmin.Text = "isAdmin";
            chkIsAdmin.TextAlign = ContentAlignment.MiddleRight;
            chkIsAdmin.UseVisualStyleBackColor = true;
            chkIsAdmin.Visible = false;
            chkIsAdmin.CheckState = CheckState.Unchecked;
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
            btnForceLogOutUser.Location = new Point(389, 284);
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
            btnNextPage.Text = ">>>";
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
            btnPreviousPage.Text = "<<<";
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
            btnCallInSick.Location = new Point(800, 159);
            btnCallInSick.Name = "btnCallInSick";
            btnCallInSick.Size = new Size(136, 30);
            btnCallInSick.TabIndex = 40;
            btnCallInSick.Text = "Call in Sick";
            btnCallInSick.UseVisualStyleBackColor = false;
            btnCallInSick.Visible = false;
            btnCallInSick.Click += btnCallInSick_Click;
            // 
            // listViewReports
            // 
            listViewReports.Columns.AddRange(new ColumnHeader[] { fileColumn, createdColumn, subjectColumn });
            listViewReports.Font = new Font("Courier New", 9F, FontStyle.Bold);
            listViewReports.FullRowSelect = true;
            listViewReports.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listViewReports.Location = new Point(941, 33);
            listViewReports.Name = "listViewReports";
            listViewReports.RightToLeft = RightToLeft.No;
            listViewReports.Size = new Size(392, 377);
            listViewReports.TabIndex = 47;
            listViewReports.UseCompatibleStateImageBehavior = false;
            listViewReports.View = View.Details;
            listViewReports.ColumnWidthChanging += listViewReports_ColumnWidthChanging;
            listViewReports.SelectedIndexChanged += listViewReports_SelectedIndexChanged;
            // 
            // fileColumn
            // 
            fileColumn.Text = "File";
            fileColumn.Width = 175;
            // 
            // createdColumn
            // 
            createdColumn.Text = "Created";
            createdColumn.Width = 75;
            // 
            // subjectColumn
            // 
            subjectColumn.Text = "Subject";
            subjectColumn.Width = 175;
            // 
            // reportLBLReports
            // 
            reportLBLReports.AutoSize = true;
            reportLBLReports.Font = new Font("Courier New", 9F, FontStyle.Bold);
            reportLBLReports.Location = new Point(942, 16);
            reportLBLReports.Name = "reportLBLReports";
            reportLBLReports.Size = new Size(56, 16);
            reportLBLReports.TabIndex = 48;
            reportLBLReports.Text = "Reports";
            // 
            // reportTxtDate
            // 
            reportTxtDate.Enabled = false;
            reportTxtDate.Font = new Font("Courier New", 12F, FontStyle.Bold);
            reportTxtDate.Location = new Point(1488, 63);
            reportTxtDate.Multiline = true;
            reportTxtDate.Name = "reportTxtDate";
            reportTxtDate.PlaceholderText = "Date";
            reportTxtDate.Size = new Size(140, 26);
            reportTxtDate.TabIndex = 45;
            reportTxtDate.TextAlign = HorizontalAlignment.Center;
            // 
            // reportTxtAlias
            // 
            reportTxtAlias.Enabled = false;
            reportTxtAlias.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            reportTxtAlias.Location = new Point(1340, 34);
            reportTxtAlias.Name = "reportTxtAlias";
            reportTxtAlias.PlaceholderText = "Alias";
            reportTxtAlias.ReadOnly = true;
            reportTxtAlias.Size = new Size(140, 26);
            reportTxtAlias.TabIndex = 44;
            reportTxtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // comboBoxSubjectReport
            // 
            comboBoxSubjectReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBoxSubjectReport.FormattingEnabled = true;
            comboBoxSubjectReport.Items.AddRange(new object[] { "Update Details", "Note", "Report", "Evaluation", "Upload File", "Other" });
            comboBoxSubjectReport.Location = new Point(1340, 64);
            comboBoxSubjectReport.Name = "comboBoxSubjectReport";
            comboBoxSubjectReport.Size = new Size(140, 24);
            comboBoxSubjectReport.TabIndex = 46;
            comboBoxSubjectReport.Text = "Subject:";
            comboBoxSubjectReport.Visible = false;
            // 
            // reportRichTxReport
            // 
            reportRichTxReport.BackColor = Color.LightGray;
            reportRichTxReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            reportRichTxReport.Location = new Point(1340, 94);
            reportRichTxReport.Name = "reportRichTxReport";
            reportRichTxReport.ReadOnly = true;
            reportRichTxReport.ScrollBars = RichTextBoxScrollBars.Vertical;
            reportRichTxReport.Size = new Size(288, 316);
            reportRichTxReport.TabIndex = 47;
            reportRichTxReport.Text = "";
            // 
            // btnCreateReport
            // 
            btnCreateReport.BackColor = SystemColors.ActiveCaption;
            btnCreateReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCreateReport.Location = new Point(1340, 416);
            btnCreateReport.Name = "btnCreateReport";
            btnCreateReport.Size = new Size(140, 30);
            btnCreateReport.TabIndex = 48;
            btnCreateReport.Text = "Report";
            btnCreateReport.UseVisualStyleBackColor = false;
            btnCreateReport.Visible = false;
            btnCreateReport.Click += btnCreateReport_Click;
            // 
            // btnSaveReport
            // 
            btnSaveReport.BackColor = Color.LightGreen;
            btnSaveReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveReport.Location = new Point(1487, 416);
            btnSaveReport.Name = "btnSaveReport";
            btnSaveReport.Size = new Size(140, 30);
            btnSaveReport.TabIndex = 49;
            btnSaveReport.Text = "Save Report";
            btnSaveReport.UseVisualStyleBackColor = false;
            btnSaveReport.Visible = false;
            btnSaveReport.Click += btnSaveReport_Click;
            // 
            // reportTxtSubject
            // 
            reportTxtSubject.Enabled = false;
            reportTxtSubject.Font = new Font("Courier New", 12F, FontStyle.Bold);
            reportTxtSubject.Location = new Point(1340, 63);
            reportTxtSubject.Multiline = true;
            reportTxtSubject.Name = "reportTxtSubject";
            reportTxtSubject.PlaceholderText = "Subject";
            reportTxtSubject.Size = new Size(140, 26);
            reportTxtSubject.TabIndex = 50;
            reportTxtSubject.TextAlign = HorizontalAlignment.Center;
            // 
            // reportTxtCreator
            // 
            reportTxtCreator.Enabled = false;
            reportTxtCreator.Font = new Font("Courier New", 12F, FontStyle.Bold);
            reportTxtCreator.Location = new Point(1488, 34);
            reportTxtCreator.Multiline = true;
            reportTxtCreator.Name = "reportTxtCreator";
            reportTxtCreator.PlaceholderText = "Creator";
            reportTxtCreator.Size = new Size(140, 26);
            reportTxtCreator.TabIndex = 51;
            reportTxtCreator.TextAlign = HorizontalAlignment.Center;
            // 
            // reportLBLSelectedAlias
            // 
            reportLBLSelectedAlias.AutoSize = true;
            reportLBLSelectedAlias.Font = new Font("Courier New", 9F, FontStyle.Bold);
            reportLBLSelectedAlias.Location = new Point(1340, 17);
            reportLBLSelectedAlias.Name = "reportLBLSelectedAlias";
            reportLBLSelectedAlias.Size = new Size(105, 16);
            reportLBLSelectedAlias.TabIndex = 52;
            reportLBLSelectedAlias.Text = "Selected Alias";
            // 
            // reportLBLCreatedBy
            // 
            reportLBLCreatedBy.AutoSize = true;
            reportLBLCreatedBy.Font = new Font("Courier New", 9F, FontStyle.Bold);
            reportLBLCreatedBy.Location = new Point(1487, 17);
            reportLBLCreatedBy.Name = "reportLBLCreatedBy";
            reportLBLCreatedBy.Size = new Size(77, 16);
            reportLBLCreatedBy.TabIndex = 53;
            reportLBLCreatedBy.Text = "Created by";
            // 
            // reportLBLCurrentDate
            // 
            reportLBLCurrentDate.AutoSize = true;
            reportLBLCurrentDate.Font = new Font("Courier New", 9F, FontStyle.Bold);
            reportLBLCurrentDate.Location = new Point(1488, 43);
            reportLBLCurrentDate.Name = "reportLBLCurrentDate";
            reportLBLCurrentDate.Size = new Size(91, 16);
            reportLBLCurrentDate.TabIndex = 54;
            reportLBLCurrentDate.Text = "Current Date";
            reportLBLCurrentDate.Visible = false;
            // 
            // btnShowListBoxLogEvents
            // 
            btnShowListBoxLogEvents.BackColor = SystemColors.ActiveCaption;
            btnShowListBoxLogEvents.Enabled = false;
            btnShowListBoxLogEvents.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnShowListBoxLogEvents.Location = new Point(799, 71);
            btnShowListBoxLogEvents.Name = "btnShowListBoxLogEvents";
            btnShowListBoxLogEvents.Size = new Size(136, 30);
            btnShowListBoxLogEvents.TabIndex = 58;
            btnShowListBoxLogEvents.Text = "Logs Events";
            btnShowListBoxLogEvents.UseVisualStyleBackColor = false;
            btnShowListBoxLogEvents.Visible = false;
            btnShowListBoxLogEvents.Click += btnShowListBoxLogs_Click;
            // 
            // btnDeleteFileReport
            // 
            btnDeleteFileReport.BackColor = Color.Red;
            btnDeleteFileReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDeleteFileReport.Location = new Point(941, 416);
            btnDeleteFileReport.Name = "btnDeleteFileReport";
            btnDeleteFileReport.Size = new Size(146, 30);
            btnDeleteFileReport.TabIndex = 59;
            btnDeleteFileReport.Text = "Delete Report";
            btnDeleteFileReport.UseVisualStyleBackColor = false;
            btnDeleteFileReport.Visible = false;
            btnDeleteFileReport.Click += btnDeleteFileReport_Click;
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
            chkIsTheOne.Visible = false;
            chkIsTheOne.CheckState = CheckState.Unchecked;
            chkIsTheOne.CheckedChanged += chkIsTheOne_CheckedChanged;
            // 
            // btnUploadFile
            // 
            btnUploadFile.BackColor = SystemColors.ActiveCaption;
            btnUploadFile.Enabled = false;
            btnUploadFile.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUploadFile.Location = new Point(800, 195);
            btnUploadFile.Name = "btnUploadFile";
            btnUploadFile.Size = new Size(135, 30);
            btnUploadFile.TabIndex = 61;
            btnUploadFile.Text = "Upload File";
            btnUploadFile.UseVisualStyleBackColor = false;
            btnUploadFile.Visible = false;
            btnUploadFile.Click += btnUploadFile_Click;
            // 
            // btnShowLogsStatus
            // 
            btnShowLogsStatus.BackColor = SystemColors.ActiveCaption;
            btnShowLogsStatus.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnShowLogsStatus.Location = new Point(800, 35);
            btnShowLogsStatus.Name = "btnShowLogsStatus";
            btnShowLogsStatus.Size = new Size(135, 30);
            btnShowLogsStatus.TabIndex = 62;
            btnShowLogsStatus.Text = "Logs Status";
            btnShowLogsStatus.UseVisualStyleBackColor = false;
            btnShowLogsStatus.Click += btnShowLogsStatus_Click;
            // 
            // lblOverview
            // 
            lblOverview.AutoSize = true;
            lblOverview.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblOverview.Location = new Point(800, 16);
            lblOverview.Name = "lblOverview";
            lblOverview.Size = new Size(63, 16);
            lblOverview.TabIndex = 63;
            lblOverview.Text = "Overview";
            // 
            // lblActions
            // 
            lblActions.AutoSize = true;
            lblActions.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblActions.Location = new Point(801, 104);
            lblActions.Name = "lblActions";
            lblActions.Size = new Size(56, 16);
            lblActions.TabIndex = 64;
            lblActions.Text = "Actions";
            // 
            // AdminMainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblActions);
            Controls.Add(lblOverview);
            Controls.Add(btnShowLogsStatus);
            Controls.Add(btnUploadFile);
            Controls.Add(chkIsTheOne);
            Controls.Add(btnDeleteFileReport);
            Controls.Add(btnShowListBoxLogEvents);
            Controls.Add(reportLBLCurrentDate);
            Controls.Add(reportLBLCreatedBy);
            Controls.Add(reportLBLSelectedAlias);
            Controls.Add(btnCreateReport);
            Controls.Add(btnSaveReport);
            Controls.Add(reportTxtAlias);
            Controls.Add(reportTxtSubject);
            Controls.Add(reportRichTxReport);
            Controls.Add(reportTxtDate);
            Controls.Add(reportTxtCreator);
            Controls.Add(reportLBLReports);
            Controls.Add(listViewReports);
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
            Size = new Size(1646, 513);
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
        public ListView listViewReports;
        private ColumnHeader fileColumn;
        private ColumnHeader subjectColumn;
        public Label reportLBLReports;
        public TextBox reportTxtDate;
        public TextBox reportTxtAlias;
        public ComboBox comboBoxSubjectReport;
        public RichTextBox reportRichTxReport;
        public Button btnCreateReport;
        public Button btnSaveReport;
        public TextBox reportTxtSubject;
        public TextBox reportTxtCreator;
        public Label reportLBLSelectedAlias;
        public Label reportLBLCreatedBy;
        public Label reportLBLCurrentDate;
        public Button btnShowListBoxLogEvents;
        public Button btnDeleteFileReport;
        public CheckBox chkIsTheOne;
        public Button btnUploadFile;
        private ColumnHeader createdColumn;
        public Button btnShowLogsStatus;
        public Label lblOverview;
        public Label lblActions;
    }
}