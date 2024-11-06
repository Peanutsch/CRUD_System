using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
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
        Repository_LogEvents logEvents = new Repository_LogEvents();

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
            CloseCreateForm();
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
        public string GeneratePSW()
        {
            return PasswordManager.PasswordGenerator();
        }


        public void SaveNewUser()
        {
            var currentUser = LoginHandler.CurrentUser;

            if (txtName.Text.Length < 2 || txtSurname.Text.Length < 2)
            {
                message.MessageInvalidInput();
                return;
            }

            // Create a new record
            string isAlias = CreateTXTAlias();
            string isPassword = GeneratePSW();

            // Check each field and assign string.Empty if it is empty
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string address = string.IsNullOrWhiteSpace(txtAddress.Text) ? string.Empty : txtAddress.Text;
            string zipCode = string.IsNullOrWhiteSpace(txtZIPCode.Text) ? string.Empty : txtZIPCode.Text;
            string city = string.IsNullOrWhiteSpace(txtCity.Text) ? string.Empty : txtCity.Text;
            string email = string.IsNullOrWhiteSpace(txtEmail.Text) ? string.Empty : txtEmail.Text;
            string phoneNumber = string.IsNullOrWhiteSpace(txtPhonenumber.Text) ? string.Empty : txtPhonenumber.Text;

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
            CloseCreateForm();
        }

        #region ALIAS
        /// <summary>
        /// Generates a unique alias for the user based on the first two letters of the first name
        /// and the last two letters of the surname, followed by a number that increments if the alias already exists.
        /// </summary>
        /// <returns>A unique alias as a string.</returns>
        public string CreateTXTAlias()
        {
            string initialAlias = txtName.Text.Substring(0, 2).ToLower() + txtSurname.Text.Substring(txtSurname.Text.Length - 2).ToLower();
            int counter = 1;
            string finalAlias = initialAlias + "001";

            // Check if the alias already exists and increment the number if necessary
            while (AliasExists(finalAlias))
            {
                counter++;
                string newNumber = counter.ToString("D3"); // Ensures it always has 3 digits
                finalAlias = initialAlias + newNumber;
            }


            txtAlias.Text = finalAlias;
            //Debug.WriteLine($"Alias user: {finalAlias}");
            
            return finalAlias;
        }

        /// <summary>
        /// Checks if the given alias already exists in the data_login.csv file.
        /// </summary>
        /// <param name="alias">The alias to check for existence.</param>
        /// <returns>True if the alias exists; otherwise, false.</returns>
        private bool AliasExists(string alias)
        {
            // Read all lines from data_login.csv
            var loginLines = File.ReadAllLines(path.LoginFilePath);

            // Check if the alias already exists
            foreach (var line in loginLines)
            {
                var loginDetails = line.Split(',');
                if (loginDetails[0] == alias)
                {
                    Debug.WriteLine($"Alias {alias} already exist");
                    return true; // Alias already exists
                }
            }

            return false; // Alias does not exist
        }

        private void TxtAlias_TextChanged(object sender, EventArgs e)
        {
            // Check if both txtName and txtSurname have at least 2 characters
            if (txtName.Text.Length >= 2 && txtSurname.Text.Length >= 2)
            {
                // Generate and display the alias
                string displayAlias = CreateTXTAlias();
                txtAlias.Text = displayAlias;
            }
            else
            {
                // Clear the alias and show placeholder text
                txtAlias.Clear();
                txtAlias.PlaceholderText = "Alias";
            }
        }
        #endregion ALIAS
        #endregion METHODS CREATE CONTROL ADMIN
    }
}
