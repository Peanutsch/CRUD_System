using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_System
{
    public class UserDetails
    {
        // Properties
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Alias { get; set; }
        public string Address { get; set; }
        public string ZIPCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Constructor userDetails array to properties
        public UserDetails(string[] userDetails)
        {
            Name = userDetails[0];
            Surname = userDetails[1];
            Alias = userDetails[2];
            Address = userDetails[3];
            ZIPCode = userDetails[4];
            City = userDetails[5];
            Email = userDetails[6];
            PhoneNumber = userDetails[7];
        }
    }
}
