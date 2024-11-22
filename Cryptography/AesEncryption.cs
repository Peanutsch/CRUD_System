using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    /// <summary>
    /// Provides AES-256 encryption and decryption methods, allowing for secure handling of sensitive data. 
    /// Includes functionality for generating random salts and deriving encryption keys from a password.
    /// </summary>
    public static class AesEncryption
    {
        internal static string EncryptionKey = "kw2qgJYEA2zKxNtY/xGU8hehZabBbN/ptirj47tX2b7kRPtrLGnRDVFGpioZfiF6";

        /// <summary>
        /// Encrypt the plaintext and return the encrypted bytes
        /// </summary>
        /// <param name="plainText">The text to be encrypted.</param>
        /// <param name="password">The password used to derive the encryption key.</param>
        /// <returns>A encrypted byte array</returns>
        public static byte[] Encrypt(string plainText, string password)
        {
            using (Aes aes = Aes.Create())
            {
                // Generate a new salt
                byte[] salt = GenerateSalt();
                // Derive a key from the password and salt
                using (var rfc2898 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
                {
                    aes.Key = rfc2898.GetBytes(32); // AES-256
                }

                using (var ms = new MemoryStream())
                {
                    // Prepend the salt to the encrypted file
                    ms.Write(salt, 0, salt.Length);
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(plainText);
                    }
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Decrypts an AES-256 encrypted byte array back to plaintext.
        /// </summary>
        /// <param name="cipherText">The encrypted data to be decrypted.</param>
        /// <param name="password">The password used to derive the decryption key.</param>
        /// <returns>A decrypted byte array.</returns>
        // Decrypt the encrypted bytes back to plaintext
        public static string Decrypt(byte[] cipherText, string password)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    using (var ms = new MemoryStream(cipherText))
                    {
                        byte[] salt = new byte[16]; // Salt size
                        ms.Read(salt, 0, salt.Length);

                        using (var rfc2898 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
                        {
                            aes.Key = rfc2898.GetBytes(32);
                        }

                        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        using (var reader = new StreamReader(cs))
                        {
                            string decrypted = reader.ReadToEnd();
                            return decrypted ?? string.Empty; // Return empty if decrypted is null
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Decryption failed. Error: {ex.Message}");
                return string.Empty; // Return empty string if exception occurs
            }
        }



        /*
        /// <summary>
        /// Decrypts an AES-256 encrypted byte array back to plaintext.
        /// </summary>
        /// <param name="cipherText">The encrypted data to be decrypted.</param>
        /// <param name="password">The password used to derive the decryption key.</param>
        /// <returns>A decrypted byte array.</returns>
        // Decrypt the encrypted bytes back to plaintext
        public static string Decrypt(byte[] cipherText, string password)
        {
            using (Aes aes = Aes.Create())
            {
                using (var ms = new MemoryStream(cipherText))
                {
                    // Read the salt from the beginning of the memory stream
                    byte[] salt = new byte[16]; // Salt size
                    ms.Read(salt, 0, salt.Length);

                    // Derive the key from the password and salt
                    using (var rfc2898 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
                    {
                        aes.Key = rfc2898.GetBytes(32);
                    }

                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
        */

        /// <summary>
        /// Generates a 16-byte random salt for encryption.
        /// </summary>
        /// <returns>A byte array representing the salt.</returns>
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16]; // 16 bytes salt
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
