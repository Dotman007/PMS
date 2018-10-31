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
    public class PrisonerController : Controller
    {
        private PMSContext db = new PMSContext();
        private HttpContext _context = System.Web.HttpContext.Current;



        [HttpGet]
        public ActionResult AddCourtCase()
        {
            ViewBag.PrisonerRegNo = new SelectList(db.Prisoners, "PrisonerId", "PrisonerRegNo");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourtCase([Bind(Include = "CaseId,CaseName,CaseDate,PrisonerId,JudgeName,CourtName,EmployeeId")] Case @case, int? prisonerId)
        {
            if (ModelState.IsValid)
            {
                Prisoner prisoner = db.Prisoners.Find(prisonerId);
                int id = (int)(_context.Session["employee_Id"]);
                @case.EmployeeId = id;
                @case.PrisonerId = prisoner.PrisonerId;



                @case.Scheduled = true;

                db.Cases.Add(@case);
                db.SaveChanges();
                return RedirectToAction("Success", "Prisoner");
            }

            ViewBag.PrisonerRegNo = new SelectList(db.Prisoners, "PrisonerId", "PrisonerRegNo", @case.Prisoner.PrisonerRegNo);
            return View(@case);
        }

        public ViewResult Success()
        {


            return View();
        }
        //Get Untrained Prisoner
        public ActionResult GetUntrainedPrisoner()
        {
            var untrained = db.Prisoners.Where(j => j.TrainingScheduled == false).ToList();
            return View(untrained);
        }

        //Shedule Prisoner for Training
        [HttpGet]
        public ActionResult TrainPrisoner()
        {
            ViewBag.TrainingTypeId = new SelectList(db.TrainingTypes, "TrainingTypeId", "TrainingTypeName");
            
            return View();
        }


        [HttpPost]
        public ActionResult TrainPrisoner(Prisoner prisoners, int? trainingType, int? prisonerId)
        {
            Prisoner prisoner = db.Prisoners.Find(prisonerId);
            if(prisoners.TrainingScheduled != true)
            {
                
                int id = (int)(_context.Session["employee_Id"]);
                prisoners.EmployeeId = id;
                prisoners.TrainingScheduled = true;
                prisoner.TrainingTypeId = trainingType;

                db.SaveChanges();
                ViewBag.Success = "Training Scheduled Successfully";
            }
            ViewBag.TrainingTypeId = new SelectList(db.TrainingTypes, "TrainingTypeId", "TrainingTypeName", prisoner.TrainingTypeId);

           
            return View();
        }

        public string GenerateRegNo(int id)
        {
            var prisoner = db.Prisoners.FirstOrDefault(m => m.PrisonerId == id);
            var rand = new Random();
            var Nrand = "PRN" + rand.Next(1000000, 5000000);
            return Nrand.ToString();

        }
        // GET: /Prisoner/
        public ActionResult Index()
        {
            var prisoners = db.Prisoners.Include(p => p.Crime).Include(p => p.Employee).Include(p => p.Qualification).Include(p => p.State);
            return View(prisoners.ToList());
        }

        // GET: /Prisoner/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prisoner prisoner = db.Prisoners.Find(id);
            if (prisoner == null)
            {
                return HttpNotFound();
            }
            return View(prisoner);
        }
       
        public ActionResult RegisterPrisoner()
        {
            ViewBag.CrimeId = new SelectList(db.Crime, "CrimeId", "CrimeName");
            
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName");
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
            return View();
        }
         [HttpPost]
        public ActionResult RegisterPrisoner([Bind(Include = "PrisonerId,FirstName,LastName,OtherName,Gender,QualificationId,EmployeeId,StateId,LocalGovId,Address,Email,PhoneNo,NextOfKin,MotherName,FatherName,MotherAddress,FatherAddress,Genotype,BloodGroup,CrimeId,PrisonerRegNo")] Prisoner prisoner)
        {
            if (ModelState.IsValid)
            {
                int id = (int)(_context.Session["employee_Id"]);
                prisoner.EmployeeId = id;
                prisoner.PrisonerRegNo = GenerateRegNo(prisoner.PrisonerId);
                db.Prisoners.Add(prisoner);
                db.SaveChanges();
                ViewBag.Success = "Prisoner Record Saved Successfully";
               
            }

            ViewBag.CrimeId = new SelectList(db.Crime, "CrimeId", "CrimeName", prisoner.CrimeId);
            
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName", prisoner.QualificationId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", prisoner.StateId);
            return View(prisoner);
           
        }

        public ActionResult ViewPrisoners()
         {
             int id = (int)(_context.Session["employee_Id"]);
             var prisoners = db.Prisoners.Include(p => p.Crime).Include(p => p.Employee).Include(p => p.Qualification).Include(p => p.State).Where(p=>p.EmployeeId == id).ToList();
             return View(prisoners);
         }


        public ActionResult ScheduleCase()
        {
           
            return View();
        }


        //[HttpPost]

        //public ActionResult ScheduleCase([Bind(Include = "CaseId,CaseName,CaseDate,PrisonerId,JudgeName,CourtName,EmployeeId,CaseTime")] Case @case, int? prisonerId, Prisoner prisoner)
        //{
        //    if (ModelState.IsValid)
        //    {
              
        //        int id = (int)(_context.Session["employee_Id"]);
        //        @case.EmployeeId = id;
        //        @case.PrisonerId = prisonerId;
        //        @case.CaseDate = DateTime.Now;
        //        @case.Scheduled = true;
                
        //        db.Cases.Add(@case);
        //        db.SaveChanges();
        //        return RedirectToAction("Success","Case");
        //    }


        //    return View(@case);
        //}

        [Authorize(Roles = "Admin")]
        public ActionResult ViewAllPrisoners()
        {
            
            var prisoners = db.Prisoners.Include(p => p.Crime).Include(p => p.Employee).Include(p => p.Qualification).Include(p => p.State).ToList();
            return View(prisoners);
        }

        // GET: /Prisoner/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CrimeId = new SelectList(db.Crime, "CrimeId", "CrimeName");
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName");
        //    ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName");
            
        //    ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
        //    return View();
        //}

        // POST: /Prisoner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PrisonerId,FirstName,LastName,OtherName,Gender,QualificationId,EmployeeId,StateId,LocalGovId,Address,Email,PhoneNo,NextOfKin,MotherName,FatherName,MotherAddress,FatherAddress,Genotype,BloodGroup,CrimeId,PrisonId")] Prisoner prisoner)
        {
            if (ModelState.IsValid)
            {
                db.Prisoners.Add(prisoner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CrimeId = new SelectList(db.Crime, "CrimeId", "CrimeName", prisoner.CrimeId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", prisoner.EmployeeId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName", prisoner.QualificationId);
           
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", prisoner.StateId);
            return View(prisoner);
        }


      

        //Add Prisoner To Training
        [HttpPost]
        public ActionResult AddToTraining([Bind(Include = "TrainingId,TrainingTypeId,Duration,RegistrationDate,EndDate,PrisonerId")] Training training)
        {

            if (ModelState.IsValid)
            {
                int id = (int)(_context.Session["employee_Id"]);

                training.EmployeeId = id;
                
                db.Trainings.Add(training);
                db.SaveChanges();
                ViewBag.Success = "The Prisoner have been added to the Training Successfully";
            }
            return View(training);
        }

        public ViewResult ViewPrisonerss()
        {
            var getAll = db.Prisoners.ToList();
            return View(getAll);
        }

        public ViewResult ViewTrainingProgress()
        {
            var getAll = db.Trainings.ToList();
            return View(getAll);
        }

        // GET: /Prisoner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prisoner prisoner = db.Prisoners.Find(id);
            if (prisoner == null)
            {
                return HttpNotFound();
            }
            ViewBag.CrimeId = new SelectList(db.Crime, "CrimeId", "CrimeName", prisoner.CrimeId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", prisoner.EmployeeId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName", prisoner.QualificationId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", prisoner.StateId);
            return View(prisoner);
        }

        // POST: /Prisoner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PrisonerId,FirstName,LastName,OtherName,Gender,QualificationId,EmployeeId,StateId,LocalGovId,Address,Email,PhoneNo,NextOfKin,MotherName,FatherName,MotherAddress,FatherAddress,Genotype,BloodGroup,CrimeId")] Prisoner prisoner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prisoner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CrimeId = new SelectList(db.Crime, "CrimeId", "CrimeName", prisoner.CrimeId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", prisoner.EmployeeId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName", prisoner.QualificationId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", prisoner.StateId);
            return View(prisoner);
        }

        // GET: /Prisoner/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prisoner prisoner = db.Prisoners.Find(id);
            if (prisoner == null)
            {
                return HttpNotFound();
            }
            return View(prisoner);
        }

        // POST: /Prisoner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prisoner prisoner = db.Prisoners.Find(id);
            db.Prisoners.Remove(prisoner);
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
