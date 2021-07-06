using AccManagement.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static AccManagement.Models.Contexts.UserModel;

namespace AccManagement.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private UserSqlCommands userSqlCommands;

        

        public UserController(IConfiguration config)
        {
            userSqlCommands = new UserSqlCommands(config);
        }

        [HttpGet]
        [Route("get/user/all")]
        public async Task<List<Users>> GetAllUsers()
        {
            List<Users> users = new List<Users>();
            DbDataReader userReader = null;
            userReader = await userSqlCommands.GetAllUsers();
            while (await userReader.ReadAsync())
            {
                users.Add(new Users()
                {
                    UserId=Int16.Parse(userReader.GetValue("UserId").ToString()),
                    FirstName = userReader.GetValue("FirstName").ToString(),
                    LastName = userReader.GetValue("LastName").ToString(),
                    Address = userReader.GetValue("Address").ToString(),
                    Organization = userReader.GetValue("Organization").ToString(),
                    PhoneNum = userReader.GetValue("PhoneNum").ToString(),
                    EmailAddress = userReader.GetValue("EmailAddress").ToString(),
                    Password = userReader.GetValue("Password").ToString(),
                    Role = userReader.GetValue("Role").ToString(),
                });    
            }
            await userSqlCommands.CloseConnection();
            return users;
        }


        //get user by id
        [HttpGet]
        [Route("get/user/{UserId}")]
        public async Task<List<Users>> GetUserById(string UserId)
        {
            List<Users> users = new List<Users>();
            DbDataReader userReader = null;
            userReader = await userSqlCommands.GetUserById(Int16.Parse(UserId));
            while (await userReader.ReadAsync())
            {
                users.Add(new Users()
                {
                    UserId = Int16.Parse(userReader.GetValue("UserId").ToString()),
                    FirstName = userReader.GetValue("FirstName").ToString(),
                    LastName = userReader.GetValue("LastName").ToString(),
                    Address = userReader.GetValue("Address").ToString(),
                    Organization = userReader.GetValue("Organization").ToString(),
                    PhoneNum = userReader.GetValue("PhoneNum").ToString(),
                    EmailAddress = userReader.GetValue("EmailAddress").ToString(),
                    Password = userReader.GetValue("Password").ToString(),
                    Role = userReader.GetValue("Role").ToString(),


                });
            }
            await userSqlCommands.CloseConnection();
            return users;
        }

        //Get user by email
       
        [HttpGet]
        [Route("get/user/ByEmailAddress")]
        public async Task<List<Users>> GetUserByEmail(string EmailAddress)
        {
            List<Users> users = new List<Users>();
            DbDataReader userReader = null;
            userReader = await userSqlCommands.GetUserByEmail(EmailAddress);
            while (await userReader.ReadAsync())
            {
                users.Add(new Users()
                {
                    UserId = Int16.Parse(userReader.GetValue("UserId").ToString()),
                    FirstName = userReader.GetValue("FirstName").ToString(),
                    LastName = userReader.GetValue("LastName").ToString(),
                    Address = userReader.GetValue("Address").ToString(),
                    Organization = userReader.GetValue("Organization").ToString(),
                    PhoneNum = userReader.GetValue("PhoneNum").ToString(),
                    EmailAddress = userReader.GetValue("EmailAddress").ToString(),
                    Password = userReader.GetValue("Password").ToString(),
                    Role = userReader.GetValue("Role").ToString(),


                });
            }
            await userSqlCommands.CloseConnection();
            return users;
        }

        //Create user
        [HttpPost]
        [Route("CreateUser")]
        public async Task<HttpStatusCode> CreateUser(Users user)
        {
            
            string FirstName = user.FirstName;
            string LastName = user.LastName;
            string Address = user.Address;
            string Organization = user.Organization;
            string PhoneNum = user.PhoneNum;
            string EmailAddress = user.EmailAddress;
            string Password=user.Password;
            string Role = user.Role;
            DbDataReader userReader = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.Created;
            try
            {
                userReader = await userSqlCommands.CreateUser(FirstName, LastName, Address, Organization, PhoneNum, EmailAddress, Password, Role);
            }
            catch (Exception ex)
            {
                httpStatusCode = HttpStatusCode.PreconditionFailed;
                Console.WriteLine(ex);
            }
            await userSqlCommands.CloseConnection();
            return httpStatusCode;
        }

        //update customer
        [HttpPut]
        [Route("update/user/{UserId}")]
        public async Task<HttpStatusCode> UpdateUser(string UserId, string FirstName, string LastName, string Address, string Organization, string PhoneNum, string EmailAddress, string Password)
        {
            DbDataReader userReader = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.Created;
            try
            {
                userReader = await userSqlCommands.UpdateUser(Int16.Parse(UserId), FirstName, LastName, Address, Organization, PhoneNum, EmailAddress, Password);
            }
            catch (Exception ex)
            {
                httpStatusCode = HttpStatusCode.PreconditionFailed;
                Console.WriteLine(ex);
            }
            await userSqlCommands.CloseConnection();
            return httpStatusCode;
        }

        //delete user
        [HttpDelete]
        [Route("delete/user/{UserId}")]
        public async Task<HttpStatusCode> DeleteUser(string UserId)
        {
            DbDataReader userReader = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.Created;
            try
            {
                userReader = await userSqlCommands.DeleteUser(Int16.Parse(UserId));
            }
            catch (Exception ex)
            {
                httpStatusCode = HttpStatusCode.PreconditionFailed;
                Console.WriteLine(ex);
            }
            await userSqlCommands.CloseConnection();
            return httpStatusCode;
        }
    }
}
