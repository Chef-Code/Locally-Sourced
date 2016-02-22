using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class LocallySourcedDBInitializer : DropCreateDatabaseAlways<LocallySourcedDB>
    {
         protected override void Seed(LocallySourcedDB context)
         {
             var members = new List<Member>
            {
                new Member { Email = "sample@email.com", FirstName = "Sam", LastName = "Ple", Password = "password", UserName = "ABCMouse", DateJoined = DateTime.Parse("2016-01-25") },
                new Member { Email = "example@email.com", FirstName = "Exam", LastName = "Ple", Password = "password", UserName = "BCDMouse", DateJoined = DateTime.Parse("2015-05-02") },
                new Member { Email = "lonnieteter@email.com", FirstName = "Lonnie", LastName = "Teter", Password = "security", UserName = "ChefCode", DateJoined = DateTime.Parse("2014-12-06") },
                new Member { Email = "parkerdavis@email.com", FirstName = "Parker", LastName = "Davis", Password = "security", UserName = "ChefCodejr", DateJoined = DateTime.Parse("2014-06-12") },
                new Member { Email = "stephaniday@email.com", FirstName = "Stephani", LastName = "Day", Password = "security", UserName = "MrsChefCode", DateJoined = DateTime.Parse("2013-11-11") }
            };
             members.ForEach(m => context.Members.Add(m));
             context.SaveChanges();

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