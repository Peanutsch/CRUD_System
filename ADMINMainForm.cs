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

        public string loggedInUser = string.Empty;

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
            LogOutButton(loggedInUser);
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

        public void LogOutButton(string loggedInUser)
        {
            Debug.WriteLine($"\n(LogOut Button)\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) User [{loggedInUser.ToUpper()}] logged OUT");
            loginForm.UsersOnline.Remove(loggedInUser); // Remove user from UsersOnline
            //Debug.WriteLine($"Total users online: {loginForm.UsersOnline.Count}\n=====");

            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{loggedInUser.ToUpper()},Logged OUT";
            File.AppendAllText(logAction, newLog + Environment.NewLine);

            this.loggedInUser = string.Empty;
            this.Hide(); // Hide the MainForm
            loginForm.ShowDialog(); // Open the LoginForm
            //this.Close(); // Once MainForm is closed, close the LoginForm
        }

        private void MainFormADMIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(loggedInUser))
            {
                Debug.WriteLine($"\n(Form Close Button)\n\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) User [{loggedInUser.ToUpper()}] logged OUT");
                loginForm.UsersOnline.Remove(loggedInUser); // Remove user from UsersOnline
                                                            //Debug.WriteLine($"Total users online: {loginForm.UsersOnline.Count}\n==========");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{loggedInUser.ToUpper()},Logged OUT";
                File.AppendAllText(logAction, newLog + Environment.NewLine);
            }
            else
            {
                Debug.WriteLine("No user is currently logged in.");
            }
        }
    }
}
