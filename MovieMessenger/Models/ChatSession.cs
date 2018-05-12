using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMessenger.Models
{
    public class ChatSession
    {   
        public int ID { get; set; }
        [ForeignKey("Username")]
        public string From { get; set; }
        [ForeignKey("Username")]
        public string To { get; set; }
        public string Chat { get; set; }
        public Account Account { get; set; }
    }
}
