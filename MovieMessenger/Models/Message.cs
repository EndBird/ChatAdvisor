using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMessenger.Models
{
    public class Message
    {
        
            public int ID { get; set; }
            public string Text { get; set; }
            public string From { get; set; }
            public string To { get; set; }

    }
}
