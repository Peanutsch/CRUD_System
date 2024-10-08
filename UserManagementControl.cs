using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_System
{
    public partial class UserManagementControl : UserControl
    {
        Data _Data = new Data();
        /*
        string dataLogin = ;
        string dataUsers = ;
        */

        public UserManagementControl()
        {
            InitializeComponent();
        }

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
            var loginLines = File.ReadAllLines("data_login.csv");

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
            string newDataLogin = $"{txtAlias},{txtPassword.Text},{chkIsAdmin.Checked}";

            // data_users: NAME, SURNAME, ALIAS, ADRESS, ZIPCODE, CITY, EMAIL ADRESS
            string newDataUsers = $"{txtName.Text},{txtSurname.Text},{txtAlias},{txtEmail.Text},{txtAddress.Text},{txtCity.Text}";

            // Append to the CSV files
            File.AppendAllText("data_login.csv", newDataLogin + Environment.NewLine);
            File.AppendAllText("data_users.csv", newDataUsers + Environment.NewLine);

            MessageBox.Show("User added successfully!");
        }

        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        private void LoadUserData()
        {
            var lines = File.ReadAllLines("data_users.csv");

            foreach (var line in lines.Skip(1)) // Skip the header
            {
                var userDetails = line.Split(',');
                listBoxUsers.Items.Add(userDetails[0]); // Add names to the list
            }
        }

        /// <summary>
        /// Handles the event when a user is selected from the list box.
        /// Displays the user's details in the respective text fields.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedUser = listBoxUsers.SelectedItem!.ToString();
            var lines = File.ReadAllLines("data_users.csv");

            foreach (var line in lines.Skip(1))
            {
                var userDetails = line.Split(',');
                if (userDetails[0] == selectedUser)
                {
                    txtName.Text = userDetails[0];
                    txtSurname.Text = userDetails[1]; // Remember to fill in the surname
                    txtPassword.Text = userDetails[2]; // Correct index for password
                    chkIsAdmin.Checked = userDetails[3] == "true"; // Correct index for admin status
                    txtEmail.Text = userDetails[4]; // Correct index for email
                    // etc. for address, city
                }
            }
        }

        /// <summary>
        /// Handles the click event to update an existing user.
        /// Updates user details in both data_login.csv and data_users.csv.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            // Read lines from data_login.csv and data_users.csv
            var loginLines = File.ReadAllLines("data_login.csv").ToList();
            var userLines = File.ReadAllLines("data_users.csv").ToList();

            // Loop through each line of both files and update
            for (int i = 0; i < userLines.Count; i++)
            {
                var userDetails = userLines[i].Split(','); // User details in data_users.csv

                if (userDetails[0] == txtName.Text) // Search by name in data_users.csv
                {
                    // Update data_users.csv
                    userLines[i] = $"{txtName.Text},{txtSurname.Text},{txtEmail.Text},{txtAddress.Text},{txtCity.Text}";

                    // Find corresponding line in data_login.csv and update password and admin status
                    for (int j = 0; j < loginLines.Count; j++)
                    {
                        var loginDetails = loginLines[j].Split(','); // Login details in data_login.csv

                        if (loginDetails[0] == txtName.Text) // Search by name in data_login.csv
                        {
                            loginLines[j] = $"{txtName.Text},{txtPassword.Text},{chkIsAdmin.Checked}";
                            break; // Stop searching once a match is found and updated
                        }
                    }

                    // Write updated data back to both files
                    File.WriteAllLines("data_users.csv", userLines);
                    File.WriteAllLines("data_login.csv", loginLines);

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
            var userLines = File.ReadAllLines("data_users.csv").ToList();

            // Read all lines from data_login.csv
            var loginLines = File.ReadAllLines("data_login.csv").ToList();

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
            File.WriteAllLines("data_users.csv", userLines);

            // Remove the user from data_login.csv using the alias
            loginLines = loginLines.Where(line => !line.StartsWith(aliasToDelete)).ToList(); // Filter out the user by alias
            File.WriteAllLines("data_login.csv", loginLines);

            MessageBox.Show("User deleted successfully!");
        }
    }
}
