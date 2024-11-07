using CRUD_System.FileHandlers;
using System;

namespace CRUD_System.Handlers
{
    public class InteractionHandler
    {
        public bool UserSelected { get; set; } // Property to store selection state

        public InteractionHandler()
        {
            //
        }

        /// <summary>
        /// Hides MainForm, Opens CreateForm
        /// </summary>
        public void OpenCreateForm(UserControl? parentControl = null)
        {
            // MustNeed: explicitly cast ParentForm to MainFormADMIN before passing it to the OpenCreateForm method.
            // Check if ParentForm is not null and is of type MainFormADMIN
            if (parentControl != null)
            {
                parentControl.Hide();

                using (ADMINCreateForm createFormADMIN = new ADMINCreateForm())
                {
                    createFormADMIN.ShowDialog(); // Show ADMINCreateForm as Dialog
                }

                if (parentControl != null)
                {
                    parentControl.Show(); // Restore visibility after closing the form
                }
            }
            else
            {
                MessageBox.Show("Parent form is not valid or is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseCreateForm(Form? parentForm = null)
        {
            // MustNeed: explicitly cast ParentForm to MainFormADMIN before passing it to the OpenCreateForm method
            // Check if ParentForm is not null and is of type MainFormADMIN
            if (parentForm != null)
            {
                parentForm.Close();
            }
            else
            {
                MessageBox.Show("Parent form is not valid or is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
