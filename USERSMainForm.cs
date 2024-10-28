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
        private OpenUserDetailFiles openDetails = new OpenUserDetailFiles();
        private LoginValidation _LoginValidation = new LoginValidation();

        public USERSMainForm()
        {
            InitializeComponent();
        }

        private void MainFormUsers_Load(object sender, EventArgs e)
        {
            // Set focus to the logout button when the form is loaded
            this.ActiveControl = buttonLOGOUT;
        }

        private void LoadUserDataListBox()
        {
            //
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
            this.Hide(); // Hide the MainForm

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog(); // Open the LoginForm

            this.Close(); // Once MainForm is closed, close the LoginForm
        }

        /// <summary>
        /// Displays the username in uppercase in the username text box
        /// and validates the user's rights based on the provided username and password.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        /// <param name="inputUserPSW">The password entered by the user.</param>
        public void BoxDisplay()
        {
            var currentUser = LoginForm.CurrentUser;

            if (currentUser != null)
            {
                textBoxUserName.Text = $"{currentUser.ToUpper()}";
            }
            else
            {
                textBoxUserName.Text = "NO_USER";
            }
            labelUser.TextAlign = ContentAlignment.TopLeft;
            labelUser.BackColor = Color.LightGreen;
            labelUser.Text = "USER";

        }
    }
}
