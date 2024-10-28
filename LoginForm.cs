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
        public static string? CurrentUser { get; private set; }

        public List<string> UsersOnline = new List<string>();

        private bool isPasswordVisible = false;

        /// <summary>
        /// Constructor. Initializes the components for the LoginForm.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            EnterKey(); //Initialize Key.Enter
        }

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

        /// <summary>
        /// Authenticates the user's login credentials by checking the provided
        /// username and password against the login data stored in the CSV file.
        /// If the credentials are valid, the user is logged in and the MainForm is shown.
        /// If invalid, an error message is displayed.
        /// </summary>
        /// <param name="inputUserName">The username input provided by the user.</param>
        /// <param name="inputUserPSW">The password input provided by the user.</param>
        private void AuthenticateUser(string inputUserName, string inputUserPSW)
        {
            LoginValidation loginValidation = new LoginValidation();
            MainFormADMIN mainFormADMIN = new MainFormADMIN();
            MainControlADMIN mainControlADMIN = new MainControlADMIN();

            // Validate login input
            if (loginValidation.ValidateLogin(inputUserName, inputUserPSW))
            {
                CurrentUser = inputUserName;

                UsersOnline.Add(inputUserName); // Add user to list UsersOnline

                Debug.WriteLine($"=====\nUser [{inputUserName.ToUpper()}] logged IN");
                Debug.WriteLine($"Total users Online: {UsersOnline.Count}\n=====");

                // Check if user is admin
                bool isAdmin = loginValidation.IsAdmin(inputUserName, inputUserPSW);

                // Hide LoginForm
                this.Hide();

                // If user is admin, open MainFormADMIN
                if (isAdmin)
                {
                    //MainFormADMIN mainFormADMIN = new MainFormADMIN();
                    mainFormADMIN.BoxDisplay(inputUserName); // Pass user input
                    mainFormADMIN.ShowDialog();
                }
                // If user is no admin, open MainFormUSERS
                else
                {
                    MainFormUSERS mainFormUSERS = new MainFormUSERS();
                    mainFormUSERS.BoxDisplay(inputUserName); // Pass user input
                    mainFormUSERS.ShowDialog();
                }

                // When MainForm is closed, close LoginForm
                this.Close();
            }
            else
            {
                // Error message when input not valid
                MessageBox.Show("Invalid username or password");
            }
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
    }
}
