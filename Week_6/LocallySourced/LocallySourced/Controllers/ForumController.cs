using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocallySourced.Models;

namespace LocallySourced.Controllers
{
    public class ForumController : Controller
    {
        private LocallySourcedDB db = new LocallySourcedDB();

        // GET: Forum
        public ActionResult Index(int? ids)
        {
            var messageVMs = new List<ForumViewModel>();
            var posts = db.Messages.Include(u => u.Member).Include(m => m.Topic);

            messageVMs = GetForumVMs(posts);

            if (GetForumViewModels(0).Count == 0)
            {
                return View(messageVMs);
            }
            else
            {
                return View(GetForumViewModels(ids));
            }           
        }

        // GET: Forum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumViewModel forumVM = GetForumViewModel(id);
            if (forumVM == null)
            {
                return HttpNotFound();
            }
            return View(forumVM);
        }

        // GET: Forum/Create
        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.Users, "MemberID", "UserName");
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Category");
            return View();
        }

        // POST: Forum/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageID,MemberID,TopicID,Subject,Body,Date")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberID = new SelectList(db.Users, "MemberID", "UserName", message.MemberID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Category", message.TopicID);
            return View(message);
        }

        // GET: Forum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Users, "MemberID", "UserName", message.MemberID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Category", message.TopicID);
            return View(message);
        }

        // POST: Forum/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageID,MemberID,TopicID,Subject,Body,Date")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.Users, "MemberID", "UserName", message.MemberID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Category", message.TopicID);
            return View(message);
        }

        // GET: Forum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Forum/Search
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        //POST: Forum/Search
        [HttpPost]
        public ActionResult Search(string searchTerm)
        {
            var forumVMs = new List<ForumViewModel>();

            var posts = db.Messages.Include("Users").Include(m => m.Topics);
                             
            forumVMs = GetForumVMs(posts.Where(p => p.Subject.Contains(searchTerm)));
      
            if(forumVMs.Count == 1)
            {
                return View("Details", forumVMs[0]);
            }
            else
            {
                return View("Index", forumVMs);
            }         
        }
        //Terminates connection string to the Database
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private List<ForumViewModel> GetForumVMs(IQueryable<Message> posts)
        {
            var forumVMs = new List<ForumViewModel>();
            
            foreach (var p in posts)
            {
                var members = (from m in db.Users
                               where p.MemberID == m.MemberID
                               select m).FirstOrDefault();

                var topics = (from t in db.Topics
                              where t.TopicID == p.TopicID
                              select t).FirstOrDefault();

                forumVMs.Add(new ForumViewModel
                {
                    MessageID = p.MessageID,
                    Body = p.Body,
                    Category = topics.Category,
                    Date = p.Date,
                    Subject = p.Subject,
                    NickName = members.NickName
                });
            }
            return forumVMs;
        }
        private List<ForumViewModel> GetForumViewModels(int? ids)
        {
            var forumVMs = new List<ForumViewModel>();
            var posts = db.Messages.Include("Users").Include(m => m.Topics);

            foreach(Message p in posts)
            {
                if (p.MessageID == ids)
                {
                    var forumVM = new ForumViewModel();
                    forumVM.MessageID = p.MessageID;
                    forumVM.Subject = p.Subject;
                    forumVM.Body = p.Body;
                    forumVM.Date = p.Date;

                    foreach (Member m in p.Users)
                    {
                        forumVM.NickName = m.NickName;

                        foreach (Topic t in p.Topics)
                        {
                            forumVM.Category = t.Category;
                            forumVMs.Add(forumVM);
                        }
                    }
                }
                
            }
            return forumVMs;
        }
        private ForumViewModel GetForumViewModel(int? forumID)
        {
            var forumVM = (from p in db.Messages
                           join m in db.Users on p.MemberID equals m.MemberID
                           join t in db.Topics on p.MemberID equals t.TopicID
                           where p.MessageID == forumID
                           select new ForumViewModel
                           {
                               MessageID = p.MessageID,
                               Body = p.Body,
                               Category = t.Category,
                               Date = p.Date,
                               Subject = p.Subject,
                               NickName = m.UserName
                           }).FirstOrDefault();

            return forumVM;
        }
    }
}
