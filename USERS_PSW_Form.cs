using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CRUD_System
{
    public partial class USERS_PSW_Form : Form
    {
        // Password conditions
        public int lengthPsw = 12;
        public int charToUpper = 3;
        public int charIsDigi = 3;

        ADMINMainControl adminMethods = new ADMINMainControl();
        
        string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");

        private bool isPasswordVisible = false;

        public USERS_PSW_Form()
        {
            InitializeComponent();

            TxtLabelPSW();
        }

        private void checkBoxTogglePSW_CheckedChanged(object sender, EventArgs e)
        {
            // Check if there is text in the password box
            if (inputChangePSW1.Text.Length > 0)
            {
                isPasswordVisible = !isPasswordVisible; // Toggle between Visible and Hide password
                inputChangePSW1.PasswordChar = isPasswordVisible ? '\0' : '*'; // Show or hide the password
                checkBoxTogglePSW1.Text = isPasswordVisible ? "Hide Password" : "Show Password"; // Update the checkbox text based on the visibility state
            }
        }

        private void btnEnterPSW_Click(object sender, EventArgs e)
        {
            ValidatePSW();
            this.Close();
        }

        private void ValidatePSW()
        {
            var userLines = File.ReadAllLines(dataUsers).ToList();
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            var currentUser = LoginForm.CurrentUser;

            if (currentUser != null)
            {
                // Find userIndex in data_login.csv and data_users.csv
                int userIndex = adminMethods.FindUserIndexByAlias(userLines, loginLines, currentUser);
                int loginIndex = adminMethods.FindUserIndexByAlias(userLines, loginLines, currentUser);

                string newPassword = inputChangePSW1.Text;
                int uppercaseCount = newPassword.Count(char.IsUpper);
                int digitCount = newPassword.Count(char.IsDigit);

                if (newPassword.Length >= lengthPsw && uppercaseCount >= charToUpper && digitCount >= charIsDigi)
                {
                    MessageBoxes message = new MessageBoxes();
                    DialogResult dr = message.MessageBoxConfirmToSAVEPasssword(currentUser);

                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }

                    Debug.WriteLine($"User psw: {newPassword}");
                    UpdateNewPassword(loginLines, userIndex, newPassword);
                }
                else
                {
                    MessageBoxes message = new MessageBoxes();
                    message.MessageInvalidPassword();
                }
            }
        }

        /// <summary>
        /// Updates the login details at the specified index in the CSV data.
        /// Writes the updated details back to data_login.csv.
        /// </summary>
        /// <param name="loginLines">The list of lines from data_users.csv.</param>
        /// <param name="userIndex">The index of the user to update.</param>
        public void UpdateNewPassword(List<string> loginLines, int userIndex, string newPassword)
        {
            var loginDetails = loginLines[userIndex].Split(',');

            // When need to keep current data on indexes
            string currentAlias = loginDetails[0];
            string currentPassword = loginDetails[1];
            string currentAdminBool = loginDetails[2];

            Debug.WriteLine($"Current: {currentAlias},{currentPassword},{currentAdminBool}");

            loginLines[userIndex] = $"{currentAlias},{newPassword},{currentAdminBool}";

            Debug.WriteLine($"After Update: {loginLines[userIndex]}");

            Debug.WriteLine($"[{currentAlias.ToUpper()}]: Changed password");

            File.WriteAllLines(dataLogin, loginLines); // Write updated data back to data_login.csv
        }

        private void TxtLabelPSW()
        {
            lblPassword.Text = $"Must contain {lengthPsw} or more chars.\n" +
                               $"Must contain at least {charToUpper} capital letters\n" +
                               $"Must contain at least {charIsDigi} numbers";
        }


    }
}
