﻿using System;
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
    public partial class USERSMainForm : Form
    {
        readonly string logAction = Path.Combine(RootPath.GetRootPath(), @"data\log.csv");

        //private LoginForm loginForm = new LoginForm();
        private UtilitiesLogin logout = new UtilitiesLogin();

        #region Initialize DateTime for logging
        LogActions log = new LogActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion

        public USERSMainForm()
        {
            InitializeComponent();
        }

        private void MainFormUsers_Load(object sender, EventArgs e)
        {
            // Set focus to the logout button when the form is loaded
            this.ActiveControl = buttonLOGOUT;
        }

        /// <summary>
        /// Displays the username in uppercase in the username text box
        /// and validates the user's rights based on the provided username and password.
        /// </summary>
        public void DisplayUserInformationForm()
        {
            var currentUser = LoginForm.CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                textBoxUserName.Text = $"{currentUser.ToUpper()}";
            }
            else
            {
                textBoxUserName.Text = "UNKNOWN";
            }
            labelUserName.TextAlign = ContentAlignment.TopLeft;
            labelUserName.BackColor = Color.LightGreen;
            labelUserName.Text = "USER";
        }

        /// <summary>
        /// Handles the click event of the logout button. 
        /// Hides the MainForm and opens the LoginForm.
        /// Closes the MainForm once the LoginForm is closed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonLOGOUT_Click(object sender, EventArgs e)
        {
            logout.PerformLogout();
            this.Hide(); // Hide the MainForm
        }

        private void USERSMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            logout.PerformLogout();
        }
    }
}

