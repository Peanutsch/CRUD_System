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
        FilePaths path = new FilePaths();

        MessageBoxes message = new MessageBoxes();

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

        public void UpdateUserDetails(List<string> userLines, int userIndex, string name, string surname, string alias, string address, string zipCode, string city, string email, string phoneNumber)
        {
            userLines[userIndex] = $"{name},{surname},{alias},{address},{zipCode.ToUpper()},{city},{email},{phoneNumber}";

            DialogResult dr = message.MessageBoxConfirmToSAVEChanges(alias);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            File.WriteAllLines(path.UserFilePath, userLines); // Write updated data back to data_users.csv
            Debug.WriteLine($"User Details after Update: {userLines[userIndex]}");

            var currentUser = LoginHandler.CurrentUser;
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Updated user details for {alias.ToUpper()}");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Updated user details for {alias.ToUpper()}";
                //File.AppendAllText(logAction, newLog + Environment.NewLine);
                path.AppendToLog(newLog);

                message.MessageUpdateSucces();
            }
        }

        public void GenerateNewPassword(string alias, bool isAdmin)
        {
            var currentUser = LoginHandler.CurrentUser;

            // Read lines from data_users.csv
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();

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
