using System;
using System.Runtime.Intrinsics.Arm;
using System.Diagnostics;
using CRUD_System.FileHandlers;
using CRUD_System.Handlers;

namespace CRUD_System
{
    internal static class Program
    {
        static readonly FilePaths filePath = new FilePaths();

        /// <summary>
        /// The main entry point for the application.
        /// This method is responsible for encrypting the CSV file and initializing the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            #region Decryption
            // Decrypt the CSV file
            //EncryptionManager.DecryptFile(filePath.LoginFilePath); // data_login.csv
            //EncryptionManager.DecryptFile(filePath.UserFilePath); // data_users.csv
            //EncryptionManager.DecryptFile(filePath.HRFilePath);
            //DecryptSingleCSVFile("peer001", "report"); //--------------> Do not forget to check if DirectoryName is correct in method. Current setup: peer001
            //DecryptSingleCSVFile("paer001", "log");
            #endregion Decryption

            #region Encryption
            // Encrypt the CSV file
            //EncryptionManager.EncryptFile(filePath.LoginFilePath); // data_login.csv
            //EncryptionManager.EncryptFile(filePath.UserFilePath); // data_users.csv
            //EncryptionManager.EncryptFile(filePath.HRFilePath); // hr.csv
            //EncryptionManager.EncryptFile(filePath.ReportFilePath); // {alias}_report.csv

            //DecryptSingleCSVFile("mist001", "logEvents");
            //DecryptSingleCSVFile("peer001", "logEvents");
            #endregion Encryption

            #region Create/Edit CSV Files
            // Add new column in csv file
            //AddNewColumn(filePath.UserFilePath);

            // Create CIS Notice CSV Files in map cis_notices
            //CreateCISNoticeCSV();

            // Create log CSV Files in map Logs
            //CreateLogCSV();

            //Create reports CSV Files in map reports
            //CreateReportCSV();
            #endregion Create/Edit CSV Files

            // Initialize the application configuration
            //ApplicationConfiguration.Initialize();

            // Run the LoginForm as the main form of the application
            Application.Run(new LoginForm());
        }

        #region ADD NEW COLUMN
        /// <summary>
        /// Adds a new column to a CSV file
        /// </summary>
        static void AddNewColumn(string filePath)
        {
            // Do not forget to Decrypt and Encrypt file!!!!!

            // New column name and its default value for each row
            string newColumnName = "[9] Absence due Illness";
            string defaultValue = "False";

            // List to store updated lines of the CSV
            List<string> updatedLines = new List<string>();

            // Read all lines from the input CSV file
            string[] lines = File.ReadAllLines(filePath);

            // Check if the file contains any lines (not empty)
            if (lines.Length > 0)
            {
                // Add the new column to the header (first line of the file)
                string header = lines[0] + "," + newColumnName;
                updatedLines.Add(header);

                // Add the default value for the new column
                for (int i = 1; i < lines.Length; i++)
                {
                    string updatedLine = lines[i] + "," + defaultValue;
                    updatedLines.Add(updatedLine);
                }
            }

            // Write the updated lines to the output file
            File.WriteAllLines(filePath, updatedLines);

            Debug.WriteLine($"Updated CSV saved as {filePath}");
        }
        #endregion ADD NEW COLUMN

        #region CREATE CSV FILES
        /// <summary>
        /// Creates "cis_notices\currentYear\{alias}\{alias}_cis_notices.csv" directory.
        /// Using aliases from data_users.csv
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateCISNoticeCSV()
        {
            DataCache cache = new DataCache();
            
            // Load all data to cache
            cache.LoadDecryptedData();

            int counter = 1;

            Debug.WriteLine($"Items CatchedUserData: {cache.CachedUserData.Count}");

            foreach (var user in cache.CachedUserData.Skip(2)) // Skip header and Admin
            {
                string alias = user[2]; // Index 2 is the alias field

                if (!string.IsNullOrEmpty(alias))
                {
                    string rootPath = RootPath.GetRootPath();

                    try
                    {
                        string noticesPath = Path.Combine(rootPath, "cis_notices", Timers.CurrentYear.ToString(), alias);

                        // Ensure the cis_notices directory exists
                        if (!Directory.Exists(noticesPath))
                        {
                            Directory.CreateDirectory(noticesPath);
                        }

                        string file_cis_notices = Path.Combine(noticesPath, $"{alias}_cis_notices.csv");

                        if (!File.Exists(file_cis_notices))
                        {
                            // Create a new file with default headers (or leave empty)
                            File.WriteAllText(file_cis_notices, $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," +
                                                                $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}" + Environment.NewLine);

                            // Encrypt the file
                            EncryptionManager.EncryptFile(file_cis_notices);

                            Debug.WriteLine($"Created {alias}_cis_notices.csv");

                            counter++;
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

            Debug.WriteLine($"Created {counter} cis_notices csv files...");
        }

        /// <summary>
        /// Creates "logs\currentYear\{alias}\{alias}_logs.csv".
        /// Using aliases from data_users.csv
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateLogCSV()
        {
            DataCache cache = new DataCache();

            // Load all data to cache
            cache.LoadDecryptedData();

            int counter = 1;

            Debug.WriteLine($"Items CatchedUserData: {cache.CachedUserData.Count}");

            foreach (var user in cache.CachedUserData.Skip(2)) // Skip header and Admin
            {
                string alias = user[2]; // Index 2 is the alias field

                if (!string.IsNullOrEmpty(alias))
                {
                    string rootPath = RootPath.GetRootPath();

                    try
                    {
                        string logPath = Path.Combine(rootPath, "logevents", Timers.CurrentYear.ToString(), alias);

                        // Ensure the cis_notices directory exists
                        if (!Directory.Exists(logPath))
                        {
                            Directory.CreateDirectory(logPath);
                        }

                        string file_logs = Path.Combine(logPath, $"{alias}_logevents.csv");

                        if (!File.Exists(file_logs))
                        {
                            // Create a new file with default headers (or leave empty)
                            File.WriteAllText(file_logs, $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," +
                                                         $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}" + Environment.NewLine);

                            // Encrypt the file
                            EncryptionManager.EncryptFile(file_logs);

                            Debug.WriteLine($"Created {alias}_logevents.csv");

                            counter++;
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

            Debug.WriteLine($"Created {counter} logevents csv files...");
        }

        /// <summary>
        /// Creates "reports\currentYear\{alias}\{alias}_logs.csv" directory.
        /// Using aliases from data_users.csv
        /// </summary>
        /// <param name="alias">The alias of the user.</param>
        public static void CreateReportCSV()
        {
            DataCache cache = new DataCache();

            // Load all data to cache
            cache.LoadDecryptedData();

            int counter = 1;

            Debug.WriteLine($"Items CatchedUserData: {cache.CachedUserData.Count}");

            foreach (var user in cache.CachedUserData.Skip(2)) // Skip header and Admin
            {
                string alias = user[2]; // Index 2 is the alias field

                if (!string.IsNullOrEmpty(alias))
                {
                    string rootPath = RootPath.GetRootPath();

                    try
                    {
                        string reportsPath = Path.Combine(rootPath, "reports", Timers.CurrentYear.ToString(), alias);

                        // Ensure the cis_notices directory exists
                        if (!Directory.Exists(reportsPath))
                        {
                            Directory.CreateDirectory(reportsPath);
                        }

                        string file_report = Path.Combine(reportsPath, $"{alias}_report.csv");

                        if (!File.Exists(file_report))
                        {
                            // Create a new file with default headers (or leave empty)
                            File.WriteAllText(file_report, $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," +
                                                           $"{string.Empty},{string.Empty},{string.Empty},{string.Empty},{string.Empty}," + Environment.NewLine);

                            // Encrypt the file
                            EncryptionManager.EncryptFile(file_report);

                            Debug.WriteLine($"Created {alias}_report.csv");

                            counter++;
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

            Debug.WriteLine($"Created {counter} report csv files...");
        }
        #endregion CREATE CSV FILES

        #region DECRYPT FILE
        public static void DecryptSingleCSVFile(string alias, string directoryName)
        {
            string rootPath = RootPath.GetRootPath();
            string logPath = Path.Combine(rootPath, directoryName);
            
            //string file_logs = Path.Combine(logPath, $"{alias}_logs.csv");
            EncryptionManager.DecryptFile(Path.Combine(logPath, "2024", alias, $"{alias}_report.csv"));
        }
        #endregion DECRYPT FILE

        #region ENCRYPT FILE
        public static void EncryptFile(string alias)
        {
            // Set rootpath
            string rootPath = RootPath.GetRootPath();
            // Set map
            string logPath = Path.Combine(rootPath, "Logs");

            //string file_logs = Path.Combine(logPath, $"{alias}_logs.csv");
            EncryptionManager.EncryptFile(Path.Combine(logPath, $"{alias}_logs.csv"));
        }
        #endregion ENCRYPT FILE
    }
}
