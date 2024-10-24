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

namespace CRUD_System
{
    public partial class MainFormADMIN : Form
    {
        LoginForm loginForm = new LoginForm();

        public List<string> UsersOnline = new List<string>();

        public string loggedInUser = string.Empty;

        public MainFormADMIN()
        {
            InitializeComponent();
        }

        private void MainFormAdmin_Load(object sender, EventArgs e)
        {
            // Set focus to the logout button when the form is loaded
            this.ActiveControl = buttonLOGOUT;
        }

        #region BUTTONS
        /// <summary>
        /// Handles the click event of the logout button. 
        /// Hides the MainForm and opens the LoginForm.
        /// Closes the MainForm once the LoginForm is closed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonLOGOUT_Click(object sender, EventArgs e)
        {
            LogOut(loggedInUser);
        }
        #endregion BUTTONS

        /// <summary>
        /// Displays the username in uppercase in the username text box
        /// and validates the user's rights based on the provided username and password.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        public void BoxDisplay(string inputUserName)
        {
            loggedInUser = inputUserName;

            textBoxUserName.Text = $"{inputUserName.ToUpper()}";

            labelAdmin.TextAlign = ContentAlignment.TopLeft;
            labelAdmin.BackColor = Color.LightGreen;
            labelAdmin.Text = "ADMIN";
        }

        public void LogOut(string loggedInUser)
        {
            Debug.WriteLine($"User [{loggedInUser}] logged out");
            UsersOnline.Remove(loggedInUser); // Remove user from UsersOnline
            Debug.WriteLine($"Total users online: {UsersOnline.Count()}");

            this.Hide(); // Hide the MainForm
            loginForm.ShowDialog(); // Open the LoginForm
            this.Close(); // Once MainForm is closed, close the LoginForm
        }

        private void MainFormADMIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(loggedInUser))
            {
                Debug.WriteLine($"\n(Form Close Button)\nUser [{loggedInUser.ToUpper()}] logged OUT");
                UsersOnline.Remove(loggedInUser); // Remove user from UsersOnline
                Debug.WriteLine($"Total users online: {UsersOnline.Count}\n==========");
            }
            else
            {
                Debug.WriteLine("No user is currently logged in.");
            }
        }

        public string ThisUserName()
        {
            if (!string.IsNullOrEmpty(loggedInUser))
            {
                Debug.WriteLine($"\n(Form Close Button)\nUser [{loggedInUser.ToUpper()}] logged OUT");
                Debug.WriteLine($"Total users online: {UsersOnline.Count}\n==========");

                return $"User[{loggedInUser.ToUpper()}] logged OUT";
            }
            else
            {
                Debug.WriteLine("No user is currently logged in.");
                return $"User[{loggedInUser.ToUpper()}] is string.Empty";
            }
        }
    }
}
