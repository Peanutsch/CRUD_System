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
        DataCache cache = new DataCache();

        public bool onlineStatus = false;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public AuthenticationService()
        {

        }
        #endregion CONSTRUCTOR

        #region LOGIN VALIDATION
        /// <summary>
        /// Validates the provided username and password by checking them against the stored login data.
        /// This method ensures the user exists in the decrypted login data and that their credentials match.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        /// <param name="inputUserPassword">The password entered by the user.</param>
        /// <returns>
        /// True if the provided username and password match a record in the login data; otherwise, false.
        /// </returns>
        public bool ValidateLogin(string inputUserName, string inputUserPassword)
        {
            // Check if the cache is empty, and reload data if necessary.
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            // Attempt to find a user in the cached login data that matches the provided username and password
            var user = cache.CachedLoginData.FirstOrDefault(u =>
                u[0].Equals(inputUserName, StringComparison.OrdinalIgnoreCase) && // Compare usernames (case-insensitive)
                u[1] == inputUserPassword); // Compare passwords (case-sensitive)

            // Return true if a matching user is found, otherwise false
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
            var user = cache.CachedLoginData.FirstOrDefault(u =>
                u[0].Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                u[1] == inputUserPassword);

            // Return the admin status if the user is found
            return user != default && bool.Parse(user[2]); //user[2];
        }

        /// <summary>
        /// Checks if the user is offline.
        /// </summary>
        /// <param name="inputUserName">The username to check.</param>
        /// <returns>True if the user is offline; otherwise, false.</returns>
        public bool ValidateOnlineStatus(string inputUserName, string inputUserPassword)
        {
            // Find the user in the list where both username and password match
            var user = cache.CachedLoginData.FirstOrDefault(u =>
                u[0].Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                u[1] == inputUserPassword);

            return user != default && bool.Parse(user[3]);
        }
        #endregion LOGIN VALIDATION

        #region LOGIN
        /// <summary>
        /// Updates the online status of a user by alias in both login and user data files.
        /// If the user is not found, an error message is displayed.
        /// </summary>
        /// <param name="alias">The alias of the user whose online status needs to be updated.</param>
        /// <param name="onlineStatus">The new online status to set for the user (true for online, false for offline).</param>
        public void UpdateUserOnlineStatus(string alias, bool onlineStatus)
        {
            // Check if the cache is empty, and reload data if necessary.
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            // Find the user in the cached user data by alias and update their online status.
            var user = cache.CachedUserData.FirstOrDefault(u => u[2] == alias); // Alias field
            if (user != null)
            {
                user[8] = onlineStatus.ToString(); // Update online status
            }

            // Find the user in the cached login data by alias and update their online status.
            var login = cache.CachedLoginData.FirstOrDefault(l => l[0] == alias); // Alias field
            if (login != null)
            {
                login[3] = onlineStatus.ToString(); // Update online status
            }

            // Save changes to the data files and encrypt them
            cache.SaveAndEncryptData();
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
        /// <summary>
        /// Logs out the current user by updating their online status to offline,
        /// logging the logout event, and clearing the current user.
        /// </summary>
        public void PerformLogout()
        {
            var currentUser = CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                UpdateUserOnlineStatus(currentUser, false);
                logEvents.UserLoggedOut(currentUser);
                CurrentUser = null;
            }
        }

        /// <summary>
        /// Forces a user to log out by an admin, updating their online status,
        /// triggering logout actions, and logging the forced logout event.
        /// </summary>
        /// <param name="aliasToLogOut">The alias of the user to be logged out.</param>
        public void ForceLogOut(string aliasToLogOut)
        {
            AdminInterface adminInterface = new AdminInterface();

            // Pass the selected alias to SetForceLogOutUserBtn in AdminInterface
            adminInterface.SetForceLogOutUserBtn(aliasToLogOut);

            // Update the user's online status to offline
            UpdateUserOnlineStatus(aliasToLogOut, false);

            // Perform the forced logout for the user
            PerformForcedLogOutByAdmin(aliasToLogOut);
        }

        /// <summary>
        /// Performs the forced logout action for a user by an admin.
        /// Logs the forced logout event and hides the user's main form if active.
        /// </summary>
        /// <param name="aliasToLogOut">The alias of the user being logged out by the admin.</param>
        public void PerformForcedLogOutByAdmin(string aliasToLogOut)
        {
            UserMainForm userForm = new UserMainForm();

            // Check if the cache is empty, and reload data if necessary.
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            var admin = CurrentUser;

            if (!string.IsNullOrEmpty(admin))
            {
                cache.SaveAndEncryptData();

                // Log the forced logout event
                logEvents.ForceUserLogOut(admin, aliasToLogOut);

                // Hide the user's form if active
                userForm.Hide();
            }
        }
        #endregion LOGOUT
    }
}
