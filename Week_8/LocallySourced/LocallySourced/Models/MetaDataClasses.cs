using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{

    public class MemberMetaData 
    {
        public int MemberID { get; set; }
        [Required(ErrorMessage = " A User Name is required. ")]
        [StringLength(24, ErrorMessage = " The User Name accepts a maximum of 24 characters ")]
        [Display(Name = "User Name")]
        public string NickName { get; set; }
        [Required(ErrorMessage = "A First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "A Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Member Since")]
        public DateTime DateJoined { get; set; }
    }

    public class MessageMetaData
    {
        [Required]
        public int MessageID { get; set; }
        [Required]
        public int MemberID { get; set; }
        [Required]
        public int TopicID { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date
        {
            get
            {
                return (this.dateCreated == default(DateTime))
                    ? this.dateCreated = DateTime.Now
                    : this.dateCreated;
            }
            set { this.dateCreated = value; }
        }
        private DateTime dateCreated = default(DateTime);
    }  
}