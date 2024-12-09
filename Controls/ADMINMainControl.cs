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
        readonly FilePaths path = new FilePaths();

        readonly AdminInterface adminInterface;
        readonly AccountManager accountManager = new AccountManager();
        readonly ProfileManager profileManager = new ProfileManager();
        readonly FormInteractionHandler interactionHandler = new FormInteractionHandler();
        readonly RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        private ReportManager reportManager;

        bool isAdmin;
        readonly bool onlineStatus = false;
        readonly bool isSick = false;
        bool editMode = false;

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
                profileManager.UpdateUserDetails(userLines, loginLines, userIndex, loginIndex, txtName.Text, txtSurname.Text, txtAlias.Text, txtAddress.Text, txtZIPCode.Text, txtCity.Text, txtEmail.Text, txtPhonenumber.Text, isAdmin, onlineStatus, isSick);
            }
            editMode = false; // Close editMode
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
                AdminMainControl adminControl = new AdminMainControl();
                DataCache cache = new DataCache();
                // Deleting user from files
                profileManager.DeleteUser(txtAlias.Text);

                // Empty TextBoxes and reload ListBox
                adminInterface.EmptyTextBoxesAdmin();
                adminInterface.ReloadListBoxAdmin(-1);

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
            adminInterface.ReloadListBoxAdmin(-1);
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
                adminInterface.ReloadListBoxAdmin(userIndex);

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

        /*
        /// <summary>
        /// Handles the double-click event for the ListView displaying files.
        /// </summary>
        /// <param name="sender">The source of the event (typically the ListView).</param>
        /// <param name="e">The event arguments.</param>
        public void listViewFiles_DoubleClick(object sender, EventArgs e)
        {
            ListViewFiles listViewFiles = new ListViewFiles(this);

            listViewFiles.HandleDoubleClick(filePath =>
            {
                // Perform decryption of the selected file
                try
                {
                    if (File.Exists(filePath))
                    {
                        EncryptionManager.DecryptFile(filePath);  // Decrypt the specific file
                    }
                    else
                    {
                        Debug.WriteLine("The selected file does not exist.");
                        MessageBox.Show("The selected file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error decrypting file: {ex.Message}");
                    MessageBox.Show($"Error decrypting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Read the content of the decrypted file
                string reportContent = File.ReadAllText(filePath);
                string selectedAlias = txtAlias.Text;

                // Open the ShowReportForm via FormInteractionHandler
                interactionHandler.Open_ShowReportForm(this, reportContent, selectedAlias); // Pass the current UserControl (e.g., parentControl)

                // After viewing, re-encrypt the file
                try
                {
                    EncryptionManager.EncryptFile(filePath); // Encrypt the file again after viewing
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error re-encrypting file: {ex.Message}");
                    MessageBox.Show($"Error re-encrypting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }
        */

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

                Debug.WriteLine($"btnMakeReport IsRepor: {adminInterface.IsReport}");

                adminInterface.TextBoxesReportConfig();
            },
            () => message.MessageInvalidNoUserSelected());
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            ReportManager reportManager = new ReportManager(this);
            reportManager.btnSaveReport();
        }
        #endregion BUTTONS SoC (Seperate of Concerns)

        #region KEY HANDLERS
        /// <summary>
        /// Handles the KeyPress event for the txtPhonenumber textbox.
        /// Allows only numeric digits, the '+' and '-' characters, and the Backspace key.
        /// Suppresses any other key inputs to ensure only valid phone number characters are entered.
        /// </summary>
        /// <param name="sender">The source of the event, typically the TxtPhonenumber textbox.</param>
        /// <param name="e">The KeyPressEventArgs containing the event data.</param>
        public void TxtPhonenumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits, the '+' and '-' characters, and Backspace
            if (!char.IsDigit(e.KeyChar)        // Check if the key is a digit (0-9)
                && e.KeyChar != '+'             // Allow the '+' character
                && e.KeyChar != '-'             // Allow the '-' character
                && e.KeyChar != (char)8)        // Allow Backspace (ASCII code 8)
            {
                e.Handled = true; // Prevent the key from being processed further
            }
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
                && !e.Control && !e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }
        #endregion KEY HANDLERS

        #region SELECTED ITEM CHANGED
        /// <summary>
        /// Handles the event when the 'Is Admin' checkbox state changes.
        /// Marks the current user as an admin when the checkbox is checked.
        /// </summary>
        /// <param name="sender">The source of the event (the CheckBox).</param>
        /// <param name="e">The event data (checkbox change).</param>
        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            isAdmin = chkIsAdmin.Checked;
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

            /*
            string selectedUserString = listViewFiles.SelectedItems[0].Text;
            string selectedAlias = txtAlias.Text;

            Debug.WriteLine($"ListBoxAdmin_SelectedIndexChanged> isSelectedAlias = {selectedAlias}");

            reportManager.ReportHandler(selectedUserString, selectedAlias);
            */
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
                    Debug.WriteLine($"listViewFiles_SelectedIndexChanged> isSelectedAlias = {selectedAlias}");
                    reportManager.ReportDisplay(selectedUserString, selectedAlias);
                }
            }
            else
            {
                Debug.WriteLine("SelectedIndex> Geen item geselecteerd.");
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
