using CRUD_System.Handlers;
using Microsoft.Win32;
using System.Diagnostics;
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
        #region PROPERTIES
        AuthenticationService loginHandler = new AuthenticationService();

        private bool isPasswordVisible = false;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        /// <summary>
        /// Constructor. Initializes the components for the LoginForm.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            EnterKey(); //Initialize Key.Enter
        }
        #endregion CONSTRUCTOR

        #region FORM INTERFACE
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Set focus ActiveControl on loginButton
            this.ActiveControl = checkBoxTogglePSW;
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
                checkBoxTogglePSW.Enabled = true;
            }
            else
            {
                loginButton.Enabled = false;
                checkBoxTogglePSW.Enabled = false;
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

                    this.Hide();
                    // Validate username and password, and display appropriate message
                    loginHandler.AuthenticateUser(loginUserNameBox.Text.ToLower(), loginUserPSWBox.Text);
                    this.Close();
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
            this.Hide();
            // Validate username and password, and display appropriate message
            loginHandler.AuthenticateUser(loginUserNameBox.Text.ToLower(), loginUserPSWBox.Text);
            this.Close();
        }

        /// <summary>
        /// Toggles the visibility of the password in the password input box when the checkbox is checked or unchecked.
        /// Updates the text of the checkbox to reflect the current action (Show Password or Hide Password).
        /// </summary>
        /// <param name="sender">The source of the event, usually the checkbox.</param>
        /// <param name="e">The event arguments associated with the checkbox state change.</param>
        private void checkBoxTogglePSW_CheckedChanged(object sender, EventArgs e)
        {
            // Check if there is text in the password box
            if (loginUserPSWBox.Text.Length > 0)
            {
                isPasswordVisible = !isPasswordVisible; // Toggle between Visible and Hide password
                loginUserPSWBox.PasswordChar = isPasswordVisible ? '\0' : '*'; // Show or hide the password
                checkBoxTogglePSW.Text = isPasswordVisible ? "Hide Password" : "Show Password"; // Update the checkbox text based on the visibility state
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //
        }
        #endregion FORM INTERFACE
    }
}
