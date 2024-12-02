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
        static void Main() 
        {
            #region Encryption / Decryption
            // Encrypt the CSV file
            //EncryptionManager.EncryptFile(filePath.LoginFilePath); // data_login.csv
            //EncryptionManager.EncryptFile(filePath.UserFilePath); // data_users.csv

            // Decrypt the CSV file
            //EncryptionManager.DecryptFile(filePath.LoginFilePath); // data_login.csv
            //EncryptionManager.DecryptFile(filePath.UserFilePath); // data_users.csv
            #endregion Encryption / Decryption

            // Initialize the application configuration
            ApplicationConfiguration.Initialize();

            // Run the LoginForm as the main form of the application
            Application.Run(new LoginForm());
        }
    }
}
