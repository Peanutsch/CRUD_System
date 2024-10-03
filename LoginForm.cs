using Microsoft.Win32;
using System.Windows.Forms;

namespace CRUD_LoginSystem
{
    /// <summary>
    /// The LoginForm class handles the user interface for the login process,
    /// including input validation and enabling login functionality once the user
    /// has entered valid credentials.
    /// </summary>
    public partial class LoginForm : Form
    {
        // Create an instance of the LoginValidation class for validating user credentials.
        LoginValidation loginValidation = new LoginValidation();

        /// <summary>
        /// Initializes the components for the LoginForm.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enables the password input box when the username input box has text.
        /// </summary>
        private void loginUserNameBox_TextChanged(object sender, System.EventArgs e)
        {
            // Check if there is text in the username box
            if (!string.IsNullOrEmpty(loginUserNameBox.Text))
            {
                // Enable the password input box
                loginUserPSWBox.Enabled = true;
            }
            else
            {
                // Disable the password input box if the username is empty
                loginUserPSWBox.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the click event of the login button.
        /// Validates the username and password, and displays a success message when login is successful,
        /// or an error message if the credentials are invalid.
        /// </summary>
        private void loginButton_Click(object sender, EventArgs e)
        {
            // Retrieve the current input values for validation
            string inputUserName = loginUserNameBox.Text;
            string inputUserPSW = loginUserPSWBox.Text;

            // Validate username and password, and show appropriate message
            if (loginValidation.ValidateLoginName(inputUserName) && loginValidation.ValidatePassword(inputUserPSW))
            {
                MessageBox.Show("Login SUCCESS");
            }
            else
            {
                // Show error message if the username or password is invalid.
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
