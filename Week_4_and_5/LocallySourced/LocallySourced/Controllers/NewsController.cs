using LocallySourced.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocallySourced.Controllers
{
    public class NewsController : Controller
    {
        
        // GET: News
        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult Archive()
        {
            News yestNews = new News();
            yestNews.Date = DateTime.Now;
            DateTime y = (yestNews.Date.AddDays(-1));
            yestNews.NewsID = 2;
            yestNews.HeadLine = "Yesterday was also Epic!!!";
            yestNews.Article = "Yesterday was " + y.DayOfWeek + " the " + News.Ordinal((int)y.Day) + ", and a lot of stuff happened that" + y.DayOfWeek;

            return View(yestNews);
        }

        public ActionResult TodaysNews()
        {             
            News todaysNews = new News();
            todaysNews.Date = DateTime.Now;
            DateTime t = todaysNews.Date;
            todaysNews.NewsID = 1;
            todaysNews.HeadLine = "Today is " + t.Date.ToShortDateString() + " and it was Epic!!!";
            todaysNews.Article = "Today is " + t.DayOfWeek + " the " + News.Ordinal((int)t.Day) + ", and a lot of stuff happened";

            return View(todaysNews);
        }
    }
}