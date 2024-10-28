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
    public partial class USERS_PSW_Form : Form
    {
        private bool isPasswordVisible = false;

        public USERS_PSW_Form()
        {
            InitializeComponent();
        }

        private void checkBoxTogglePSW_CheckedChanged(object sender, EventArgs e)
        {
            // Check if there is text in the password box
            if (inputChangePSW1.Text.Length > 0)
            {
                isPasswordVisible = !isPasswordVisible; // Toggle between Visible and Hide password
                inputChangePSW1.PasswordChar = isPasswordVisible ? '\0' : '*'; // Show or hide the password
                checkBoxTogglePSW1.Text = isPasswordVisible ? "Hide Password" : "Show Password"; // Update the checkbox text based on the visibility state
            }
        }

        private void ValidatePSW(int length = 10)
        {
            string userPSW = inputChangePSW1.Text;

            char[] chars = userPSW.ToCharArray();


        }
    }
}
