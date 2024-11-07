using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Interfaces;
using CRUD_System.Repositories;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CRUD_System
{
    public partial class AdminCreateControl : UserControl
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();
        ADMINMainForm mainFormADMIN = new ADMINMainForm();
        RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        AccountManager userRepository = new AccountManager();
        RepositoryLogEvents logEvents = new RepositoryLogEvents();
        ProfileManager profileManager = new ProfileManager();
        FormInteractionHandler interactionHandler = new FormInteractionHandler();

        bool isAdmin = false;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public AdminCreateControl()
        {
            InitializeComponent();
        }
        #endregion CONSTRUCTOR

        #region BUTTONS
        private void btnCancel_Click(object sender, EventArgs e)
        {
            interactionHandler.CloseCreateForm(this.ParentForm);
        }

        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            isAdmin = !isAdmin; // Toggle between true and false
        }

        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            profileManager.SaveNewUser(txtName.Text, txtSurname.Text, 
                                       txtAddress.Text, txtZIPCode.Text,
                                       txtCity.Text, txtEmail.Text,
                                       txtPhonenumber.Text, isAdmin);

            // Close CreateFormADMIN, return to MainFormADMIN
            interactionHandler.CloseCreateForm(this.ParentForm);
        }
        #endregion BUTTONS

        #region HANDLERS
        private void TxtAlias_TextChanged(object sender, EventArgs e)
        {
            // Check if both txtName and txtSurname have at least 2 characters
            if (txtName.Text.Length >= 1 && txtSurname.Text.Length >= 1)
            {
                // Generate and display the alias
                string displayAlias = userRepository.CreateTXTAlias(txtName.Text, txtSurname.Text);
                txtAlias.Text = displayAlias;
            }
            else
            {
                // Clear the alias and show placeholder text
                txtAlias.Clear();
                txtAlias.PlaceholderText = "Alias";
            }
        }
        #endregion HANDLERS
    }
}
