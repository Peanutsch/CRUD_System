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
        private readonly AdminMainControl adminControl;
        //private readonly System.Windows.Forms.ListView listView;

        /// <summary>
        /// Initializes a new instance of the ListViewFiles class and sets up the ListView.
        /// </summary>
        /// <param name="adminControl">The AdminMainControl containing the ListView.</param>
        public ListViewFiles(AdminMainControl? adminControl = null)
        {
            this.adminControl = adminControl ?? new AdminMainControl();
        }

        /// <summary>
        /// Loads files from a specified directory into the ListView.
        /// </summary>
        /// <param name="directoryPath">The directory path to load the files from.</param>
        public void LoadFilesIntoListView(string directoryPath, string selectedAlias)
        {
            adminControl.listViewFiles.Visible = true;

            if (!string.IsNullOrEmpty(selectedAlias))
            {
                Debug.WriteLine($"ListViewFiles Selected Alias: {selectedAlias}");
                string reportDirectory = FindCSVFiles.FindReportFile(selectedAlias, "report");
            }

            // Check if the specified directory exists
            if (Directory.Exists(directoryPath) && !string.IsNullOrEmpty(directoryPath))
            {
                // Retrieve all CSV files in the directory
                string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv");

                // Check if any CSV files are found
                if (csvFiles.Length > 0)
                {
                    foreach (string file in csvFiles)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        Debug.WriteLine($"ListViewFiles Adding File: {fileInfo.Name}");
                        //Debug.WriteLine($"ListViewFiles File Name: {fileInfo.Name}, Size: {fileInfo.Length}, Creation Time: {fileInfo.CreationTime}");

                        // Create a ListViewItem for each file
                        ListViewItem item = new ListViewItem(fileInfo.Name);
                        item.SubItems.Add(fileInfo.Length.ToString() + " bytes");
                        item.SubItems.Add(fileInfo.CreationTime.ToString());

                        // Add the item to the ListView
                        adminControl.listViewFiles.Items.Add(item);
                    }

                    // Force a refresh of the ListView to ensure it's displaying correctly
                    adminControl.listViewFiles.Refresh();
                }
                else
                {
                    MessageBox.Show("No CSV files found in the specified directory.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                // Show an error message if the directory does not exist
                MessageBox.Show("The specified directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        /// <summary>
        /// Registers the event handler for the ListView's double-click event.
        /// </summary>
        /// <param name="onFileOpen">The action to perform when a file is double-clicked (e.g., open the file).</param>
        public void HandleDoubleClick(Action<string> onFileOpen)
        {
            adminControl.listViewFiles.DoubleClick += (sender, e) =>
            {
                // Check if any item is selected and if the Tag property is not null
                var selectedItem = adminControl.listViewFiles.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                if (selectedItem?.Tag is string filePath && !string.IsNullOrEmpty(filePath))
                {
                    // Invoke the action if filePath is valid
                    onFileOpen?.Invoke(filePath);
                }
                else
                {
                    // Handle invalid Tag or empty filePath
                    MessageBox.Show("The selected file does not have a valid path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }
    }
}
