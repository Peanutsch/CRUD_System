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
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //EncryptCSVFiles();
            //DecryptCSVFiles();


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());

        }

        static void EncryptCSVFiles() {

            //string string csvFilePath_users = filePath.LoginFilePath;
            string csvFilePath_login = filePath.LoginFilePath;

            // Example: Encrypt and save a CSV file
            //string csvFilePath = @"path\to\your\data.csv";
            string encryptedFilePath = csvFilePath_login;

            // Step 1: Read the CSV content
            string csvContent = File.ReadAllText(csvFilePath_login);

            // Step 2: Encrypt the CSV content
            byte[] encryptedData = Crypto.EncryptWithFixedKey(csvContent);

            // Step 3: Save the encrypted data to a new file
            File.WriteAllBytes(encryptedFilePath, encryptedData);
        }

        static void DecryptCSVFiles() {
            // Bestandslocatie van het versleutelde CSV bestand
            string encryptedFilePath = filePath.LoginFilePath; // Dit verwijst naar het versleutelde bestand
            string decryptedFilePath = encryptedFilePath;

            // Stap 1: Lees de versleutelde data (byte-array)
            byte[] encryptedFileData = File.ReadAllBytes(encryptedFilePath);

            // Stap 2: Ontsleutel de data met de vaste sleutel (gebruik het wachtwoord of de vaste sleutel)
            string decryptedContent = Crypto.DecryptWithFixedKey(encryptedFileData, Crypto.EncryptionKey);

            // Stap 3: Sla de ontsleutelde inhoud op in een nieuw bestand (of gebruik de string verder)
            File.WriteAllText(decryptedFilePath, decryptedContent);
        }

    }
}