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
    public partial class CreateFormADMIN : Form
    {
        public CreateFormADMIN()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set focus ActiveControl on Form
            this.ActiveControl = focusButton;
        }

    }
}
