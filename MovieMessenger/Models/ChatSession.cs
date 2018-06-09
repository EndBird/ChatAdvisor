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
        
        //public int ID { get; set; }
        [ForeignKey("Username")] //make from and to a key collectively so that we dont have duplicate. plus no worry about id
        [Key, Column(Order =1)]
        public string From { get; set; }
        [ForeignKey("Username")]
        [Key, Column(Order =2)]
        public string To { get; set; }
        public string Chat { get; set; }
        public Account Account { get; set; } //to tell server where to look for username

        public ChatSession()
        {

        }
        public ChatSession(string From, string To, string chat)
        {
            this.From = From;
            this.To = To;
            this.Chat = chat;
        }

        public override string ToString()
        {
            return this.From + " " + this.To;
        }
    }
}
