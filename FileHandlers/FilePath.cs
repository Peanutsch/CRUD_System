using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System.FileHandlers
{
    public class FilePaths
    {
        public string Users { get; private set; }
        public string Login { get; private set; }
        public string Log { get; private set; }

        public FilePaths()
        {
            string rootPath = RootPath.GetRootPath();
            if (!string.IsNullOrEmpty(rootPath))
            {
                Users = Path.Combine(rootPath, "FilesUserDetails", "data_users.csv");
                Login = Path.Combine(rootPath, "FilesUserDetails", "data_login.csv");
                Log = Path.Combine(rootPath, "FilesUserDetails", "logEvents.csv");
            }
            else
            {
                // Fallback of error handling indien nodig
                Users = string.Empty;
                Login = string.Empty;
                Log = string.Empty;
            }
        }
    }
}
