using CRUD_System.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System.FileHandlers
{
    internal class FindCSVFiles
    {
        /// <summary>
        /// Searches for a CSV file in the specified directory based on the given alias and directory name.
        /// </summary>
        /// <param name="directory">The alias of the user for which the file is being searched.</param>
        /// <param name="map">The name of the directory where the file is expected to be located (e.g., "Logs").</param>
        /// <returns>The full path to the CSV file if found, otherwise an empty string.</returns>
        public static string FindCSVFileLogEvent(string alias, string directory)
        {
            // Get the root directory path
            string rootPath = RootPath.GetRootPath();
            string filePath = Path.Combine(rootPath, $"{directory}", Timers.CurrentYear.ToString(), alias);

            // Check if the target directory exists
            if (!Directory.Exists(filePath))
            {
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

            return string.Empty; // Return an empty string if file does not exist
        }


        /// <summary>
        /// Finds all directories containing files for a specific alias within a given folder, 
        /// searching across the current year and the previous five years.
        /// </summary>
        /// <param name="selectedAlias">The alias to search for within the folder structure.</param>
        /// <param name="folder">The folder name where the alias directories are located (e.g., "logevents").</param>
        /// <returns>A list of directories that match the alias and exist within the specified year range.</returns>
        public static List<string> FindFilesInFolders(string selectedAlias, string folder)
        {
            // Get the root directory path
            string rootPath = RootPath.GetRootPath();

            // Get the current year for constructing the search range
            int currentYear = Timers.CurrentYear;

            // Create a range of years from the current year to 5 years back
            var rangeYears = Enumerable.Range(currentYear - 5, 6).Reverse(); // 6 because it’s inclusive of the start year

            // Initialize a list to store all found directories
            List<string> foundDirectories = new List<string>();

            // Iterate through each year in the range
            foreach (int year in rangeYears)
            {
                // Construct the directory path for the given year, folder, and alias
                string filePath = Path.Combine(rootPath, folder, year.ToString(), selectedAlias);

                // Check if the directory exists
                if (Directory.Exists(filePath))
                {
                    // If the directory exists, add it to the list
                    foundDirectories.Add(filePath);
                }
            }

            // Return all found directories, or an empty list if none were found
            return foundDirectories;
        }

    }
}
