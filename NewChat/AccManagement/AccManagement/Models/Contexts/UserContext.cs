using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AccManagement.Models.Contexts.UserModel;

namespace AccManagement.Models.Contexts
{
    public class UserContext : DbContext
    {
       public UserContext(DbContextOptions options) : base(options) 
        { 
        
        }
        public DbSet<Users> users { get; set; }
    }
}
