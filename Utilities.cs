using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUD_System
{
    internal class Utilities
    {
        #region PROPERTIES
        readonly string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        readonly string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");
        readonly string logAction = Path.Combine(RootPath.GetRootPath(), @"data\log.csv");

        private ADMINMainControl adminMainControl = new ADMINMainControl();
        
        private Interface _interface;
        private UserDetails _currentUserDetails;
        private ADMINMainForm _mainForm;

        bool editMode = false;
        bool userSelected = false;
        bool isAdmin = false;

        #region Initialize DateTime for logging
        LogActions log = new LogActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion Initialize DateTime
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public Utilities(Interface userInterface, ADMINMainForm mainForm)
        {
            _interface = userInterface;
            _currentUserDetails = new UserDetails(new string[9]); // Initialiseer een lege gebruiker
            _mainForm = mainForm;
        }
        #endregion CONSTRUCTOR

        #region INITIALIZATIONS
        public void ToggleEditMode(bool editMode)
        {
            _interface.ToggleEditMode();
        }

        public void LoadUserDetailsFromInterface()
        {
            // Verplaats gegevens van de UI naar UserDetails
            _currentUserDetails = _interface.ExtractUserDetails();
        }

        public void LoadInterfaceFromUserDetails(UserDetails userDetails)
        {
            // Verplaats gegevens van UserDetails naar de UI
            _interface.PopulateFields(userDetails);
        }
        #endregion INITIALIZATIONS

        #region HANDLERS
        /// <summary>
        /// Finds the index of a user in the CSV data by alias.
        /// Returns -1 if the alias is not found.
        /// </summary>
        /// <param name="userLines">The list of lines from data_users.csv.</param>
        /// <param name="alias">The alias to search for.</param>
        /// <returns>The index of the user, or -1 if not found.</returns>
        public int FindUserIndexByAlias(List<string> userLines, List<string> loginLines, string alias)
        {
            for (int index = 0; index < userLines.Count; index++)
            {
                var userDetails = userLines[index].Split(',');
                var loginDetails = loginLines[index].Split(",");
                if (userDetails[2] == alias && loginDetails[0] == alias)
                {
                    return index;
                }
            }

            // Alias not found
            MessageBox.Show("User not found");
            return -1;
        }

        /// <summary>
        /// Updates the user details at the specified index in the CSV data.
        /// Writes the updated details back to data_users.csv.
        /// </summary>
        /// <param name="userLines">The list of lines from data_users.csv.</param>
        /// <param name="userIndex">The index of the user to update.</param>
        public void UpdateUserDetails(List<string> userLines, int userIndex)
        {
            string currentUserDetails = userLines[userIndex].Trim();

            //userLines[userIndex] = $"{txtName.Text},{txtSurname.Text},{txtAlias.Text},{txtAddress.Text},{txtZIPCode.Text.ToUpper()},{txtCity.Text},{txtEmail.Text},{txtPhonenumber.Text}";
            userLines[userIndex] = $"{_currentUserDetails.Name},{_currentUserDetails.Surname},{_currentUserDetails.Alias},{_currentUserDetails.Address}," +
                                   $"{_currentUserDetails.ZIPCode.ToUpper()},{_currentUserDetails.City},{_currentUserDetails.Email},{_currentUserDetails.PhoneNumber}";

            File.WriteAllLines(dataUsers, userLines); // Write updated data back to data_users.csv
            Debug.WriteLine($"After Update: {userLines[userIndex]}");
        }

        /// <summary>
        /// Updates the login details at the specified index in the CSV data.
        /// Writes the updated details back to data_login.csv.
        /// </summary>
        /// <param name="loginLines">The list of lines from data_users.csv.</param>
        /// <param name="userIndex">The index of the user to update.</param>
        public void UpdateUserLogin(List<string> loginLines, int userIndex)
        {
            var loginDetails = loginLines[userIndex].Split(',');

            // When need to keep current data on indexes
            string currentAlias = loginDetails[0];

            loginLines[userIndex] = $"{currentAlias},{loginDetails[1]},{isAdmin}";

            File.WriteAllLines(dataLogin, loginLines); // Write updated data back to data_login.csv

            Debug.WriteLine($"After Update: {loginLines[userIndex]}");
        }

        /// <summary>
        /// Ignores BTN_click action when no user is selected 
        /// (btnEditUserDetails_Click, btnDeleteUser_Click, btnGeneratePSW_Click)
        /// </summary>
        public void PerformActionIfUserSelected(Action action)
        {
            if (userSelected)
            {
                action(); // Execute the action if a user is selected
            }
            else
            {
                MessageBox.Show("Please select a user first."); // Feedback if no user is selected
            }
        }

        /// <summary>
        /// Hides MainForm, Opens CreateForm
        /// </summary>
        public void OpenCreateForm()
        {
            // Stays iN ADMINMainControl
        }
        #endregion HANDLERS

        #region TASKS
        /// <summary>
        /// Method for save changes user details
        /// </summary>
        public void SaveEditUserDetails()
        {
            LoadUserDetailsFromInterface();

            var currentUser = LoginForm.CurrentUser;

            // Read lines from data_users.csv
            var userLines = File.ReadAllLines(dataUsers).ToList();
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            // Find user index
            int userIndex = FindUserIndexByAlias(userLines, loginLines, _currentUserDetails.Alias);
            int loginIndex = FindUserIndexByAlias(userLines, loginLines, _currentUserDetails.Alias);

            if (userIndex >= 0)
            {
                var userDetails = userLines[userIndex].Split(',');
                var loginDetails = loginLines[userIndex].Split(",");

                // MessageBox YesNo to confirm changes
                MessageBoxes messageBoxes = new MessageBoxes();
                DialogResult dr = messageBoxes.MessageBoxConfirmToSAVEChanges(userDetails[2]);

                if (dr != DialogResult.Yes)
                {
                    return;
                }
                UpdateUserDetails(userLines, userIndex); // Save changes to data_users.csv
                UpdateUserLogin(loginLines, loginIndex); // Save changes to data_loging.csv

                if (currentUser != null)
                {
                    Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Edited details from user [{userDetails[2].ToUpper()}]");

                    string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Edited details from user [{userDetails[2].ToUpper()}]";
                    File.AppendAllText(logAction, newLog + Environment.NewLine);
                }
                else
                {
                    Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [UNKNOWN]: Edited details from user [{userDetails[2].ToUpper()}]");

                    string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},[UNKNOWN],Edited details from user [{userDetails[2].ToUpper()}]";
                    File.AppendAllText(logAction, newLog + Environment.NewLine);
                }

                MessageBoxes message = new MessageBoxes();
                message.MessageUpdateSucces();

                EmptyTextBoxes(); // Clear textboxes
                FillTextboxes(userDetails); // Reload txtboxes
                ReloadListBoxAdmin(userIndex); // Reload interface
            }
            else
            {
                // Close editMode
                editMode = false;
            }
        }

        public void GenerateNewPassword()
        {
            var currentUser = LoginForm.CurrentUser;

            // Read lines from data_users.csv
            var userLines = File.ReadAllLines(dataUsers).ToList();
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            // Find user index
            int userIndex = FindUserIndexByAlias(userLines, loginLines, _currentUserDetails.Alias);
            int loginIndex = FindUserIndexByAlias(userLines, loginLines, _currentUserDetails.Alias);
            var loginDetails = loginLines[loginIndex].Split(",");

            MessageBoxes messageConfirmSave = new MessageBoxes();
            DialogResult dr = messageConfirmSave.MessageBoxConfirmToSAVEPassword(loginDetails[0]);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            string generatedPassword = PasswordManager.GenerateUserPassword();
            //txtPassword.Text = generatedPassword;
            //_interface.Password = generatedPassword;

            loginLines[userIndex] = $"{loginDetails[0]},{generatedPassword},{isAdmin}";

            UpdateUserLogin(loginLines, loginIndex);

            MessageBoxes messageSucces = new MessageBoxes();
            messageSucces.MessageUpdateSucces();
            if (currentUser != null)
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}] Changed password for [{loginDetails[0].ToUpper()}]");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Changed password for user [{loginDetails[0].ToUpper()}]";
                File.AppendAllText(logAction, newLog + Environment.NewLine);
            }
            else
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [UNKNOWN] Changed password for [{loginDetails[0].ToUpper()}]");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},[UNKNOWN],Changed password for user [{loginDetails[0].ToUpper()}]";
                File.AppendAllText(logAction, newLog + Environment.NewLine);
            }
        }

        /// <summary>
        /// Method for deleting user from data_users.csv and data_log.csv
        /// </summary>
        private void DeleteUser()
        {
            var currentUser = LoginForm.CurrentUser;

            // Read lines from data_users.csv and data_login.csv
            var userLines = File.ReadAllLines(dataUsers).ToList();
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            int userIndex = FindUserIndexByAlias(userLines, loginLines, _currentUserDetails.Alias);

            // Get alias to delete from the selected user
            string aliasToDelete = _currentUserDetails.Alias;

            // MessageBox to confirm task
            MessageBoxes messageBoxes = new MessageBoxes();
            DialogResult dr = messageBoxes.MessageBoxConfirmToDELETE(aliasToDelete);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            // Remove the user from data_users.csv by alias
            userLines = userLines.Where(line =>
                                        !line.Split(',')[2].Trim().Equals(aliasToDelete,
                                        StringComparison.OrdinalIgnoreCase)).ToList();
            File.WriteAllLines(dataUsers, userLines);

            // Remove the user from data_login.csv using the alias
            loginLines = loginLines.Where(line =>
                                            !line.Split(',')[0].Trim().Equals(aliasToDelete,
                                            StringComparison.OrdinalIgnoreCase)).ToList();
            File.WriteAllLines(dataLogin, loginLines);

            if (currentUser != null)
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Deleted user [{aliasToDelete.ToUpper()}]");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Deleted user [{aliasToDelete.ToUpper()}]";
                File.AppendAllText(logAction, newLog + Environment.NewLine);
            }
            else
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [UNKNOWN]: Deleted user [{aliasToDelete.ToUpper()}]");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},[UNKNOWN],Deleted user [{aliasToDelete.ToUpper()}]";
                File.AppendAllText(logAction, newLog + Environment.NewLine);
            }

            messageBoxes.MessageDeleteSucces(); // Show MessageBox Delete Succes
            ReloadListBoxAdmin(userIndex);
            EmptyTextBoxes();
        }
        #endregion TASKS

        #region SETUP INTERFACE
        /// <summary>
        /// Reloads the user interface after saving changes.
        /// </summary>
        /// <param name="userIndex">The index of the updated user.</param>
        public void ReloadListBoxAdmin(int userIndex)
        {
            if (userIndex >= 0 && userIndex < adminMainControl.listBoxAdmin.Items.Count)
            {
                adminMainControl.listBoxAdmin.Refresh();
            }

            // Clear and reload listbox
            adminMainControl.listBoxAdmin.Items.Clear();
            adminMainControl.LoadUserDataListBox();

            // Reset editMode to false after saving and reload interface
            editMode = false;
            InterfaceEditMode(editMode);
        }

        public void EmptyTextBoxes()
        {
            // Refill textboxes with empty values
            _currentUserDetails.Name = string.Empty;
            _currentUserDetails.Surname = string.Empty;
            _currentUserDetails.Alias = string.Empty;
            _currentUserDetails.Address = string.Empty;
            _currentUserDetails.ZIPCode = string.Empty;
            _currentUserDetails.City = string.Empty;
            _currentUserDetails.Email = string.Empty;
            _currentUserDetails.PhoneNumber = string.Empty;
        }

        public void FillTextboxes(string[] userDetailsArray)
        {
            // Initialize the UserDetails object with the array of user details
            UserDetails userDetails = new UserDetails(userDetailsArray);

            // Populate the text fields with the details of the selected user
            _currentUserDetails.Name = userDetails.Name;
            _currentUserDetails.Surname = userDetails.Surname;
            _currentUserDetails.Alias = userDetails.Alias;
            _currentUserDetails.Address = userDetails.Address;
            _currentUserDetails.ZIPCode = userDetails.ZIPCode;
            _currentUserDetails.City = userDetails.City;
            _currentUserDetails.Email = userDetails.Email;
            _currentUserDetails.PhoneNumber = userDetails.PhoneNumber;
        }

        public void InterfaceEditMode(bool editMode)
        {
            // Use toggle to keep 1 button for Edit and Cancel
            _interface.btnEditUserDetails.Text = editMode ? _interface.btnEditUserDetails.Text = "Cancel" : _interface.btnEditUserDetails.Text = "Edit User";

            // Indication Edit mode is Enabled in Controlfield: color.Orange
            _mainForm.BackColor = editMode ? Color.Orange : SystemColors.ActiveCaption;

            // Manage btnUpdateUser
            _interface.btnSaveEditUserDetails.Visible = editMode ? true : false;
            _interface.btnSaveEditUserDetails.BackColor = Color.LightGreen;

            // Manage CheckBox isAdmin
            _interface.chkIsAdmin.Visible = editMode ? true : false;
            _interface.chkIsAdmin.Enabled = editMode ? true : false;

            // Manage TextBoxes and buttons
            _interface.Name.Enabled = editMode ? true : false;
            _interface.Surname.Enabled = editMode ? true : false;
            _interface.Admin.Enabled = editMode ? true : false;
            _interface.Address.Enabled = editMode ? true : false;
            _interface.ZIPCode.Enabled = editMode ? true : false;
            _interface.City.Enabled = editMode ? true : false;
            _interface.Email.Enabled = editMode ? true : false;
            _interface.Phone.Enabled = editMode ? true : false;

            _interface.btnCreateUser.Visible = editMode ? false : true;
            _interface.btnCreateUser.Enabled = editMode ? false : true;
            _interface.btnDeleteUser.Visible = editMode ? false : true;
            _interface.btnDeleteUser.Enabled = editMode ? false : true;
            _interface.btnGeneratePSW.Visible = editMode ? false : true;
            _interface.btnGeneratePSW.Enabled = editMode ? false : true;

            // Manage status ListBox
            adminMainControl.listBoxAdmin.Enabled = editMode ? false : true;
        }
        #endregion SETUP INTERFACE
    }
}
