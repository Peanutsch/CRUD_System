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
        public DialogResult MessageSucces()
        {
            return MessageBox.Show("User Details updated successfully!", "Succes", MessageBoxButtons.OK);
        }

        public DialogResult MessageBoxConfirmToSAVE(string alias)
        {
            return MessageBox.Show($"Please confirm to SAVE the changes for {alias.ToUpper()}?", "", MessageBoxButtons.YesNo);
        }

    public DialogResult MessageBoxConfirmToDELETE(string alias)
        {
            return MessageBox.Show($"Please confirm to DELETE user {alias.ToUpper()}?", "", MessageBoxButtons.YesNo);
        }

    }
}
