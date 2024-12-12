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

        #region BUTTONS
        /// <summary>
        /// Initializes the components of the AdminCreateControl class.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            interactionHandler.Close_CreateForm(this.ParentForm);

            // Reload Cache
            DataCache cache = new DataCache();
            cache.LoadDecryptedData();
        }

        /// <summary>
        /// Handles the click event to cancel the user creation process and close the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            isAdmin = !isAdmin; // Toggle between true and false
        }

        /// <summary>
        /// Handles the click event to save the new user's details to the database. 
        /// It saves the data and then closes the user creation form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            profileManager.SaveNewUser(txtName.Text.Trim(), txtSurname.Text.Trim(), 
                                       txtAddress.Text.Trim(), txtZIPCode.Text.Trim(),
                                       txtCity.Text.Trim(), txtEmail.Text.Trim(),
                                       txtPhonenumber.Text.Trim(), isAdmin);

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                Debug.WriteLine("Details are not complete. Name, Surname and Email are mandatory");
                message.MessageDetailsNotComplete();
                return;
            }

            // Close CreateFormADMIN, return to MainFormADMIN
            interactionHandler.Close_CreateForm(this.ParentForm);
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
        /// Handles the KeyPress event for the txtPhonenumber textbox.
        /// Allows only numeric digits, the '+' and '-' characters, and the Backspace key.
        /// Suppresses any other key inputs to ensure only valid phone number characters are entered.
        /// </summary>
        /// <param name="sender">The source of the event, typically the TxtPhonenumber textbox.</param>
        /// <param name="e">The KeyPressEventArgs containing the event data.</param>
        public void TxtPhonenumber_KeyDown(object sender, KeyEventArgs e)
        {
            // Allow only digits, the '+' and '-' characters, Backspace, and Ctrl+V, Ctrl+C
            if (!char.IsDigit((char)e.KeyCode)                              // Check if the key is a digit (0-9)
                && e.KeyCode != Keys.Add                                    // Allow the '+' character
                && e.KeyCode != Keys.Subtract                               // Allow the '-' character
                && e.KeyCode != Keys.Back                                   // Allow Backspace
                //&& e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9   // Allow Numpad
                && !(e.Control && e.KeyCode == Keys.V)                      // Allow Ctrl+V for paste
                && !(e.Control && e.KeyCode == Keys.C))                     // Allow Ctrl+C for copy
            {
                e.SuppressKeyPress = true; // Prevent the key from being processed further
            }
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
                && !(e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)) // Block NumPad numbers
            {
                e.SuppressKeyPress = true; // Suppress invalid keypress
            }
        }

        #endregion ALIAS TEXTBOX HANDLER
    }
}
