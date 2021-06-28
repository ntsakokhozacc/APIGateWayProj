using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Feeds.Models.Commands
{
    public class NewsFeedsSqlCommands
    {
        private SqlConnection Connection = null;
        private SqlCommand SqlCommand = null;

        public NewsFeedsSqlCommands(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("Default").Value);

        }

        public async Task<DbDataReader> GetAllFeed()
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("select * from newsFeeds", Connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }

        public async Task<DbDataReader> Newfeed(string FeedTitle, string FeedBody)
        {
            DateTime FeedDate = DateTime.Now;
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("INSERT INTO newsFeeds(FeedTitle,FeedBody,FeedDate) VALUES ('" + FeedTitle + "','" + FeedBody + "','" + FeedDate + "')", Connection);


            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;

        }
        public async Task<DbDataReader> DeleteFeedById(int FeedId)
        {
            await Connection.OpenAsync();
            SqlCommand = new SqlCommand("Delete From newsFeeds where FeedId =" + FeedId, Connection);
            var Result = await SqlCommand.ExecuteReaderAsync();
            return Result;
        }
        public async Task CloseConnection()
        {
            await Connection.CloseAsync();
        }
    }
}
