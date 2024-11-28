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
}
