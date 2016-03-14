using LocallySourced.DAL;
using LocallySourced.Models;
using LocallySourced.ModelViews;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocallySourced.Controllers
{
    public class AuthController : Controller
    {
        private LocallySourcedDB db = new LocallySourcedDB();

        UserManager<Member> userManager = new UserManager<Member>(
               new UserStore<Member>(new LocallySourcedDB()));

        //IOwinContext owinCtx;
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }
        [AllowAnonymous]
        //
        // GET: /Auth/Login/
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            //var user = userManager.Find(model.Email, model.Password);
            var user2 = userManager.FindByEmail(model.Email);
            var user = userManager.Find(user2.UserName, model.Password);

            if (user != null)
            {
                var identity = userManager.CreateIdentity(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authentication failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new Member
            {
                UserName = model.Email,
                NickName = model.NickName,
                Email = model.Email,
                DateJoined = DateTime.Now,
                FirstName = model.FirstName,
                LastName = model.LastName  
            };

            var result = userManager.Create(user, model.Password);

            if (result.Succeeded)
            {
                SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        private void SignIn(Member user) //was private
        {
            var identity = userManager.CreateIdentity(
                user, DefaultAuthenticationTypes.ApplicationCookie);

            GetAuthenticationManager().SignIn(identity);
        }

        private IAuthenticationManager GetAuthenticationManager() //was private
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: /Roles/Create
        [Authorize(Roles = "Admin")]
        public ActionResult CreateRole()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateRole(FormCollection collection)
        {
            try
            {
                db.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                db.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string roleName)
        {
            var thisRole = db.Roles.Where(r => r.Name.Equals(
                roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            db.Roles.Remove(thisRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Roles/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult EditRole(string roleName)
        {
            var thisRole = db.Roles.Where(r => r.Name.Equals(
                roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(IdentityRole role)
        {
            try
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList()
                .Select(rr =>
                    new SelectListItem 
                    { 
                        Value = rr.Name.ToString(), 
                        Text = rr.Name 
                    })
                    .ToList();
               
            ViewBag.Roles = list;

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string userName, string roleName)
        {
            Member user = db.Users.Where(u => u.UserName.Equals(
                userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            userManager.AddToRole(user.Id, roleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList()
                .Select(rr => 
                    new SelectListItem 
                    { 
                        Value = rr.Name.ToString(), 
                        Text = rr.Name 
                    })
                    .ToList();

            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                Member user = db.Users.Where(u => u.UserName.Equals(
                    userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                ViewBag.RolesForThisUser = userManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = db.Roles.OrderBy(r => r.Name).ToList()
                    .Select(rr => 
                        new SelectListItem 
                        { 
                            Value = rr.Name.ToString(), 
                            Text = rr.Name 
                        })
                        .ToList();

                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }
    }
}