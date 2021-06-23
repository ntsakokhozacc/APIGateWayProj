using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccManagement.Models.Contexts
{
    public class UserModel 
    {
        public partial class Users
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
