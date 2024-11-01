using CRUD_System.FileHandlers;
using System;

namespace CRUD_System.Handlers
{
    internal class UserInteractionHandler
    {
        public bool UserSelected { get; set; } // Property to store selection state

        public UserInteractionHandler()
        {
            //
        }

        /// <summary>
        /// Opens a form to create a new password, optionally hiding a parent control during the form display.
        /// </summary>
        /// <param name="parentControl">The parent control to hide while the new password form is displayed. If null, no control is hidden.</param>
        public void Open_CreateNewPasswordForm(UserControl? parentControl = null)
        {
            if (parentControl != null)
            {
                parentControl.Hide();
            }

            using (CreateNewPassword_Form createNewPassword = new CreateNewPassword_Form())
            {
                createNewPassword.ShowDialog(); // Show CreateNewPassword_Form as a dialog
            }

            if (parentControl != null)
            {
                parentControl.Show(); // Restore visibility after closing the form
            }
        }

        /// <summary>
        /// Executes the specified action if a user is selected, otherwise triggers a fallback action.
        /// </summary>
        /// <param name="action">The action to execute if a user is selected.</param>
        /// <param name="noUserSelectedAction">The action to execute if no user is selected. If null, no fallback action is performed.</param>
        public void PerformActionIfUserSelected(Action action, Action? noUserSelectedAction = null)
        {
            if (UserSelected)
            {
                action?.Invoke(); // Execute the action if a user is selected
            }
            else
            {
                noUserSelectedAction?.Invoke();
            }
        }
    }
}
