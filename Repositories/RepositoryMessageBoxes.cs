using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System.Repositories
{
    /// <summary>
    /// Class for all MessageBoxes
    /// </summary>
    internal class RepositoryMessageBoxes
    {
        #region CONFIRM
        public DialogResult MessageConfirmNewUser(string alias)
        {
            return MessageBox.Show($"Please confirm to SAVE new account {alias.ToUpper()}", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageConfirmToSAVEChanges(string alias)
        {
            return MessageBox.Show($"Please confirm to SAVE the changes for {alias.ToUpper()}", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageConfirmToSAVEPassword(string alias)
        {
            return MessageBox.Show($"Please confirm to SAVE the new password for {alias.ToUpper()}", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageConfirmToGeneratePassword(string alias)
        {
            return MessageBox.Show($"Please confirm to GENERATE a NEW password for {alias.ToUpper()}", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageConfirmToDELETE(string aliasToDelete)
        {
            return MessageBox.Show($"Please confirm to DELETE account {aliasToDelete.ToUpper()}", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageConfirmCallInSickNotification(string alias)
        {
            return MessageBox.Show($"Please confirm to set user {alias.ToUpper()} on Absence due Illness", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageConfirmForceLogOutUser(string alias)
        {
            return MessageBox.Show($"Please confirm to logout user {alias.ToUpper()}", "Confirm", MessageBoxButtons.YesNo);
        }
        public DialogResult MessageConfirmSaveNote(string alias)
        {
            return MessageBox.Show($"Please confirm to save this note for user {alias.ToUpper()}", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageConfirmDeleteFile(string fileName)
        {
        return MessageBox.Show($"Are you sure you want to delete the file '{fileName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public DialogResult MessageConfirmIsTheOne(string alias)
        {
            return MessageBox.Show($"Are you sure to change SuperUser role for {alias}?", "Confirm Change Role", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageConfirmExit()
        {
            return MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo);
        }
        #endregion CONFIRM

        #region SUCCES
        public DialogResult MessageUpdateSucces()
        {
            return MessageBox.Show("User Details updated successfully!", "Succes", MessageBoxButtons.OK);
        }

        public DialogResult MessageDeleteSucces(string aliasToDelete)
        {
            return MessageBox.Show($"Account deleted [{aliasToDelete.ToUpper()}] successfully!", "Succes", MessageBoxButtons.OK);
        }

        public DialogResult MessageChangePasswordSucces(string alias)
        {
            return MessageBox.Show($"Password for [{alias.ToUpper()}] updated succesfully!");
        }

        public DialogResult MessageNewAccountSucces(string alias)
        {
            return MessageBox.Show($"New account {alias} created succesfully!");
        }

        public DialogResult MessageReportSaved(string date, string selectedAlias)
        {
            return MessageBox.Show($"Report saved as {selectedAlias}_{date}_report.csv");
        }

        #endregion SUCCES

        #region INVALID
        public DialogResult MessageInvalidNoUserSelected()
        {
            return MessageBox.Show("Please select a user first");
        }

        public DialogResult MessageInvalidInput()
        {
            return MessageBox.Show("Invalid input for Name and/or Surname");
        }

        public DialogResult MessageInvalidNamePassword()
        {
            return MessageBox.Show("Invalid username or password");
        }

        public DialogResult MessageInvalidPassword()
        {
            CreateNewPasswordForm psw = new CreateNewPasswordForm();
            return MessageBox.Show($"Invalid Password\n" +
                                   $"Must contain {psw.lengthPsw} or more chars.\n" +
                                   $"Must contain at least {psw.charToUpper} capital letters\n" +
                                   $"Must contain at least {psw.charIsDigi} numbers");
        }

        public DialogResult MessageInvalidConfirmationPassword()
        {
            return MessageBox.Show("Input password and confirmation password are not the same!");
        }

        public DialogResult MessageSomethingWentWrong()
        {
            return MessageBox.Show("Something went wrong! No currentUser known.");
        }

        public DialogResult MessageUserNotFound(string alias)
        {
            return MessageBox.Show($"Something went wrong! User {alias} not found.");
        }

        public DialogResult MessageUserAlreadyOnline(string alias)
        {
            return MessageBox.Show($"User with alias [{alias.ToUpper()}] is already online");
        }

        public DialogResult MessageDetailsNotComplete()
        {
            return MessageBox.Show("Details are not complete. Name, Surname and Email are required...");
        }
        #endregion INVALID
    }
}


