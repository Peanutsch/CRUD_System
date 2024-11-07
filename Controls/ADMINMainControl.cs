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
using System.Diagnostics;
using CRUD_System.Handlers;
using CRUD_System.FileHandlers;
using CRUD_System.Interfaces;

namespace CRUD_System
{
    public partial class AdminMainControl : UserControl
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        AdminInterface adminInterface;
        UserRepository userRespository = new UserRepository();
        ProfileManager userProfileManager = new ProfileManager();
        InteractionHandler interactionHandler = new InteractionHandler();

        // Property to expose the InteractionHandler instance for external access
        public InteractionHandler InteractionHandler => interactionHandler;

        MessageBoxes message = new MessageBoxes();

        bool editMode = false;
        bool isAdmin = false;
        #endregion PROPERTIES

        #region Constructor
        public AdminMainControl(AdminInterface? adminInterface = null)
        {
            InitializeComponent();

            // Assign the UserInterface field; if no instance is provided, create a new UserInterface instance
            this.adminInterface = adminInterface ?? new AdminInterface(this);

            // Load data_users.csv for display in listbox
            this.adminInterface.LoadDetailsListBox();
        }
        #endregion CONSTRUCTOR

        #region BUTTONS SoC (Seperate of Concerns)
        /// <summary>
        /// Handles the click event to toggle edit mode for the selected user in listBoxUsers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnEditUserDetails_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(() =>  
            {
                // Toggle edit mode
                adminInterface.EditMode = ToggleEditMode();
                adminInterface.InterfaceEditModeAmin();
            },
             () => message.MessageInvalidNoUserSelected());
        }

        /// <summary>
        /// Handles the click event to save the edited user details.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnSaveEditUserDetails_Click(object sender, EventArgs e)
        {
            UserRepository userRepository = new UserRepository();
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();
            int userIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, txtAlias.Text);
            if (userIndex != -1)
            {
                userProfileManager.UpdateUserDetails(userLines, userIndex, txtName.Text, txtSurname.Text, txtAlias.Text, txtAddress.Text, txtZIPCode.Text, txtCity.Text, txtEmail.Text, txtPhonenumber.Text);
            }
            adminInterface.EditMode = false;
            adminInterface.InterfaceEditModeAmin();
            adminInterface.ReloadListBoxAdmin(userIndex); // Reload listbox
        }

        /// <summary>
        /// Handles the click event to delete user from data_users.csv and data_login.csv
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                userProfileManager.DeleteUser(txtAlias.Text); // Perform delete action only if a user is selected
                
                listBoxAdmin.Items.Clear();
                adminInterface.LoadDetailsListBox();
                adminInterface.EmptyTextBoxesAdmin();
            },
             () => message.MessageInvalidNoUserSelected());
        }

        /// <summary>
        /// Handles the click event to add a new user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            UserRepository userRepository = new UserRepository();
            interactionHandler.OpenCreateForm(this);
            // Reload listbox
            listBoxAdmin.Items.Clear();
            adminInterface.LoadDetailsListBox();
            // Empty Textboxes
            adminInterface.EmptyTextBoxesAdmin();
        }
        
        /// <summary>
        /// Handles click event to create a new password
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            ProfileManager userProfileManager = new ProfileManager();
            interactionHandler.PerformActionIfUserSelected(() =>
            {
                userProfileManager.GenerateNewPassword(txtAlias.Text, chkIsAdmin.Checked);
            },
             () => message.MessageInvalidNoUserSelected());
        }

        /// <summary>
        /// Handles the event when the 'Is Admin' checkbox state changes.
        /// Marks the current user as an admin when the checkbox is checked.
        /// </summary>
        /// <param name="sender">The source of the event (the CheckBox).</param>
        /// <param name="e">The event data (checkbox change).</param>
        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            isAdmin = !isAdmin; // Toggle between true and false
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            interactionHandler.Open_CreateNewPasswordForm();
        }

        private bool ToggleEditMode()
        {
            bool modus = editMode = !editMode;

            return modus;
        }

        public void ListBoxAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            adminInterface.ListBoxAdmin_SelectedIndexChanged();
        }
        #endregion BUTTONS SoC (Seperate of Concerns)
    }
}
