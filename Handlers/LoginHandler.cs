using CRUD_System.FileHandlers;
using CRUD_System.Repositories;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUD_System.Handlers
{
    public class LoginHandler
    {
        #region PROPERTIES
        FilePaths path = new FilePaths();

        public static string? CurrentUser { get; set; }

        //public List<string> UsersOnline = new List<string>();

        ReadFiles readFiles = new ReadFiles();
        RepositoryLogEvents logEvents = new RepositoryLogEvents();
        RepositoryMessageBoxes message = new RepositoryMessageBoxes();

        public bool onlineStatus = false;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public LoginHandler()
        {

        }
        #endregion CONSTRUCTOR

        #region LOGIN VALIDATION
        /// <summary>
        /// Validates the provided username and password by checking them against the stored data in the CSV.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        /// <param name="inputUserPSW">The password entered by the user.</param>
        /// <returns>True if the credentials are valid; otherwise, false.</returns>
        public bool ValidateLogin(string inputUserName, string inputUserPSW)
        {
            // Get login data from the CSV file
            List<(string Username, string Password, bool IsAdmin, bool onlineStatus)> loginData = readFiles.GetLoginData();

            // Find the user in the list where both username and password match
            var user = loginData.FirstOrDefault(u =>
                                                u.Username.Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                                                u.Password == inputUserPSW);

            // If user is found, credentials are valid
            return user != default;
        }

        /// <summary>
        /// Determines if the logged-in user is an admin.
        /// </summary>
        /// <param name="inputUserName">The username of the user.</param>
        /// <param name="inputUserPSW">The password of the user.</param>
        /// <returns>True if the user is an admin; otherwise, false.</returns>
        public bool IsAdmin(string inputUserName, string inputUserPSW)
        {
            // Get login data from the CSV file
            List<(string Username, string Password, bool IsAdmin, bool onlineStatus)> loginData = readFiles.GetLoginData();

            // Find the user in the list where both username and password match
            var user = loginData.FirstOrDefault(u =>
                u.Username.Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == inputUserPSW);

            // Return the admin status if the user is found
            return user != default && user.IsAdmin;
        }

        /// <summary>
        /// Checks if the user is offline (not currently in UsersOnline).
        /// </summary>
        /// <param name="inputUserName">The username to check.</param>
        /// <returns>True if the user is offline; otherwise, false.</returns>
        public bool ValidateOnlineStatus(string inputUserName, string inputUserPSW)
        {
            //return !UsersOnline.Contains(inputUserName);
            List<(string Username, string Password, bool IsAdmin, bool onlineStatus)> loginData = readFiles.GetLoginData();
            // Find the user in the list where both username and password match
            var user = loginData.FirstOrDefault(u =>
                u.Username.Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == inputUserPSW);

            return user != default && user.onlineStatus;
        }
        #endregion LOGIN VALIDATION

        #region LOGIN

        public void OnlineStatusHandler(string alias, bool status)
        {
            AccountManager accountManager = new AccountManager();
            
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            // Find user index
            int loginIndex = accountManager.FindUserIndexByAlias(userLines, loginLines, alias);
            var loginDetails = loginLines[loginIndex].Split(",");

            string currentUserName = loginDetails[0];
            string currentPassword = loginDetails[1];
            string currentIsAdmin = loginDetails[2];

            loginLines[loginIndex] = $"{currentUserName},{currentPassword},{currentIsAdmin},{status}";

            File.WriteAllLines(path.LoginFilePath, loginLines);
        }

        /// <summary>
        /// Authenticates the user's login credentials and handles login processing.
        /// </summary>
        /// <param name="inputUserName">The username input provided by the user.</param>
        /// <param name="inputUserPSW">The password input provided by the user.</param>
        public void AuthenticateUser(string inputUserName, string inputUserPSW)
        {
            // T E S T //
            //UsersOnline.Add("peer001");
            // _ _ _ _ //

            if (!ValidateUserLogin(inputUserName, inputUserPSW))
            {
                return;
            }

            ProcessSuccessfulLogin(inputUserName, inputUserPSW);
        }

        /// <summary>
        /// Validates the user's login credentials and online status.
        /// </summary>
        /// <param name="inputUserName">The username input provided by the user.</param>
        /// <param name="inputUserPSW">The password input provided by the user.</param>
        /// <returns>True if the user credentials and status are valid; otherwise, false.</returns>
        private bool ValidateUserLogin(string inputUserName, string inputUserPSW)
        {
            LoginForm loginForm = new LoginForm();

            // Validate login credentials
            if (!ValidateLogin(inputUserName, inputUserPSW))
            {
                message.MessageInvalidNamePassword();
                loginForm.ShowDialog(); // Reopen LoginForm for retry
                return false;
            }
            if (ValidateOnlineStatus(inputUserName, inputUserPSW))
            {
                message.MessageUserAlreadyOnline(inputUserName);
                loginForm.ShowDialog(); // Reopen LoginForm for retry
                return false;
            }
            /*
            // Check user online status
            if (!ValidateStatus(inputUserName))
            {
                message.MessageUserAlreadyOnline(inputUserName);
                loginForm.ShowDialog(); // Reopen LoginForm for retry
                return false;
            }
            */
            return true;
        }

        /// <summary>
        /// Processes actions upon a successful login.
        /// </summary>
        /// <param name="inputUserName">The validated username.</param>
        /// <param name="inputUserPSW">The validated password.</param>
        private void ProcessSuccessfulLogin(string inputUserName, string inputUserPSW)
        {
            CurrentUser = inputUserName.ToLower();
            //UsersOnline.Add(CurrentUser);
            OnlineStatusHandler(CurrentUser, true);

            logEvents.UserLoggedIn(CurrentUser);

            bool isAdmin = IsAdmin(inputUserName, inputUserPSW);
            if (isAdmin)
            {
                ADMINMainForm adminForm = new ADMINMainForm();
                DisplayUserAlias(adminForm, isAdmin);
                adminForm.ShowDialog();
            }
            else
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
        /// Logs out the current user by removing them from the online user list,
        /// logging the logout event, and clearing the CurrentUser.
        /// </summary>
        public void PerformLogout()
        {
            var currentUser = CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                logEvents.UserLoggedOut(currentUser);
                //UsersOnline.Remove(currentUser); // Remove user from List UsersOnline
                OnlineStatusHandler(currentUser, false);
                CurrentUser = null;
            }           
        }
        #endregion LOGOUT
    }
}
