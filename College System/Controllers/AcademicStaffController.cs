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
    public class AcademicStaffController : Controller
    {
        private Model db = new Model();

        // GET: /AcademicStaff/
        public ActionResult Index(Guid? id, string name)
        {
            IQueryable<AcademicStaff> academicstaffs=null;
            if (id == null && name == null)
                academicstaffs = db.AcademicStaffs.Include(a => a.AcademicStaffRole).Include(a => a.Department);
            else if (name == "department")
                academicstaffs = db.AcademicStaffs.Where(a => (a.DepartmentID == id));
            else if (name == "staffrole")
                academicstaffs = db.AcademicStaffs.Where(a => (a.RoleID == id));
            else if (name == "course")
                academicstaffs = db.Courses.Where(c => c.CourseID == id).SelectMany(c => c.AcademicStaffs);
            else
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            return View(academicstaffs.ToList());
        }

        // GET: /AcademicStaff/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicStaff academicstaff = db.AcademicStaffs.Find(id);
            if (academicstaff == null)
            {
                return HttpNotFound();
            }
            return View(academicstaff);
        }

        // GET: /AcademicStaff/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.AcademicStaffRoles, "RoleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: /AcademicStaff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="InstructorID,Name,Email,RoleID,DepartmentID")] AcademicStaff academicstaff)
        {
            if (ModelState.IsValid)
            {
                academicstaff.InstructorID = Guid.NewGuid();
                db.AcademicStaffs.Add(academicstaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.AcademicStaffRoles, "RoleID", "Name", academicstaff.RoleID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicstaff.DepartmentID);
            return View(academicstaff);
        }
        public ActionResult StaffCourses(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult StaffCourses(Guid CourseID)
        {
            if (ModelState.IsValid)
            {
                AcademicStaff staff =db.AcademicStaffs.Find(Guid.Parse(Request.Url.Segments[3]));
                Cours c = db.Courses.Find(CourseID);
                staff.Courses.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
                //ViewBag.ID = id;
            }
            return View();
        }
        // GET: /AcademicStaff/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicStaff academicstaff = db.AcademicStaffs.Find(id);
            if (academicstaff == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.AcademicStaffRoles, "RoleID", "Name", academicstaff.RoleID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicstaff.DepartmentID);
            return View(academicstaff);
        }

        // POST: /AcademicStaff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="InstructorID,Name,Email,RoleID,DepartmentID")] AcademicStaff academicstaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicstaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.AcademicStaffRoles, "RoleID", "Name", academicstaff.RoleID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicstaff.DepartmentID);
            return View(academicstaff);
        }

        // GET: /AcademicStaff/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicStaff academicstaff = db.AcademicStaffs.Find(id);
            if (academicstaff == null)
            {
                return HttpNotFound();
            }
            return View(academicstaff);
        }

        // POST: /AcademicStaff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AcademicStaff academicstaff = db.AcademicStaffs.Find(id);
            db.AcademicStaffs.Remove(academicstaff);
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
