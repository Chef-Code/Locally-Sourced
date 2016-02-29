using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocallySourced.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your about page";

            return View();
        }

        public ActionResult History()
        {
            ViewBag.Message = "Your History page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If you would like to know more about Locally Sourced " +
                "or would like to become a community member or even just have a question or concern " +
                "please don't hesitate to contact us.  Happy coding, Chow!";

            return View();
        }
    }
}