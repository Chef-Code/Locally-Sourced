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
        public int CommentID { get; set; }
        public int MemberID { get; set; }
        public int MessageID { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public virtual Member Member { get; set; }
        public virtual Message Message { get; set; }

        ////TODO: Map Comments in Comment to use Unique ID un-ambiguously
        //public virtual ICollection<Comment> Comments { get; set; }

    }
}