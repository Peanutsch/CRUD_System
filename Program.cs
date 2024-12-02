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
            #region Encryption / Decryption
            // Encrypt the CSV file
            //EncryptionManager.EncryptFile(filePath.LoginFilePath); // data_login.csv
            //EncryptionManager.EncryptFile(filePath.UserFilePath); // data_users.csv
            //EncryptionManager.EncryptFile(filePath.HRFilePath); // hr.csv


            // Decrypt the CSV file
            //EncryptionManager.DecryptFile(filePath.LoginFilePath); // data_login.csv
            //EncryptionManager.DecryptFile(filePath.UserFilePath); // data_users.csv
            //EncryptionManager.DecryptFile(filePath.HRFilePath);
            #endregion Encryption / Decryption

            // Add new column in csv file
            //AddNewColumn(filePath.UserFilePath);

            // Initialize the application configuration
            ApplicationConfiguration.Initialize();

            // Run the LoginForm as the main form of the application
            Application.Run(new LoginForm());
        }

        /// <summary>
        /// Adds a new column to a CSV file
        /// </summary>
        static void AddNewColumn(string filePath)
        {
            // Input and output file paths
            //string inputFile = filePath;
            //string outputFile = filePath;

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
    }
}
