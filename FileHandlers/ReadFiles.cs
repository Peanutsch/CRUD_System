using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System.FileHandlers
{
    internal class ReadFiles
    {
        /// <summary>
        /// Controleert of een string Base64-gecodeerd is.
        /// </summary>
        /// <param name="input">De string die gecontroleerd moet worden.</param>
        /// <returns>True als de string Base64-gecodeerd is, anders False.</returns>
        public static bool IsBase64(string input)
        {
            // Probeer de string te decoderen
            try
            {
                Convert.FromBase64String(input);
                return true; // Het is een geldige Base64-string
            }
            catch
            {
                return false; // Fout betekent dat het geen geldige Base64 is
            }
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
        /// - statusOnline (bool): A flag indicating whether the user is online (true) or not (false)
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
                Debug.WriteLine($"Error! No such file with path {file}\nRootPath = {RootPath.GetRootPath()}");
                return new List<(string Username, string Password, bool IsAdmin, bool onlineStatus)>();
            }

            /*
            // Read the first line of the file to check if it seems Base64 encoded
            string? firstLine = File.ReadLines(file).FirstOrDefault();

            // Check if the first line is Base64 encoded
            if (firstLine != null && !IsBase64(firstLine))
            {
                // Decrypt the file before reading its contents
                EncryptionManager.DecryptFile(file);
            }
            */

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

                    // Ensure the line has exactly 4 elements before proceeding
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
            // Encrypt data_login.csv
            //EncryptionManager.EncryptFile(file);
            // Return the populated list of login data
            return loginData;
        }

        /// <summary>
        /// Retrieves user data from a CSV file and returns it as a list of tuples
        /// containing the name, surname, address, zip code, city, and email address.
        /// </summary>
        /// <returns>
        /// A list of tuples where each tuple consists of:
        /// - Name (string): The user's name
        /// - Surname (string): The user's surname
        /// - Address (string): The user's address
        /// - ZipCode (string): The user's zip code
        /// - City (string): The user's city
        /// - EmailAddress (string): The user's email address
        /// </returns>
        /// <remarks>
        /// The CSV file is expected to have each row in the following format:
        /// Name, Surname, Address, ZipCode, City, EmailAddress
        /// </remarks>
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

            // Decrypt the file before reading its contents
            EncryptionManager.DecryptFile(file);

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
    }
}
