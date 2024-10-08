using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    internal class RootPath
    {      
        /// <summary>
        /// Initializes the root path for the application by determining the base directory 
        /// of the current AppDomain and locating the "CRUD_System" directory within it.
        /// </summary>
        /// <returns>
        /// Returns the root path as a string if the "CRUD_System" directory is found. 
        /// If the directory cannot be determined, it displays an error message and returns an empty string.
        /// </returns>
        internal static string GetRootPath()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;

            if (string.IsNullOrEmpty(directoryPath))
            {
                Debug.WriteLine("Error: Unable to determine root path.");
                MessageBox.Show("Error: Unable to determine root path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty; // Return an empty string
            }

            string[] directorySplitPath = directoryPath.Split(Path.DirectorySeparatorChar);
            int index = Array.IndexOf(directorySplitPath, "CRUD_System");

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
                //Debug.WriteLine("Error: 'CRUD_System' directory not found in path.");
                //MessageBox.Show("Error: 'CRUD_System' directory not found in path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty; // Return an empty string
            }
        }
    }
}
