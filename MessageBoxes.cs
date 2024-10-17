using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    /// <summary>
    /// Class for all MessageBoxes
    /// </summary>
    internal class MessageBoxes
    {
        public DialogResult MessageBoxConfirmationSAVE(string alias)
        {
            return MessageBox.Show($"Do you want to SAVE the changes for {alias.ToUpper()}?", "", MessageBoxButtons.YesNo);
        }

        public DialogResult MessageSucces()
        {
           return MessageBox.Show("User Details updated successfully!", "Succes", MessageBoxButtons.OK);
        }


    public DialogResult MessageBoxConfirmationDELETE(string alias)
        {
            return MessageBox.Show($"Do you want to DELETE the changes for {alias.ToUpper()}?", "", MessageBoxButtons.YesNo);
        }
    }
}
