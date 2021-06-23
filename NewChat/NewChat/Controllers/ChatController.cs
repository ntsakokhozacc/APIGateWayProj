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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewChat.Controllers
{
    [Route("Chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private ChatSqlCommands chatSqlCommands;

        public ChatController(IConfiguration config)
        {
            chatSqlCommands = new ChatSqlCommands(config);
        }


        [HttpPost]
        [Route("/InsertMessages/(ChatId)")]
        public async Task<HttpStatusCode> NewMessage(string Message, string SenderId, string RecipientId)
        {
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
        [Route("/getChats")]
        public async Task<List<ChatModel>> ChatMessages(string Sender, string Reciever)
        {
            List<ChatModel> Chats = new List<ChatModel>();
            DbDataReader ChatReader = null;
            ChatReader = await chatSqlCommands.ChatMessages(Int16.Parse(Sender), Int16.Parse(Reciever));

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
