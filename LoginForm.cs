using Microsoft.Win32;
using System.Windows.Forms;

namespace CRUD_System
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

        private bool isPasswordVisible = false;

        /// <summary>
        /// Constructor. Initializes the components for the LoginForm.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            EnterKey();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Set focus ActiveControl
            this.ActiveControl = loginButton;
        }

        /// <summary>
        /// Enables the password input box when the username input box has text.
        /// </summary>
        private void LoginUserNameBox_TextChanged_1(object sender, EventArgs e)
        {
            // Check if there is text in the username box
            if (loginUserNameBox.Text.Length > 0)
            {
                // Enable the password input box
                loginUserPSWBox.Enabled = true;
            }
            else
            {
                loginUserPSWBox.Enabled = false;
            }
        }

        /// <summary>
        /// Enables the login button when there is text in the password box.
        /// </summary>
        private void LoginUserPSWBox_TextChanged(object sender, EventArgs e)
        {
            // Check if there is text in the password box
            if (loginUserPSWBox.Text.Length > 0)
            {
                // Enable login button
                loginButton.Enabled = true;
                buttonShowPSW.Enabled = true;
            }
            else
            {
                loginButton.Enabled = false;
                buttonShowPSW.Enabled = false;
            }
        }

        /// <summary>
        /// Initialize EnterKey to confirm input when Enter is pressed.
        /// </summary>
        private void EnterKey()
        {
            // Attach the KeyDown event to the password box without any conditions
            loginUserPSWBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true; // Prevent the Enter key from inserting a new line
                    e.SuppressKeyPress = true; // Prevent the "ding" sound when Enter is pressed

                    // Validate username and password, and display appropriate message
                    AuthenticateUser(loginUserNameBox.Text, loginUserPSWBox.Text);
                }
            };
        }

        /// <summary>
        /// Handles the click event of the login button.
        /// Validates the username and password, and displays a success message when login is successful,
        /// or an error message if the credentials are invalid.
        /// </summary>
        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Validate username and password, and display appropriate message
            AuthenticateUser(loginUserNameBox.Text, loginUserPSWBox.Text);
        }

        private void TogglePasswordButton_Click(object sender, EventArgs e)
        {
            // Check if there is text in the password box
            if (loginUserPSWBox.Text.Length > 0)
            {
                isPasswordVisible = !isPasswordVisible; // Toggle between Visible and Hide password

                // Update PasswordChar based on visibility state
                loginUserPSWBox.PasswordChar = isPasswordVisible ? '\0' : '*'; // Show or hide the password
                buttonShowPSW.BackColor = isPasswordVisible ? Color.Red : Color.Green; // Toggle color buttonShowPSW

                // Update the label text based on the visibility state
                labelShowPassword.Text = isPasswordVisible ? "Hide Password" : "Show Password";
            }
        }

        /// <summary>
        /// Authenticates the user's login credentials. 
        /// If the username and password are valid, the LoginForm is hidden, and the MainForm is displayed. 
        /// If invalid, an error message is shown.
        /// </summary>
        /// <param name="inputUserName">The username input provided by the user.</param>
        /// <param name="inputUserPSW">The password input provided by the user.</param>
        public void AuthenticateUser(string inputUserName, string inputUserPSW)
        {
            if (loginValidation.ValidateLoginName(inputUserName) && loginValidation.ValidatePassword(inputUserPSW))
            {
                // Hide the LoginForm
                this.Hide();

                // Open the MainForm
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();

                //Once MainForm is closed, close the LoginForm
                this.Close();
            }
            else
            {
                // Show error message if the username or password is invalid
                MessageBox.Show("Invalid username or password");
            }
        }

    }
}
