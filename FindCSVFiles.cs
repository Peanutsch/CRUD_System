using CRUD_System.FileHandlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    internal class FindCSVFiles
    {
        /// <summary>
        /// Searches for a CSV log file for a given alias.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        /// <returns>The path to the CSV log file, or an empty string if the file is not found.</returns>
        public static string FindLogCSV(string alias)
        {
            string rootPath = RootPath.GetRootPath();
            string logsPath = Path.Combine(rootPath, "Logs");

            // Check if the Logs directory exists
            if (!Directory.Exists(logsPath))
            {
                Debug.WriteLine("Logs directory does not exist.");
                return string.Empty;
            }

            string file_logs = Path.Combine(logsPath, $"{alias}_logs.csv");

            // Search for the correct file
            foreach (var file in Directory.GetFiles(logsPath, "*.csv"))
            {
                if (file.Equals(file_logs, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.WriteLine($"File found: {file_logs}");
                    return file_logs;
                }
            }

            // File not found
            Debug.WriteLine($"No such file in Logs\\{alias}_logs.csv");
            return string.Empty;
        }

        public static string FindCSVFile(string alias, string map)
        {
            string rootPath = RootPath.GetRootPath();
            string filePath = Path.Combine(rootPath, $"{map}");

            // Check if the Logs directory exists
            if (!Directory.Exists(filePath))
            {
                Debug.WriteLine($"{map} directory does not exist.");
                return string.Empty;
            }

            string isFile = Path.Combine(filePath, $"{alias}_{map}.csv");

            // Search for the correct file
            foreach (var file in Directory.GetFiles(filePath, "*.csv"))
            {
                if (file.Equals(isFile, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.WriteLine($"File found: {isFile}");
                    return isFile;
                }
            }

            Debug.WriteLine($"No such file in Logs\\{alias}_logs.csv");
            return string.Empty;
        }

        public static void FindCisFiles()
        {
            ///
        }
    }
}
