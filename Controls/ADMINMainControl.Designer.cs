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
            txtCurrentDateNotes = new TextBox();
            panelNotes = new Panel();
            buttonSaveNote = new Button();
            buttonEmptyNote = new Button();
            richTextBoxNotefield = new RichTextBox();
            comboBoxSubjectNotes = new ComboBox();
            listViewFiles = new ListView();
            lvlListViewFiles = new Label();
            panelNotes.SuspendLayout();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Enabled = false;
            txtName.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtName.Location = new Point(127, 270);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Name";
            txtName.Size = new Size(325, 26);
            txtName.TabIndex = 1;
            txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtEmail
            // 
            txtEmail.Enabled = false;
            txtEmail.Font = new Font("Courier New", 12F, FontStyle.Bold);
            txtEmail.Location = new Point(127, 328);
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
            txtAddress.Location = new Point(127, 299);
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
            txtCity.Location = new Point(576, 299);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "City";
            txtCity.Size = new Size(218, 26);
            txtCity.TabIndex = 5;
            txtCity.TextAlign = HorizontalAlignment.Center;
            // 
            // listBoxAdmin
            // 
            listBoxAdmin.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxAdmin.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listBoxAdmin.ItemHeight = 15;
            listBoxAdmin.Location = new Point(24, 35);
            listBoxAdmin.Name = "listBoxAdmin";
            listBoxAdmin.Size = new Size(932, 229);
            listBoxAdmin.TabIndex = 0;
            listBoxAdmin.DrawItem += ListBoxAdmin_DrawItem;
            listBoxAdmin.SelectedIndexChanged += ListBoxAdmin_SelectedIndexChanged;
            // 
            // btnCreateUser
            // 
            btnCreateUser.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnCreateUser.Location = new Point(635, 3);
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
            btnSaveEditUserDetails.Location = new Point(270, 357);
            btnSaveEditUserDetails.Name = "btnSaveEditUserDetails";
            btnSaveEditUserDetails.Size = new Size(72, 30);
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
            btnDeleteUser.Location = new Point(500, 357);
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
            txtSurname.Location = new Point(458, 270);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(336, 26);
            txtSurname.TabIndex = 2;
            txtSurname.TextAlign = HorizontalAlignment.Center;
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
            txtZIPCode.Location = new Point(458, 299);
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
            txtPhonenumber.Location = new Point(576, 328);
            txtPhonenumber.Name = "txtPhonenumber";
            txtPhonenumber.PlaceholderText = "Phonenumber";
            txtPhonenumber.Size = new Size(218, 26);
            txtPhonenumber.TabIndex = 7;
            txtPhonenumber.TextAlign = HorizontalAlignment.Center;
            // 
            // btnGeneratePSW
            // 
            btnGeneratePSW.BackColor = SystemColors.ActiveCaption;
            btnGeneratePSW.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnGeneratePSW.Location = new Point(348, 357);
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
            btnEditUserDetails.Location = new Point(126, 357);
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
            txtAdmin.Location = new Point(24, 299);
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
            chkIsAdmin.Location = new Point(24, 362);
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
            btnForceLogOutUser.Location = new Point(806, 357);
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
            txtSearch.Location = new Point(863, 6);
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
            lblSearchTxt.Location = new Point(779, 9);
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
            lblPageNumber.Font = new Font("Courier New", 12F, FontStyle.Bold);
            lblPageNumber.Location = new Point(821, 267);
            lblPageNumber.Name = "lblPageNumber";
            lblPageNumber.Size = new Size(118, 18);
            lblPageNumber.TabIndex = 35;
            lblPageNumber.Text = "Page 1 of 2";
            // 
            // btnNextPage
            // 
            btnNextPage.BackColor = SystemColors.ActiveCaption;
            btnNextPage.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnNextPage.Location = new Point(881, 288);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(75, 23);
            btnNextPage.TabIndex = 36;
            btnNextPage.Text = "Next Page";
            btnNextPage.UseVisualStyleBackColor = false;
            btnNextPage.Click += btnNextPage_Click;
            // 
            // btnPreviousPage
            // 
            btnPreviousPage.BackColor = SystemColors.ActiveCaption;
            btnPreviousPage.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnPreviousPage.Location = new Point(806, 288);
            btnPreviousPage.Name = "btnPreviousPage";
            btnPreviousPage.Size = new Size(75, 23);
            btnPreviousPage.TabIndex = 37;
            btnPreviousPage.Text = "Prev Page";
            btnPreviousPage.UseVisualStyleBackColor = false;
            btnPreviousPage.Click += btnPreviousPage_Click;
            // 
            // txtAbsenceIllness
            // 
            txtAbsenceIllness.BackColor = Color.Violet;
            txtAbsenceIllness.Enabled = false;
            txtAbsenceIllness.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAbsenceIllness.Location = new Point(24, 328);
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
            btnCallInSick.Location = new Point(806, 321);
            btnCallInSick.Name = "btnCallInSick";
            btnCallInSick.Size = new Size(150, 30);
            btnCallInSick.TabIndex = 40;
            btnCallInSick.Text = "Call in Sick";
            btnCallInSick.UseVisualStyleBackColor = false;
            btnCallInSick.Click += btnCallInSick_Click;
            // 
            // listBoxLogs
            // 
            listBoxLogs.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listBoxLogs.FormattingEnabled = true;
            listBoxLogs.Location = new Point(962, 35);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(530, 116);
            listBoxLogs.TabIndex = 41;
            listBoxLogs.SelectedIndexChanged += listBoxLogs_SelectedIndexChanged;
            // 
            // lblLoggings
            // 
            lblLoggings.AutoSize = true;
            lblLoggings.Font = new Font("Courier New", 9F, FontStyle.Bold);
            lblLoggings.Location = new Point(962, 17);
            lblLoggings.Name = "lblLoggings";
            lblLoggings.Size = new Size(63, 16);
            lblLoggings.TabIndex = 42;
            lblLoggings.Text = "Loggings";
            // 
            // txtAliasNotes
            // 
            txtAliasNotes.Enabled = false;
            txtAliasNotes.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAliasNotes.Location = new Point(3, 3);
            txtAliasNotes.Name = "txtAliasNotes";
            txtAliasNotes.PlaceholderText = "Alias";
            txtAliasNotes.ReadOnly = true;
            txtAliasNotes.Size = new Size(97, 26);
            txtAliasNotes.TabIndex = 44;
            txtAliasNotes.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCurrentDateNotes
            // 
            txtCurrentDateNotes.Enabled = false;
            txtCurrentDateNotes.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCurrentDateNotes.Location = new Point(151, 38);
            txtCurrentDateNotes.Multiline = true;
            txtCurrentDateNotes.Name = "txtCurrentDateNotes";
            txtCurrentDateNotes.Size = new Size(140, 23);
            txtCurrentDateNotes.TabIndex = 45;
            txtCurrentDateNotes.TextAlign = HorizontalAlignment.Center;
            // 
            // panelNotes
            // 
            panelNotes.BackColor = SystemColors.GradientActiveCaption;
            panelNotes.Controls.Add(buttonSaveNote);
            panelNotes.Controls.Add(buttonEmptyNote);
            panelNotes.Controls.Add(richTextBoxNotefield);
            panelNotes.Controls.Add(comboBoxSubjectNotes);
            panelNotes.Controls.Add(txtAliasNotes);
            panelNotes.Controls.Add(txtCurrentDateNotes);
            panelNotes.Location = new Point(1189, 157);
            panelNotes.Name = "panelNotes";
            panelNotes.Size = new Size(303, 290);
            panelNotes.TabIndex = 46;
            // 
            // buttonSaveNote
            // 
            buttonSaveNote.BackColor = SystemColors.ActiveCaption;
            buttonSaveNote.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonSaveNote.Location = new Point(151, 262);
            buttonSaveNote.Name = "buttonSaveNote";
            buttonSaveNote.Size = new Size(140, 23);
            buttonSaveNote.TabIndex = 49;
            buttonSaveNote.Text = "Save Note";
            buttonSaveNote.UseVisualStyleBackColor = false;
            // 
            // buttonEmptyNote
            // 
            buttonEmptyNote.BackColor = SystemColors.ActiveCaption;
            buttonEmptyNote.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonEmptyNote.Location = new Point(3, 262);
            buttonEmptyNote.Name = "buttonEmptyNote";
            buttonEmptyNote.Size = new Size(140, 23);
            buttonEmptyNote.TabIndex = 48;
            buttonEmptyNote.Text = "Empty Note";
            buttonEmptyNote.UseVisualStyleBackColor = false;
            // 
            // richTextBoxNotefield
            // 
            richTextBoxNotefield.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextBoxNotefield.Location = new Point(3, 65);
            richTextBoxNotefield.Name = "richTextBoxNotefield";
            richTextBoxNotefield.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBoxNotefield.Size = new Size(288, 191);
            richTextBoxNotefield.TabIndex = 47;
            richTextBoxNotefield.Text = "Your Notes here!";
            // 
            // comboBoxSubjectNotes
            // 
            comboBoxSubjectNotes.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBoxSubjectNotes.FormattingEnabled = true;
            comboBoxSubjectNotes.Items.AddRange(new object[] { "Subject 1", "Subject 2", "Subject 3", "Subject 4" });
            comboBoxSubjectNotes.Location = new Point(0, 32);
            comboBoxSubjectNotes.Name = "comboBoxSubjectNotes";
            comboBoxSubjectNotes.Size = new Size(140, 24);
            comboBoxSubjectNotes.TabIndex = 46;
            comboBoxSubjectNotes.Text = "Subject:";
            // 
            // listViewFiles
            // 
            listViewFiles.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listViewFiles.Location = new Point(965, 160);
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(221, 253);
            listViewFiles.TabIndex = 47;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.View = View.Details;
            // 
            // lvlListViewFiles
            // 
            lvlListViewFiles.AutoSize = true;
            lvlListViewFiles.Font = new Font("Courier New", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lvlListViewFiles.Location = new Point(1000, 421);
            lvlListViewFiles.Name = "lvlListViewFiles";
            lvlListViewFiles.Size = new Size(138, 18);
            lvlListViewFiles.TabIndex = 48;
            lvlListViewFiles.Text = "ListViewFiles";
            // 
            // AdminMainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lvlListViewFiles);
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
        public TextBox txtCurrentDateNotes;
        public Panel panelNotes;
        public ComboBox comboBoxSubjectNotes;
        public Button buttonSaveNote;
        public Button buttonEmptyNote;
        public RichTextBox richTextBoxNotefield;
        public ListView listViewFiles;
        public Label lvlListViewFiles;
    }
}
