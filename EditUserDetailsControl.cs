using System;
using System.Windows.Forms;

namespace CRUD_System
{
    public partial class EditUserDetailsControl : UserControl
    {
        private EditFormADMIN editFormAdmin; // Referentie naar EditFormADMIN

        // Constructor die de EditFormADMIN- referentie accepteert
        public EditUserDetailsControl(EditFormADMIN editForm)
        {
            InitializeComponent();
            this.editFormAdmin = editForm; // Opslaan van de referentie
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Sluit het huidige EditFormADMIN venster
            editFormAdmin.Close();

            // Je kunt hier eventueel ook de AdminManagementControl tonen
            AdminManagementControl adminManagementControl = new AdminManagementControl();
            adminManagementControl.Show();

            // Als EditUserDetailsControl een UserControl is en niet zelfstandig wordt gesloten, 
            // is deze regel mogelijk niet nodig. Als het wel nodig is, kun je dit doen:
            // this.Parent.Controls.Remove(this); // Verwijder de UserControl uit de parent
        }
    }
}
