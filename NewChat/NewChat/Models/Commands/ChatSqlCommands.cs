using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NewChat.Models.Commands
{
    public class ChatSqlCommands
    {
        private SqlConnection Connection = null;
        private SqlCommand SqlCommand = null;

        public ChatSqlCommands(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("Default").Value);

        }

    public async Task<DbDataReader> NewMessage(string Message, int SenderId, int RecipientId)
        {
            DateTime SendDate = DateTime.Now;
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("INSERT INTO chats(SenderId,RecipientId,Message,DayOfMessage) VALUES ('" + SenderId + "','" + RecipientId + "','" + Message + "','" + SendDate + "')", Connection);


            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }

        public async Task<DbDataReader>ChatMessages(int Sender, int Reciever)
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("SELECT * FROM chats WHERE SenderId IN (" + Sender + "," + Reciever + ")" + " AND RecipientId IN (" + Sender + "," + Reciever + ") ORDER BY  DayOfMessage", Connection);

            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        public async Task CloseConnection()
        {
            await Connection.CloseAsync();
        }

    }
    
}
