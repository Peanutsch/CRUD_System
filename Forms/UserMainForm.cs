using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
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
    public partial class UserMainForm : Form
    {
        #region PROPERTIES
        AuthenticationService authService = new AuthenticationService();
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public UserMainForm()
        {
            InitializeComponent();
        }
        #endregion CONSTRUCTOR

        #region BUTTONS
        private void MainFormUsers_Load(object sender, EventArgs e)
        {
            // Set focus to the logout button when the form is loaded
            this.ActiveControl = buttonLOGOUT;
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
            authService.PerformLogout();
            this.Hide(); // Hide the MainForm
        }

        private void USERSMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            authService.PerformLogout();
        }
        #endregion BUTTONS
    }
}

