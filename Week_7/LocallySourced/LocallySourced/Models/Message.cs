using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    [MetadataType(typeof(MessageMetaData))]
    public class Message
    {
        public int MessageID { get; set; }
        public int MemberID { get; set; }
        public int TopicID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public virtual Member Member { get; set; }
        public virtual Topic Topic { get; set; }

    }
}