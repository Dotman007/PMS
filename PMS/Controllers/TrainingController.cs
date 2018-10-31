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
    public class TrainingController : Controller
    {
        private PMSContext db = new PMSContext();
        private readonly HttpContext _context = System.Web.HttpContext.Current;
        // GET: /Training/
        public ActionResult Index()
        {
            var trainings = db.Trainings.Include(t => t.Employee).Include(t => t.Prisoner).Include(t => t.TrainingType);
            return View(trainings.ToList());
        }

        // GET: /Training/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }
         public ActionResult Success()
        {
            return View();
        }
        // GET: /Training/Create
        public ActionResult Create()
        {
            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName");
            ViewBag.PrisonerId = new SelectList(db.Prisoners, "PrisonerId", "PrisonerRegNo");
            ViewBag.TrainingTypeId = new SelectList(db.TrainingTypes, "TrainingTypeId", "TrainingTypeName");
            return View();
        }

        // POST: /Training/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TrainingId,TrainingTypeId,Duration,Trained,PrisonerId,EmployeeId")] Training training)
        {
            if (ModelState.IsValid)
            {
                int id = (int)(_context.Session["employee_Id"]);
                training.EmployeeId = id;
                training.Trained = true;
                db.Trainings.Add(training);
                db.SaveChanges();
                return RedirectToAction("Success");
            }

            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", training.EmployeeId);
            ViewBag.PrisonerId = new SelectList(db.Prisoners, "PrisonerId", "PrisonerRegNo", training.PrisonerId);
            ViewBag.TrainingTypeId = new SelectList(db.TrainingTypes, "TrainingTypeId", "TrainingTypeName", training.TrainingTypeId);
            return View(training);
        }

        public ActionResult ViewTraining()
        {
            var training = db.Trainings.Where(j => j.Trained == true).ToList();
            return View(training);
        }

        // GET: /Training/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", training.EmployeeId);
            ViewBag.PrisonerId = new SelectList(db.Prisoners, "PrisonerId", "FirstName", training.PrisonerId);
            ViewBag.TrainingTypeId = new SelectList(db.TrainingTypes, "TrainingTypeId", "TrainingTypeName", training.TrainingTypeId);
            return View(training);
        }

        // POST: /Training/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TrainingId,TrainingTypeId,Duration,Trained,PrisonerId,EmployeeId")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", training.EmployeeId);
            ViewBag.PrisonerId = new SelectList(db.Prisoners, "PrisonerId", "FirstName", training.PrisonerId);
            ViewBag.TrainingTypeId = new SelectList(db.TrainingTypes, "TrainingTypeId", "TrainingTypeName", training.TrainingTypeId);
            return View(training);
        }

        // GET: /Training/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // POST: /Training/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Training training = db.Trainings.Find(id);
            db.Trainings.Remove(training);
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
