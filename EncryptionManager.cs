using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CRUD_System
{
    internal static class EncryptionManager
    {
        /// <summary>
        /// Encrypts the content of a CSV file and writes the encrypted content back to the same file.
        /// Each field in the CSV is encrypted individually using AES encryption.
        /// </summary>
        /// <param name="filePath">The path to the CSV file to be encrypted.</param>
        public static void EncryptFile(string filePath)
        {
            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(filePath);
            var encryptedLines = new List<string>();

            // Iterate through each line of the file
            foreach (string line in lines)
            {
                // Split the line into fields based on the comma separator
                string[] fields = line.Split(',');

                // Encrypt each field using a fixed encryption key
                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = AesEncryption.EncryptWithFixedKey(fields[i]);
                }

                // Combine the encrypted fields into a single line and add it to the encryptedLines list
                encryptedLines.Add(string.Join(",", fields));
            }

            // Write the encrypted lines back to the same file
            File.WriteAllLines(filePath, encryptedLines);

            // Log the completion of the encryption process
            Debug.WriteLine("\nEncryption completed for the file: " + filePath + "\n");

            // Display a message to inform the user that the encryption is complete
            MessageBox.Show("Encryption completed for the file: " + filePath);
        }

        /// <summary>
        /// Decrypts the content of an encrypted CSV file and writes the decrypted content back to the same file.
        /// Each field in the CSV is decrypted individually using AES decryption.
        /// </summary>
        /// <param name="filePath">The path to the CSV file to be decrypted.</param>
        public static void DecryptFile(string filePath)
        {
            // Read all lines from the encrypted CSV file
            string[] lines = File.ReadAllLines(filePath);
            var decryptedLines = new List<string>();

            // Iterate through each line of the file
            foreach (string line in lines)
            {
                // Split the line into fields based on the comma separator
                string[] fields = line.Split(',');

                // Decrypt each field using a fixed encryption key
                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = AesEncryption.DecryptWithFixedKey(fields[i], AesEncryption.EncryptionKey);
                }

                // Combine the decrypted fields into a single line and add it to the decryptedLines list
                decryptedLines.Add(string.Join(",", fields));
            }

            // Write the decrypted lines back to the same file
            File.WriteAllLines(filePath, decryptedLines);

            // Log the completion of the decryption process
            Debug.WriteLine("Decryption completed for the file: " + filePath + "\n");
            MessageBox.Show("Decryption completed for the file: " + filePath);
        }
    }
}
