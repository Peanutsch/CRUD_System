using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
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
        public bool IsReport { get; set; }
        public bool SelectedUserIsTheOne { get; set; }
        public static bool SelectedUserIsAdmin { get; set; }

        public List<string[]> CachedUserData => cache.CachedUserData;
        public List<string[]> CachedLoginData => cache.CachedLoginData;

        private int currentPage = 1; // Track pagenumbers
        private const int itemsPerPage = 15; // Maximum items per page in listBoxAdmin

        readonly DataCache cache = new DataCache();
        private readonly AdminMainControl adminControl;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public AdminInterface(AdminMainControl? adminControl = null)
        {
            this.adminControl = adminControl ?? new AdminMainControl();
        }
        #endregion CONSTRUCTOR

        #region LISTBOX ADMIN
        /// <summary>
        /// Loads user details from data_users.csv and populates the ListBox with formatted information.
        /// The method reads data from the user file, skips the header and admin details, and processes each user's details.
        /// It formats the list item to display the user's name, surname, alias, email, phone number, and indicates whether the user is online based on the data in the file.
        /// Loads a specific page of user details into the ListBox, with a maximum of 15 items per page.
        /// </summary>>
        public void LoadDetailsListBox()
        {
            // Ensure DataCache is loaded
            DataCache.LoadCache();

            // Check if the cached user data is empty or not loaded
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            // Clear the ListBox
            adminControl.listBoxAdmin.Items.Clear();

            // Calculate start and end indices for the current page
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, CachedUserData.Count);

            // Skip header rows and load items for the current page
            var userDetailsForPage = CachedUserData.Skip(2).Skip(startIndex).Take(itemsPerPage);

            foreach (var userDetailsArray in userDetailsForPage)
            {
                // Selection of items to display in ListBoxAdmin
                string name = userDetailsArray[0];
                string surname = userDetailsArray[1];
                string alias = userDetailsArray[2];
                string email = userDetailsArray[6];
                string phonenumber = userDetailsArray[7];
                string isOnline = userDetailsArray.Length > 8 && userDetailsArray[8] == "True" ? "| [ONLINE]" : string.Empty;
                string isSick = userDetailsArray.Length > 9 && userDetailsArray[9] == "True" ? "| [ABSENCE due ILLNESS]" : string.Empty;

                string listItem = $"{name} {surname} ({alias}) | {email} | {phonenumber} {isOnline} {isSick}";

                adminControl.listBoxAdmin.Items.Add(listItem);

                UpdatePageLabel();
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

            // Declare the default text color
            Color textColor = Color.Black;

            // Determine the color based on item content. 
            if (listItem.Contains("ONLINE") && listItem.Contains("ABSENCE due ILLNESS"))
            {
                textColor = Color.Orange;
            }
            else if (listItem.Contains("ONLINE"))
            {
                textColor = Color.DarkOliveGreen;
            }
            else if (listItem.Contains("ABSENCE due ILLNESS"))
            {
                textColor = Color.Violet;
            }
            else
            {
                textColor = Color.Black;
            }

            // Use a fallback font if e.Font is null
            Font font = e.Font ?? SystemFonts.DefaultFont;

            using (Brush brush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(listItem, font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Reloads the ListBox and reselects the specified item.
        /// </summary>
        /// <param name="aliasToSelect">The alias of the user to reselect after reloading.</param>
        public void ReloadListBoxWithSelection(string aliasToSelect)
        {
            // Controleer of ListBoxAdmin is geïnitialiseerd
            if (adminControl?.listBoxAdmin == null)
            {
                throw new InvalidOperationException("ListBoxAdmin is not initialized.");
            }

            // Refresh the cache
            cache.LoadDecryptedData();

            // Clear the ListBox
            adminControl.listBoxAdmin.Items.Clear();

            // Calculate start and end indices for the current page
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, cache.CachedUserData.Count);

            // Skip header rows and load items for the current page
            var userDetailsForPage = cache.CachedUserData.Skip(2).Skip(startIndex).Take(itemsPerPage);

            foreach (var userDetailsArray in userDetailsForPage)
            {
                // Selection of items to display in ListBoxAdmin
                string name = userDetailsArray[0];
                string surname = userDetailsArray[1];
                string alias = userDetailsArray[2];
                string email = userDetailsArray[6];
                string phonenumber = userDetailsArray[7];
                string isOnline = userDetailsArray.Length > 8 && userDetailsArray[8] == "True" ? "| [ONLINE]" : string.Empty;
                string isSick = userDetailsArray.Length > 9 && userDetailsArray[9] == "True" ? "| [ABSENCE due ILLNESS]" : string.Empty;

                string listItem = $"{name} {surname} ({alias}) | {email} | {phonenumber} {isOnline} {isSick}";
                if (!string.IsNullOrEmpty(listItem))
                {
                    adminControl.listBoxAdmin.Items.Add(listItem);
                }
            }

            // Update the page label
            UpdatePageLabel();

            // Try to reselect the previously edited item
            if (!string.IsNullOrEmpty(aliasToSelect))
            {
                for (int i = 0; i < adminControl.listBoxAdmin.Items.Count; i++)
                {
                    var currentItem = adminControl.listBoxAdmin.Items[i];
                    if (currentItem?.ToString()!.Contains($"({aliasToSelect})") == true)
                    {
                        adminControl.listBoxAdmin.SelectedIndex = i;
                        break;
                    }
                }
            }

            // Refresh the ListBox to trigger the DrawItem event
            adminControl.listBoxAdmin.Refresh();
        }

        /// <summary>
        /// Handles the event when a user is selected in the ListBox. It fills the details for the selected user in the textboxes,
        /// and disables the Force log Out button if the selected user is the current admin user.
        /// </summary>
        public void ListBoxAdmin_SelectedIndexChangedHandler()
        {
            // Empty textboxes report field
            TextBoxesReportEmpty();

            // Check if the cached user data is empty or not loaded
            if (cache.CachedLoginData == null || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
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

                // Retrieve login details from the cache
                var loginDetailsArray = cache.CachedLoginData!
                                       .Skip(1) // Skip header row
                                       .FirstOrDefault(details => details[0] == selectedAlias);

                // If selected user is TheOne, bool selectedUserIsTheOne is true
                if (loginDetailsArray![4] == "True")
                {
                    SelectedUserIsTheOne = true;
                }

                FindReportFile(selectedAlias);
                HandleSelectedUserStatus(selectedAlias);
            }
        }

        public void FindReportFile(string selectedAlias)
        {
            ListViewFiles listView = new ListViewFiles(adminControl);
            string reportDirectory = FindCSVFiles.FindReportFile(selectedAlias, "report");
            listView.LoadFilesIntoListView(reportDirectory);
        }

        /// <summary>
        /// Validates the selected user alias and updates the UI accordingly. It checks the login details for the selected alias, 
        /// determines if the user is an admin, and updates the visibility of admin-related fields. 
        /// It also checks if the user is online and enables/disables the Force log Out button.
        /// </summary>
        /// <param name="selectedAlias">The alias of the selected user to be validated.</param>
        public void HandleSelectedUserStatus(string selectedAlias)
        {
            // Check if the cached user data is empty or not loaded
            if (cache.CachedLoginData == null || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            // Retrieve login details from the cache
            var loginDetails = cache.CachedLoginData?.FirstOrDefault(details => details[0] == selectedAlias); // Match alias in login data
            var userDetails = cache.CachedUserData?.FirstOrDefault(details => details[2] == selectedAlias); // Match alias in user data

            adminControl.txtAbsenceIllness.Visible = userDetails![9] == "True"; // isSick
            adminControl.txtAdmin.Visible = loginDetails![2] == "True"; // IsAdmin

            if (loginDetails![2] == "True")
            {
                SelectedUserIsAdmin = true;
            }

            if (loginDetails != null && userDetails != null && AuthenticationService.CurrentUserIsTheOne)
            {
                adminControl.btnDeleteUser.Visible = EditMode;
                adminControl.btnShowListBoxLogs.Visible = EditMode;
                adminControl.btnDeleteReport.Visible = EditMode;

                adminControl.chkIsTheOne.Visible = EditMode;
                adminControl.chkIsTheOne.Checked = SelectedUserIsTheOne;
                adminControl.chkIsAdmin.Visible = EditMode;

                // Update checkbox fields based on login- and userdetails
                adminControl.txtAdmin.Visible = loginDetails[2] == "True"; // IsAdmin
                adminControl.chkIsAdmin.Checked = loginDetails[2] == "True";
                adminControl.chkIsTheOne.Checked = loginDetails[4] == "True";
            }
            
            // Enable Force log Out button if the selected user is not the current user
            if (AuthenticationService.CurrentUser != selectedAlias)
            {
                // Update the state of the Force log Out button based on the selected user's online status
                SetForceLogOutUserBtn(selectedAlias);
            }
            else
            {
                adminControl.txtAdmin.Visible = false; // Hide admin-related fields if no login details are found
                adminControl.txtAbsenceIllness.Visible = false; // Hide isSick-related fields if no user details are found
            }
        }
        #endregion LISTBOX ADMIN

        #region LISTBOX PAGES
        /// <summary>
        /// Navigates to the next page listBoxAdmin if it exists.
        /// </summary>
        public void NextPage()
        {
            int totalPages = (int)Math.Ceiling((CachedUserData.Count - 2) / (double)itemsPerPage); // Total pages (subtract header rows)
            if (currentPage < totalPages)
            {
                adminControl.btnNextPage.Enabled = true;
                currentPage++;
                LoadDetailsListBox();
                //EmptyTextBoxesAdmin();
            }
        }

        /// <summary>
        /// Navigates to the previous page listBoxAdmin if it exists.
        /// </summary>
        public void PreviousPage()
        {
            if (currentPage > 1)
            {
                adminControl.btnPreviousPage.Enabled = true;
                currentPage--;
                LoadDetailsListBox();
                EmptyTextBoxesAdmin();
            }
        }

        /// <summary>
        /// Updates the page navigation label to display the current page and total pages.
        /// If no pages are available, it shows a placeholder message.
        /// </summary>
        public void UpdatePageLabel()
        {
            int totalPages = (int)Math.Ceiling((CachedUserData.Count - 2) / (double)itemsPerPage);
            adminControl.lblPageNumber.Text = totalPages > 0 ? $"Page {currentPage} of {totalPages}" : "No pages available";
        }
        #endregion LISTBOX PAGES

        #region EDIT MODE DISPLAY ADMIN
        /// <summary>
        /// Manages the interface display and controls based on the edit mode status.
        /// </summary>
        public void InterfaceEditModeAdmin()
        {
            var currentUser = AuthenticationService.CurrentUser;
            // Toggle Edit and Cancel button text based on EditMode status
            adminControl.btnEditUserDetails.Text = EditMode ? "Exit" : "Edit User";

            // Set the background color based on EditMode for visual feedback
            adminControl.BackColor = EditMode ? Color.Orange : SystemColors.ActiveCaption;

            ToggleControlVisibility(adminControl.btnSaveEditUserDetails, EditMode, Color.LightGreen);
            ToggleControlVisibility(adminControl.btnGeneratePSW, EditMode);

            if (AuthenticationService.CurrentUserIsTheOne)
            {
                // Adjust visibility and enablement of buttons based on EditMode
                ToggleControlVisibility(adminControl.btnSaveEditUserDetails, EditMode, Color.LightGreen);
                ToggleControlVisibility(adminControl.btnGeneratePSW, EditMode);
                ToggleControlVisibility(adminControl.chkIsAdmin, EditMode);
                ToggleControlVisibility(adminControl.chkIsTheOne, EditMode);
                ToggleControlVisibility(adminControl.btnDeleteReport, EditMode);
                ToggleControlVisibility(adminControl.btnDeleteUser, EditMode);
                ToggleControlVisibility(adminControl.btnShowListBoxLogs, EditMode);
            }

            // Array of text fields to enable or disable in EditMode for user editing
            var textFields = new[]
            {
                adminControl.txtName,
                adminControl.txtSurname,
                adminControl.txtAddress,
                adminControl.txtZIPCode,
                adminControl.txtCity,
                adminControl.txtEmail,
                adminControl.txtPhonenumber,
             };

            foreach (var field in textFields)
            {
                if (field != null) // Check for null to prevent runtime errors
                {
                    field.Enabled = EditMode;
                }
            }
            
            // Disable ListBox when in edit mode to prevent user changes in selection
            if (adminControl.listBoxAdmin != null)
            {
                adminControl.listBoxAdmin.Enabled = !EditMode;
                adminControl.txtSearch.Enabled = !EditMode;
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
        /// Sets the enabled state of the Force log Out User button based on the user's online status.
        /// </summary>
        /// <param name="aliasToLogOut">The alias of the selected user to check online status.</param>
        public void SetForceLogOutUserBtn(string aliasToLogOut)
        {
            var currentUser = AuthenticationService.CurrentUser;
            if (AuthenticationService.CurrentUserIsTheOne)
            {
                // Check if the cache is empty, and reload data if necessary.
                if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
                {
                    cache.LoadDecryptedData();
                }

                // Determine if the selected user is online
                bool isOnline = cache.CachedUserData
                    .Skip(1) // Skip the header row
                    .Where(userDetailsArray => userDetailsArray.Length > 8 && userDetailsArray[2] == aliasToLogOut) // Match alias
                    .Any(userDetailsArray => userDetailsArray[8] == "True"); // Check if the user is online based on the 9th column

                // Display the "Force log Out User" button if the user is online
                adminControl.btnForceLogOutUser.Enabled = isOnline;
                adminControl.btnForceLogOutUser.Visible = isOnline;
            }
        }
        #endregion EDITMODE DISPLAY

        #region TEXTBOXES ADMIN
        /// <summary>
        /// Clears all textboxes in the interface, resetting their content.
        /// </summary>
        public void EmptyTextBoxesAdmin()
        {
            // Array textboxes for refill with string.Empty
            var textFields = new[]
            {
            adminControl.txtName,
            adminControl.txtSurname,
            adminControl.txtAlias,
            adminControl.txtAddress,
            adminControl.txtZIPCode,
            adminControl.txtCity,
            adminControl.txtEmail,
            adminControl.txtPhonenumber 
            };

            foreach (var field in textFields)
            {
                field.Text = string.Empty;
            }

            adminControl.txtAdmin.Visible = false;
            adminControl.txtAbsenceIllness.Visible = false;

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

            // Populate text fields in Notes
            string currentDate = Timers.CurrentDate.ToShortDateString();
            string currentTime = Timers.CurrentTime.ToString(@"hh\:mm\:ss");

            adminControl.txtAliasReport.Text = adminControl.txtAlias.Text;
        }
        #endregion TEXTBOXES ADMIN

        #region TEXTBOXES REPORT
        public void TextBoxesReportEmpty()
        {
            adminControl.txtDateReport.Text = string.Empty;
            adminControl.txtCreator.Text = string.Empty;
            adminControl.txtSubject.Text = string.Empty;
            adminControl.rtxReport.Text = string.Empty;
        }

        public void TextBoxesReportConfig()
        {
            TextBoxesReportEmpty();

            adminControl.txtDateReport.Text = DateTime.Now.ToString("dd-MM-yyyy");

            adminControl.txtSubject.Visible = !IsReport;
            adminControl.txtCreator.Visible = !IsReport;
            adminControl.rtxReport.ReadOnly = !IsReport;
            adminControl.lblCreatedBy.Visible = !IsReport;
            adminControl.lblCurrentDate.Visible = IsReport;
            
            adminControl.comboBoxSubjectReport.Visible = IsReport;
            
            adminControl.btnCreateReport.Text = adminControl.ToggleIsReportMode() ? "Report" : "Exit";
            adminControl.rtxReport.BackColor = adminControl.ToggleIsReportMode() ? Color.White : Color.LightGray;
            adminControl.btnSaveReport.Visible = IsReport;
            adminControl.listViewFiles.SelectedItems.Clear();
        }
        #endregion TEXTBOXES REPORT
    }
}
