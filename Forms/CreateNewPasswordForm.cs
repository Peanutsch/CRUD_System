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

            TxtLabelPassword();
            EnterKey();
        }
        #endregion CONSTRUCTOR
        /// <summary>
        /// Event handler for the form load event. Sets focus to the confirm password input field.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Form_Load(object sender, EventArgs e)
        {
            this.ActiveControl = inputConfirmPSW;
        }

        /// <summary>
        /// Enables the confirm password input field if the change password field has 12 or more characters.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void inputChangePassword_TextChanged(object sender, EventArgs e)
        {
            if (inputChangePSW.Text.Length >= 12)
            {
                inputConfirmPSW.Enabled = true;
            }
        }

        /// <summary>
        /// Enables the Apply Password button if the confirm password field has 12 or more characters.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void inputConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (inputConfirmPSW.Text.Length >= 12)
            {
                btnApplyPSW.Enabled = true;
            }
        }

        /// <summary>
        /// Toggles the visibility of the password fields based on the checkbox state.
        /// Updates the checkbox text and sets password visibility accordingly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void checkBoxTogglePassword_CheckedChanged(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; // Toggle between Visible and Hide password
            checkBoxTogglePSW.Text = isPasswordVisible ? "Hide Password" : "Show Password"; // Update the checkbox text based on the visibility state
            inputChangePSW.PasswordChar = isPasswordVisible ? '\0' : '*'; // Show or hide the password
            inputConfirmPSW.PasswordChar = isPasswordVisible ? '\0' : '*'; // Show or hide the password
        }

        /// <summary>
        /// Validates that the password and confirm password fields match. If they do, proceeds to validate the new password.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnApplyPassword_Click(object sender, EventArgs e)
        {
            if (inputConfirmPSW.Text != inputChangePSW.Text)
            {
                message.MessageInvalidConfirmationPassword();
                inputConfirmPSW.Clear();
            }
            else
            {
                ProcessNewPassword();
            }
        }

        /// <summary>
        /// Closes the form when the cancel button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Sets up the Enter key to act as a confirmation trigger for the password inputs,
        /// calling the apply button's click event if passwords match.
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
                        // Call btnApplyPassword_Click with dummy parameters
                        btnApplyPassword_Click(this, EventArgs.Empty);
                    }
                }
            };
        }

        /// <summary>
        /// Validates the new password based on length, uppercase letter, and digit requirements.
        /// If valid, prompts the user to save the password and updates the password in data files.
        /// </summary>
        private void ProcessNewPassword()
        {
            var currentUser = AuthenticationService.CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                // Read lines from data_users.csv and data_login.csv
                (var userLines, var loginLines) = path.ReadUserAndLoginData();

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
        /// Updates the specified user's password in the login data, saves it to the CSV file, 
        /// logs the event, and displays a success message.
        /// </summary>
        /// <param name="loginLines">The list of lines from data_login.csv.</param>
        /// <param name="userIndex">The index of the user to update.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        public void UpdateNewPassword(List<string> loginLines, int userIndex, string newPassword)
        {
            var loginDetails = loginLines[userIndex].Split(',');

            // When need to keep current data on indexes
            string currentAlias = loginDetails[0];
            string currentAdminBool = loginDetails[2];
            string currentOnlineStatus = loginDetails[3];

            loginLines[userIndex] = $"{currentAlias},{newPassword},{currentAdminBool},{currentOnlineStatus}";

            // Write updated data back to data_login.csv
            File.WriteAllLines(path.LoginFilePath, loginLines);
            // log event to logEvents.csv
            logEvents.LogEventNewPasswordCreated(currentAlias);
            
            message.MessageChangePasswordSucces(currentAlias);
        }

        private void TxtLabelPassword()
        {
            lblPassword.Text = $"Must contain {lengthPsw} or more chars.\n" +
                               $"Must contain at least {charToUpper} capital letters\n" +
                               $"Must contain at least {charIsDigi} numbers";
        }
    }
}
