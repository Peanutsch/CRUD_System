using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Models;
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
    /// <summary>
    /// Class for handling setup/layout Admin interface
    /// </summary>
    public class AdminInterface
    {
        #region PROPERTIES
        public bool EditMode { get; set; }
        private List<string[]> CachedUserData => cache.CachedUserData;
        private List<string[]> CachedLoginData => cache.CachedLoginData;

        readonly DataCache cache = new DataCache();
        private readonly AdminMainControl adminControl;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public AdminInterface(AdminMainControl? adminControl = null)
        {
            this.adminControl = adminControl ?? new AdminMainControl();

            // Initialize cache
            //LoadCacheData();
        }
        #endregion CONSTRUCTOR

        #region LISTBOX ADMIN
        /// <summary>
        /// Loads user details from data_users.csv and populates the ListBox with formatted information.
        /// The method reads data from the user file, skips the header and admin details, and processes each user's details.
        /// It formats the list item to display the user's name, surname, alias, email, phone number, 
        /// and indicates whether the user is online based on the data in the file.
        /// </summary>>
        public void LoadDetailsListBox()
        {
            // Ensure DataCache is loaded
            DataCache.LoadCache();

            // Check if the cached user data is empty or not loaded
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                Debug.WriteLine("LoadDetailsListBox: Cache is empty, reloading Cache.");
                cache.LoadDecryptedData();
            }

            adminControl.listBoxAdmin.Items.Clear();

            foreach (var userDetailsArray in CachedUserData.Skip(2)) // Skip header
            {
                // Selection of items to display n ListBoxAdmin
                string name = userDetailsArray[0];
                string surname = userDetailsArray[1];
                string alias = userDetailsArray[2];
                string email = userDetailsArray[6];
                string phonenumber = userDetailsArray[7];
                string isOnline = userDetailsArray.Length > 8 && userDetailsArray[8] == "True" ? "| [ONLINE]" : string.Empty;

                string listItem = $"{name} {surname} ({alias}) | {email} | {phonenumber} {isOnline}";
                adminControl.listBoxAdmin.Items.Add(listItem);
            }
        }

        /// <summary>
        /// Handles the custom drawing of items in the ListBox, allowing for conditional formatting based on the item content.
        /// This method modifies the color of the text based on whether the item contains the word "ONLINE" and ensures
        /// that the list item is not null before attempting to draw it. It also ensures that a fallback font is used if needed.
        /// </summary>
        /// <param name="sender">The source of the event, expected to be the ListBox control.</param>
        /// <param name="e">The event data that contains drawing parameters, such as the item to be drawn and the graphics context.</param>

        public void ListBoxAdmin_DrawItemHandler(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Safely cast sender to ListBox and check if it’s null
            if (sender is not ListBox listBox ) return;

            // Get the item from the list and handle possible null
            string? listItem = listBox.Items[e.Index]?.ToString();
            if (listItem == null) return;

            e.DrawBackground();

            // Determine the color based on item content
            Color textColor = listItem.Contains("ONLINE") ? Color.DarkOliveGreen : Color.Black;

            // Use a fallback font if e.Font is null
            Font font = e.Font ?? SystemFonts.DefaultFont;

            using (Brush brush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(listItem, font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Reloads the user list box after making changes, refreshing the interface display.
        /// </summary>
        /// <param name="userIndex">The index of the updated user.</param>
        public void ReloadListBoxAdmin(int userIndex)
        {
            // Refresh the cache
            cache.LoadDecryptedData();

            // Refresh only if a valid index is selected
            if (userIndex >= 0 && userIndex < adminControl.listBoxAdmin.Items.Count)
            {
                adminControl.Refresh();
            }

            // Clear the ListBox
            adminControl.listBoxAdmin.Items.Clear();

            // Iterate through the cache and add items to the ListBox
            foreach (var userDetailsArray in cache.CachedUserData.Skip(2)) // Skip headers
            {
                if (userDetailsArray.Length >= 8) // Ensure there are enough fields
                {
                    string name = userDetailsArray[0];
                    string surname = userDetailsArray[1];
                    string alias = userDetailsArray[2];
                    string email = userDetailsArray[6];
                    string phonenumber = userDetailsArray[7];
                    string isOnline = userDetailsArray.Length > 8 && userDetailsArray[8] == "True" ? "| [ONLINE]" : string.Empty;

                    // Format and add the item to the ListBox
                    string listItem = $"{name} {surname} ({alias}) | {email} | {phonenumber} {isOnline}";
                    adminControl.listBoxAdmin.Items.Add(listItem);

                    // Empty textboxes
                    EmptyTextBoxesAdmin();
                }
                else
                {
                    Console.WriteLine("Insufficient fields in a user record.");
                }
            }
        }

        /// <summary>
        /// Handles the event when a user is selected in the ListBox. It fills the details for the selected user in the textboxes,
        /// and disables the Force Log Out button if the selected user is the current admin user.
        /// </summary>
        public void ListBoxAdmin_SelectedIndexChangedHandler()
        {
            // Check if the cached user data is empty or not loaded
            if (cache.CachedLoginData == null || !cache.CachedLoginData.Any())
            {
                MessageBox.Show("\nListBoxAdmin_SelectedIndexChangedHandler>\nCachedUserData is empty or not loaded!"); // Show error if cache is empty
                return;
            }

            var currentUser = AuthenticationService.CurrentUser;

            // Get the selected user from the ListBox; ignore clicks on empty line in listBox
            if (adminControl.listBoxAdmin.SelectedItem is string selectedUserString && !string.IsNullOrEmpty(selectedUserString))
            {
                // Set UserSelected on true
                adminControl.InteractionHandler.UserSelected = true; // Pass bool true to InterActionHandler

                // Extract the alias from the selected text (in the format: "Name Surname (Alias)")
                string selectedAlias = selectedUserString.Split('(', ')')[1]; // Extract the alias between parentheses

                // Ignore btnForceLogOutUser when selection is users own admin account 
                if (currentUser == selectedAlias)
                {
                    adminControl.btnForceLogOutUser.Enabled = false;
                    adminControl.btnForceLogOutUser.Visible = false;
                }

                // Retrieve user details from the cache
                var userDetailsArray = cache.CachedUserData
                                       .Skip(1) // Skip header row
                                       .FirstOrDefault(details => details[2] == selectedAlias);

                if (userDetailsArray != null)
                {
                    FillTextboxesAdmin(userDetailsArray);
                }
                HandleSelectedUserStatus(selectedAlias);
            }
        }

        /// <summary>
        /// Validates the selected user alias and updates the UI accordingly. It checks the login details for the selected alias, 
        /// determines if the user is an admin, and updates the visibility of admin-related fields. 
        /// It also checks if the user is online and enables/disables the Force Log Out button.
        /// </summary>
        /// <param name="selectedAlias">The alias of the selected user to be validated.</param>
        public void HandleSelectedUserStatus(string selectedAlias)
        {
            // Check if the cached login data is empty or not loaded
            if (cache.CachedLoginData == null || !cache.CachedLoginData.Any())
            {
                MessageBox.Show("\nHandleSelectedUserStatus>\nCachedLoginData is empty or not loaded!"); // Show error if cache is empty
                return;
            }

            // Retrieve login details from the cache
            var loginDetails = cache.CachedLoginData
                               .FirstOrDefault(details => details[0] == selectedAlias); // Match alias in login data

            if (loginDetails != null)
            {
                // Update admin-related fields based on login details
                adminControl.txtAdmin.Visible = loginDetails[2] == "True"; // IsAdmin field
                adminControl.chkIsAdmin.Checked = loginDetails[2] == "True";

                // Enable Force Log Out button if the selected user is not the current user
                if (AuthenticationService.CurrentUser != selectedAlias)
                {
                    // Reload the cache to ensure it's up to date before checking online status
                    // LoadCacheData();

                    // Update the state of the Force Log Out button based on the selected user's online status
                    SetForceLogOutUserBtn(selectedAlias);
                }
            }
            else
            {
                adminControl.txtAdmin.Visible = false; // Hide admin-related fields if no login details are found
            }
        }
        #endregion LISTBOX ADMIN

        #region EDIT MODE DISPLAY ADMIN
        /// <summary>
        /// Manages the interface display and controls based on the edit mode status.
        /// </summary>
        public void InterfaceEditModeAdmin()
        {
            Debug.WriteLine($"EditMode AdminInterface: {EditMode}");

            // Toggle Edit and Cancel button text based on EditMode status
            adminControl.btnEditUserDetails.Text = EditMode ? "Cancel" : "Edit User";

            // Set the background color based on EditMode for visual feedback
            adminControl.BackColor = EditMode ? Color.Orange : SystemColors.ActiveCaption;

            // Adjust visibility and enablement of buttons based on EditMode
            ToggleControlVisibility(adminControl.btnSaveEditUserDetails, EditMode, Color.LightGreen);
            ToggleControlVisibility(adminControl.chkIsAdmin, EditMode);

            // Array of text fields to enable or disable in EditMode for user editing
            var textFields = new[]
            {
                adminControl.txtName,
                adminControl.txtSurname,
                adminControl.txtAdmin,
                adminControl.txtAddress,
                adminControl.txtZIPCode,
                adminControl.txtCity,
                adminControl.txtEmail,
                adminControl.txtPhonenumber
             };

            foreach (var field in textFields)
            {
                if (field != null) // Check for null to prevent runtime errors
                {
                    field.Enabled = EditMode;
                }
            }

            // Adjust other action buttons based on EditMode status
            ToggleControlVisibility(adminControl.btnCreateUser, !EditMode);
            ToggleControlVisibility(adminControl.btnDeleteUser, EditMode);
            ToggleControlVisibility(adminControl.btnGeneratePSW, EditMode);

            // Disable ListBox when in edit mode to prevent user changes in selection
            if (adminControl.listBoxAdmin != null)
            {
                adminControl.listBoxAdmin.Enabled = !EditMode;
            }
        }

        /// <summary>
        /// Helper method to set control visibility, enablement, and optional background color.
        /// </summary>
        /// <param name="control">The control to modify.</param>
        /// <param name="isVisible">Whether the control should be visible.</param>
        /// <param name="backColor">Optional background color to set when control is visible.</param>
        private void ToggleControlVisibility(Control control, bool isVisible, Color? backColor = null)
        {
            if (control != null) // Check if control is not null
            {
                control.Visible = isVisible;
                control.Enabled = isVisible;

                if (isVisible && backColor.HasValue)
                {
                    control.BackColor = backColor.Value;
                }
            }
        }

        /// <summary>
        /// Sets the enabled state of the Force Log Out User button based on the user's online status.
        /// </summary>
        /// <param name="aliasToLogOut">The alias of the selected user to check online status.</param>
        public void SetForceLogOutUserBtn(string aliasToLogOut)
        {
            // Check if the cache is empty, and reload data if necessary.
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                Debug.WriteLine("SetForceLogOutUserBtn: Cache is empty, reloading Cache.");
                cache.LoadDecryptedData();
            }

            // Determine if the selected user is online
            bool isOnline = cache.CachedUserData
                .Skip(1) // Skip the header row
                .Where(userDetailsArray => userDetailsArray.Length > 8 && userDetailsArray[2] == aliasToLogOut) // Match alias
                .Any(userDetailsArray => userDetailsArray[8] == "True"); // Check if the user is online based on the 9th column

            Debug.WriteLine($"Checking online status for {aliasToLogOut}: {isOnline}");

            // Enable and display the "Force Log Out User" button if the user is online
            adminControl.btnForceLogOutUser.Enabled = isOnline;
            adminControl.btnForceLogOutUser.Visible = isOnline;
        }

        #endregion EDITMODE DISPLAY

        #region TEXTBOXES ADMIN
        /// <summary>
        /// Clears all textboxes in the interface, resetting their content.
        /// </summary>
        public void EmptyTextBoxesAdmin()
        {
            // Refill textboxes with empty values
            adminControl.txtName.Text = string.Empty;
            adminControl.txtSurname.Text = string.Empty;
            adminControl.txtAlias.Text = string.Empty;
            adminControl.txtAddress.Text = string.Empty;
            adminControl.txtZIPCode.Text = string.Empty;
            adminControl.txtCity.Text = string.Empty;
            adminControl.txtEmail.Text = string.Empty;
            adminControl.txtPhonenumber.Text = string.Empty;

            // Update button states to false
            adminControl.InteractionHandler.UserSelected = false;
        }

        /// <summary>
        /// Populates the textboxes with the details of a selected user.
        /// </summary>
        /// <param name="userDetailsArray">Array containing the user details.</param>
        public void FillTextboxesAdmin(string[] userDetailsArray)
        {
            // Populate the text fields with the details of the selected user
            adminControl.txtName.Text = userDetailsArray[0];
            adminControl.txtSurname.Text = userDetailsArray[1];
            adminControl.txtAlias.Text = userDetailsArray[2];
            adminControl.txtAddress.Text = userDetailsArray[3];
            adminControl.txtZIPCode.Text = userDetailsArray[4];
            adminControl.txtCity.Text = userDetailsArray[5];
            adminControl.txtEmail.Text = userDetailsArray[6];
            adminControl.txtPhonenumber.Text = userDetailsArray[7];
        }
        #endregion TEXTBOXES ADMIN
    }
}
