using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    internal static class EncryptionManager
    {
        /// <summary>
        /// Encrypts the content of a CSV file and saves it back to the same file.
        /// </summary>
        /// <param name="filePath">The path to the CSV file to be encrypted.</param>
        public static void EncryptFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            var encryptedLines = new List<string>();

            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = AesEncryption.EncryptWithFixedKey(fields[i]);
                }
                encryptedLines.Add(string.Join(",", fields));
            }

            File.WriteAllLines(filePath, encryptedLines);
            Debug.WriteLine("Encryption completed for the file: " + filePath);
        }

        /// <summary>
        /// Decrypts the content of an encrypted CSV file and saves it back to the same file.
        /// </summary>
        /// <param name="filePath">The path to the CSV file to be decrypted.</param>
        public static void DecryptFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            var decryptedLines = new List<string>();

            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = AesEncryption.DecryptWithFixedKey(fields[i], AesEncryption.EncryptionKey);
                }
                decryptedLines.Add(string.Join(",", fields));
            }

            File.WriteAllLines(filePath, decryptedLines);
            Debug.WriteLine("Decryption completed for the file: " + filePath);
        }
    }
}
