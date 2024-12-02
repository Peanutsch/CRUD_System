using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUD_System.Interfaces
{
    public class UserInterface
    {
        #region PROPERTIES
        public bool EditMode
        {
            get; set;
        }

        FilePaths path = new FilePaths();

        readonly AccountManager repository = new AccountManager();
        private readonly DataCache cache = new DataCache();
        private readonly UserMainControl userControl;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public UserInterface(UserMainControl? userControl = null)
        {
            this.userControl = userControl ?? new UserMainControl();
        }
        #endregion CONSTRUCTOR

        #region LISTBOX USER
        /// <summary>
        /// Loads the current user's details into the list box using DataCache.
        /// </summary>
        public void LoadDetailsListBoxThisUser()
        {
            // Check if the cache is empty, and reload data if necessary.
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            // Convert cached user and login data arrays into lists of strings, skipping the header row
            var userLines = cache.CachedUserData
                                 .Select(arr => string.Join(",", arr))
                                 .ToList();
            var loginLines = cache.CachedLoginData
                                  .Select(arr => string.Join(",", arr))
                                  .ToList();

            // Retrieve the currently logged-in user
            var currentUser = AuthenticationService.CurrentUser;

            if (string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine("No user is currently logged in.");
                return;
            }

            // Find the index of the current user in the cached user data (userLines has already skipped the header)
            var userIndex = repository.FindUserIndexByAlias(userLines, loginLines, currentUser!);
            if (userIndex < 0)
            {
                Debug.WriteLine($"LoadDetailsListBoxThisUser: User with alias '{currentUser}' not found in cache.");
                return;
            }

            // Retrieve the user's details from the cache, without skipping any rows in CachedUserData
            var userDetailsArray = cache.CachedUserData[userIndex];  // No Skip(1) here since we're using the correct index
            string name = userDetailsArray[0];          // First name
            string surname = userDetailsArray[1];      // Last name
            string alias = userDetailsArray[2];        // Alias (username)
            string email = userDetailsArray[6];        // Email address
            string phonenumber = userDetailsArray[7];  // Phone number
            string isOnline = userDetailsArray.Length > 8 && userDetailsArray[8] == "True" ? "| [ONLINE]" : string.Empty; // Online status

            // Construct the item string to display in the list box
            string listItem = $"{name} {surname} ({alias}) | {email} | {phonenumber} {isOnline}";

            // Clear existing items in the list box and add the current user's details
            userControl.listBoxUser.Items.Clear();
            userControl.listBoxUser.Items.Add(listItem);

            // Automatically select the first (and only) item in the list box
            userControl.listBoxUser.SelectedIndex = 0;

            // Fill the textboxes with the user's details
            FillTextboxes(userDetailsArray);
        }

        /// <summary>
        /// Reloads the user interface after saving changes.
        /// </summary>
        /// <param name="userIndex">The index of the updated user.</param>
        public void ReloadListBoxUser(int userIndex)
        {
            if (userIndex >= 0 && userIndex < userControl.listBoxUser.Items.Count)
            {
                userControl.Refresh();
            }

            // Clear and reload listbox
            userControl.listBoxUser.Items.Clear();
            LoadDetailsListBoxThisUser();

            // Reset editMode to false after saving and reload interface
            EditMode = false;
            InterfaceEditModeUser();
        }

        /// <summary>
        /// Handles the event when a user is selected in the ListBox. 
        /// Retrieves the alias from the selected item and fetches user details from the user file.
        /// If user details are found, populates the textboxes with the retrieved information.
        /// </summary>
        public void ListBoxUser_SelectedIndexChangedHandler()
        {
            // Get the selected user from the ListBox; ignore clicks on empty line in listBox
            if (userControl.listBoxUser.SelectedItem is string selectedUserString && !string.IsNullOrEmpty(selectedUserString))
            {
                userControl.InteractionHandler.UserSelected = true; // Sync selection state with ControlsHandler
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
        #endregion LISTBOX USER

        #region INTERFACE USERS
        /// <summary>
        /// Toggles the interface elements between edit mode and view mode for user details.
        /// Adjusts the button text, background color, visibility, and enablement of controls 
        /// based on the current edit mode status.
        /// </summary>
        public void InterfaceEditModeUser()
        {
            // Toggle Edit and Cancel button text
            userControl.btnEditUserDetails.Text = EditMode ? "Exit" : "Edit User";

            // Set background color based on EditMode
            userControl.BackColor = EditMode ? Color.Orange : SystemColors.ActiveCaption;

            // Manage visibility and enablement of buttons and controls
            userControl.btnSaveEditUserDetails.Visible = EditMode;
            userControl.btnSaveEditUserDetails.BackColor = Color.LightGreen;
            
            userControl.btnChangePassword.Visible = EditMode;

            // Array of text fields to enable or disable in EditMode
            var textFields = new[]
            {
                userControl.txtName,
                userControl.txtSurname,
                userControl.txtAddress,
                userControl.txtZIPCode,
                userControl.txtCity,
                userControl.txtEmail,
                userControl.txtPhonenumber
            };

            foreach (var field in textFields)
            {
                field.Enabled = EditMode;
            }

            // Enable or disable ListBox based on EditMode
            userControl.listBoxUser.Enabled = !EditMode;
        }

        public void StatusIndicator(string status)
        {
            switch (status)
            {
                case "Online":
                    userControl.txtStatusIndicator.BackColor = Color.Blue;
                    break;
                case "Active":
                    userControl.txtStatusIndicator.BackColor = Color.Green;
                    break;
                case "Away":
                    userControl.txtStatusIndicator.BackColor = Color.Orange;
                    break;
                case "Break":
                    userControl.txtStatusIndicator.BackColor = Color.Yellow;
                    break;
                default:
                    // Handle an invalid status
                    userControl.txtStatusIndicator.BackColor = SystemColors.ActiveCaption;
                    break;
            }
        }
        #endregion INTERFACE USERS

        #region TEXTBOXES
        /// <summary>
        /// Populates the user interface text fields with details from the specified user details array.
        /// Initializes a UserDetails object from the array and assigns values to the respective text fields.
        /// </summary>
        public void FillTextboxes(string[] userDetailsArray)
        {
            // Populate the text fields with the details of the selected user
            userControl.txtName.Text = userDetailsArray[0];
            userControl.txtSurname.Text = userDetailsArray[1];
            userControl.txtAlias.Text = userDetailsArray[2];
            userControl.txtAddress.Text = userDetailsArray[3];
            userControl.txtZIPCode.Text = userDetailsArray[4];
            userControl.txtCity.Text = userDetailsArray[5];
            userControl.txtEmail.Text = userDetailsArray[6];
            userControl.txtPhonenumber.Text = userDetailsArray[7];
        }
        #endregion TEXTBOXES
    }
}
