using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Interfaces;
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
    public partial class USERSMainControl : UserControl
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        Repository userRepository = new Repository();
        AdminMainControl adminMainControl = new AdminMainControl();
        ProfileManager profileManager = new ProfileManager();
        InteractionHandler interactionHandler = new InteractionHandler();
        //UserInterface userInterface = new UserInterface();
        MessageBoxes message = new MessageBoxes();

        // Property to expose the InteractionHandler instance for external access
        public InteractionHandler InteractionHandler => interactionHandler;

        bool editMode = false;

        #region Initialize DateTime for logging
        LogEntryActions log = new LogEntryActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public USERSMainControl()
        {
            InitializeComponent();

            //this.userInterface.ListBoxThisUser();
            ListBoxThisUser();
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
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();
            int userIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, txtAlias.Text);
            if (userIndex != -1)
            {
                profileManager.UpdateUserDetails(userLines, userIndex, txtName.Text, txtSurname.Text, txtAlias.Text, txtAddress.Text, txtZIPCode.Text, txtCity.Text, txtEmail.Text, txtPhonenumber.Text);
            }
            editMode = false; // Close editMode
            ReloadListBoxUser(userIndex); // Reload interface
        }

        /// <summary>
        /// Handles the click event to toggle edit mode for the selected user in listBoxUsers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnEditUserDetails_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(
                () =>   {
                            // Toggle editMode
                            editMode = ToggleEditMode();
                            InterfaceEditMode();
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

        #region METHODS MANAGEMENT CONTROL USER

        public void FillTextboxes(string[] userDetailsArray)
        {
            // Initialize the UserDetails object with the array of user details
            UserDetails userDetails = new UserDetails(userDetailsArray);

            // Populate the text fields with the details of the selected user
            txtName.Text = userDetails.Name;
            txtSurname.Text = userDetails.Surname;
            txtAlias.Text = userDetails.Alias;
            txtAddress.Text = userDetails.Address;
            txtZIPCode.Text = userDetails.ZIPCode;
            txtCity.Text = userDetails.City;
            txtEmail.Text = userDetails.Email;
            txtPhonenumber.Text = userDetails.PhoneNumber;
        }
        #endregion METHODS MANAGEMENT CONTROL USER

        #region LISTBOX
        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        private void ListBoxThisUser()
        {
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();

            var currentUser = LoginHandler.CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                var userIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, currentUser);
                var userDetailsArray = userLines[userIndex].Split(',');

                UserDetails userDetails = new UserDetails(userDetailsArray);
                string listItem = $"{userDetails.Name} {userDetails.Surname} ({userDetails.Alias}) | {userDetails.Email} | {userDetails.PhoneNumber}";

                listBoxUser.Items.Add(listItem);
            }
            else
            {
                MessageBox.Show($"No user details found");
            }

            listBoxUser.SelectedIndex = 0; // auto select user to fill textboxes
        }


        public void ListBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected user from the ListBox; ignore clicks on empty line in listBox
            if (listBoxUser.SelectedItem is string selectedUserString && !string.IsNullOrEmpty(selectedUserString))
            {
                interactionHandler.UserSelected = true; // Sync selection state with ControlsHandler
                // Extract the alias from the selected text (in the format: "Name Surname (Alias)")
                string selectedAlias = selectedUserString.Split('(', ')')[1]; // Extract the alias between parentheses

                // Read user details
                var userDetailsArray = File.ReadAllLines(path.UserFilePath)
                                      .Skip(2)
                                      .Select(line => line.Split(','))
                                      .FirstOrDefault(details => details[2] == selectedAlias);

                if (userDetailsArray != null)
                {
                    FillTextboxes(userDetailsArray);
                }
            }
        }

        /// <summary>
        /// Reloads the user interface after saving changes.
        /// </summary>
        /// <param name="userIndex">The index of the updated user.</param>
        public void ReloadListBoxUser(int userIndex)
        {
            if (userIndex >= 0 && userIndex < listBoxUser.Items.Count)
            {
                this.Refresh();
            }

            // Clear and reload listbox
            listBoxUser.Items.Clear();
            //userInterface.ListBoxThisUser();
            ListBoxThisUser();

            // Reset editMode to false after saving and reload interface
            editMode = false;
            InterfaceEditMode();
        }
        #endregion LISTBOX

        #region INTERFACE
        public void InterfaceEditMode()
        {
            // Use toggle to keep 1 button for Edit and Cancel
            btnEditUserDetails.Text = editMode ? btnEditUserDetails.Text = "Cancel" : btnEditUserDetails.Text = "Edit User";

            // Indication Edit mode is Enabled in Controlfield: color.Orange
            this.BackColor = editMode ? Color.Orange : SystemColors.ActiveCaption;

            // Manage btnUpdateUser
            btnSaveEditUserDetails.Visible = editMode ? true : false;
            btnSaveEditUserDetails.BackColor = Color.LightGreen;

            // Manage TextBoxes
            txtName.Enabled = editMode ? true : false;
            txtSurname.Enabled = editMode ? true : false;
            txtAddress.Enabled = editMode ? true : false;
            txtZIPCode.Enabled = editMode ? true : false;
            txtCity.Enabled = editMode ? true : false;
            txtEmail.Enabled = editMode ? true : false;
            txtPhonenumber.Enabled = editMode ? true : false;
            txtPassword.Enabled = editMode ? true : false;

            // Manage status visible and enable Buttons and CheckBox
            btnChangePassword.Visible = editMode ? false : true;
            btnChangePassword.Enabled = editMode ? false : true;

            // Manage status ListBox
            listBoxUser.Enabled = editMode ? false : true;
        }
        #endregion INTERFACE
    }
}
