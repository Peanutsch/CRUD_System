using CRUD_System.FileHandlers;
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

        public List<string> UsersOnline = new List<string>();

        ReadFiles ReadDataFiles = new ReadFiles();
        Repository_LogEvents logEvents = new Repository_LogEvents();
        MessageBoxes message = new MessageBoxes();

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

        public bool ValidateStatus(string inputUserName)
        {
            foreach (string item in UsersOnline)
            {
                if (item.Equals(inputUserName))
                {
                    MessageBox.Show($"User [{inputUserName}] is already online");
                    return false;
                }
            }
            return true;
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
            if (ValidateLogin(inputUserName, inputUserPSW) && ValidateStatus(inputUserName))
            {
                CurrentUser = inputUserName.ToLower();
                UsersOnline.Add(CurrentUser); // Add user to list UsersOnline

                bool isAdmin = IsAdmin(inputUserName, inputUserPSW);

                logEvents.UserLoggedIn(CurrentUser);

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
                logEvents.UserLoggedOut(currentUser);
                UsersOnline.Remove(currentUser); // Remove user from List UsersOnline
                CurrentUser = null;
            }           
        }
        #endregion LOGOUT
    }
}
