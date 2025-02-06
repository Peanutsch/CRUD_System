using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Interfaces;
using CRUD_System.Repositories;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace CRUD_System
{
    /// <summary>
    /// Provides functionality for creating new users in the CRUD system. It includes actions such as saving 
    /// new user details, toggling admin status, generating user aliases based on the name and surname, and 
    /// canceling the creation process. It integrates with ProfileManager for user data management, 
    /// FormInteractionHandler for form control, and AccountManager for alias generation.
    /// </summary>
    public partial class AdminCreateControl : UserControl
    {
        #region PROPERTIES
        private readonly AccountManager accountManager = new AccountManager();
        private readonly ProfileManager profileManager = new ProfileManager();
        private readonly FormInteractionHandler interactionHandler = new FormInteractionHandler();
        private readonly RepositoryMessageBoxes message = new RepositoryMessageBoxes();

        bool isAdmin = false;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public AdminCreateControl()
        {
            InitializeComponent();
            InitializeEvents();
            SetChkIsAdmin();
        }
        #endregion CONSTRUCTOR

        // Only when CurrentUserIsTheOne, then chkIsAdmin visible and enabled
        public void SetChkIsAdmin()
        { 
            if (AuthenticationService.CurrentUserIsTheOne)
            {
                chkIsAdmin.Visible = true;
                chkIsAdmin.Enabled = true;
            }
        }

        /// <summary>
        /// Initializes event handlers for text fields.
        /// </summary>
        public void InitializeEvents()
        {
            txtName.TextChanged += txtFields_TextChanged!;
            txtSurname.TextChanged += txtFields_TextChanged!;
            txtEmail.TextChanged += txtFields_TextChanged!;
        }

        #region BUTTONS
        /// <summary>
        /// Cancels the user creation process and closes the form.
        /// Also reloads the cached user data to reflect any changes.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = message.MessageConfirmCancel();

            if (dr == DialogResult.Yes)
            {
                interactionHandler.Close_CreateForm(this.ParentForm);

                // Reload Cache
                DataCache cache = new DataCache();
                cache.LoadDecryptedData();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Toggles the admin status when the checkbox is checked or unchecked.
        /// </summary>
        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            isAdmin = !isAdmin; // Toggle between true and false
        }

        #region SAVE NEW ACCOUNT
        /// <summary>
        /// Handles the save action for a new user.
        /// Ensures proper capitalization of the name, city, and address before saving.
        /// Converts the ZIP code to uppercase.
        /// Updates the data cache and closes the user creation form after a successful save.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnSaveNewAccount_Click(object sender, EventArgs e)
        {
            // First letter uppercase
            string isName = FormatText(txtName.Text);
            string isSurname = FormatText(txtSurname.Text);
            string isAddress = FormatText(txtAddress.Text);
            string isCity = FormatText(txtCity.Text);

            // All letters uppercase
            string zipCode = FormatText(txtZIPCode.Text, true);

            // Validate format emailaddress
            string email = GetValidEmail(txtEmail.Text);

            string phoneNumber = string.IsNullOrWhiteSpace(txtPhonenumber.Text) ? string.Empty : txtPhonenumber.Text.Trim();

            // Process data to create new user account
            profileManager.SaveNewUser(isName, isSurname,
                                       isAddress, zipCode,
                                       isCity, email,
                                       phoneNumber, isAdmin);

            // Update the DataCache with the latest data
            DataCache cache = new DataCache();
            cache.LoadDecryptedData();

            // Close Form
            interactionHandler.Close_CreateForm(this.ParentForm);
        }

        /// <summary>
        /// Formats a given string by either capitalizing only the first letter 
        /// or converting the entire string to uppercase.
        /// </summary>
        /// <param name="input">The input string to be formatted.</param>
        /// <param name="fullUpper">If true, converts the entire string to uppercase; otherwise, only the first letter is capitalized.</param>
        /// <returns>The formatted string based on the chosen capitalization style, or an empty string if the input is null or whitespace.</returns>
        private string FormatText(string input, bool fullUpper = false)
        {
            // Return an empty string if input is null, empty, or contains only whitespace
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Trim whitespace from the input
            input = input.Trim();

            // Return the fully uppercase version if fullUpper is true, otherwise capitalize only the first letter
            return fullUpper ? input.ToUpper() : char.ToUpper(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// Validates whether an email address is correctly formatted. 
        /// If the email is invalid, the method returns an empty string.
        /// </summary>
        /// <param name="input">The email input as a string.</param>
        /// <returns>A properly formatted email address or an empty string if invalid.</returns>
        private string GetValidEmail(string input)
        {
            // Trim input and check if it's empty or contains only whitespace
            string email = string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim();

            /*
             * Regex for validating a more complete email format: example@domain.com
             * 
             * ^[a-zA-Z0-9._%+-]+: Matches the local part of the email (before the '@') which can include letters, digits, and certain special characters
             * @: Ensures there is an '@' symbol separating the local part and domain
             * [a-zA-Z0-9.-]+: Matches the domain name (after the '@') which can contain letters, digits, dots, and hyphens
             * \.: Ensures there is a dot between the domain name and the top-level domain (TLD)
             * [a-zA-Z]{2,}$: Ensures the top-level domain (TLD) has at least two letters (e.g., .nl .com, .org, .net)
            */
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // If the email isn't empty and matches the pattern, validate it
            if (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, emailPattern))
            {
                try
                {
                    // Attempt to create a MailAddress object to validate the email format
                    var addr = new MailAddress(email);
                }
                catch
                {
                    // If an exception occurs, the email is invalid
                    email = string.Empty;
                }
            }
            else
            {
                // If the email doesn't match the pattern, reset to empty
                email = string.Empty;
            }

            // Return the validated email or an empty string if invalid
            return email;
        }
        #endregion SAVE NEW ACCOUNT

        /// <summary>
        /// Enables or disables the save button based on user input validation.
        /// </summary>
        private void txtFields_TextChanged(object sender, EventArgs e)
        {
            btnSaveNewAccount.Enabled = ValidateUserInput();
        }

        /// <summary>
        /// Validates user input fields to ensure required fields are filled and format email address is valid
        /// Required fields: Name, Surname, and Email.
        /// </summary>
        /// <returns>True if input is valid; otherwise, false.</returns>
        private bool ValidateUserInput()
        {
            bool isValid = !string.IsNullOrEmpty(txtName.Text.Trim()) &&
                           !string.IsNullOrEmpty(txtSurname.Text.Trim()) &&
                           !string.IsNullOrEmpty(txtEmail.Text.Trim()) &&
                           !string.IsNullOrEmpty(GetValidEmail(txtEmail.Text.Trim()));

            return isValid;
        }
        #endregion BUTTONS

        #region ALIAS TEXTBOX HANDLER
        /// <summary>
        /// Handles the event when the alias text is changed. It generates and displays an alias 
        /// based on the first name and surname if both have at least one character.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void TxtAlias_TextChanged(object sender, EventArgs e)
        {
            // Check if both txtName and txtSurname have at least 2 characters
            if (txtName.Text.Trim().Length >= 1 && txtSurname.Text.Trim().Length >= 1)
            {
                // Generate and display the alias
                string displayAlias = accountManager.CreateTXTAlias(txtName.Text.Trim(), txtSurname.Text.Trim());
                txtAlias.Text = displayAlias;
            }
            else
            {
                // Clear the alias and show placeholder text
                txtAlias.Clear();
                txtAlias.PlaceholderText = "Alias";
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the txtPhonenumber textbox.
        /// Allows numeric digits, '+', '-', Backspace, Spacebar, Arrow Keys left and right and clipboard shortcuts (Ctrl+C, Ctrl+V).
        /// Suppresses any other invalid key inputs.
        /// </summary>
        public void TxtPhonenumber_KeyDown(object sender, KeyEventArgs e)
        {
            // List of allowed keys
            HashSet<Keys> allowedKeys = new HashSet<Keys>
            {
                Keys.Back, Keys.Space, Keys.Oemplus, Keys.Add, Keys.OemMinus, Keys.Subtract, Keys.Left, Keys.Right,
                Keys.Home, Keys.Home, Keys.ShiftKey, Keys.ControlKey,
                Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9,
                Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9,
                Keys.C, Keys.V // Clipboard shortcuts
            };

            // If the pressed key is not in the allowed keys, suppress it
            if (!allowedKeys.Contains(e.KeyCode) && !(e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V)))
            {
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the txtName textbox.
        /// Allows only letters, Backspace, arrow keys, and Ctrl/Shift key combinations.
        /// Suppresses any other key inputs, including numeric keys from NumPad, to prevent invalid characters from being entered.
        /// </summary>
        public void TxtName_KeyDown(object sender, KeyEventArgs e)
        {
            // List of allowed keys for the Name field
            HashSet<Keys> allowedKeys = new HashSet<Keys>
            {
                Keys.Back, Keys.Left, Keys.Right, Keys.Space, Keys.Control, Keys.Home, Keys.End,
                Keys.OemMinus, Keys.Subtract,
                Keys.C, Keys.V
            };

            // Allow only alphabetic characters and allowed keys
            if (!char.IsLetter((char)e.KeyCode) && !allowedKeys.Contains(e.KeyCode))
            {
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the txtSurname textbox.
        /// Allows only letters, Backspace, arrow keys, and Ctrl/Shift key combinations.
        /// Suppresses any other key inputs, including numeric keys from NumPad, to prevent invalid characters from being entered.
        /// </summary>
        public void TxtSurname_KeyDown(object sender, KeyEventArgs e)
        {
            // List of allowed keys for the Surname field
            HashSet<Keys> allowedKeys = new HashSet<Keys>
            {
                Keys.Back, Keys.Left, Keys.Right, Keys.Space, Keys.Control, Keys.Home, Keys.End,
                Keys.OemMinus, Keys.Subtract,
                Keys.C, Keys.V
            };

            // Allow only alphabetic characters and allowed keys
            if (!char.IsLetter((char)e.KeyCode) && !allowedKeys.Contains(e.KeyCode))
            {
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the txtCity textbox.
        /// Allows only letters, Backspace, arrow keys, and Ctrl/Shift key combinations.
        /// Suppresses any other key inputs, including numeric keys from NumPad, to prevent invalid characters from being entered.
        /// </summary>
        public void TxtCity_KeyDown(object sender, KeyEventArgs e)
        {
            // List of allowed keys for the City field
            HashSet<Keys> allowedKeys = new HashSet<Keys>
            {
                Keys.Back, Keys.Left, Keys.Right, Keys.Space, Keys.Control, Keys.Home, Keys.End,
                Keys.OemMinus, Keys.Subtract,
                Keys.C, Keys.V
            };

            // Allow only alphabetic characters and allowed keys
            if (!char.IsLetter((char)e.KeyCode) && !allowedKeys.Contains(e.KeyCode))
            {
                e.SuppressKeyPress = true;
            }
        }
        #endregion ALIAS TEXTBOX HANDLER
    }
}
