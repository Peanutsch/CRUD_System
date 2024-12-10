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
    public partial class AdminMainForm : Form
    {
        #region PROPERTIES
        AuthenticationService authService = new AuthenticationService();
        private readonly AdminMainControl adminControl = new AdminMainControl();
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public AdminMainForm()
        {
            InitializeComponent();

            // Roep de methode aan om de bestanden te laden in de ListView
            //LoadFilesIntoListView(reportDirectory, alias!);
        }
        #endregion CONSTRUCTOR

        #region FOCUS
        private void MainFormAdmin_Load(object sender, EventArgs e)
        {
            // Set focus to the logout button when the form is loaded
            this.ActiveControl = buttonLOGOUT;
        }
        #endregion FOCUS

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
            // Trigger FormClosing event
            this.Close();
        }

        /// <summary>
        /// Handles the form closing event for the MainFormADMIN. 
        /// This method ensures that the user is logged out, their online status is updated, 
        /// and the current user session is cleared when the form is being closed.
        /// Also triggerd by ALT-F4
        /// </summary>
        private void MainFormADMIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            authService.PerformLogout();
        }
        #endregion BUTTONS

        public void FormConfig(string isTheOne)
        {
            if (isTheOne == "admin" || isTheOne == "mist001")
            {
                this.BackColor = Color.DarkRed;
                this.Text = "F I U M (FuckItUp- mode)";
            }
        }
    }
}