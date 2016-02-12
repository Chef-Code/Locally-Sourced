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
    public class ForaController : Controller
    {
        private LocallySourcedDB db = new LocallySourcedDB();

        // GET: Fora
        public ActionResult Index()
        {
            return View(GetMessageTopicMembers(0));
        }

        // GET: Fora/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Fora.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // GET: Fora/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Fora.OrderBy(m => m.MessageItem.TopicItem.Category), "TopicID", "Category");
            ViewBag.UserNames = new SelectList(db.Fora.OrderBy(m => m.MessageItem.From.UserName), "MemberID", "UserName");

            return View();
        }

        // POST: Fora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForumID")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Fora.Add(forum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forum);
        }

        // GET: Fora/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Fora.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Fora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForumID")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forum);
        }

        // GET: Fora/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Fora.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Fora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = db.Fora.Find(id);
            db.Fora.Remove(forum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

         private List<ForumViewModel> GetMessageTopicMembers(int? messageID)
        {
            var posts = new List<ForumViewModel>();

            var fora = from forum in db.Fora.Include("Messages")
                       select forum;
           
            var messageMembers = from message in db.Messages.Include("Members")
                          select message;
           
            var messageTopics = from message in db.Messages.Include("Topics")
                          select message;

           foreach (Forum f in fora)
           {
               foreach(Message mes in f.Messages)
               {
                   if(mes.MessageID == messageID || 0 == messageID)
                   {
                       var forumVM = new ForumViewModel();
                       forumVM.User = mes.From;
                       forumVM.Category = mes.TopicItem;
                       forumVM.Message = mes;
                       posts.Add(forumVM);
                   }
               }
           }
           return posts;
         
        }
         
    }
}
