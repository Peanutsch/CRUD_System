using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Repositories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CRUD_System
{
    public partial class CreateNewPasswordForm : Form
    {
        // Password conditions
        public int lengthPsw = 12;
        public int charToUpper = 3;
        public int charIsDigi = 3;

        AccountManager accountManager = new AccountManager();
        AdminMainControl adminControl = new AdminMainControl();
        RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        RepositoryLogEvents logEvents = new RepositoryLogEvents();

        readonly FilePaths path = new FilePaths();

        private bool isPasswordVisible = false;

        #region CONSTRUCTOR
        public CreateNewPasswordForm()
        {
            InitializeComponent();

            TxtLabelPSW();
            EnterKey();
        }
        #endregion CONSTRUCTOR

        private void Form_Load(object sender, EventArgs e)
        {
            this.ActiveControl = inputConfirmPSW;
        }

        private void inputChangePSW1_TextChanged(object sender, EventArgs e)
        {
            if (inputChangePSW.Text.Length >= 12)
            {
                inputConfirmPSW.Enabled = true;
            }
        }

        private void inputConfirmPSW_TextChanged(object sender, EventArgs e)
        {
            if (inputConfirmPSW.Text.Length >= 12)
            {
                btnApplyPSW.Enabled = true;
            }
        }

        private void checkBoxTogglePSW_CheckedChanged(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; // Toggle between Visible and Hide password
            checkBoxTogglePSW.Text = isPasswordVisible ? "Hide Password" : "Show Password"; // Update the checkbox text based on the visibility state
            inputChangePSW.PasswordChar = isPasswordVisible ? '\0' : '*'; // Show or hide the password
            inputConfirmPSW.PasswordChar = isPasswordVisible ? '\0' : '*'; // Show or hide the password
        }

        private void btnApplyPSW_Click(object sender, EventArgs e)
        {
            if (inputConfirmPSW.Text != inputChangePSW.Text)
            {
                message.MessageInvalidConfirmationPassword();
                inputConfirmPSW.Clear();
            }
            else
            {
                ValidatePSW();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Initialize EnterKey to confirm input when Enter is pressed.
        /// </summary>
        private void EnterKey()
        {
            // Attach the KeyDown event to the form or password input fields
            this.KeyPreview = true; // Enable form-wide key handling
            this.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true; // Prevent the Enter key from inserting a new line
                    e.SuppressKeyPress = true; // Prevent the "ding" sound when Enter is pressed

                    if (inputChangePSW.Text != inputConfirmPSW.Text)
                    {
                        RepositoryMessageBoxes message = new RepositoryMessageBoxes();
                        message.MessageInvalidConfirmationPassword();
                        inputConfirmPSW.Clear();
                    }
                    else
                    {
                        // Call btnApplyPSW_Click with dummy parameters
                        btnApplyPSW_Click(this, EventArgs.Empty);
                    }
                }
            };
        }

        private void ValidatePSW()
        {
            var currentUser = LoginHandler.CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                var userLines = path.ReadFileContent(path.UserFilePath);
                var loginLines = path.ReadFileContent(path.LoginFilePath);

                // Find userIndex in data_login.csv and data_users.csv
                int userIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, currentUser);
                int loginIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, currentUser);

                string newPassword = inputChangePSW.Text;
                int uppercaseCount = newPassword.Count(char.IsUpper);
                int digitCount = newPassword.Count(char.IsDigit);

                if (newPassword.Length >= lengthPsw && uppercaseCount >= charToUpper && digitCount >= charIsDigi)
                {
                    RepositoryMessageBoxes message = new RepositoryMessageBoxes();
                    DialogResult dr = message.MessageBoxConfirmToSAVEPassword(currentUser);

                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                    UpdateNewPassword(loginLines, userIndex, newPassword);
                    this.Close();
                }
                else
                {
                    RepositoryMessageBoxes message = new RepositoryMessageBoxes();
                    message.MessageInvalidPassword();
                }
            }
        }

        /// <summary>
        /// Updates the login details at the specified index in the CSV data.
        /// Writes the updated details back to data_login.csv.
        /// </summary>
        /// <param name="loginLines">The list of lines from data_users.csv.</param>
        /// <param name="userIndex">The index of the user to update.</param>
        public void UpdateNewPassword(List<string> loginLines, int userIndex, string newPassword)
        {
            var loginDetails = loginLines[userIndex].Split(',');

            // When need to keep current data on indexes
            string currentAlias = loginDetails[0];
            string currentPassword = loginDetails[1];
            string currentAdminBool = loginDetails[2];

            loginLines[userIndex] = $"{currentAlias},{newPassword},{currentAdminBool}";

            // Write updated data back to data_login.csv
            File.WriteAllLines(path.LoginFilePath, loginLines);
            // log event to logEvents.csv
            logEvents.LogEventNewPasswordCreated(currentAlias);
            
            message.MessageChangePasswordSucces(currentAlias);
        }

        private void TxtLabelPSW()
        {
            lblPassword.Text = $"Must contain {lengthPsw} or more chars.\n" +
                               $"Must contain at least {charToUpper} capital letters\n" +
                               $"Must contain at least {charIsDigi} numbers";
        }
    }
}
