using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewChat.Models
{
    public partial class ChatModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int ChatId { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int RecipientId { get; set; }
        [Required]
        public string Message { get; set; }
      
        public string DayOfMessage { get; set; }




    }
}
