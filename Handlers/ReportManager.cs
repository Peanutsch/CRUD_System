using CRUD_System.FileHandlers;
using CRUD_System.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_System.Handlers
{
    internal class ReportManager
    {
        //private readonly ListView listView = new ListView();
        private readonly RepositoryMessageBoxes message = new RepositoryMessageBoxes();

        #region CONSTRUCTOR
        private readonly AdminMainControl? adminControl;

        public ReportManager(AdminMainControl? adminControl = null)
        {
            this.adminControl = adminControl ?? new AdminMainControl();
        }
        #endregion CONSTRUCTOR
        /// <summary>
        /// Handles the logic for saving a report. Validates input, confirms the action with the user, 
        /// creates a new report CSV file, and refreshes the ListView to display the new report.
        /// </summary>
        public void ButtonSaveReport_ClickHandler()
        {
            // Get the current logged-in user
            var currentUser = AuthenticationService.CurrentUser;
            // Retrieve the alias of the user to whom the report is related
            string selectedAlias = adminControl!.txtAlias.Text;
            // Retrieve the report text, enclosed in quotes
            string newReportText = $"\"{adminControl.rtxReport.Text}\"";
            // Retrieve the selected subject from the dropdown
            string subject = adminControl.comboBoxSubjectReport.Text;
            // Generate a unique timestamp for the report file
            string dateFile = DateTime.Now.ToString("ddMMyyyy-HHmmss");

            // Validate the input fields
            if (!string.IsNullOrEmpty(newReportText) &&
                !string.IsNullOrEmpty(selectedAlias) &&
                adminControl.comboBoxSubjectReport.Text != "Subject:")
            {
                // Confirm the save action with the user
                DialogResult dr = message.MessageConfirmSaveNote(selectedAlias);
                if (dr == DialogResult.No)
                {
                    return; // Exit if the user cancels the action
                }

                // Create the report file in the format: {Date},{CreatorAlias},{UserAlias},{Subject},{Report}
                CreateCSVFiles.CreateReportsCSV(dateFile, currentUser!, selectedAlias, subject, newReportText);

                // Refresh the ListView to show the new report
                RefreshListViewFiles();
            }
            else
            {
                // Log and display a message if validation fails
                Debug.WriteLine("Not Valid! Missing conditions...");
                MessageBox.Show("Not Valid! Missing conditions...");
                return;
            }

            // Clear the report text box after saving
            adminControl.rtxReport.Text = string.Empty;
        }

        /// <summary>
        /// Refreshes the ListView in the UI to display the latest report files.
        /// Clears the current items, fetches the report files from the directory, 
        /// sorts them by creation time, and updates the ListView with the file details.
        /// </summary>
        public void RefreshListViewFiles()
        {
            // Ensure the AdminMainControl instance is available
            if (adminControl == null) return;

            // Reference the ListView in the UI
            var listView = adminControl.listViewFiles;
            listView.Items.Clear();

            // Find the directory containing the report files for the given alias
            string reportDirectory = FindCSVFiles.FindReportFile(adminControl.txtAlias.Text, "report");

            // Check if the directory exists
            if (Directory.Exists(reportDirectory))
            {
                // Get all report files in the directory
                string[] reportFiles = Directory.GetFiles(reportDirectory, "*.csv");
                // Convert file paths to FileInfo objects for sorting and details
                FileInfo[] fileInfos = reportFiles.Select(file => new FileInfo(file)).ToArray();

                // Sort files by creation time in descending order (newest first)
                Array.Sort(fileInfos, (f1, f2) => f2.CreationTime.CompareTo(f1.CreationTime));

                // Add each file to the ListView
                foreach (var fileInfo in fileInfos)
                {
                    // Create a ListViewItem with the file name
                    ListViewItem item = new ListViewItem(fileInfo.Name);
                    // Add the creation date as a sub-item
                    item.SubItems.Add(fileInfo.CreationTime.ToString("dd/MM/yyyy"));
                    // Store the full file path in the Tag property
                    item.Tag = fileInfo.FullName;

                    // Add the item to the ListView
                    listView.Items.Add(item);
                }
            }
            else
            {
                // Log if the directory is not found
                Debug.WriteLine($"Directory not found: {reportDirectory}");
            }

            // Ensure the ListView visually updates
            listView.Refresh();
        }

        public void DisplayReport(string fileName, string alias)
        {
            Debug.WriteLine($"DisplayReport: {fileName}");
            string rootPath = RootPath.GetRootPath();
            string filePath = Path.Combine(rootPath, "report", Timers.CurrentYear.ToString(), alias, fileName);

            var reportContent = File.ReadAllText(filePath);
            var reportContentSplit = reportContent.Split(",");

            // Ensure that the data contains enough elements to assign to the controls
            if (reportContentSplit.Length >= 5)
            {
                // Assign the split data to the appropriate fields
                adminControl!.txtDateReport.Text = reportContentSplit[0]; // Date
                adminControl.txtAliasNotes.Text = reportContentSplit[1];   // Creator Alias
                //adminControl.txtSelectedAlias.Text = reportContentSplit[2];  // Selected Alias
                adminControl.txtSubject.Text = reportContentSplit[3];        // Subject
                adminControl.rtxReport.Text = reportContentSplit[4].Trim('"'); // Report text
            }
            else
            {
                // Show an error message if the data does not match the expected format
                Debug.WriteLine("The report does not match the expected format.");
                MessageBox.Show("The report does not match the expected format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
        /// <summary>
        /// Loads the report content into the form controls from the provided report content string.
        /// </summary>
        /// <param name="reportContent">The content of the report as a comma-separated string.</param>
        public void LoadReport(string reportContent)
        {
            if (!string.IsNullOrEmpty(reportContent))
            {
                // Split the content of the report based on commas (considering that content can be inside quotes)
                string[] reportData = reportContent.Split(new[] { ',' }, 5); // Split into 4 parts maximum

                // Ensure that the data contains enough elements to assign to the controls
                if (reportData.Length == 5)
                {
                    // Assign the split data to the appropriate fields
                    txtDate.Text = FormatDate(reportData[0]); // Date
                    txtAliasCreator.Text = reportData[1];   // Creator Alias
                    txtSelectedAlias.Text = reportData[2];  // Selected Alias
                    txtSubject.Text = reportData[3];        // Subject
                    rtxtDisplayReport.Text = reportData[4].Trim('"'); // Report text
                }
                else
                {
                    // Show an error message if the data does not match the expected format
                    Debug.WriteLine("The report does not match the expected format.");
                    MessageBox.Show("The report does not match the expected format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Show an error message if the report content is empty
                Debug.WriteLine("The report content is empty.");
                MessageBox.Show("The report content is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
    }
}
