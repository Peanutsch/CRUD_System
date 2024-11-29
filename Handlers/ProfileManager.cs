using CRUD_System.FileHandlers;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CRUD_System.Interfaces;
using CRUD_System.Repositories;

namespace CRUD_System.Handlers
{
    /// <summary>
    /// Manages user profile operations, such as updating, deleting, and generating new passwords.
    /// </summary>
    internal class ProfileManager
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        AccountManager accountManager = new AccountManager();
        RepositoryLogEvents logEvents = new RepositoryLogEvents();
        FormInteractionHandler interactionHandler = new FormInteractionHandler();
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public ProfileManager()
        {
            // 
        }
        #endregion CONSTRUCTOR

        #region OPERATIONS
        /// <summary>
        /// Updates user details in data_users.csv
        /// Updates isAdmin in data_login.csv
        /// </summary>
        public void UpdateUserDetails(List<string> userLines, List<string> loginLines, int userIndex, int loginIndex, string name, string surname, string alias, string address, string zipCode, string city, string email, string phoneNumber, bool isAdmin, bool onlineStatus)
        {
            var loginDetails = loginLines[loginIndex].Split(",");
            string currentAlias = loginDetails[0];
            string currentPassword = loginDetails[1];
            bool currentOnlineStatus = onlineStatus;

            userLines[userIndex] = $"{name},{surname},{alias},{address},{zipCode.ToUpper()},{city},{email},{phoneNumber},{currentOnlineStatus}";
            loginLines[loginIndex] = $"{currentAlias},{currentPassword},{isAdmin},{onlineStatus}";

            DialogResult dr = message.MessageBoxConfirmToSAVEChanges(alias);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            var currentUser = AuthenticationService.CurrentUser;
            if (!string.IsNullOrEmpty(currentUser))
            {
                // Write updated data back to data_users.csv
                File.WriteAllLines(path.UserFilePath, userLines); 
                File.WriteAllLines(path.LoginFilePath, loginLines);

                logEvents.LogEventUpdateUserDetails(currentUser, alias);
                message.MessageUpdateSucces();
            }
            else
            {
                message.MessageSomethingWentWrong();
                return;
            }
        }

        #region DELETE USER
        /// <summary>
        /// Deletes a user from the data files (data_users.csv and data_login.csv), 
        /// after confirming the action and performing necessary decryption and encryption.
        /// </summary>
        /// <param name="alias">The alias of the user to delete.</param>
        public void DeleteUser(string alias)
        {
            AdminInterface adminInterface = new AdminInterface();
            var currentUser = AuthenticationService.CurrentUser;

            string aliasToDelete = alias;

            // Confirm the deletion with the user
            if (!ConfirmDeletion(aliasToDelete))
            {
                return;
            }

            // If current user is not valid, show error and exit
            if (string.IsNullOrEmpty(currentUser))
            {
                message.MessageSomethingWentWrong();
                return;
            }

            // Decrypt files before modifying
            DecryptFiles();

            // Read the current data from the files
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            // Remove the specified user from the data
            (userLines, loginLines) = RemoveUserFromLines(userLines, loginLines, aliasToDelete);

            // Write the updated user data back to the files
            WriteUpdatedDataToFile(userLines, loginLines);

            // Log the deletion event
            LogDeletion(currentUser, aliasToDelete);

            // Show a success message after deletion
            message.MessageDeleteSucces(aliasToDelete);

            // Re-encrypt the files after modification
            ReEncryptFiles();

            // Reload the list box to reflect the updated data
            adminInterface.ReloadListBoxAdmin(accountManager.FindUserIndexByAlias(userLines, loginLines, aliasToDelete));
        }

        /// <summary>
        /// Prompts the user with a confirmation dialog for deletion.
        /// </summary>
        /// <param name="alias">Alias of the user to delete.</param>
        /// <returns>True if the user confirms the deletion; otherwise, false.</returns>
        private bool ConfirmDeletion(string alias)
        {
            // Show a confirmation dialog for deletion
            DialogResult dr = message.MessageBoxConfirmToDELETE(alias);
            return dr == DialogResult.Yes;
        }

        /// <summary>
        /// Decrypts the data files (data_users.csv and data_login.csv) before any modifications.
        /// </summary>
        private void DecryptFiles()
        {
            // Decrypt both user and login files before performing any actions on them
            EncryptionManager.DecryptFile(path.UserFilePath);
            EncryptionManager.DecryptFile(path.LoginFilePath);
        }

        /// <summary>
        /// Removes the user with the specified alias from the given user and login data.
        /// </summary>
        /// <param name="userLines">List of user data lines.</param>
        /// <param name="loginLines">List of login data lines.</param>
        /// <param name="aliasToDelete">Alias of the user to delete.</param>
        /// <returns>Updated user and login data without the deleted user.</returns>
        private (List<string>, List<string>) RemoveUserFromLines(List<string> userLines, List<string> loginLines, string aliasToDelete)
        {
            // Remove the user from userLines based on the alias
            userLines = userLines.Where(line => !line.Split(',')[2].Trim().Equals(aliasToDelete, StringComparison.OrdinalIgnoreCase)).ToList();

            // Remove the user from loginLines based on the alias
            loginLines = loginLines.Where(line => !line.Split(',')[0].Trim().Equals(aliasToDelete, StringComparison.OrdinalIgnoreCase)).ToList();

            return (userLines, loginLines);
        }

        /// <summary>
        /// Writes the updated user and login data back to the files.
        /// </summary>
        /// <param name="userLines">Updated user data lines.</param>
        /// <param name="loginLines">Updated login data lines.</param>
        private void WriteUpdatedDataToFile(List<string> userLines, List<string> loginLines)
        {
            // Write the updated user and login data back to the respective files
            File.WriteAllLines(path.UserFilePath, userLines);
            File.WriteAllLines(path.LoginFilePath, loginLines);
        }

        /// <summary>
        /// Logs the event of deleting a user.
        /// </summary>
        /// <param name="currentUser">The alias of the user who is performing the deletion.</param>
        /// <param name="aliasToDelete">The alias of the user being deleted.</param>
        private void LogDeletion(string currentUser, string aliasToDelete)
        {
            // Log the deletion event, including the current user and the user being deleted
            logEvents.LogEventDeleteUser(currentUser, aliasToDelete);
        }

        /// <summary>
        /// Re-encrypts the data files after modifications.
        /// </summary>
        private void ReEncryptFiles()
        {
            // Re-encrypt the user and login files to ensure data security
            EncryptionManager.EncryptFile(path.UserFilePath);
            EncryptionManager.EncryptFile(path.LoginFilePath);
        }

        /// <summary>
        /// Removes the user with the specified alias from the listBoxAdmin.
        /// </summary>
        /// <param name="aliasToDelete">The alias of the user to remove from the list box.</param>
        public void RemoveUserFromListBoxAdmin(string aliasToDelete)
        {
            // Create a new instance of the AdminMainControl to access the listBoxAdmin
            AdminMainControl adminControl = new AdminMainControl();

            // Iterate through the items in the listBoxAdmin to find the user
            for (int index = 0; index < adminControl.listBoxAdmin.Items.Count; index++)
            {
                // Check if the list item is not null and contains the alias to delete
                var item = adminControl.listBoxAdmin.Items[index];
                if (item != null)
                {
                    string? itemString = item.ToString();
                    if (itemString != null && itemString.Contains($"({aliasToDelete})"))
                    {
                        // Remove the item (user) from the list box if a match is found
                        adminControl.listBoxAdmin.Items.RemoveAt(index);
                        break; // Exit the loop after removing the user
                    }
                }
            }
        }

        /*
        public void DeleteUser(string alias)
        {
            AdminInterface adminInterface = new AdminInterface();

            var currentUser = AuthenticationService.CurrentUser;

            // Get alias to delete from the selected user
            string aliasToDelete = alias;

            // Decrypt the files before modifying
            EncryptionManager.DecryptFile(path.UserFilePath);
            EncryptionManager.DecryptFile(path.LoginFilePath);

            // Read lines from data_users.csv and data_login.csv
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            // Find user index
            int userIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, aliasToDelete);

            // MessageBox to confirm task
            DialogResult dr = message.MessageBoxConfirmToDELETE(aliasToDelete);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            if (!string.IsNullOrEmpty(currentUser))
            {
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

                // Delete account from data_users.csv and data_login.csv
                File.WriteAllLines(path.UserFilePath, userLines);
                File.WriteAllLines(path.LoginFilePath, loginLines);

                // Log events
                logEvents.LogEventDeleteUser(currentUser, aliasToDelete);
                message.MessageDeleteSucces(aliasToDelete); // Show MessageBox Delete Success
            }
            else
            {
                message.MessageSomethingWentWrong();
                return;
            }

            // Re-encrypt the files after modifying them
            EncryptionManager.EncryptFile(path.UserFilePath);
            EncryptionManager.EncryptFile(path.LoginFilePath);

            adminInterface.ReloadListBoxAdmin(userIndex);
        }
        */
        #endregion DELETE USER

        /// <summary>
        /// Generates a new password for a user and logs the event.
        /// </summary>
        /// <param name="alias">Alias of the user for whom to generate a password.</param>
        /// <param name="isAdmin">Indicates if the user has admin privileges.</param>
        public void GenerateNewPassword(string alias, bool isAdmin)
        {
            var currentUser = AuthenticationService.CurrentUser;

            // Read lines from data_users.csv and data_login.csv
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            // Find user index
            int loginIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, alias);
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
        #endregion OPERATIONS

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
            // Validate user input (e.g., required fields, format, etc.)
            if (!ValidateUserInput(Name, Surname))
            {
                return;
            }

            // Generate a unique alias and password for the user
            string isAlias = GenerateAlias(Name, Surname);

            // TEMP alias as password. Will be replaced with password generator
            //string isPassword = PasswordManager.PasswordGenerator();
            string isPassword = isAlias;

            // Default new user to offline
            bool onlineStatus = false;

            // Confirm the creation of the new user with the alias
            if (ConfirmNewUserCreation(isAlias))
            {
                // Save the new user's data to the system (cache and file storage)
                SaveUserData(isAlias, isPassword, Name, Surname, Address, ZIPCode, City, Email, Phonenumber, isAdmin, onlineStatus);

                // Add the user to the cache and ListBoxAdmin
                string[] userDetails = new string[]
                {
                    Name, Surname, isAlias, Phonenumber, onlineStatus.ToString()
                };

                // Log the creation of the new user
                LogNewAccountCreation(isAlias);

                // Notify the admin that the account creation was successful
                message.MessageNewAccountSucces(isAlias);

                // ** Reload the ListBox with the updated cache **
                AdminInterface adminInterface = new AdminInterface();
                adminInterface.LoadDetailsListBox();
            }
        }

        /// <summary>
        /// Saves the new user data to the CSV files and ensures that data is encrypted.
        /// </summary>
        private void SaveUserData(string alias, string password, string name, string surname,
                                   string address, string zipCode, string city, string email,
                                   string phoneNumber, bool isAdmin, bool onlineStatus)
        {
            // Decrypt the user and login files before updating
            EncryptionManager.DecryptFile(path.UserFilePath);
            Debug.WriteLine("DataCache.SaveUserData> data_users.csv DECRYPTED");

            EncryptionManager.DecryptFile(path.LoginFilePath);
            Debug.WriteLine("DataCache.SaveUserData> data_login.csv DECRYPTED");

            // Prepare the new data
            string newDataLogin = $"{alias},{password},{isAdmin},{onlineStatus}";
            string newDataUsers = $"{name},{surname},{alias},{address},{zipCode},{city},{email},{phoneNumber},{onlineStatus}";

            // Append the new data to the files
            File.AppendAllText(path.UserFilePath, newDataUsers + Environment.NewLine);
            File.AppendAllText(path.LoginFilePath, newDataLogin + Environment.NewLine);

            // Encrypt the user and login files again to secure the data
            EncryptionManager.EncryptFile(path.UserFilePath);
            Debug.WriteLine("DataCache.SaveUserData> data_users.csv ENCRYPTED");

            EncryptionManager.EncryptFile(path.LoginFilePath);
            Debug.WriteLine("DataCache.SaveUserData> data_login.csv ENCRYPTED");

            // ** Update the DataCache with the latest data **
            DataCache.LoadCache();
            Debug.WriteLine("DataCache updated after saving new user.");
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
        /// Generates a unique alias by calling the accountManager's CreateTXTAlias method.
        /// </summary>
        /// <param name="name">The user's first name.</param>
        /// <param name="surname">The user's surname.</param>
        /// <returns>A generated alias string.</returns>
        private string GenerateAlias(string name, string surname)
        {
            return accountManager.CreateTXTAlias(name, surname);
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
        /// Logs the creation of a new user account.
        /// </summary>
        private void LogNewAccountCreation(string alias)
        {
            var currentUser = AuthenticationService.CurrentUser;
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
