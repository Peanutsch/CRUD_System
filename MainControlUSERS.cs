using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_System
{
    public partial class MainControlUSERS : UserControl
    {
        #region PROPERTIES
        string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");

        MainControlADMIN adminMethods = new MainControlADMIN();

        bool editMode = false;
        bool userSelected = false;
        bool isAdmin = false;
        #endregion PROPERTIES

        public MainControlUSERS()
        {
            InitializeComponent();


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

        public void LoadUserDetails_ListBox()
        {
            adminMethods.LoadUserDataListBox();
        }

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

    }
}
