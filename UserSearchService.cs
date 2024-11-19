using CRUD_System.FileHandlers;
using CRUD_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    internal class UserSearchService
    {
        private FilePaths path = new FilePaths();

        public UserSearchService()
        {

        }

        /// <summary>
        /// Searches for users by alias in the user data and returns a list of formatted user details.
        /// It filters the users based on the provided alias and returns their name, surname, email, 
        /// phone number, and online status (if available).
        /// </summary>
        /// <param name="alias">The alias of the user to search for.</param>
        /// <returns>A list of formatted strings representing the matched users' details.</returns>
        public List<string> SearchByAlias(string alias)
        {
            // Read user and login data from CSV files using the 'ReadUserAndLoginData' method
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            // Return all users if alias is empty
            if (string.IsNullOrEmpty(alias))
            {
                AdminInterface adminInterface = new AdminInterface();
                adminInterface.LoadDetailsListBox();
                // Assuming you want to return all users when the input is empty
                //return userLines.Select(line => line.Split(',')[0]).ToList();  // Adjust as necessary to return the desired format
            }

            // Proceed with matching based on the alias prefix
            var matchedUsers = userLines
                .Select(line => line.Split(','))
                .Where(details => details.Length > 2 && details[2].StartsWith(alias, StringComparison.OrdinalIgnoreCase))  // Match prefixes
                .Select(details =>
                {
                    string name = details[0];
                    string surname = details[1];
                    string email = details.Length > 6 ? details[6] : string.Empty;
                    string phonenumber = details.Length > 7 ? details[7] : string.Empty;
                    string isOnline = details.Length > 8 && details[8] == "True" ? "| [ONLINE]" : string.Empty;

                    return $"{name} {surname} ({alias}) | {email} | {phonenumber} {isOnline}";
                })
                .ToList();

            return matchedUsers;
        }
    }
}
