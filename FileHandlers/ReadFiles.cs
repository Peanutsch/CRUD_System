using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CRUD_System.FileHandlers
{
    /// <summary>
    /// Handles file reading operations for login and user data, including decrypting encrypted CSV files.
    /// Provides methods to retrieve login details and user details.
    /// </summary>
    internal class ReadFiles
    {
        // Cached list of login data loaded from a CSV file.
        private static List<(string Username, string Password, bool IsAdmin, bool OnlineStatus)> loginData = new List<(string Username, string Password, bool IsAdmin, bool OnlineStatus)>();
        private static List<(string Name, string Surname, string Address, string ZipCode, string City, string EmailAddress)>? userDataCache = null;

        private static FilePaths path = new FilePaths();

        /// <summary>
        /// Gets the list of login data. If the data is not yet loaded, it decrypts the file and loads it.
        /// </summary>
        public static List<(string Username, string Password, bool IsAdmin, bool OnlineStatus)> LoginData
        {
            get
            {
                if (loginData.Count == 0)
                {
                    EncryptionManager.DecryptFile(path.LoginFilePath);
                    loginData = LoadLoginData();
                }
                return loginData;
            }
        }

        /// <summary>
        /// Gets the cached user data. If the data is not yet loaded, it reads and processes the file.
        /// </summary>
        public static List<(string Name, string Surname, string Address, string ZipCode, string City, string EmailAddress)> UserData
        {
            get
            {
                if (userDataCache == null || userDataCache.Count == 0)
                {
                    EncryptionManager.DecryptFile(path.UserFilePath);
                    userDataCache = LoadUserData();
                }
                return userDataCache;
            }
        }

        /// <summary>
        /// Reads login data from a CSV file and returns it as a list of tuples.
        /// </summary>
        /// <returns>
        /// A list of tuples where each tuple contains:
        /// - Username: The user's username.
        /// - Password: The user's password (decoded if Base64-encoded).
        /// - IsAdmin: Indicates whether the user is an admin (true) or not (false).
        /// - OnlineStatus: Indicates whether the user is currently online (true) or not (false).
        /// </returns>
        private static List<(string Username, string Password, bool IsAdmin, bool OnlineStatus)> LoadLoginData()
        {
            // Read all lines from the CSV file
            var loginLines = File.ReadAllLines(path.LoginFilePath);
            var userList = new List<(string Username, string Password, bool IsAdmin, bool OnlineStatus)>();

            foreach (var line in loginLines.Skip(1)) // Skip the header line
            {
                var parts = line.Split(',');

                // Check if the password is Base64-encoded
                string password = parts[1];
                if (IsBase64(password))
                {
                    try
                    {
                        // Decode the Base64 password
                        byte[] passwordBytes = Convert.FromBase64String(password);
                        password = System.Text.Encoding.UTF8.GetString(passwordBytes);
                    }
                    catch
                    {
                        // Log an error or handle it appropriately
                        Console.WriteLine($"Failed to decode Base64 password for user {parts[0]}.");
                    }
                }

                // Parse admin and online status values
                bool isAdmin = Convert.ToBoolean(parts[2]);
                bool onlineStatus = Convert.ToBoolean(parts[3]);

                // Add the user to the list
                userList.Add((parts[0], password, isAdmin, onlineStatus));
            }

            return userList;
        }

        /// <summary>
        /// Determines whether a given string is a valid Base64-encoded string.
        /// </summary>
        /// <param name="input">The string to validate.</param>
        /// <returns>True if the string is Base64-encoded; otherwise, False.</returns>
        public static bool IsBase64(string input)
        {
            // Check if the input is null, empty, or whitespace, 
            // or if its length is not a multiple of 4 (a requirement for Base64 encoding).
            if (string.IsNullOrWhiteSpace(input) || input.Length % 4 != 0)
                return false;

            // Use a regular expression to verify that the string matches the Base64 character set
            // (alphanumeric characters, '+', '/', and optional '=' padding at the end).
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z0-9\+/]*={0,2}$");
        }

        /// <summary>
        /// Reads user data from a CSV file, decodes Base64-encoded fields (if any), and loads it into memory.
        /// </summary>
        /// <returns>
        /// A list of tuples where each tuple contains:
        /// - Name: The user's first name.
        /// - Surname: The user's surname.
        /// - Address: The user's address.
        /// - ZipCode: The user's postal code.
        /// - City: The user's city of residence.
        /// - EmailAddress: The user's email address.
        /// </returns>
        private static List<(string Name, string Surname, string Address, string ZipCode, string City, string EmailAddress)> LoadUserData()
        {
            var userLines = File.ReadAllLines(path.UserFilePath);
            var userList = new List<(string Name, string Surname, string Address, string ZipCode, string City, string EmailAddress)>();

            foreach (var line in userLines.Skip(1)) // Skip the header line
            {
                var parts = line.Split(',');

                // If the input is valid Base64, it decodes the string using UTF-8 encoding. Otherwise, it returns the input as-is without decoding.
                string DecodeIfBase64(string input) =>
                    IsBase64(input) // Check if the input string is Base64-encoded
                        ? System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input)) // If true, decode the Base64 string and convert it to UTF-8 text
                        : input;  // If not Base64, return the original input string unchanged

                // Process each field, checking for Base64 encoding
                string name = string.IsNullOrWhiteSpace(parts[0]) ? "None" : DecodeIfBase64(parts[0].Trim());
                string surname = string.IsNullOrWhiteSpace(parts[1]) ? "None" : DecodeIfBase64(parts[1].Trim());
                string address = string.IsNullOrWhiteSpace(parts[2]) ? "None" : DecodeIfBase64(parts[2].Trim());
                string zipCode = string.IsNullOrWhiteSpace(parts[3]) ? "None" : DecodeIfBase64(parts[3].Trim());
                string city = string.IsNullOrWhiteSpace(parts[4]) ? "None" : DecodeIfBase64(parts[4].Trim());
                string emailAddress = string.IsNullOrWhiteSpace(parts[5]) ? "None" : DecodeIfBase64(parts[5].Trim());

                // Add the decoded fields to the user list
                userList.Add((name, surname, address, zipCode, city, emailAddress));
            }

            return userList;
        }

    }
}
