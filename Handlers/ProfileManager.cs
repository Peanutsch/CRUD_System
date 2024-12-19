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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
        /// Updates user details and login data.
        /// </summary>
        public void UpdateUserDetails(List<string> userLines, List<string> loginLines, int userIndex, int loginIndex,
                               string name, string surname, string alias, string address, string zipCode, string city,
                               string email, string phoneNumber, bool isAdmin, bool onlineStatus, bool isSick, bool isTheOne)
        {
            // Ensure the cache is loaded with decrypted data before proceeding
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            try
            {
                // Update user details in the cached data
                UpdateCachedUserDetails(alias, name, surname, address, zipCode, city, email, phoneNumber, onlineStatus, isSick);

                // Update "The One" status if it has been modified
                if (AdminMainControl.ChkIsTheOneChanged)
                {
                    UpdateCachedLoginDetails(alias, isTheOne);
                    AdminMainControl.ChkIsTheOneChanged = false;
                }

                // Save updated data and log the changes
                SaveDataAndLogUpdates(alias);

                // Reload the UI and maintain selection
                ReloadUIWithSelection(alias);
            }
            catch (Exception ex)
            {
                // Log the error and notify the user
                Debug.WriteLine($"Error while updating user {alias}: {ex}");
                message.MessageSomethingWentWrong();
            }
        }

        /// <summary>
        /// Updates the user's details in the cached user data.
        /// </summary>
        private void UpdateCachedUserDetails(string alias, string name, string surname, string address, string zipCode,
                                      string city, string email, string phoneNumber, bool onlineStatus, bool isSick)
        {
            // Confirm with the user before saving changes
            DialogResult dr = message.MessageConfirmToSAVEChanges(alias);
            if (dr == DialogResult.No)
            {
                return;
            }

            // Find and update the user in the cached data
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
        }

        /// <summary>
        /// Updates the login details of the user, including "The One" status.
        /// </summary>
        /// <param name="alias">The alias of the user to update.</param>
        /// <param name="isTheOne">Indicates if the user has special "The One" status.</param>
        private void UpdateCachedLoginDetails(string alias, bool isTheOne)
        {
            // Confirm with the user before modifying "The One" status
            DialogResult dr = message.MessageConfirmIsTheOne(alias);
            if (dr == DialogResult.No)
            {
                return;
            }

            // Find and update the login details in the cached data
            var login = cache.CachedLoginData.FirstOrDefault(l => l[0] == alias);
            if (login != null)
            {
                if (AdminInterface.SelectedUserIsAdmin)
                {
                    login[4] = AdminMainControl.IsTheOne.ToString();
                    Debug.WriteLine($"Updated 'The One' status to {login[4]}");

                    var currentUser = AuthenticationService.CurrentUser;
                    logEvents.LogEventUpdateStatusIsTheOne(currentUser!, alias);
                }
            }
        }

        /// <summary>
        /// Saves the updated user and login data, and logs the changes.
        /// </summary>
        /// <param name="alias">The alias of the updated user.</param>
        private void SaveDataAndLogUpdates(string alias)
        {
            // Save and encrypt the updated data
            cache.SaveAndEncryptData();

            // Log the changes
            var currentUser = AuthenticationService.CurrentUser;
            logEvents.LogEventUpdateUserDetails(currentUser!, alias);

            // Notify the user of a successful update
            message.MessageUpdateSucces();
        }

        /// <summary>
        /// Reloads the UI and ensures the updated user remains selected in the list.
        /// </summary>
        /// <param name="alias">The alias of the updated user.</param>
        private void ReloadUIWithSelection(string alias)
        {
            AdminInterface adminInterface = new AdminInterface();
            adminInterface.ReloadListBoxWithSelection(alias);
        }

        #endregion UPDATE USER DETAILS

        #region DELETE USER
        /// <summary>
        /// Deletes a user from the data files (data_users.csv and data_login.csv) 
        /// after confirming the action, decrypting the files, and updating the data.
        /// </summary>
        /// <param name="aliasToDelete">The alias of the user to be deleted.</param>
        public void DeleteUser(string aliasToDelete)
        {
            // Confirm deletion and validate the current user
            if (!ConfirmAndValidate(aliasToDelete))
                return;

            // Decrypt the user and login data files
            EncryptionManager.DecryptFile(path.UserFilePath);
            EncryptionManager.DecryptFile(path.LoginFilePath);

            // Remove the user from the data
            var (updatedUserLines, updatedLoginLines) = RemoveUserFromData(aliasToDelete);

            // Save the updated data and re-encrypt the files
            SaveAndEncryptDataFiles(updatedUserLines, updatedLoginLines);

            // Generate a report and log the deletion event
            ReportAndLogDeletion(aliasToDelete);

            // Reload the cache with the updated data
            cache.LoadDecryptedData();
        }

        /// <summary>
        /// Confirms the deletion of a user and validates the current logged-in user.
        /// </summary>
        /// <param name="aliasToDelete">The alias of the user to be deleted.</param>
        /// <returns>True if the confirmation and validation succeed; otherwise, false.</returns>
        private bool ConfirmAndValidate(string aliasToDelete)
        {
            // Show a confirmation dialog for the deletion
            if (message.MessageConfirmToDELETE(aliasToDelete) == DialogResult.No)
                return false;

            // Validate the current logged-in user
            if (string.IsNullOrEmpty(AuthenticationService.CurrentUser))
            {
                message.MessageSomethingWentWrong();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Removes the user from the user and login data lists.
        /// </summary>
        /// <param name="aliasToDelete">The alias of the user to be removed.</param>
        /// <returns>
        /// A tuple containing the updated user data lines and login data lines.
        /// </returns>
        private (List<string> updatedUserLines, List<string> updatedLoginLines) RemoveUserFromData(string aliasToDelete)
        {
            // Read the existing data from the files
            var (userLines, loginLines) = path.ReadUserAndLoginData();

            // Filter out the user to be deleted from the user data
            var updatedUserLines = userLines.Where(line =>
                !line.Split(',')[2].Trim().Equals(aliasToDelete, StringComparison.OrdinalIgnoreCase)).ToList();

            // Filter out the user to be deleted from the login data
            var updatedLoginLines = loginLines.Where(line =>
                !line.Split(',')[0].Trim().Equals(aliasToDelete, StringComparison.OrdinalIgnoreCase)).ToList();

            return (updatedUserLines, updatedLoginLines);
        }

        /// <summary>
        /// Saves the updated user and login data back to the files and re-encrypts them.
        /// </summary>
        /// <param name="userLines">The updated user data lines.</param>
        /// <param name="loginLines">The updated login data lines.</param>
        private void SaveAndEncryptDataFiles(List<string> userLines, List<string> loginLines)
        {
            // Save the updated user data to the file
            File.WriteAllLines(path.UserFilePath, userLines);

            // Save the updated login data to the file
            File.WriteAllLines(path.LoginFilePath, loginLines);

            // Re-encrypt the user data file
            EncryptionManager.EncryptFile(path.UserFilePath);

            // Re-encrypt the login data file
            EncryptionManager.EncryptFile(path.LoginFilePath);
        }

        /// <summary>
        /// Generates a report and logs the deletion event.
        /// </summary>
        /// <param name="aliasToDelete">The alias of the deleted user.</param>
        private void ReportAndLogDeletion(string aliasToDelete)
        {
            string? currentUser = AuthenticationService.CurrentUser;

            // Create a deletion report
            string reportText = $"{DateTime.Today:dd-MM-yyyy},{DateTime.Now:HH:mm:ss}\n[{currentUser!.ToUpper()}],Deleted user [{aliasToDelete.ToUpper()}]";
            var adminControl = new AdminMainControl();
            ReportManager.ReportDeleteUser(aliasToDelete, "Account Deleted", reportText);

            // Show a success message to the user
            message.MessageDeleteSucces(aliasToDelete);
            
            // Log the deletion event
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

            // Generate a unique alias and password for the user
            string isAlias = GenerateAlias(Name, Surname);

            //=== PASSWORD TEMP ISALIAS ===//
            // Generate password
            string isPassword = PasswordManager.PasswordGenerator();
            //string isPassword = isAlias;

            // Default values new user
            bool onlineStatus = false;
            bool isSick = false;

            // Confirm the creation of the new user with the alias
            if (ConfirmNewUserCreation(isAlias))
            {
                Debug.WriteLine($"New account for Alias: {isAlias}");
                Debug.WriteLine($"Created Password: {isPassword}");
                Debug.WriteLine($"Welcome Email to {Email}");

                // Save the new user's data to the system (cache and file storage)
                SaveUserData(isAlias, isPassword, Name, Surname, Address, ZIPCode, City, Email, Phonenumber, isAdmin, onlineStatus, isSick);

                // Add the user to the cache and ListBoxAdmin
                string[] userDetails = new string[]
                {
                    Name, Surname, isAlias, Phonenumber, onlineStatus.ToString()
                };

                // log the creation of the new user
                LogNewAccountCreation(isAlias, isPassword, Email);

                // Notify the admin that the account creation was successful
                message.MessageNewAccountSucces(isAlias);

                AdminInterface adminInterface = new AdminInterface();
                AdminMainControl adminControl = new AdminMainControl();
                adminControl.listBoxAdmin.Items.Clear();
                adminInterface.ReloadListBoxWithSelection(isAlias);
                adminInterface.EditMode = false;
                adminInterface.InterfaceEditModeAdmin();
            }
            else
            {
                return;
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

            bool isTheOne = false;

            // Prepare the new data
            string newDataLogin = $"{alias},{password},{isAdmin},{onlineStatus},{isTheOne}";
            string newDataUsers = $"{name},{surname},{alias},{address},{zipCode},{city},{email},{phoneNumber},{onlineStatus},{isSick}";

            // Append the new data to the files
            File.AppendAllText(path.UserFilePath, newDataUsers + Environment.NewLine);
            File.AppendAllText(path.LoginFilePath, newDataLogin + Environment.NewLine);

            CreateEmailNewUser(alias, email, password);

            // Encrypt the user and login files again to secure the data
            EncryptionManager.EncryptFile(path.UserFilePath);
            EncryptionManager.EncryptFile(path.LoginFilePath);

            // Update the DataCache with the latest data
            DataCache.LoadCache();
        }

        /// <summary>
        /// Creates a new user and sends an email with login credentials.
        /// </summary>
        public void CreateEmailNewUser(string alias, string email, string password)
        {
            EmailManager emailManager = new EmailManager();

            // Prepare and send the welcome email
            string subject = "Your New Account Details";
            string body = $@"
            <html>
            <body>
                <h2>Welcome {alias}!</h2>
                <p>Your account has been created successfully.</p>
                <p><strong>Username:</strong> {alias}</p>
                <p><strong>Password:</strong> {password}</p>
                <p>Login to start using your account.</p>
            </body>
            </html>";

            emailManager.SendEmail(email, subject, body, isHtml: true);
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
        private void LogNewAccountCreation(string alias, string password, string email)
        {
            var currentUser = AuthenticationService.CurrentUser;
            if (!string.IsNullOrEmpty(currentUser))
            {
                AdminMainControl adminControl = new AdminMainControl();
                // Create default CSV files line = {string.Empty},{string.Empty},{string.Empty}
                //CreateCSVFiles.CreateCISNoticeCSV(alias);
                CreateCSVFiles.CreateLogCSV(alias); //, currentUser.ToUpper(), logEvent);

                // log event in {alias}_log.csv
                logEvents.NewAccount(currentUser, alias, password, email);

                // Temporary copy of logEvent in rtxReport
                string reportText = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")}\n[{currentUser!.ToUpper()}]," +
                                    $"Created user [{alias.ToUpper()}].\nSent email to {email} with password: {password}";
                ReportManager.ReportSaveNewUser(alias, "New User", reportText);
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
