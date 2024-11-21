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

            // Proceed with matching based on the alias prefix
            var matchedUsers = userLines
                .Select(line => line.Split(','))
                .Where(userDetails => userDetails.Length > 2 && userDetails[2].StartsWith(alias, StringComparison.OrdinalIgnoreCase))  // Match prefixes
                .Select(userDetails =>
                {
                    string name = userDetails[0];
                    string surname = userDetails[1];
                    string email = userDetails.Length > 6 ? userDetails[6] : string.Empty;
                    string phonenumber = userDetails.Length > 7 ? userDetails[7] : string.Empty;
                    string isOnline = userDetails.Length > 8 && userDetails[8] == "True" ? "| [ONLINE]" : string.Empty;

                    return $"{name} {surname} ({alias}) | {email} | {phonenumber} {isOnline}";
                })
                .ToList();

            return matchedUsers;
        }
    }
}