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
        public ActionResult Index()
        {
           /* var posts = new List<ForumViewModel>();
            var messages = db.Messages.Include(m => m.Member).Include(m => m.Topic);

            foreach (Message m in messages)
            {
                var postVM = new ForumViewModel();
                postVM.Body = m.Body;
                postVM.Category = m.Topic.Category;
                postVM.Date = m.Date;
                postVM.MessageID = m.MessageID;
                postVM.Subject = m.Subject;
                posts.Add(postVM);
            }

            if(GetForumViewModels(0).Count == 0)
            {
                return View(posts);
            }
            else
            {*/
                 return View(GetForumViewModels(0));
            
           
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
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "UserName");
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Category");
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "UserName", message.MemberID);
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
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "UserName", message.MemberID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Category", message.TopicID);
            return View(message);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "UserName", message.MemberID);
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
            List<ForumViewModel> forumVMs = new List<ForumViewModel>();
            var posts = (from p in db.Messages
                         where p.Subject.Contains(searchTerm)
                         select p).ToList();

            foreach(var p in posts)
            {
                var members = (from m in db.Members
                               where m.MemberID == p.MemberID
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
                        UserName = members.UserName
                    });
            }

            if(forumVMs.Count == 1)
            {
                return View("Details", forumVMs[0]);
            }
            else
            {
                return View("Index", forumVMs);
            }         
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private List<ForumViewModel> GetForumViewModels(int? forumIDs)
        {
            var forumVMs = new List<ForumViewModel>();
            var posts = db.Messages.Include(m => m.Members).Include(m => m.Topics);

            foreach(Message p in posts)
            {
                var forumVM = new ForumViewModel();
                forumVM.MessageID = p.MessageID;
                forumVM.Subject = p.Subject;
                forumVM.Body = p.Body;
                forumVM.Date = p.Date;

                foreach(Member m in p.Members)
                {
                    forumVM.UserName = m.UserName;

                    foreach(Topic t in p.Topics)
                    {                          
                            forumVM.Category = t.Category;                          
                            forumVMs.Add(forumVM);
                                              
                    }
                }
            }
            return forumVMs;


        }
        private ForumViewModel GetForumViewModel(int? forumID)
        {
            var forumVM = (from p in db.Messages
                           join m in db.Members on p.MemberID equals m.MemberID
                           join t in db.Topics on p.MemberID equals t.TopicID
                           where p.MessageID == forumID
                           select new ForumViewModel
                           {
                               MessageID = p.MessageID,
                               Body = p.Body,
                               Category = t.Category,
                               Date = p.Date,
                               Subject = p.Subject,
                               UserName = m.UserName
                           }).FirstOrDefault();

            return forumVM;
        }
    }
}
