using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public partial class Member
    {
        public int MemberID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateJoined { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
       
    }
}