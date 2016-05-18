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
    public class CourseGradeController : Controller
    {
        private Model db = new Model();

        // GET: /CourseGrade/
        public ActionResult Index(Guid?id,string name)
        {
            IQueryable<CourseGrade> coursegrades=null;
            if (id == null && name == null)
                coursegrades = db.CourseGrades.Include(c => c.Cours);
            else if (name == "course")
                coursegrades = db.CourseGrades.Where(c => c.CourseID == id);
            else if (name == "student")
                coursegrades = db.CourseGrades.Where(c => c.StudentGrades.Any(x => x.StudentID == id));
            return View(coursegrades.ToList());
        }
        
        // GET: /CourseGrade/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGrade coursegrade = db.CourseGrades.Find(id);
            if (coursegrade == null)
            {
                return HttpNotFound();
            }
            return View(coursegrade);
        }

        // GET: /CourseGrade/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            return View();
        }

        // POST: /CourseGrade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="GradeID,CourseID,Name,Total")] CourseGrade coursegrade)
        {
            if (ModelState.IsValid)
            {
                coursegrade.GradeID = Guid.NewGuid();
                db.CourseGrades.Add(coursegrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursegrade.CourseID);
            return View(coursegrade);
        }

        // GET: /CourseGrade/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGrade coursegrade = db.CourseGrades.Find(id);
            if (coursegrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursegrade.CourseID);
            return View(coursegrade);
        }

        // POST: /CourseGrade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="GradeID,CourseID,Name,Total")] CourseGrade coursegrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursegrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursegrade.CourseID);
            return View(coursegrade);
        }

        // GET: /CourseGrade/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGrade coursegrade = db.CourseGrades.Find(id);
            if (coursegrade == null)
            {
                return HttpNotFound();
            }
            return View(coursegrade);
        }

        // POST: /CourseGrade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CourseGrade coursegrade = db.CourseGrades.Find(id);
            db.CourseGrades.Remove(coursegrade);
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
