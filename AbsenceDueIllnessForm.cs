using CRUD_System.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_System
{
    public partial class AbsenceDueIllnessForm : Form
    {
        //private readonly AdminMainControl adminControl = new AdminMainControl();
        private readonly DataCache cache = new DataCache();
        private readonly ProfileManager profileManager = new ProfileManager();

        public AbsenceDueIllnessForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set focus ActiveControl at textbox txtAlias
            this.ActiveControl = txtAlias;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // passing bool false for testing
            //profileManager.AbsenceDueIllness(false, txtAlias.Text);
            this.Close();
        }
    }
}
