using CRUD_System.FileHandlers;
using System.Diagnostics;
using CRUD_System.Handlers;

/// <summary>
/// The DataCache class is responsible for managing in-memory caching of user and login data.
/// It provides functionality to decrypt files when loading data into memory and encrypt files when saving data back.
/// </summary>
public class DataCache
{
    #region PROPERTIES
    // Instance of FilePaths to get paths to the required files.
    private readonly static FilePaths path = new FilePaths();

    // File paths for user and login data.
    private readonly string userFilePath = path.UserFilePath;
    private readonly string loginFilePath = path.LoginFilePath;

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
        // 
    }
    #endregion CONSTRUCTOR

    #region LOAD DATA
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
        // Decrypt user and login data files to prepare them for reading
        EncryptionManager.DecryptFile(userFilePath);
        EncryptionManager.DecryptFile(loginFilePath);

        // Read the decrypted user data file and split each line into fields (CSV format)
        // Skip the header and split by comma, caching all records into CachedUserData
        CachedUserData = File.ReadAllLines(userFilePath)
                             .Select(line => line.Split(",")) // Split each line into an array of fields
                             .ToList(); // Store all records in CachedUserData

        // Read the decrypted login data file and split each line into fields (CSV format)
        // Skip the header and split by comma, caching all records into CachedLoginData
        CachedLoginData = File.ReadAllLines(loginFilePath)
                              .Select(line => line.Split(",")) // Split each line into an array of fields
                              .ToList(); // Store all records in CachedLoginData

        // Encrypt the user and login data files again to ensure the data is secured after loading
        EncryptionManager.EncryptFile(userFilePath);
        EncryptionManager.EncryptFile(loginFilePath);
    }
    #endregion LOAD DATA

    #region SAVE DATA
    /// <summary>
    /// Saves the cached data back to the user and login files and encrypts the files.
    /// Ensures that any changes made to the in-memory cache are persisted securely.
    /// </summary>
    public void SaveAndEncryptData()
    {
        EncryptionManager.DecryptFile(userFilePath);

        EncryptionManager.DecryptFile(loginFilePath);

        // Save the cached user data to the file, joining fields into CSV lines.
        // Skip Header, ensure the header and Admin user details are ignored.
        File.WriteAllLines(userFilePath, 
                           new[] { "[0] NAME,[1] SURNAME,[2] ALIAS,[3] ADRESS,[4] ZIPCODE,[5] CITY,[6] EMAIL ADRESS,[7] PHONENUMBER,[8] ONLINE STATUS" }
                           .Skip(1)
                           .Concat(CachedUserData
                           .Select(fields => string.Join(",", fields)
                            )));

        // Save the cached login data to the login file.
        // Add the header row explicitly before saving the data.
        File.WriteAllLines(loginFilePath, 
                           new[] { "[0] Alias,[1] PASSWORD,[2] ADMIN,[3] ONLINESTATUS" }
                           .Skip(1)
                           .Concat(CachedLoginData
                           .Select(fields => string.Join(",", fields)
                            )));

        // Encrypt the user and login data files to secure the contents.
        EncryptionManager.EncryptFile(userFilePath);
        EncryptionManager.EncryptFile(loginFilePath);
    }
    #endregion SAVE DATA
}
