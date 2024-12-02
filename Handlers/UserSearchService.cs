using CRUD_System.FileHandlers;
using CRUD_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System.Handlers
{
    internal class UserSearchService
    {
        /// <summary>
        /// Searches for users by matches in the user data and returns a list of formatted user details.
        /// It filters the users based on the provided alias and returns their name, surname, email, 
        /// and online status (if available).
        /// </summary>
        /// <param name="searchTerm">The term to search for in the user details.</param>
        /// <returns>A list of formatted strings representing the matched users' details.</returns>
        public List<string> SearchUsers(string searchTerm)
        {
            DataCache cache = new DataCache();
            // Check if the cache is empty, and reload data if necessary.
            if (cache.CachedUserData.Count == 0 || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            // Search for matches in different fields
            var matchedUsers = cache.CachedUserData
                .Skip(2) // Skip header and Admin
                .Where(userDetails =>
                {
                    // Check if the search term matches the alias, name, surname, or email
                    return userDetails.Length > 2 &&
                           (userDetails[0].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || // Name
                            userDetails[1].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || // Surname
                            userDetails[2].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || // Alias
                            userDetails[5].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || // City
                            userDetails.Length > 6 && userDetails[6].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || // Email
                            userDetails[7].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) || // Phonenumber
                            userDetails.Length > 8 && userDetails[8].StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase)); // Online status
                                                                                                                                  // searchterm = bool true or false
                })
                .Select(userDetails =>
                {
                    string name = userDetails[0];
                    string surname = userDetails[1];
                    string alias = userDetails[2];
                    string email = userDetails.Length > 6 ? userDetails[6] : string.Empty;
                    string phonenumber = userDetails.Length > 7 ? userDetails[7] : string.Empty;
                    string isOnline = userDetails.Length > 8 && userDetails[8] == "True" ? "| [ONLINE]" : string.Empty;

                    // Format the matched user details as a string
                    return $"{name} {surname} ({alias}) | {email} | {phonenumber} {isOnline}";
                })
                .ToList();

            // Return the list of matched users
            return matchedUsers;
        }
    }
}

