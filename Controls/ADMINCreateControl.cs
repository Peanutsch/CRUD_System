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
        private readonly ADMINMainForm mainFormADMIN = new ADMINMainForm();
        private readonly RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        private readonly AccountManager accountManager = new AccountManager();
        private readonly RepositoryLogEvents logEvents = new RepositoryLogEvents();
        private readonly ProfileManager profileManager = new ProfileManager();
        private readonly FormInteractionHandler interactionHandler = new FormInteractionHandler();

        bool isAdmin = false;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public AdminCreateControl()
        {
            InitializeComponent();
        }
        #endregion CONSTRUCTOR

        #region BUTTONS
        /// <summary>
        /// Initializes the components of the AdminCreateControl class.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            return;
            //interactionHandler.CloseCreateForm(this.ParentForm);
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
        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            profileManager.SaveNewUser(txtName.Text, txtSurname.Text, 
                                       txtAddress.Text, txtZIPCode.Text,
                                       txtCity.Text, txtEmail.Text,
                                       txtPhonenumber.Text, isAdmin);

            // Close CreateFormADMIN, return to MainFormADMIN
            interactionHandler.CloseCreateForm(this.ParentForm);
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
            if (txtName.Text.Length >= 1 && txtSurname.Text.Length >= 1)
            {
                // Generate and display the alias
                string displayAlias = accountManager.CreateTXTAlias(txtName.Text, txtSurname.Text);
                txtAlias.Text = displayAlias;
            }
            else
            {
                // Clear the alias and show placeholder text
                txtAlias.Clear();
                txtAlias.PlaceholderText = "Alias";
            }
        }
        #endregion ALIAS TEXTBOX HANDLER
    }
}
