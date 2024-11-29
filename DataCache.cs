using CRUD_System.FileHandlers;
using CRUD_System;
using System.Diagnostics;

/// <summary>
/// The DataCache class is responsible for managing in-memory caching of user and login data.
/// It provides functionality to decrypt files when loading data into memory and encrypt files when saving data back.
/// </summary>
public class DataCache
{
    #region PROPERTIES
    // Instance of FilePaths to get paths to the required files.
    private static FilePaths path = new FilePaths();

    // File paths for user and login data.
    private readonly string userFilePath = path.UserFilePath;
    private readonly string loginFilePath = path.LoginFilePath;

    // A flag to indicate whether the cache is valid
    public static bool IsCacheValid { get; set; } = false;

    // Flag to check if data has already been loaded
    private bool isDataLoaded = false;

    public static List<string> CachedLoginLines { get; private set; } = new List<string>();
    public static List<string> CachedUserLines { get; private set; } = new List<string>();


    /// <summary>
    /// Cached user data, where each array represents a record (line) split into fields.
    /// </summary>
    public List<string[]> CachedUserData { get; private set; } = new List<string[]>();

    /// <summary>
    /// Cached login data, where each array represents a record (line) split into fields.
    /// </summary>
    public List<string[]> CachedLoginData { get; private set; } = new List<string[]>();
    #endregion PROPERTIES

    #region CONSTRUCTOR
    /// <summary>
    /// Constructor for the DataCache class.
    /// Automatically decrypts and loads data into memory upon initialization.
    /// </summary>
    public DataCache()
    {
        //LoadDecryptedData();
    }
    #endregion CONSTRUCTOR

    public static void InvalidateCache()
    {
        IsCacheValid = false;
    }

    // Method to load cache from files
    public static void LoadCache()
    {
        if (!CachedLoginLines.Any() || !CachedUserLines.Any()) // Check if cache is already loaded
        {
            // Load login and user data from the files
            var (userLines, loginLines) = path.ReadUserAndLoginData();
            CachedUserLines = userLines;
            CachedLoginLines = loginLines;
        }
    }

    /// <summary>
    /// Decrypts and loads data into memory if not already loaded.
    /// Ensures that data is loaded only once to avoid unnecessary decryption.
    /// </summary>
    public void LoadDecryptedData()
    {
        // If data has already been loaded, skip reloading and display a message
        if (isDataLoaded)
        {
            /*
            Debug.WriteLine("Data already loaded, skipping reload.");
            return; // If data is already loaded, do nothing.
            */
        }

        // Decrypt user and login data files to prepare them for reading
        EncryptionManager.DecryptFile(userFilePath);
        EncryptionManager.DecryptFile(loginFilePath);

        // Read the decrypted user data file and split each line into fields (CSV format)
        // Skip the header and split by comma, caching all records into CachedUserData
        CachedUserData = File.ReadAllLines(userFilePath)
            .Skip(1) // Skip the header row in the CSV
            .Select(line => line.Split(",")) // Split each line into an array of fields
            .ToList(); // Store all records in CachedUserData
        Debug.WriteLine($"***\nDetails data_users.csv cached in CachedUserData: {CachedUserData.Count} items");

        // Read the decrypted login data file and split each line into fields (CSV format)
        // Skip the header and split by comma, caching all records into CachedLoginData
        CachedLoginData = File.ReadAllLines(loginFilePath)
            .Skip(1) // Skip the header row in the CSV
            .Select(line => line.Split(",")) // Split each line into an array of fields
            .ToList(); // Store all records in CachedLoginData
        Debug.WriteLine($"Details data_login.csv cached in CachedLoginData: {CachedLoginData.Count} items\n***");

        // Encrypt the user and login data files again to ensure the data is secured after loading
        EncryptionManager.EncryptFile(userFilePath);
        EncryptionManager.EncryptFile(loginFilePath);

        // Mark that the data has been successfully loaded
        isDataLoaded = true;
    }


    /// <summary>
    /// Saves the cached data back to the user and login files and encrypts the files.
    /// Ensures that any changes made to the in-memory cache are persisted securely.
    /// </summary>
    public void SaveAndEncryptData()
    {
        EncryptionManager.DecryptFile(userFilePath);
        Debug.WriteLine("***\nDataCache.SaveAndEncryptData> data_users.csv DECRYPTED");

        EncryptionManager.DecryptFile(loginFilePath);
        Debug.WriteLine("DataCache.SaveAndEncryptData> data_login.csv DECRYPTED");

        // Save the cached user data to the file, joining fields into CSV lines.
        // Skip Header, ensure the header is ignored.
        File.WriteAllLines(userFilePath, 
                           new[] { "[0] NAME,[1] SURNAME,[2] ALIAS,[3] ADRESS,[4] ZIPCODE,[5] CITY,[6] EMAIL ADRESS,[7] PHONENUMBER,[8] ONLINE STATUS" }
                           .Concat(CachedUserData
                           .Select(fields => string.Join(",", fields)
                            )));

        Debug.WriteLine("\nSaving user details to data_users.csv");

        // Save the cached login data to the login file.
        // Add the header row explicitly before saving the data.
        File.WriteAllLines(loginFilePath, 
                           new[] { "[0] Alias,[1] PASSWORD,[2] ADMIN,[3] ONLINESTATUS" }
                           .Concat(CachedLoginData
                           .Select(fields => string.Join(",", fields)
                            )));

        Debug.WriteLine("Saving login details to data_login.csv\n");

        // Encrypt the user and login data files to secure the contents.
        EncryptionManager.EncryptFile(userFilePath);
        Debug.WriteLine("DataCache.SaveAndEncryptData> data_users.csv ENCRYPTED");
        EncryptionManager.EncryptFile(loginFilePath);
        Debug.WriteLine("DataCache.SaveAndEncryptData> data_login.csv ENCRYPTED\n***");
    }
}
