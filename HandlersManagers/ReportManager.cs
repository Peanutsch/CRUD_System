using CRUD_System.Encryption;
using CRUD_System.FileHandlers;
using CRUD_System.Interfaces;
using CRUD_System.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private readonly RepositoryLogEvents logEvents = new RepositoryLogEvents();
        private readonly AdminMainControl? adminControl;

        readonly string rootPath = RootPath.GetRootPath();

        #region CONSTRUCTOR
        public ReportManager(AdminMainControl control)
        {
            adminControl = control;
        }
        #endregion CONSTRUCTOR

        #region BUTTONS
        /// <summary>
        /// Handles the logic for saving a report. Validates input, confirms the action with the user, 
        /// creates a new report CSV file, and refreshes the ListView to display the new report.
        /// </summary>
        public void BtnSaveReportHandler()
        {
            var currentUser = AuthenticationService.CurrentUser;
            string selectedAlias = adminControl!.txtAlias.Text;
            string newReportText = $"{adminControl.reportRichTxReport.Text.Replace(",", ";")}";
            string subject = adminControl.comboBoxSubjectReport.Text;
            string timeStamp = DateTime.Now.ToString("ddMMyyyy-HHmmss");

            if (adminControl!.comboBoxSubjectReport.Text != "Subject:" && 
                !string.IsNullOrEmpty(adminControl.reportRichTxReport.Text) &&
                !string.IsNullOrEmpty(adminControl.reportTxtAlias.Text))
            {
                DialogResult dr = message.MessageConfirmSaveReport(selectedAlias, subject);
                if (dr == DialogResult.No)
                {
                    return;
                }

                CreateCSVFiles.CreateReportsCSV(timeStamp, currentUser!, selectedAlias, subject, newReportText);

                // Reset to default state after saving
                AdminInterface adminInterface = new AdminInterface();

                AdminInterface.IsReport = false;
                ToggleReportMode(false); // Exit report mode
                adminInterface.ReportConfig();

                // Set EditMode back to true
                adminInterface.EditMode = true;
                adminInterface.InterfaceEditModeAdmin();
            }
            else
            {
                Debug.WriteLine("Button SaveReport> Report details is not complete! Subject and text are required...");
                message.MessageReportIsInvalid();
                return;
            }

            adminControl.reportRichTxReport.Clear();
            RefreshListViewFiles(selectedAlias);
        }

        /// <summary>
        /// Handles file uploads for a specific user by ensuring the upload directory exists and processing the file.
        /// </summary>
        /// <param name="selectedAlias">The alias of the user for whom the file is being uploaded.</param>
        /// <remarks>
        /// This method builds a path to the "reports" directory for the specified user,
        /// ensures the directory exists, and initiates the file upload process.
        /// </remarks>
        public void BtnUploadFileHandler(string selectedAlias)
        {
            // Build the path to the "reports" directory for the specified alias
            string uploadPath = Path.Combine(rootPath, "report", Timers.CurrentYear.ToString(), selectedAlias);

            // Ensure the alias directory exists within the "reports" folder
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Initialize the upload process
            UploadFile uploadFile = new UploadFile();
            UploadFile.Upload(uploadPath);
        }
        #endregion BUTTONS

        #region PROCESSING AND HANDLING
        /// <summary>
        /// Prepares the report data for saving by generating a timestamp, retrieving the current user,
        /// and sanitizing the report text.
        /// </summary>
        /// <param name="isReportText">The content or body of the report.</param>
        /// <returns>A tuple containing the timestamp, current user, and sanitized report text.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the current user is null.</exception>
        private static (string TimeStamp, string CurrentUser, string SanitizedText) PrepareReportData(string isReportText)
        {
            // Generate a unique timestamp for the report
            string timeStamp = DateTime.Now.ToString("ddMMyyyy-HHmmss");

            // Get the username of the currently authenticated user
            string? currentUser = AuthenticationService.CurrentUser ?? throw new InvalidOperationException("Current user is not authenticated.");

            // Ensure the report text doesn't contain commas by replacing them with semicolons.
            // Commas are breaking the text and only the part before the first comma will be used as report
            string sanitizedText = isReportText.Replace(",", ";");

            return (timeStamp, currentUser, sanitizedText);
        }

        /// <summary>
        /// Creates and saves a report for a newly created user account.
        /// </summary>
        /// <param name="isNewAlias">The alias of the newly created user account.</param>
        /// <param name="isSubject">The subject or title of the report.</param>
        /// <param name="isReportText">The content or body of the report.</param>
        public static void ReportSaveNewUser(string isNewAlias, string isSubject, string isReportText)
        {
            try
            {
                // Prepare report data
                var (timeStamp, currentUser, sanitizedText) = PrepareReportData(isReportText);

                // Save the report to a CSV file
                CreateCSVFiles.CreateReportsCSV(timeStamp, currentUser, isNewAlias, isSubject, sanitizedText);
                Debug.WriteLine($"Successfully created report for new user account {isNewAlias}!");
            }
            catch (Exception e)
            {
                // Log any errors that occur during report creation
                Debug.WriteLine($"Error creating report for new user account {isNewAlias}:\n{e}");
            }
        }

        /// <summary>
        /// Creates and saves a report for a deleted user account.
        /// </summary>
        /// <param name="isAlias">The alias of the deleted user account.</param>
        /// <param name="isSubject">The subject or title of the report.</param>
        /// <param name="isReportText">The content or body of the report.</param>
        public static void ReportDeleteUser(string isAlias, string isSubject, string isReportText)
        {
            try
            {
                // Prepare report data
                var (timeStamp, currentUser, sanitizedText) = PrepareReportData(isReportText);

                // Save the report to a CSV file
                CreateCSVFiles.CreateReportsCSV(timeStamp, currentUser, isAlias, isSubject, sanitizedText);
                Debug.WriteLine($"Successfully created report for deleted user account {isAlias}!");

                // Disable and Clear listViewFiles
                AdminMainControl adminControl = new AdminMainControl();
                adminControl.listViewReports.Items.Clear();
                adminControl.listViewReports.Enabled = false;
                
            }
            catch (Exception e)
            {
                // Log any errors that occur during report creation
                Debug.WriteLine($"Error creating report for deleted user account {isAlias}:\n{e}");
            }
        }

        /// <summary>
        /// Deletes a selected report file.
        /// Validates the selection, confirms the action, and processes the file deletion.
        /// </summary>
        public void DeleteFileReport()
        {
            // Get the name of the selected file
            string selectedFile = adminControl!.listViewReports.SelectedItems[0].Text;
            string fileName = selectedFile + "_report.csv"; // Append the "_report.csv" suffix

            // Validate and retrieve the file path to delete
            var (fileToDelete, currentUser) = ValidateAndGetFileToDelete(fileName);

            if (fileToDelete != null)
            {
                // Delete the file if validation is successful
                ProcessDeleteFile(fileToDelete, fileName, currentUser!);
                adminControl.reportRichTxReport.Clear();
                adminControl.reportRichTxReport.ReadOnly = true;
            }
        }

        /// <summary>
        /// Validates the selected report file and retrieves its path if it exists.
        /// Checks if a file is selected, locates the file in the directories, and confirms deletion with the user.
        /// </summary>
        /// <returns>A tuple containing the file path to delete and the current user, or null if validation fails.</returns>
        private (string? fileToDelete, string? currentUser) ValidateAndGetFileToDelete(string fileName)
        {
            // Check if a file is selected in the ListView
            if (!adminControl!.btnDeleteFileReport.Visible || adminControl.listViewReports.SelectedItems.Count == 0)
            {
                // Notify the user that no file is selected
                MessageBox.Show("Please select a file to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return (null, null);
            }

            // Locate directories containing report files
            List<string> reportDirectories = FindCSVFiles.FindFilesInFolders(adminControl.txtAlias.Text, "report");

            if (!reportDirectories.Any())
            {
                // Notify the user that no report directory is found
                MessageBox.Show("Report directory not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (null, null);
            }

            // Check if the file exists in any of the report directories
            foreach (var reportDirectory in reportDirectories)
            {
                string fileToDelete = Path.Combine(reportDirectory, fileName);

                if (File.Exists(fileToDelete)) // File exists in the directory
                {
                    var currentUser = AuthenticationService.CurrentUser;
                    Debug.WriteLine($"CurrentUser: {currentUser}");

                    // Ask for user confirmation before deletion
                    DialogResult dr = message.MessageConfirmDeleteFile(fileName);
                    if (dr == DialogResult.Yes)
                    {
                        return (fileToDelete, currentUser); // Return the path of the file to delete and the current user
                    }
                }
            }

            return (null, null);
        }


        /// <summary>
        /// Deletes the specified file and updates the UI.
        /// Removes the file from the filesystem and the ListView, and notifies the user of the result.
        /// </summary>
        /// <param name="fileToDelete">The path of the file to delete.</param>
        private void ProcessDeleteFile(string fileToDelete, string fileName, string currentUser)
        {
            try
            {
                // Delete the file from the filesystem
                File.Delete(fileToDelete);

                // Remove the deleted file from the ListView
                adminControl!.listViewReports.Items.Remove(adminControl.listViewReports.SelectedItems[0]);

                // Notify the user of successful deletion
                Debug.WriteLine($"File [{fileName}] successfully deleted...");
                message.MessageReportDeletedSucces(fileName);

                if (!string.IsNullOrEmpty(currentUser))
                {
                    logEvents.LogEventReportDeleted(currentUser!, adminControl.reportTxtAlias.Text, fileName);
                }
                else
                {
                    logEvents.LogEventReportDeleted("UNKNOWN", adminControl.reportTxtAlias.Text, fileName);
                }

            }
            catch (Exception ex)
            {
                // Notify the user of an error during deletion
                message.MessageReportDeletedError(fileToDelete, ex.Message);
            }
        }
        #endregion PROCESSING AND HANDLING

        #region REFRESH LISTVIEWREPORTS
        /// <summary>
        /// Refreshes the ListView with the latest report files for the given alias.
        /// </summary>
        public void RefreshListViewFiles(string selectedAlias)
        {
            // Ensure the AdminMainControl instance is available
            if (adminControl == null)
                return;

            // Clear the ListView before refreshing to remove any existing items
            adminControl.listViewReports.Items.Clear();

            // Get the report directories for the given alias
            List<string> reportDirectories = GetReportDirectories(selectedAlias);

            // Check if any report directories were found
            if (reportDirectories.Any())
            {
                // Process each report directory
                foreach (string reportDirectory in reportDirectories)
                {
                    ProcessReportFilesInDirectory(reportDirectory);
                }

                // Refresh the ListView to ensure it visually updates with new data
                adminControl.listViewReports.Refresh();
            }
        }

        /// <summary>
        /// Retrieves the report directories for the specified alias from the "report" folder.
        /// </summary>
        /// <param name="alias">The alias used to find specific report directories.</param>
        /// <returns>A list of report directories for the given alias.</returns>
        public List<string> GetReportDirectories(string selectedAlias)
        {
            // Call FindCSVFiles to get the list of report directories for the alias
            return FindCSVFiles.FindFilesInFolders(selectedAlias, "report");
        }

        /// <summary>
        /// Processes all report files within the specified directory, adds them to the ListView, and sorts them by creation time.
        /// </summary>
        /// <param name="reportDirectory">The directory containing the report files to process.</param>
        public void ProcessReportFilesInDirectory(string reportDirectory)
        {
            // Check if the report directory exists before attempting to process it
            if (Directory.Exists(reportDirectory))
            {
                // Get all CSV report files in the directory and subdirectories
                string[] reportFiles = Directory.GetFiles(reportDirectory, "*.csv", SearchOption.AllDirectories);

                // Convert file paths to FileInfo objects for sorting and handling
                FileInfo[] fileInfos = reportFiles.Select(file => new FileInfo(file)).ToArray();

                // Sort files by creation time in descending order (newest first)
                Array.Sort(fileInfos, (f1, f2) => f2.CreationTime.CompareTo(f1.CreationTime));

                // Process each file and add it to the ListView
                foreach (var fileInfo in fileInfos)
                {
                    // Split the file name to extract useful information (alias, subject, etc.)
                    string[] itemSplit = fileInfo.Name.Split("_");
                    if (itemSplit.Length >= 2)
                    {
                        // Create an instance of ListViewFiles (or use the existing one) to retrieve the file's subject
                        ListViewReports listViewFiles = new ListViewReports();
                        string itemUse = string.Join("_", itemSplit[0], itemSplit[1]);
                        (string reportSubject, string reportCreator) = listViewFiles.GetSubjectAndCreatorAlias(itemUse, itemSplit[0]); // itemSplit[0] is alias

                        // Create a new ListViewItem for each report file
                        ListViewItem item = new ListViewItem(itemUse);

                        // Add file subject as a subitem (or "Unknown" if not found)
                        item.SubItems.Add(!string.IsNullOrEmpty(reportCreator) ? reportCreator : "Unknown"); // subitem 1: creator
                        item.SubItems.Add(!string.IsNullOrEmpty(reportSubject) ? reportSubject : "Unknown"); // subitem 2: subject

                        // Add the created item to the ListView
                        adminControl?.listViewReports.Items.Add(item);

                        // Store the full file path in the Tag property of the item
                        item.Tag = fileInfo.FullName;
                    }
                    else
                    {
                        // Log if the file format is invalid (unable to extract alias and subject)
                        Debug.WriteLine($"Invalid file format: {fileInfo.Name}");
                    }
                }
            }
            else
            {
                // Log if the report directory doesn't exist
                Debug.WriteLine($"Directory does not exist: {reportDirectory}");
            }
        }
        #endregion REFRESH LISTVIEWREPORTS

        #region REPORT DISPLAY
        /// <summary>
        /// Displays the user's report by preparing, decrypting, parsing, and updating the admin control fields.
        /// Includes error handling for missing files and format issues.
        /// </summary>
        /// <param name="selectedUserReportFileName">The filename of the user's report, excluding the extension.</param>
        /// <param name="selectedAlias">The alias of the selected user, used to locate the report file.</param>
        public void ReportDisplay(string selectedUserReportFileName, string selectedAlias)
        {
            try
            {
                // Prepare and decrypt the report
                string filePath = PrepareAndDecryptReport(selectedUserReportFileName, selectedAlias);

                // Parse and display the report content
                ParseAndDisplayReport(filePath, selectedUserReportFileName);

                // Re-encrypt the report file after processing
                EncryptionManager.EncryptFile(filePath);
            }
            catch (FileNotFoundException ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("The report file could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("The report content does not match the expected format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error: {ex.Message}");
                MessageBox.Show("An unexpected error occurred while processing the report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Extracts the year from the filename, which is expected to follow the format alias_date_timestamp_report.csv.
        /// </summary>
        /// <param name="isFileName">The filename containing the date information.</param>
        /// <returns>The extracted year as a string.</returns>
        public static string GetYearFolder(string isFileName)
        {
            // Find the part of the filename that contains the date
            string isDate = isFileName.Split('_')[1].Substring(0, 8);

            // Extract the year from the date (last 4 characters)
            string getYear = isDate.Substring(4, 4);

            return getYear;
        }

        /// <summary>
        /// Constructs the file path for the report, verifies its existence, and decrypts it for further processing.
        /// </summary>
        /// <param name="selectedUserReportFileName">The filename of the report, excluding the extension.</param>
        /// <param name="selectedAlias">The alias of the selected user.</param>
        /// <returns>The decrypted file path of the report.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the report file does not exist.</exception>
        private string PrepareAndDecryptReport(string selectedUserReportFileName, string selectedAlias)
        {
            // Construct the report file path.
            // Format: rootPath\"report"\yearFolder\selectedAlias\isFileName
            string isFileName = $"{selectedUserReportFileName}_report.csv";
            string yearFolder = GetYearFolder(selectedUserReportFileName);
            string filePath = Path.Combine(rootPath, "report", yearFolder, selectedAlias, isFileName);

            // Check if the file exists before attempting to decrypt
            if (!File.Exists(filePath))
            {
                Debug.WriteLine($"File not found: {filePath}");
                throw new FileNotFoundException("Report file not found.", filePath);
            }

            // Decrypt the file
            EncryptionManager.DecryptFile(filePath);

            return filePath; // Return the file path
        }

        /// <summary>
        /// Reads, parses, and displays the content of the decrypted report file.
        /// Updates the admin control fields with the extracted data.
        /// </summary>
        /// <param name="filePath">The file path of the decrypted report.</param>
        /// <param name="selectedUserReportFileName">The filename of the report, excluding the extension.</param>
        /// <exception cref="FormatException">Thrown if the report content does not match the expected format.</exception>
        private void ParseAndDisplayReport(string filePath, string selectedUserReportFileName)
        {
            // Read the content of the report file
            string reportContent = File.ReadAllText(filePath);
            string[] reportContentSplit = reportContent.Split(","); // Split content by comma
            string[] isFileNameSplit = selectedUserReportFileName.Split("_"); // Split filename

            // Check if the content is correctly formatted
            if (reportContentSplit.Length < 5)
            {
                throw new FormatException("The report content does not match the expected format.");
            }

            // Parse the content of the report
            string reportCreator = reportContentSplit[1]; // Creator Alias
            string reportSubject = reportContentSplit[3]; // Subject
            string reportTextReport = reportContentSplit[4]; // Full text, including commas
            string reportDate = isFileNameSplit[1].Replace("-", " "); // Format date part

            // Ensure adminControl is not null
            if (adminControl == null)
            {
                Debug.WriteLine("adminControl is null!");
                return;
            }

            // Update the fields in adminControl
            adminControl.reportTxtCreator.Text = reportCreator; // Creator
            adminControl.reportTxtSubject.Text = reportSubject; // Subject
            adminControl.reportRichTxReport.Text = reportTextReport.Replace(";", ",").Trim();
            adminControl.reportTxtDate.Text = Regex.Replace(reportDate.Replace("\"", ""), @"(\d{2})(\d{2})(\d{4})", "$1-$2-$3").Trim();
        }
        #endregion REPORT DISPLAY

        #region TOGGLE REPORT MODE
        /// <summary>
        /// Toggles the application between report mode and standard mode.
        /// </summary>
        /// <param name="enable">
        /// A boolean value indicating whether to enable report mode.
        /// If <c>true</c>, report mode is activated; otherwise, standard mode is enabled.
        /// </param>
        /// <remarks>
        /// This method updates the visibility, read-only state, and appearance of various UI elements
        /// based on the provided <paramref name="enable"/> parameter.
        /// </remarks>
        public void ToggleReportMode(bool enable)
        {
            // Create an instance of the AdminInterface to manage global states and UI logic
            AdminInterface adminInterface = new AdminInterface();

            // Update the IsReport flag in the AdminInterface class
            AdminInterface.IsReport = enable;

            // Clear all report-related text boxes to reset the UI
            adminInterface.TextBoxesReportEmpty();

            // Configure the visibility and state of standard mode controls
            adminControl!.reportTxtSubject.Visible = !AdminInterface.IsReport; // Subject text box is visible only in standard mode
            adminControl.reportTxtCreator.Visible = !AdminInterface.IsReport; // Creator text box is visible only in standard mode
            adminControl.reportRichTxReport.ReadOnly = !AdminInterface.IsReport; // Report text box is read-only in standard mode
            adminControl.reportLBLCreatedBy.Visible = !AdminInterface.IsReport; // "Created By" label is visible only in standard mode
            adminControl.reportLBLCurrentDate.Visible = AdminInterface.IsReport; // "Current Date" label is visible only in report mode

            // Configure visibility of report-specific controls
            adminControl.comboBoxSubjectReport.Visible = AdminInterface.IsReport; // Subject dropdown is visible only in report mode

            // Configure report-related area
            adminControl.reportTxtDate.Text = DateTime.Now.ToString("dd-MM-yyyy"); // Set current date in the report date text box
            adminControl.btnCreateReport.Text = AdminInterface.IsReport ? "Exit" : "Report"; // Toggle button text based on mode
            adminControl.reportRichTxReport.BackColor = AdminInterface.IsReport ? Color.White : Color.LightGray; // Adjust text box background color
            adminControl.btnSaveReport.Visible = AdminInterface.IsReport; // Show or hide "Save Report" button based on mode

            // Clear any selected items in the list view to reset its state
            adminControl.listViewReports.SelectedItems.Clear();
            adminControl.listViewReports.Enabled = !AdminInterface.IsReport;
        }
        #endregion TOGGLE REPORT MODE
    }
}
