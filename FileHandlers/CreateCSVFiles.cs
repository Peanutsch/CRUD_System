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
        string currentYear = Timers.CurrentYear.ToString();

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
                    string noticesPath = Path.Combine(rootPath, "cis_notices", Timers.CurrentYear.ToString(), alias);

                    // Zorg ervoor dat de alias-map in cis_notices bestaat
                    if (!Directory.Exists(noticesPath))
                    {
                        Directory.CreateDirectory(noticesPath);
                    }

                    string fileCisNotices = Path.Combine(noticesPath, $"{alias}_cis_notices.csv");

                    if (!File.Exists(fileCisNotices))
                    {
                        // Maak een nieuwe leeg bestand aan
                        File.WriteAllText(fileCisNotices, $"{string.Empty},{string.Empty},{string.Empty}" + Environment.NewLine);

                        // Versleutel het bestand
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
        /// Creates a {alias}_logs.csv file in the "Logs" directory.
        /// Creates line {date},{},{}
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

                    // Zorg ervoor dat de alias-map in logs bestaat
                    if (!Directory.Exists(logsPath))
                    {
                        Directory.CreateDirectory(logsPath);
                    }

                    string fileLogs = Path.Combine(logsPath, $"{alias}_logevents.csv");

                    if (!File.Exists(fileLogs))
                    {
                        // Maak een nieuwe leeg bestand aan
                        File.WriteAllText(fileLogs, $"{string.Empty},{string.Empty},{string.Empty},{string.Empty}" + Environment.NewLine);

                        // Versleutel het bestand
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
        /// Creates a {alias}_reports.csv file in the "Logs" directory.
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateReportsCSV(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                string rootPath = RootPath.GetRootPath();

                try
                {
                    // Bouw het pad naar de "reports" map en de submap voor de alias
                    string reportsPath = Path.Combine(rootPath, "reports", Timers.CurrentYear.ToString(), alias);

                    // Zorg ervoor dat de alias-map in reports bestaat
                    if (!Directory.Exists(reportsPath))
                    {
                        Directory.CreateDirectory(reportsPath);
                    }

                    // Bepaal het volledige pad voor het rapportbestand
                    string fileReports = Path.Combine(reportsPath, $"{alias}_reports.csv");

                    if (!File.Exists(fileReports))
                    {
                        // Maak een nieuwe leeg bestand aan
                        File.WriteAllText(fileReports, $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," +
                                                       $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," + Environment.NewLine);

                        // Versleutel het bestand
                        EncryptionManager.EncryptFile(fileReports);

                        Debug.WriteLine($"Created {fileReports}");
                    }
                }
                catch (Exception ex)
                {
                    // Handel fouten af en geef debug- en gebruikersvriendelijke berichten
                    Debug.WriteLine($"An error occurred for alias {alias}: {ex.Message}");
                    MessageBox.Show($"An error occurred for alias {alias}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Alias cannot be null or empty.");
            }
        }
    }
}
