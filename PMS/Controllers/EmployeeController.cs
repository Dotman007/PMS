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
    public class EmployeeController : Controller
    {
        private PMSContext db = new PMSContext();

        private readonly HttpContext _context = System.Web.HttpContext.Current;


        public ActionResult Dashboard()
        {
            int id = (int)(_context.Session["employee_Id"]);
            var employee = db.Employees.Find(id);
            return View(employee);
        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            if (userName == "" && password == "")
            {
                ViewBag.RegNoEmpty = "The Reg No Field is Empty";
                ViewBag.PasswordEmpty = "The Password Field is Empty";
                return View("Login");
            }
            if (userName == "")
            {
                ViewBag.RegNoEmpty = "The Reg No Field is Empty";

                return View("Login");
            }
            if (password == "")
            {

                ViewBag.PasswordEmpty = "The Password Field is Empty";
                return View("Login");
            }

            var employee = db.Employees.SingleOrDefault(m => m.UserName == userName && m.Password == password);
            if (employee != null)
            {
                ViewBag.PasswordEmpty = null;
                ViewBag.PasswordEmpty = null;
                ViewBag.ApplicantNotExist = null;

                _context.Session["user_Name"] = employee.UserName ;
                _context.Session["employee_Id"] = employee.EmployeeId;
                _context.Session["first_Name"] = employee.FirstName;
                _context.Session["last_Name"] = employee.LastName;

                return RedirectToAction("Dashboard", "Employee");
            }
            _context.Session["user_Name"] = null;
            _context.Session["employee_Id"] = null;
            _context.Session["first_Name"] = null;
            _context.Session["last_Name"] = null;
            ViewBag.ApplicantNotExist = "Invalid Registration No. or Password";
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "EmployeeTypeId", "EmployeeTypeName");
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName");
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "EmployeeId,FirstName,LastName,Gender,BirthDate,AppointmentDate,QualificationId,EmployeeTypeId,UserName,Password,PhoneNo")] Employee employee)
        {

            try
            {
                if(ModelState.IsValid)
                {
                    employee.UserName = GenerateRegNo(employee.EmployeeId);
                    employee.Password = GeneratePassword();
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    ViewBag.Success = "Registration was Successful !!" +" "+ "Your Username is: " + employee.UserName +" "+"and Password is:" + employee.Password;
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "EmployeeTypeId", "EmployeeTypeName", employee.EmployeeTypeId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName", employee.QualificationId);
            return View(employee);
        }
        public string GenerateRegNo(int id)
        {
            var applicant = db.Employees.FirstOrDefault(m => m.EmployeeId == id);
            var rand = new Random();
            var Nrand = "EMP" + rand.Next(1000000, 5000000);
            return Nrand.ToString();

        }

        //Send Email


        //Generate Password

        public string GeneratePassword()
        {
            var allowedChas = "";
            const int length = 8;
            allowedChas += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            allowedChas += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowedChas += "0,1,2,3,4,5,6,7,8,9";
            var arr = allowedChas.Split(',');
            var password = "";
            var rand = new Random();
            for (int i = 0; i < length; i++)
            {
                var temp = arr[rand.Next(0, arr.Length)];
                password += temp;
            }
            return password;
        }


        

        // GET: /Employee/
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.EmployeeType).Include(e => e.Qualification);
            return View(employees.ToList());
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "EmployeeTypeId", "EmployeeTypeName");
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName");
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EmployeeId,FirstName,LastName,Gender,BirthDate,AppointmentDate,QualificationId,EmployeeTypeId,UserName,Password,PhoneNo")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "EmployeeTypeId", "EmployeeTypeName", employee.EmployeeTypeId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName", employee.QualificationId);
            return View(employee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "EmployeeTypeId", "EmployeeTypeName", employee.EmployeeTypeId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName", employee.QualificationId);
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="EmployeeId,FirstName,LastName,Gender,BirthDate,AppointmentDate,QualificationId,EmployeeTypeId,UserName,Password,PhoneNo")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "EmployeeTypeId", "EmployeeTypeName", employee.EmployeeTypeId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "QualificationId", "QualificationName", employee.QualificationId);
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
