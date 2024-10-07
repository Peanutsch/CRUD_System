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
        private Data _Data = new Data(); 
        private LoginValidation _LoginValidation = new LoginValidation();
        

        public MainFormADMIN()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set focus to the logout button when the form is loaded
            this.ActiveControl = buttonLOGOUT;

            // Load user data from data_users.csv
            LoadUserData();
        }

        private void LoadUserData()
        {
            // Get the user data from the CSV file
            var userData = _Data.GetUserData();

            // Create a DataTable to hold the data
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Surname", typeof(string));
            dataTable.Columns.Add("Address", typeof(string));
            dataTable.Columns.Add("Zip Code", typeof(string));
            dataTable.Columns.Add("City", typeof(string));
            dataTable.Columns.Add("Email Address", typeof(string));

            // Fill the DataTable with data from the user data list
            foreach (var user in userData)
            {
                dataTable.Rows.Add(user.Name, user.Surname, user.Address, user.ZipCode, user.City, user.Emailadress);
            }

            // Bind the DataTable to the DataGridView
            dataGridViewUsers.DataSource = dataTable;
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
        /// Updates the label that displays the user's role (ADMIN or USER).
        /// Changes the label's background color based on the user's admin status.
        /// </summary>
        /// <param name="isAdmin">A boolean indicating whether the user is an admin (true) or not (false).</param>

        public void UpdateRoleLabel(bool isAdmin)
        {
            labelAdmin.TextAlign = ContentAlignment.TopLeft;
            labelAdmin.BackColor = isAdmin ? Color.LightSkyBlue : Color.LightGreen;
            labelAdmin.Text = isAdmin ? "ADMIN" : "USER";
        }

        /// <summary>
        /// Displays the username in uppercase in the username text box
        /// and validates the user's rights based on the provided username and password.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        /// <param name="inputUserPSW">The password entered by the user.</param>
        public void BoxDisplay(string inputUserName, string inputUserPSW)
        {
            textBoxUserName.Text = $"{inputUserName.ToUpper()}";
            _LoginValidation.ValidateRights(this, inputUserName, inputUserPSW);
        }
    }
}
