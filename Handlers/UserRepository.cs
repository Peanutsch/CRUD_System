using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using CRUD_System.FileHandlers;
using System.IO;
using System.Xml.Linq;

namespace CRUD_System.Handlers
{
    /// <summary>
    /// Provides data access methods for user information, including searching by alias.
    /// </summary>
    public class UserRepository
    {
        FilePaths path = new FilePaths();

        MessageBoxes message = new MessageBoxes();

        public int FindUserIndexByAlias(List<string> userLines, List<string> loginLines, string alias)
        {
            for (int index = 0; index < userLines.Count; index++)
            {
                var userDetails = userLines[index].Split(',');
                var loginDetails = loginLines[index].Split(",");
                if (userDetails[2] == alias && loginDetails[0] == alias)
                {
                    return index;
                }
            }
            // Alias not found
            message.MessageUserNotFound(alias);
            return -1;
        }

        /// <summary>
        /// Generates a unique alias for the user based on the first two letters of the first name
        /// and the last two letters of the surname, followed by a number that increments if the alias already exists.
        /// </summary>
        /// <returns>A unique alias as a string.</returns>
        public string CreateTXTAlias(string Name, string Surname)
        {
            if (Name.Length < 2)
            {
                Name += Name; // Double name
            }
            if (Surname.Length < 2)
            {
                Surname += Surname; // Double surmame
            }

            string initialAlias = Name.Substring(0, 2).ToLower() + Surname.Substring(Surname.Length - 2).ToLower();
            int counter = 1;
            string finalAlias = initialAlias + "001";

            // Check if the alias already exists and increment the number if necessary
            while (AliasExists(finalAlias))
            {
                counter++;
                string newNumber = counter.ToString("D3"); // Ensures it always has 3 digits
                finalAlias = initialAlias + newNumber;
            }

            Name = finalAlias;

            return finalAlias;
        }

        /// <summary>
        /// Checks if the given alias already exists in the data_login.csv file.
        /// </summary>
        /// <param name="alias">The alias to check for existence.</param>
        /// <returns>True if the alias exists; otherwise, false.</returns>
        private bool AliasExists(string alias)
        {
            // Read all lines from data_login.csv
            var loginLines = File.ReadAllLines(path.LoginFilePath);

            // Check if the alias already exists
            foreach (var line in loginLines)
            {
                var loginDetails = line.Split(',');
                if (loginDetails[0] == alias)
                {
                    Debug.WriteLine($"Alias {alias} already exist");
                    return true; // Alias already exists
                }
            }
            return false; // Alias does not exist
        }
    }
}
