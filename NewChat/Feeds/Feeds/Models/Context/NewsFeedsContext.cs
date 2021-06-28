using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Feeds.Models.Context.NewsFeedsModel;

namespace Feeds.Models.Context
{
    public class NewsFeedsContext : DbContext
    {
        public NewsFeedsContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<NewsFeeds> newsFeeds { get; set; }
    }
}
