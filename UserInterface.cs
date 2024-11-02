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
        readonly FilePaths path = new FilePaths();
        private readonly ADMINMainControl adminControl;
        public bool editMode;
        

        public UserInterface(ADMINMainControl adminControl)
        {
            this.adminControl = adminControl;
        }

        #region TEXTBOXES
        public void EmptyTextBoxesADMIN()
        {
            ADMINMainControl mainControl = new ADMINMainControl();
            // Refill textboxes with empty values
            mainControl.txtName.Text = string.Empty;
            mainControl.txtSurname.Text = string.Empty;
            mainControl.txtAlias.Text = string.Empty;
            mainControl.txtAddress.Text = string.Empty;
            mainControl.txtZIPCode.Text = string.Empty;
            mainControl.txtCity.Text = string.Empty;
            mainControl.txtEmail.Text = string.Empty;
            mainControl.txtPhonenumber.Text = string.Empty;
        }
        #endregion TEXTBOXES

        #region LISTBOX ADMIN
        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        public void LoadUserDataListBox()
        {
            ADMINMainControl mainControl = new ADMINMainControl();

            var lines = File.ReadAllLines(path.UserFilePath);

            mainControl.listBoxAdmin.Items.Clear();

            foreach (var line in lines.Skip(2)) // Skip Header and details Admin
            {
                // Split each line into an array of user details
                var userDetailsArray = line.Split(',');

                // Create a UserDetails object using the array
                UserDetails userDetails = new UserDetails(userDetailsArray);

                // Use the UserDetails properties to format the string for the listBox
                string listItem = $"{userDetails.Name} {userDetails.Surname} ({userDetails.Alias}) | {userDetails.Email} | {userDetails.PhoneNumber}";

                // Add the formatted string to the listBox
                mainControl.listBoxAdmin.Items.Add(listItem);
            }
        }

        /// <summary>
        /// Reloads the user interface after saving changes.
        /// </summary>
        /// <param name="userIndex">The index of the updated user.</param>
        public void ReloadListBoxAdmin(int userIndex)
        {
            ADMINMainControl mainControl = new ADMINMainControl();
            if (userIndex >= 0 && userIndex < mainControl.listBoxAdmin.Items.Count)
            {
                mainControl.Refresh();
            }

            // Clear and reload listbox
            mainControl.listBoxAdmin.Items.Clear();
            LoadUserDataListBox();

            // Reset editMode to false after saving and reload interface
            //InterfaceEditModeADMIN();
        }
        #endregion LISTBOX ADMIN

        #region EDITMODE DISPLAY
        public void ToggleEditMode()
        {
            editMode = !editMode;
            InterfaceEditModeADMIN();
        }

        public void InterfaceEditModeADMIN()
        {
            // Gebruik editMode vanuit de UserInterface
            ADMINMainControl mainControl = new ADMINMainControl();

            // Toggle de text voor de edit-knop
            mainControl.btnEditUserDetails.Text = editMode ? "Cancel" : "Edit User";

            // Wijzig de achtergrondkleur
            mainControl.BackColor = editMode ? Color.Orange : SystemColors.ActiveCaption;

            // Pas de zichtbaarheid en enabled-status van knoppen en velden aan
            mainControl.btnSaveEditUserDetails.Visible = editMode ? true : false;
            mainControl.btnSaveEditUserDetails.BackColor = Color.LightGreen;

            mainControl.chkIsAdmin.Visible = editMode ? true : false;
            mainControl.chkIsAdmin.Enabled = editMode ? true : false;

            // Maak tekstvakken in of uitgeschakeld op basis van editMode
            mainControl.txtName.Enabled = editMode ? true : false;
            mainControl.txtSurname.Enabled = editMode ? true : false;
            mainControl.txtAlias.Enabled = editMode ? true : false;
            mainControl.txtAddress.Enabled = editMode ? true : false;
            mainControl.txtZIPCode.Enabled = editMode ? true : false;
            mainControl.txtCity.Enabled = editMode ? true : false;
            mainControl.txtEmail.Enabled = editMode ? true : false;
            mainControl.txtPhonenumber.Enabled = editMode ? true : false;

            mainControl.btnCreateUser.Visible = editMode ? true : false;
            mainControl.btnDeleteUser.Visible = editMode ? true : false;
            mainControl.btnGeneratePSW.Visible = editMode ? true : false;

            // Beheer de status van de ListBox
            mainControl.listBoxAdmin.Enabled = editMode ? true : false;
        }
        #endregion EDITMODE DISPLAY
    }
}
