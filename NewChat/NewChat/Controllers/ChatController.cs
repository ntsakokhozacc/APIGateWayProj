using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewChat.Models;
using NewChat.Models.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace NewChat.Controllers
{
    [Route("chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private ChatSqlCommands chatSqlCommands;

        public ChatController(IConfiguration config)
        {
            chatSqlCommands = new ChatSqlCommands(config);
        }


        [HttpPost]
        [Route("InsertMessage")]
        public async Task<HttpStatusCode> NewMessage(ChatModel Chat)
        {
            string Message = Chat.Message;
            string SenderId = Chat.SenderId.ToString();
            string RecipientId = Chat.RecipientId.ToString();

            DbDataReader ChatReader = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.Created;

            try
            {
                ChatReader = await chatSqlCommands.NewMessage( Message, Int16.Parse(SenderId), Int16.Parse(RecipientId));
            }
            catch (Exception ex)
            {
                httpStatusCode = HttpStatusCode.PreconditionFailed;
                Console.WriteLine(ex);

            }
            await chatSqlCommands.CloseConnection();
            return httpStatusCode;

        }

        [HttpGet]
        [Route("getChats/{SenderId}/{RecipientId}")]
        public async Task<List<ChatModel>> ChatMessages(string SenderId, string RecipientId)
        {
            List<ChatModel> Chats = new List<ChatModel>();
            DbDataReader ChatReader = null;
            ChatReader = await chatSqlCommands.ChatMessages(Int16.Parse(SenderId), Int16.Parse(RecipientId));

            while (await ChatReader.ReadAsync())
            {
                Chats.Add(new ChatModel()
                {
                    ChatId=Int16.Parse(ChatReader.GetValue("ChatId").ToString()),
                    SenderId = Int16.Parse(ChatReader.GetValue("SenderId").ToString()),
                    RecipientId = Int16.Parse(ChatReader.GetValue("RecipientId").ToString()),
                    Message = ChatReader.GetValue("Message").ToString(),
                    DayOfMessage = (string)(ChatReader.GetValue("DayOfMessage").ToString()),//Convert.ToDateTime(ChatReader.GetValue("DateTime"))

                });
            }
            await chatSqlCommands.CloseConnection();
            return Chats;

        }
    }


} 
