using System;

namespace CRUD_LoginSystem
{
    /// <summary>
    /// The LoginValidation class provides methods to validate user credentials,
    /// including checking the username and password against predefined values.
    /// </summary>
    internal class LoginValidation
    {
        // Predefined username and password for validation.
        private string isUserName = "admin";
        private string isUserPSW = "admin";

        /// <summary>
        /// Validates the provided username by comparing it to the stored username.
        /// The comparison is case-insensitive.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        /// <returns>True if the username is valid; otherwise, false.</returns>
        public bool ValidateLoginName(string inputUserName)
        {
            // Convert input to lowercase and compare with the stored username
            return inputUserName.ToLower() == isUserName.ToLower();
        }

        /// <summary>
        /// Validates the provided password by comparing it to the stored password.
        /// </summary>
        /// <param name="inputUserPSW">The password entered by the user.</param>
        /// <returns>True if the password is valid; otherwise, false.</returns>
        public bool ValidatePassword(string inputUserPSW)
        {
            // Compare input with the stored password
            return inputUserPSW == isUserPSW;
        }
    }
}
