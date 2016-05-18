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
    public class StudentCoursesController : Controller
    {
        private Model db = new Model();

        // GET: /StudentCourses/
        public ActionResult Index(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.StudentID = id;
                var studentcourses = db.StudentCourses.Where(s=>s.StudentID==id);
                return View(studentcourses);
        }

        // GET: /StudentCourses/Details/5
        public ActionResult Details(Guid? id, Guid? CourseID,int? Year)
        {
            if (id == null||CourseID==null||Year==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCours studentcours = db.StudentCourses.Find(id, CourseID, Year);
            if (studentcours == null)
            {
                return HttpNotFound();
            }
            return View(studentcours);
        }

        // GET: /StudentCourses/Create
        public ActionResult Create(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.CourseID =new SelectList( db.Courses.Where(s => s.StudentCourses.All(a=>a.StudentID!=id)),"CourseID", "Name");
            StudentCours stu = new StudentCours();
            stu.StudentID = id.GetValueOrDefault();
            return View(stu);
        }

        // POST: /StudentCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,CourseID,Year")] StudentCours studentcours)
        {
            if (ModelState.IsValid)
            {
                //studentcours.StudentID = Guid.NewGuid();
                studentcours.StudentID =Guid.Parse(Request.Url.Segments[3]);
                db.StudentCourses.Add(studentcours);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = studentcours.StudentID });
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", studentcours.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentcours.StudentID);
            return View(studentcours);
        }

        // GET: /StudentCourses/Edit/5
        public ActionResult Edit(Guid? id, Guid? CourseID, int? Year)
        {
            if (id == null || CourseID == null || Year == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCours studentcours = db.StudentCourses.Find(id,CourseID,Year);
            if (studentcours == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", studentcours.CourseID);
            return View(studentcours);
        }

        // POST: /StudentCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,CourseID,Year")] StudentCours studentcours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentcours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {id=studentcours.StudentID });
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", studentcours.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentcours.StudentID);
            return View(studentcours);
        }

        // GET: /StudentCourses/Delete/5
        public ActionResult Delete(Guid? id, Guid? CourseID, int? Year)
        {
            if (id == null || CourseID == null || Year == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCours studentcours = db.StudentCourses.Find(id,CourseID,Year);
            if (studentcours == null)
            {
                return HttpNotFound();
            }
            return View(studentcours);
        }

        // POST: /StudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id,Guid CourseID,int Year)
        {
            StudentCours studentcours = db.StudentCourses.Find(id, CourseID,Year);
            db.StudentCourses.Remove(studentcours);
            db.SaveChanges();
            return RedirectToAction("Index", new {id=id });
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
