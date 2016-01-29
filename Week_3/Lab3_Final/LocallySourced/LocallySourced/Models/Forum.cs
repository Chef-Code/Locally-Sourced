using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class Forum
    {
       
        public virtual int ForumID { get; set; }

        List<Message> messages = new List<Message>();

         public List<Message> Messages { get { return messages; } }

         public virtual Message MessageItem { get; set; }

    }
}