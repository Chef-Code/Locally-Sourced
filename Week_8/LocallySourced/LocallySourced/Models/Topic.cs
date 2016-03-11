using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class Topic
    {
        public int TopicID { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}