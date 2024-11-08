﻿using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Models;
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
    /// <summary>
    /// Class for handling setup/layout Admin interface
    /// </summary>
    public class AdminInterface
    {
        #region PROPERTIES
        /// <summary>
        /// Indicates whether the interface is in edit mode.
        /// </summary>
        public bool EditMode { get; set; }

        readonly FilePaths path = new FilePaths();
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
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        public void LoadDetailsListBox()
        {
            var lines = File.ReadAllLines(path.UserFilePath);

            adminControl.listBoxAdmin.Items.Clear();

            foreach (var line in lines.Skip(2)) // Skip Header and details Admin
            {
                // Split each line into an array of user details
                var userDetailsArray = line.Split(',');

                // Create a UserDetails object using the array
                UserDetails userDetails = new UserDetails(userDetailsArray);

                // Use the UserDetails properties to format the string for the listBox
                string listItem = $"{userDetails.Name} {userDetails.Surname} ({userDetails.Alias}) | {userDetails.Email} | {userDetails.PhoneNumber}";

                // Add the formatted string to the listBox
                adminControl.listBoxAdmin.Items.Add(listItem);
            }
        }

        /// <summary>
        /// Reloads the user list box after making changes, refreshing the interface display.
        /// </summary>
        /// <param name="userIndex">The index of the updated user.</param>
        public void ReloadListBoxAdmin(int userIndex)
        {
            if (userIndex >= 0 && userIndex < adminControl.listBoxAdmin.Items.Count)
            {
                adminControl.Refresh();
            }

            // Clear and reload listbox
            adminControl.listBoxAdmin.Items.Clear();
            LoadDetailsListBox();

            // Reset editMode to false after saving and reload interface
            InterfaceEditModeAmin();
        }

        /// <summary>
        /// Handles the event when a user is selected in the list box, filling the details for the selected user in the textboxes.
        /// </summary>
        public void ListBoxAdmin_SelectedIndexChangedHandler()
        {
            // Get the selected user from the ListBox; ignore clicks on empty line in listBox
            if (adminControl.listBoxAdmin.SelectedItem is string selectedUserString && !string.IsNullOrEmpty(selectedUserString))
            {
                // Set UserSelected on true
                adminControl.InteractionHandler.UserSelected = true; // Pass bool true to InterActionHandler

                // Extract the alias from the selected text (in the format: "Name Surname (Alias)")
                string selectedAlias = selectedUserString.Split('(', ')')[1]; // Extract the alias between parentheses

                // Read user details
                var userDetailsArray = File.ReadAllLines(path.UserFilePath)
                                      .Skip(2)
                                      .Select(line => line.Split(','))
                                      .FirstOrDefault(details => details[2] == selectedAlias);

                if (userDetailsArray != null)
                {
                    FillTextboxesAdmin(userDetailsArray);
                }

                // Read the lines from data_login.csv
                var loginLines = File.ReadAllLines(path.LoginFilePath).Skip(2); // Skip the headers
                var loginDetailsList = loginLines.Select(line => line.Split(',')); // Split each line into details
                var loginDetails = loginDetailsList.FirstOrDefault(details => details[0] == selectedAlias); // Find the login details for the selected alias

                // Check if loginDetails is not null
                if (loginDetails != null)
                {
                    // Check if the admin status is true
                    if (loginDetails[2]! == "True") // Use '==' for comparison
                    {
                        // Show the admin label
                        adminControl.txtAdmin.Visible = true;
                        adminControl.chkIsAdmin.Checked = true; // checkbox chkAdmin checked
                    }
                    else
                    {
                        // Hide the admin label if not an admin
                        adminControl.txtAdmin.Visible = false;
                        adminControl.chkIsAdmin.Checked = false; // checkbox chkAdmin unchecked
                    }
                }
                else
                {
                    // Handle the case where loginDetails is null (optional)
                    adminControl.txtAdmin.Visible = false; // Hide the textbox if no details found
                }
            }
        }
        #endregion LISTBOX ADMIN

        #region EDIT MODE DISPLAY ADMIN
        /// <summary>
        /// Manages the interface display and controls based on edit mode status.
        /// </summary>
        public void InterfaceEditModeAmin()
        {
            Debug.WriteLine($"EditMode AdminInterface: {EditMode}");

            // Toggle Edit and Cancel button text
            adminControl.btnEditUserDetails.Text = EditMode ? "Cancel" : "Edit User";

            // Set background color based on EditMode
            adminControl.BackColor = EditMode ? Color.Orange : SystemColors.ActiveCaption;

            // Manage visibility and enablement of buttons and controls
            adminControl.btnSaveEditUserDetails.Visible = EditMode;
            adminControl.btnSaveEditUserDetails.BackColor = Color.LightGreen;
            adminControl.chkIsAdmin.Visible = EditMode;
            adminControl.chkIsAdmin.Enabled = EditMode;

            // Array of text fields to enable or disable in EditMode
            var textFields = new[]
            {
                adminControl.txtName,
                adminControl.txtSurname,
                adminControl.txtAdmin,
                adminControl.txtAddress,
                adminControl.txtZIPCode,
                adminControl.txtCity,
                adminControl.txtEmail,
                adminControl.txtPhonenumber
            };

            foreach (var field in textFields)
            {
                field.Enabled = EditMode;
            }

            // Hide or show other action buttons
            adminControl.btnCreateUser.Visible = !EditMode;
            adminControl.btnCreateUser.Enabled = !EditMode;
            adminControl.btnDeleteUser.Visible = !EditMode;
            adminControl.btnDeleteUser.Enabled = !EditMode;
            adminControl.btnGeneratePSW.Visible = !EditMode;
            adminControl.btnGeneratePSW.Enabled = !EditMode;

            // Enable or disable ListBox based on EditMode
            adminControl.listBoxAdmin.Enabled = !EditMode;
        }
        #endregion EDITMODE DISPLAY

        #region TEXTBOXES ADMIN
        /// <summary>
        /// Clears all textboxes in the interface, resetting their content.
        /// </summary>
        public void EmptyTextBoxesAdmin()
        {
            // Refill textboxes with empty values
            adminControl.txtName.Text = string.Empty;
            adminControl.txtSurname.Text = string.Empty;
            adminControl.txtAlias.Text = string.Empty;
            adminControl.txtAddress.Text = string.Empty;
            adminControl.txtZIPCode.Text = string.Empty;
            adminControl.txtCity.Text = string.Empty;
            adminControl.txtEmail.Text = string.Empty;
            adminControl.txtPhonenumber.Text = string.Empty;

            // Update button states to false
            adminControl.InteractionHandler.UserSelected = false;
        }

        /// <summary>
        /// Populates the textboxes with the details of a selected user.
        /// </summary>
        /// <param name="userDetailsArray">Array containing the user details.</param>
        public void FillTextboxesAdmin(string[] userDetailsArray)
        {
            // Initialize the UserDetails object with the array of user details
            UserDetails userDetails = new UserDetails(userDetailsArray);

            // Populate the text fields with the details of the selected user
            adminControl.txtName.Text = userDetails.Name;
            adminControl.txtSurname.Text = userDetails.Surname;
            adminControl.txtAlias.Text = userDetails.Alias;
            adminControl.txtAddress.Text = userDetails.Address;
            adminControl.txtZIPCode.Text = userDetails.ZIPCode;
            adminControl.txtCity.Text = userDetails.City;
            adminControl.txtEmail.Text = userDetails.Email;
            adminControl.txtPhonenumber.Text = userDetails.PhoneNumber;
        }
        #endregion TEXTBOXES ADMIN
    }
}
