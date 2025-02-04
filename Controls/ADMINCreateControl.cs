using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Interfaces;
using CRUD_System.Repositories;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

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

        /// <summary>
        /// Handles the save action for a new user.
        /// Ensures proper capitalization of the name, city and zipcode before saving.
        /// Closes the user creation form after successful save.
        /// </summary>
        private void btnSaveNewAccount_Click(object sender, EventArgs e)
        {
            string isName = char.ToUpper(txtName.Text.Trim()[0]) + txtName.Text.Trim().Substring(1);
            string isCity = char.ToUpper(txtCity.Text.Trim()[0]) + txtCity.Text.Trim().Substring(1);

            profileManager.SaveNewUser(isName, txtSurname.Text.Trim(),
                                       txtAddress.Text.Trim(), txtZIPCode.Text.ToUpper().Trim(),
                                       isCity, txtEmail.Text.Trim(),
                                       txtPhonenumber.Text.Trim(), isAdmin);

            interactionHandler.Close_CreateForm(this.ParentForm);
        }

        /// <summary>
        /// Enables or disables the save button based on user input validation.
        /// </summary>
        private void txtFields_TextChanged(object sender, EventArgs e)
        {
            btnSaveNewAccount.Enabled = ValidateUserInput();
        }

        /// <summary>
        /// Validates user input fields to ensure required fields are filled.
        /// Required fields: Name, Surname, and Email.
        /// </summary>
        /// <returns>True if input is valid; otherwise, false.</returns>
        private bool ValidateUserInput()
        {
            bool isValid = !string.IsNullOrEmpty(txtName.Text.Trim()) &&
                           !string.IsNullOrEmpty(txtSurname.Text.Trim()) &&
                           !string.IsNullOrEmpty(txtEmail.Text.Trim());

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
        /// Allows numeric digits, '+', '-', Backspace, Spacebar, and clipboard shortcuts (Ctrl+C, Ctrl+V).
        /// Suppresses any other invalid key inputs.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The KeyEventArgs containing the event data.</param>
        public void TxtPhonenumber_KeyDown(object sender, KeyEventArgs e)
        {
            // Allow valid keys: digits (main and numpad), Backspace, Space, '+', '-', and clipboard shortcuts
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || // Digits (main keyboard)
                (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||  // Digits (numpad)
                e.KeyCode == Keys.Back             ||                        // Backspace
                e.KeyCode == Keys.Space            ||                        // Spacebar
                e.KeyCode == Keys.Oemplus          ||                        // Plus Numpad
                e.KeyCode == Keys.Add              ||                        // Plus
                e.KeyCode == Keys.OemMinus         ||                        // Minus Numpad
                e.KeyCode == Keys.Subtract         ||                        // Minus
                e.KeyCode == Keys.Home             ||                        // Home
                e.KeyCode == Keys.ShiftKey         ||                        // Shift
                e.KeyCode == Keys.ControlKey       ||                        // Control key
                (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V))) // Clipboard shortcuts
            {
                return;
            }

            // Suppress all other keys
            e.SuppressKeyPress = true;
        }

        /// <summary>
        /// Handles the KeyDown event for the txtName textbox.
        /// Allows only letters, Backspace, arrow keys, and Ctrl/Shift key combinations.
        /// Suppresses any other key inputs, including numeric keys from NumPad, to prevent invalid characters from being entered.
        /// </summary>
        public void TxtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsLetter((char)e.KeyCode)                                 // Block non-letter keys
                && e.KeyCode != Keys.Back                                       // Allow Backspace
                && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right            // Allow arrow keys
                && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down
                && e.KeyCode != Keys.Space                      
                && e.KeyCode != Keys.Control
                && e.KeyCode != Keys.Home
                && e.KeyCode != Keys.End
                && e.KeyCode == Keys.Subtract
                && !(e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))   // Block NumPad numbers
            {
                e.SuppressKeyPress = true; // Suppress invalid keypress
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the txtSurname textbox.
        /// Allows only letters, Backspace, arrow keys, and Ctrl/Shift key combinations.
        /// Suppresses any other key inputs, including numeric keys from NumPad, to prevent invalid characters from being entered.
        /// </summary>
        public void TxtSurname_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsLetter((char)e.KeyCode)                      // Block non-letter keys
                && e.KeyCode != Keys.Back                            // Allow Backspace
                && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right // Allow arrow keys
                && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down
                && e.KeyCode != Keys.Space
                && e.KeyCode != Keys.Control
                && e.KeyCode != Keys.Home
                && e.KeyCode != Keys.End
                && e.KeyCode == Keys.Subtract
                && !(e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)) // Block NumPad numbers
            {
                e.SuppressKeyPress = true; // Suppress invalid keypress
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the txtCity textbox.
        /// Allows only letters, Backspace, arrow keys, and Ctrl/Shift key combinations.
        /// Suppresses any other key inputs, including numeric keys from NumPad, to prevent invalid characters from being entered.
        /// </summary>
        public void TxtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsLetter((char)e.KeyCode)                      // Block non-letter keys
                && e.KeyCode != Keys.Back                            // Allow Backspace
                && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right // Allow arrow keys
                && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down
                && e.KeyCode != Keys.Space
                && e.KeyCode != Keys.Control
                && e.KeyCode != Keys.Home
                && e.KeyCode != Keys.End
                && !(e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)) // Block NumPad numbers
            {
                e.SuppressKeyPress = true; // Suppress invalid keypress
            }
        }
        #endregion ALIAS TEXTBOX HANDLER
    }
}
