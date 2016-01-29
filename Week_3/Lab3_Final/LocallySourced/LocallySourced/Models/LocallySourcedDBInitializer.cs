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
             Topic topic1 = new Topic { Category = "STEM", Description = "STEM refers to Science, Technology, Engineering, and Math. " };
             Topic topic2 = new Topic { Category = "Hobby", Description = "Hobby refers to recreational subjects " };
             Member member1 = new Member { FirstName = "Lonnie", LastName = "Teter-Davis", Email = "lonnieteter@yahoo.com", Password = "password", UserName = "Chef_Code" };
             Member member2 = new Member { FirstName = "Parker", LastName = "Davis", Email = "parkerdavis@gmail.com", Password = "password", UserName = "Chef_Code_jr" };
             Message message1 = new Message { From = member1, Subject = "Learning ASP.NET MVC", Body = "Here is the Body of my message #1 from Lonnie. ", Date = new DateTime(2016, 01, 13), Category = topic1 };
             Message message2 = new Message { From = member1, Subject = "Learning ASP.NET MVC", Body = "Here is the Body of my message #2 from Lonnie. ", Date = new DateTime(2016, 01, 01), Category = topic2 };
             Message message3 = new Message { From = member2, Subject = "Learning ASP.NET MVC", Body = "Here is the Body of my message #3 from Parker. ", Date = new DateTime(2015, 12, 25), Category = topic1 };
             Message message4 = new Message { From = member2, Subject = "Learning ASP.NET MVC", Body = "Here is the Body of my message #4 from Parker. ", Date = new DateTime(2015, 12, 24), Category = topic2 };
             

             Forum forum1 = new Forum {Messages = { message1, message2, message3, message4} };

             context.Fora.Add(forum1);

             base.Seed(context);
         }
            
        
    }
}