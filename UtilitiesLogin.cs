using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    public class UtilitiesLogin
    {
        readonly string logAction = Path.Combine(RootPath.GetRootPath(), @"data\log.csv");

        public List<string> UsersOnline = new List<string>();
        public static string? CurrentUser { get; set; }

        LogActions log = new LogActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };

        public void AuthenticateUser(string inputUserName, string inputUserPSW)
        {
            LoginValidation loginValidation = new LoginValidation();

            if (loginValidation.ValidateLogin(inputUserName, inputUserPSW))
            {
                CurrentUser = inputUserName.ToLower();
                UsersOnline.Add(CurrentUser);
                bool isAdmin = loginValidation.IsAdmin(inputUserName, inputUserPSW);

                LogUserLogin(CurrentUser, isAdmin);
                OpenMainForm(isAdmin);
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

        public void PerformLogout()
        {
            if (!string.IsNullOrEmpty(CurrentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{CurrentUser.ToUpper()}] logged OUT");
                UsersOnline.Remove(CurrentUser);

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{CurrentUser.ToUpper()},Logged OUT";
                File.AppendAllText(logAction, newLog + Environment.NewLine);

                CurrentUser = null;
            }
        }

        private void LogUserLogin(string currentUser, bool isAdmin)
        {
            string role = isAdmin ? "Admin" : "User";
            Debug.WriteLine($"=====\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) {role} [{currentUser.ToUpper()}] Logged IN");

            string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Logged IN";
            File.AppendAllText(logAction, newLog + Environment.NewLine);
        }

        private void OpenMainForm(bool isAdmin)
        {
            Form mainForm = isAdmin ? new ADMINMainForm() : new USERSMainForm();
            mainForm.ShowDialog();
        }
    }
}

