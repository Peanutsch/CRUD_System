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
            path.AppendToLogEvents(currentUser, newLog); // Log login in logevent.csv
            path.AppendToLogStatus(currentUser, newLog); // // Log login in logstatus.csv
        }

        /// <summary>
        /// Logs the event when a user logs out.
        /// </summary>
        /// <param name="currentUser">The username of the user who logged out.</param>
        public void UserLoggedOut(string currentUser)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged OUT");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged OUT";
            path.AppendToLogEvents(currentUser, newLog); // Log login in logevent.csv
            path.AppendToLogStatus(currentUser, newLog); // // Log login in logstatus.csv
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
            path.AppendToLogEvents(currentUser, newLog);
            path.AppendToLogEvents(alias, newLog);
        }
        #endregion AUTHENTICATIONSERVICE

        #region ADMINCREATECONTROL
        /// <summary>
        /// Logs the event when a new user account is created.
        /// </summary>
        /// <param name="currentUser">The username of the user who created the new account.</param>
        /// <param name="isAlias">The alias of the user who was created.</param>
        public void NewAccount(string currentUser, string newAlias, string isPassword, string isEmail)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created user [{newAlias.ToUpper()}]. Sent email to {isEmail} with password: {isPassword}");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created user [{newAlias.ToUpper()}]. Sent email to {isEmail} with password: {isPassword}";
            path.AppendToLogEvents(currentUser, newLog);
            path.AppendToLogEvents(newAlias, newLog);
        }
        #endregion ADMINCREATECONTROL

        #region PROFILEMANAGER
        /// <summary>
        /// Logs the event when a password is generated for a user.
        /// </summary>
        /// <param name="currentUser">The username of the user generating the password.</param>
        /// <param name="alias">The alias of the user for whom the password was generated.</param>
        public void LogEventPasswordGenerated(string currentUser, string alias, string generatedPassword)
        {
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Generated new password for [{alias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Generated new password for [{alias.ToUpper()}]: {generatedPassword}";
                path.AppendToLogEvents(currentUser, newLog);
                path.AppendToLogEvents(alias, newLog);
            }
            else
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[UNKNOWN USER],Generated password for [{alias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[UNKNOW USER],Generated password for [{alias.ToUpper()}]";
                path.AppendToLogEvents(alias, newLog);
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

            if (AuthenticationService.CurrentUserIsAdmin && currentUser != alias) // Log the event in admin and user files
            {
                path.AppendToLogEvents(currentUser, newLog);
            }
            path.AppendToLogEvents(alias, newLog);
        }

        public void LogEventUpdateStatusIsTheOne(string currentUser, string alias, bool isTheOne)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated status IsTheOne for [{alias.ToUpper()}] to {isTheOne}");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated status IsTheOne for [{alias.ToUpper()}] to {isTheOne}";
            path.AppendToLogEvents(currentUser, newLog);
            path.AppendToLogEvents(alias, newLog);
        }

        public void LogEventUpdateStatusIsAdmin(string currentUser, string alias, bool isAdmin)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated status Admin for [{alias.ToUpper()}] to {isAdmin}");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated status Admin for [{alias.ToUpper()}] to {isAdmin}";
            path.AppendToLogEvents(currentUser, newLog);
            path.AppendToLogEvents(alias, newLog);
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
            path.AppendToLogEvents(currentUser, newLog);
            path.AppendToLogEvents(aliasToDelete, newLog);
        }
        #endregion PROFILEMANAGER

        #region USER MAIN CONTROL
        public void CheckStatus(string currentUser, string status, string time)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],checked {status}: {time}");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],checked {status}: {time}";
            path.AppendToLogStatus(currentUser, newLog);
        }
        #endregion USER MAIN CONTROL

        #region CREATE NEW PASSWORD
        /// <summary>
        /// Logs the event when a new password is created for a user.
        /// Temp. new psw in log.
        /// </summary>
        /// <param name="currentAlias">The alias of the user whose password was changed.</param>
        public void LogEventNewPasswordCreated(string currentAlias, string newPassword)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentAlias.ToUpper()}],Changed own password");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentAlias.ToUpper()}],Changed own password: {newPassword}";
            path.AppendToLogEvents(currentAlias, newLog);
        }
        #endregion CREATE NEW PASSWORD

        #region REPORT MANAGER
        public void LogEventReportDeleted(string currentUser, string selectedAlias, string fileNameReport)
        {
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Deleted report [{fileNameReport}] of [{selectedAlias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Deleted report [{fileNameReport}] of [{selectedAlias.ToUpper()}]";
                path.AppendToLogEvents(currentUser, newLog);
                path.AppendToLogEvents(selectedAlias, newLog);
            }
            else
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[UNKNOWN USER],Deleted report [{fileNameReport}] for [{selectedAlias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[UNKNOW USER],Deleted report [{fileNameReport}] for [{selectedAlias.ToUpper()}]";
                path.AppendToLogEvents(selectedAlias, newLog);
            }
        }
        #endregion REPORT MANAGER

        #region SAVE NOTE
        public void LogEventSaveNote(string currentUser, string alias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created note for user [{alias.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created note for user [{alias.ToUpper()}]";

            if (AuthenticationService.CurrentUserIsAdmin && currentUser != alias) // Log the event in admin and user files
            {
                path.AppendToLogEvents(currentUser, newLog);
            }
            path.AppendToLogEvents(alias, newLog);
        }
        #endregion SAVE NOTE
    }
}