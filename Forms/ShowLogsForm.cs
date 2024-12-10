using CRUD_System.FileHandlers;
using CRUD_System.Handlers;
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
    public partial class ShowLogsForm : Form
    {
        public ShowLogsForm()
        {
            InitializeComponent();
        }

        #region LISTBOX LOGS
        /// <summary>
        /// Loads the log entries of a specified user (alias) into the ListBox, 
        /// sorting them in descending order by timestamp. Decrypts the log file for processing 
        /// and re-encrypts it afterward.
        /// </summary>
        /// <param name="alias">The alias of the user whose logs need to be loaded.</param>
        public void LoadListBoxLogs(string alias)
        {
            txtSelectedAlias.Text = alias;

            // Prepare the log file
            string? logFile = PrepareLogFile(alias);

            if (!string.IsNullOrEmpty(logFile))
            {
                // Parse log entries from the file into structured data
                var logEntries = ParseLogFile(logFile);

                // Step 3: Sort log entries by date/time in descending order
                var sortedEntries = SortLogEntriesDescending(logEntries);

                // Populate the ListBox with the sorted log entries
                PopulateListBox(sortedEntries);

                // Re-encrypt the log file after processing
                EncryptionManager.EncryptFile(logFile);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Finds the log file for the specified user alias and decrypts it if the file exists.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        /// <returns>The decrypted file path, or null if the file is not found.</returns>
        private string? PrepareLogFile(string alias)
        {
            // Find the file path for the user's logs
            string logFile = FindCSVFiles.FindCSVFile(alias, "logevents");

            // Check if the file exists
            if (File.Exists(logFile))
            {
                // Decrypt the file
                EncryptionManager.DecryptFile(logFile);
                return logFile;
            }

            // Return null if the file does not exist
            return null;
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
        #endregion ADMIN LISTBOX

        public void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
