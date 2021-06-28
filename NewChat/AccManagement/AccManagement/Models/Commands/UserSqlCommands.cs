using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace AccManagement.Models.Commands
{
    public class UserSqlCommands
    {
        private SqlConnection Connection = null;
        private SqlCommand SqlCommand = null;

        public UserSqlCommands(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("Default").Value);
        }

        public async Task<DbDataReader> GetAllUsers()
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("select * from users", Connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }

        public async Task<DbDataReader> GetUserById(int UserId) 
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("select * from users where UserId = "+UserId, Connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }

        public async Task<DbDataReader> GetUserByRole(int Role)
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("select * from users where Role = " + Role, Connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }

        public async Task<DbDataReader> CreateUser(string FirstName, string LastName, string Address, string Organization, string PhoneNum, string EmailAddress, string Password, string Role)
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("INSERT INTO users (FirstName, LastName, Address, Organization, PhoneNum, EmailAddress, Password, Role) VALUES('" + FirstName + "','" + LastName + "','" + Address + "','" + Organization + "','" + PhoneNum + "','"+EmailAddress+"','" + Password + "','" + Role + "')", Connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }

        public async Task<DbDataReader> UpdateUser(int UserId, string FirstName, string LastName, string Address, string Organization, string PhoneNum, string EmailAddress, string Password)
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("UPDATE users SET  FirstName = '" + FirstName + "', LastName = '" + LastName + "', Address = '" + Address + "', Organization = '" + Organization + "', PhoneNum = '" + PhoneNum + "', EmailAddress = '" + EmailAddress + "', Password = '" + Password + "' WHERE UserId='" + UserId + "'", Connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }

        public async Task<DbDataReader> DeleteUser(int UserId)
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("Delete From users where UserId =" + UserId, Connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        public async Task<DbDataReader> GetUserByEmail(string EmailAddress)
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("select * From users where EmailAddress ='"+ EmailAddress+"'", Connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        public async Task CloseConnection()
        {
            await Connection.CloseAsync();
        }
    }



    }

