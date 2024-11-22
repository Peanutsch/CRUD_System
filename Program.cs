using System;
using System.IO;
using System.Windows.Forms;
using CRUD_System.FileHandlers;
using CRUD_System.Handlers;

namespace CRUD_System
{
    internal static class Program
    {
        static FilePaths filePath = new FilePaths();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Encrypt data_users.csv and data_login.csv
            //EncryptCSV();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }

        /// <summary>
        /// Encrypts the contents of `data_users.csv` and `data_login.csv` using AES encryption.
        /// The original files are overwritten with their encrypted counterparts.
        /// </summary>
        /// <remarks>
        /// Ensure the encryption key is securely stored and only accessible to authorized parts of the system.
        /// This method should be executed only once unless re-encryption is explicitly required.
        /// </remarks>
        /// <exception cref="Exception">
        /// Thrown if an error occurs during the encryption process, such as file access issues.
        /// </exception>
        public static void EncryptCSV()
        {
            var encryptionManager = new EncryptionManager();

            string encryptionKey = AesEncryption.EncryptionKey;
            string data_users = filePath.UserFilePath;
            string data_login = filePath.LoginFilePath;

            try
            {
                // data_users.csv
                encryptionManager.EncryptData(data_users, data_users, encryptionKey);
                Console.WriteLine("data_users.csv encrypted");

                // data_login.csv
                encryptionManager.EncryptData(data_login, data_login, encryptionKey);
                Console.WriteLine("data_login.csv encrypted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encrypting CSV files: {ex.Message}");
                MessageBox.Show($"Error encrypting CSV files: {ex.Message}", "Encryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
