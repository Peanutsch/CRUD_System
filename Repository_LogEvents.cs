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

namespace CRUD_System
{
    internal class Repository_LogEvents
    {
        FilePaths path = new FilePaths();

        // Initialize DateTime for logging
        LogEntryActions log = new LogEntryActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };

        #region LOGINHANDLER
        public void UserLoggedIn(string CurrentUser)
        {
            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{CurrentUser.ToUpper()},Logged IN";
            Debug.WriteLine($"=====\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{CurrentUser.ToUpper()}] Logged IN");
            path.AppendToLog(newLog);
        }
        #endregion LOGINHANDLER

        #region ADMINCREATECONTROL
        public void NewAccount(string currentUser, string isAlias)
        {
            Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Created new user [{isAlias.ToUpper()}]");
            Debug.WriteLine($"User {isAlias} added successfully!");

            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Created new user [{isAlias.ToUpper()}]";
            path.AppendToLog(newLog);
        }
        #endregion ADMINCREATECONTROL

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
