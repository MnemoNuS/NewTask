using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskVer2.Models;

namespace TaskVer2.Controllers
{
    public class ChanelsController : Controller
    {
        private dataContext db = new dataContext();

        // GET: Chanels
        public ActionResult Index()
        {
            return View(db.Chanel.ToList());
            
        }

        // GET: Chanels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chanel chanel = db.Chanel.Find(id);
            if (chanel == null)
            {
                return HttpNotFound();
            }
            return View(chanel);
        }

        // GET: Chanels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chanels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChanelID,name,link")] Chanel chanel)
        {
            if (ModelState.IsValid)
            {
                db.Chanel.Add(chanel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chanel);
        }

        // GET: Chanels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chanel chanel = db.Chanel.Find(id);
            if (chanel == null)
            {
                return HttpNotFound();
            }
            return View(chanel);
        }

        // POST: Chanels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChanelID,name,link")] Chanel chanel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chanel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chanel);
        }

        // GET: Chanels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chanel chanel = db.Chanel.Find(id);
            if (chanel == null)
            {
                return HttpNotFound();
            }
            return View(chanel);
        }

        // POST: Chanels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chanel chanel = db.Chanel.Find(id);
            db.Chanel.Remove(chanel);
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
