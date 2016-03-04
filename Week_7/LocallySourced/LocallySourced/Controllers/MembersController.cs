using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocallySourced.Models;
using PagedList;
using System.Collections;

namespace LocallySourced.Controllers
{
    public class MembersController : Controller
    {
        private LocallySourcedDB db = new LocallySourcedDB();

        // GET: Members
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int max = 6;
            int min = 1;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var members = (from m in db.Users select m).OrderBy(m => m.FirstName);
                           
            if(!String.IsNullOrEmpty(searchString))
            {               
                 var sResult = members.Where(m =>
                    m.FirstName.Contains(searchString)
                    );

                 switch (sortOrder)
                 {
                     case "name_desc":
                         sResult.OrderByDescending(m => m.UserName);
                         break;
                     case "Date":
                         sResult.OrderBy(m => m.DateJoined);
                         break;
                     case "date_desc":
                         sResult.OrderByDescending(m => m.DateJoined);
                         break;
                     default:
                         sResult.OrderBy(m => m.UserName);
                         break;
                 }
                
                 int childPageSize = max;
                 if (sResult.Count() <= childPageSize) { childPageSize = sResult.Count(); }

                 int childPageNumber = (page ?? 1);
                 if (!sResult.Any()) 
                 {
                     childPageSize = min;
                     return View(sResult.ToPagedList(childPageNumber, childPageSize));
                 }

                 return View(sResult.ToPagedList(childPageNumber, childPageSize));
            }
   
            switch (sortOrder)
            {
                case "name_desc":
                    members.OrderByDescending(m => m.UserName);
                    break;
                case "Date":
                    members.OrderBy(m => m.DateJoined);
                    break;
                case "date_desc":
                    members.OrderByDescending(m => m.DateJoined);
                    break;
                default:
                    members.OrderBy(m => m.UserName);
                    break;
            }
            int pageSize = max;
            if (members.Count() <= pageSize) { pageSize = members.Count(); }
            
            int pageNumber = (page ?? 1);
            if (!members.Any()) 
            {
                pageSize = min;
                return View(members.ToPagedList(pageNumber, pageSize)); 
            }
            return View(members.ToPagedList(pageNumber, pageSize));
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Users.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,UserName,FirstName,LastName,Email,Password,DateJoined")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.DateJoined = DateTime.Now;
                db.Users.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Users.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,UserName,FirstName,LastName,Email,Password")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Users.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Users.Find(id);
            db.Users.Remove(member);
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
    }
}
