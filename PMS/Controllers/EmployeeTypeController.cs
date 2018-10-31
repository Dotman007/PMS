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
    public class EmployeeTypeController : Controller
    {
        private PMSContext db = new PMSContext();

        // GET: /EmployeeType/
        public ActionResult Index()
        {
            return View(db.EmployeeTypes.ToList());
        }

        // GET: /EmployeeType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeetype = db.EmployeeTypes.Find(id);
            if (employeetype == null)
            {
                return HttpNotFound();
            }
            return View(employeetype);
        }

        // GET: /EmployeeType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EmployeeType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EmployeeTypeId,EmployeeTypeName")] EmployeeType employeetype)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeTypes.Add(employeetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeetype);
        }

        // GET: /EmployeeType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeetype = db.EmployeeTypes.Find(id);
            if (employeetype == null)
            {
                return HttpNotFound();
            }
            return View(employeetype);
        }

        // POST: /EmployeeType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="EmployeeTypeId,EmployeeTypeName")] EmployeeType employeetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeetype);
        }

        // GET: /EmployeeType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeetype = db.EmployeeTypes.Find(id);
            if (employeetype == null)
            {
                return HttpNotFound();
            }
            return View(employeetype);
        }

        // POST: /EmployeeType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeType employeetype = db.EmployeeTypes.Find(id);
            db.EmployeeTypes.Remove(employeetype);
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
