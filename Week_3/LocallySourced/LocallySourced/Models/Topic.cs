using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class Topic
    {
        public virtual int TopicID { get; set; }
        public virtual string Category { get; set; }
        public virtual string Description { get; set; }
    }
}