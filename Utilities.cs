using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUD_System
{
    public class Utilities
    {
        #region PROPERTIES
        readonly string dataLogin = Path.Combine(RootPath.GetRootPath(), @"data\data_login.csv");
        readonly string dataUsers = Path.Combine(RootPath.GetRootPath(), @"data\data_users.csv");
        readonly string logAction = Path.Combine(RootPath.GetRootPath(), @"data\log.csv");

        LoginForm loginForm = new LoginForm();

        bool editMode = false;
        bool userSelected = false;
        bool isAdmin = false;

        #region Initialize DateTime for logging
        LogActions log = new LogActions
        {
            Date = DateTime.Now.Date,
            Time = DateTime.Now
        };
        #endregion Initialize DateTime
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public Utilities()
        {

        }
        #endregion CONSTRUCTOR

        #region HANDLERS
        public void PerformLogout()
        {
            var currentUser = LoginForm.CurrentUser;

            if (!string.IsNullOrEmpty(currentUser))
            {
                Debug.WriteLine($"\n({log.Date.ToShortDateString()} {log.Time.ToShortTimeString()}) [{currentUser.ToUpper()}] logged OUT");
                loginForm.UsersOnline.Remove(currentUser); // Remove user from List UsersOnline

                string newLog = $"{log.Date.ToShortDateString()},{log.Time.ToShortTimeString()},{currentUser.ToUpper()},Logged OUT";
                File.AppendAllText(logAction, newLog + Environment.NewLine);

                LoginForm.CurrentUser = null;
            }
        }
        #endregion HANDLERS
    }
}
