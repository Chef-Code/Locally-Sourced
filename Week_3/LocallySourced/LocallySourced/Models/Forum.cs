using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class Forum
    {
        public virtual int ForumID { get; set; }
        public virtual Member Member { get; set; }
        public virtual Message Message { get; set; }
        public virtual Topic Topic { get; set; }
        
    }
}