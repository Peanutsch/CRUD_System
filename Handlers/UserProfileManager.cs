using CRUD_System.FileHandlers;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_System.Handlers
{
    internal class UserProfileManager
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();
        UserRepository userRepository;

        // Initialize DateTime for logging
        LogEntryActions log = new LogEntryActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public UserProfileManager(ADMINMainControl adminControl)
        {
            userRepository = new UserRepository(adminControl);
        }
        #endregion CONSTRUCTOR

        /// <summary>
        /// Generates a new password for the specified user alias and updates the login details in the system.
        /// </summary>
        /// <param name="alias">The alias of the user for whom the password is being generated.</param>
        /// <param name="isAdmin">A boolean indicating whether the user has admin privileges.</param>
        public void GenerateNewPassword(string alias, bool isAdmin)
        {
            // Retrieve the currently logged-in user
            var currentUser = LoginHandler.CurrentUser;

            // Read all lines from data_users.csv and data_login.csv into lists
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();

            // Find the index of the user and their login details using the provided alias
            int userIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, alias);
            int loginIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, alias);
            var loginDetails = loginLines[loginIndex].Split(",");

            // Create a message box instance to confirm password change
            MessageBoxes messageConfirmSave = new MessageBoxes();
            DialogResult dr = messageConfirmSave.MessageBoxConfirmToSAVEPassword(loginDetails[0]);

            // If the user cancels the action, exit the method
            if (dr != DialogResult.Yes)
            {
                return;
            }

            // Generate a new password for the user
            string generatedPassword = PasswordManager.PasswordGenerator();

            // Update the login details with the new password and admin status
            loginLines[loginIndex] = $"{loginDetails[0]},{generatedPassword},{isAdmin}";

            // Update the login information in the repository
            userRepository.UpdateUserLogin(loginLines, loginIndex);

            // Show a success message to the user
            MessageBoxes messageSucces = new MessageBoxes();
            messageSucces.MessageUpdateSucces();

            // Log the password change action if there is a current user
            if (!string.IsNullOrEmpty(currentUser))
            {
                userRepository.LogPasswordChange(currentUser, loginDetails[0]);
            }
        }

    }
}
