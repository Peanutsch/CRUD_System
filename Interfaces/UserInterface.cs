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
    public class UserInterface
    {
        #region PROPERTIES
        public bool EditMode
        {
            get; set;
        }

        FilePaths path = new FilePaths();

        readonly AccountManager repository = new AccountManager();
        readonly ProfileManager profileManager = new ProfileManager();
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
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        public void LoadDetailsListBoxThisUser()
        {
            // Read lines from data_users.csv and data_login.csv
            (var userLines, var loginLines) = profileManager.ReadUserAndLoginData();

            var currentUser = LoginHandler.CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                
                var userIndex = repository.FindUserIndexByAlias(userLines, loginLines, currentUser);
                var userDetailsArray = userLines[userIndex].Split(',');

                UserDetails userDetails = new UserDetails(userDetailsArray);
                string listItem = $"{userDetails.Name} {userDetails.Surname} ({userDetails.Alias}) | {userDetails.Email} | {userDetails.PhoneNumber}";

                userControl.listBoxUser.Items.Add(listItem);
            }
            else
            {
                MessageBox.Show($"No user details found");
            }

            userControl.listBoxUser.SelectedIndex = 0; // auto select user to fill textboxes
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

        #region INTERFACE USERS
        public void InterfaceEditModeUser()
        {
            // Toggle Edit and Cancel button text
            userControl.btnEditUserDetails.Text = EditMode ? "Cancel" : "Edit User";

            // Set background color based on EditMode
            userControl.BackColor = EditMode ? Color.Orange : SystemColors.ActiveCaption;

            // Manage visibility and enablement of buttons and controls
            userControl.btnSaveEditUserDetails.Visible = EditMode;
            userControl.btnSaveEditUserDetails.BackColor = Color.LightGreen;

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
        #endregion INTERFACE USERS

        #region TEXTBOXES
        public void FillTextboxes(string[] userDetailsArray)
        {
            // Initialize the UserDetails object with the array of user details
            UserDetails userDetails = new UserDetails(userDetailsArray);

            // Populate the text fields with the details of the selected user
            userControl.txtName.Text = userDetails.Name;
            userControl.txtSurname.Text = userDetails.Surname;
            userControl.txtAlias.Text = userDetails.Alias;
            userControl.txtAddress.Text = userDetails.Address;
            userControl.txtZIPCode.Text = userDetails.ZIPCode;
            userControl.txtCity.Text = userDetails.City;
            userControl.txtEmail.Text = userDetails.Email;
            userControl.txtPhonenumber.Text = userDetails.PhoneNumber;
        }
        #endregion TEXTBOXES
    }
}
