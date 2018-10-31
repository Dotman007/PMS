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
    public class CaseController : Controller
    {
        private PMSContext db = new PMSContext();
        private readonly HttpContext _context = System.Web.HttpContext.Current;
        // GET: /Case/
        public ActionResult Index()
        {
            var cases = db.Cases.Include(e => e.Employee).Include(e => e.Prisoner);
            return View(cases.ToList());
        }



        public ActionResult ViewAllCases()
        {
            var cases = db.Cases.Include(e => e.Employee).Include(e => e.Prisoner);
            return View(cases.ToList());
        }

        // GET: /Case/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        public ActionResult ViewCases()
        {
            var cases = db.Cases.Include(e => e.Employee).Include(e => e.Prisoner).Where(j => j.Scheduled == true);
            return View(cases);
        }

        public ActionResult Success()
        {
            return View();
        }
        // GET: /Case/Create
        public ActionResult Create()
        {

            ViewBag.PrisonerId = new SelectList(db.Prisoners, "PrisonerId", "PrisonerRegNo");
            return View();
        }

        // POST: /Case/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CaseId,CaseName,CaseDate,PrisonerId,JudgeName,CourtName,CaseDescription,Scheduled,EmployeeId")] Case @case)
        {
            if (ModelState.IsValid)
            {
                int id = (int)(_context.Session["employee_Id"]);
                @case.EmployeeId = id;
                @case.Scheduled = true;
                db.Cases.Add(@case);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            ViewBag.PrisonerId = new SelectList(db.Prisoners, "PrisonerId", "PrisonerRegNo", @case.Prisoner.PrisonerRegNo);
            return View(@case);
        }

        // GET: /Case/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", @case.EmployeeId);
            ViewBag.PrisonerId = new SelectList(db.Prisoners, "PrisonerId", "FirstName", @case.PrisonerId);
            return View(@case);
        }

        // POST: /Case/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CaseId,CaseName,CaseDate,PrisonerId,JudgeName,CourtName,CaseDescription,Scheduled,EmployeeId")] Case @case)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@case).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", @case.EmployeeId);
            ViewBag.PrisonerId = new SelectList(db.Prisoners, "PrisonerId", "FirstName", @case.PrisonerId);
            return View(@case);
        }

        // GET: /Case/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // POST: /Case/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Case @case = db.Cases.Find(id);
            db.Cases.Remove(@case);
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
