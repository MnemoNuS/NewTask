using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using TaskVer2.Models;

namespace TaskVer2.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        dataContext db = new dataContext();



        public ActionResult Index()
        {
            //ViewBag.Categories = Categories;
            ViewBag.Category = "All news";
            ViewBag.Cagegories = db.Category.ToList();
            return View(db.Category.ToList());
        }

        // GET: News/NewsByCategory/5
        //public ActionResult NewsByCategory(int? id)
        //{
        //    List<News> News = db.News.ToList();// получаем все объекты News из базы
        //    List<Category> Category = db.Category.ToList();
        //    List<News> selectedNews = new List<News>(); // создаем список для отобраных новостей
        //    string[] CatArray = new string[Category.Count];
        //    int i = 0;

        //    foreach (News n in News)
        //    {
        //        if (n.CategoryID == id + 1)
        //        {
        //            selectedNews.Add(n);

        //        }
        //    }
        //    foreach (Category text in Category)
        //    {
        //        CatArray[i] = text.category;
        //        i++;
        //    }
        //    string Cat = Category.ElementAt((int)id).category;
        //    ViewBag.CatArray = CatArray;
        //    ViewBag.Cat = Cat;
        //    ViewBag.selectedNews = selectedNews;
        //    return View();
        //}
        public ActionResult ByCategory(int? id)
        {

            ViewBag.Category = db.Category.Find(id).category;
            ViewBag.Cagegories = db.Category.ToList();
            return View(db.Category.Find(id));
        }
        //public ActionResult NewIndex()
        //{
        //    //ViewBag.Categories = Categories;
        //    return View(db.Category.ToList());
        //}
        public RedirectToRouteResult UpdateNews()
        {
            Util.NewsUtil.UpdateNews();
            return RedirectToAction("Index");
        }
        public ActionResult NewsLog()
        {
            FileStream NewStream = new FileStream("../NewsLog.txt", FileMode.Open, FileAccess.Read);
            StreamReader ReadSream = new StreamReader(NewStream);
            List<string> LogFile = new List<string>();
            while (!ReadSream.EndOfStream)
            {
                LogFile.Add(ReadSream.ReadLine());
            }
            ReadSream.Close();
            NewStream.Close();
            return View(LogFile);
        }
        public ActionResult NavigationPanel()//рисует панель со ссылками
        {
            return PartialView();
        }
    }
}

