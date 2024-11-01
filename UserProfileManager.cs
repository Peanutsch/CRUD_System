using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_System
{
    internal class UserProfileManager
    {
        #region PROPERTIES
        readonly string dataLogin = Path.Combine(RootPath.GetRootPath(), @"FilesUserDetails\data_login.csv");
        readonly string dataUsers = Path.Combine(RootPath.GetRootPath(), @"FilesUserDetails\data_users.csv");
        readonly string logAction = Path.Combine(RootPath.GetRootPath(), @"FilesUserDetails\logEvents.csv");

        UserRepository userRepository = new UserRepository();

        // Initialize DateTime for logging
        LogEntryActions log = new LogEntryActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public UserProfileManager()
        {
            // Constructor logic here if needed
        }
        #endregion CONSTRUCTOR

        public void GenerateNewPassword(string alias, bool isAdmin)
        {
            var currentUser = LoginHandler.CurrentUser;

            // Read lines from data_users.csv
            var userLines = File.ReadAllLines(dataUsers).ToList();
            var loginLines = File.ReadAllLines(dataLogin).ToList();

            // Find user index
            int userIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, alias);
            int loginIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, alias);
            var loginDetails = loginLines[loginIndex].Split(",");

            MessageBoxes messageConfirmSave = new MessageBoxes();
            DialogResult dr = messageConfirmSave.MessageBoxConfirmToSAVEPassword(loginDetails[0]);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            string generatedPassword = PasswordManager.PasswordGenerator();
            loginLines[loginIndex] = $"{loginDetails[0]},{generatedPassword},{isAdmin}";

            userRepository.UpdateUserLogin(loginLines, loginIndex);

            MessageBoxes messageSucces = new MessageBoxes();
            messageSucces.MessageUpdateSucces();

             if (!string.IsNullOrEmpty(currentUser))
            {
                userRepository.LogPasswordChange(currentUser, loginDetails[0]);
            }
        }
    }
}
