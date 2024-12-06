using CRUD_System.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System.FileHandlers
{
    internal class CreateCSVFiles
    {
        #region CREATE CSV FILES
        /// <summary>
        /// Creates a {alias}_cis_notices.csv file in the "cis_notices" directory.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateCISNoticeCSV(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                string rootPath = RootPath.GetRootPath();

                try
                {
                    string noticesPath = Path.Combine(rootPath, "cis_notices");

                    // Ensure the cis_notices directory exists
                    if (!Directory.Exists(noticesPath))
                    {
                        Directory.CreateDirectory(noticesPath);
                    }

                    string file_cis_notices = Path.Combine(noticesPath, $"{alias}_cis_notices.csv");

                    if (!File.Exists(file_cis_notices))
                    {
                        // Create a new file with string.Empty as default)
                        File.WriteAllText(file_cis_notices, $"{string.Empty},{string.Empty},{string.Empty}" + Environment.NewLine);

                        // Encrypt the file
                        EncryptionManager.EncryptFile(file_cis_notices);

                        Debug.WriteLine($"Created {alias}_cis_notices.csv");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"An error occurred for alias {alias}: {ex.Message}");
                    MessageBox.Show($"An error occurred for alias {alias}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Alias cannot be null or empty.");
            }
        }

        /// <summary>
        /// Creates a {alias}_logs.csv file in the "Logs" directory.
        /// Creates line {date},{},{}
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateLogCSV(string alias) //, string currentUser, string logEvent)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                string rootPath = RootPath.GetRootPath();

                try
                {
                    string logsPath = Path.Combine(rootPath, "Logs");

                    // Ensure the cis_notices directory exists
                    if (!Directory.Exists(logsPath))
                    {
                        Directory.CreateDirectory(logsPath);
                    }

                    string file_logs = Path.Combine(logsPath, $"{alias}_logs.csv");

                    if (!File.Exists(file_logs))
                    {
                        // Create a new file with string.Empty as default
                        File.WriteAllText(file_logs, $"{string.Empty},{string.Empty},{string.Empty},{string.Empty}" + Environment.NewLine);

                        // Encrypt the file
                        EncryptionManager.EncryptFile(file_logs);

                        Debug.WriteLine($"Created {alias}_logs.csv");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions gracefully (e.g., log the error or show a user-friendly message)
                    Debug.WriteLine($"An error occurred for alias {alias}: {ex.Message}");
                    MessageBox.Show($"An error occurred for alias {alias}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Alias cannot be null or empty.");
            }
        }

        /// <summary>
        /// Creates a {alias}_logs.csv file in the "Logs" directory.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateReportCSV(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                string rootPath = RootPath.GetRootPath();

                try
                {
                    string reportsPath = Path.Combine(rootPath, "reports");

                    // Ensure the cis_notices directory exists
                    if (!Directory.Exists(reportsPath))
                    {
                        Directory.CreateDirectory(reportsPath);
                    }

                    string file_reports = Path.Combine(reportsPath, $"{alias}_reports.csv");

                    if (!File.Exists(file_reports))
                    {
                        // Create a new file with string.Empty as default)
                        File.WriteAllText(file_reports, $"{string.Empty},{string.Empty},{string.Empty}" + Environment.NewLine);

                        // Encrypt the file
                        EncryptionManager.EncryptFile(file_reports);

                        Debug.WriteLine($"Created {alias}_reports.csv");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions gracefully (e.g., log the error or show a user-friendly message)
                    Debug.WriteLine($"An error occurred for alias {alias}: {ex.Message}");
                    MessageBox.Show($"An error occurred for alias {alias}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Alias cannot be null or empty.");
            }
        }
        #endregion CREATE CSV FILES
    }
}
