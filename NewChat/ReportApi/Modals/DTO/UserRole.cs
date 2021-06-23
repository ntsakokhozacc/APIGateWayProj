using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ReportApi.Modals.DTO
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public string Roles { get; set; }

        public string Email { get; }
    }
}
