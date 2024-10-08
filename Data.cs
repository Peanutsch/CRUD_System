﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CRUD_System
{
    internal class Data
    {
        /// <summary>
        /// Retrieves login data from a CSV file and returns it as a list of tuples
        /// containing the username, password, and admin status.
        /// </summary>
        /// <returns>
        /// A list of tuples where each tuple consists of:
        /// - Username (string): The user's username
        /// - Password (string): The user's password
        /// - IsAdmin (bool): A flag indicating whether the user is an admin (true) or not (false)
        /// </returns>
        /// <remarks>
        /// The CSV file is expected to have each row in the following format:
        /// Username, Password, IsAdmin
        /// </remarks>
        public List<(string Username, string Password, bool IsAdmin)> GetLoginData()
        {
            // Construct the full path to the CSV file
            string file = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
            Debug.WriteLine($"RootPath data_login.csv: {RootPath.GetRootPath}");

            // Check if the file exists; if not, log an error and return an empty list
            if (!File.Exists(file))
            {
                Debug.WriteLine($"Error! No such file with path {file}\nRootPath = {RootPath.GetRootPath}");
                return new List<(string Username, string Password, bool IsAdmin)>();
            }

            // List to store the login data from the CSV
            List<(string Username, string Password, bool IsAdmin)> loginData = new List<(string Username, string Password, bool IsAdmin)>();

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
                    if (values.Length == 3)
                    {
                        string username = values[0].Trim(); // Alias
                        string password = values[1].Trim(); // Password
                        bool isAdmin = bool.Parse(values[2].Trim()); // True or False IsAdmin

                        // Add the extracted data to the loginData list
                        loginData.Add((username, password, isAdmin));
                    }
                }
            }

            // Return the populated list of login data
            return loginData;
        }

        public List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)> GetUserData()
        {
            // Construct the full path to the CSV file
            string file = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");
            Debug.WriteLine($"RootPath data_users.csv: {RootPath.GetRootPath}");

            // Check if the file exists; if not, log an error and return an empty list
            if (!File.Exists(file))
            {
                Debug.WriteLine($"Error! No such file with path {file}");
                return new List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)>();
            }

            List<(string Name, string Surname, string Address, string ZipCode, string City, string Emailadress)> users = new List<(string, string, string, string, string, string)>();

            using (var reader = new StreamReader(file))
            {
                string headerLine = reader.ReadLine()!; // Read and ignore the header line

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line!.Split(',');

                    // Create a tuple with the user data
                    users.Add((values[0].Trim(), 
                               values[1].Trim(), 
                               values[2].Trim(), 
                               values[3].Trim(), 
                               values[4].Trim(), 
                               values[5].Trim()));
                }
            }

            return users;
        }
    }
}
