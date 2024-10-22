/*
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CRUD_System
{
    /// <summary>
    /// Represents the interface for user details management, encapsulating 
    /// the interaction between the user interface elements and user data.
    /// </summary>
    public class Interface
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Alias { get; set; }
        public required string Address { get; set; }
        public required string ZIPCode { get; set; }
        public required string City { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }

        private readonly Dictionary<string, TextBox?> textBoxes; /// Dictionary to store references to the relevant TextBox controls.
        public readonly UserDetails? UserDetails;
        public readonly Button? BtnEditUserDetails, BtnDeleteUserDetails, BtnSaveEditUserDetails, BtnCreateUser, BtnDeleteUser, BtnGeneratePSW;
        public readonly CheckBox? ChkIsAdmin;
        public readonly ListBox? ListBoxUsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Interface"/> class.
        /// </summary>
        /// <param name="user">The user details.</param>
        /// <param name="textBoxes">A dictionary of TextBox controls.</param>
        /// <param name="isAdmin">The checkbox indicating if the user is an admin.</param>
        /// <param name="listBoxUsers">The list box for displaying users.</param>
        public Interface(UserDetails? user,
                         Dictionary<string, TextBox?> textBoxes,
                         CheckBox? isAdmin,
                         ListBox? listBoxUsers)
        {
            UserDetails = user;
            this.textBoxes = textBoxes;
            ChkIsAdmin = isAdmin;
            ListBoxUsers = listBoxUsers;
        }

        /// <summary>
        /// Toggles the edit mode for the interface elements.
        /// </summary>
        /// <param name="editMode">If set to <c>true</c>, enables edit mode; otherwise, disables it.</param>
        public void SetEditMode(bool editMode)
        {
            Form parentForm = new Form();
            parentForm.BackColor = editMode ? Color.Orange : SystemColors.ActiveCaption;

            SetControlVisibility(BtnSaveEditUserDetails, editMode);
            SetControlVisibility(ChkIsAdmin, editMode);

            foreach (var textBox in textBoxes.Values)
            {
                SetTextBoxEnabled(textBox, editMode);
            }

            // Handle buttons and other controls based on editMode
            SetControlVisibility(BtnCreateUser, !editMode);
            SetControlVisibility(BtnDeleteUser, !editMode);
            SetControlVisibility(BtnGeneratePSW, !editMode);
            SetControlVisibility(textBoxes["TxtPassword"], !editMode);
            SetControlEnabled(ListBoxUsers, !editMode);
        }

        /// <summary>
        /// Fills the interface elements with user details.
        /// </summary>
        public void DisplayUserDetails()
        {
            if (UserDetails != null)
            {
                SetTextBoxValue("TxtName", UserDetails.Name);
                SetTextBoxValue("TxtSurname", UserDetails.Surname);
                SetTextBoxValue("TxtAlias", UserDetails.Alias);
                SetTextBoxValue("TxtAddress", UserDetails.Address);
                SetTextBoxValue("TxtZipCode", UserDetails.ZIPCode);
                SetTextBoxValue("TxtCity", UserDetails.City);
                SetTextBoxValue("TxtEmail", UserDetails.Email);
                SetTextBoxValue("TxtPhoneNumber", UserDetails.PhoneNumber);
            }
        }

        /// <summary>
        /// Updates the user details with values from the interface.
        /// </summary>
        public void UpdateUserDetails()
        {
            if (UserDetails != null)
            {
                UserDetails.Name = GetTextBoxValue("TxtName");
                UserDetails.Surname = GetTextBoxValue("TxtSurname");
                UserDetails.Alias = GetTextBoxValue("TxtAlias");
                UserDetails.Address = GetTextBoxValue("TxtAddress");
                UserDetails.ZIPCode = GetTextBoxValue("TxtZipCode");
                UserDetails.City = GetTextBoxValue("TxtCity");
                UserDetails.Email = GetTextBoxValue("TxtEmail");
                UserDetails.PhoneNumber = GetTextBoxValue("TxtPhoneNumber");
            }
        }

        /// <summary>
        /// Clears the values in the interface elements.
        /// </summary>
        public void ClearUserDetails()
        {
            foreach (var textBox in textBoxes.Values)
            {
                SetTextBoxValue(textBox, string.Empty);
            }
        }

        #region Helper methods
        /// <summary>
        /// Sets the visibility of a control.
        /// </summary>
        /// <param name="control">The control to modify.</param>
        /// <param name="visible">If set to <c>true</c>, the control will be visible; otherwise, it will be hidden.</param>
        private void SetControlVisibility(Control? control, bool visible)
        {
            if (control != null)
            {
                control.Visible = visible;
            }
        }

        /// <summary>
        /// Enables or disables a TextBox control.
        /// </summary>
        /// <param name="textBox">The TextBox to modify.</param>
        /// <param name="enabled">If set to <c>true</c>, the TextBox will be enabled; otherwise, it will be disabled.</param>
        private void SetTextBoxEnabled(TextBox? textBox, bool enabled)
        {
            if (textBox != null)
            {
                textBox.Enabled = enabled;
            }
        }

        /// <summary>
        /// Enables or disables a control.
        /// </summary>
        /// <param name="control">The control to modify.</param>
        /// <param name="enabled">If set to <c>true</c>, the control will be enabled; otherwise, it will be disabled.</param>
        private void SetControlEnabled(Control? control, bool enabled)
        {
            if (control != null)
            {
                control.Enabled = enabled;
            }
        }

        /// <summary>
        /// Sets the value of a TextBox control identified by its key.
        /// </summary>
        /// <param name="key">The key to identify the TextBox.</param>
        /// <param name="value">The value to set.</param>
        private void SetTextBoxValue(string key, string value)
        {
            if (textBoxes.TryGetValue(key, out var textBox) && textBox != null)
            {
                textBox.Text = value;
            }
        }

        /// <summary>
        /// Sets the value of a TextBox control.
        /// </summary>
        /// <param name="textBox">The TextBox to modify.</param>
        /// <param name="value">The value to set.</param>
        private void SetTextBoxValue(TextBox? textBox, string value)
        {
            if (textBox != null)
            {
                textBox.Text = value;
            }
        }

        /// <summary>
        /// Gets the value of a TextBox control identified by its key.
        /// </summary>
        /// <param name="key">The key to identify the TextBox.</param>
        /// <returns>The text value of the TextBox.</returns>
        private string GetTextBoxValue(string key)
        {
            if (textBoxes.TryGetValue(key, out var textBox) && textBox != null)
            {
                return textBox.Text;
            }
            return string.Empty;
        }
        #endregion Helper methods
    }
}
*/