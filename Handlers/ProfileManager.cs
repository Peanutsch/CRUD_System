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
    /// <summary>
    /// Manages user profile operations, such as updating, deleting, and generating new passwords.
    /// </summary>
    internal class ProfileManager
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        MessageBoxes message = new MessageBoxes();
        UserRepository userRepository = new UserRepository();
        Repository_LogEvents logEvents = new Repository_LogEvents();
        InteractionHandler interactionHandler = new InteractionHandler();
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public ProfileManager()
        {
            // 
        }
        #endregion CONSTRUCTOR
        /// <summary>
        /// Returns path userLines and loginLines
        /// </summary>
        /// <returns></returns>
        public (List<string> userLines, List<string> loginLines) ReadUserAndLoginData()
        {
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();
            return (userLines, loginLines);
        }

        /// <summary>
        /// Updates user details in the data_users.csv
        /// </summary>
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
                // Write updated data back to data_users.csv
                File.WriteAllLines(path.UserFilePath, userLines); 

                logEvents.LogEventUpdateUserDetails(currentUser, alias);
                message.MessageUpdateSucces();
            }
            else
            {
                message.MessageSomethingWentWrong();
                return;
            }
        }

        /// <summary>
        /// Method for deleting user from data_users.csv and data_log.csv
        /// </summary>
        /// /// <param name="alias">Alias of the user to delete.</param>
        public void DeleteUser(string alias)
        {
            AdminInterface adminInterface = new AdminInterface();

            var currentUser = LoginHandler.CurrentUser;

            // Get alias to delete from the selected user
            string aliasToDelete = alias;

            // Read lines from data_users.csv and data_login.csv
            (var userLines, var loginLines) = ReadUserAndLoginData();

            // Find user index
            int userIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, aliasToDelete);

            // MessageBox to confirm task
            DialogResult dr = message.MessageBoxConfirmToDELETE(aliasToDelete);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            // Remove the user from data_users.csv by alias
            userLines = userLines.Where(
                                        line =>
                                        !line.Split(',')[2].Trim().Equals(aliasToDelete,
                                        StringComparison.OrdinalIgnoreCase)).ToList();
            
            // Remove the user from data_login.csv using the alias
            loginLines = loginLines.Where(
                                            line =>
                                            !line.Split(',')[0].Trim().Equals(aliasToDelete,
                                            StringComparison.OrdinalIgnoreCase)).ToList();
            

            if (!string.IsNullOrEmpty(currentUser))
            {
                // Delete account from data_users.csv and data_login.csv
                File.WriteAllLines(path.UserFilePath, userLines);
                File.WriteAllLines(path.LoginFilePath, loginLines);

                // Log events
                logEvents.LogEventDeleteUser(currentUser, aliasToDelete);
                message.MessageDeleteSucces(aliasToDelete); // Show MessageBox Delete Succes
            }
            else
            {
                message.MessageSomethingWentWrong();
                return;
            }
            adminInterface.ReloadListBoxAdmin(userIndex);
        }

        /// <summary>
        /// Generates a new password for a user and logs the event.
        /// </summary>
        /// <param name="alias">Alias of the user for whom to generate a password.</param>
        /// <param name="isAdmin">Indicates if the user has admin privileges.</param>
        public void GenerateNewPassword(string alias, bool isAdmin)
        {
            var currentUser = LoginHandler.CurrentUser;

            // Read lines from data_users.csv
            (var userLines, var loginLines) = ReadUserAndLoginData();

            // Find user index
            int loginIndex = userRepository.FindUserIndexByAlias(userLines, loginLines, alias);
            var loginDetails = loginLines[loginIndex].Split(",");

            DialogResult dr = message.MessageBoxConfirmToGeneratePassword(loginDetails[0]);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            if (!string.IsNullOrEmpty(currentUser))
            {
                // Update password
                string generatedPassword = PasswordManager.PasswordGenerator();
                loginLines[loginIndex] = $"{loginDetails[0]},{generatedPassword},{isAdmin}";
                
                // Write updated data back to data_login.csv
                File.WriteAllLines(path.LoginFilePath, loginLines); 

                // Log event
                logEvents.LogEventPasswordGenerated(currentUser, loginDetails[0]);
                message.MessageChangePasswordSucces(loginDetails[0]);
            }
            else
            {
                message.MessageSomethingWentWrong();
                return;
            }
        }

        #region SAVE NEW USER
        /// <summary>
        /// Saves a new user by collecting input data from the form, generating an alias and password,
        /// and appending the new user data to the relevant CSV files. It also logs the event and shows
        /// appropriate messages based on the result.
        /// </summary>
        public void SaveNewUser(string Name, string Surname,
                                string Address, string ZIPCode,
                                string City, string Email,
                                string Phonenumber, bool isAdmin)
        {
            if (!ValidateUserInput(Name, Surname))
            {
                return;
            }

            string isAlias = GenerateAlias(Name, Surname);
            string isPassword = PasswordManager.PasswordGenerator();

            if (ConfirmNewUserCreation(isAlias))
            {
                SaveUserData(isAlias, isPassword, Name, Surname, Address, ZIPCode, City, Email, Phonenumber, isAdmin);
                LogNewAccountCreation(isAlias);
                message.MessageNewAccountSucces(isAlias);
            }
        }

        /// <summary>
        /// Validates the user input to ensure that required fields are not empty.
        /// </summary>
        private bool ValidateUserInput(string name, string surname)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                message.MessageInvalidInput();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Generates a unique alias for the user based on their name and surname.
        /// </summary>
        private string GenerateAlias(string name, string surname)
        {
            return userRepository.CreateTXTAlias(name, surname);
        }

        /// <summary>
        /// Confirms the creation of the new user with a dialog box.
        /// </summary>
        private bool ConfirmNewUserCreation(string alias)
        {
            DialogResult dr = message.MessageBoxConfirmNewUser(alias);
            return dr == DialogResult.Yes;
        }

        /// <summary>
        /// Saves the new user data to the CSV files.
        /// </summary>
        private void SaveUserData(string alias, string password, string name, string surname,
                                   string address, string zipCode, string city, string email,
                                   string phoneNumber, bool isAdmin)
        {
            string newDataLogin = $"{alias},{password},{isAdmin}";
            string newDataUsers = $"{name},{surname},{alias},{address},{zipCode},{city},{email},{phoneNumber}";

            File.AppendAllText(path.UserFilePath, newDataUsers + Environment.NewLine);
            File.AppendAllText(path.LoginFilePath, newDataLogin + Environment.NewLine);
        }

        /// <summary>
        /// Logs the creation of a new user account.
        /// </summary>
        private void LogNewAccountCreation(string alias)
        {
            var currentUser = LoginHandler.CurrentUser;
            if (!string.IsNullOrEmpty(currentUser))
            {
                logEvents.NewAccount(currentUser, alias);
            }
            else
            {
                message.MessageSomethingWentWrong();
            }
        }
        #endregion SAVE NEW USER
    }
}
