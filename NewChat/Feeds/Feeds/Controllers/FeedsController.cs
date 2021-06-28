using Feeds.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Feeds.Models.Context.NewsFeedsModel;

namespace Feeds.Controllers
{
    [ApiController]
    [Route("newsfeeds")]
    public class FeedsController : ControllerBase
    {
       private NewsFeedsSqlCommands newsFeedsSqlCommands;



        public FeedsController(IConfiguration config)
        {
            newsFeedsSqlCommands = new NewsFeedsSqlCommands(config);
        }

        
        [HttpGet]
        [Route("getAll")]
        public async Task<List<NewsFeeds>> GetAll()
        {
            List<NewsFeeds> users = new List<NewsFeeds>();
            DbDataReader  feedsReader = null;
            feedsReader = await newsFeedsSqlCommands.GetAllFeed();
            while (await feedsReader.ReadAsync())
            {
                users.Add(new NewsFeeds()
                {
                    FeedId = Int16.Parse(feedsReader.GetValue("FeedId").ToString()),
                    FeedTitle = feedsReader.GetValue("FeedTitle").ToString(),
                    FeedBody = feedsReader.GetValue("FeedBody").ToString(),
                    FeedDate = (string)(feedsReader.GetValue("FeedDate").ToString())
                });
            }
            await newsFeedsSqlCommands.CloseConnection();
            return users;
        }

        [HttpPost]
        [Route("newFeed")]
        public async Task<HttpStatusCode> CreateFeed(string FeedTitle, string FeedBody)
        {
            DbDataReader feedsReader = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.Created;
            try
            {
                feedsReader = await newsFeedsSqlCommands.Newfeed(FeedTitle, FeedBody);
            }
            catch (Exception ex)
            {
                httpStatusCode = HttpStatusCode.PreconditionFailed;
                Console.WriteLine(ex);
            }
            await newsFeedsSqlCommands.CloseConnection();
            return httpStatusCode;
        }

        [HttpDelete]
        [Route("delete/feed/{FeedId}")]
        public async Task<HttpStatusCode> DeleteUser(string FeedId)
        {
            DbDataReader feedsReader = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.Created;
            try
            {
                feedsReader = await newsFeedsSqlCommands.DeleteFeedById(Int16.Parse(FeedId));
            }
            catch (Exception ex)
            {
                httpStatusCode = HttpStatusCode.PreconditionFailed;
                Console.WriteLine(ex);
            }
            await newsFeedsSqlCommands.CloseConnection();
            return httpStatusCode;
        }
    }
}



