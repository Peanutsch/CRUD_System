using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    /// <summary>
    /// Encrypts data in a CSV file and writes the encrypted content to a specified output file.
    /// </summary>
    internal class EncryptionManager
    {
        #region Encrypt
        /// <summary>
        /// Encrypts the CSV data and saves it to a specified file.
        /// </summary>
        /// <param name="csvData">The CSV data to encrypt.</param>
        /// <param name="filePath">The path where the encrypted file should be saved.</param>
        /// <param name="password">The password used for encryption.</param>
        public void EncryptData(string csvFilePath, string outputFilePath, string encryptionKey)
        {
            // Read each line from the CSV file
            var lines = File.ReadAllLines(csvFilePath);
            var encryptedLines = new List<string>();

            foreach (var line in lines)
            {
                var fields = line.Split(',');
                for (int i = 0; i < fields.Length; i++)
                {
                    // Encrypt each field
                    fields[i] = Convert.ToBase64String(AesEncryption.Encrypt(fields[i], encryptionKey));
                }
                // Join encrypted fields and add to list
                encryptedLines.Add(string.Join(",", fields));
            }

            // Write the encrypted data back to the specified file
            File.WriteAllLines(outputFilePath, encryptedLines);
        }
        #endregion Encrypt

        #region Decrypt
        /// <summary>
        /// Decrypts the CSV data from a specified file.
        /// </summary>
        /// <param name="filePath">The path of the encrypted CSV file.</param>
        /// <param name="password">The password used for decryption.</param>
        /// <returns>The decrypted CSV data.</returns>
        public static string DecryptCsv(string filePath, string password)
        {
            // Read each line from the encrypted CSV file
            var encryptedLines = File.ReadAllLines(filePath);
            var decryptedLines = new List<string>();

            foreach (var encryptedLine in encryptedLines)
            {
                // Split the line into encrypted fields
                var encryptedFields = encryptedLine.Split(',');
                var decryptedFields = new List<string>();

                foreach (var encryptedField in encryptedFields)
                {
                    // Convert the Base64 string back to bytes and decrypt each field
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedField);
                    string decryptedField = AesEncryption.Decrypt(encryptedBytes, password);
                    decryptedFields.Add(decryptedField);
                }

                // Join decrypted fields and add to the list of decrypted lines
                decryptedLines.Add(string.Join(",", decryptedFields));
            }

            // Join all decrypted lines with newlines to reconstruct the CSV structure
            return string.Join(Environment.NewLine, decryptedLines);
        }
        #endregion Decrypt
    }
}
