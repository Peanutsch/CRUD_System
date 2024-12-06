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

            LoadListBoxLogs(alias);
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

        #region LISTBOX LOGS
        /// <summary>
        /// Loads the log entries of user (alias) into the ListBox, 
        /// sorting them in descending order by timestamp. Decrypts the log file for processing 
        /// and re-encrypts it afterward.
        /// </summary>
        /// <param name="alias">The alias of the user whose logs need to be loaded.</param>
        public void LoadListBoxLogs(string alias)
        {
            // Prepare the log file
            string? logFile = PrepareLogFile(alias);

            if (!string.IsNullOrEmpty(logFile))
            {
                // Parse log entries from the file into structured data
                var logEntries = ParseLogFile(logFile);

                // Step 3: Sort log entries by date/time in descending order
                var sortedEntries = SortLogEntriesDescending(logEntries);

                // Populate the ListBox with the sorted log entries
                PopulateListBox(sortedEntries);

                // Re-encrypt the log file after processing
                EncryptionManager.EncryptFile(logFile);
            }
            else
            {
                // Show a message if no log file is found
                MessageBox.Show("Log file not found.");
            }
        }

        /// <summary>
        /// Finds the log file for user alias and decrypts it if the file exists.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        /// <returns>The decrypted file path, or null if the file is not found.</returns>
        private string? PrepareLogFile(string alias)
        {
            // Find the file path for the user's logs
            string logFile = FindCSVFiles.FindCSVFile(alias, "logevents");

            // Check if the file exists
            if (File.Exists(logFile))
            {
                // Decrypt the file
                EncryptionManager.DecryptFile(logFile);
                return logFile;
            }

            // Return null if the file does not exist
            return null;
        }

        /// <summary>
        /// Parses the log file into a list of tuples containing the timestamp and the full log entry string.
        /// </summary>
        /// <param name="logFile">The path to the log file.</param>
        /// <returns>A list of log entries with timestamps for sorting.</returns>
        private List<Tuple<DateTime, string>> ParseLogFile(string logFile)
        {
            var logEntries = new List<Tuple<DateTime, string>>();

            // Read all lines from the file
            var lines = File.ReadAllLines(logFile);

            foreach (var line in lines)
            {
                // Split each line into parts
                var parts = line.Split(',');

                // Ensure the line has the expected number of parts
                if (parts.Length >= 4)
                {
                    string date = parts[0];         // Date dd-MM-yyyy
                    string time = parts[1];         // Time HH:mm:ss
                    string aliasInLog = parts[2];   // Alias
                    string logEvent = parts[3];     // Log event

                    // Combine date and time into a single DateTime object
                    if (DateTime.TryParse($"{date} {time}", out DateTime logDateTime))
                    {
                        // Add the parsed log entry as a tuple (timestamp, full entry string)
                        logEntries.Add(new Tuple<DateTime, string>(logDateTime, $"{date} {time} {aliasInLog} {logEvent}"));
                    }
                }
            }

            return logEntries;
        }

        /// <summary>
        /// Sorts the log entries by their timestamp in descending order (most recent first).
        /// </summary>
        /// <param name="logEntries">The list of log entries with timestamps.</param>
        /// <returns>A list of log entry strings sorted by timestamp.</returns>
        private List<string> SortLogEntriesDescending(List<Tuple<DateTime, string>> logEntries)
        {
            // Order entries by the DateTime component and return only the log strings
            return logEntries
                .OrderByDescending(entry => entry.Item1)
                .Select(entry => entry.Item2)
                .ToList();
        }

        /// <summary>
        /// Populates the ListBox control with sorted log entries.
        /// </summary>
        /// <param name="sortedEntries">The sorted list of log entry strings.</param>
        private void PopulateListBox(List<string> sortedEntries)
        {
            // Clear the ListBox before adding new items
            userControl.listBoxLogs.Items.Clear();

            // Add each log entry to the ListBox
            foreach (var entry in sortedEntries)
            {
                userControl.listBoxLogs.Items.Add(entry);

                // Check if the current entry contains "logged IN"
                if (entry.Contains("logged IN"))
                {
                    userControl.listBoxLogs.Items.Add("======="); // Extra line in listBoxLogs as divider
                }
            }
        }
        #endregion LISTBOX LOGS

        #region INTERFACE USERS
        /// <summary>
        /// Toggles the interface elements between edit mode and view mode for user details.
        /// Adjusts the button text, background color, visibility, and enablement of controls 
        /// based on the current edit mode status.
        /// </summary>
        public void InterfaceEditModeUser()
        {
            // Reload cache data
            cache.LoadDecryptedData();

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
