using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_System.FileHandlers
{
    /// <summary>
    /// Manages file paths and provides methods for file operations related to user data, login data, and log events.
    /// Allows reading file content, appending log entries, and retrieving both user and login data.
    /// </summary>
    public class FilePaths
    {
        public string UserFilePath { get; private set; }
        public string LoginFilePath { get; private set; }
        public string LogEventFilePath { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePaths"/> class.
        /// Sets file paths for user data, login data, and log events based on the root directory.
        /// </summary>
        public FilePaths()
        {
            string rootPath = RootPath.GetRootPath() ?? string.Empty;

            UserFilePath = Path.Combine(rootPath, "CSV", "data_users.csv");
            LoginFilePath = Path.Combine(rootPath, "CSV", "data_login.csv");
            LogEventFilePath = Path.Combine(rootPath, "CSV", "logEvents.csv");
        }

        /// <summary>
        /// Reads the content of a specified file and returns it as a list of strings.
        /// </summary>
        /// <param name="filePath">The path of the file to read.</param>
        /// <returns>A list of strings containing each line of the file, or an empty list if the file does not exist.</returns>
        public List<string> ReadFileContent(string filePath)
        {
            return File.Exists(filePath) ? File.ReadAllLines(filePath).ToList() : new List<string>();
        }

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
    }
}
