using System;
using System.Runtime.Intrinsics.Arm;
using System.Diagnostics;
using CRUD_System.FileHandlers;
using CRUD_System.Handlers;

namespace CRUD_System
{
    internal static class Program
    {
        static FilePaths filePath = new FilePaths();
        // Get the path for the login CSV file
        //string csvFilePath_login = filePath.LoginFilePath;

        /// <summary>
        /// The main entry point for the application.
        /// This method is responsible for encrypting the CSV file and initializing the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // Encrypt the CSV file
            //EncryptCSVFiles(filePath.LoginFilePath); // data_login.csv
            //EncryptCSVFiles(filePath.UserFilePath); // data_users.csv

            // Decrypt the CSV file
            //DecryptCSVFiles(filePath.LoginFilePath);
            //DecryptCSVFiles(filePath.UserFilePath);

            // Initialize the application configuration
            ApplicationConfiguration.Initialize();

            // Run the LoginForm as the main form of the application
            Application.Run(new LoginForm());
        }

        /// <summary>
        /// Encrypts the CSV file content using a fixed encryption key.
        /// The content is read from the specified file, encrypted, and saved back to the same file.
        /// </summary>
        static void EncryptCSVFiles(string filePath)
        {
            // Read all lines from the CSV file
            string[] linesLogin = File.ReadAllLines(filePath);

            // Create a list to store the encrypted lines
            var encryptedLines = new List<string>();

            // Iterate through each line in the file
            foreach (string line in linesLogin)
            {
                // Split the line into fields based on commas
                string[] fields = line.Split(',');

                // Encrypt each field
                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = Crypto.EncryptWithFixedKey(fields[i]);
                }

                // Add the modified (encrypted) line to the list
                encryptedLines.Add(string.Join(",", fields));
            }

            // Optionally, write the encrypted lines back to the file
            File.WriteAllLines(filePath, encryptedLines);

            Console.WriteLine("Encryption completed for the CSV file.");
        }

        /// <summary>
        /// Decrypts an encrypted CSV file (saved as Base64) and saves the decrypted content back to the file.
        /// </summary>
        static void DecryptCSVFiles(string filePath)
        {
            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(filePath);

            // Create a list to store the decrypted lines
            var decryptedLines = new List<string>();

            // Iterate through each line in the file
            foreach (string line in lines)
            {
                // Split the line into fields based on commas
                string[] fields = line.Split(',');

                // Decrypt each field
                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = Crypto.DecryptWithFixedKey(fields[i], Crypto.EncryptionKey);
                }

                // Add the modified (decrypted) line to the list
                decryptedLines.Add(string.Join(",", fields));
            }

            // Overwrite the original CSV file with the decrypted content
            File.WriteAllLines(filePath, decryptedLines);

            Console.WriteLine("Decryption of all columns completed.");
        }
    }
}
