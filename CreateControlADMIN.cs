using System;
using System.Windows.Forms;

namespace CRUD_System
{
    public partial class CreateControlADMIN : UserControl
    {
        private CreateFormADMIN editFormAdmin; // Referentie naar EditFormADMIN

        // Constructor die de EditFormADMIN- referentie accepteert
        public CreateControlADMIN(CreateFormADMIN editForm)
        {
            InitializeComponent();
            this.editFormAdmin = editForm; // Opslaan van de referentie
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Sluit het huidige EditFormADMIN venster
            editFormAdmin.Close();

            // Je kunt hier eventueel ook de AdminManagementControl tonen
            ManagementControlADMIN adminManagementControl = new ManagementControlADMIN();
            adminManagementControl.Show();
        }
    }
}
