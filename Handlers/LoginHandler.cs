using CRUD_System.FileHandlers;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public List<string> UsersOnline = new List<string>();

        ReadFiles ReadDataFiles = new ReadFiles();

        #region Initialize DateTime for logging
        LogEntryActions log = new LogEntryActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion Initialize DateTime
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
            List<(string Username, string Password, bool IsAdmin)> loginData = ReadDataFiles.GetLoginData();

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
            List<(string Username, string Password, bool IsAdmin)> loginData = ReadDataFiles.GetLoginData();

            // Find the user in the list where both username and password match
            var user = loginData.FirstOrDefault(u =>
                u.Username.Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == inputUserPSW);

            // Return the admin status if the user is found
            return user != default && user.IsAdmin;
        }
        #endregion LOGIN VALIDATION

        #region LOGIN
        /// <summary>
        /// Authenticates the user's login credentials by checking the provided
        /// username and password against the login data stored in the CSV file.
        /// </summary>
        /// <param name="inputUserName">The username input provided by the user.</param>
        /// <param name="inputUserPSW">The password input provided by the user.</param>
        public void AuthenticateUser(string inputUserName, string inputUserPSW)
        {
            // Validate login input
            if (ValidateLogin(inputUserName, inputUserPSW))
            {
                CurrentUser = inputUserName.ToLower();
                UsersOnline.Add(CurrentUser); // Add user to list UsersOnline

                bool isAdmin = IsAdmin(inputUserName, inputUserPSW);
                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{CurrentUser.ToUpper()},Logged IN";
                Debug.WriteLine($"=====\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{CurrentUser.ToUpper()}] Logged IN");
                //File.AppendAllText(logAction, logEntry + Environment.NewLine);
                path.AppendToLog(newLog);

                if (isAdmin)
                {
                    ADMINMainForm adminForm = new ADMINMainForm();
                    DisplayUserAlias(adminForm, isAdmin); // Pass adminForm, isAdmin to DisplayUserAlias
                    adminForm.ShowDialog(); // Open adminForm
                }
                else
                {
                    UserMainForm usersForm = new UserMainForm();
                    DisplayUserAlias(usersForm, !isAdmin); // Pass usersForm, !isAdmin to DisplayUserAlias
                    usersForm.ShowDialog(); // Open usersForm
                }
            }
            else
            {
                MessageBoxes message = new MessageBoxes();
                message.MessageInvalidNamePassword();

                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog(); // Open LoginForm
            }
        }

        // Adjust DisplayUserAlias to accept the correct form type as a parameter
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
            form.labelAlias.BackColor = Color.LightGreen;
            form.labelAlias.Text = isAdmin ? "ADMIN" : "USER";
        }
        #endregion LOGIN

        #region LOGOUT
        public void PerformLogout()
        {
            var currentUser = CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}] logged OUT");
                UsersOnline.Remove(currentUser); // Remove user from List UsersOnline

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Logged OUT";
                //File.AppendAllText(logAction, newLog + Environment.NewLine);
                path.AppendToLog(newLog);

                CurrentUser = null;
            }
        }
        #endregion LOGOUT
    }
}
