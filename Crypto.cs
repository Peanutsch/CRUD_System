using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    internal static class Crypto
    {
        /*
            Encryption Key:         l8GGfRqI8hEzw00+WP0SMpptSNw/WjgSeF74sbUL1LizDXicc5wi4YsbLDdTyMqH
            Cipher:	                aes-256-cbc
            Initialization Vector:	c2144d621e8a153a5bcc5bffc2c6cab3
            Encrypted Data:	        4dcb75a90bf851625a0f9f6e7289f25f6f628be4288ee80f6eed530129110adb
            Password:	            ZtJeqp!#RB2+rqF%b9kjwXD%+t&KM6pm
            Encryption Key length:	64
            Base64:     	        base64:bDhHR2ZScUk4aEV6dzAwK1dQMFNNcHB0U053L1dqZ1NlRjc0c2JVTDFMaXpEWGljYzV3aTRZc2JMRGRUeU1xSA==
            md5 hash:	            73ae8a2d066063e8f5b400fffc63dcf4
         */

        // The fixed encryption key (32 bytes) for AES encryption.
        public static string EncryptionKey { get; } = "l8GGfRqI8hEzw00+WP0SMpptSNw/WjgSeF74sbUL1LizDXicc5wi4YsbLDdTyMqH";

        // The fixed initialization vector (IV) used in encryption/decryption.
        public static string VI { get; } = "c2144d621e8a153a5bcc5bffc2c6cab3";

        /// <summary>
        /// Encrypts the given plain text using a fixed encryption key and a random IV.
        /// The IV is prefixed to the encrypted data.
        /// </summary>
        /// <param name="plainText">The plain text string to be encrypted.</param>
        /// <returns>A byte array containing the IV and encrypted data.</returns>
        public static byte[] EncryptWithFixedKey(string plainText) {
            using (Aes aes = Aes.Create()) {
                aes.Key = GetKeyFromPassword(EncryptionKey);
                aes.GenerateIV(); // Generate a random IV

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream()) {
                    // Write the IV at the beginning of the stream
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs)) {
                        writer.Write(plainText);
                    }

                    return ms.ToArray(); // Return the combined IV and encrypted data
                }
            }
        }

        /// <summary>
        /// Decrypts the given cipher text (which includes an IV) using a fixed password.
        /// The IV is extracted from the beginning of the cipher text before decryption.
        /// </summary>
        /// <param name="cipherTextWithIv">The byte array containing the IV and encrypted data.</param>
        /// <param name="password">The password used to derive the decryption key.</param>
        /// <returns>The decrypted plain text.</returns>
        public static string DecryptWithFixedKey(byte[] cipherTextWithIv, string password) {
            using (Aes aes = Aes.Create()) {
                aes.Key = GetKeyFromPassword(password);

                using (var ms = new MemoryStream(cipherTextWithIv)) {
                    // Read the IV from the beginning of the stream
                    byte[] iv = new byte[aes.BlockSize / 8];
                    ms.Read(iv, 0, iv.Length);

                    using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cs)) {
                        return reader.ReadToEnd(); // Return the decrypted plain text
                    }
                }
            }
        }

        /// <summary>
        /// Generates a 256-bit key from the provided password using SHA-256 hashing.
        /// </summary>
        /// <param name="password">The password used to derive the encryption key.</param>
        /// <returns>A 256-bit encryption key derived from the password.</returns>
        private static byte[] GetKeyFromPassword(string password) {
            using (var sha256 = SHA256.Create()) {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); // Return the hashed key
            }
        }
    }
}
