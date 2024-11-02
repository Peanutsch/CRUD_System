using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_System.FileHandlers
{
    public class FilePaths
    {
        public string UserFilePath { get; private set; }
        public string LoginFilePath { get; private set; }
        public string LogEventFilePath { get; private set; }

        public FilePaths()
        {
            string rootPath = RootPath.GetRootPath() ?? string.Empty;

            UserFilePath = Path.Combine(rootPath, "FilesUserDetails", "data_users.csv");
            LoginFilePath = Path.Combine(rootPath, "FilesUserDetails", "data_login.csv");
            LogEventFilePath = Path.Combine(rootPath, "FilesUserDetails", "logEvents.csv");
        }

        public List<string> ReadFileContent(string filePath)
        {
            return File.Exists(filePath) ? File.ReadAllLines(filePath).ToList() : new List<string>();
        }

        public void AppendToLog(string newLog)
        {
            if (!string.IsNullOrEmpty(LogEventFilePath))
            {
                File.AppendAllText(LogEventFilePath, newLog + Environment.NewLine);
            }
        }
    }
}
