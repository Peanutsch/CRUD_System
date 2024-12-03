﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using CRUD_System.Handlers;

namespace CRUD_System.FileHandlers
{
    /// <summary>
    /// Manages file paths and provides methods for file operations related to user data, login data, and log events.
    /// Allows reading file content, appending log entries, and retrieving both user and login data.
    /// </summary>
    public class FilePaths
    {
        #region PROPERTIES
        public string UserFilePath { get; private set; }
        public string LoginFilePath { get; private set; }
        public string LogEventFilePath { get; private set; }
        public string HRFilePath { get; private set; }

        public static string rootPath = RootPath.GetRootPath() ?? string.Empty;

        #endregion PROPERTIES

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="FilePaths"/> class.
        /// Sets file paths for user data, login data, and log events based on the root directory.
        /// </summary>
        public FilePaths()
        {
            UserFilePath = Path.Combine(rootPath, "CSV", "data_users.csv");
            LoginFilePath = Path.Combine(rootPath, "CSV", "data_login.csv");
            LogEventFilePath = Path.Combine(rootPath, "CSV", "logEvents.csv");
            HRFilePath = Path.Combine(rootPath, "Cis_Notices", "{alias}_cis_notices.csv");
        }
        #endregion CONSTRUCTOR

        #region PROCESSING
        /// <summary>
        /// Appends a new log entry to the log events file.
        /// </summary>
        /// <param name="newLog">The log entry to append.</param>
        public void AppendToLog(string newLog)
        {
            if (!string.IsNullOrEmpty(LogEventFilePath))
            {
                File.AppendAllText(LogEventFilePath, newLog + Environment.NewLine);
            }
        }

        /// <summary>
        /// Returns path userLines and loginLines
        /// </summary>
        /// <returns></returns>
        public (List<string> userLines, List<string> loginLines) ReadUserAndLoginData()
        {
            var userLines = File.ReadAllLines(UserFilePath).ToList();
            var loginLines = File.ReadAllLines(LoginFilePath).ToList();
            return (userLines, loginLines);
        }

        public List<string> ReadHrLines()
        {
            var hrLines = File.ReadAllLines(HRFilePath).ToList();
            return hrLines;
        }
        #endregion PROCESSING

        /// <summary>
        /// Searches for the correct CSV file for the user: {alias}_cis_notices.csv.
        /// If the file doesn't exist, it creates a new one in the "Cis_Notices" directory.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public void SearchCIS_Notice(string alias)
        {
            DataCache cache = new DataCache();
            if (!string.IsNullOrEmpty(alias))
            {
                try
                {
                    string noticesPath = Path.Combine(rootPath, "Cis_Notices");

                    // Ensure the Cis_Notices directory exists
                    if (!Directory.Exists(noticesPath))
                    {
                        Directory.CreateDirectory(noticesPath);
                    }

                    string file_cis_notices = Path.Combine(noticesPath, $"{alias}_cis_notices.csv");

                    if (!File.Exists(file_cis_notices))
                    {
                        // Create a new file with default headers (or leave empty)
                        File.WriteAllText(file_cis_notices, "Alias,date_sick,date_recovery\n"); // Example CSV headers

                        // Encrypt file
                        EncryptionManager.EncryptFile(file_cis_notices);
                    }
                    else
                    {
                        // Read the file contents (if needed, process the data further)
                        string[] fileContents = File.ReadAllLines(file_cis_notices);
                        // Optionally process or log the data
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions gracefully (e.g., log the error or show a user-friendly message)
                    Debug.WriteLine($"An error occurred: {ex.Message}");
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Alias cannot be null or empty.");
                return;
            }
        }


    }
}
