﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Diagnostics;
using CRUD_System.Handlers;
using CRUD_System.FileHandlers;
using CRUD_System.Interfaces;
using CRUD_System.Repositories;

namespace CRUD_System
{
    /// <summary>
    /// Provides administrative control functionalities in the CRUD system, enabling user management tasks 
    /// like editing, saving, deleting, and creating users, as well as setting admin permissions and 
    /// generating passwords. Integrates with other components including AdminInterface, 
    /// ProfileManager, FormInteractionHandler, and FilePaths to manage user interactions, data updates, 
    /// and interface updates.
    /// </summary>
    public partial class AdminMainControl : UserControl
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        AdminInterface adminInterface;
        AccountManager accountManager = new AccountManager();
        ProfileManager userProfileManager = new ProfileManager();
        FormInteractionHandler interactionHandler = new FormInteractionHandler();
        UserSearchService search = new UserSearchService();

        // Property to expose the InteractionHandler instance for external access
        public FormInteractionHandler InteractionHandler => interactionHandler;

        RepositoryMessageBoxes message = new RepositoryMessageBoxes();

        bool editMode = false;
        public bool isAdmin = false;
        #endregion PROPERTIES

        #region Constructor
        public AdminMainControl(AdminInterface? adminInterface = null)
        {
            InitializeComponent();

            // Assign the UserInterface field; if no instance is provided, create a new UserInterface instance
            this.adminInterface = adminInterface ?? new AdminInterface(this);

            // Load data_users.csv for display in listbox
            this.adminInterface.LoadDetailsListBox();
        }
        #endregion CONSTRUCTOR

        #region BUTTONS SoC (Seperate of Concerns)
        /// <summary>
        /// Handles the click event to toggle edit mode for the selected user in listBoxUsers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnEditUserDetails_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                // Toggle edit mode
                adminInterface.EditMode = ToggleEditMode();
                adminInterface.InterfaceEditModeAdmin();
            },
             () => message.MessageInvalidNoUserSelected());
        }

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
            //bool isAdmin = bool.TryParse(loginDetails[2], out bool parsedIsAdmin) && parsedIsAdmin;
            bool onlineStatus = bool.TryParse(loginDetails[3], out bool parsedOnlineStatus) && parsedOnlineStatus;

            Debug.WriteLine("ADMINMainControl.btnSaveEditUserDetails_Click");
            Debug.WriteLine($"isAdmin = {isAdmin}");
            Debug.WriteLine($"omlineStatus = {onlineStatus}");

            if (userIndex != -1)
            {
                userProfileManager.UpdateUserDetails(userLines, loginLines, userIndex, loginIndex, txtName.Text, txtSurname.Text, txtAlias.Text, txtAddress.Text, txtZIPCode.Text, txtCity.Text, txtEmail.Text, txtPhonenumber.Text, isAdmin, onlineStatus);
            }
            adminInterface.EditMode = false;
            adminInterface.InterfaceEditModeAdmin();
            adminInterface.ReloadListBoxAdmin(userIndex); // Reload listbox
        }

        /// <summary>
        /// Handles the click event to delete user from data_users.csv and data_login.csv
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                userProfileManager.DeleteUser(txtAlias.Text); // Perform delete action only if a user is selected

                listBoxAdmin.Items.Clear();
                adminInterface.LoadDetailsListBox();
                adminInterface.EmptyTextBoxesAdmin();

                // Toggle edit mode
                adminInterface.EditMode = ToggleEditMode();
                adminInterface.InterfaceEditModeAdmin();
            },
             () => message.MessageInvalidNoUserSelected());
        }

        /// <summary>
        /// Handles the click event to add a new user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            AccountManager accountManager = new AccountManager();
            interactionHandler.Open_CreateForm(this);
            // Reload listbox
            listBoxAdmin.Items.Clear();
            adminInterface.LoadDetailsListBox();
            // Empty Textboxes
            adminInterface.EmptyTextBoxesAdmin();
        }

        /// <summary>
        /// Handles click event to create a new password
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            ProfileManager userProfileManager = new ProfileManager();
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                userProfileManager.GenerateNewPassword(txtAlias.Text, chkIsAdmin.Checked);
            },
             () => message.MessageInvalidNoUserSelected());
        }

        /// <summary>
        /// Handles the event when the 'Is Admin' checkbox state changes.
        /// Marks the current user as an admin when the checkbox is checked.
        /// </summary>
        /// <param name="sender">The source of the event (the CheckBox).</param>
        /// <param name="e">The event data (checkbox change).</param>
        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            isAdmin = chkIsAdmin.Checked;
            Debug.WriteLine($"isAdmin updated to: {isAdmin}");
        }

        /// <summary>
        /// Handles click event to open for for creating new password 
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event data</param>
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            interactionHandler.Open_CreateNewPasswordForm();
        }

        private void btnForceLogOutUser_Click(object sender, EventArgs e)
        {
            AuthenticationService authenticationService = new AuthenticationService();
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                MessageBox.Show("Pushing force logout");
                authenticationService.ForceLogOut(txtAlias.Text);
            },
            () => message.MessageInvalidNoUserSelected());
        }

        /// <summary>
        /// Toggle between editMode and !editMode
        /// </summary>
        /// <returns></returns>
        private bool ToggleEditMode()
        {
            bool modus = editMode = !editMode;

            return modus;
        }

        /// <summary>
        /// Handles the selection change event for the ListBox in the admin interface.
        /// Triggers the appropriate selection handler in AdminInterface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void ListBoxAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            adminInterface.ListBoxAdmin_SelectedIndexChangedHandler();
        }

        /// <summary>
        /// Handles the drawing of items in the ListBox. This method delegates the actual drawing 
        /// process to the <see cref="ListBoxAdmin_DrawItemHandler"/> method in the AdminInterface.
        /// </summary>
        /// <param name="sender">The source of the event, typically the ListBox control.</param>
        /// <param name="e">The event data that contains the drawing information for the item.</param>
        private void ListBoxAdmin_DrawItem(object sender, DrawItemEventArgs e)
        {
            adminInterface.ListBoxAdmin_DrawItemHandler(sender, e);
        }
        #endregion BUTTONS SoC (Seperate of Concerns)

        /// <summary>
        /// Handles the text changed event for the alias search textbox. It dynamically updates the list of users
        /// displayed in the listbox based on the search input. If the input is empty, it loads all users;
        /// otherwise, it filters the users based on the provided alias prefix.
        /// </summary>
        /// <param name="sender">The source of the event (typically the text box control that triggered the event).</param>
        /// <param name="e">The event data, which contains information about the text change event.</param>
        private void txtAliasToSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the alias input by the user in the search box
            string alias = txtAliasToSearch.Text;

            // If the alias is empty, load all users into the listbox
            if (string.IsNullOrEmpty(alias))
            {
                // Load all user details into the listbox (when no search term is entered)
                adminInterface.LoadDetailsListBox();
            }
            else
            {
                // If alias is not empty, search for users by the alias prefix
                var searchResults = new UserSearchService().SearchByAlias(alias);

                // Clear the current items in the listbox to display the search results
                listBoxAdmin.Items.Clear();

                // Add each matched result (user details) to the listbox
                foreach (var result in searchResults)
                {
                    listBoxAdmin.Items.Add(result);
                }
            }
        }
    }
}

