using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CRUD_System.FileHandlers
{
    internal class ReadFiles
    {
        public static string DecryptData(string encryptedData, string encryptionPassword)
        {
            return AesEncryption.Decrypt(Convert.FromBase64String(encryptedData), encryptionPassword);
        }


        /// <summary>
        /// Retrieves login data from a CSV file and returns it as a list of tuples
        /// containing the username, password, and admin status.
        /// </summary>
        /// <returns>
        /// A list of tuples where each tuple consists of:
        /// - Username (string): The user's username
        /// - Password (string): The user's password
        /// - IsAdmin (bool): A flag indicating whether the user is an admin (true) or not (false)
        /// - statusOnline (bool): A flag indicating wheter the user is online (true) or not (false)
        /// </returns>
        /// <remarks>
        /// The CSV file is expected to have each row in the following format:
        /// Username, Password, IsAdmin, onlineStatus
        /// </remarks>
        public List<(string Username, string Password, bool IsAdmin, bool onlineStatus)> GetLoginData()
        {
            // Construct the full path to the CSV file
            string file = Path.Combine(RootPath.GetRootPath(), @"CSV\data_login.csv");

            // Check if the file exists; if not, log an error and return an empty list
            if (!File.Exists(file))
            {
                Debug.WriteLine($"Error! No such file with path {file}\nRootPath = {RootPath.GetRootPath}");
                return new List<(string Username, string Password, bool IsAdmin, bool onlineStatus)>();
            }

            // List to store the login data from the CSV
            List<(string Username, string Password, bool IsAdmin, bool onlineStatus)> loginData = new List<(string Username, string Password, bool IsAdmin, bool onlineStatus)>();

            // Read the CSV file line by line
            using (StreamReader reader = new StreamReader(file))
            {
                // Skip the header line
                string headerLine = reader.ReadLine()!;

                string line;
                while ((line = reader.ReadLine()!) != null)
                {
                    // Split the line by commas to extract the username, password, and admin status
                    string[] values = line.Split(',');

                    // Ensure the line has exactly 3 elements before proceeding
                    if (values.Length == 4)
                    {
                        string username = values[0].Trim(); // Alias
                        string password = values[1].Trim(); // Password
                        bool isAdmin = bool.Parse(values[2].Trim()); // True or False IsAdmin
                        bool onlineStatus = bool.Parse(values[3].Trim());

                        // Add the extracted data to the loginData list
                        loginData.Add((username, password, isAdmin, onlineStatus));
                    }
                }
            }
            // Return the populated list of login data
            return loginData;
        }

        public List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)> GetUserData()
        {
            // Construct the full path to the CSV file
            string file = Path.Combine(RootPath.GetRootPath(), @"CSV\data_users.csv");
            Debug.WriteLine($"RootPath data_users.csv: {RootPath.GetRootPath()}");

            // Check if the file exists; if not, log an error and return an empty list
            if (!File.Exists(file))
            {
                Debug.WriteLine($"Error! No such file with path {file}");
                return new List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)>();
            }

            // Use EncryptionManager to decrypt the CSV file
            string decryptedData = EncryptionManager.DecryptCsv(file, AesEncryption.EncryptionKey);

            // Split the decrypted data into lines and parse each line into a tuple
            List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)> users =
                new List<(string, string, string, string, string, string)>();

            // Split the decrypted data into lines
            var lines = decryptedData.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Skip the first two lines (header and first data row)
            for (int i = 2; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');

                // Assign "None" to any missing or empty value
                string name = string.IsNullOrWhiteSpace(values[0]) ? string.Empty : values[0].Trim();
                string surname = string.IsNullOrWhiteSpace(values[1]) ? string.Empty : values[1].Trim();
                string address = string.IsNullOrWhiteSpace(values[2]) ? string.Empty : values[2].Trim();
                string zipCode = string.IsNullOrWhiteSpace(values[3]) ? string.Empty : values[3].Trim();
                string city = string.IsNullOrWhiteSpace(values[4]) ? string.Empty : values[4].Trim();
                string emailAddress = string.IsNullOrWhiteSpace(values[5]) ? string.Empty : values[5].Trim();

                // Add the user data as a tuple to the list
                users.Add((name, surname, address, zipCode, city, emailAddress));
            }

            return users;
        }



        /* ---> ORIGINAL GETUSERDATA()
        /// <summary>
        /// Retrieves user data from the `data_users.csv` file, decrypts the encrypted information, 
        /// and returns a list of user details. Each user's data is represented as a tuple containing 
        /// their name, surname, address, zip code, city, and email address.
        /// </summary>
        /// <returns>A list of tuples, where each tuple contains user details (Name, Surname, Address, ZipCode, City, Email).</returns>
        public List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)> GetUserData()
        {
            // Construct the full path to the CSV file
            string file = Path.Combine(RootPath.GetRootPath(), @"CSV\data_users.csv");
            Debug.WriteLine($"RootPath data_users.csv: {RootPath.GetRootPath()}");

            // Check if the file exists; if not, log an error and return an empty list
            if (!File.Exists(file))
            {
                Debug.WriteLine($"Error! No such file with path {file}");
                return new List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)>();
            }

            List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)> users =
                new List<(string, string, string, string, string, string)>();

            using (var reader = new StreamReader(file))
            {
                string headerLine = reader.ReadLine()!; // Read and ignore the header line

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line!.Split(',');

                    // Assign "None" to any missing or empty value
                    string name = string.IsNullOrWhiteSpace(values[0]) ? "None" : values[0].Trim();
                    string surname = string.IsNullOrWhiteSpace(values[1]) ? "None" : values[1].Trim();
                    string address = string.IsNullOrWhiteSpace(values[2]) ? "None" : values[2].Trim();
                    string zipCode = string.IsNullOrWhiteSpace(values[3]) ? "None" : values[3].Trim();
                    string city = string.IsNullOrWhiteSpace(values[4]) ? "None" : values[4].Trim();
                    string emailAddress = string.IsNullOrWhiteSpace(values[5]) ? "None" : values[5].Trim();

                    // Add the user data as a tuple to the list
                    users.Add((name, surname, address, zipCode, city, emailAddress));
                }
            }
            return users;
        }
        */

        /*
        public List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)> GetUserData()
        {
            string file = Path.Combine(RootPath.GetRootPath(), @"CSV\data_users.csv");
            Debug.WriteLine($"RootPath data_users.csv: {RootPath.GetRootPath()}");

            if (!File.Exists(file))
            {
                Debug.WriteLine($"Error! No such file with path {file}");
                return new List<(string, string, string, string, string, string)>();
            }

            List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)> users =
                new List<(string, string, string, string, string, string)>();

            using (var reader = new StreamReader(file))
            {
                string? headerLine = reader.ReadLine(); // Skip header line

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(',');

                        // Decode the Base64 encoded encrypted data
                        byte[] encryptedName = Convert.FromBase64String(values[0].Trim());
                        byte[] encryptedSurname = Convert.FromBase64String(values[1].Trim());
                        byte[] encryptedAddress = Convert.FromBase64String(values[2].Trim());
                        byte[] encryptedZipCode = Convert.FromBase64String(values[3].Trim());
                        byte[] encryptedCity = Convert.FromBase64String(values[4].Trim());
                        byte[] encryptedEmail = Convert.FromBase64String(values[5].Trim());

                        // Decrypt the data
                        string name = AesEncryption.Decrypt(encryptedName, AesEncryption.EncryptionKey);
                        string surname = AesEncryption.Decrypt(encryptedSurname, AesEncryption.EncryptionKey);
                        string address = AesEncryption.Decrypt(encryptedAddress, AesEncryption.EncryptionKey);
                        string zipCode = AesEncryption.Decrypt(encryptedZipCode, AesEncryption.EncryptionKey);
                        string city = AesEncryption.Decrypt(encryptedCity, AesEncryption.EncryptionKey);
                        string emailAddress = AesEncryption.Decrypt(encryptedEmail, AesEncryption.EncryptionKey);

                        // Add the decrypted user data to the list
                        users.Add((name, surname, address, zipCode, city, emailAddress));
                    }
                }
            }
            return users;
        }
        */
    }
}
