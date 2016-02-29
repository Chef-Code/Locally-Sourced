using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace LocallySourced.Models
{
    public class LocallySourcedDBInitializer : DropCreateDatabaseAlways<LocallySourcedDB>
    {
         protected override void Seed(LocallySourcedDB context)
         {
             var userManager = new UserManager<Member>(
               new UserStore<Member>(context));
                            
            var members = new List<Member>
            {
                new Member { Email = "sample@email.com",UserName="sample@email.com", FirstName = "Sam", LastName = "Ple", Password = "Password1", NickName = "ABCMouse", DateJoined = DateTime.Parse("2016-01-25"), MemberID = 1 },
                new Member { Email = "example@email.com", UserName="example@email.com", FirstName = "Exam", LastName = "Ple", Password = "Password1", NickName = "BCDMouse", DateJoined = DateTime.Parse("2015-05-02"), MemberID = 2},
                new Member { Email = "lonnieteter@email.com",UserName="lonnieteter@email.com", FirstName = "Lonnie", LastName = "Teter", Password = "Password1", NickName = "ChefCode", DateJoined = DateTime.Parse("2014-12-06"), MemberID =3 },
                new Member { Email = "parkerdavis@email.com",UserName="parkerdavis@email.com", FirstName = "Parker", LastName = "Davis", Password = "Password1", NickName = "ChefCodejr", DateJoined = DateTime.Parse("2014-06-12"), MemberID =4 },
                new Member { Email = "stephaniday@email.com",UserName="stephaniday@email.com",FirstName = "Stephani", LastName = "Day", Password = "Password1", NickName = "MrsChefCode", DateJoined = DateTime.Parse("2013-11-11"), MemberID =5 }
            };
             //Now I save each member by logging them in and out to hash their password using OWIN -LONNIE
            foreach (var member in members)
            {
                var result = userManager.Create(member, member.Password);
                if (result.Succeeded)
                {
                    var identity = userManager.CreateIdentity(
                       member, DefaultAuthenticationTypes.ApplicationCookie);

                    HttpContext.Current.GetOwinContext().Authentication.SignIn(identity); //in
                    HttpContext.Current.GetOwinContext().Authentication.SignOut("ApplicationCookie"); //right back out
                }
            }
            
             try
             {
                 context.SaveChanges();
             }
             catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
             {
                 Exception raise = dbEx;
                 foreach (var validationErrors in dbEx.EntityValidationErrors)
                 {
                     foreach (var validationError in validationErrors.ValidationErrors)
                     {
                         string message = string.Format("{0}:{1}",
                             validationErrors.Entry.Entity.ToString(),
                             validationError.ErrorMessage);
                         // raise a new exception nesting
                         // the current instance as InnerException
                         raise = new InvalidOperationException(message, raise);
                     }
                 }
                 throw raise;
             }
 
             var topics = new List<Topic>
            {
                new Topic { Category = "Food", Description = "All things food"},
                new Topic { Category = "Science", Description = "Everything under the sun and beyond"},
                new Topic { Category = "Math", Description = "Things don't add up"},
                new Topic { Category = "Career", Description = "Post college life"},
                new Topic { Category = "College", Description = "Before they were professionals"}
            };
             topics.ForEach(t => context.Topics.Add(t));
             context.SaveChanges();

             var messages = new List<Message>
            {
                new Message { MemberID = 1, TopicID = 1, Subject = "I Love Food", Body = "How long you got...", Date = DateTime.Parse("2005-09-01")},
                new Message { MemberID = 2, TopicID = 1, Subject = "RE:I Love Food", Body = "I Got awhile...", Date = DateTime.Parse("2005-09-01")},
                new Message { MemberID = 3, TopicID = 1, Subject = "RE:I Love Food", Body = "I Got awhile too...", Date = DateTime.Parse("2005-09-01")},
                new Message { MemberID = 4, TopicID = 1, Subject = "I Hate Food", Body = "How long you got...", Date = DateTime.Parse("2005-09-01")},
                new Message { MemberID = 5, TopicID = 1, Subject = "RE:I Hate Food", Body = "Save it buddy...", Date = DateTime.Parse("2005-09-01")},
                new Message { MemberID = 1, TopicID = 2, Subject = "I Love Science", Body = "How long you got...", Date = DateTime.Parse("2005-09-01")},
                new Message { MemberID = 1, TopicID = 3, Subject = "I Love Math", Body = "How long you got...", Date = DateTime.Parse("2005-09-01")},
                new Message { MemberID = 1, TopicID = 4, Subject = "I Love My Career", Body = "How long you got...", Date = DateTime.Parse("2005-09-01")},
                new Message { MemberID = 1, TopicID = 5, Subject = "I Loved College", Body = "How long you got...", Date = DateTime.Parse("2005-09-01")},
                new Message { MemberID = 2, TopicID = 2, Subject = "I Love Science", Body = "How long you got...", Date = DateTime.Parse("2005-09-01")}
            };
             messages.ForEach(ms => context.Messages.Add(ms));
             context.SaveChanges();

             base.Seed(context);
         }
            
        
    }
}