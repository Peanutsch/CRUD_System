using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    internal class Data
    {
        #region USERDATA
        // Predefined username and password for validation.
        //List<object> LOGINDATA = new List<object> { "michiel", "admin", true };
        List<object> LOGINDATA = new List<object> { "personX", "admin", false };

        public string USERNAME => (string)LOGINDATA[0];
        public string PASSWORD => (string)LOGINDATA[1];
        public bool IsAdmin => (bool)LOGINDATA[2];

        #endregion

        string loginData = Path.Combine(RootPath(), "CRUD_LOGIN.xlsx");

        /// <summary>
        /// Initializes the root path for the application by determining the base directory 
        /// of the current AppDomain and locating the "CRUD_LoginSystem" directory within it.
        /// </summary>
        /// <returns>
        /// Returns the root path as a string if the "CRUD_LoginSystem" directory is found. 
        /// If the directory cannot be determined or is not found, it displays an error message 
        /// and returns an empty string.
        /// </returns>
        /// <remarks>
        /// This method splits the base directory path into segments, searches for the index 
        /// of the "CRUD_LoginSystem" directory, and constructs the root path from the segments 
        /// up to and including that index. If the directory is not found, an error message is logged 
        /// and shown to the user, indicating the issue.
        /// </remarks>
        static string RootPath()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;

            if (string.IsNullOrEmpty(directoryPath))
            {
                Debug.WriteLine("Error: Unable to determine root path.");
                MessageBox.Show("Error: Unable to determine root path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty; // Return an empty string
            }

            string[] directorySplitPath = directoryPath.Split(Path.DirectorySeparatorChar);
            int index = Array.IndexOf(directorySplitPath, "CRUD_LoginSystem");

            if (index != -1)
            {
                string rootPath = string.Join(Path.DirectorySeparatorChar.ToString(), directorySplitPath.Take(index + 1));

                if (!rootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    rootPath += Path.DirectorySeparatorChar;
                }
                return rootPath;
            }
            else
            {
                Debug.WriteLine("Error: 'CRUD_LoginSystem' directory not found in path.");
                MessageBox.Show("Error: 'CRUD_LoginSystem' directory not found in path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty; // Return an empty string
            }
        }


    }
}
