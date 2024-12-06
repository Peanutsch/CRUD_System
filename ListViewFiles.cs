using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_System
{
    /// <summary>
    /// A class that handles the configuration and population of a ListView with file details.
    /// </summary>
    internal class ListViewFiles
    {
        private readonly AdminMainControl adminControl = new AdminMainControl();
        private readonly ListView listView;

        /// <summary>
        /// Initializes a new instance of the ListViewFiles class and sets up the ListView.
        /// </summary>
        /// <param name="adminControl">The AdminMainControl containing the ListView.</param>
        public ListViewFiles(AdminMainControl adminControl)
        {
            this.listView = adminControl.listViewFiles;
            ConfigureListView();
        }

        /// <summary>
        /// Configures the ListView with the desired view and columns.
        /// </summary>
        private void ConfigureListView()
        {
            // Add columns for Name, Size, and Date
            listView.Columns.Add("Name", 300);
            listView.Columns.Add("Size", 200);
            listView.Columns.Add("Date", 200);

            // Adjust column width to fit content and header size
            listView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            // Ensure header width adjusts properly
            listView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Loads files from a specified directory into the ListView.
        /// </summary>
        /// <param name="directoryPath">The directory path to load the files from.</param>
        public void LoadFilesIntoListView(string directoryPath, string alias)
        {
            // Check if the specified directory exists
            if (Directory.Exists(directoryPath))
            {
                // Clear the ListView before adding new items
                adminControl.listViewFiles.Items.Clear();

                // Retrieve the single file path using FindCSVFiles
                string file = FindCSVFiles.FindCSVFile(alias, "report");

                // Check if a valid file was returned
                if (!string.IsNullOrEmpty(file) && File.Exists(file))
                {
                    FileInfo fileInfo = new FileInfo(file);

                    // Create a ListViewItem for the file
                    ListViewItem item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add(fileInfo.Length.ToString() + " bytes");
                    item.SubItems.Add(fileInfo.CreationTime.ToString());
                    item.Tag = file; // Store the full file path in the Tag property

                    // Add the item to the ListView
                    adminControl.listViewFiles.Items.Add(item);
                }
                else
                {
                    MessageBox.Show("No file found for the specified alias.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Show an error message if the directory does not exist
                MessageBox.Show("The specified directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Registers the event handler for the ListView's double-click event.
        /// </summary>
        /// <param name="onFileOpen">The action to perform when a file is double-clicked (e.g., open the file).</param>
        public void HandleDoubleClick(Action<string> onFileOpen)
        {
            listView.DoubleClick += (sender, e) =>
            {
                // Check if any item is selected and if the Tag property is not null
                var selectedItem = listView.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
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
