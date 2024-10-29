using System.Windows.Forms;
using System.Drawing;

namespace CRUD_System
{
    public class Interface
    {
        // UI-componenten voor het beheren van de gebruikersinterface
        public required TextBox Name { get; set; }
        public required TextBox Surname { get; set; }
        public required TextBox Alias { get; set; }
        public required TextBox Address { get; set; }
        public required TextBox ZIPCode { get; set; }
        public required TextBox City { get; set; }
        public required TextBox Email { get; set; }
        public required TextBox Phone { get; set; }
        public required TextBox Password { get; set; }
        public required TextBox Admin {  get; set; }

        public required Button btnEditUserDetails { get; set; }
        public required Button btnSaveEditUserDetails { get; set; }
        public required Button btnCreateUser { get; set; }
        public required Button btnDeleteUser { get; set; }
        public required Button btnGeneratePSW { get; set; }

        public required CheckBox chkIsAdmin { get; set; }
        public required ListBox listBoxAdmin { get; set; }

        private bool editMode = false;

        public Interface(TextBox name, TextBox surname, TextBox alias, TextBox address, TextBox zipCode,
                         TextBox city, TextBox email, TextBox phone, TextBox password, TextBox admin, Button editUserButton,
                         Button saveEditButton, Button createUserButton, Button deleteUserButton,
                         Button generatePasswordButton, CheckBox isAdminCheckBox, ListBox adminListBox)
        {
            Name = name;
            Surname = surname;
            Alias = alias;
            Address = address;
            ZIPCode = zipCode;
            City = city;
            Email = email;
            Phone = phone;
            Password = password;
            Admin = admin;

            btnEditUserDetails = editUserButton;
            btnSaveEditUserDetails = saveEditButton;
            btnCreateUser = createUserButton;
            btnDeleteUser = deleteUserButton;
            btnGeneratePSW = generatePasswordButton;

            chkIsAdmin = isAdminCheckBox;
            listBoxAdmin = adminListBox;
        }

        public UserDetails ExtractUserDetails()
        {
            return new UserDetails(new string[]
            {
                Name.Text,
                Surname.Text,
                Alias.Text,
                Address.Text,
                ZIPCode.Text,
                City.Text,
                Email.Text,
                Phone.Text,
                Password.Text
            });
        }

        public void PopulateFields(UserDetails userDetails)
        {
            Name.Text = userDetails.Name;
            Surname.Text = userDetails.Surname;
            Alias.Text = userDetails.Alias;
            Address.Text = userDetails.Address;
            ZIPCode.Text = userDetails.ZIPCode;
            City.Text = userDetails.City;
            Email.Text = userDetails.Email;
            Phone.Text = userDetails.PhoneNumber;
            //Password.Text = userDetails.Password;
        }

        public void ToggleEditMode()
        {
            // Toggle editMode
            editMode = !editMode;

            // Update knoptekst
            btnEditUserDetails.Text = editMode ? "Cancel" : "Edit User";

            // Achtergrondkleur bijwerken om de editMode-status weer te geven
            var parentForm = btnEditUserDetails.FindForm();
            if (parentForm != null)
                parentForm.BackColor = editMode ? Color.Orange : SystemColors.ActiveCaption;

            // Pas zichtbaarheid en inschakeling van knoppen en checkboxes aan
            btnSaveEditUserDetails.Visible = editMode;
            btnSaveEditUserDetails.BackColor = Color.LightGreen;

            chkIsAdmin.Visible = editMode;
            chkIsAdmin.Enabled = editMode;

            // Activeer of deactiveer de TextBoxes
            Name.Enabled = editMode;
            Surname.Enabled = editMode;
            Alias.Enabled = editMode;
            Address.Enabled = editMode;
            ZIPCode.Enabled = editMode;
            City.Enabled = editMode;
            Email.Enabled = editMode;
            Phone.Enabled = editMode;
            Password.Enabled = editMode;

            btnCreateUser.Visible = !editMode;
            btnCreateUser.Enabled = !editMode;
            btnDeleteUser.Visible = !editMode;
            btnDeleteUser.Enabled = !editMode;
            btnGeneratePSW.Visible = !editMode;
            btnGeneratePSW.Enabled = !editMode;

            listBoxAdmin.Enabled = !editMode;
        }

        public void ClearFields()
        {
            Name.Clear();
            Surname.Clear();
            Alias.Clear();
            Address.Clear();
            ZIPCode.Clear();
            City.Clear();
            Email.Clear();
            Phone.Clear();
            Password.Clear();
        }
    }
}
