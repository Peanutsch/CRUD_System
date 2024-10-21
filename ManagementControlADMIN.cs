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

namespace CRUD_System
{
    public partial class ManagementControlADMIN : UserControl
    {
        #region PROPERTIES
        string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");


        Data _Data = new Data();

        bool editMode = false;
        bool userSelected = false;
        //bool isAdmin = false;
        #endregion

        #region Constructor
        public ManagementControlADMIN()
        {
            InitializeComponent();

            LoadUserData(); // Load data_users.csv for display in listbox
        }
        #endregion

        #region BUTTONS SoC (Seperate of Concerns)
        /// <summary>
        /// Handles the click event to toggle edit mode for the selected user in listBoxUsers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnEditUserDetails_Click(object sender, EventArgs e)
        {
            PerformActionIfUserSelected(() =>
            {
                // Toggle edit mode
                editMode = !editMode;
                InterfaceEditMode();
            });
        }

        /// <summary>
        /// Handles the click event to save the edited user details.
        /// Updates user details in data_users.csv if confirmed by the user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnSaveEditUserDetails_Click(object sender, EventArgs e)
        {
            SaveEditUserDetails();
        }

        /// <summary>
        /// Handles the click event to delete user from data_users.csv and data_login.csv
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            PerformActionIfUserSelected(() =>
            {
                DeleteUser(); // Perform delete action only if a user is selected
            });
        }

        /// <summary>
        /// Handles the click event to add a new user.
        /// Creates a new record in both data_login.csv and data_users.csv.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            CreateUser();
        }

        /// <summary>
        /// Handles click event to create a new password
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnGeneratePSW_Click(object sender, EventArgs e)
        {
            PerformActionIfUserSelected(() =>
            {
                string generatedPassword = PasswordManager.GenerateUserPassword();
                txtPassword.Text = generatedPassword;
            });
        }
        #endregion BUTTONS SoC (Seperate of Concerns)


        #region METHODS MANAGEMENT CONTROLADMIN
        /// <summary>
        /// Ignores click action when no user is selected.
        /// </summary>
        private void PerformActionIfUserSelected(Action action)
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
        /// Finds the index of a user in the CSV data by alias.
        /// Returns -1 if the alias is not found.
        /// </summary>
        /// <param name="userLines">The list of lines from data_users.csv.</param>
        /// <param name="alias">The alias to search for.</param>
        /// <returns>The index of the user, or -1 if not found.</returns>
        public int FindUserIndexByAlias(List<string> userLines, string alias)
        {
            for (int index = 0; index < userLines.Count; index++)
            {
                var userDetails = userLines[index].Split(',');
                if (userDetails[2] == alias)
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
        public void UpdateUser(List<string> userLines, int userIndex)
        {
            // Update data_users.csv
            userLines[userIndex] = $"{txtName.Text},{txtSurname.Text},{txtAlias.Text},{txtAddress.Text},{txtZIPCode.Text.ToUpper()},{txtCity.Text},{txtEmail.Text},{txtPhonenumber.Text}";
            // Write updated data back to data_users.csv
            File.WriteAllLines(dataUsers, userLines);
            // Confirm successful update
            MessageBoxes messageBoxes = new MessageBoxes();
            messageBoxes.MessageSucces();
        }


        /// <summary>
        /// Method for creating new user.
        /// Writes new user details in data_users.csv and data_login.csv.
        /// </summary>
        public void CreateUser()
        {
            // Create a new record
            string txtAlias = CreateTXTAlias();

            // data_login: ALIAS, PASSWORD, ADMIN
            string newDataLogin = $"{txtAlias},{txtPassword.Text}";

            // data_users: NAME, SURNAME, ALIAS, ADRESS, ZIPCODE, CITY, EMAIL ADRESS
            string newDataUsers = $"{txtName.Text},{txtSurname.Text},{txtAlias},{txtEmail.Text},{txtAddress.Text},{txtCity.Text}";

            // Append to the CSV files
            File.AppendAllText(dataLogin, newDataLogin + Environment.NewLine);
            File.AppendAllText(dataUsers, newDataUsers + Environment.NewLine);

            MessageBox.Show("User added successfully!");
        }

        /// <summary>
        /// Method for save changes user details
        /// </summary>
        public void SaveEditUserDetails()
        {
            // Read lines from data_users.csv
            var userLines = File.ReadAllLines(dataUsers).ToList();
            // Find user index
            int userIndex = FindUserIndexByAlias(userLines, txtAlias.Text);

            if (userIndex >= 0)
            {
                var userDetails = userLines[userIndex].Split(',');

                // MessageBox YesNo to confirm changes
                MessageBoxes messageBoxes = new MessageBoxes();
                DialogResult dr = messageBoxes.MessageBoxConfirmToSAVE(userDetails[2]);

                if (dr == DialogResult.Yes)
                {

                    UpdateUser(userLines, userIndex); // Save changes to data_users.csv

                    ClearTextBoxes(); // Clear textboxes
                    ReloadListBoxUsers(userIndex); // Reload interface
                    FillTextboxes(userDetails);
                }

                if (dr == DialogResult.No)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Method for deleting user from data_users.csv and data_log.csv
        /// </summary>
        private void DeleteUser()
        {
            // Read lines from data_users.csv and data_login.csv
            var userLines = File.ReadAllLines(dataUsers).ToList();
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            int userIndex = FindUserIndexByAlias(userLines, txtAlias.Text);

            // Get alias to delete from the selected user
            string aliasToDelete = txtAlias.Text;

            // MessageBox to confirm task
            MessageBoxes messageBoxes = new MessageBoxes();
            DialogResult dr = messageBoxes.MessageBoxConfirmToDELETE(aliasToDelete);

            if (dr == DialogResult.Yes)
            {
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

                messageBoxes.MessageSucces(); // Show MessageBox Succes
                ReloadListBoxUsers(userIndex);
                ClearTextBoxes();
            }
            if (dr != DialogResult.No)
            {
                return;
            }

        }

        /// <summary>
        /// Method for saving and sending (new) generated password to (new) user
        /// </summary>
        private void SaveEditPSW()
        {
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            // Find corresponding line in data_login.csv and update password and admin status
            for (int j = 0; j < loginLines.Count; j++)
            {
                var loginDetails = loginLines[j].Split(','); // Login details in data_login.csv

                if (loginDetails[0] == txtName.Text) // Search by name in data_login.csv
                {
                    loginLines[j] = $"{txtName.Text},{txtPassword.Text}";
                    break; // Stop searching once a match is found and updated
                }
            }
        }

        #region ALIAS
        /// <summary>
        /// Generates a unique alias for the user based on the first two letters of the first name
        /// and the last two letters of the surname, followed by a number that increments if the alias already exists.
        /// </summary>
        /// <returns>A unique alias as a string.</returns>
        private string CreateTXTAlias()
        {
            string txtAlias = txtName.Text.Substring(0, 2).ToLower() + txtSurname.Text.Substring(txtSurname.Text.Length - 2).ToLower();
            int counter = 1;
            string finalAlias = txtAlias + "001";

            // Check if the alias already exists and increment the number if necessary
            while (AliasExists(finalAlias))
            {
                counter++;
                string newNumber = counter.ToString("D3"); // Ensures it always has 3 digits
                finalAlias = txtAlias + newNumber;
            }

            return finalAlias;
        }


        /// <summary>
        /// Checks if the given alias already exists in the data_login.csv file.
        /// </summary>
        /// <param name="alias">The alias to check for existence.</param>
        /// <returns>True if the alias exists; otherwise, false.</returns>
        private bool AliasExists(string alias)
        {
            // Read all lines from data_login.csv
            var loginLines = File.ReadAllLines(dataLogin);

            // Check if the alias already exists
            foreach (var line in loginLines)
            {
                var loginDetails = line.Split(',');
                if (loginDetails[0] == alias)
                {
                    return true; // Alias already exists
                }
            }

            return false; // Alias does not exist
        }
        #endregion ALIAS

        #region LISTBOX
        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        private void LoadUserData()
        {
            var lines = File.ReadAllLines(dataUsers);

            foreach (var line in lines.Skip(2)) // Skip Header and details Admin
            {
                // Split each line into an array of user details
                var userDetailsArray = line.Split(',');

                // Create a UserDetails object using the array
                UserDetails userDetails = new UserDetails(userDetailsArray);

                // Use the UserDetails properties to format the string for the listBox
                string listItem = $"{userDetails.Name} {userDetails.Surname} ({userDetails.Alias}) | {userDetails.Email} | {userDetails.PhoneNumber}";

                // Add the formatted string to the listBox
                listBoxUsers.Items.Add(listItem);
            }
        }

        /// <summary>
        /// Handles the event when a user is selected from the list box.
        /// Displays the user's details in the respective text fields.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        /// <summary>
        /// Handles the event when a user is selected from the list box.
        /// Extracts the alias from the selected item.
        /// Retrieves the corresponding user details from both data_users.csv and data_login.csv. 
        /// The user's information is then displayed in the appropriate text fields.
        /// </summary>
        /// <param name="sender">The source of the event (the ListBox).</param>
        /// <param name="e">The event data (user selection).</param>
        private void ListBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected user from the ListBox; ignore clicks on empty line in listBox
            if (listBoxUsers.SelectedItem is string selectedUserString && !string.IsNullOrEmpty(selectedUserString))
            {
                // Set userSelected on true
                userSelected = true;

                // Extract the alias from the selected text (in the format: "Name Surname (Alias)")
                string selectedAlias = selectedUserString.Split('(', ')')[1]; // Extract the alias between parentheses

                // Read user details
                var userDetailsArray = File.ReadAllLines(dataUsers)
                                      .Skip(2)
                                      .Select(line => line.Split(','))
                                      .FirstOrDefault(details => details[2] == selectedAlias);

                if (userDetailsArray != null)
                {
                    FillTextboxes(userDetailsArray);
                }

                // Read the lines from data_login.csv
                var loginLines = File.ReadAllLines(dataLogin).Skip(2); // Skip the headers
                var loginDetailsList = loginLines.Select(line => line.Split(',')); // Split each line into details
                var loginDetails = loginDetailsList.FirstOrDefault(details => details[0] == selectedAlias); // Find the login details for the selected alias

                // Check if loginDetails is not null
                if (loginDetails != null)
                {
                    // Check if the admin status is true
                    if (loginDetails[2]! == "true") // Use '==' for comparison
                    {
                        // Show the admin label
                        txtAdmin.Visible = true;
                        chkIsAdmin.Checked = true; // checkbox chkAdmin checked
                    }
                    else
                    {
                        // Hide the admin label if not an admin
                        txtAdmin.Visible = false;
                        chkIsAdmin.Checked = false; // checkbox chkAdmin unchecked
                    }
                }
                else
                {
                    // Handle the case where loginDetails is null (optional)
                    txtAdmin.Visible = false; // Hide the textbox if no details found
                }
            }
        }

        /// <summary>
        /// Handles the event when the 'Is Admin' checkbox state changes.
        /// Marks the current user as an admin when the checkbox is checked.
        /// </summary>
        /// <param name="sender">The source of the event (the CheckBox).</param>
        /// <param name="e">The event data (checkbox change).</param>
        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            //isAdmin = true;
        }

        #endregion ALIAS
        #endregion METHODS MANAGEMENT CONTROLADMIN

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

            // Manage CheckBox isAdmin
            chkIsAdmin.Visible = editMode ? true : false;
            chkIsAdmin.Enabled = editMode ? true : false;

            // Manage TextBoxes
            txtName.Enabled = editMode ? true : false;
            txtSurname.Enabled = editMode ? true : false;
            txtAdmin.Enabled = editMode ? true : false;
            txtAddress.Enabled = editMode ? true : false;
            txtZIPCode.Enabled = editMode ? true : false;
            txtCity.Enabled = editMode ? true : false;
            txtEmail.Enabled = editMode ? true : false;
            txtPhonenumber.Enabled = editMode ? true : false;
            txtPassword.Enabled = editMode ? true : false;

            // Manage status visible and enable Buttons and CheckBox
            btnCreateUser.Visible = editMode ? false : true;
            btnCreateUser.Enabled = editMode ? false : true;
            btnDeleteUser.Visible = editMode ? false : true;
            btnDeleteUser.Enabled = editMode ? false : true;
            btnGeneratePSW.Visible = editMode ? false : true;
            btnGeneratePSW.Enabled = editMode ? false : true;
            txtPassword.Visible = editMode ? false : true;
            txtPassword.Enabled = editMode ? false : true;

            // Manage status ListBox
            listBoxUsers.Enabled = editMode ? false : true;
        }

        public void InterfaceDisplayMode()
        {
            // Use toggle to keep 1 button for Edit and Cancel
            btnEditUserDetails.Text = editMode ? btnEditUserDetails.Text = "Cancel" : btnEditUserDetails.Text = "Edit User";

            // Indication Edit mode is Enabled in Controlfield: color.Orange
            this.BackColor = editMode ? Color.Orange : SystemColors.ActiveCaption;

            // Manage btnUpdateUser
            btnSaveEditUserDetails.Visible = editMode ? true : false;
            btnSaveEditUserDetails.BackColor = Color.LightGreen;

            // Manage CheckBox isAdmin
            chkIsAdmin.Visible = editMode ? true : false;
            chkIsAdmin.Enabled = editMode ? true : false;

            // Manage TextBoxes
            txtName.Enabled = editMode ? true : false;
            txtSurname.Enabled = editMode ? true : false;
            txtAdmin.Enabled = editMode ? true : false;
            txtAddress.Enabled = editMode ? true : false;
            txtZIPCode.Enabled = editMode ? true : false;
            txtCity.Enabled = editMode ? true : false;
            txtEmail.Enabled = editMode ? true : false;
            txtPhonenumber.Enabled = editMode ? true : false;
            txtPassword.Enabled = editMode ? true : false;

            // Manage status visible and enable Buttons and CheckBox
            btnCreateUser.Visible = editMode ? false : true;
            btnCreateUser.Enabled = editMode ? false : true;
            btnDeleteUser.Visible = editMode ? false : true;
            btnDeleteUser.Enabled = editMode ? false : true;
            btnGeneratePSW.Visible = editMode ? false : true;
            btnGeneratePSW.Enabled = editMode ? false : true;
            txtPassword.Visible = editMode ? false : true;
            txtPassword.Enabled = editMode ? false : true;

            // Manage status enable ListBox
            listBoxUsers.Enabled = editMode ? false : true;
        }

        /// <summary>
        /// Reloads the user interface after saving changes.
        /// </summary>
        /// <param name="userIndex">The index of the updated user.</param>
        public void ReloadListBoxUsers(int userIndex)
        {
            if (userIndex >= 0 && userIndex < listBoxUsers.Items.Count)
            {
                this.Refresh();
            }

            // Clear and reload listbox
            listBoxUsers.Items.Clear();
            LoadUserData();

            // Reset editMode to false after saving and reload interface
            editMode = false;
            InterfaceEditMode();
        }

        public void ClearTextBoxes()
        {
            // Refill textboxes with empty values
            txtName.Text = string.Empty;
            txtSurname.Text = string.Empty;
            txtAlias.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtZIPCode.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhonenumber.Text = string.Empty;
        }

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
        #endregion INTERFACE
    }
}
