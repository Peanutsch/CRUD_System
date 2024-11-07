using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Interfaces;
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
        MessageBoxes message = new MessageBoxes();
        UserRepository userRepository = new UserRepository();
        Repository_LogEvents logEvents = new Repository_LogEvents();
        ProfileManager profileManager = new ProfileManager();
        InteractionHandler interactionHandler = new InteractionHandler();

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
            SaveNewUser();
        }
        #endregion

        #region METHODS CREATE CONTROL ADMIN
        /*
        public void CloseCreateForm()
        {
            // MustNeed: explicitly cast ParentForm to MainFormADMIN before passing it to the OpenCreateForm method
            // Check if ParentForm is not null and is of type MainFormADMIN
            if (this.ParentForm is ADMINCreateForm createFormADMIN)
            {
                this.ParentForm.Close();
            }
            else
            {
                MessageBox.Show("Parent form is not valid or is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
        public void SaveNewUser()
        {
            var currentUser = LoginHandler.CurrentUser;

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text))
            {
                message.MessageInvalidInput();
                return;
            }

            // Check each field and assign string.Empty if it is empty
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string address = string.IsNullOrWhiteSpace(txtAddress.Text) ? string.Empty : txtAddress.Text;
            string zipCode = string.IsNullOrWhiteSpace(txtZIPCode.Text) ? string.Empty : txtZIPCode.Text;
            string city = string.IsNullOrWhiteSpace(txtCity.Text) ? string.Empty : txtCity.Text;
            string email = string.IsNullOrWhiteSpace(txtEmail.Text) ? string.Empty : txtEmail.Text;
            string phoneNumber = string.IsNullOrWhiteSpace(txtPhonenumber.Text) ? string.Empty : txtPhonenumber.Text;

            // Create a new record
            string isAlias = userRepository.CreateTXTAlias(name, surname);
            string isPassword = PasswordManager.PasswordGenerator();

            DialogResult dr = message.MessageBoxConfirmNewUser(isAlias);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            if (!string.IsNullOrEmpty(currentUser))
            {
                string newDataLogin = $"{isAlias},{isPassword},{isAdmin}";
                string newDataUsers = $"{name},{surname},{isAlias},{address},{zipCode},{city},{email},{phoneNumber}";

                // Append to the CSV files
                File.AppendAllText(path.UserFilePath, newDataUsers + Environment.NewLine);
                File.AppendAllText(path.LoginFilePath, newDataLogin + Environment.NewLine);

                // Log event
                logEvents.NewAccount(currentUser, isAlias);
                message.MessageNewAccountSucces(isAlias);
            }
            else
            {
                message.MessageSomethingWentWrong();
            }
            // Close CreateFormADMIN, return to MainFormADMIN
            interactionHandler.CloseCreateForm(this.ParentForm);
        }
        

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
        #endregion METHODS CREATE CONTROL ADMIN
    }
}
