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
        public static bool IsTheOne {  get; set; }
        public static bool ChkIsTheOneChanged { get; set; }

        readonly FilePaths path = new FilePaths();

        readonly AdminInterface adminInterface;
        readonly AccountManager accountManager = new AccountManager();
        readonly ProfileManager profileManager = new ProfileManager();
        readonly FormInteractionHandler interactionHandler = new FormInteractionHandler();
        readonly RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        readonly ReportManager reportManager;

        bool isAdmin;
        bool editMode = false;
        readonly bool isTheOne = false;
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
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            int userIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, txtAlias.Text);
            int loginIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, txtAlias.Text);

            var loginDetails = loginLines[loginIndex].Split(",");

            if (userIndex != -1)
            {
                profileManager.UpdateUserDetails(userLines, loginLines, userIndex, loginIndex, txtName.Text, txtSurname.Text, txtAlias.Text, txtAddress.Text, txtZIPCode.Text, txtCity.Text, txtEmail.Text, txtPhonenumber.Text, isAdmin, onlineStatus, isSick, isTheOne);
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
                profileManager.GeneratePasswordNewUser(txtAlias.Text, chkIsAdmin.Checked);
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

            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            // Find the user index based on the alias
            int userIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, txtAlias.Text);

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
        /// Toggle between editMode and !editMode
        /// </summary>
        /// <returns></returns>
        private bool ToggleEditMode()
        {
            bool modus = editMode = !editMode;

            return modus;
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

        public bool ToggleIsReportMode()
        {
            bool modus = adminInterface.IsReport = !adminInterface.IsReport;
            return modus;
        }

        private void buttonMakeReport_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                adminInterface.TextBoxesReportEmpty();
                adminInterface.IsReport = ToggleIsReportMode();
                adminInterface.TextBoxesReportConfig();
            },
            () => message.MessageInvalidNoUserSelected());
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            ReportManager reportManager = new ReportManager(this);
            reportManager.btnSaveReportHandler();
        }

        /// <summary>
        /// Handles the deletion of a selected file from the listViewFiles control. 
        /// Only TheOne's are allowed to perform this action.
        /// </summary>
        /// <param name="sender">The source of the event, typically the delete button.</param>
        /// <param name="e">Contains event data.</param>
        private void btnDeleteReport_Click(object sender, EventArgs e)
        {
            DeleteFileReport();
        }

        /// <summary>
        /// Handles the click event of the "Show Logs" button.
        /// Opens the report form to display log details in a ListBox.
        /// </summary>
        /// <param name="sender">The source of the event (button).</param>
        /// <param name="e">Event arguments associated with the click event.</param>
        private void btnShowListBoxLogs_Click(object sender, EventArgs e)
        {
            interactionHandler.Open_ShowReportForm(this, txtAlias.Text);
        }

        #endregion BUTTONS SoC (Seperate of Concerns)

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

        #region SELECTED ITEM CHANGED
        /// <summary>
        /// Handles the event triggered when the 'Is Admin' checkbox state changes.
        /// </summary>
        /// <param name="sender">The source of the event (the CheckBox).</param>
        /// <param name="e">The event data (state change of the checkbox).</param>
        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            isAdmin = chkIsAdmin.Checked;
        }

        /// <summary>
        /// Handles the event triggered when the 'Is The One' checkbox state changes.
        /// Toggles the 'IsTheOne' property based on the checkbox state.
        /// If the checkbox is checked and the selected user is an admin, marks the current user as 'The One'.
        /// If the checkbox is unchecked or the selected user is not an admin, the 'IsTheOne' property is set to false.
        /// Sets flag 'ChkIsTheOneChanged' to true to indicate a change in the checkbox state.
        /// </summary>
        /// <param name="sender">The source of the event, typically the 'chkIsTheOne' checkbox.</param>
        /// <param name="e">The event data containing information about the state change of the checkbox.</param>
        private void chkIsTheOne_CheckedChanged(object sender, EventArgs e)
        {
            ChkIsTheOneChanged = true;

            if (AdminInterface.SelectedUserIsAdmin && chkIsTheOne.Checked)
            {
                IsTheOne = chkIsTheOne.Checked;
            }
            else
            {
                IsTheOne = false;
            }
        }


        /// <summary>
        /// Handles the selection change event for the ListBox in the admin interface.
        /// Triggers the appropriate selection handler in AdminInterface.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void ListBoxAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewFiles.Items.Clear();
            adminInterface.ListBoxAdmin_SelectedIndexChangedHandler();
        }

        private void listBoxLogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Controleer of een item geselecteerd is
            if (listViewFiles.SelectedItems.Count > 0)
            {
                // Verkrijg de tekst van het eerste geselecteerde item
                string selectedUserString = listViewFiles.SelectedItems[0].Text;

                if (!string.IsNullOrEmpty(selectedUserString))
                {
                    string selectedAlias = txtAlias.Text;
                    reportManager.ReportDisplay(selectedUserString, selectedAlias);
                }
            }
        }
        #endregion SELECTED ITEM CHANGED

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
                listViewFiles.Items.Clear();
                txtAliasReport.Clear();
                txtDateReport.Clear();
                rtxReport.Clear();
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

        #region DELETE FILE REPORT
        /// <summary>
        /// Deletes a report file associated with the specified alias.
        /// </summary>
        /// <param name="aliasToDelete">The alias of the user whose report file is to be deleted.</param>
        /// <remarks>
        /// This method checks if a file is selected in the ListView before attempting to delete it.
        /// Only The One admins have access to this functionality. A confirmation prompt is displayed 
        /// before the file is deleted. If the deletion succeeds, the file is removed from both the file system 
        /// and the ListView control.
        /// </remarks>
        /// <exception cref="IOException">Thrown if there is an issue accessing or deleting the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown if access to the file is denied.</exception>
        public void DeleteFileReport()
        {
            // Check if a file is selected in the ListView
            if (btnDeleteReport.Visible && listViewFiles.SelectedItems.Count > 0)
            {
                // Get the name of the selected file
                string selectedFile = listViewFiles.SelectedItems[0].Text;


                // Construct the full name of the report file
                string fileName = selectedFile + "_report.csv";
                Debug.WriteLine($"fileName: {fileName}");
                // Locate the directory where the report file resides
                string reportDirectory = FindCSVFiles.FindReportFile(txtAlias.Text, "report");
                Debug.WriteLine($"reportDirectory: {reportDirectory}");
                string fileToDelete = Path.Combine(reportDirectory, fileName);

                Debug.WriteLine($"fileToDelete: {fileToDelete}");
                // Show a confirmation dialog before deleting the file
                DialogResult dr = message.MessageConfirmDeleteFile(fileName);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        // Attempt to delete the file from the file system
                        File.Delete(fileToDelete);

                        // Remove the deleted file from the ListView
                        listViewFiles.Items.Remove(listViewFiles.SelectedItems[0]);

                        // Notify the user about the successful deletion
                        MessageBox.Show("File successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Display an error message if the deletion fails
                        MessageBox.Show($"An error occurred while deleting the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Notify the user to select a file before attempting to delete
                MessageBox.Show("Please select a file to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion DELETE FILE REPORT
    }
}
