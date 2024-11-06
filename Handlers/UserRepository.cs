using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using CRUD_System.FileHandlers;
using System.IO;

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
    }
}
