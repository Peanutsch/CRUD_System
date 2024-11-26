using System;
using System.Runtime.Intrinsics.Arm;
using CRUD_System.FileHandlers;
using CRUD_System.Handlers;

namespace CRUD_System
{
    internal static class Program
    {
        static FilePaths filePath = new FilePaths();

        /// <summary>
        /// The main entry point for the application.
        /// This method is responsible for encrypting the CSV file and initializing the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // Encrypt the CSV file
            EncryptCSVFiles();

            // Decrypt the CSV file
            // DecryptCSVFiles();

            // Initialize the application configuration
            ApplicationConfiguration.Initialize();

            // Run the LoginForm as the main form of the application
            Application.Run(new LoginForm());
        }

        /// <summary>
        /// Encrypts the CSV file content using a fixed encryption key.
        /// The content is read from the specified file, encrypted, and saved back to the same file.
        /// </summary>
        static void EncryptCSVFiles() {
            // Get the path for the login CSV file
            string csvFilePath_login = filePath.LoginFilePath;

            // Specify the path to save the encrypted CSV file (can be the same or a new path)
            string encryptedFilePath = csvFilePath_login;

            // Step 1: Read the CSV content as a string
            string csvContent = File.ReadAllText(csvFilePath_login);

            // Step 2: Encrypt the CSV content using the fixed encryption key
            byte[] encryptedData = Crypto.EncryptWithFixedKey(csvContent);

            // Step 3: Save the encrypted data to a file
            File.WriteAllBytes(encryptedFilePath, encryptedData);
        }

        /// <summary>
        /// Decrypts an encrypted CSV file and saves the decrypted content back to a file.
        /// </summary>
        static void DecryptCSVFiles() {
            // File path of the encrypted CSV file
            string encryptedFilePath = filePath.LoginFilePath; // This points to the encrypted file
            string decryptedFilePath = encryptedFilePath; // Optionally overwrite the encrypted file with the decrypted content

            // Step 1: Read the encrypted data (byte array)
            byte[] encryptedFileData = File.ReadAllBytes(encryptedFilePath);

            // Step 2: Decrypt the data using the fixed key (either password or the fixed key)
            string decryptedContent = Crypto.DecryptWithFixedKey(encryptedFileData, Crypto.EncryptionKey);

            // Step 3: Save the decrypted content back to the file (or use it further as needed)
            File.WriteAllText(decryptedFilePath, decryptedContent);
        }
    }
}
