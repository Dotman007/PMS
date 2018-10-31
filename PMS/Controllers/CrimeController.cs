using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMS.Models;
using PMS.DAL;

namespace PMS.Controllers
{
    public class CrimeController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: /Crime/
        public ActionResult Index()
        {
            return View(db.Crime.ToList());
        }

        // GET: /Crime/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crime crime = db.Crime.Find(id);
            if (crime == null)
            {
                return HttpNotFound();
            }
            return View(crime);
        }

        // GET: /Crime/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Crime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CrimeId,CrimeName")] Crime crime)
        {
            if (ModelState.IsValid)
            {
                db.Crime.Add(crime);
                db.SaveChanges();
                return RedirectToAction("Index", "Role");
            }

            return View(crime);
        }

        // GET: /Crime/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crime crime = db.Crime.Find(id);
            if (crime == null)
            {
                return HttpNotFound();
            }
            return View(crime);
        }

        // POST: /Crime/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CrimeId,CrimeName")] Crime crime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crime);
        }

        // GET: /Crime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crime crime = db.Crime.Find(id);
            if (crime == null)
            {
                return HttpNotFound();
            }
            return View(crime);
        }

        // POST: /Crime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crime crime = db.Crime.Find(id);
            db.Crime.Remove(crime);
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
