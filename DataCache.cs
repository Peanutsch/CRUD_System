using CRUD_System.FileHandlers;
using CRUD_System;
using System.Diagnostics;

/// <summary>
/// The DataCache class is responsible for managing in-memory caching of user and login data.
/// It provides functionality to decrypt files when loading data into memory and encrypt files when saving data back.
/// </summary>
public class DataCache
{
    // Instance of FilePaths to get paths to the required files.
    private static FilePaths path = new FilePaths();

    // File paths for user and login data.
    private readonly string userFilePath = path.UserFilePath;
    private readonly string loginFilePath = path.LoginFilePath;

    /// <summary>
    /// Cached user data, where each array represents a record (line) split into fields.
    /// </summary>
    public List<string[]> CachedUserData { get; private set; } = new List<string[]>();

    /// <summary>
    /// Cached login data, where each array represents a record (line) split into fields.
    /// </summary>
    public List<string[]> CachedLoginData { get; private set; } = new List<string[]>();

    /// <summary>
    /// Constructor for the DataCache class.
    /// Automatically decrypts and loads data into memory upon initialization.
    /// </summary>
    public DataCache()
    {
        LoadDecryptedData();
    }

    /// <summary>
    /// Decrypts the user and login files and loads their data into the cache.
    /// Each record is split into fields and stored as a list of string arrays.
    /// </summary>
    public void LoadDecryptedData()
    {
        // Decrypt user and login data files to prepare them for reading.
        EncryptionManager.DecryptFile(userFilePath);
        //MessageBox.Show("DataCache.LoadDecryptedData>\n data_users.csv DECRYPTED");
        EncryptionManager.DecryptFile(loginFilePath);
        //MessageBox.Show("DataCache.LoadDecryptedData>\ndata_login.csv DECRYPTED");

        // Read the decrypted user data file and split each line into fields (CSV format).
        // Skip the header and split by comma, making sure all data is retained.
        CachedUserData = File.ReadAllLines(userFilePath)
                       .Skip(1) // Skip Header, ensure the header is ignored.
                       .Select(line => line.Split(",")) // Split each line into array.
                       .ToList(); // Cache all records in CachedUserData.
        Debug.WriteLine($"Details data_users.csv cached in CachedUserData: {CachedUserData.Count} items");
        MessageBox.Show($"Details data_users.csv cached in CachedUserData: {CachedUserData.Count} items");

        // Read the decrypted login data file and split each line into fields (CSV format).
        // Skip the header and split by comma, making sure all login data is retained.
        CachedLoginData = File.ReadAllLines(loginFilePath)
                        .Skip(1) // Skip Header, ensure the header is ignored.
                        .Select(line => line.Split(",")) // Split each line into array.
                        .ToList(); // Cache all records in CachedLoginData.
        Debug.WriteLine($"Details data_login.csv cached in CachedLoginData: {CachedLoginData.Count} items");
        MessageBox.Show($"Details data_login.csv cached in CachedLoginData: {CachedLoginData.Count} items");

        // Encrypt the files again to ensure they are secured after being loaded into memory
        EncryptionManager.EncryptFile(userFilePath);
        EncryptionManager.EncryptFile(loginFilePath);
    }


    /// <summary>
    /// Saves the cached data back to the user and login files and encrypts the files.
    /// Ensures that any changes made to the in-memory cache are persisted securely.
    /// </summary>
    public void SaveAndEncryptData()
    {
        EncryptionManager.DecryptFile(userFilePath);
        MessageBox.Show("DataCache.SaveAndEncryptData>\n data_users.csv DECRYPTED");
        Debug.WriteLine("DataCache.SaveAndEncryptData>\n data_users.csv DECRYPTED");

        EncryptionManager.DecryptFile(loginFilePath);
        MessageBox.Show("DataCache.SaveAndEncryptData>\ndata_login.csv DECRYPTED");
        Debug.WriteLine("DataCache.SaveAndEncryptData>\n data_login.csv DECRYPTED");

        // Save the cached user data to the file, joining fields into CSV lines.
        // Skip Header, ensure the header is ignored.
        File.WriteAllLines(userFilePath, new[] { "[0]NAME, [1] SURNAME, [2] ALIAS, [3] ADRESS, [4] ZIPCODE, [5] CITY, [6] EMAIL ADRESS, [7] PHONENUMBER, [8] ONLINE STATUS" }
                                               .Concat(CachedUserData
                                               .Select(fields => string.Join(",", fields)
                                                )));


        // Save the cached login data to the login file.
        // Add the header row explicitly before saving the data.
        File.WriteAllLines(loginFilePath, new[] { "[0] Alias, [1] PASSWORD, [2] ADMIN, [3] ONLINESTATUS" }
                                                .Concat(CachedLoginData
                                                .Select(fields => string.Join(",", fields)
                                                 )));

        // Encrypt the user and login data files to secure the contents.
        EncryptionManager.EncryptFile(userFilePath);
        MessageBox.Show("DataCache.SaveAndEncryptData>\n data_users.csv ENCRYPTED");
        Debug.WriteLine("DataCache.SaveAndEncryptData>\n data_users.csv ENCRYPTED");
        EncryptionManager.EncryptFile(loginFilePath);
        Debug.WriteLine("DataCache.SaveAndEncryptData>\n data_login.csv ENCRYPTED");
        MessageBox.Show("DataCache.SaveAndEncryptData>\ndata_login.csv ENCRYPTED");
    }
}

/*
 /// <summary>
        /// Updates the online status of a user by alias in both login and user data files.
        /// If the user is not found, displays an error message.
        /// </summary>
        /// <param name="alias">The alias of the user whose online status needs to be updated.</param>
        /// <param name="onlineStatus">The new online status to set for the user (true for online, false for offline).</param>
        public void UpdateUserOnlineStatus(string alias, bool onlineStatus)
        {
            var user = cache.CachedUserData.FirstOrDefault(u => u[2] == alias); // Alias veld
            if (user != null)
            {
                user[8] = onlineStatus.ToString(); // Update online status
            }

            var login = cache.CachedLoginData.FirstOrDefault(l => l[0] == alias); // Alias veld
            if (login != null)
            {
                login[3] = onlineStatus.ToString(); // Update online status
            }
         }
/// <summary>
/// Saves the cached data back to the user and login files and encrypts the files.
/// Ensures that any changes made to the in-memory cache are persisted securely.
/// </summary>
public void SaveAndEncryptData()
{
    EncryptionManager.DecryptFile(userFilePath);
    MessageBox.Show("DataCache.SaveAndEncryptData>\n data_users.csv DECRYPTED");
    Debug.WriteLine("DataCache.SaveAndEncryptData>\n data_users.csv DECRYPTED");

    EncryptionManager.DecryptFile(loginFilePath);
    MessageBox.Show("DataCache.SaveAndEncryptData>\ndata_login.csv DECRYPTED");
    Debug.WriteLine("DataCache.SaveAndEncryptData>\n data_login.csv DECRYPTED");

    // Save the cached user data to the file, joining fields into CSV lines.
    // Skip Header, ensure the header is ignored.
    File.WriteAllLines(userFilePath, CachedUserData.Skip(1).Select(fields => string.Join(",", fields)));

    // Save the cached login data to the file, joining fields into CSV lines.
    // Skip Header, ensure the header is ignored.
    File.WriteAllLines(loginFilePath, CachedLoginData.Skip(1).Select(fields => string.Join(",", fields)));


    // Encrypt the user and login data files to secure the contents.
    EncryptionManager.EncryptFile(userFilePath);
    MessageBox.Show("DataCache.SaveAndEncryptData>\n data_users.csv ENCRYPTED");
    Debug.WriteLine("DataCache.SaveAndEncryptData>\n data_users.csv ENCRYPTED");
    EncryptionManager.EncryptFile(loginFilePath);
    Debug.WriteLine("DataCache.SaveAndEncryptData>\n data_login.csv ENCRYPTED");
    MessageBox.Show("DataCache.SaveAndEncryptData>\ndata_login.csv ENCRYPTED");
}
Kan het zijn dat hier bij het aanroepen van cache.SaveAndEncryptData(); niet alle gegevens in de csv bestanden worden opgeslagen en nu missen? We gaan zomaar van 13 naar 11 items
Debug:
Details data_users.csv cached in CachedUserData: 13 items
Details data_login.csv cached in CachedLoginData: 13 items
DataCache.SaveAndEncryptData>
 data_users.csv DECRYPTED
DataCache.SaveAndEncryptData>
 data_login.csv DECRYPTED
DataCache.SaveAndEncryptData>
 data_users.csv ENCRYPTED
DataCache.SaveAndEncryptData>
 data_login.csv ENCRYPTED
(28-11-2024 14:48) [MIST001] Logged IN
Details data_users.csv cached in CachedUserData: 11 items
Details data_login.csv cached in CachedLoginData: 11 items
 */
