using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using College_System.Models;

namespace College_System.Controllers
{
    public class StudentController : Controller
    {
        private Model db = new Model();

        // GET: /Student/
        public ActionResult Index(Guid? id, string name)
        {
            IQueryable<Student> students;
            if (id == null && name == null)
                students = db.Students.Include(s => s.AcademicYear).Include(s => s.AcademicYearSection).Include(s => s.Department);
            else if (name == "academicyear")
                students = db.Students.Where(s => (s.CurrentAcademicYearID == id));
            else if (name == "department")
                students = db.Students.Where(s => (s.DepartmentID == id));
            else if (name == "section")
                students = db.Students.Where(s => (s.SectionID == id));
            else if (name == "course")
                students = db.Students.Where(s => s.StudentCourses.Any(e => e.CourseID == id));
            else
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            return View(students.ToList());
        }

        // GET: /Student/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult Filter(string FilterationString)
        {
            string[] filters = FilterationString.Split('|');
            IQueryable<Student> students = db.Students;
            string tmp;
            if (filters[0] != "")
            {
                tmp = filters[0];
                students = students.Where(s => s.Name.Contains(tmp));
            }
            if (filters[1] != "")
            {
                tmp = filters[1];
                students = students.Where(s => s.Email.Contains(tmp));
            }
            if (filters[2] != "")
            {
                int AddYear = int.Parse(filters[2]);
                students = students.Where(s => s.AdmissionYear == AddYear);
            }
            if (filters[3] != "")
            {
                int GradYear = int.Parse(filters[3]);
                students = students.Where(s => s.GraduationYear == GradYear);
            }
            if (filters[4] != "")
            {
                tmp = filters[4];
                students = students.Where(s => s.SerialID == tmp);
            }
            students=students.Include(s => s.AcademicYear).Include(s => s.AcademicYearSection).Include(s => s.Department);
            var l = students.ToList();
            return PartialView("_StudentFiltered", students.ToList());

        }
        // GET: /Student/Create
        public ActionResult Create()
        {
            ViewBag.CurrentAcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name");
            ViewBag.SectionID = new SelectList(db.AcademicYearSections, "SectionID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,Name,Email,AdmissionYear,GraduationYear,CurrentAcademicYearID,SectionID,SerialID,DepartmentID")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.StudentID = Guid.NewGuid();
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrentAcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", student.CurrentAcademicYearID);
            ViewBag.SectionID = new SelectList(db.AcademicYearSections, "SectionID", "Name", student.SectionID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", student.DepartmentID);
            return View(student);
        }

        // GET: /Student/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentAcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", student.CurrentAcademicYearID);
            ViewBag.SectionID = new SelectList(db.AcademicYearSections, "SectionID", "Name", student.SectionID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", student.DepartmentID);
            return View(student);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,Name,Email,AdmissionYear,GraduationYear,CurrentAcademicYearID,SectionID,SerialID,DepartmentID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentAcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", student.CurrentAcademicYearID);
            ViewBag.SectionID = new SelectList(db.AcademicYearSections, "SectionID", "Name", student.SectionID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", student.DepartmentID);
            return View(student);
        }

        // GET: /Student/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
