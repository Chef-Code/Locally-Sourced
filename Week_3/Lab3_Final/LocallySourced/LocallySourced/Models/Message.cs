using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class Message
    {
        public virtual int MessageID { get; set; }

        public virtual string Subject { get; set; }

        public virtual string Body { get; set; }

        public virtual DateTime Date { get; set; }
 
        public virtual Member From { get; set; }  //this seems out of place, get clarification
        public virtual Topic Category { get; set; }

    }
}