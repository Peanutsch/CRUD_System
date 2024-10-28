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
using static System.Windows.Forms.LinkLabel;

namespace CRUD_System
{
    public partial class USERSMainControl : UserControl
    {
        #region PROPERTIES
        string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");

        ADMINMainControl adminMethods = new ADMINMainControl();

        bool editMode = false;
        bool userSelected = false;
        bool isAdmin = false;
        #endregion PROPERTIES

        public USERSMainControl()
        {
            InitializeComponent();

            LoadDetailsListBox();
        }

        #region BUTTONS
        /// <summary>
        /// Handles the click event to save the edited user details.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnSaveEditUserDetails_Click(object sender, EventArgs e)
        {
            //SaveEditUserDetails();
        }
        #endregion BUTTONS

        /// <summary>
        /// Handles the click event to toggle edit mode for the selected user in listBoxUsers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void btnEditUserDetails_Click(object sender, EventArgs e)
        {
            adminMethods.PerformActionIfUserSelected(() =>
            {
                // Toggle edit mode
                editMode = !editMode;
                adminMethods.InterfaceEditMode();
            });
        }

        /// <summary>
        /// Loads user data from data_users.csv and populates the list box with user names.
        /// </summary>
        private void LoadDetailsListBox()
        {
            var userLines = File.ReadAllLines(dataUsers).ToList();
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            Debug.WriteLine($"userLines Count: {userLines.Count}");

            var currentUser = LoginForm.CurrentUser;

            if (currentUser != null)
            {
                var userIndex = adminMethods.FindUserIndexByAlias(userLines, loginLines, currentUser);
                var userDetailsArray = userLines[userIndex].Split(',');

                UserDetails userDetails = new UserDetails(userDetailsArray);
                string listItem = $"{userDetails.Name} {userDetails.Surname} ({userDetails.Alias}) | {userDetails.Email} | {userDetails.PhoneNumber}";

                listBoxUsers.Items.Add(listItem);
            }
            else
            {
                MessageBox.Show($"No user details found for {currentUser!.ToUpper()}");
            }
        }

        /*
        private void FillTextBoxes()
        {
            var currentUser = LoginForm.CurrentUser!.ToUpper();

            var userLines = File.ReadAllLines(dataUsers);
            // Split each line into an array of user details
            var userDetailsArray = userLines.Split(',');

            // Create a UserDetails object using the array
            UserDetails userDetails = new UserDetails(currentUser);

            adminMethods.FillTextboxes(string[] userDetailsArray);
        }
        */
    }
}
