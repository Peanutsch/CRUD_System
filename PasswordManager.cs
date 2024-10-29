using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace CRUD_System
{
    /// <summary>
    /// Provides methods for securely generating and verifying password hashes using 
    /// the PBKDF2 (Password-Based Key Derivation Function 2) algorithm with SHA-256.
    /// Also includes methods for encrypting and decrypting CSV files.
    /// </summary>
    internal class PasswordManager
    {
        private const int SaltSize = 16; // 16 bytes for salt
        private const int HashSize = 20; // 20 bytes for hash
        private const int Iterations = 100000; // Recommended number of iterations

        /// <summary>
        /// Generates a secure random password of the specified length.
        /// </summary>
        /// <param name="length">The length of the password to be generated.</param>
        /// <returns>A random password string.</returns>
        public static string GenerateUserPassword(int length = 12)
        {
            const string lowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numericChars = "1234567890";

            // Ensure minimum length of 12 characters
            if (length < 12) length = 12; 

            // Create lists for each character type
            List<char> password = new List<char>();
            byte[] randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            // Add at least 3 uppercase letters
            for (int index = 0; index < 3; index++)
            {
                password.Add(upperCaseChars[randomBytes[index] % upperCaseChars.Length]);
            }

            // Add at least 3 numbers
            for (int index = 3; index < 6; index++)
            {
                password.Add(numericChars[randomBytes[index] % numericChars.Length]);
            }

            // Fill the remaining characters with random lowercase or uppercase letters and numbers
            const string validChars = lowerCaseChars + upperCaseChars + numericChars;
            for (int index = 6; index < length; index++)
            {
                password.Add(validChars[randomBytes[index] % validChars.Length]);
            }

            // Shuffle to ensure randomness
            password = password.OrderBy(_ => randomBytes[new Random().Next(randomBytes.Length)]).ToList();

            return new string(password.ToArray());
        }


        #region HASH
        /// <summary>
        /// Generates a hash for the specified password using PBKDF2.
        /// The generated hash includes a randomly generated salt.
        /// </summary>
        /// <param name="password">The plaintext password to be hashed.</param>
        /// <returns>
        /// A Base64-encoded string that contains the salt and the hash of the password.
        /// </returns>
        public static string GenerateHash(string password)
        {
            using (var hasher = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = hasher.GetBytes(HashSize);
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(hasher.Salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Verifies whether the provided password matches the stored hash.
        /// </summary>
        /// <param name="password">The plaintext password to verify.</param>
        /// <param name="storedHash">The stored hash to compare against, 
        /// which should include the salt.</param>
        /// <returns>
        /// True if the password matches the stored hash; otherwise, false.
        /// </returns>
        public static bool Verify(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            using (var hasher = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = hasher.GetBytes(HashSize);
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false; // Password does not match
                    }
                }
            }
            return true; // Password matches
        }

        /// <summary>
        /// Encrypts the CSV data and saves it to a specified file.
        /// </summary>
        /// <param name="csvData">The CSV data to encrypt.</param>
        /// <param name="filePath">The path where the encrypted file should be saved.</param>
        /// <param name="password">The password used for encryption.</param>
        public static void EncryptCsv(string csvData, string filePath, string password)
        {
            byte[] encryptedData = AesEncryption.Encrypt(csvData, password);
            File.WriteAllBytes(filePath, encryptedData);
        }

        /// <summary>
        /// Decrypts the CSV data from a specified file.
        /// </summary>
        /// <param name="filePath">The path of the encrypted CSV file.</param>
        /// <param name="password">The password used for decryption.</param>
        /// <returns>The decrypted CSV data.</returns>
        public static string DecryptCsv(string filePath, string password)
        {
            byte[] encryptedData = File.ReadAllBytes(filePath);
            return AesEncryption.Decrypt(encryptedData, password);
        }

        /// <summary>
        /// Verifies a password by hashing it and checking it against a stored hash.
        /// This method demonstrates how to hash a password and verify it 
        /// using the PBKDF2 algorithm. It outputs the hashed password and 
        /// indicates whether the entered password matches the hashed version.
        /// </summary>
        internal void VerifyPassword()
        {
            // Example of password hashing and verification using PBKDF2
            string password = "mijnVeiligWachtwoord";

            // Hash the password
            string hashedPassword = PasswordManager.GenerateHash(password);
            Console.WriteLine($"Hashed Password: {hashedPassword}");

            // During login
            bool isCorrect = PasswordManager.Verify("Entered Password:", hashedPassword);
            Debug.WriteLine($"Password correct? {isCorrect}");
        }

    }
    #endregion

    /// <summary>
    /// Provides AES encryption and decryption methods.
    /// </summary>
    public static class AesEncryption
    {
        // Encrypt the plaintext and return the encrypted bytes
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

        // Generate a random salt
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
