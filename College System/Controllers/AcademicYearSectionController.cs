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
    public class AcademicYearSectionController : Controller
    {
        private Model db = new Model();

        // GET: /AcademicYearSection/
        public ActionResult Index(Guid? id,string name)
        {
            IQueryable<AcademicYearSection> academicyearsections;
            if (id == null&&name==null)
                academicyearsections = db.AcademicYearSections.Include(a => a.AcademicYear).Include(a => a.Department);
            else if (name == "academicyear")
                academicyearsections = db.AcademicYearSections.Where(a => (a.AcademicYearID == id));
            else if (name == "department")
                academicyearsections = db.AcademicYearSections.Where(a => (a.DepartmentID == id));
            else
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            return View(academicyearsections.ToList());
        }

        // GET: /AcademicYearSection/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYearSection academicyearsection = db.AcademicYearSections.Find(id);
            if (academicyearsection == null)
            {
                return HttpNotFound();
            }
            return View(academicyearsection);
        }

        // GET: /AcademicYearSection/Create
        public ActionResult Create()
        {
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: /AcademicYearSection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SectionID,AcademicYearID,Name,DepartmentID")] AcademicYearSection academicyearsection)
        {
            if (ModelState.IsValid)
            {
                academicyearsection.SectionID = Guid.NewGuid();
                db.AcademicYearSections.Add(academicyearsection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", academicyearsection.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicyearsection.DepartmentID);
            return View(academicyearsection);
        }

        // GET: /AcademicYearSection/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYearSection academicyearsection = db.AcademicYearSections.Find(id);
            if (academicyearsection == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", academicyearsection.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicyearsection.DepartmentID);
            return View(academicyearsection);
        }

        // POST: /AcademicYearSection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SectionID,AcademicYearID,Name,DepartmentID")] AcademicYearSection academicyearsection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicyearsection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", academicyearsection.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicyearsection.DepartmentID);
            return View(academicyearsection);
        }

        // GET: /AcademicYearSection/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYearSection academicyearsection = db.AcademicYearSections.Find(id);
            if (academicyearsection == null)
            {
                return HttpNotFound();
            }
            return View(academicyearsection);
        }

        // POST: /AcademicYearSection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AcademicYearSection academicyearsection = db.AcademicYearSections.Find(id);
            db.AcademicYearSections.Remove(academicyearsection);
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
