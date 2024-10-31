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

        private readonly Utilities utilities = new Utilities();

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

            if (!string.IsNullOrEmpty(currentUser))
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

        private void MainFormADMIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            utilities.PerformLogout();
        }

        public void LogOutButton()
        {
            utilities.PerformLogout(); // Call a separate method for logout logic
            this.Hide();
        }
    }
}
