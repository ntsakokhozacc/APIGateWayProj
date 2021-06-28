using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Feeds.Models.Context
{
    public class NewsFeedsModel
    {
        public partial class NewsFeeds
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int FeedId { get; set; }
            [Required]
            public string FeedTitle { get; set; }
            [Required]
            public string FeedBody { get; set; }
            [Required]
            public string FeedDate { get; set; }
        }
    }
}
