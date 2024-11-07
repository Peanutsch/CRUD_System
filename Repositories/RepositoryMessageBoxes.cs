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
        public DialogResult MessageBoxConfirmNewUser(string alias)
        {
            return MessageBox.Show($"Please confirm to SAVE new account {alias.ToUpper()}", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageBoxConfirmToSAVEChanges(string alias)
        {
            return MessageBox.Show($"Please confirm to SAVE the changes for {alias.ToUpper()}?", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageBoxConfirmToSAVEPassword(string alias)
        {
            return MessageBox.Show($"Please confirm to SAVE the new password for {alias.ToUpper()}?", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageBoxConfirmToGeneratePassword(string alias)
        {
            return MessageBox.Show($"Please confirm to GENERATE a NEW password for {alias.ToUpper()}?", "Confirm", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageBoxConfirmToDELETE(string aliasToDelete)
        {
            return MessageBox.Show($"Please confirm to DELETE account {aliasToDelete.ToUpper()}?", "Confirm", MessageBoxButtons.YesNo);
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
            return MessageBox.Show($"User with alias [{alias}] is already online");
        }
        #endregion INVALID
    }
}


