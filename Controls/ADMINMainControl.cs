using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        public List<bool> storeIsAdminNeoStatus = new List<bool>(); // index 0 = bool admin, index 1 = bool Neo

        public static bool IsTheOne
        {
            get; set;
        }
        public static bool ChkIsTheOneChanged
        {
            get; set;
        }

        public static bool ChkIsAdminChanged
        {
            get; set;
        }



        readonly FilePaths path = new FilePaths();

        readonly AdminInterface adminInterface;
        readonly AccountManager accountManager = new AccountManager();
        readonly ProfileManager profileManager = new ProfileManager();
        readonly FormInteractionHandler interactionHandler = new FormInteractionHandler();
        readonly RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        readonly ReportManager reportManager;

        bool isAdmin;
        bool editMode = false;
        //readonly bool isTheOne = false;
        readonly bool onlineStatus = false;
        readonly bool isSick = false;

        // Property to expose the InteractionHandler instance for external access
        public FormInteractionHandler InteractionHandler => interactionHandler;

        #endregion PROPERTIES

        #region Constructor
        public AdminMainControl(AdminInterface? adminInterface = null)
        {
            InitializeComponent();

            // Assign the UserInterface field; if no instance is provided, create a new UserInterface instance
            this.adminInterface = adminInterface ?? new AdminInterface(this);

            reportManager = new ReportManager(this);

            // Load data_users.csv for display in listbox
            this.adminInterface.LoadDetailsListBox();
        }
        #endregion CONSTRUCTOR

        public void UserControl_Load(object sender, EventArgs e)
        {
            string selectedAlias = txtAlias.Text;
        }

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
            int userIndex = accountManager.FindUserIndexByAlias(txtAlias.Text);

            if (userIndex != -1)
            {
                profileManager.UpdateUserDetails(txtName.Text, txtSurname.Text, txtAlias.Text, txtAddress.Text, txtZIPCode.Text, txtCity.Text,
                                                 txtEmail.Text, txtPhonenumber.Text, isAdmin, onlineStatus, isSick);
            }
            editMode = false; // Close editMode
            adminInterface.EditMode = false;
            adminInterface.InterfaceEditModeAdmin();
            adminInterface.ReloadListBoxWithSelection(txtAlias.Text); // Reload listbox
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
                AdminMainControl adminControl = new AdminMainControl();
                DataCache cache = new DataCache();
                // Deleting user from files
                profileManager.DeleteUser(txtAlias.Text);

                // Empty TextBoxes and reload ListBox
                adminInterface.EmptyTextBoxesAdmin();
                adminInterface.ReloadListBoxWithSelection(txtAlias.Text);

                // Toggle edit mode
                adminInterface.EditMode = ToggleEditMode();
                adminInterface.InterfaceEditModeAdmin();
            },
            () => message.MessageInvalidNoUserSelected()); // Handle no user selected case
        }

        /// <summary>
        /// Handles the click event to add a new user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            interactionHandler.Open_CreateForm(this);

            // Reload Cache
            DataCache cache = new DataCache();
            cache.LoadDecryptedData();

            // Reload listbox
            listBoxAdmin.Items.Clear();
            adminInterface.ReloadListBoxWithSelection(txtAlias.Text);
        }

        /// <summary>
        /// Handles click event to create a new password
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                profileManager.GeneratePasswordNewUser(txtAlias.Text);
            },
             () => message.MessageInvalidNoUserSelected());
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

        /// <summary>
        /// Handles the click event to force log out the selected user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnForceLogOutUser_Click(object sender, EventArgs e)
        {
            AuthenticationService authenticationService = new AuthenticationService();

            // Find the user index based on the alias
            int userIndex = accountManager.FindUserIndexByAlias(txtAlias.Text);

            // Perform action only if a user is selected
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                // If a valid user is found, force logout
                authenticationService.ForceLogOut(txtAlias.Text);
                MessageBox.Show($"User {txtAlias.Text} has been forced logged out.");

                // Reload the listbox to reflect changes
                listBoxAdmin.Items.Clear();
                adminInterface.ReloadListBoxWithSelection(txtAlias.Text);

                // Disable and hide the logout button after action
                btnForceLogOutUser.Enabled = false;
                btnForceLogOutUser.Visible = false;
            },
            () =>
            {
                // Handle the case where no user is selected
                message.MessageInvalidNoUserSelected();
            });
        }

        /// <summary>
        /// Handles the drawing of items in the ListBox. This method delegates the actual drawing 
        /// process to the <see cref="ListBoxAdmin_DrawItemHandler"/> method in the AdminInterface.
        /// </summary>
        /// <param name="sender">The source of the event, typically the ListBox control.</param>
        /// <param name="e">The event data that contains the drawing information for the item.</param>
        public void ListBoxAdmin_DrawItem(object sender, DrawItemEventArgs e)
        {
            adminInterface.ListBoxAdmin_DrawItemHandler(sender, e);
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            adminInterface.PreviousPage();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            adminInterface.NextPage();
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            //
        }

        /// <summary>
        /// Handles the state change of the 'Absence Due to Illness' checkbox.
        /// If the checkbox changes from checked to unchecked, it performs an action
        /// to update the illness absence status for the selected user.
        /// </summary>
        /// <param name="sender">The source of the event (the CheckBox).</param>
        /// <param name="e">The event data (checkbox state change).</param>
        private void chkAbsenceDueIllness_CheckedChanged(object sender, EventArgs e)
        {
            /*
            // Update the isSick variable to reflect the current state of the checkbox
            isSick = chkAbsenceDueIllness.Checked;

            // Check if the checkbox state changes from checked (true) to unchecked (false)
            if (!isSick && previousSickStatus)
            {
                // Confirm to save changes
                DialogResult dr = message.MessageConfirmCallInSickNotification(txtAlias.Text);
                if (dr != DialogResult.Yes)
                {
                    return;
                }

                // Perform an action when the checkbox is unchecked (illness resolved)
                profileManager.AbsenceDueIllness(isSick, txtAlias.Text);
            }
            else
            {
                return;
            }

            // Update the previousSickStatus to store the current state of the checkbox
            previousSickStatus = isSick;
            */
        }

        private void btnCallInSick_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                // Create an instance of the AbsenceDueIllness form
                AbsenceDueIllnessForm absence = new AbsenceDueIllnessForm();

                // Pass the form instance to DisplayUserAlias to set the alias
                DisplayUserAlias(absence);

                // Open the absence form
                interactionHandler.Open_AbsenceDueIllnessForm(absence); // Pass the existing instance to the method
            },
            () => message.MessageInvalidNoUserSelected());
        }

        public void DisplayUserAlias(AbsenceDueIllnessForm absence)
        {
            if (!string.IsNullOrEmpty(txtAlias.Text))
            {
                absence.txtAlias.Text = $"{txtAlias.Text.ToUpper()}";
            }
            else
            {
                absence.txtAlias.Text = "UNKNOWN";
            }
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            ReportManager reportManager = new ReportManager(this);
            reportManager.BtnSaveReportHandler();
            btnEditUserDetails.Enabled = true; // Enable btnEditUserDetails
            btnSaveEditUserDetails.Enabled = true; // Enable btnSaveEditUserDetails
        }

        /// <summary>
        /// Handles the deletion of a selected file from the listViewFiles control. 
        /// Only TheOne's are allowed to perform this action.
        /// </summary>
        /// <param name="sender">The source of the event, typically the delete button.</param>
        /// <param name="e">Contains event data.</param>
        private void btnDeleteFileReport_Click(object sender, EventArgs e)
        {
            reportManager.DeleteFileReport();
        }

        /// <summary>
        /// Handles the click event of the "Show Logs" button.
        /// Opens the report form to display log details in a ListBox.
        /// </summary>
        /// <param name="sender">The source of the event (button).</param>
        /// <param name="e">Event arguments associated with the click event.</param>
        private void btnShowListBoxLogs_Click(object sender, EventArgs e)
        {
            // Null check
            string logFile = FindCSVFiles.FindCSVFileLogEvent(txtAlias.Text, "logevents");
            if (string.IsNullOrEmpty(logFile))
            {
                // Show a message if no log file is found
                MessageBox.Show("Log file not found.");
                return;
            }
            else
            {
                interactionHandler.Open_ShowLogEventsForm(this, txtAlias.Text);
            }
        }

        private void btnShowLogsStatus_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                // Null check
                string logFile = FindCSVFiles.FindCSVFileLogEvent(txtAlias.Text, "logstatus");
                if (string.IsNullOrEmpty(logFile))
                {
                    // Show a message if no log file is found
                    MessageBox.Show("Log file not found.");
                    return;
                }
                else
                {
                    interactionHandler.Open_ShowLogStatusForm(this, txtAlias.Text);
                }
            },
            () => message.MessageInvalidNoUserSelected());
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            comboBoxSubjectReport.Text = "Subject:";
            btnEditUserDetails.Enabled = AdminInterface.IsReport; // Toggle btnEditUserDetails
            btnSaveEditUserDetails.Enabled = AdminInterface.IsReport; // Toggle btnSaveEditUserDetails
            adminInterface.TextBoxesReportEmpty();
            AdminInterface.IsReport = ToggleIsReportMode();
            adminInterface.ReportConfig();

            /*
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                comboBoxSubjectReport.Text = "Subject:";
                btnEditUserDetails.Enabled = AdminInterface.IsReport; // Toggle btnEditUserDetails
                btnSaveEditUserDetails.Enabled = AdminInterface.IsReport; // Toggle btnSaveEditUserDetails
                adminInterface.TextBoxesReportEmpty();
                AdminInterface.IsReport = ToggleIsReportMode();
                adminInterface.ReportConfig();
            },
            () => message.MessageInvalidNoUserSelected());
        */
        }
        #endregion BUTTONS SoC (Seperate of Concerns)

        #region TOGGLE MODES
        /// <summary>
        /// Toggle between editMode and !editMode
        /// </summary>
        private bool ToggleEditMode()
        {
            bool modus = editMode = !editMode;

            return modus;
        }

        /// <summary>
        /// Toggle between IsReport and !IsReport
        /// </summary>
        public bool ToggleIsReportMode()
        {
            bool modus = AdminInterface.IsReport = !AdminInterface.IsReport;
            return modus;
        }
        #endregion TOGGLE MODES

        #region KEY HANDLERS
        /// <summary>
        /// Handles the KeyPress event for the txtPhonenumber textbox.
        /// Allows only numeric digits, '+', '-', Backspace, Spacebar, and clipboard shortcuts (Ctrl+C and Ctrl+V).
        /// Suppresses any other key inputs to ensure only valid phone number characters are entered.
        /// </summary>
        /// <param name="sender">The source of the event, typically the TxtPhonenumber textbox.</param>
        /// <param name="e">The KeyEventArgs containing the event data.</param>
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
                (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || // Digits (numpad)
                e.KeyCode == Keys.Back || // Backspace
                e.KeyCode == Keys.Space || // Spacebar
                e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add || // Plus
                e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract || // Minus
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
        /// Suppresses any other key inputs to prevent invalid characters from being entered.
        /// </summary>
        public void TxtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsLetter((char)e.KeyCode)
                && e.KeyCode != Keys.Back
                && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right
                && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down
                && e.KeyCode != Keys.Space
                && !e.Control && !e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the txtSurname textbox.
        /// Allows only letters, Backspace, arrow keys, and Ctrl/Shift key combinations.
        /// Suppresses any other key inputs to prevent invalid characters from being entered.
        /// </summary>
        public void TxtSurname_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsLetter((char)e.KeyCode)
                && e.KeyCode != Keys.Back
                && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right
                && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down
                && e.KeyCode != Keys.Space
                && !e.Control && !e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Handles the KeyDown event for the txtCity textbox.
        /// Allows only letters, Backspace, arrow keys, and Ctrl/Shift key combinations.
        /// Suppresses any other key inputs to prevent invalid characters from being entered.
        /// </summary>
        public void TxtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsLetter((char)e.KeyCode)
                && e.KeyCode != Keys.Back
                && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right
                && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down
                && e.KeyCode != Keys.Space
                && !e.Control && !e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }
        #endregion KEY HANDLERS

        #region CHECKBOXES
        /// <summary>
        /// Handles the CheckedChanged event for the chkIsAdmin CheckBox. 
        /// Compares the new isAdmin status with the stored initial status to determine if a change has occurred.
        /// Updates the ChkIsAdminChanged flag and synchronizes the isAdmin value with the AdminInterface if needed.
        /// </summary>
        /// <param name="sender">The source of the event (chkIsAdmin).</param>
        /// <param name="e">Event data associated with the CheckedChanged event.</param>
        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            // Retrieve the initial isAdmin status from the first item in storeIsAdminStatus
            bool initialIsAdminStatus = storeIsAdminNeoStatus[0];

            // Get the new isAdmin status from the CheckBox
            bool isAdminNewStatus = chkIsAdmin.Checked;

            // Compare the new status with the initial status
            if (isAdminNewStatus == initialIsAdminStatus)
            {
                // No change in the isAdmin status
                ChkIsAdminChanged = false;
                Debug.WriteLine($"\nisAdminNewStatus = {isAdminNewStatus} initialIsAdminStatus = {storeIsAdminNeoStatus[0]}");
                Debug.WriteLine($"No changes: ChkIsAdminChanged = {ChkIsAdminChanged}");
            }
            else
            {
                // Status has changed; update the flag and isAdmin property
                Debug.WriteLine($"\nisAdminNewStatus = {isAdminNewStatus}, initialIsAdminStatus = {storeIsAdminNeoStatus[0]}");

                ChkIsAdminChanged = true;
                Debug.WriteLine($"ChkIsAdminChanged: {ChkIsAdminChanged}");

                // Update isAdmin and synchronize with the AdminInterface
                isAdmin = chkIsAdmin.Checked;
                AdminInterface.IsSelectedUserAdmin = isAdmin;
            }

            // Note: The clearing of storeIsAdminStatus is handled in AdminInterface.ListBoxAdmin_SelectedIndexChangedHandler.
        }

        /// <summary>
        /// Handles the CheckedChanged event for the chkIsTheOne CheckBox. 
        /// Compares the new isTheOne status with the stored initial status to determine if a change has occurred.
        /// Updates the ChkIsTheOneChanged flag and synchronizes the isTheOne value with the AdminInterface if needed.
        /// </summary>
        /// <param name="sender">The source of the event (chkIsTheOne).</param>
        /// <param name="e">Event data associated with the CheckedChanged event.</param>
        private void chkIsTheOne_CheckedChanged(object sender, EventArgs e)
        {
            // Retrieve the initial isTheOne status from the second item (index 1) in storeIsAdminNeoStatus
            bool initialIsTheOneStatus = storeIsAdminNeoStatus[1];

            // Get the new isTheOne status from the CheckBox
            bool isTheOneNewStatus = chkIsTheOne.Checked;

            // Compare the new status with the initial status
            if (isTheOneNewStatus == initialIsTheOneStatus)
            {
                // No change in the isTheOne status
                ChkIsTheOneChanged = false;
                Debug.WriteLine($"isTheOneNewStatus = {isTheOneNewStatus}, initialIsTheOneStatus = {storeIsAdminNeoStatus[1]}");
                Debug.WriteLine($"No changes: ChkIsTheOneChanged = {ChkIsTheOneChanged}");
            }
            else
            {
                // Status has changed; update the flag and isTheOne property
                Debug.WriteLine($"isTheOneNewStatus = {isTheOneNewStatus}, initialIsTheOneStatus = {storeIsAdminNeoStatus[1]}");

                ChkIsTheOneChanged = true;
                Debug.WriteLine($"ChkIsTheOneChanged: {ChkIsTheOneChanged}");

                // Update isTheOne and synchronize with the AdminInterface
                IsTheOne = chkIsTheOne.Checked;
                AdminInterface.IsSelectedUserTheOne = IsTheOne;
            }

            // Note: The clearing of storeIsAdminNeoStatus is handled in AdminInterface.ListBoxAdmin_SelectedIndexChangedHandler.
        }


        /*
        /// <summary>
        /// Handles the event triggered when the 'Is The One' checkbox state changes.
        /// Toggles the 'IsTheOne' property based on the checkbox state.
        /// If the checkbox is checked and the selected user is an admin, marks the current user as 'The One'.
        /// If the checkbox is unchecked or the selected user is not an admin, the 'IsTheOne' property is set to false.
        /// Sets flag 'ChkIsTheOneChanged' to true to indicate a change in the checkbox state.
        /// </summary>
        /// <param name="sender">The source of the event, typically the 'chkIsTheOne' checkbox.</param>
        /// <param name="e">The event data containing information about the state change of the checkbox.</param>
        public void chkIsTheOne_CheckedChanged(object sender, EventArgs e)
        {
            // Retrieve the initial isAdmin status from the first item in storeIsAdminStatus
            bool initialIsNeoStatus = storeIsAdminNeoStatus[1];
            Debug.WriteLine($"initialIsAdminStatus = {initialIsNeoStatus}");

            // Get the new isAdmin status from the CheckBox
            bool isAdminNewStatus = chkIsAdmin.Checked;


            ChkIsTheOneChanged = true;

            if (AdminInterface.IsSelectedUserIsAdmin && chkIsTheOne.Checked)
            {
                IsTheOne = chkIsTheOne.Checked;
            }
            else
            {
                IsTheOne = false;
                ChkIsTheOneChanged = true;
            }
        }
        */
        #endregion CHECKBOXES

        #region SELECTED INDEX CHANGED
        /// <summary>
        /// Handles the selection change event for the ListBox in the admin interface.
        /// Triggers the appropriate selection handler in AdminInterface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void ListBoxAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewReports.Items.Clear();
            adminInterface.ListBoxAdmin_SelectedIndexChangedHandler();
        }

        /// <summary>
        /// Event handler triggered when the selected item in the listViewReports changes.
        /// Displays the selected user report if an item is selected and valid.
        /// </summary>
        /// <param name="sender">The source of the event, typically the listViewReports.</param>
        /// <param name="e">Event data containing information about the event.</param>
        private void listViewReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if at least one item is selected in the ListView
            if (listViewReports.SelectedItems.Count > 0)
            {
                // Get the file name of the selected user report
                string selectedUserReportFileName = listViewReports.SelectedItems[0].Text;

                // Ensure the selected file name is not null or empty
                if (!string.IsNullOrEmpty(selectedUserReportFileName))
                {
                    // Retrieve the alias of the selected user from the txtAlias textbox
                    string selectedAlias = txtAlias.Text;

                    // Use the ReportManager to display the selected report
                    reportManager.ReportDisplay(selectedUserReportFileName, selectedAlias);
                }
            }
        }

        /// <summary>
        /// Event handler triggered when there is an attempt to resize a column in listViewReports.
        /// Prevents the user from resizing columns by locking their widths.
        /// </summary>
        /// <param name="sender">The source of the event, typically the listViewReports.</param>
        /// <param name="e">Event data containing information about the column width change.</param>
        private void listViewReports_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            // Prevent column width changes by setting the new width to the current width
            e.NewWidth = listViewReports.Columns[e.ColumnIndex].Width;

            // Cancel the resize action
            e.Cancel = true;
        }
        #endregion SELECTED INDEX CHANGED

        #region TEXTBOX SEARCH
        public int searchCurrentPage = 1; // Current page number for the search results, starting at 1
        private const int searchItemsPerPage = 15; // Maximum number of items displayed per page during search
        private List<string> currentSearchResults = new(); // Cache to store the current search results for efficient pagination


        /// <summary>
        /// Dynamically updates the user list displayed in the ListBox based on the search term entered.
        /// If the search term is empty, it resets the list to show all users.
        /// </summary>
        /// <param name="sender">The source of the event (the TextBox).</param>
        /// <param name="e">The event data for the text change event.</param>
        private void txtAliasToSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the trimmed search term from the TextBox, handling possible null values
            string? searchTerm = txtSearch.Text?.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                // If the search term is empty:
                // Reset the page number, clear cached results, and reload the full list
                searchCurrentPage = 1;
                currentSearchResults.Clear();
                DisplaySearchResults(searchCurrentPage); // Clear the display and show all users
                adminInterface.LoadDetailsListBox(); // Load all users into the ListBox
                adminInterface.EmptyTextBoxesAdmin(); // Clear all input TextBoxes

                // Empty all report textboxes and listView
                AdminInterface.IsReport = false;
                adminInterface.ReportConfig();
                adminInterface.TextBoxesReportEmpty();
                reportTxtAlias.Text = string.Empty;
                listViewReports.Items.Clear();
                /*
                txtAliasReport.Clear();
                txtDateReport.Clear();
                rtxReport.Clear();
                */
                //adminInterface.TextBoxesReportConfig();
            }
            else
            {
                // If a search term is provided:
                // Perform the search and display results starting from the first page
                currentSearchResults = new UserSearchService().SearchUsers(searchTerm);
                DisplaySearchResults(1); // Always start at page 1 for new search terms
            }
        }

        /// <summary>
        /// Displays a subset of the search results in the ListBox based on the specified page number.
        /// If no results are found, it shows a placeholder message.
        /// </summary>
        /// <param name="page">The current page to display.</param>
        private void DisplaySearchResults(int page)
        {
            // Check if there are any results in the search cache
            int totalResults = currentSearchResults.Count;
            if (totalResults == 0)
            {
                // If no results are found:
                // Clear the ListBox, add a placeholder message, and update the page label
                listBoxAdmin.Items.Clear();
                listBoxAdmin.Items.Add("No results found.");
                adminInterface.UpdatePageLabel();
                return;
            }

            // Calculate the total number of pages based on results per page
            int totalPages = (int)Math.Ceiling(totalResults / (double)searchItemsPerPage);

            // Ensure the current page is within the valid range
            searchCurrentPage = Math.Clamp(page, 1, totalPages);

            // Calculate the range of results to display for the current page
            int startIndex = (searchCurrentPage - 1) * searchItemsPerPage;
            int endIndex = Math.Min(startIndex + searchItemsPerPage, totalResults);

            // Populate the ListBox with results for the current page
            listBoxAdmin.Items.Clear();
            listBoxAdmin.Items.AddRange(currentSearchResults.Skip(startIndex).Take(searchItemsPerPage).ToArray());

            // Update the page navigation label to reflect the current page and total pages
            adminInterface.UpdatePageLabel();
        }
        #endregion TEXTBOX SEARCH
    }
}