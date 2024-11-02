using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using CRUD_System.FileHandlers;

namespace CRUD_System.Handlers
{
    /// <summary>
    /// Manages user accounts by handling user details and login information, 
    /// including updating, logging changes, and finding users by their alias.
    /// </summary>
    public class UserRepository
    {
        // File paths for user and login data, and log actions
        FilePaths path = new FilePaths();
        UserInterface userInterface;
        MessageBoxes message = new MessageBoxes();

        // Initialize DateTime for logging actions
        LogEntryActions log = new LogEntryActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };

        public UserRepository(ADMINMainControl adminControl)
        {
            userInterface = new UserInterface(adminControl);
        }

        /// <summary>
        /// Updates the user details in the system and saves them to the data_users.csv file.
        /// </summary>
        /// <param name="userLines">The list of user lines from the CSV file.</param>
        /// <param name="userIndex">The index of the user to update.</param>
        /// <param name="name">The new name of the user.</param>
        /// <param name="surname">The new surname of the user.</param>
        /// <param name="alias">The alias of the user.</param>
        /// <param name="address">The new address of the user.</param>
        /// <param name="zipCode">The new ZIP code of the user.</param>
        /// <param name="city">The new city of the user.</param>
        /// <param name="email">The new email of the user.</param>
        /// <param name="phoneNumber">The new phone number of the user.</param>
        public void UpdateUserDetails(List<string> userLines, int userIndex, string name, string surname, string alias, string address, string zipCode, string city, string email, string phoneNumber)
        {
            // Update the user line with new details
            userLines[userIndex] = $"{name},{surname},{alias},{address},{zipCode.ToUpper()},{city},{email},{phoneNumber}";

            // Confirm with the user before saving changes
            DialogResult dr = message.MessageBoxConfirmToSAVEChanges(alias);
            if (dr != DialogResult.Yes)
            {
                return; // Exit if the user does not confirm
            }

            // Write the updated user data back to the CSV file
            File.WriteAllLines(path.UserFilePath, userLines);
            Debug.WriteLine($"User Details after Update: {userLines[userIndex]}");

            // Log the update action if a current user is logged in
            var currentUser = LoginHandler.CurrentUser;
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Updated user details for {alias.ToUpper()}");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Updated user details for {alias.ToUpper()}";
                File.AppendAllText(path.LogEventFilePath, newLog + Environment.NewLine);

                // Display a success message to the user
                message.MessageUpdateSucces();
            }
        }

        /// <summary>
        /// Updates the user login details and saves them to the data_login.csv file.
        /// </summary>
        /// <param name="loginLines">The list of login lines from the CSV file.</param>
        /// <param name="userIndex">The index of the user to update.</param>
        public void UpdateUserLogin(List<string> loginLines, int userIndex)
        {
            var loginDetails = loginLines[userIndex].Split(','); // Split the current login details
            string currentAlias = loginDetails[0];

            // Update the login line, keeping the current admin status
            loginLines[userIndex] = $"{currentAlias},{loginDetails[1]},{loginDetails[2]}";

            // Confirm with the user before generating a new password
            DialogResult dr = message.MessageBoxConfirmToGeneratePassword(currentAlias);
            if (dr != DialogResult.Yes)
            {
                return; // Exit if the user does not confirm
            }

            // Write the updated login data back to the CSV file
            File.WriteAllLines(path.LoginFilePath, loginLines);
            Debug.WriteLine($"User Login after Update: {loginLines[userIndex]}");

            // Log the password generation action if a current user is logged in
            var currentUser = LoginHandler.CurrentUser;
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Generated new password for {currentAlias.ToUpper()}");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Generated new password for {currentAlias.ToUpper()}";
                File.AppendAllText(path.LogEventFilePath, newLog + Environment.NewLine);
            }
        }

        /// <summary>
        /// Logs the action of changing a password for a specified user.
        /// </summary>
        /// <param name="currentUser">The alias of the user who made the change.</param>
        /// <param name="alias">The alias of the user whose password was changed.</param>
        public void LogPasswordChange(string currentUser, string alias)
        {
            // Log the password change with the current user
            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}] Changed password for [{alias.ToUpper()}]");
                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Changed password for user [{alias.ToUpper()}]";
                File.AppendAllText(path.LogEventFilePath, newLog + Environment.NewLine);
            }
            else
            {
                // Log the action as unknown if no current user is logged in
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [UNKNOWN] Changed password for [{alias.ToUpper()}]");
                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},[UNKNOWN],Changed password for user [{alias.ToUpper()}]";
                File.AppendAllText(path.LogEventFilePath, newLog + Environment.NewLine);
            }
        }

        /// <summary>
        /// Finds the index of a user in the user and login lists based on the specified alias.
        /// </summary>
        /// <param name="userLines">The list of user lines from the CSV file.</param>
        /// <param name="loginLines">The list of login lines from the CSV file.</param>
        /// <param name="alias">The alias of the user to find.</param>
        /// <returns>The index of the user if found, otherwise -1.</returns>
        public int FindUserIndexByAlias(List<string> userLines, List<string> loginLines, string alias)
        {
            // Loop through user lines to find a match for the alias
            for (int index = 0; index < userLines.Count; index++)
            {
                var userDetails = userLines[index].Split(',');
                var loginDetails = loginLines[index].Split(",");
                if (userDetails[2] == alias && loginDetails[0] == alias)
                {
                    return index; // Return the index if alias is found
                }
            }

            // Alias not found; notify the user
            MessageBox.Show("User not found");
            return -1; // Return -1 to indicate the alias was not found
        }


        #region DELETE USER
        /// <summary>
        /// Method for deleting user from data_users.csv and data_log.csv
        /// </summary>
        public void DeleteUser(ADMINMainControl adminControl)
        {
            var currentUser = LoginHandler.CurrentUser;

            // Read lines from data_users.csv and data_login.csv
            var userLines = File.ReadAllLines(path.UserFilePath).ToList();
            var loginLines = File.ReadAllLines(path.LoginFilePath).ToList();

            //Get alias to delete from the selected user
            string aliasToDelete = adminControl.txtAlias.Text;

            int userIndex = FindUserIndexByAlias(userLines, loginLines, aliasToDelete);

            // MessageBox to confirm task
            MessageBoxes messageBoxes = new MessageBoxes();
            DialogResult dr = messageBoxes.MessageBoxConfirmToDELETE(aliasToDelete);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            // Remove the user from data_users.csv by alias
            userLines = userLines.Where(line =>
                                        !line.Split(',')[2].Trim().Equals(aliasToDelete,
                                        StringComparison.OrdinalIgnoreCase)).ToList();
            File.WriteAllLines(path.UserFilePath, userLines);

            // Remove the user from data_login.csv using the alias
            loginLines = loginLines.Where(line =>
                                            !line.Split(',')[0].Trim().Equals(aliasToDelete,
                                            StringComparison.OrdinalIgnoreCase)).ToList();
            File.WriteAllLines(path.LoginFilePath, loginLines);

            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}]: Deleted user [{aliasToDelete.ToUpper()}]");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Deleted user [{aliasToDelete.ToUpper()}]";
                File.AppendAllText(path.LogEventFilePath, newLog + Environment.NewLine);
            }
            else
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [UNKNOWN]: Deleted user [{aliasToDelete.ToUpper()}]");

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},[UNKNOWN],Deleted user [{aliasToDelete.ToUpper()}]";
                File.AppendAllText(path.LogEventFilePath, newLog + Environment.NewLine);
            }
            messageBoxes.MessageDeleteSucces(); // Show MessageBox Delete Succes
            userInterface.ReloadListBoxAdmin(userIndex);
            userInterface.EmptyTextBoxesADMIN();
        }
        #endregion DELETE USER
    }
}
