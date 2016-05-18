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
    public class AcademicYearsController : Controller
    {
        private Model db = new Model();

        // GET: /AcademicYears/
        public ActionResult Index()
        {
            return View(db.AcademicYears.ToList());
        }

        // GET: /AcademicYears/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicyear = db.AcademicYears.Find(id);
            if (academicyear == null)
            {
                return HttpNotFound();
            }
            return View(academicyear);
        }

        // GET: /AcademicYears/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AcademicYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AcademicYearID,Name")] AcademicYear academicyear)
        {
            if (ModelState.IsValid)
            {
                academicyear.AcademicYearID = Guid.NewGuid();
                db.AcademicYears.Add(academicyear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academicyear);
        }

        // GET: /AcademicYears/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicyear = db.AcademicYears.Find(id);
            if (academicyear == null)
            {
                return HttpNotFound();
            }
            return View(academicyear);
        }

        // POST: /AcademicYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AcademicYearID,Name")] AcademicYear academicyear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicyear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academicyear);
        }

        // GET: /AcademicYears/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicyear = db.AcademicYears.Find(id);
            if (academicyear == null)
            {
                return HttpNotFound();
            }
            return View(academicyear);
        }

        // POST: /AcademicYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            db.AcademicYears.Remove(academicyear);
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
