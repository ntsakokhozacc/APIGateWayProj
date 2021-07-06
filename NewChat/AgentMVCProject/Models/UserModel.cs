using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentMVCProject.Models
{
    public class UserModel
    {
        public class Users
        {

            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string Organization { get; set; }
            public string PhoneNum { get; set; }
            public string EmailAddress { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }

        }
    }
}
