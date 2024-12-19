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
        public void btnSaveReportHandler()
        {
            if (adminControl!.comboBoxSubjectReport.Text != "Subject:" && !string.IsNullOrEmpty(adminControl.rtxReport.Text) &&
                !string.IsNullOrEmpty(adminControl.txtAliasReport.Text) && !string.IsNullOrEmpty(adminControl.txtDateReport.Text))
            {
                var currentUser = AuthenticationService.CurrentUser;
                string selectedAlias = adminControl!.txtAlias.Text;
                string newReportText = $"{adminControl.rtxReport.Text.Replace(",", ";")}";
                string subject = adminControl.comboBoxSubjectReport.Text;
                string timeStamp = DateTime.Now.ToString("ddMMyyyy-HHmmss");

                DialogResult dr = message.MessageConfirmSaveNote(selectedAlias);
                if (dr == DialogResult.No)
                {
                    return;
                }

                CreateCSVFiles.CreateReportsCSV(timeStamp, currentUser!, selectedAlias, subject, newReportText);

                // Reset to default state after saving
                ToggleReportMode(false); // Exit report mode
            }
            else
            {
                Debug.WriteLine("Button SaveReport> Not Valid! Missing conditions...");
                MessageBox.Show("Not Valid! Missing conditions...");
                return;
            }

            adminControl.rtxReport.Clear();
            RefreshListViewFiles();
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
            string? currentUser = AuthenticationService.CurrentUser
                ?? throw new InvalidOperationException("Current user is not authenticated.");

            // Ensure the report text doesn't contain commas by replacing them with semicolons
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
                adminControl.listViewFiles.Items.Clear();
                adminControl.listViewFiles.Enabled = false;
                
            }
            catch (Exception e)
            {
                // Log any errors that occur during report creation
                Debug.WriteLine($"Error creating report for deleted user account {isAlias}:\n{e}");
            }
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
                    string[] itemSplit = fileInfo.Name.Split("_");
                    if (itemSplit.Length >= 2)
                    {
                        ListViewFiles listViewFiles = new ListViewFiles();
                        string itemUse = string.Join("_", itemSplit[0], itemSplit[1]);
                        string subject = listViewFiles.GetSubject(itemUse, itemSplit[0]); // itemSplit[0] is alias

                        // Create ListViewItem
                        ListViewItem item = new ListViewItem(itemUse);

                        ////// GET FILE SUBJECT AS SUBITEMS ////
                        item.SubItems.Add(!string.IsNullOrEmpty(subject) ? subject : "Unknown");

                        // Add item to ListView
                        adminControl.listViewFiles.Items.Add(item);

                        // Set the Tag property to the full file path
                        item.Tag = fileInfo.FullName;
                    }
                    else
                    {
                        // Log if the directory is not found
                        Debug.WriteLine($"Directory not found: {reportDirectory}");
                    }

                    // Ensure the ListView visually updates
                    listView.Refresh();
                }
            }
        }
        #endregion PROCESSING AND HANDLING

        #region REPORT DISPLAY
        /// <summary>
        /// Displays the report for a selected user by reading and decrypting the report file, 
        /// parsing its content, and updating the adminControl fields accordingly.
        /// </summary>
        /// <param name="selectedUserString">The selected user string, typically from a list box or list view.</param>
        /// <param name="selectedAlias">The alias of the selected user, used to locate the report file.</param>
        public void ReportDisplay(string selectedUserString, string selectedAlias)
        {
            // Get the root path and construct the file path for the report
            
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
                string reportTextReport = reportContentSplit[4];            // Full text, including commas
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
            adminControl!.txtSubject.Visible = !AdminInterface.IsReport; // Subject text box is visible only in standard mode
            adminControl.txtCreator.Visible = !AdminInterface.IsReport; // Creator text box is visible only in standard mode
            adminControl.rtxReport.ReadOnly = !AdminInterface.IsReport; // Report text box is read-only in standard mode
            adminControl.lblCreatedBy.Visible = !AdminInterface.IsReport; // "Created By" label is visible only in standard mode
            adminControl.lblCurrentDate.Visible = AdminInterface.IsReport; // "Current Date" label is visible only in report mode

            // Configure visibility of report-specific controls
            adminControl.comboBoxSubjectReport.Visible = AdminInterface.IsReport; // Subject dropdown is visible only in report mode

            // Configure report-related area
            adminControl.txtDateReport.Text = DateTime.Now.ToString("dd-MM-yyyy"); // Set current date in the report date text box
            adminControl.btnCreateReport.Text = AdminInterface.IsReport ? "Exit" : "Report"; // Toggle button text based on mode
            adminControl.rtxReport.BackColor = AdminInterface.IsReport ? Color.White : Color.LightGray; // Adjust text box background color
            adminControl.btnSaveReport.Visible = AdminInterface.IsReport; // Show or hide "Save Report" button based on mode

            // Clear any selected items in the list view to reset its state
            adminControl.listViewFiles.SelectedItems.Clear();
        }
        #endregion TOGGLE REPORT MODE
    }
}

/*
/// <summary>
/// Displays the report for a selected user by reading and decrypting the report file, 
/// parsing its content, and updating the adminControl fields accordingly.
/// </summary>
/// <param name="selectedUserString">The selected user string, typically from a list box or list view.</param>
/// <param name="selectedAlias">The alias of the selected user, used to locate the report file.</param>
public void ReportDisplay(string selectedUserString, string selectedAlias)
{
    // Get the array of report file paths
    string[] filePaths = GetReportFilePaths(selectedUserString, selectedAlias);

    // Check if there are any files in the array
    if (filePaths.Length == 0)
    {
        Debug.WriteLine("No report files found!");
        MessageBox.Show("No report files found for the selected user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    // Select the first file in the array (you can modify this logic to select another file if needed)
    string filePath = filePaths[0];

    // Check if the adminControl is null to prevent NullReferenceException
    if (adminControl == null)
    {
        Debug.WriteLine("adminControl is null!");
        return;
    }

    // Decrypt and read the report file content
    string reportContent = DecryptAndReadReport(filePath);
    if (string.IsNullOrEmpty(reportContent))
        return;

    // Parse and update the admin control fields
    if (ParseAndUpdateReportContent(reportContent, filePath))
    {
        Debug.WriteLine("The report was successfully loaded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // Re-encrypt the report file after processing
    EncryptionManager.EncryptFile(filePath);
}

/// <summary>
/// Constructs the file path for the report and returns all files in the directory.
/// </summary>
private string[] GetReportFilePaths(string selectedUserString, string selectedAlias)
{
    // Get the full directory path for the selected user and alias
    string directoryPath = Path.Combine(rootPath, "report", Timers.CurrentYear.ToString(), selectedAlias);

    // Check if the directory exists
    if (Directory.Exists(directoryPath))
    {
        // Retrieve all files in the directory
        return Directory.GetFiles(directoryPath, "*.*"); // Get all files with any extension
    }
    else
    {
        return Array.Empty<string>(); // Return an empty array if directory does not exist
    }
}

/// <summary>
/// Decrypts and reads the report file content.
/// </summary>
private string DecryptAndReadReport(string filePath)
{
    if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
    {
        Debug.WriteLine($"Filepath {filePath} is null or does not exist");
        MessageBox.Show($"Filepath {filePath} is null or does not exist");
        return string.Empty;
    }

    // Decrypt the report file and read its content
    EncryptionManager.DecryptFile(filePath);
    string reportContent = File.ReadAllText(filePath);
    return reportContent;
}

/// <summary>
/// Parses the report content and updates the admin control fields.
/// </summary>
private bool ParseAndUpdateReportContent(string reportContent, string filePath)
{
    string[] reportContentSplit = reportContent.Split(",");
    string[] fileNameSplit = Path.GetFileName(filePath).Split("_");

    // Ensure the report content has at least the expected number of elements
    if (reportContentSplit.Length < 5)
    {
        MessageBox.Show("The report does not match the expected format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }

    // Parse the report content
    string reportCreator = reportContentSplit[1]; // Creator Alias
    string reportSubject = reportContentSplit[3]; // Subject
    string reportTextReport = reportContentSplit[4]; // Full text, including commas
    string reportDate = fileNameSplit[1].Replace("-", " "); // Format date part of the filename (if applicable)

    // Update the admin control fields with parsed data
    adminControl!.txtCreator.Text = reportCreator;
    adminControl.txtSubject.Text = reportSubject;
    adminControl.rtxReport.Text = reportTextReport.Replace(";", ",").Trim();
    adminControl.txtDateReport.Text = FormatDate(reportDate);

    return true;
}

/// <summary>
/// Formats the date as DD-MM-YYYY.
/// </summary>
private string FormatDate(string date)
{
    return Regex.Replace(date.Replace("\"", ""), @"(\d{2})(\d{2})(\d{4})", "$1-$2-$3").Trim();
}
*/
