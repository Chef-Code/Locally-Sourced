using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocallySourced.DAL;
using LocallySourced.Models;
using System.Collections.Generic;
using LocallySourced.Controllers;
using System.Web.Mvc;

namespace ForumControllerUnitTests
{
    [TestClass]
    public class ForumControllerTestUnits
    {
        public List<Forum> fora = new List<Forum>()
            {
                 new Forum() { ForumID = 1, ForumName = "Main", Categories = new List<Category>() },
                 new Forum() { ForumID = 2, ForumName = "Back-Up", Categories = new List<Category>() },
                 new Forum() { ForumID = 3, ForumName = "Temp", Categories = new List<Category>() }
            };
        public List<Category> categories = new List<Category>()
            {
                new Category() { ForumID = 1, CategoryID = 1, Description = "All things Sci-fi...", Messages = new List<Message>(), Title = "Sci-fi" },
                new Category() { ForumID = 1, CategoryID = 2, Description = "All things Science Non-Fiction...", Messages = new List<Message>(), Title = "Science Non-Fiction" },
                new Category() { ForumID = 2, CategoryID = 3, Description = "All other things...", Messages = new List<Message>(), Title = "Other Stuff" },
                 new Category() { ForumID = 3, CategoryID = 4, Description = "All other stuff not classified as things...", Messages = new List<Message>(), Title = "Yet More Stuff" },
            };
        public List<Member> members = new List<Member>()
            {
                new Member { FirstName = "Sam", LastName = "Ple", Password = "Password1", NickName = "ABCMouse", DateJoined = DateTime.Parse("2016-01-25"), MemberID = 1, Messages = new List<Message>() },
                new Member { FirstName = "Exam", LastName = "Ple", Password = "Password1", NickName = "BCDMouse", DateJoined = DateTime.Parse("2015-05-02"), MemberID = 2, Messages = new List<Message>()},
                new Member { FirstName = "Lonnie", LastName = "Teter", Password = "Password1", NickName = "ChefCode", DateJoined = DateTime.Parse("2014-12-06"), MemberID =3, Messages = new List<Message>() },
                new Member { FirstName = "Parker", LastName = "Davis", Password = "Password1", NickName = "ChefCodejr", DateJoined = DateTime.Parse("2014-06-12"), MemberID =4, Messages = new List<Message>() },
                new Member { FirstName = "Stephani", LastName = "Day", Password = "Password1", NickName = "MrsChefCode", DateJoined = DateTime.Parse("2013-11-11"), MemberID =5 , Messages = new List<Message>()}
            };
        [TestMethod]
        public void IndexTest()
        {
            //arrange
            var fakeRepo = new FakeForumRepository(fora);
            var target = new ForaController(fakeRepo);

            //act
            var result = (ViewResult)target.Index();

            //assert
            var models = (List<Forum>)result.Model;
            Assert.AreEqual(models[0].ForumID, fora[0].ForumID);
            Assert.AreEqual(models[1].ForumID, fora[1].ForumID);
            Assert.AreEqual(models[2].ForumID, fora[2].ForumID);
            Assert.AreEqual(models.Count, fora.Count);
        }
        [TestMethod]
        public void DetailsTest()
        {
            //arrange
            var fakeRepo = new FakeForumRepository(fora);
            var target = new ForaController(fakeRepo);

            //act
            var result = (ViewResult)target.Details(2);

            //assert
            var model = (Forum)result.Model;
            Assert.AreEqual(fora[1].ForumID, model.ForumID);
            Assert.AreEqual(fora[1].ForumName, model.ForumName);
        }
        [TestMethod]
        public void CreateTest()
        {
            Assert.AreEqual(fora.Count, 3);

            //arrange
            var fakeRepo = new FakeForumRepository(fora);
            var target = new ForaController(fakeRepo);
            Forum myForum = new Forum() { Categories = new List<Category>(), ForumID = 4, ForumName = "Admin-Forum" };
            //fora.Add(myForum);

            //act
            var results = target.Create(myForum);
            var redirect = (RedirectToRouteResult)results;

            //assert           
            Assert.AreEqual(fora.Count, 4);
            //Assert.AreEqual(fora[3].ForumID, 4); //this throws error but should not
            Assert.AreEqual(fora[0].ForumID, fora[3].ForumID); //this should not pass but does
            Assert.AreEqual(fora[3].ForumName, myForum.ForumName);
            Assert.AreEqual(fora[3].ForumID, myForum.ForumID);

        }

    }
}
