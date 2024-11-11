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

        #region AUTHENTICATIONSERVICE
        public void UserLoggedIn(string CurrentUser)
        {
            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{CurrentUser.ToUpper()},Logged IN";
            Debug.WriteLine($"=====\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{CurrentUser.ToUpper()}] Logged IN");
            path.AppendToLog(newLog);
        }

        public void UserLoggedOut(string currentUser)
        {
            Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}] logged OUT");
            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Logged OUT";
            path.AppendToLog(newLog);
        }

        public void ForceUserLogOut(string currentUser, string alias)
        {
            Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Forced [{alias.ToUpper()}] logged OUT");
            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Forced [{alias.ToUpper()}] to log OUT)";
            path.AppendToLog(newLog);
        }
        #endregion AUTHENTICATIONSERVICE

        #region AdminCreateControl
        public void NewAccount(string currentUser, string isAlias)
        {
            Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Created new user [{isAlias.ToUpper()}]");
            Debug.WriteLine($"User {isAlias} added successfully!");

            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Created new user [{isAlias.ToUpper()}]";
            path.AppendToLog(newLog);
        }
        #endregion AdminCreateControl

        #region PROFILEMANAGER
        public void LogEventPasswordGenerated(string currentUser, string alias)
        {
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}] Changed password for [{alias.ToUpper()}]");
                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Changed password for user [{alias.ToUpper()}]";
                path.AppendToLog(newLog);
            }
            else
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [UNKNOWN] Changed password for [{alias.ToUpper()}]");
                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},[UNKNOWN],Changed password for user [{alias.ToUpper()}]";
                path.AppendToLog(newLog);
            }
        }

        public void LogEventUpdateUserDetails(string currentUser, string alias)
        {
            Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Updated user details for {alias.ToUpper()}");
            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Updated user details for {alias.ToUpper()}";
            path.AppendToLog(newLog);
        }

        public void LogEventDeleteUser(string currentUser, string aliasToDelete)
        {
            Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Deleted user [{aliasToDelete.ToUpper()}]");

            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Deleted user [{aliasToDelete.ToUpper()}]";
            path.AppendToLog(newLog);
        }
        #endregion PROFILEMANAGER

        #region CREATENEWPASSWORD
        public void LogEventNewPasswordCreated(string currentAlias)
        {
            Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentAlias.ToUpper()}]: Changed password");
            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentAlias.ToUpper()},Changed password";
            path.AppendToLog(newLog);
        }
        #endregion CREATENEWPASSWORD
    }
}
