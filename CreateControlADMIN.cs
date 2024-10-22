using System;
using System.Windows.Forms;

namespace CRUD_System
{
    public partial class CreateControlADMIN : UserControl
    {
        #region PROPERTIES
        MainFormADMIN mainFormADMIN = new MainFormADMIN();
        MainControlADMIN mainControlADMIN = new MainControlADMIN();

        readonly string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        readonly string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");

        bool isAdmin = false;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public CreateControlADMIN()
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
            isAdmin = !isAdmin;
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
            if (this.ParentForm is CreateFormADMIN createFormADMIN)
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
            return PasswordManager.GenerateUserPassword();
        }


        public void SaveNewUser()
        {
            if (txtName.Text.Length < 1 || txtSurname.Text.Length < 1)
            {
                MessageBox.Show("Missing Name and/or Surname");
                return;
            }

            // Initial txtBoxes are empty
            mainControlADMIN.EmptyTextBoxes();

            // Create a new record
            string isAlias = mainControlADMIN.CreateTXTAlias();
            string isPassword = GeneratePSW();

            // data_login: ALIAS, PASSWORD, ADMIN
            string newDataLogin = $"{isAlias},{isPassword},{isAdmin}";

            // Check each field and assign string.Empty if it is empty
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string alias = string.IsNullOrWhiteSpace(txtAlias.Text) ? string.Empty : txtAlias.Text;
            string address = string.IsNullOrWhiteSpace(txtAddress.Text) ? string.Empty : txtAddress.Text;
            string zipCode = string.IsNullOrWhiteSpace(txtZIPCode.Text) ? string.Empty : txtZIPCode.Text;
            string city = string.IsNullOrWhiteSpace(txtCity.Text) ? string.Empty : txtCity.Text;
            string email = string.IsNullOrWhiteSpace(txtEmail.Text) ? string.Empty : txtEmail.Text;
            string phoneNumber = string.IsNullOrWhiteSpace(txtPhonenumber.Text) ? string.Empty : txtPhonenumber.Text;

            // data_users: NAME, SURNAME, ALIAS, ADRESS, ZIPCODE, CITY, EMAIL ADRESS
            string newDataUsers = $"{name},{surname},{alias},{email},{address},{zipCode},{city},{phoneNumber}";

            // Append to the CSV files
            File.AppendAllText(dataLogin, newDataLogin + Environment.NewLine);
            File.AppendAllText(dataUsers, newDataUsers + Environment.NewLine);

            MessageBox.Show("User added successfully!");

            // Close CreateFormADMIN, return to MainFormADMIN
            CloseCreateForm();
        }

        #endregion METHODS CREATE CONTROL ADMIN
    }
}
