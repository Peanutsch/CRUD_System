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
        /// Searches for a CSV file in the specified directory based on the given alias and directory name.
        /// </summary>
        /// <param name="directory">The alias of the user for which the file is being searched.</param>
        /// <param name="map">The name of the directory where the file is expected to be located (e.g., "Logs").</param>
        /// <returns>The full path to the CSV file if found, otherwise an empty string.</returns>
        public static string FindCSVFile(string alias, string directory)
        {
            // Get the root directory path
            string rootPath = RootPath.GetRootPath();
            string filePath = Path.Combine(rootPath, $"{directory}", Timers.CurrentYear.ToString(), alias);

            // Check if the target directory exists
            if (!Directory.Exists(filePath))
            {
                Debug.WriteLine($"{directory} directory does not exist.");
                MessageBox.Show($"{directory} directory does not exist.");
                return string.Empty;
            }

            // Construct the expected file path based on alias and directory name
            string isFile = Path.Combine(filePath, $"{alias}_{directory}.csv");

            // Search for the file in the directory
            foreach (var file in Directory.GetFiles(filePath, "*.csv"))
            {
                // Compare the current file with the expected file path
                if (file.Equals(isFile, StringComparison.OrdinalIgnoreCase))
                {
                    return isFile; // Return the path of the found file
                }
            }

            // File not found
            Debug.WriteLine($"No such file in {isFile}");
            MessageBox.Show($"No such file in {isFile}");
            return string.Empty; // Return an empty string if file does not exist
        }

        public static string FindReportFile(string alias, string directory)
        {
            // Get the root directory path
            string rootPath = RootPath.GetRootPath();
            string filePath = Path.Combine(rootPath, directory, Timers.CurrentYear.ToString(), alias);

            // Check if the target directory exists
            if (!Directory.Exists(filePath))
            {
                Debug.WriteLine($"{directory} directory does not exist.");
                MessageBox.Show($"{directory} directory does not exist.");
                return string.Empty;
            }

            // Return the directory path (not the full file path)
            return filePath;
        }

        /*
        internal class FindCSVFiles
    {
        /// <summary>
        /// Searches for a CSV file in the specified directory based on the given alias and directory name.
        /// </summary>
        /// <param name="alias">The alias of the user for which the file is being searched.</param>
        /// <param name="map">The name of the directory where the file is expected to be located (e.g., "Logs").</param>
        /// <returns>The full path to the CSV file if found, otherwise an empty string.</returns>
        public static string FindCSVFile(string alias, string map)
        {
            // Get the root directory path
            string rootPath = RootPath.GetRootPath();
            string filePath = Path.Combine(rootPath, $"{map}");

            // Check if the target directory exists
            if (!Directory.Exists(filePath))
            {
                Debug.WriteLine($"{map} directory does not exist.");
                MessageBox.Show($"{map} directory does not exist.");
                return string.Empty;
            }

            // Construct the expected file path based on alias and directory name
            string isFile = Path.Combine(filePath, $"{alias}_{map}.csv");

            // Search for the file in the directory
            foreach (var file in Directory.GetFiles(filePath, "*.csv"))
            {
                // Compare the current file with the expected file path
                if (file.Equals(isFile, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.WriteLine($"[FindCSVFile] file found: {isFile}");
                    return isFile; // Return the path of the found file
                }
            }

            // File not found
            Debug.WriteLine($"No such file in {map}\\{alias}_{map}.csv");
            MessageBox.Show($"No such file in {map}\\{alias}_{map}.csv");
            return string.Empty; // Return an empty string if file does not exist
        }
        */
    }
}
