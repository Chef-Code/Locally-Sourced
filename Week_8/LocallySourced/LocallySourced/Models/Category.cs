using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}