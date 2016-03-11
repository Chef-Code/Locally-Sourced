using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class Comment
    {
        [Key, ForeignKey("Message")]
        public int MessageID { get; set; }
        
        public string Body { get; set; }
        public DateTime Date { get; set; }

        public int MemberID { get; set; }
        public virtual Member Member { get; set; }
        public virtual Message Message { get; set; }

    }
}