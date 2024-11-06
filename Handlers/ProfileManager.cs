using CRUD_System.FileHandlers;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CRUD_System.Interfaces;

namespace CRUD_System.Handlers
{
    internal class ProfileManager
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        MessageBoxes message = new MessageBoxes();
        Repository userRepository = new Repository();
        Repository_LogEvents logEvents = new Repository_LogEvents();
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public ProfileManager()
        {
            // 
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

            var currentUser = LoginHandler.CurrentUser;
            if (!string.IsNullOrEmpty(currentUser))
            {
                File.WriteAllLines(path.UserFilePath, userLines); // Write updated data back to data_users.csv
                Debug.WriteLine($"User Details after Update: {userLines[userIndex]}");

                logEvents.LogEventUpdateUserDetails(currentUser, alias);
                message.MessageUpdateSucces();
            }
            else
            {
                message.MessageSomethingWentWrong();
            }
        }

        /// <summary>
        /// Method for deleting user from data_users.csv and data_log.csv
        /// </summary>
        public void DeleteUser(string alias)
        {
            AdminInterface userInterface = new AdminInterface();

            var currentUser = LoginHandler.CurrentUser;

            // Get alias to delete from the selected user
            string aliasToDelete = alias;

            // Read lines from data_users.csv and data_login.csv
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();

            int userIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, aliasToDelete);

            // MessageBox to confirm task
            DialogResult dr = message.MessageBoxConfirmToDELETE(aliasToDelete);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            // Remove the user from data_users.csv by alias
            userLines = userLines.Where(line =>
                                        !line.Split(',')[2].Trim().Equals(aliasToDelete,
                                        StringComparison.OrdinalIgnoreCase)).ToList();
            File.WriteAllLines(path.UserFilePath, userLines);

            // Remove the user from data_login.csv using the alias
            loginLines = loginLines.Where(line =>
                                            !line.Split(',')[0].Trim().Equals(aliasToDelete,
                                            StringComparison.OrdinalIgnoreCase)).ToList();
            File.WriteAllLines(path.LoginFilePath, loginLines);

            if (!string.IsNullOrEmpty(currentUser))
            {
                logEvents.LogEventDeleteUser(currentUser, aliasToDelete);
                message.MessageDeleteSucces(aliasToDelete); // Show MessageBox Delete Succes
            }
            else
            {
                message.MessageSomethingWentWrong();
            }
            userInterface.ReloadListBoxAdmin(userIndex);
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

            DialogResult dr = message.MessageBoxConfirmToGeneratePassword(loginDetails[0]);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            string generatedPassword = PasswordManager.PasswordGenerator();
            loginLines[loginIndex] = $"{loginDetails[0]},{generatedPassword},{isAdmin}";

            userRepository.UpdateGeneratedPassword(loginLines, loginIndex);

            if (!string.IsNullOrEmpty(currentUser))
            {
                logEvents.LogEventPasswordGenerated(currentUser, loginDetails[0]);
                message.MessageChangePasswordSucces(loginDetails[0]);
            }
        }
    }
}
