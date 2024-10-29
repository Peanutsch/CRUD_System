using Microsoft.VisualBasic.Logging;
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
    public partial class ADMINMainForm : Form
    {
        readonly string logAction = Path.Combine(RootPath.GetRootPath(), @"data\log.csv");

        LoginForm loginForm = new LoginForm();

        #region Initialize DateTime for logging
        LogActions log = new LogActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion

        public ADMINMainForm()
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
            LogOutButton();
        }
        #endregion BUTTONS

        /// <summary>
        /// Displays the username in uppercase in the username text box
        /// and validates the user's rights based on the provided username and password.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        public void BoxDisplay()
        {
            var currentUser = LoginForm.CurrentUser;

            if (currentUser != null)
            {
                textBoxUserName.Text = $"{currentUser.ToUpper()}";
            }
            else
            {
                textBoxUserName.Text = $"UNKNOWN";
            }
            labelAdmin.TextAlign = ContentAlignment.TopLeft;
            labelAdmin.BackColor = Color.LightGreen;
            labelAdmin.Text = "ADMIN";
        }

        public void LogOutButton()
        {
            var currentUser = LoginForm.CurrentUser;

            if (currentUser != null)
            {
                Debug.WriteLine($"\n(LogOut Button)\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) User [{currentUser.ToUpper()}] logged OUT");
                loginForm.UsersOnline.Remove(currentUser); // Remove user from UsersOnline

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Logged OUT";
                File.AppendAllText(logAction, newLog + Environment.NewLine);

                LoginForm.CurrentUser = string.Empty;
            }
            LoginForm.CurrentUser = string.Empty;
            this.Hide(); // Hide the MainForm
            this.Close();
        }

        private void MainFormADMIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            var currentUser = LoginForm.CurrentUser;

            if (currentUser != null)
            {
                Debug.WriteLine($"\n(Form Close Button)\n\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) User [{currentUser.ToUpper()}] logged OUT");
                loginForm.UsersOnline.Remove(currentUser); // Remove user from UsersOnline

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Logged OUT";
                File.AppendAllText(logAction, newLog + Environment.NewLine);

                LoginForm.CurrentUser = string.Empty;
            }
            else
            {
                Debug.WriteLine("No user is currently logged in.");
            }
        }
    }
}
