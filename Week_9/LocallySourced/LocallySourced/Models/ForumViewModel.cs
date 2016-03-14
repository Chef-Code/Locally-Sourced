using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class ForumViewModel
    {
        public int MessageID { get; set; }
        public string NickName { get; set; }
        public string Topic { get; set; }
        public string Subject { get; set; }  
        public string Body { get; set; }
        public DateTime Date { get; set; }
      
    }
}