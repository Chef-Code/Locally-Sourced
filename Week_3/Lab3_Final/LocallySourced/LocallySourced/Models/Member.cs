using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class Member
    {
        public virtual int MemberID { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }

        //Could the Member class have a property like the following
        //public virtual Message To { get; set; }
        //Should a Member have a List of messages associated to the class
        /*List<Message> messages = new List<Message>();
        public virtual List<Message> Messages { get { return messages; } }*/
    }
}