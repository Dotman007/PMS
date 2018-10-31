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
    public class TrainingTypeController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: /TrainingType/
        public ActionResult Index()
        {
            return View(db.TrainingTypes.ToList());
        }

        // GET: /TrainingType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingType trainingtype = db.TrainingTypes.Find(id);
            if (trainingtype == null)
            {
                return HttpNotFound();
            }
            return View(trainingtype);
        }

        // GET: /TrainingType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TrainingType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TrainingTypeId,TrainingTypeName")] TrainingType trainingtype)
        {
            if (ModelState.IsValid)
            {
                db.TrainingTypes.Add(trainingtype);
                db.SaveChanges();
                return RedirectToAction("Index","Role");
            }

            return View(trainingtype);
        }

        // GET: /TrainingType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingType trainingtype = db.TrainingTypes.Find(id);
            if (trainingtype == null)
            {
                return HttpNotFound();
            }
            return View(trainingtype);
        }

        // POST: /TrainingType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TrainingTypeId,TrainingTypeName")] TrainingType trainingtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainingtype);
        }

        // GET: /TrainingType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingType trainingtype = db.TrainingTypes.Find(id);
            if (trainingtype == null)
            {
                return HttpNotFound();
            }
            return View(trainingtype);
        }

        // POST: /TrainingType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingType trainingtype = db.TrainingTypes.Find(id);
            db.TrainingTypes.Remove(trainingtype);
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
