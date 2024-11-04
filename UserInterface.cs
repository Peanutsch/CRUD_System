using CRUD_System.FileHandlers;
using System;
using System.Collections.Generic;
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
            // Toggle de text voor de edit-knop
            adminControl.btnEditUserDetails.Text = EditMode ? "Cancel" : "Edit User";

            // Wijzig de achtergrondkleur
            adminControl.BackColor = EditMode ? Color.Orange : SystemColors.ActiveCaption;

            // Pas de zichtbaarheid en enabled-status van knoppen en velden aan
            adminControl.btnSaveEditUserDetails.Visible = EditMode ? true : false;
            adminControl.btnSaveEditUserDetails.BackColor = Color.LightGreen;

            adminControl.chkIsAdmin.Visible = EditMode ? true : false;
            adminControl.chkIsAdmin.Enabled = EditMode ? true : false;

            // Maak tekstvakken in of uitgeschakeld op basis van editMode
            adminControl.txtName.Enabled = EditMode ? true : false;
            adminControl.txtSurname.Enabled = EditMode ? true : false;
            adminControl.txtAlias.Enabled = EditMode ? true : false;
            adminControl.txtAddress.Enabled = EditMode ? true : false;
            adminControl.txtZIPCode.Enabled = EditMode ? true : false;
            adminControl.txtCity.Enabled = EditMode ? true : false;
            adminControl.txtEmail.Enabled = EditMode ? true : false;
            adminControl.txtPhonenumber.Enabled = EditMode ? true : false;

            adminControl.btnCreateUser.Visible = EditMode ? true : false;
            adminControl.btnDeleteUser.Visible = EditMode ? true : false;
            adminControl.btnGeneratePSW.Visible = EditMode ? true : false;

            // Beheer de status van de ListBox
            adminControl.listBoxAdmin.Enabled = EditMode ? true : false;
        }
        #endregion EDITMODE DISPLAY
    }
}
