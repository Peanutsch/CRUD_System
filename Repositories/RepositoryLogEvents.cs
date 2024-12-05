﻿using CRUD_System.FileHandlers;
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
        public void UserLoggedIn(string currentUser)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged IN\n==========");
            string userLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged IN";
            string adminLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged IN";
            string logFile = FindCSVFiles.FindCSVFile(currentUser, "logs");
            
            if (AuthenticationService.CurrentUserRole)
            {
                AdminMainControl adminControl = new AdminMainControl();
                path.AppendToLog(logFile, adminLog);
            }
            else
            {
                UserMainControl userControl = new UserMainControl();
                path.AppendToLog(logFile, userLog);
            }
        }

        public void UserLoggedOut(string currentUser)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged OUT");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],logged OUT";
            string logFile = FindCSVFiles.FindCSVFile(currentUser, "logs");
            path.AppendToLog(logFile, newLog);
        }

        public void ForceUserLogOut(string currentUser, string alias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Forced [{alias.ToUpper()}] log OUT");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Forced [{alias.ToUpper()}] log OUT";
            string logFile = FindCSVFiles.FindCSVFile(currentUser, "logs");
            string userLogFile = FindCSVFiles.FindCSVFile(alias, "logs");
            path.AppendToLog(logFile, newLog);
            path.AppendToLog(userLogFile, newLog);
        }
        #endregion AUTHENTICATIONSERVICE

        #region AdminCreateControl
        public void NewAccount(string currentUser, string isAlias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created user [{isAlias.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Created user [{isAlias.ToUpper()}]";
            string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logs");
            string userLogFile = FindCSVFiles.FindCSVFile(isAlias, "logs");
            path.AppendToLog(adminlogFile, newLog);
            path.AppendToLog(userLogFile, newLog);
        }
        #endregion AdminCreateControl

        #region PROFILEMANAGER
        public void LogEventPasswordGenerated(string currentUser, string alias)
        {
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Generated password for [{alias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Generated password for [{alias.ToUpper()}]";
                string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logs");
                string userLogFile = FindCSVFiles.FindCSVFile(alias, "logs");
                path.AppendToLog(adminlogFile, newLog);
                path.AppendToLog(userLogFile, newLog);
            }
            else
            {
                Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[UNKNOWN USER],Generated password for [{alias.ToUpper()}]");
                string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[UNKNOW USER],Generated password for [{alias.ToUpper()}]";
                string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logs");
                string userLogFile = FindCSVFiles.FindCSVFile(alias, "logs");
                path.AppendToLog(adminlogFile, newLog);
                path.AppendToLog(userLogFile, newLog);
            }
        }
        
        public void LogEventUpdateUserDetails(string currentUser, string alias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated details [{alias.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Updated details [{alias.ToUpper()}]";
            string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logs");
            string userLogFile = FindCSVFiles.FindCSVFile(alias, "logs");

            if (AuthenticationService.CurrentUserRole)
            {
                Debug.WriteLine($"CurrentUserRole: {AuthenticationService.CurrentUserRole}");
                path.AppendToLog(adminlogFile, newLog);
            }
            path.AppendToLog(userLogFile, newLog);
        }

        public void LogEventDeleteUser(string currentUser, string aliasToDelete)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Deleted user [{aliasToDelete.ToUpper()}]");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentUser.ToUpper()}],Deleted user [{aliasToDelete.ToUpper()}]";
            string adminlogFile = FindCSVFiles.FindCSVFile(currentUser, "logs");
            string userLogFile = FindCSVFiles.FindCSVFile(aliasToDelete, "logs");
            path.AppendToLog(adminlogFile, newLog);
            path.AppendToLog(userLogFile, newLog);
        }
        #endregion PROFILEMANAGER

        #region CREATENEWPASSWORD
        public void LogEventNewPasswordCreated(string currentAlias)
        {
            Debug.WriteLine($"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentAlias.ToUpper()}],Changed password");
            string newLog = $"{DateTime.Today.ToString("dd-MM-yyyy")},{DateTime.Now.ToString("HH:mm:ss")},[{currentAlias.ToUpper()}],Changed password";
            string logFile = FindCSVFiles.FindCSVFile(currentAlias, "logs");
            path.AppendToLog(logFile, newLog);
        }
        #endregion CREATENEWPASSWORD
    }
}
