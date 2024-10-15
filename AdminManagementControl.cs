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
    public partial class AdminManagementControl : UserControl
    {
        string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");


        Data _Data = new Data();

        bool editMode = false;
        bool userSelected = false;
        //bool isAdmin = false;

        public AdminManagementControl()
        {
            InitializeComponent();

            LoadUserData(); // Load data_users.csv for display in listbox
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
        #endregion

        #region BUTTONS
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // No action when no user is selected in listBox
            if (userSelected != true)
            {
                return;
            }

            // Toggle editMode on and off
            editMode = !editMode;

            btnEdit.Text = editMode ? btnEdit.Text = "Cancel" : btnEdit.Text = "Edit User";
            
            // Indication Edit mode is Enabled in Controlfield: color.Orange
            this.BackColor = editMode ? Color.Orange : SystemColors.ActiveCaption;

            InterfaceEditMode();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the click event to add a new user.
        /// Creates a new record in both data_login.csv and data_users.csv.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnAddUser_Click(object sender, EventArgs e)
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
        /// Handles the click event to update an existing user.
        /// Updates user details in both data_login.csv and data_users.csv.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            // Read lines from data_login.csv and data_users.csv
            var loginLines = File.ReadAllLines(dataLogin).ToList();
            var userLines = File.ReadAllLines(dataUsers).ToList();

            // Loop through each line of both files and update
            for (int i = 0; i < userLines.Count; i++)
            {
                var userDetails = userLines[i].Split(','); // User details in data_users.csv

                if (userDetails[0] == txtName.Text) // Search by name in data_users.csv
                {
                    // NAME, SURNAME, ALIAS, ADRESS, ZIPCODE, CITY, EMAIL ADRESS, PHONENUMBER
                    // Update data_users.csv
                    userLines[i] = $"{txtName.Text},{txtSurname.Text},{txtAlias.Text},{txtAddress.Text},{txtZIPCode.Text},{txtCity.Text},{txtEmail.Text}.{txtPhonenumber.Text}";

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

                    // Write updated data back to both files
                    File.WriteAllLines(dataLogin, loginLines);
                    File.WriteAllLines(dataUsers, userLines);

                    // Confirm successful update
                    MessageBox.Show("User updated successfully!");
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the click event to delete a user.
        /// Removes the user from both data_users.csv and data_login.csv.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            // Read all lines from data_users.csv
            var userLines = File.ReadAllLines(dataUsers).ToList();

            // Read all lines from data_login.csv
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            // Find the alias for the selected user
            string aliasToDelete = string.Empty;
            foreach (var line in loginLines)
            {
                var loginDetails = line.Split(',');
                if (loginDetails[0] == txtName.Text) // Assuming txtName.Text contains the user's name
                {
                    aliasToDelete = loginDetails[0]; // Get the alias
                    break;
                }
            }

            // Remove the user from data_users.csv
            userLines = userLines.Where(line => !line.StartsWith(txtName.Text)).ToList(); // Filter out the selected user
            File.WriteAllLines(dataUsers, userLines);

            // Remove the user from data_login.csv using the alias
            loginLines = loginLines.Where(line => !line.StartsWith(aliasToDelete)).ToList(); // Filter out the user by alias
            File.WriteAllLines(dataLogin, loginLines);

            MessageBox.Show("User deleted successfully!");
        }

        private void btnGenPSW_Click(object sender, EventArgs e)
        {
            string generatedPassword = PasswordManager.GenerateUserPassword();
            txtPassword.Text = generatedPassword;
        }
        #endregion

        #region LISTBOX
        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        private void LoadUserData()
        {
            var lines = File.ReadAllLines(dataUsers);

            foreach (var line in lines.Skip(2)) // Skip Header and details Admin
            {
                var userDetails = line.Split(',');
                //                     userDetails[0] = Name  userDetails[1] = Surname     userDetails[2] = Alias
                //NAME, SURNAME, ALIAS, ADRESS, ZIPCODE, CITY, EMAIL ADRESS, PHONENUMBER
                string NAME = userDetails[0];
                string SURNAME = userDetails[1];
                string ALIAS = userDetails[2];
                string ADRESS = userDetails[3];
                string ZIPCODE = userDetails[4];
                string CITY = userDetails[5];
                string EMAIL = userDetails[6];
                string PHONE = userDetails[7];
                //listBoxUsers.Items.Add(userDetails[0] + " " + userDetails[1] + " " + "(" + userDetails[2] + ")"); // Add names to the list
                //listBoxUsers.Items.Add(NAME + " | " + SURNAME + " | " + "(" + ALIAS + ")" + " | " + ADRESS + " | " + 
                //                       ZIPCODE + " | " + CITY + " | " + EMAIL + " | " + PHONE); // Add names to the list
                listBoxUsers.Items.Add(NAME + " " + SURNAME + " " + "(" + ALIAS + ")" + " | " + EMAIL + " | " + PHONE); // Add names to the list
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
        /// Extracts the alias from the selected item and retrieves the corresponding 
        /// user details from both data_users.csv and data_login.csv. 
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
                var userDetails = File.ReadAllLines(dataUsers).Skip(2)
                    .Select(line => line.Split(',')).FirstOrDefault(details => details[2] == selectedAlias);

                if (userDetails != null)
                {
                    // Populate the text fields with the details of the selected user
                    txtName.Text = userDetails[0];
                    txtSurname.Text = userDetails[1];
                    txtAlias.Text = userDetails[2];
                    txtAddress.Text = userDetails[3];
                    txtZIPCode.Text = userDetails[4];
                    txtCity.Text = userDetails[5];
                    txtEmail.Text = userDetails[6];
                    txtPhonenumber.Text = userDetails[7];
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

        #endregion

        #region MODES
        public void InterfaceEditMode()
        {
            // Manage btnUpdateUser
            btnSaveEdit.Visible = editMode ? true : false;
            btnSaveEdit.BackColor = Color.LightGreen;

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
            btnGenPSW.Visible = editMode ? false : true;
            btnGenPSW.Enabled = editMode ? false : true;
            txtPassword.Visible = editMode ? false : true;
            txtPassword.Enabled = editMode ? false : true;

            // Manage status enable ListBox
            listBoxUsers.Enabled = editMode ? false : true;
        }

        public void DisplayMode()
        {

        }
        #endregion
    }
}
