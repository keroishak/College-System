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
    public class CoursesController : Controller
    {
        private Model db = new Model();

        // GET: /Courses/
        public ActionResult Index(Guid? id, string name)
        {
            IQueryable<Cours> courses=null;
            if (id == null && name == null)
                courses = db.Courses.Include(c => c.AcademicYear).Include(c => c.Department).Include(c => c.Semester);
            else if (name == "academicyear")
                courses = db.Courses.Where(c => (c.AcademicYearID == id));
            else if (name == "department")
                courses = db.Courses.Where(c => (c.DepartmentID == id));
            else if (name == "semester")
                courses = db.Courses.Where(c => (c.SemesterID == id));
            else if(name=="student")
                courses = db.Courses.Where(s => s.StudentCourses.Any(e => e.StudentID == id));
            else
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            return View(courses.ToList());
        }

        // GET: /Courses/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: /Courses/Create
        public ActionResult Create()
        {
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name");
            return View();
        }

        // POST: /Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CourseID,Name,SemesterID,DepartmentID,AcademicYearID")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                cours.CourseID = Guid.NewGuid();
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", cours.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", cours.DepartmentID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name", cours.SemesterID);
            return View(cours);
        }
  
        // GET: /Courses/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", cours.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", cours.DepartmentID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name", cours.SemesterID);
            return View(cours);
        }

        // POST: /Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CourseID,Name,SemesterID,DepartmentID,AcademicYearID")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", cours.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", cours.DepartmentID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name", cours.SemesterID);
            return View(cours);
        }

        // GET: /Courses/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: /Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
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
