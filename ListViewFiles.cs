using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_System
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
                string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv");

                // Check if any CSV files are found
                if (csvFiles.Length > 0)
                {
                    // Create an array of FileInfo objects for sorting
                    FileInfo[] fileInfos = csvFiles.Select(file => new FileInfo(file)).ToArray();

                    // Sort the files by CreationTime in descending order (newest first)
                    Array.Sort(fileInfos, (f1, f2) => f2.CreationTime.CompareTo(f1.CreationTime));

                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        // Create a ListViewItem for each file, using Name and Date
                        ListViewItem item = new ListViewItem(fileInfo.Name);
                        item.SubItems.Add(fileInfo.CreationTime.ToString("dd/MM/yyyy"));

                        // Set the Tag property to the full file path
                        item.Tag = fileInfo.FullName;

                        // Add the item to the ListView
                        adminControl.listViewFiles.Items.Add(item);
                    }

                    // Force a refresh of the ListView to ensure it's displaying correctly
                    adminControl.listViewFiles.Refresh();
                }
                else
                {
                    // No error message: directory and file will be made when report file created
                    return;
                }
            }
            else
            {
                // No error message: directory and file will be made when report file created
                return;
            }
        }


        /// <summary>
        /// Registers the event handler for the ListView's double-click event.
        /// </summary>
        /// <param name="onFileOpen">The action to perform when a file is double-clicked (e.g., open the file).</param>
        public void HandleDoubleClick(Action<string> onFileOpen)
        {
            adminControl.listViewFiles.DoubleClick += (sender, e) => // Subscribe DoubleClick event
            {
                // Check if any item is selected and if the Tag property is not null
                var selectedItem = adminControl.listViewFiles.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                //Debug.WriteLine($"ListViewFiles HandleDoubleClick selectedItem: {selectedItem}");

                if (selectedItem?.Tag is string filePath && !string.IsNullOrEmpty(filePath))
                {
                    // Invoke the action if filePath is valid
                    onFileOpen?.Invoke(filePath);
                }
                else
                {
                    // Handle invalid Tag or empty filePath
                    Debug.WriteLine("ListViewFiles HandleDoubleClick: The selected file does not have a valid path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            };
        }
        #endregion PROCESS
    }
}
