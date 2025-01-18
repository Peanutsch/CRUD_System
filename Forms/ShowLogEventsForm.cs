using CRUD_System.Encryption;
using CRUD_System.FileHandlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_System
{
    public partial class ShowLogEventsForm : Form
    {
        public ShowLogEventsForm()
        {
            InitializeComponent();
        }

        #region PROCESS LISTBOX LOGS
        /// <summary>
        /// Loads the log entries of a specified user (alias) into the ListBox, 
        /// sorting them in descending order by timestamp. Decrypts the log files for processing 
        /// and re-encrypts them afterward.
        /// </summary>
        /// <param name="alias">The alias of the user whose logs need to be loaded.</param>
        public void LoadListBoxLogs(string alias)
        {
            txtSelectedAlias.Text = alias;

            // Prepare the log files from all year directories
            List<string> logFiles = PrepareLogFiles(alias);

            if (logFiles.Any())
            {
                var allLogEntries = new List<Tuple<DateTime, string>>();

                foreach (string logFile in logFiles)
                {
                    // Parse log entries from each file into structured data
                    var logEntries = ParseLogFile(logFile);

                    // Combine entries from all files
                    allLogEntries.AddRange(logEntries);

                    // Re-encrypt the log file after processing
                    EncryptionManager.EncryptFile(logFile);
                }

                // Sort log entries by date/time in descending order
                var sortedEntries = SortLogEntriesDescending(allLogEntries);

                // Populate the ListBox with the sorted log entries
                PopulateListBox(sortedEntries);
            }
        }

        /// <summary>
        /// Finds and decrypts log files for the specified alias across all year directories.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        /// <returns>A list of decrypted log file paths.</returns>
        private List<string> PrepareLogFiles(string alias)
        {
            var logFiles = new List<string>();

            // Use FindCSVFiles.FindFilesInFolders to search for directories across multiple years
            List<string> logDirectories = FindCSVFiles.FindFilesInFolders(alias, "logevents");

            foreach (string directory in logDirectories)
            {
                if (Directory.Exists(directory))
                {
                    // Find all CSV files in the directory
                    string[] files = Directory.GetFiles(directory, "*.csv", SearchOption.TopDirectoryOnly);

                    foreach (string file in files)
                    {
                        // Decrypt the file and add it to the list
                        EncryptionManager.DecryptFile(file);
                        logFiles.Add(file);
                    }
                }
            }

            return logFiles;
        }


        /// <summary>
        /// Parses the log file into a list of tuples containing the timestamp and the full log entry string.
        /// </summary>
        /// <param name="logFile">The path to the log file.</param>
        /// <returns>A list of log entries with timestamps for sorting.</returns>
        private List<Tuple<DateTime, string>> ParseLogFile(string logFile)
        {
            var logEntries = new List<Tuple<DateTime, string>>();

            // Read all lines from the file
            var lines = File.ReadAllLines(logFile);

            foreach (var line in lines)
            {
                // Split each line into parts
                var parts = line.Split(',');

                // Ensure the line has the expected number of parts
                if (parts.Length >= 4)
                {
                    string date = parts[0];         // Date dd-MM-yyyy
                    string time = parts[1];         // Time HH:mm:ss
                    string aliasInLog = parts[2];   // Alias
                    string logEvent = parts[3];     // Log event

                    // Combine date and time into a single DateTime object
                    if (DateTime.TryParse($"{date} {time}", out DateTime logDateTime))
                    {
                        // Add the parsed log entry as a tuple (timestamp, full entry string)
                        logEntries.Add(new Tuple<DateTime, string>(logDateTime, $"{date} {time} {aliasInLog} {logEvent}"));
                    }
                }
            }

            return logEntries;
        }

        /// <summary>
        /// Sorts the log entries by their timestamp in descending order (most recent first).
        /// </summary>
        /// <param name="logEntries">The list of log entries with timestamps.</param>
        /// <returns>A list of log entry strings sorted by timestamp.</returns>
        private List<string> SortLogEntriesDescending(List<Tuple<DateTime, string>> logEntries)
        {
            // Order entries by the DateTime component and return only the log strings
            return logEntries
                .OrderByDescending(entry => entry.Item1)
                .Select(entry => entry.Item2)
                .ToList();
        }

        /// <summary>
        /// Populates the ListBox control with sorted log entries.
        /// </summary>
        /// <param name="sortedEntries">The sorted list of log entry strings.</param>
        private void PopulateListBox(List<string> sortedEntries)
        {
            listBoxLogs.Items.Clear();

            foreach (var entry in sortedEntries)
            {
                listBoxLogs.Items.Add(entry);

                // Check if the current entry contains "logged IN"
                if (entry.Contains("logged IN"))
                {
                    listBoxLogs.Items.Add("======="); // Extra line in listBoxLogs as divider
                }
            }
        }
        #endregion PROCESS LISTBOX LOGS

        public void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
