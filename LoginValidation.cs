using System;
using System.Collections.Generic;


namespace CRUD_System
{
    /// <summary>
    /// The LoginValidation class provides methods to validate user credentials,
    /// including checking the username and password against predefined values.
    /// </summary>
    internal class LoginValidation
    {
        /* 
         * FORMAT LOGIN DATA CSV:
         * string USERNAME, string PASSWORD, bool ADMIN
         *      mtelst    ,     *****      ,      true
         *      userX     ,     *****      ,      false
        */

        /*
         * FORMAT USER DATA CSV
         * string FIRST NAME, (string INFIX), string SURNAME, string EMAIL
         * maybe also:
         * string STREET, string HOUSENUMBER, string HN EXT, string CITY, string COUNTRY, DateTime BIRTHDAY
         */

        Data _data = new Data();
        //MainForm _mainForm = new MainForm();

        /// <summary>
        /// Validates the provided username by comparing it to the stored username.
        /// The comparison is case-insensitive.
        /// </summary>
        /// <param name="inputUserName">The username entered by the user.</param>
        /// <returns>True if the username is valid; otherwise, false.</returns>
        public bool ValidateLoginName(string inputUserName)
        {
            // Convert input to lowercase and compare with the stored username
            return inputUserName.ToLower() == _data.USERNAME.ToLower();
        }

        /// <summary>
        /// Validates the provided password by comparing it to the stored password.
        /// </summary>
        /// <param name="inputUserPSW">The password entered by the user.</param>
        /// <returns>True if the password is valid; otherwise, false.</returns>
        public bool ValidatePassword(string inputUserPSW)
        {
            // Compare input with the stored password
            return inputUserPSW == _data.PASSWORD;
        }

        public void ValidateRights(MainForm _mainForm)
        {
            if (_data.IsAdmin)
            {
                _mainForm.UpdateRoleLabel(true);
                //return "ADMIN";
            }
            else
            {
                _mainForm.UpdateRoleLabel(false);
                //return "USER";
            }
        }
    }
}
