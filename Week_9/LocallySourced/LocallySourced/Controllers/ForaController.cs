using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocallySourced.DAL;
using LocallySourced.Models;

namespace LocallySourced.Controllers
{
    public class ForaController : Controller
    {
        //private LocallySourcedDB db = new LocallySourcedDB();
        IForumRepository repo;

        public ForaController()
        {
            repo = new ForumRepository();
        }

        public ForaController(IForumRepository f)
        {
            repo = f;
        }

        // GET: Fora
        [Authorize]
        public ActionResult Index()
        {
            //var forum = db.Fora.ToList();
            //var cats = (from c in db.Categories
            //            select c).ToList();

            //return View(db.Fora.Include("Categories").ToList());
            return View(repo.GetAllForums());
        }

        // GET: Fora/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = repo.GetForumByID(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // GET: Fora/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForumID,ForumName")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                repo.AddForum(forum);
                return RedirectToAction("Index");
            }

            return View(forum);
        }

        // GET: Fora/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = repo.GetForumByID(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Fora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForumID,ForumName")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateForum(forum);
                return RedirectToAction("Index");
            }
            return View(forum);
        }

        // GET: Fora/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = repo.GetForumByID(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Fora/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteForumById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
