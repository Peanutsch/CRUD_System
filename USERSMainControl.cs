﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace CRUD_System
{
    public partial class USERSMainControl : UserControl
    {
        #region PROPERTIES
        string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");

        ADMINMainControl adminMethods = new ADMINMainControl();

        bool editMode = false;
        bool userSelected = false;
        #endregion PROPERTIES

        public USERSMainControl()
        {
            InitializeComponent();

            LoadDetailsListBoxUser();
        }

        #region BUTTONS
        /// <summary>
        /// Handles the click event to save the edited user details.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnSaveEditUserDetails_Click(object sender, EventArgs e)
        {
           SaveEditUserDetails();
        }

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
        #endregion BUTTONS

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

        /// <summary>
        /// Method for save changes user details
        /// </summary>
        public void SaveEditUserDetails()
        {
            var currentUser = LoginForm.CurrentUser;

            if (currentUser != null)
            {
                // Read lines from data_users.csv
                var userLines = File.ReadAllLines(dataUsers).ToList();
                var loginLines = File.ReadAllLines(dataLogin).ToList();
                // Find user index
                int userIndex = adminMethods.FindUserIndexByAlias(userLines, loginLines, currentUser);
                int loginIndex = adminMethods.FindUserIndexByAlias(userLines, loginLines, currentUser);

                if (userIndex >= 0)
                {
                    var userDetails = userLines[userIndex].Split(',');
                    var loginDetails = loginLines[userIndex].Split(",");

                    // MessageBox YesNo to confirm changes
                    MessageBoxes messageBoxes = new MessageBoxes();
                    DialogResult dr = messageBoxes.MessageBoxConfirmToSAVE(userDetails[2]);

                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }

                    if (dr == DialogResult.Yes)
                    {
                        UpdateUserDetails(userLines, userIndex); // Save changes to data_users.csv

                        if (currentUser != null)
                        {
                            Debug.WriteLine($"[{currentUser.ToUpper()}]: Edited details from user [{userDetails[2]}]");
                        }
                        else
                        {
                            Debug.WriteLine($"[UNKNOWN]: Edited details from user [{userDetails[2]}]");
                        }

                        // MessageBox Succes
                        messageBoxes.MessageUpdateSucces();

                        FillTextboxes(userDetails); // Reload txtboxes
                        ReloadListBoxUser(userIndex); // Reload interface
                    }
                }
                else
                {
                    // Close editMode
                    editMode = false;
                }
            }
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
            Debug.WriteLine($"Current User Details: {currentUserDetails}");
            userLines[userIndex] = $"{txtName.Text},{txtSurname.Text},{txtAlias.Text},{txtAddress.Text},{txtZIPCode.Text.ToUpper()},{txtCity.Text},{txtEmail.Text},{txtPhonenumber.Text}";
            File.WriteAllLines(dataUsers, userLines); // Write updated data back to data_users.csv
            Debug.WriteLine($"After Update: {userLines[userIndex]}");
        }

        #region LISTBOX
        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        private void LoadDetailsListBoxUser()
        {
            var userLines = File.ReadAllLines(dataUsers).ToList();
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            var currentUser = LoginForm.CurrentUser;

            if (currentUser != null)
            {
                var userIndex = adminMethods.FindUserIndexByAlias(userLines, loginLines, currentUser);
                var userDetailsArray = userLines[userIndex].Split(',');

                UserDetails userDetails = new UserDetails(userDetailsArray);
                string listItem = $"{userDetails.Name} {userDetails.Surname} ({userDetails.Alias}) | {userDetails.Email} | {userDetails.PhoneNumber}";

                listBoxUser.Items.Add(listItem);
            }
            else
            {
                MessageBox.Show($"No user details found");
            }
        }

        public void ListBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected user from the ListBox; ignore clicks on empty line in listBox
            if (listBoxUser.SelectedItem is string selectedUserString && !string.IsNullOrEmpty(selectedUserString))
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
            }
        }

        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        public void LoadUserDetailsListBoxUser()
        {
            var lines = File.ReadAllLines(dataUsers);

            listBoxUser.Items.Clear();

            foreach (var line in lines.Skip(2)) // Skip Header and details Admin
            {
                // Split each line into an array of user details
                var userDetailsArray = line.Split(',');

                // Create a UserDetails object using the array
                UserDetails userDetails = new UserDetails(userDetailsArray);

                // Use the UserDetails properties to format the string for the listBox
                string listItem = $"{userDetails.Name} {userDetails.Surname} ({userDetails.Alias}) | {userDetails.Email} | {userDetails.PhoneNumber}";

                // Add the formatted string to the listBox
                listBoxUser.Items.Add(listItem);
            }
        }

        /// <summary>
        /// Reloads the user interface after saving changes.
        /// </summary>
        /// <param name="userIndex">The index of the updated user.</param>
        public void ReloadListBoxUser(int userIndex)
        {
            if (userIndex >= 0 && userIndex < listBoxUser.Items.Count)
            {
                this.Refresh();
            }

            // Clear and reload listbox
            listBoxUser.Items.Clear();
            LoadUserDetailsListBoxUser();

            // Reset editMode to false after saving and reload interface
            editMode = false;
            InterfaceEditMode();
        }
        #endregion LISTBOX

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

            // Manage TextBoxes
            txtName.Enabled = editMode ? true : false;
            txtSurname.Enabled = editMode ? true : false;
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
            listBoxUser.Enabled = editMode ? false : true;
        }
        #endregion INTERFACE
    }
}
