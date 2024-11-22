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
        public List<string> SearchByAlias(string searchTerm)
        {
            // Read user and login data from CSV files
            (var userLines, var loginLines) = path.ReadUserAndLoginData();

            // Search for matches in different fields
            var matchedUsers = userLines
<<<<<<< Updated upstream
                .Select(line => line.Split(','))
                .Where(details => details.Length > 2 && details[2].StartsWith(alias, StringComparison.OrdinalIgnoreCase))  // Match prefixes
                .Select(details =>
                {
                    string name = details[0];
                    string surname = details[1];
                    string email = details.Length > 6 ? details[6] : string.Empty;
                    string phonenumber = details.Length > 7 ? details[7] : string.Empty;
                    string isOnline = details.Length > 8 && details[8] == "True" ? "| [ONLINE]" : string.Empty;
=======
                .Select(line => line.Split(',')) // Split each line into individual fields
                .Where(userDetails =>
                {
                    // Check if the search term matches the alias, name, surname, or email
                    return userDetails.Length > 2 &&
                           (userDetails[0].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || // Name
                            userDetails[1].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || // Surname
                            userDetails[2].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || // Alias
                            (userDetails.Length > 6 && userDetails[6].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))); // Email
                })
                .Select(userDetails =>
                {
                    string name = userDetails[0];
                    string surname = userDetails[1];
                    string alias = userDetails[2];
                    string email = userDetails.Length > 6 ? userDetails[6] : string.Empty;
                    string phonenumber = userDetails.Length > 7 ? userDetails[7] : string.Empty;
                    string isOnline = userDetails.Length > 8 && userDetails[8] == "True" ? "| [ONLINE]" : string.Empty;
>>>>>>> Stashed changes

                    // Format the matched user details as a string
                    return $"{name} {surname} ({alias}) | {email} | {phonenumber} {isOnline}";
                })
                .ToList();

            // Return the list of matched users
            return matchedUsers;
        }
    }
}
