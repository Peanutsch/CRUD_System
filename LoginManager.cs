using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CRUD_System
{
    /// <summary>
    /// The LoginValidation class provides methods to validate user credentials,
    /// including checking the username and password against the data from the CSV.
    /// </summary>
    internal class LoginManager
    {
        #region PROPERTIES
        readonly string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        readonly string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");
        readonly string logAction = Path.Combine(RootPath.GetRootPath(), @"data\log.csv");

        LoginForm loginForm = new LoginForm();
        ADMINMainForm adminForm = new ADMINMainForm();
        USERSMainForm usersForm = new USERSMainForm();

        private Data dataFile = new Data();

        // Initialize DateTime for logging
        LogActions log = new LogActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion PROPERTIES

        #region VALIDATION
        /// <summary>
        /// Validates the provided username and password by checking them against the stored data in the CSV.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        /// <param name="inputUserPSW">The password entered by the user.</param>
        /// <returns>True if the credentials are valid; otherwise, false.</returns>
        public bool ValidateLogin(string inputUserName, string inputUserPSW)
        {
            // Get login data from the CSV file
            List<(string Username, string Password, bool IsAdmin)> loginData = dataFile.GetLoginData();

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
            List<(string Username, string Password, bool IsAdmin)> loginData = dataFile.GetLoginData();

            // Find the user in the list where both username and password match
            var user = loginData.FirstOrDefault(u =>
                u.Username.Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == inputUserPSW);

            // Return the admin status if the user is found
            return user != default && user.IsAdmin;
        }
        #endregion VALIDATION

        #region LOGIN
        //
        #endregion LOGIN
        //================================

        //================================


        #region LOGOUT
        public void PerformLogout()
        {
            var currentUser = LoginForm.CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}] logged OUT");
                loginForm.UsersOnline.Remove(currentUser); // Remove user from List UsersOnline

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Logged OUT";
                File.AppendAllText(logAction, newLog + Environment.NewLine);

                currentUser = null;
            }
        }
        #endregion LOGOUT

    }
}
