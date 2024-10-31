using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
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

        LoginHandler loginHandler = new LoginHandler();

        #region Initialize DateTime for logging
        LogEntryActions log = new LogEntryActions
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

        private void MainFormADMIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            //loginManager.PerformLogout();
            loginHandler.PerformLogout();
        }

        public void LogOutButton()
        {
            //loginManager.PerformLogout();
            loginHandler.PerformLogout();
            this.Hide();
        }
    }
}