using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using CRUD_System.Interfaces;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System.Repositories
{
    /// <summary>
    /// Handles logging events, such as user login/logout, account creation, user updates, password changes and deletions. 
    /// Logs include date, time, user information, and event details.
    /// </summary>
    internal class RepositoryLogEvents
    {
        #region PROPERTIES
        private readonly FilePaths path = new FilePaths();
        #endregion PROPERTIES

        #region AUTHENTICATIONSERVICE
        /// <summary>
        /// Logs the event when a user logs in.
        /// </summary>
        /// <param name="currentUser">The username of the user who logged in.</param>
        public void UserLoggedIn(string currentUser)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged IN\n==========");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged IN";
            string logFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
            path.AppendToLog(currentUser, newLog);
        }

        /// <summary>
        /// Logs the event when a user logs out.
        /// </summary>
        /// <param name="currentUser">The username of the user who logged out.</param>
        public void UserLoggedOut(string currentUser)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged OUT");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged OUT";
            string logFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
            path.AppendToLog(currentUser, newLog);
        }

        /// <summary>
        /// Logs the event when a user is forced to log out by an admin.
        /// </summary>
        /// <param name="currentUser">The username of the user who forced the log out.</param>
        /// <param name="alias">The alias of the user who was forced to log out.</param>
        public void ForceUserLogOut(string currentUser, string alias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Forced [{alias.ToUpper()}] log OUT");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Forced [{alias.ToUpper()}] log OUT";
            string logFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
            string userLogFile = FindCSVFiles.FindCSVFile(alias, "logevents");
            path.AppendToLog(currentUser, newLog);
            path.AppendToLog(alias, newLog);
        }
        #endregion AUTHENTICATIONSERVICE

        #region ADMINCREATECONTROL
        /// <summary>
        /// Logs the event when a new user account is created.
        /// </summary>
        /// <param name="currentUser">The username of the user who created the new account.</param>
        /// <param name="isAlias">The alias of the user who was created.</param>
        public void NewAccount(string currentUser, string isAlias, string isPassword, string isEmail)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created user [{isAlias.ToUpper()}]. Sent email to {isEmail} with password: {isPassword}");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created user [{isAlias.ToUpper()}]. Sent email to {isEmail} with password: {isPassword}";
            string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
            string userLogFile = FindCSVFiles.FindCSVFile(isAlias, "logevents");
            path.AppendToLog(currentUser, newLog);
            path.AppendToLog(isAlias, newLog);
        }
        #endregion ADMINCREATECONTROL

        #region PROFILEMANAGER
        /// <summary>
        /// Logs the event when a password is generated for a user.
        /// </summary>
        /// <param name="currentUser">The username of the user generating the password.</param>
        /// <param name="alias">The alias of the user for whom the password was generated.</param>
        public void LogEventPasswordGenerated(string currentUser, string alias)
        {
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Generated new password for [{alias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Generated new password for [{alias.ToUpper()}]";
                string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
                string userLogFile = FindCSVFiles.FindCSVFile(alias, "logevents");
                path.AppendToLog(adminlogFile, newLog);
                path.AppendToLog(userLogFile, newLog);
            }
            else
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[UNKNOWN USER],Generated password for [{alias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[UNKNOW USER],Generated password for [{alias.ToUpper()}]";
                string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
                string userLogFile = FindCSVFiles.FindCSVFile(alias, "logevents");
                path.AppendToLog(currentUser, newLog);
                path.AppendToLog(alias, newLog);
            }
        }

        /// <summary>
        /// Logs the event when a user's details are updated.
        /// When update is done by Admin, log events admin and user.
        /// Ignore when Admin edits own details: only log as user
        /// </summary>
        /// <param name="currentUser">The username of the user performing the update.</param>
        /// <param name="alias">The alias of the user whose details were updated.</param>
        public void LogEventUpdateUserDetails(string currentUser, string alias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated details [{alias.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated details [{alias.ToUpper()}]";
            string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
            string userLogFile = FindCSVFiles.FindCSVFile(alias, "logevents");

            if (AuthenticationService.CurrentUserIsAdmin && currentUser != alias) // Log the event in admin and user files
            {
                path.AppendToLog(currentUser, newLog);
            }
            path.AppendToLog(alias, newLog);
        }

        public void LogEventUpdateStatusIsTheOne(string currentUser, string alias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated status IsTheOne for [{alias.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated status IsTheOne for [{alias.ToUpper()}]";
            string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
            string userLogFile = FindCSVFiles.FindCSVFile(alias, "logevents");
            path.AppendToLog(currentUser, newLog);
            path.AppendToLog(alias, newLog);
        }

        /// <summary>
        /// Logs the event when a user is deleted.
        /// </summary>
        /// <param name="currentUser">The username of the user performing the deletion.</param>
        /// <param name="aliasToDelete">The alias of the user who is being deleted.</param>
        public void LogEventDeleteUser(string currentUser, string aliasToDelete)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Deleted user [{aliasToDelete.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Deleted user [{aliasToDelete.ToUpper()}]";
            string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
            string userLogFile = FindCSVFiles.FindCSVFile(aliasToDelete, "logevents");
            path.AppendToLog(currentUser, newLog);
            path.AppendToLog(aliasToDelete, newLog);
        }
        #endregion PROFILEMANAGER

        #region CREATE NEW PASSWORD
        /// <summary>
        /// Logs the event when a new password is created for a user.
        /// </summary>
        /// <param name="currentAlias">The alias of the user whose password was changed.</param>
        public void LogEventNewPasswordCreated(string currentAlias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentAlias.ToUpper()}],Changed own password");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentAlias.ToUpper()}],Changed own password";
            string logFile = FindCSVFiles.FindCSVFile(currentAlias, "logevents");
            path.AppendToLog(currentAlias, newLog);
        }
        #endregion CREATE NEW PASSWORD

        #region SAVE NOTE
        public void LogEventSaveNote(string currentUser, string alias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created note for user [{alias.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created note for user [{alias.ToUpper()}]";
            string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logevents");
            string userLogFile = FindCSVFiles.FindCSVFile(alias, "logevents");

            if (AuthenticationService.CurrentUserIsAdmin && currentUser != alias) // Log the event in admin and user files
            {
                path.AppendToLog(currentUser, newLog);
            }
            path.AppendToLog(alias, newLog);
        }
        #endregion SAVE NOTE
    }
}
