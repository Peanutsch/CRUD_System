using CRUD_System.Encryption;
using CRUD_System.FileHandlers;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_System.Handlers
{
    /// <summary>
    /// A class that handles the configuration and population of a ListView with file details.
    /// </summary>
    internal class ListViewReports
    {
        #region PROPERTIES
        private readonly AdminMainControl adminControl;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the ListViewFiles class and sets up the ListView.
        /// </summary>
        /// <param name="adminControl">The AdminMainControl containing the ListView.</param>
        public ListViewReports(AdminMainControl? adminControl = null)
        {
            this.adminControl = adminControl ?? new AdminMainControl();
        }
        #endregion CONSTRUCTOR

        #region PROCESS
        /// <summary>
        /// Loads files from a specified directory into the ListView.
        /// </summary>
        /// <param name="directoryPath">The directory path to load the files from.</param>
        public void LoadReportsIntoListView(string directoryPath)
        {
            adminControl.listViewReports.Visible = true;

            // Check if the specified directory exists
            if (Directory.Exists(directoryPath) && !string.IsNullOrEmpty(directoryPath))
            {
                // Retrieve all CSV files in the directory
                string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv", SearchOption.AllDirectories);

                // Check if any CSV files are found
                if (csvFiles.Any())
                {
                    // Create an array of FileInfo objects for sorting
                    FileInfo[] fileInfos = csvFiles.Select(file => new FileInfo(file)).ToArray();

                    // Sort the files by CreationTime in descending order (newest first)
                    Array.Sort(fileInfos, (f1, f2) => f2.CreationTime.CompareTo(f1.CreationTime));

                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        // Format csv filename: {alias}_{date}_{time}_report.csv
                        string[] itemSplit = fileInfo.Name.Split("_");
                        if (itemSplit.Length >= 2)
                        {
                            string reportName = string.Join("_", itemSplit[0], itemSplit[1]);
                            (string reportSubject, string reportCreator) = GetSubjectAndCreatorAlias(reportName, itemSplit[0]); // itemSplit[0] is alias

                            // Create ListViewItem
                            ListViewItem item = new ListViewItem(reportName);

                            // Add reportCreator and reportSubjct as subitems in listview.
                            // Adding subitems goes in order of the columns.
                            item.SubItems.Add(!string.IsNullOrEmpty(reportCreator) ? reportCreator : "Unknown");
                            item.SubItems.Add(!string.IsNullOrEmpty(reportSubject) ? reportSubject : "Unknown");

                            // Add item to ListView
                            adminControl.listViewReports.Items.Add(item);

                            // Set the Tag property to the full file path
                            item.Tag = fileInfo.FullName;
                        }
                    }

                    // Force a refresh of the ListView to ensure it's displaying correctly
                    adminControl.listViewReports.Refresh();
                }
            }
        }

        /// <summary>
        /// Retrieves the subject field from a specific user's report file. 
        /// Decrypts the file to read its content and re-encrypts it after processing.
        /// </summary>
        /// <param name="selectedUserString">The string identifying the user and report.</param>
        /// <param name="alias">The alias of the user.</param>
        /// <returns>The subject field from the report, or an empty string if an error occurs or the subject is not found.</returns>
        public (string Subject, string Creator) GetSubjectAndCreatorAlias(string selectedUserString, string alias)
        {
            try
            {
                // Ensure the input string is not null or empty
                if (!string.IsNullOrEmpty(selectedUserString))
                {
                    string rootPath = RootPath.GetRootPath(); // Get the root directory path   
                    string fileName = selectedUserString + "_report.csv"; // Construct the expected file name
                    string yearFolder = ReportManager.GetYearFolder(selectedUserString); // Determine the year folder for the report
                    string filePath = Path.Combine(rootPath, "report", yearFolder, alias, fileName); // Construct the full file path

                    EncryptionManager.DecryptFile(filePath); // Decrypt the file for reading

                    // Read all lines from the file and split each line into fields
                    var readFile = File.ReadAllLines(filePath)
                                       .Select(line => line.Split(",")) // Split by commas into arrays of strings
                                       .ToList(); // Convert to a list for easy iteration

                    // Iterate through the lines to find and return the subject
                    foreach (string[] line in readFile)
                    {
                        string isSubject = line[3]; // Subject is index 3
                        string isCreator = line[1]; // Creator is index 1
                        EncryptionManager.EncryptFile(filePath); // Re-encrypt the file after processing
                        return (isSubject, isCreator);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception error: {e}...\nReturn string.Empty");
                return ((string.Empty, string.Empty)); // If any exception occurs, return an empty string
            }

            return ((string.Empty, string.Empty)); // Return an empty string if no subject is found or input is invalid
        }
        #endregion PROCESS
    }
}
