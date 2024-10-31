using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    internal class ControlsHandler
    {
        

        public ControlsHandler()
        {
            //
        }

        public void Open_CreateNewPasswordForm()
        {
            ADMINMainControl mainControl = new ADMINMainControl();
            CreateNewPassword_Form createNewPassword = new CreateNewPassword_Form();
            
            mainControl.Hide();
            createNewPassword.ShowDialog(); // Show CreateNewPassword_Form
            mainControl.Show(); // Show the main controls form again after createNewPassword is closed
        }
    }
}
