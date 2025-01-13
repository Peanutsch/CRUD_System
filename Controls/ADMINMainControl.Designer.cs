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
            this.listViewReports = new ListView();
            this.fileColumn = new ColumnHeader();
            this.createdColumn = new ColumnHeader();
            this.subjectColumn = new ColumnHeader();
            this.reportLBLReports = new Label();
            this.reportTxtDate = new TextBox();
            this.reportTxtAlias = new TextBox();
            this.comboBoxSubjectReport = new ComboBox();
            this.reportRichTxReport = new RichTextBox();
            this.btnCreateReport = new Button();
            this.btnSaveReport = new Button();
            this.reportTxtSubject = new TextBox();
            this.reportTxtCreator = new TextBox();
            this.reportLBLSelectedAlias = new Label();
            this.reportLBLCreatedBy = new Label();
            this.reportLBLCurrentDate = new Label();
            this.btnShowListBoxLogs = new Button();
            this.btnDeleteReport = new Button();
            this.chkIsTheOne = new CheckBox();
            this.btnUploadFile = new Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.txtName.Location = new Point(24, 320);
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
            this.txtEmail.Location = new Point(24, 384);
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
            this.txtAddress.Location = new Point(24, 352);
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
            this.txtCity.Location = new Point(473, 352);
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
            this.listBoxAdmin.HorizontalScrollbar = true;
            this.listBoxAdmin.ItemHeight = 15;
            this.listBoxAdmin.Location = new Point(24, 35);
            this.listBoxAdmin.Name = "listBoxAdmin";
            this.listBoxAdmin.Size = new Size(770, 244);
            this.listBoxAdmin.TabIndex = 0;
            this.listBoxAdmin.DrawItem += this.ListBoxAdmin_DrawItem;
            this.listBoxAdmin.SelectedIndexChanged += this.ListBoxAdmin_SelectedIndexChanged;
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.BackColor = SystemColors.ActiveCaption;
            this.btnCreateUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnCreateUser.Location = new Point(473, 2);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new Size(138, 30);
            this.btnCreateUser.TabIndex = 11;
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.UseVisualStyleBackColor = false;
            this.btnCreateUser.Click += this.btnCreateUser_Click;
            // 
            // btnSaveEditUserDetails
            // 
            this.btnSaveEditUserDetails.BackColor = Color.LightGreen;
            this.btnSaveEditUserDetails.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveEditUserDetails.Location = new Point(168, 419);
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
            this.btnDeleteUser.BackColor = Color.Red;
            this.btnDeleteUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnDeleteUser.Location = new Point(432, 419);
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
            this.txtSurname.Location = new Point(355, 320);
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
            this.txtAlias.Location = new Point(24, 288);
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
            this.txtZIPCode.Location = new Point(355, 352);
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
            this.txtPhonenumber.Location = new Point(473, 384);
            this.txtPhonenumber.Name = "txtPhonenumber";
            this.txtPhonenumber.PlaceholderText = "Phonenumber";
            this.txtPhonenumber.Size = new Size(218, 26);
            this.txtPhonenumber.TabIndex = 7;
            this.txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            this.txtPhonenumber.KeyDown += this.TxtPhonenumber_KeyDown;
            // 
            // btnGeneratePSW
            // 
            this.btnGeneratePSW.BackColor = SystemColors.ActiveCaption;
            this.btnGeneratePSW.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnGeneratePSW.Location = new Point(280, 419);
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
            this.btnEditUserDetails.Location = new Point(24, 419);
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
            this.txtAdmin.Location = new Point(697, 320);
            this.txtAdmin.Multiline = true;
            this.txtAdmin.Name = "txtAdmin";
            this.txtAdmin.Size = new Size(97, 26);
            this.txtAdmin.TabIndex = 26;
            this.txtAdmin.Text = "Admin";
            this.txtAdmin.TextAlign = HorizontalAlignment.Center;
            this.txtAdmin.Visible = false;
            // 
            // chkIsAdmin
            // 
            this.chkIsAdmin.AutoSize = true;
            this.chkIsAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.chkIsAdmin.Location = new Point(127, 289);
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
            this.btnForceLogOutUser.BackColor = Color.LightGreen;
            this.btnForceLogOutUser.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnForceLogOutUser.Location = new Point(389, 284);
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
            this.lblPageNumber.Location = new Point(626, 288);
            this.lblPageNumber.Name = "lblPageNumber";
            this.lblPageNumber.Size = new Size(84, 16);
            this.lblPageNumber.TabIndex = 35;
            this.lblPageNumber.Text = "Page 1 of 2";
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackColor = SystemColors.ActiveCaption;
            this.btnNextPage.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnNextPage.Location = new Point(716, 285);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new Size(75, 23);
            this.btnNextPage.TabIndex = 36;
            this.btnNextPage.Text = ">>>";
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += this.btnNextPage_Click;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.BackColor = SystemColors.ActiveCaption;
            this.btnPreviousPage.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.btnPreviousPage.Location = new Point(545, 285);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new Size(75, 23);
            this.btnPreviousPage.TabIndex = 37;
            this.btnPreviousPage.Text = "<<<";
            this.btnPreviousPage.UseVisualStyleBackColor = false;
            this.btnPreviousPage.Click += this.btnPreviousPage_Click;
            // 
            // txtAbsenceIllness
            // 
            this.txtAbsenceIllness.BackColor = Color.Violet;
            this.txtAbsenceIllness.Enabled = false;
            this.txtAbsenceIllness.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtAbsenceIllness.Location = new Point(697, 352);
            this.txtAbsenceIllness.Multiline = true;
            this.txtAbsenceIllness.Name = "txtAbsenceIllness";
            this.txtAbsenceIllness.Size = new Size(97, 26);
            this.txtAbsenceIllness.TabIndex = 38;
            this.txtAbsenceIllness.Text = "Ab.ill";
            this.txtAbsenceIllness.TextAlign = HorizontalAlignment.Center;
            this.txtAbsenceIllness.Visible = false;
            // 
            // btnCallInSick
            // 
            this.btnCallInSick.BackColor = SystemColors.ActiveCaption;
            this.btnCallInSick.Enabled = false;
            this.btnCallInSick.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCallInSick.Location = new Point(331, 2);
            this.btnCallInSick.Name = "btnCallInSick";
            this.btnCallInSick.Size = new Size(136, 30);
            this.btnCallInSick.TabIndex = 40;
            this.btnCallInSick.Text = "Call in Sick";
            this.btnCallInSick.UseVisualStyleBackColor = false;
            this.btnCallInSick.Click += this.btnCallInSick_Click;
            // 
            // listViewReports
            // 
            this.listViewReports.Columns.AddRange(new ColumnHeader[] { this.fileColumn, this.createdColumn, this.subjectColumn });
            this.listViewReports.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.listViewReports.FullRowSelect = true;
            this.listViewReports.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewReports.Location = new Point(800, 36);
            this.listViewReports.Name = "listViewReports";
            this.listViewReports.RightToLeft = RightToLeft.No;
            this.listViewReports.Size = new Size(392, 377);
            this.listViewReports.TabIndex = 47;
            this.listViewReports.UseCompatibleStateImageBehavior = false;
            this.listViewReports.View = View.Details;
            this.listViewReports.ColumnWidthChanging += this.listViewReports_ColumnWidthChanging;
            this.listViewReports.SelectedIndexChanged += this.listViewReports_SelectedIndexChanged;
            // 
            // fileColumn
            // 
            this.fileColumn.Text = "File";
            this.fileColumn.Width = 175;
            // 
            // createdColumn
            // 
            this.createdColumn.Text = "Created";
            this.createdColumn.Width = 75;
            // 
            // subjectColumn
            // 
            this.subjectColumn.Text = "Subject";
            this.subjectColumn.Width = 175;
            // 
            // reportLBLReports
            // 
            this.reportLBLReports.AutoSize = true;
            this.reportLBLReports.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.reportLBLReports.Location = new Point(800, 17);
            this.reportLBLReports.Name = "reportLBLReports";
            this.reportLBLReports.Size = new Size(56, 16);
            this.reportLBLReports.TabIndex = 48;
            this.reportLBLReports.Text = "Reports";
            // 
            // reportTxtDate
            // 
            this.reportTxtDate.Enabled = false;
            this.reportTxtDate.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.reportTxtDate.Location = new Point(1350, 65);
            this.reportTxtDate.Multiline = true;
            this.reportTxtDate.Name = "reportTxtDate";
            this.reportTxtDate.PlaceholderText = "Date";
            this.reportTxtDate.Size = new Size(140, 26);
            this.reportTxtDate.TabIndex = 45;
            this.reportTxtDate.TextAlign = HorizontalAlignment.Center;
            // 
            // reportTxtAlias
            // 
            this.reportTxtAlias.Enabled = false;
            this.reportTxtAlias.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.reportTxtAlias.Location = new Point(1202, 35);
            this.reportTxtAlias.Name = "reportTxtAlias";
            this.reportTxtAlias.PlaceholderText = "Alias";
            this.reportTxtAlias.ReadOnly = true;
            this.reportTxtAlias.Size = new Size(140, 26);
            this.reportTxtAlias.TabIndex = 44;
            this.reportTxtAlias.TextAlign = HorizontalAlignment.Center;
            // 
            // comboBoxSubjectReport
            // 
            this.comboBoxSubjectReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.comboBoxSubjectReport.FormattingEnabled = true;
            this.comboBoxSubjectReport.Items.AddRange(new object[] { "Update Details", "Note", "Report", "Evaluation", "Upload File", "Other" });
            this.comboBoxSubjectReport.Location = new Point(1202, 67);
            this.comboBoxSubjectReport.Name = "comboBoxSubjectReport";
            this.comboBoxSubjectReport.Size = new Size(140, 24);
            this.comboBoxSubjectReport.TabIndex = 46;
            this.comboBoxSubjectReport.Text = "Subject:";
            this.comboBoxSubjectReport.Visible = false;
            // 
            // reportRichTxReport
            // 
            this.reportRichTxReport.BackColor = Color.LightGray;
            this.reportRichTxReport.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.reportRichTxReport.Location = new Point(1202, 97);
            this.reportRichTxReport.Name = "reportRichTxReport";
            this.reportRichTxReport.ReadOnly = true;
            this.reportRichTxReport.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.reportRichTxReport.Size = new Size(288, 316);
            this.reportRichTxReport.TabIndex = 47;
            this.reportRichTxReport.Text = "";
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.BackColor = SystemColors.ActiveCaption;
            this.btnCreateReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCreateReport.Location = new Point(1202, 419);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new Size(140, 30);
            this.btnCreateReport.TabIndex = 48;
            this.btnCreateReport.Text = "Report";
            this.btnCreateReport.UseVisualStyleBackColor = false;
            this.btnCreateReport.Visible = false;
            this.btnCreateReport.Click += this.btnCreateReport_Click;
            // 
            // btnSaveReport
            // 
            this.btnSaveReport.BackColor = Color.LightGreen;
            this.btnSaveReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveReport.Location = new Point(1350, 419);
            this.btnSaveReport.Name = "btnSaveReport";
            this.btnSaveReport.Size = new Size(140, 30);
            this.btnSaveReport.TabIndex = 49;
            this.btnSaveReport.Text = "Save Report";
            this.btnSaveReport.UseVisualStyleBackColor = false;
            this.btnSaveReport.Visible = false;
            this.btnSaveReport.Click += this.btnSaveReport_Click;
            // 
            // reportTxtSubject
            // 
            this.reportTxtSubject.Enabled = false;
            this.reportTxtSubject.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.reportTxtSubject.Location = new Point(1202, 65);
            this.reportTxtSubject.Multiline = true;
            this.reportTxtSubject.Name = "reportTxtSubject";
            this.reportTxtSubject.PlaceholderText = "Subject";
            this.reportTxtSubject.Size = new Size(140, 26);
            this.reportTxtSubject.TabIndex = 50;
            this.reportTxtSubject.TextAlign = HorizontalAlignment.Center;
            // 
            // reportTxtCreator
            // 
            this.reportTxtCreator.Enabled = false;
            this.reportTxtCreator.Font = new Font("Courier New", 12F, FontStyle.Bold);
            this.reportTxtCreator.Location = new Point(1350, 35);
            this.reportTxtCreator.Multiline = true;
            this.reportTxtCreator.Name = "reportTxtCreator";
            this.reportTxtCreator.PlaceholderText = "Creator";
            this.reportTxtCreator.Size = new Size(140, 26);
            this.reportTxtCreator.TabIndex = 51;
            this.reportTxtCreator.TextAlign = HorizontalAlignment.Center;
            // 
            // reportLBLSelectedAlias
            // 
            this.reportLBLSelectedAlias.AutoSize = true;
            this.reportLBLSelectedAlias.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.reportLBLSelectedAlias.Location = new Point(1202, 16);
            this.reportLBLSelectedAlias.Name = "reportLBLSelectedAlias";
            this.reportLBLSelectedAlias.Size = new Size(105, 16);
            this.reportLBLSelectedAlias.TabIndex = 52;
            this.reportLBLSelectedAlias.Text = "Selected Alias";
            // 
            // reportLBLCreatedBy
            // 
            this.reportLBLCreatedBy.AutoSize = true;
            this.reportLBLCreatedBy.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.reportLBLCreatedBy.Location = new Point(1350, 16);
            this.reportLBLCreatedBy.Name = "reportLBLCreatedBy";
            this.reportLBLCreatedBy.Size = new Size(77, 16);
            this.reportLBLCreatedBy.TabIndex = 53;
            this.reportLBLCreatedBy.Text = "Created by";
            // 
            // reportLBLCurrentDate
            // 
            this.reportLBLCurrentDate.AutoSize = true;
            this.reportLBLCurrentDate.Font = new Font("Courier New", 9F, FontStyle.Bold);
            this.reportLBLCurrentDate.Location = new Point(1350, 46);
            this.reportLBLCurrentDate.Name = "reportLBLCurrentDate";
            this.reportLBLCurrentDate.Size = new Size(91, 16);
            this.reportLBLCurrentDate.TabIndex = 54;
            this.reportLBLCurrentDate.Text = "Current Date";
            this.reportLBLCurrentDate.Visible = false;
            // 
            // btnShowListBoxLogs
            // 
            this.btnShowListBoxLogs.BackColor = SystemColors.ActiveCaption;
            this.btnShowListBoxLogs.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnShowListBoxLogs.Location = new Point(800, 419);
            this.btnShowListBoxLogs.Name = "btnShowListBoxLogs";
            this.btnShowListBoxLogs.Size = new Size(127, 30);
            this.btnShowListBoxLogs.TabIndex = 58;
            this.btnShowListBoxLogs.Text = "ListBoxLogs";
            this.btnShowListBoxLogs.UseVisualStyleBackColor = false;
            this.btnShowListBoxLogs.Visible = false;
            this.btnShowListBoxLogs.Click += this.btnShowListBoxLogs_Click;
            // 
            // btnDeleteReport
            // 
            this.btnDeleteReport.BackColor = Color.Red;
            this.btnDeleteReport.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnDeleteReport.Location = new Point(933, 419);
            this.btnDeleteReport.Name = "btnDeleteReport";
            this.btnDeleteReport.Size = new Size(126, 30);
            this.btnDeleteReport.TabIndex = 59;
            this.btnDeleteReport.Text = "Delete File";
            this.btnDeleteReport.UseVisualStyleBackColor = false;
            this.btnDeleteReport.Visible = false;
            this.btnDeleteReport.Click += this.btnDeleteReport_Click;
            // 
            // chkIsTheOne
            // 
            this.chkIsTheOne.AutoSize = true;
            this.chkIsTheOne.CheckAlign = ContentAlignment.MiddleRight;
            this.chkIsTheOne.Enabled = false;
            this.chkIsTheOne.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.chkIsTheOne.Location = new Point(242, 289);
            this.chkIsTheOne.Name = "chkIsTheOne";
            this.chkIsTheOne.Size = new Size(107, 22);
            this.chkIsTheOne.TabIndex = 60;
            this.chkIsTheOne.Text = "isTheOne";
            this.chkIsTheOne.TextAlign = ContentAlignment.MiddleRight;
            this.chkIsTheOne.UseVisualStyleBackColor = true;
            this.chkIsTheOne.Visible = false;
            this.chkIsTheOne.CheckedChanged += this.chkIsTheOne_CheckedChanged;
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.BackColor = SystemColors.ActiveCaption;
            this.btnUploadFile.Enabled = false;
            this.btnUploadFile.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnUploadFile.Location = new Point(1065, 419);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new Size(127, 30);
            this.btnUploadFile.TabIndex = 61;
            this.btnUploadFile.Text = "Upload File";
            this.btnUploadFile.UseVisualStyleBackColor = false;
            this.btnUploadFile.Visible = false;
            this.btnUploadFile.Click += this.btnUploadFile_Click;
            // 
            // AdminMainControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaption;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.btnUploadFile);
            this.Controls.Add(this.chkIsTheOne);
            this.Controls.Add(this.btnDeleteReport);
            this.Controls.Add(this.btnShowListBoxLogs);
            this.Controls.Add(this.reportLBLCurrentDate);
            this.Controls.Add(this.reportLBLCreatedBy);
            this.Controls.Add(this.reportLBLSelectedAlias);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.btnSaveReport);
            this.Controls.Add(this.reportTxtAlias);
            this.Controls.Add(this.reportTxtSubject);
            this.Controls.Add(this.reportRichTxReport);
            this.Controls.Add(this.reportTxtDate);
            this.Controls.Add(this.reportTxtCreator);
            this.Controls.Add(this.reportLBLReports);
            this.Controls.Add(this.listViewReports);
            this.Controls.Add(this.comboBoxSubjectReport);
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
            this.Size = new Size(1566, 513);
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
        public Button btnShowListBoxLogs;
        public Button btnDeleteReport;
        public CheckBox chkIsTheOne;
        public Button btnUploadFile;
        private ColumnHeader createdColumn;
    }
}
