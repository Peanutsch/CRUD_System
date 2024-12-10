using CRUD_System.FileHandlers;
using CRUD_System.Interfaces;
using CRUD_System.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_System.Handlers
{
    internal class ReportManager
    {
        private readonly RepositoryMessageBoxes message = new RepositoryMessageBoxes();

        #region CONSTRUCTOR
        private readonly AdminMainControl? adminControl;

        public ReportManager(AdminMainControl control)
        {
            adminControl = control;
        }
        #endregion CONSTRUCTOR

        /// <summary>
        /// Handles the logic for saving a report. Validates input, confirms the action with the user, 
        /// creates a new report CSV file, and refreshes the ListView to display the new report.
        /// </summary>
        public void btnSaveReport()
        {
            if (adminControl!.comboBoxSubjectReport.Text != "Subject:" && !string.IsNullOrEmpty(adminControl.rtxReport.Text))
            {
                
                var currentUser = AuthenticationService.CurrentUser;                                // Get the current logged-in user
                string selectedAlias = adminControl!.txtAlias.Text;                                 // Retrieve the alias of the user to whom the report is related
                string newReportText = $"{adminControl.rtxReport.Text.Replace(",", ";")}";      // Retrieve the report text, replacing "," with ";"
                string subject = adminControl.comboBoxSubjectReport.Text;                           // Retrieve the selected subject from the dropdown
                string timeStamp = DateTime.Now.ToString("ddMMyyyy-HHmmss");                        // Generate a unique timestamp for the report file

                // Confirm the save action with the user
                DialogResult dr = message.MessageConfirmSaveNote(selectedAlias);
                if (dr == DialogResult.No)
                {
                    return; // Exit if the user cancels the action
                }

                // Create the report file in the format: {Date},{CreatorAlias},{UserAlias},{Subject},{Report}
                CreateCSVFiles.CreateReportsCSV(timeStamp, currentUser!, selectedAlias, subject, newReportText);

                AdminInterface adminInterface = new AdminInterface();
                adminInterface.IsReport = false;
                Debug.WriteLine($"btnSaveReport IsReport: {adminInterface.IsReport}");
                adminInterface.TextBoxesReportConfig();
            }
            else
            {
                // Log and display a message if validation fails
                Debug.WriteLine("Not Valid! Missing conditions...");
                MessageBox.Show("Not Valid! Missing conditions...");
                return;
            }
            // Clear rtxReport, refresh ListViewFiles
            adminControl.rtxReport.Clear();
            RefreshListViewFiles();    
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

        /// <summary>
        /// Displays the report for a selected user by reading and decrypting the report file, 
        /// parsing its content, and updating the adminControl fields accordingly.
        /// </summary>
        /// <param name="selectedUserString">The selected user string, typically from a list box or list view.</param>
        /// <param name="selectedAlias">The alias of the selected user, used to locate the report file.</param>
        public void ReportDisplay(string selectedUserString, string selectedAlias)
        {
            // Get the root path and construct the file path for the report
            string rootPath = RootPath.GetRootPath();
            string isFileName = $"{selectedUserString}_report.csv";
            string filePath = Path.Combine(rootPath, "report", Timers.CurrentYear.ToString(), selectedAlias, isFileName);

            // Check if the adminControl is null to prevent NullReferenceException
            if (adminControl == null)
            {
                Debug.WriteLine("adminControl is null!");
                return;
            }

            // Decrypt the report file to make it readable
            EncryptionManager.DecryptFile(filePath);

            // Read the report file content
            string reportContent = File.ReadAllText(filePath);
            string[] reportContentSplit = reportContent.Split(","); // Split content by comma to extract fields
            string[] isFileNameSplit = isFileName.Split("_"); // Split filename to extract parts like date if needed

            // Ensure the report content has at least the expected number of elements
            if (reportContentSplit.Length >= 5)
            {
                // Parse the report content
                string reportCreator = reportContentSplit[1];               // Creator Alias
                string reportSubject = reportContentSplit[3];               // Subject
                string reportTextReport = reportContentSplit[4];     // // Full text, including commas
                string reportDate = isFileNameSplit[1].Replace("-", " ");   // Format date part of the filename (if applicable)

                // Update the admin control fields with parsed data
                adminControl.txtCreator.Text = reportCreator; // Creator
                adminControl.txtSubject.Text = reportSubject; // Subject
                adminControl.rtxReport.Text = $"{reportTextReport}".Replace(";", ",").Trim();

                // Format numbers as DD-MM-YYYY and clean up text
                adminControl.txtDateReport.Text = Regex.Replace(reportDate
                                                  .Replace("\"", ""),
                                                  @"(\d{2})(\d{2})(\d{4})", "$1-$2-$3")
                                                  .Trim();
            }
            else
            {
                // Show an error message if the report content is not in the expected format
                Debug.WriteLine("The report does not match the expected format.");
                MessageBox.Show("The report does not match the expected format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Re-encrypt the report file after processing to maintain security
            EncryptionManager.EncryptFile(filePath);
        }

        /// <summary>
        /// Parses a date string and returns it in "dd-MM-yyyy" format.
        /// </summary>
        /// <param name="dateString">The date string to parse.</param>
        /// <returns>The formatted date string, or "Invalid Date" if parsing fails.</returns>
        public string FormatDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime parsedDate))
            {
                return parsedDate.ToString("dd-MM-yyyy"); // Format as "dd-MM-yyyy"
            }
            else
            {
                Debug.WriteLine($"Failed to parse the date: {dateString}");
                return "Invalid Date"; // Fallback for invalid date
            }
        }
    }
}
