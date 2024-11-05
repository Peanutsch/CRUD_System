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

namespace CRUD_System
{
    public partial class ADMINMainControl : UserControl
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        UserInterface userInterface;
        Repository userRespository = new Repository();
        ProfileManager userProfileManager = new ProfileManager();
        InteractionHandler interactionHandler = new InteractionHandler();

        // Property to expose the InteractionHandler instance for external access
        public InteractionHandler InteractionHandler => interactionHandler;

        MessageBoxes message = new MessageBoxes();

        bool editMode = false;
        bool isAdmin = false;

        #region Initialize DateTime for logging
        LogEntryActions log = new LogEntryActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion
        #endregion PROPERTIES

        #region Constructor
        public ADMINMainControl(UserInterface? userInterface = null)
        {
            InitializeComponent();

            // Assign the UserInterface field; if no instance is provided, create a new UserInterface instance
            this.userInterface = userInterface ?? new UserInterface(this);

            // Load data_users.csv for display in listbox
            this.userInterface.LoadUserDataListBox();
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
                userInterface.EditMode = ToggleEditMode();
                userInterface.InterfaceEditModeADMIN();
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
            Repository userRepository = new Repository();
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();
            int userIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, txtAlias.Text);
            if (userIndex != -1)
            {
                userProfileManager.UpdateUserDetails(userLines, userIndex, txtName.Text, txtSurname.Text, txtAlias.Text, txtAddress.Text, txtZIPCode.Text, txtCity.Text, txtEmail.Text, txtPhonenumber.Text);
            }
            //editMode = false;
            userInterface.EditMode = false;
            userInterface.InterfaceEditModeADMIN();
            userInterface.ReloadListBoxAdmin(userIndex); // Reload listbox
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
                userInterface.LoadUserDataListBox();
                userInterface.EmptyTextBoxesAdmin();
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
            Repository userRepository = new Repository();
            interactionHandler.OpenCreateForm(this);
            listBoxAdmin.Items.Clear();
            userInterface.LoadUserDataListBox();
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
        #endregion BUTTONS SoC (Seperate of Concerns)

        /// <summary>
        /// Handles the event when a user is selected from the list box.
        /// Extracts the alias from the selected item.
        /// Retrieves the corresponding user details from both data_users.csv and data_login.csv. 
        /// The user's information is then displayed in the appropriate text fields.
        /// </summary>
        /// <param name="sender">The source of the event (the ListBox).</param>
        /// <param name="e">The event data (user selection).</param>
        public void ListBoxAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            userInterface.ListBoxAdmin_SelectedIndexChanged();
        }
    }
}
