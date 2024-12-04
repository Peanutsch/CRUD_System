using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
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
        FilePaths path = new FilePaths();

        /// <summary>
        /// Stores the current date and time for logging purposes.
        /// Initializes with the current date and time when the instance is created.
        /// </summary>
        private readonly LogDateTime log = new LogDateTime
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion PROPERTIES



        #region AUTHENTICATIONSERVICE
        public void UserLoggedIn(string currentUser)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],logged IN");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],logged IN";
            string logFile = FindCSVFiles.FindLogCSV(currentUser);
            path.AppendToLog(logFile, newLog);
        }

        public void UserLoggedOut(string currentUser)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],logged OUT");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],logged OUT";
            string logFile = FindCSVFiles.FindLogCSV(currentUser);
            path.AppendToLog(logFile, newLog);
        }

        public void ForceUserLogOut(string currentUser, string alias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Forced [{alias.ToUpper()}] to log OUT");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Forced [{alias.ToUpper()}] to log OUT";
            string logFile = FindCSVFiles.FindLogCSV(currentUser);
            path.AppendToLog(logFile, newLog);
        }
        #endregion AUTHENTICATIONSERVICE

        #region AdminCreateControl
        public void NewAccount(string currentUser, string isAlias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Created new user [{isAlias.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Created new user [{isAlias.ToUpper()}]";
            string logFile = FindCSVFiles.FindLogCSV(currentUser);
            path.AppendToLog(logFile, newLog);
        }
        #endregion AdminCreateControl

        #region PROFILEMANAGER
        public void LogEventPasswordGenerated(string currentUser, string alias)
        {
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Generated new password for [{alias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Generated new password for [{alias.ToUpper()}]";
                string logFile = FindCSVFiles.FindLogCSV(currentUser);
                path.AppendToLog(logFile, newLog);
            }
            else
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[UNKNOWN USER],Generated new password for [{alias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[UNKNOW USER],Generated new password for [{alias.ToUpper()}]";
                string logFile = FindCSVFiles.FindLogCSV(currentUser);
                path.AppendToLog(logFile, newLog);
            }
        }

        public void LogEventUpdateUserDetails(string currentUser, string alias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Updated user details for [{alias.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Updated user details for [{alias.ToUpper()}]";
            string logFile = FindCSVFiles.FindLogCSV(currentUser);
            path.AppendToLog(logFile, newLog);
        }

        public void LogEventDeleteUser(string currentUser, string aliasToDelete)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Deleted user [{aliasToDelete.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentUser.ToUpper()}],Deleted user [{aliasToDelete.ToUpper()}]";
            string logFile = FindCSVFiles.FindLogCSV(currentUser);
            path.AppendToLog(logFile, newLog);
        }
        #endregion PROFILEMANAGER

        #region CREATENEWPASSWORD
        public void LogEventNewPasswordCreated(string currentAlias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentAlias.ToUpper()}],Changed password");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm")},[{currentAlias.ToUpper()}],Changed password";
            string logFile = FindCSVFiles.FindLogCSV(currentAlias);
            path.AppendToLog(logFile, newLog);
        }
        #endregion CREATENEWPASSWORD
    }
}
