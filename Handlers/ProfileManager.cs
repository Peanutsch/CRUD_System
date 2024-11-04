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
    internal class ProfileManager
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        MessageBoxes message = new MessageBoxes();
        Repository userRepository = new Repository();

        // Initialize DateTime for logging
        LogEntryActions log = new LogEntryActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public ProfileManager()
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

        /// <summary>
        /// Method for deleting user from data_users.csv and data_log.csv
        /// </summary>
        public void DeleteUser(string alias)
        {
            ADMINMainControl adminControl = new ADMINMainControl(); // Temp object for ReloadListBoxAdmin and EmptyTextBoxes

            var currentUser = LoginHandler.CurrentUser;

            // Read lines from data_users.csv and data_login.csv
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();

            int userIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, adminControl.txtAlias.Text);

            // Get alias to delete from the selected user
            string aliasToDelete = alias;

            // MessageBox to confirm task
            MessageBoxes messageBoxes = new MessageBoxes();
            DialogResult dr = messageBoxes.MessageBoxConfirmToDELETE(aliasToDelete);

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
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Deleted user [{aliasToDelete.ToUpper()}]");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Deleted user [{aliasToDelete.ToUpper()}]";
                path.AppendToLog(newLog);
            }
            else
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [UNKNOWN]: Deleted user [{aliasToDelete.ToUpper()}]");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},[UNKNOWN],Deleted user [{aliasToDelete.ToUpper()}]";
                path.AppendToLog(newLog);
            }

            messageBoxes.MessageDeleteSucces(); // Show MessageBox Delete Succes
            adminControl.ReloadListBoxAdmin(userIndex);
            adminControl.EmptyTextBoxes();
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
