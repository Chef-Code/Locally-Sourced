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
             Member member1 = new Member { FirstName = "Lonnie", LastName = "Teter-Davis", Email = "lonnieteter@yahoo.com", Password = "password", UserName = "Chef_Code" };
             Member member2 = new Member { FirstName = "Parker", LastName = "Davis", Email = "parkerdavis@gmail.com", Password = "password", UserName = "Chef_Code_jr" };
             Message message1 = new Message { From = member1, Subject = "Learning ASP.NET MVC", Body = "Here is the Body of my message #1 from Lonnie. ", Date = DateTime.Now, };
             Message message2 = new Message { From = member1, Subject = "Learning ASP.NET MVC", Body = "Here is the Body of my message #2 from Lonnie. ", Date = DateTime.Now, };
             Message message3 = new Message { From = member2, Subject = "Learning ASP.NET MVC", Body = "Here is the Body of my message #3 from Parker. ", Date = DateTime.Now, };
             Message message4 = new Message { From = member2, Subject = "Learning ASP.NET MVC", Body = "Here is the Body of my message #4 from Parker. ", Date = DateTime.Now, };
             Topic topic1 = new Topic { Category = "STEM", Description = "STEM refers to Science, Technology, Engineering, and Math. " };
             Topic topic2 = new Topic { Category = "Hobby", Description = "Hobby refers to recreational subjects " };

             base.Seed(context);
         }
            
        
    }
}