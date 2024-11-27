using CRUD_System.FileHandlers;
using CRUD_System.Interfaces;
using CRUD_System.Repositories;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CRUD_System.Handlers
{
    /// <summary>
    /// Responsible for managing user authentication, login validation, and session handling.
    /// It validates user credentials, checks login status, updates the online status of users, and handles user login and logout processes.
    /// This class also determines user roles (admin or regular user) and manages session transitions, including updating the user interface of the form with the appropriate user information.
    /// </summary>
    public class AuthenticationService
    {
        #region PROPERTIES
        public static string? CurrentUser { get; set; }

        FilePaths path = new FilePaths();
        RepositoryLogEvents logEvents = new RepositoryLogEvents();
        RepositoryMessageBoxes message = new RepositoryMessageBoxes();

        public bool onlineStatus = false;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public AuthenticationService()
        {

        }
        #endregion CONSTRUCTOR

        #region LOGIN VALIDATION
        /// <summary>
        /// Validates the provided username and password by checking them against the stored data in the CSV.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        /// <param name="inputUserPassword">The password entered by the user.</param>
        /// <returns>True if the credentials are valid; otherwise, false.</returns>
        public bool ValidateLogin(string inputUserName, string inputUserPassword)
        {
            // Find the user in the list where both username and password match
            var user = ReadFiles.LoginData.FirstOrDefault(u =>
                                                u.Username.Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                                                u.Password == inputUserPassword);

            // If user is found, credentials are valid
            return user != default;
        }

        /// <summary>
        /// Determines if the logged-in user is an admin.
        /// </summary>
        /// <param name="inputUserName">The username of the user.</param>
        /// <param name="inputUserPassword">The password of the user.</param>
        /// <returns>True if the user is an admin; otherwise, false.</returns>
        public bool IsAdmin(string inputUserName, string inputUserPassword)
        {
            // Find the user in the list where both username and password match
            var user = ReadFiles.LoginData.FirstOrDefault(u =>
                u.Username.Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == inputUserPassword);

            // Return the admin status if the user is found
            return user != default && user.IsAdmin;
        }

        /// <summary>
        /// Checks if the user is offline.
        /// </summary>
        /// <param name="inputUserName">The username to check.</param>
        /// <returns>True if the user is offline; otherwise, false.</returns>
        public bool ValidateOnlineStatus(string inputUserName, string inputUserPassword)
        {
            // Find the user in the list where both username and password match
            var user = ReadFiles.LoginData.FirstOrDefault(u =>
                u.Username.Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == inputUserPassword);

            return user != default && user.OnlineStatus;
        }
        #endregion LOGIN VALIDATION

        #region LOGIN
        /// <summary>
        /// Updates the online status of a user by alias in both login and user data files.
        /// If the user is not found, displays an error message.
        /// </summary>
        /// <param name="alias">The alias of the user whose online status needs to be updated.</param>
        /// <param name="onlineStatus">The new online status to set for the user (true for online, false for offline).</param>
        public void UpdateUserOnlineStatus(string alias, bool onlineStatus)
        {
            AccountManager accountManager = new AccountManager();
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            // Find user index
            int accountIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, alias);

            if (accountIndex == -1)
            {
                return;
            }

            var loginDetails = loginLines[accountIndex].Split(",");
            var userDetails = userLines[accountIndex].Split(",");


            // Update online status in data_login.csv using string.Join
            loginDetails[3] = onlineStatus.ToString(); // Update only the onlineStatus field
            loginLines[accountIndex] = string.Join(",", loginDetails);

            // Update online status in data_user,csv using string.Join
            userDetails[8] = onlineStatus.ToString(); // Update only the onlineStatus field
            userLines[accountIndex] = string.Join(",", userDetails);

            // Update details in data_users.csv and data_login.csv
            File.WriteAllLines(path.LoginFilePath, loginLines);
            File.WriteAllLines(path.UserFilePath, userLines);
        }

        /// <summary>
        /// Authenticates the user's login credentials and handles login processing.
        /// </summary>
        /// <param name="inputUserName">The username input provided by the user.</param>
        /// <param name="inputUserPassword">The password input provided by the user.</param>
        public void AuthenticateUser(string inputUserName, string inputUserPassword)
        {
            if (!ValidateUserLogin(inputUserName, inputUserPassword))
            {
                return;
            }

            ProcessSuccessfulLogin(inputUserName, inputUserPassword);
        }

        /// <summary>
        /// Validates the user's login credentials and online status.
        /// </summary>
        /// <param name="inputUserName">The username input provided by the user.</param>
        /// <param name="inputUserPassword">The password input provided by the user.</param>
        /// <returns>True if the user credentials and status are valid; otherwise, false.</returns>
        private bool ValidateUserLogin(string inputUserName, string inputUserPassword)
        {
            LoginForm loginForm = new LoginForm();

            if (!ValidateLogin(inputUserName, inputUserPassword))
            {
                message.MessageInvalidNamePassword();
                loginForm.ShowDialog(); // Reopen LoginForm for retry
                return false;
            }
            if (ValidateOnlineStatus(inputUserName, inputUserPassword))
            {
                message.MessageUserAlreadyOnline(inputUserName);
                loginForm.ShowDialog(); // Reopen LoginForm for retry
                return false;
            }
            return true;
        }

        /// <summary>
        /// Processes actions upon a successful login.
        /// Updates the user's online status, logs the login event, 
        /// and directs the user to the admin or user interface based on their role.
        /// </summary>
        /// <param name="inputUserName">The validated username.</param>
        /// <param name="inputUserPassword">The validated password.</param>
        private void ProcessSuccessfulLogin(string inputUserName, string inputUserPassword)
        {
            CurrentUser = inputUserName.ToLower();

            // Online Status = true
            UpdateUserOnlineStatus(CurrentUser, true);

            logEvents.UserLoggedIn(CurrentUser);

            bool isAdmin = IsAdmin(inputUserName, inputUserPassword);
            if (isAdmin) // Send to admin interface
            {
                AdminMainForm adminForm = new AdminMainForm();
                DisplayUserAlias(adminForm, isAdmin);
                adminForm.ShowDialog();
            }
            else // Send to user interface
            {
                UserMainForm usersForm = new UserMainForm();
                DisplayUserAlias(usersForm, isAdmin);
                usersForm.ShowDialog();
            }
            // Encrypt data_login.csv file after a successful login
            EncryptionManager.EncryptFile(path.LoginFilePath); // Directly call the static method
        }

        /// <summary>
        /// Displays the current user's alias and role in the specified form.
        /// Updates the form's text fields with the username and sets the role label
        /// to indicate if the user is an Admin or a regular User.
        /// </summary>
        /// <param name="form">The form where the user information will be displayed.</param>
        /// <param name="isAdmin">Indicates if the current user has admin privileges.</param>
        public void DisplayUserAlias(dynamic form, bool isAdmin)
        {
            var currentUser = CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                form.textBoxUserName.Text = $"{currentUser.ToUpper()}";
            }
            else
            {
                form.textBoxUserName.Text = "UNKNOWN";
            }
            form.labelAlias.TextAlign = ContentAlignment.TopLeft;
            form.labelAlias.BackColor = isAdmin ? Color.LightGreen : Color.LightBlue;
            form.labelAlias.Text = isAdmin ? "Admin" : "User";
        }
        #endregion LOGIN

        #region LOGOUT
        public void ForceLogOut(string aliasToLogOut)
        {
            AdminMainControl adminControl = new AdminMainControl();
            AdminInterface adminInterface = new AdminInterface();
            AccountManager accountManager = new AccountManager();

            // Pass selectedAlias to SetForceLogOutUserBtn
            adminInterface.SetForceLogOutUserBtn(aliasToLogOut);

            // Set user as offline
            UpdateUserOnlineStatus(aliasToLogOut, false);
            // Force log out user
            this.PerformForcedLogOutByAdmin(aliasToLogOut);
        }

        /// <summary>
        /// Logs out the current user by updating their online status to offline,
        /// logging the logout event, and clearing the current user.
        /// </summary>
        public void PerformLogout()
        {
            var currentUser = CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                logEvents.UserLoggedOut(currentUser);
                UpdateUserOnlineStatus(currentUser, false);
                CurrentUser = null;
            }
        }

        /// <summary>
        /// Forces a user to log out by an admin, logging the forced logout event
        /// and hiding the user's main form if active.
        /// </summary>
        /// <param name="aliasToLogOut">The alias of the user to be logged out by admin.</param>
        public void PerformForcedLogOutByAdmin(string aliasToLogOut)
        {
            UserMainForm userForm = new UserMainForm();
            var admin = CurrentUser;

            if (!string.IsNullOrEmpty(admin))
            {
                logEvents.ForceUserLogOut(admin, aliasToLogOut);
                userForm.Hide();
            }
        }
        #endregion LOGOUT
    }
}
