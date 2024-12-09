﻿using CRUD_System.FileHandlers;
using Microsoft.Win32;
using System;

namespace CRUD_System.Handlers
{
    public class FormInteractionHandler
    {
        #region PROPERTIES
        public bool UserSelected { get; set; } // Property to store selection state
        
        private ShowReportForm? showReportForm;
        #endregion PROPERTIES

        #region CONSTRUCTOR
        public FormInteractionHandler()
        {
            //
        }
        #endregion CONSTRUCTOR

        #region CREATE FORM
        /// <summary>
        /// Hides the specified parent control, opens the admin creation form as a dialog, 
        /// and restores the parent control's visibility after closing the form.
        /// </summary>
        /// <param name="parentControl">The parent control to hide while the admin creation form is displayed. If null, an error is shown.</param>
        public void Open_CreateForm(UserControl? parentControl = null)
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

        /// <summary>
        /// Closes the specified form. If the form is null, displays an error message.
        /// </summary>
        /// <param name="parentForm">The parent form to close. If null, an error is shown.</param>
        public void Close_CreateForm(Form? parentForm = null)
        {
            // Check if the parent form is provided and not null
            if (parentForm != null)
            {
                parentForm.Close();
            }
            else
            {
                MessageBox.Show("Parent form is not valid or is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion CREATE FORM

        #region CREATE NEW PASSWORD
        /// <summary>
        /// Opens a form to create a new password, optionally hiding a parent control during the form display.
        /// Restores the parent control's visibility once the new password form is closed.
        /// </summary>
        /// <param name="parentControl">The parent control to hide while the new password form is displayed. If null, no control is hidden.</param>
        public void Open_CreateNewPasswordForm(UserControl? parentControl = null)
        {
            // Hide the parent control if it is not null
            if (parentControl != null)
            {
                parentControl.Hide();
            }
            // Open the CreateNewPasswordForm as a dialog, ensuring it blocks interaction with other forms
            using (CreateNewPasswordForm createNewPassword = new CreateNewPasswordForm())
            {
                createNewPassword.ShowDialog(); // Show CreateNewPassword_Form as a dialog
            }
            // After closing the CreateNewPasswordForm, restore the parent control's visibility
            if (parentControl != null)
            {
                parentControl.Show();
            }
        }
        #endregion CREATE NEW PASSWORD

        #region SHOW REPORT FORM
        /// <summary> 
        /// Opens the ShowReportForm as a dialog without hiding the parent control.
        /// </summary>
        /// <param name="parentControl">
        /// The parent control (e.g., a UserControl or Form) to pass along to the ShowReportForm. 
        /// If null, an error message will be shown indicating that the parent control is invalid.
        /// </param>
        public void Open_ShowReportForm(UserControl? parentControl = null, string reportContent = "", string selectedAlias = "")
        {
            if (parentControl == null)
            {
                MessageBox.Show("Parent control is not valid or is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (showReportForm == null || showReportForm.IsDisposed)
            {
                showReportForm = new ShowReportForm();
                showReportForm.FormClosed += (s, e) => parentControl.Show();
            }

            // Load the report content into the ShowReportForm
            //parentControl.Hide();
            //showReportForm.LoadReport(reportContent);
            showReportForm.Show();
        }
        #endregion SHOW REPORT FORM

        #region CALL IN SICK
        public void Open_AbsenceDueIllnessForm(Form? parentControl = null)
        {
            if (parentControl != null)
            {
                parentControl.Hide();

                // Open AbsenceDueIllnessForm zonder te proberen het opnieuw te sluiten via Close()
                using AbsenceDueIllnessForm? absenceForm = parentControl as AbsenceDueIllnessForm;
                {
                    absenceForm?.ShowDialog(); // Show the existing form as Dialog
                }

                parentControl.Show(); // Restore visibility after closing the form
            }
            else
            {
                MessageBox.Show("Parent form is not valid or is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion CALL IN SICK

        #region BUTTON HANDLER
        /// <summary>
        /// Executes a specified action if a user is selected, otherwise executes a fallback action if provided.
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
        #endregion BUTTON HANDLER
    }
}
