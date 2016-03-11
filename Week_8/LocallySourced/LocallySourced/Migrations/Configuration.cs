namespace LocallySourced.Migrations
{
    using LocallySourced.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LocallySourced.DAL.LocallySourcedDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LocallySourced.DAL.LocallySourcedDB context)
        {
            var forums = new List<Forum>()
            {
                new Forum { Categories = new List<Category>(), ForumName = "Main"}
            };
            forums.ForEach(s => context.Fora.AddOrUpdate(p => p.ForumName, s));

            InitializeIdentityForEF(context);
            var userManager = new UserManager<Member>(
              new UserStore<Member>(context));

            var members = new List<Member>
            {
                new Member { Email = "sample@email.com",UserName="sample@email.com", FirstName = "Sam", LastName = "Ple", Password = "Password1", NickName = "ABCMouse", DateJoined = DateTime.Parse("2016-01-25"), MemberID = 1, Messages = new List<Message>() },
                new Member { Email = "example@email.com", UserName="example@email.com", FirstName = "Exam", LastName = "Ple", Password = "Password1", NickName = "BCDMouse", DateJoined = DateTime.Parse("2015-05-02"), MemberID = 2, Messages = new List<Message>()},
                new Member { Email = "lonnieteter@email.com",UserName="lonnieteter@email.com", FirstName = "Lonnie", LastName = "Teter", Password = "Password1", NickName = "ChefCode", DateJoined = DateTime.Parse("2014-12-06"), MemberID =3, Messages = new List<Message>() },
                new Member { Email = "parkerdavis@email.com",UserName="parkerdavis@email.com", FirstName = "Parker", LastName = "Davis", Password = "Password1", NickName = "ChefCodejr", DateJoined = DateTime.Parse("2014-06-12"), MemberID =4, Messages = new List<Message>() },
                new Member { Email = "stephaniday@email.com",UserName="stephaniday@email.com",FirstName = "Stephani", LastName = "Day", Password = "Password1", NickName = "MrsChefCode", DateJoined = DateTime.Parse("2013-11-11"), MemberID =5 , Messages = new List<Message>()}
            };
            //Now I save each member by logging them in and out to hash their password using OWIN -LONNIE
            members.ForEach(s => context.Users.AddOrUpdate(p => p.Email, s));
            foreach (var member in members)
            {
                var user = context.Users.SingleOrDefault(u => u.Email == member.Email);
                if (user == null)
                {
                    var result = userManager.Create(member, member.Password);
                    if (result.Succeeded)
                    {
                        InitializeAuthenticationApplyAppCookie(userManager, member);
                    }
                }
            }
            members.ForEach(s => context.Users.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();

            var topics = new List<Topic>
            {
                new Topic {  Description = "All things food", Messages = new List<Message>()},
                new Topic {  Description = "Everything under the sun and beyond", Messages = new List<Message>()},
                new Topic {  Description = "Things don't add up", Messages = new List<Message>()},
                new Topic {  Description = "Post college life", Messages = new List<Message>()},
                new Topic {  Description = "Before they were professionals", Messages = new List<Message>()}
            };
            topics.ForEach(s => context.Topics.AddOrUpdate(p => p.Description, s));
            context.SaveChanges();

            var messages = new List<Message>
            {
                new Message { MemberID = members.Single(s => s.Email == "sample@email.com").MemberID, 
                    TopicID = topics[0].TopicID, Subject = "I Love Food", Body = "How long you got...", Date = DateTime.Parse("2005-09-01")},

                new Message { MemberID = members.Single(s => s.Email == "example@email.com").MemberID,
                    TopicID = topics[0].TopicID, Subject = "RE:I Love Food", Body = "I Got awhile...", Date = DateTime.Parse("2005-09-02")},

                new Message { MemberID = members.Single(s => s.Email == "lonnieteter@email.com").MemberID,
                    TopicID = topics[0].TopicID, Subject = "RE:I Love Food", Body = "I Got awhile too...", Date = DateTime.Parse("2005-09-03")},

                new Message { MemberID = members.Single(s => s.Email == "parkerdavis@email.com").MemberID,
                    TopicID = topics[1].TopicID, Subject = "I Hate Food", Body = "How long you got...", Date = DateTime.Parse("2005-09-04")},

                new Message { MemberID = members.Single(s => s.Email == "stephaniday@email.com").MemberID,
                    TopicID = topics[1].TopicID, Subject = "RE:I Hate Food", Body = "Save it buddy...", Date = DateTime.Parse("2005-09-05")},

                new Message { MemberID = members.Single(s => s.Email == "sample@email.com").MemberID,
                    TopicID = topics[2].TopicID, Subject = "I Love Science", Body = "How long you got...", Date = DateTime.Parse("2005-09-06")},

                new Message { MemberID = members.Single(s => s.Email == "example@email.com").MemberID,
                    TopicID = topics[3].TopicID, Subject = "I Love Math", Body = "How long you got...", Date = DateTime.Parse("2005-09-07")},

                new Message { MemberID = members.Single(s => s.Email == "lonnieteter@email.com").MemberID,
                    TopicID = topics[4].TopicID, Subject = "I Love My Career", Body = "How long you got...", Date = DateTime.Parse("2005-09-08")},

                new Message { MemberID = members.Single(s => s.Email == "parkerdavis@email.com").MemberID,
                    TopicID = topics[4].TopicID, Subject = "I Loved College", Body = "How long you got...", Date = DateTime.Parse("2005-09-09")},

                new Message { MemberID = members.Single(s => s.Email == "stephaniday@email.com").MemberID,
                    TopicID = topics[4].TopicID, Subject = "I Love Science", Body = "How long you got...", Date = DateTime.Parse("2005-09-10")}
            };
            messages.ForEach(s => context.Messages.AddOrUpdate(p => p.Date, s));

            context.SaveChanges();

            var comments = new List<Comment>()
            {
                new Comment { Message = messages[0], Member = members[2], Date = DateTime.Parse("2005-10-01"), Body = "All Day :)" },
                new Comment { Message = messages[0], Member = members[2], Date = DateTime.Parse("2005-10-02"), Body = "HellO??" },
                new Comment { Message = messages[0], Member = members[0], Date = DateTime.Parse("2005-10-03"), Body = "Sorry I'm Back" },
                new Comment { Message = messages[2], Member = members[1], Date = DateTime.Parse("2005-11-01"), Body = "What's up?" },
                new Comment { Message = messages[0], Member = members[0], Date = DateTime.Parse("2005-11-02"), Body = "Comment on my own post" }
            };
            comments.ForEach(s => context.Comments.AddOrUpdate(p => p.Date, s));
            context.SaveChanges();

            var categories = new List<Category>()
            {
                new Category { Messages = new List<Message>(), Title = "The Future"},
                new Category { Messages = new List<Message>(), Title = "Sustainability"},
                new Category { Messages = new List<Message>(), Title = "Restaurants"}
            };
            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

            AddOrUpdate_Message_Into_Category(context, messages[0], categories[0].Title);
            AddOrUpdate_Message_Into_Category(context, messages[1], categories[1].Title);
            AddOrUpdate_Message_Into_Category(context, messages[2], categories[2].Title);
            AddOrUpdate_Message_Into_Category(context, messages[3], categories[1].Title);
            AddOrUpdate_Message_Into_Category(context, messages[4], categories[1].Title);
            AddOrUpdate_Message_Into_Category(context, messages[5], categories[1].Title);
            AddOrUpdate_Message_Into_Category(context, messages[6], categories[1].Title);
            AddOrUpdate_Message_Into_Category(context, messages[7], categories[1].Title);
            AddOrUpdate_Message_Into_Category(context, messages[8], categories[1].Title);
            AddOrUpdate_Message_Into_Category(context, messages[9], categories[1].Title);

            context.SaveChanges();

            AddOrUpdate_Category_Into_Forum(context, categories[0].Title, forums[0].ForumName);
            AddOrUpdate_Category_Into_Forum(context, categories[1].Title, forums[0].ForumName);
            AddOrUpdate_Category_Into_Forum(context, categories[2].Title, forums[0].ForumName);

            context.SaveChanges();

        }
        void AddOrUpdate_Category_Into_Forum(LocallySourced.DAL.LocallySourcedDB context,
                                                                          string categoryTitle,
                                                                      string forumName)
        {
            var forum = context.Fora.SingleOrDefault(f => f.ForumName == forumName);
            var category = forum.Categories.SingleOrDefault(c => c.Title == categoryTitle);
            if (category == null)
                forum.Categories.Add(context.Categories.Single(ct => ct.Title == categoryTitle));
        }
        void AddOrUpdate_Message_Into_Category(LocallySourced.DAL.LocallySourcedDB context,
                                                                           Message message,
                                                                      string categoryTitle)
        {
            var category = context.Categories.SingleOrDefault(c => c.Title == categoryTitle);
            if(category == null)
            {
                throw new NullReferenceException("category does NOT exist!!");
            }
            var messageByDate = category.Messages.Where(m => m.Date == message.Date);
            if(messageByDate.Count() > 1)
            {
                var single = messageByDate.SingleOrDefault(s => s.MemberID == message.MemberID);
                if(single == null)
                {
                    category.Messages.Add(context.Messages.Single(mes => mes.MemberID == message.MemberID));
                    context.SaveChanges();
                }
            }

            if (messageByDate == null)
            {
                category.Messages.Add(context.Messages.Single(mes => mes.MemberID == message.MemberID));
                context.SaveChanges();
            }
        }
        
        private static void InitializeAuthenticationApplyAppCookie(UserManager<Member> userManager,
            Member member)
        {
            var identity = userManager.CreateIdentity(
               member, DefaultAuthenticationTypes.ApplicationCookie);

            IOwinContext octx = new OwinContext();
            octx.Request.Context.Authentication.SignIn(identity);
            octx.Request.Context.Authentication.SignOut("ApplicationCookie");
        }
        public static void InitializeIdentityForEF(LocallySourced.DAL.LocallySourcedDB db)
        {
            var userManager = new UserManager<Member>(
              new UserStore<Member>(db));

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(db));

            userManager.UserValidator = new UserValidator<Member>(userManager) { AllowOnlyAlphanumericUserNames = false };
            const string userName = "admin@admin.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role "Admin" if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var member = userManager.FindByName(userName);
            if (member == null)
            {
                member = new Member { UserName = userName, Email = userName, NickName = roleName, FirstName = roleName, LastName = roleName, DateJoined = DateTime.Parse("2000-01-01") };
                var result = userManager.Create(member, password);
                InitializeAuthenticationApplyAppCookie(userManager, member);
                result = userManager.SetLockoutEnabled(member.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(member.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(member.Id, role.Name);
            }
        }    
    }
}
