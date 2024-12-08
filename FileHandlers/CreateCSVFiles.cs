using CRUD_System.Handlers;
using CRUD_System.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRUD_System.FileHandlers
{
    internal class CreateCSVFiles
    {
        /// <summary>
        /// Creates /cis_notices/current year/{alias}_cis_notices.csv.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateCISNoticeCSV(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                string rootPath = RootPath.GetRootPath();

                try
                {
                    string noticesPath = Path.Combine(rootPath, "cis_notices", Timers.CurrentYear.ToString(), alias);

                    // Ensure the alias folder exists within the cis_notices directory
                    if (!Directory.Exists(noticesPath))
                    {
                        Directory.CreateDirectory(noticesPath);
                    }

                    string fileCisNotices = Path.Combine(noticesPath, $"{alias}_cis_notices.csv");

                    if (!File.Exists(fileCisNotices))
                    {
                        // Create a new empty file
                        File.WriteAllText(fileCisNotices, $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," +
                                                          $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," + Environment.NewLine);

                        // Encrypt the file
                        EncryptionManager.EncryptFile(fileCisNotices);

                        Debug.WriteLine($"Created {fileCisNotices}");
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
        /// Creates /logEvents/current year/{alias}_logs.csv.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateLogCSV(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                string rootPath = RootPath.GetRootPath();

                try
                {
                    string logsPath = Path.Combine(rootPath, "logevents", Timers.CurrentYear.ToString(), alias);

                    // Ensure the alias folder exists within the logevents directory
                    if (!Directory.Exists(logsPath))
                    {
                        Directory.CreateDirectory(logsPath);
                    }

                    string fileLogs = Path.Combine(logsPath, $"{alias}_logevents.csv");

                    if (!File.Exists(fileLogs))
                    {
                        // Create a new empty file with a basic format
                        File.WriteAllText(fileLogs, $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," +
                                                    $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," + Environment.NewLine);

                        // Encrypt the file
                        EncryptionManager.EncryptFile(fileLogs);

                        Debug.WriteLine($"Created {fileLogs}");
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
        /// Creates /reports/current year/{alias}_reports.csv.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateReportsCSV(string dateFile, string aliasCreator, string selectedAlias, string subject, string report)
        {
            if (!string.IsNullOrEmpty(selectedAlias))
            {
                string rootPath = RootPath.GetRootPath();

                try
                {
                    // Build the path to the "reports" directory and the alias subdirectory
                    string reportsPath = Path.Combine(rootPath, "report", Timers.CurrentYear.ToString(), selectedAlias);

                    // Ensure the alias folder exists within the reports directory
                    if (!Directory.Exists(reportsPath))
                    {
                        Directory.CreateDirectory(reportsPath);
                    }

                    // Determine the full path for the report file
                    string fileReports = Path.Combine(reportsPath, $"{selectedAlias}_{dateFile}_report.csv");

                    if (!File.Exists(fileReports))
                    {
                        string currentDate = DateTime.Now.ToString();

                        // Create a new empty file with a detailed format {Date},{aliasCreater},{aliasUser},{subject},{Report}
                        File.WriteAllText(fileReports, $"{currentDate},{aliasCreator},{selectedAlias},{subject},{report}");

                        // Encrypt the file
                        EncryptionManager.EncryptFile(fileReports);

                        Debug.WriteLine($"Created {fileReports}");

                        RepositoryMessageBoxes message = new RepositoryMessageBoxes();
                        message.MessageReportSaved(DateTime.Now.ToString("ddMMyyyy-HHmmss"), selectedAlias);
                    }
                }
                catch (Exception ex)
                {
                    // Handle errors and provide both debug and user-friendly messages
                    Debug.WriteLine($"An error occurred for alias {selectedAlias}: {ex.Message}");
                    MessageBox.Show($"An error occurred for alias {selectedAlias}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Alias cannot be null or empty.");
            }
        }
    }
}
