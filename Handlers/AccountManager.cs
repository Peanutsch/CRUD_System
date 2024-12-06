using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using CRUD_System.FileHandlers;
using System.IO;
using System.Xml.Linq;
using CRUD_System.Repositories;
using System.Globalization;
using System.Text;
namespace CRUD_System.Handlers
{
    /// <summary>
    /// Class to provide data access methods for user information, including searching by alias.
    /// </summary>
    public class AccountManager
    {
        #region PROPERTIES
        RepositoryMessageBoxes message = new RepositoryMessageBoxes();
        #endregion PROPERTIES

        #region PROCES
        /// <summary>
        /// Finds the index of a user by their alias in both user and login data.
        /// </summary>
        /// <param name="userLines">A list of strings representing user details, where each entry is a CSV-formatted line.</param>
        /// <param name="loginLines">A list of strings representing login details, where each entry is a CSV-formatted line.</param>
        /// <param name="alias">The alias of the user to search for.</param>
        /// <returns>
        /// The index of the user if found; otherwise, -1. 
        /// If the alias is not found, a message is displayed to indicate this.
        /// </returns>
        public int FindUserIndexByAlias(List<string> userLines, List<string> loginLines, string alias)
        {
            // Load cache
            DataCache.LoadCache();

            // Check if cache is loaded correctly
            if (DataCache.CachedLoginLines == null || DataCache.CachedLoginLines.Count == 0)
            {
                DataCache dataCache = new DataCache();
                dataCache.LoadDecryptedData();
            }

            // Search through the cached login data for the alias
            for (int index = 0; index < DataCache.CachedLoginLines?.Count; index++)
            {
                var loginDetails = DataCache.CachedLoginLines[index].Split(",");

                // Assuming the first element of loginDetails is the encrypted alias
                string encryptedAlias = loginDetails[0].Trim();

                // Decrypt the alias using your decryption method
                string decryptedAlias = AesEncryption.DecryptWithFixedKey(encryptedAlias, AesEncryption.EncryptionKey);

                // Compare the decrypted alias with the provided alias
                if (decryptedAlias == alias.Trim())
                {
                    return index; // Return the index if found
                }
            }

            // If not found, show a message
            message.MessageUserNotFound(alias);
            return -1; // Return -1 if the alias was not found
        }

        /// <summary>
        /// Generates a unique alias for the user based on the first two letters of the first name
        /// and the last two letters of the surname, followed by a number that increments if the alias already exists.
        /// Diacritics and unwanted characters are removed for the alias.
        /// </summary>
        /// <returns>A unique alias as a string.</returns>
        public string CreateTXTAlias(string Name, string Surname)
        {
            // Normalize and clean the Name and Surname
            Name = CleanText(Name);
            Surname = CleanText(Surname);

            // Extract only letters
            Name = new string(Name.Where(char.IsLetter).ToArray());
            Surname = new string(Surname.Where(char.IsLetter).ToArray());

            // Ensure Name and Surname have enough letters
            if (Name.Length < 2) Name = Name.PadRight(2, 'x'); // Fallback to 'x' if insufficient letters
            if (Surname.Length < 2) Surname = Surname.PadRight(2, 'y'); // Fallback to 'y' if insufficient letters

            // Generate the base alias
            string initialAlias = Name.Substring(0, 2).ToLower() + Surname.Substring(Surname.Length - 2).ToLower();
            int counter = 1;

            // Loop to generate a unique alias
            string finalAlias;
            while (true)
            {
                string newNumber = counter.ToString("D3"); // Ensures it always has 3 digits
                finalAlias = initialAlias + newNumber;

                // Check if alias already exists
                if (!AliasExists(finalAlias))
                {
                    Debug.WriteLine($"Unique alias generated: {finalAlias}");
                    break; // Exit the loop if the alias is unique
                }

                counter++; // Increment counter if alias exists
            }

            return finalAlias;
        }


        /// <summary>
        /// Removes diacritics, punctuation, and unwanted characters from the input string.
        /// </summary>
        /// <param name="text">The input string with potential diacritics, punctuation, and unwanted characters.</param>
        /// <returns>A string without diacritics, punctuation, and unwanted characters.</returns>
        private string CleanText(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var chars in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(chars);

                // Retain only letters, numbers, and spaces (or any other desired categories)
                if (unicodeCategory == UnicodeCategory.LowercaseLetter ||
                    unicodeCategory == UnicodeCategory.UppercaseLetter ||
                    unicodeCategory == UnicodeCategory.DecimalDigitNumber ||
                    unicodeCategory == UnicodeCategory.SpaceSeparator)
                {
                    stringBuilder.Append(chars);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }


        /// <summary>
        /// Checks if the given alias already exists in the data_login.csv file.
        /// </summary>
        /// <param name="alias">The alias to check for existence.</param>
        /// <returns>True if the alias exists; otherwise, false.</returns>
        private bool AliasExists(string alias)
        {
            DataCache cache = new DataCache();
            // Check if the cached user data is empty or not loaded
            if (cache.CachedLoginData == null || cache.CachedLoginData.Count == 0)
            {
                cache.LoadDecryptedData();
            }

            foreach (var line in cache.CachedLoginData!)
            {
                //var loginDetails = line.Split(','); // Ensure splitting is done if data contains multiple fields
                if (line[0].Trim() == alias)
                {
                    Debug.WriteLine($"Alias {alias} already exists.");
                    return true;
                }
            }
            return false;
        }
    }
    #endregion PROCES
}
