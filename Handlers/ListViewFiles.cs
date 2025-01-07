using CRUD_System.FileHandlers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_System.Handlers
{
    /// <summary>
    /// A class that handles the configuration and population of a ListView with file details.
    /// </summary>
    internal class ListViewFiles
    {
        #region PROPERTIES
        private readonly AdminMainControl adminControl;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the ListViewFiles class and sets up the ListView.
        /// </summary>
        /// <param name="adminControl">The AdminMainControl containing the ListView.</param>
        public ListViewFiles(AdminMainControl? adminControl = null)
        {
            this.adminControl = adminControl ?? new AdminMainControl();
        }
        #endregion CONSTRUCTOR

        #region PROCESS
        /// <summary>
        /// Loads files from a specified directory into the ListView.
        /// </summary>
        /// <param name="directoryPath">The directory path to load the files from.</param>
        public void LoadFilesIntoListView(string directoryPath)
        {
            adminControl.listViewFiles.Visible = true;

            // Check if the specified directory exists
            if (Directory.Exists(directoryPath) && !string.IsNullOrEmpty(directoryPath))
            {
                // Retrieve all CSV files in the directory
                string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv", SearchOption.AllDirectories);

                // Check if any CSV files are found
                if (csvFiles.Length > 0)
                {
                    // Create an array of FileInfo objects for sorting
                    FileInfo[] fileInfos = csvFiles.Select(file => new FileInfo(file)).ToArray();

                    // Sort the files by CreationTime in descending order (newest first)
                    Array.Sort(fileInfos, (f1, f2) => f2.CreationTime.CompareTo(f1.CreationTime));

                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string[] itemSplit = fileInfo.Name.Split("_");
                        if (itemSplit.Length >= 2)
                        {
                            string itemUse = string.Join("_", itemSplit[0], itemSplit[1]);
                            string subject = GetSubject(itemUse, itemSplit[0]); // itemSplit[0] is alias

                            Debug.WriteLine($"itemUse: {itemUse} subject: {subject}");

                            // Create ListViewItem
                            ListViewItem item = new ListViewItem(itemUse);

                            ////// GET FILE SUBJECT AS SUBITEMS ////
                            item.SubItems.Add(!string.IsNullOrEmpty(subject) ? subject : "Unknown");

                            // Add item to ListView
                            adminControl.listViewFiles.Items.Add(item);

                            // Set the Tag property to the full file path
                            item.Tag = fileInfo.FullName;
                        }
                    }

                    // Force a refresh of the ListView to ensure it's displaying correctly
                    adminControl.listViewFiles.Refresh();
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
        public string GetSubject(string selectedUserString, string alias)
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
                        string isSubject = line[3]; // Subject is in the 4th column (index 3)

                        EncryptionManager.EncryptFile(filePath); // Re-encrypt the file after processing
                        return isSubject;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception error: {e}...\nReturn string.Empty");
                return string.Empty; // If any exception occurs, return an empty string
            }

            return string.Empty; // Return an empty string if no subject is found or input is invalid
        }
        #endregion PROCESS
    }
}
