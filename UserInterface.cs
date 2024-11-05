using CRUD_System.FileHandlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUD_System
{
    public class UserInterface
    {
        public bool EditMode { get; set; }

        readonly FilePaths path = new FilePaths();
        private readonly ADMINMainControl adminControl;

        public UserInterface(ADMINMainControl adminControl)
        {
            this.adminControl = adminControl;
        }

        #region TEXTBOXES
        public void EmptyTextBoxesADMIN()
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
        }
        #endregion TEXTBOXES

        #region LISTBOX ADMIN
        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        public void LoadUserDataListBox()
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
        /// Reloads the user interface after saving changes.
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
            LoadUserDataListBox();

            // Reset editMode to false after saving and reload interface
            InterfaceEditModeADMIN();
        }
        #endregion LISTBOX ADMIN

        #region EDITMODE DISPLAY

        public void InterfaceEditModeADMIN()
        {
            Debug.WriteLine($"EditMode: {EditMode}");

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
    }
}
