using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class Forum
    {
        public int ForumID { get; set; }
        public string ForumName { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}