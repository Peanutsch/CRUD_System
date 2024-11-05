using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using CRUD_System.FileHandlers;
using System.IO;

namespace CRUD_System.Handlers
{
    public class Repository
    {
        FilePaths path = new FilePaths();

        MessageBoxes message = new MessageBoxes();

        // Initialize DateTime for logging
        LogEntryActions log = new LogEntryActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };

        public void UpdateUserPassword(List<string> loginLines, int userIndex)
        {
            var loginDetails = loginLines[userIndex].Split(',');
            string currentAlias = loginDetails[0];

            loginLines[userIndex] = $"{currentAlias},{loginDetails[1]},{loginDetails[2]}"; // Keep current admin status

            DialogResult dr = message.MessageBoxConfirmToGeneratePassword(currentAlias);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            File.WriteAllLines(path.LoginFilePath, loginLines); // Write updated data back to data_login.csv
            Debug.WriteLine($"User Login after Update: {loginLines[userIndex]}");

            var currentUser = LoginHandler.CurrentUser;
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Generated new password for {currentAlias.ToUpper()}");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Generated new password for {currentAlias.ToUpper()}";
                //File.AppendAllText(logAction, newLog + Environment.NewLine);
                path.AppendToLog(newLog);
            }
        }

        public void LogPasswordChange(string currentUser, string alias)
        {
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}] Changed password for [{alias.ToUpper()}]");
                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Changed password for user [{alias.ToUpper()}]";
                //File.AppendAllText(logAction, newLog + Environment.NewLine);
                path.AppendToLog(newLog);
            }
            else
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [UNKNOWN] Changed password for [{alias.ToUpper()}]");
                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},[UNKNOWN],Changed password for user [{alias.ToUpper()}]";
                //File.AppendAllText(logAction, newLog + Environment.NewLine);
                path.AppendToLog(newLog);
            }
        }

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
            MessageBox.Show("User not found");
            return -1;
        }
    }
}
