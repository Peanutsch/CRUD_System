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
        readonly FilePaths path = new FilePaths();
        readonly RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        readonly AccountManager accountManager = new AccountManager();
        readonly RepositoryLogEvents logEvents = new RepositoryLogEvents();
        readonly DataCache cache = new DataCache();
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public ProfileManager()
        {
            //
        }
        #endregion CONSTRUCTOR

        #region UPDATE USER DETAILS
        /// <summary>
        /// Updates user details in data_users.csv
        /// Updates isAdmin in data_login.csv
        /// </summary>
        public void UpdateUserDetails(List<string> userLines, List<string> loginLines, int userIndex, int loginIndex,
                                      string name, string surname, string alias, string address, string zipCode, string city,
                                      string email, string phoneNumber, bool isAdmin, bool onlineStatus, bool isSick, bool isTheOne)
        {
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            DialogResult dr = message.MessageConfirmToSAVEChanges(alias);
            if (dr == DialogResult.No)
            {
                return;
            }

            var currentUser = AuthenticationService.CurrentUser;
            if (!string.IsNullOrEmpty(currentUser))
            {
                try
                {
                    Debug.WriteLine($"Looking for {alias} in CachedUserData");
                    var user = cache.CachedUserData.FirstOrDefault(u => u[2] == alias);
                    if (user != null)
                    {
                        user[0] = name;
                        user[1] = surname;
                        user[2] = alias;
                        user[3] = address;
                        user[4] = zipCode;
                        user[5] = city;
                        user[6] = email;
                        user[7] = phoneNumber;
                        user[8] = onlineStatus.ToString();
                        user[9] = isSick.ToString();
                    }

                    Debug.WriteLine("CachedLoginData:");
                    foreach (var line in cache.CachedLoginData)
                    {
                        Debug.WriteLine(string.Join(", ", line));
                    }

                    DialogResult result = message.MessageConfirmIsTheOne(alias);
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    Debug.WriteLine($"Looking for {alias} in CachedLoginData");
                    var login = cache.CachedLoginData.FirstOrDefault(l => l[0] == alias);
                    if (login != null)
                    {
                        Debug.WriteLine($"Found login: {string.Join(", ", login)}");

                        if (AdminInterface.SelectedUserIsAdmin)
                        {
                            login[4] = AdminMainControl.IsTheOne.ToString();
                            Debug.WriteLine($"Updated 'The One' status to: {login[4]}");
                        }
                    }

                    cache.SaveAndEncryptData();

                    AdminInterface adminInterface = new AdminInterface();
                    logEvents.LogEventUpdateUserDetails(currentUser, alias);
                    message.MessageUpdateSucces();
                    adminInterface.ReloadListBoxWithSelection(alias);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error while updating user {alias}: {ex}");
                    message.MessageSomethingWentWrong();
                }
            }
        }

        #endregion UPDATE USER DETAILS

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

            // Decrypt both user and login files before performing any actions on them
            EncryptionManager.DecryptFile(path.UserFilePath);
            EncryptionManager.DecryptFile(path.LoginFilePath);

            // Read the current data from the files
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            // Remove the specified user from the data
            (userLines, loginLines) = RemoveUserFromLines(userLines, loginLines, aliasToDelete);

            // Write the updated user data back to the files
            WriteUpdatedDataToFile(userLines, loginLines);

            // log the deletion event
            LogDeletion(currentUser, aliasToDelete);

            // Show a success message after deletion
            message.MessageDeleteSucces(aliasToDelete);

            // Re-encrypt the user and login files to ensure data security
            EncryptionManager.EncryptFile(path.UserFilePath);
            EncryptionManager.EncryptFile(path.LoginFilePath);

            // Reload Cache
            cache.LoadDecryptedData();
        }

        /// <summary>
        /// Prompts the user with a confirmation dialog for deletion.
        /// </summary>
        /// <param name="alias">Alias of the user to delete.</param>
        /// <returns>True if the user confirms the deletion; otherwise, false.</returns>
        private bool ConfirmDeletion(string alias)
        {
            // Show a confirmation dialog for deletion
            DialogResult dr = message.MessageConfirmToDELETE(alias);
            return dr == DialogResult.Yes;
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
            // log the deletion event, including the current user and the user being deleted
            logEvents.LogEventDeleteUser(currentUser, aliasToDelete);
        }
        #endregion DELETE USER

        #region GENERATE PASSWORD NEW USER
        /// <summary>
        /// Generates a new password for a user and logs the event.
        /// </summary>
        /// <param name="alias">Alias of the user for whom to generate a password.</param>
        /// <param name="isAdmin">Indicates if the user has admin privileges.</param>
        public void GeneratePasswordNewUser(string alias, bool isAdmin)
        {
            DialogResult dr = message.MessageConfirmToGeneratePassword(alias);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            // Check if the cache is empty, and reload data if necessary.
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            var currentUser = AuthenticationService.CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                // Update password
                string generatedPassword = PasswordManager.PasswordGenerator();

                // Find the user in the cached login data by alias and update their online status.
                var login = cache.CachedLoginData.FirstOrDefault(l => l[0] == alias); // Alias field
                if (login != null)
                {
                    login[1] = generatedPassword; // Update password
                    Debug.WriteLine($"Generated password for {alias}: {generatedPassword}");
                }

                // Save changes to the data files and encrypt them
                cache.SaveAndEncryptData();

                // log event
                logEvents.LogEventPasswordGenerated(currentUser, alias);
                message.MessageChangePasswordSucces(alias);
            }
            else
            {
                message.MessageSomethingWentWrong();
                return;
            }
        }
        #endregion GENERATE PASSWORD NEW USER

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
            Debug.WriteLine($"New account for Alias: {isAlias}");

            //=== PASSWORD TEMP ISALIAS ===//
            // Generate password
            string isPassword = PasswordManager.PasswordGenerator();
            //string isPassword = isAlias;
            Debug.WriteLine($"Created Password: {isPassword}");
            Debug.WriteLine($"Welcome Email to {Email}");

            // Default values new user
            bool onlineStatus = false;
            bool isSick = false;

            // Confirm the creation of the new user with the alias
            if (ConfirmNewUserCreation(isAlias))
            {
                // Save the new user's data to the system (cache and file storage)
                SaveUserData(isAlias, isPassword, Name, Surname, Address, ZIPCode, City, Email, Phonenumber, isAdmin, onlineStatus, isSick);

                // Add the user to the cache and ListBoxAdmin
                string[] userDetails = new string[]
                {
                    Name, Surname, isAlias, Phonenumber, onlineStatus.ToString()
                };

                // log the creation of the new user
                LogNewAccountCreation(isAlias);

                // Notify the admin that the account creation was successful
                message.MessageNewAccountSucces(isAlias);

                AdminInterface adminInterface = new AdminInterface();
                AdminMainControl adminControl = new AdminMainControl();
                adminControl.listBoxAdmin.Items.Clear();
                adminInterface.ReloadListBoxWithSelection(isAlias);
            }
        }

        /// <summary>
        /// Saves the new user data to the CSV files and ensures that data is encrypted.
        /// </summary>
        private void SaveUserData(string alias, string password, string name, string surname,
                                   string address, string zipCode, string city, string email,
                                   string phoneNumber, bool isAdmin, bool onlineStatus, bool isSick)
        {
            // Decrypt the user and login files before updating
            EncryptionManager.DecryptFile(path.UserFilePath);
            EncryptionManager.DecryptFile(path.LoginFilePath);

            // Prepare the new data
            string newDataLogin = $"{alias},{password},{isAdmin},{onlineStatus}";
            string newDataUsers = $"{name},{surname},{alias},{address},{zipCode},{city},{email},{phoneNumber},{onlineStatus},{isSick}";

            // Append the new data to the files
            File.AppendAllText(path.UserFilePath, newDataUsers + Environment.NewLine);
            File.AppendAllText(path.LoginFilePath, newDataLogin + Environment.NewLine);

            // Encrypt the user and login files again to secure the data
            EncryptionManager.EncryptFile(path.UserFilePath);
            EncryptionManager.EncryptFile(path.LoginFilePath);

            // ** Update the DataCache with the latest data **
            DataCache.LoadCache();
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
            DialogResult dr = message.MessageConfirmNewUser(alias);
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
                // Create default CSV files line = {string.Empty},{string.Empty},{string.Empty}
                CreateCSVFiles.CreateCISNoticeCSV(alias);
                CreateCSVFiles.CreateLogCSV(alias); //, currentUser.ToUpper(), logEvent);

                // log event in {alias}_log.csv
                logEvents.NewAccount(currentUser, alias);
            }
        }
        #endregion SAVE NEW USER

        #region ABSENCE DUE ILLNESS
        public void AbsenceDueIllness(bool isSick, string alias)
        {
            // Confirm the action
            DialogResult dr = message.MessageConfirmCallInSickNotification(alias);
            if (dr == DialogResult.No)
            {
                return;
            }

            // Check if the cache is empty and reload if necessary
            if (cache.CachedCisData.Count == 0)
            {
                cache.LoadDecryptedCISData(alias);
                MessageBox.Show($"CachedCisData: {cache.CachedCisData.Count} items");
            }

            MessageBox.Show($"ProfileManager.AbsenceDueIllness> isSick: {isSick}. User {alias} on Absence due Illness");

            DateTime date_sick = DateTime.Today;

            // Update the sick leave notification
            var sickLeaveNotification = cache.CachedCisData.FirstOrDefault(n => n[0] == alias);
            if (sickLeaveNotification != null)
            {
                sickLeaveNotification[1] = date_sick.ToString("yyyy-MM-dd"); // Update start date of sick leave
                sickLeaveNotification[2] = isSick ? "Pending" : "Recovered"; // Update sick leave status
            }
            else
            {
                // Add a new sick leave entry if not found
                cache.CachedCisData.Add(new string[] { alias, date_sick.ToString("yyyy-MM-dd"), "Pending" });
            }

            // Decrypt the file for updates
            EncryptionManager.DecryptFile(path.FileCisNotices!);

            // Append the updated data to the file
            foreach (var notification in cache.CachedCisData)
            {
                string dataLine = string.Join(",", notification); // Combine array into CSV line
                File.AppendAllText(path.FileCisNotices!, dataLine + Environment.NewLine);
            }

            // Encrypt the file after updates
            EncryptionManager.EncryptFile(path.FileCisNotices!);

            // Update the cache with the latest data
            DataCache.LoadCache();
        }
        #endregion ABSENCE DUE ILLNESS

        #region IS THE ONE
        /// <summary>
        /// Updates the 'Is The One' status for a specific user identified by their alias.
        /// Ensures the cached login data is loaded, modifies the relevant entry, 
        /// and saves the changes to the encrypted data files.
        /// </summary>
        /// <param name="selectedAlias">The alias of the user to update.</param>
        /// <param name="isTheOne">The new status indicating whether the user is "The One".</param>
        public void IsTheOne(string selectedAlias, bool isTheOne)
        {
            AdminInterface adminInterface = new AdminInterface();
            // Check if the cached login data is empty. If so, load the decrypted data.
            if (cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            DialogResult dr = message.MessageConfirmIsTheOne(selectedAlias);
            if (dr == DialogResult.No)
            {
                return;
            }

            // Initialize an instance of AdminMainControl (if necessary for further use).
            //AdminMainControl adminControl = new AdminMainControl();

            // Locate the user in the cached login data by matching their alias.
            var login = cache.CachedLoginData.FirstOrDefault(l => l[0] == selectedAlias); // Alias is in the first field (index 0)

            if (login != null && AdminInterface.SelectedUserIsAdmin)
            {
                Debug.WriteLine($"***\nloginLine before: {login}");
                // Update 'IsTheOne' status
                login[4] = isTheOne.ToString();
                Debug.WriteLine($"login[4]: {login[4]}");
                Debug.WriteLine($"loginLine after: {login}\n***");
            }
            else
            {
                Debug.WriteLine($"login != null and selected user must be Admin");
                Debug.WriteLine($"login: {login} Selected User: {AdminInterface.SelectedUserIsAdmin}");
            }

            // Save the updated login data and encrypt it for security.
            cache.SaveAndEncryptData();

            // Notify the user that the update was successful.
            message.MessageUpdateSucces();
        }

        #endregion IS THE ONE
    }
}
