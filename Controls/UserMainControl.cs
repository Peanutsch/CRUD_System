using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Interfaces;
using CRUD_System.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace CRUD_System
{
    /// <summary>
    /// Manages user-related interactions within the main user control panel of the CRUD system.
    /// Provides functionality to edit user details, toggle edit mode, change passwords, and handle 
    /// ListBox selection events. Integrates with other components like UserInterface, AccountManager,
    /// ProfileManager, and FormInteractionHandler to manage user data and interface behavior.
    /// </summary>
    public partial class UserMainControl : UserControl
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        UserInterface userInterface;
        AccountManager accountManager = new AccountManager();
        ProfileManager profileManager = new ProfileManager();
        FormInteractionHandler interactionHandler = new FormInteractionHandler();

        RepositoryMessageBoxes message = new RepositoryMessageBoxes();

        // Property to expose the InteractionHandler instance for external access
        public FormInteractionHandler InteractionHandler => interactionHandler;

        bool editMode = false;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public UserMainControl(UserInterface? userInterface = null)
        {
            InitializeComponent();
            this.userInterface = userInterface ?? new UserInterface(this);

            this.userInterface.LoadDetailsListBoxThisUser();
        }
        #endregion CONSTRUCTOR

        #region BUTTONS
        /// <summary>
        /// Handles the click event to save the edited user details.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnSaveEditUserDetails_Click(object sender, EventArgs e)
        {
            // Read lines from data_users.csv and data_login.csv
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            int userIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, txtAlias.Text);
            int loginIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, txtAlias.Text);

            var loginDetails = loginLines[loginIndex].Split(",");

            // Parse the admin status and online status as bools
            bool isAdmin = bool.TryParse(loginDetails[2], out bool parsedIsAdmin) && parsedIsAdmin;
            bool onlineStatus = bool.TryParse(loginDetails[3], out bool parsedOnlineStatus) && parsedOnlineStatus;

            if (userIndex != -1)
            {
                profileManager.UpdateUserDetails(userLines, loginLines, userIndex, loginIndex, txtName.Text, txtSurname.Text, txtAlias.Text, txtAddress.Text, txtZIPCode.Text, txtCity.Text, txtEmail.Text, txtPhonenumber.Text, isAdmin, onlineStatus);
            }
            editMode = false; // Close editMode
            userInterface.ReloadListBoxUser(userIndex); // Reload interface
        }

        /// <summary>
        /// Handles the click event to toggle edit mode for the selected user in listBoxUsers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnEditUserDetails_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(
                () =>
                {
                    // Toggle editMode
                    userInterface.EditMode = ToggleEditMode();
                    userInterface.InterfaceEditModeUser();
                },
                () => message.MessageInvalidNoUserSelected());
        }

        private void ChangePassword_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                interactionHandler.Open_CreateNewPasswordForm();
            });

        }

        private bool ToggleEditMode()
        {
            bool modus = editMode = !editMode;

            return modus;
        }
        #endregion BUTTONS

        public void ListBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            userInterface.ListBoxUser_SelectedIndexChangedHandler();
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
