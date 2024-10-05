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
    public partial class MainForm : Form
    {
        Data _Data = new Data(); 
        LoginValidation _LoginValidation = new LoginValidation();

        public MainForm()
        {
            InitializeComponent();

            BoxDisplay();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set focus ActiveControl
            this.ActiveControl = buttonLOGOUT;
        }

        private void buttonLOGOUT_Click(object sender, EventArgs e)
        {
            // Hide the MainForm
            this.Hide();

            // Open the LoginForm
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            // Once MainForm is closed, close the LoginForm
            this.Close();
        }

        public void UpdateRoleLabel(bool isAdmin)
        {
            labelAdmin.BackColor = isAdmin ? Color.LightSkyBlue : Color.LightGreen;
            labelAdmin.Text = isAdmin ? "ADMIN" : "USER";
            labelAdmin.TextAlign = ContentAlignment.TopLeft;
        }

        public void BoxDisplay()
        {
            textBoxUserName.Text = $"{_Data.USERNAME.ToUpper()}";
            _LoginValidation.ValidateRights(this);
            //textBoxRole.Text = $"{LoginValidation.ValidateRights()}";
            //textBoxRole.Text = $"{LoginValidation.ValidateRights(this)}";
        }
    }
}
