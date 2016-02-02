using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskVer2.Util;

namespace TaskVer2.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        public ActionResult Index()
        {
            NewsUtil.UpdateNews();
            ViewBag.Text = Models.Feed.AddFeed();
            return View();
            
        }
    }
}