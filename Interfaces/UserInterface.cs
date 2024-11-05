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

        Repository repository = new Repository();
        USERSMainControl userControl = new USERSMainControl();

        #endregion PROPERTIES

        #region CONSTRUCTOR
        public UserInterface()
        {

        }
        #endregion CONSTRUCTOR

        #region LISTBOX USER
        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        private void ListBoxThisUser()
        {
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();

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
            ListBoxThisUser();

            // Reset editMode to false after saving and reload interface
            EditMode = false;
            InterfaceEditModeUser();
        }
        #endregion LISTBOX USER

        #region INTERFACE USERS
        public void InterfaceEditModeUser()
        {
            Debug.WriteLine($"EditMode UserInterface: {EditMode}");

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
