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
            if (txtName.Text.Length < 1)
            {
                MessageBox.Show("There is no input");
                return;
            }

            // Create a new record
            string isAlias = mainControlADMIN.CreateTXTAlias();
            string isPassword = GeneratePSW();

            // data_login: ALIAS, PASSWORD, ADMIN
            string newDataLogin = $"{isAlias},{isPassword},{isAdmin}";

            // data_users: NAME, SURNAME, ALIAS, ADRESS, ZIPCODE, CITY, EMAIL ADRESS
            string newDataUsers = $"{txtName.Text},{txtSurname.Text},{txtAlias},{txtEmail.Text},{txtAddress.Text},{txtCity.Text}";

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
