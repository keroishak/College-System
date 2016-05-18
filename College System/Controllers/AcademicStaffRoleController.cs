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
    public class AcademicStaffRoleController : Controller
    {
        private Model db = new Model();

        // GET: /AcademicStaffRole/
        public ActionResult Index()
        {
            return View(db.AcademicStaffRoles.ToList());
        }

        // GET: /AcademicStaffRole/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicStaffRole academicstaffrole = db.AcademicStaffRoles.Find(id);
            if (academicstaffrole == null)
            {
                return HttpNotFound();
            }
            return View(academicstaffrole);
        }

        // GET: /AcademicStaffRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AcademicStaffRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RoleID,Name")] AcademicStaffRole academicstaffrole)
        {
            if (ModelState.IsValid)
            {
                academicstaffrole.RoleID = Guid.NewGuid();
                db.AcademicStaffRoles.Add(academicstaffrole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academicstaffrole);
        }

        // GET: /AcademicStaffRole/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicStaffRole academicstaffrole = db.AcademicStaffRoles.Find(id);
            if (academicstaffrole == null)
            {
                return HttpNotFound();
            }
            return View(academicstaffrole);
        }

        // POST: /AcademicStaffRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RoleID,Name")] AcademicStaffRole academicstaffrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicstaffrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academicstaffrole);
        }

        // GET: /AcademicStaffRole/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicStaffRole academicstaffrole = db.AcademicStaffRoles.Find(id);
            if (academicstaffrole == null)
            {
                return HttpNotFound();
            }
            return View(academicstaffrole);
        }

        // POST: /AcademicStaffRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AcademicStaffRole academicstaffrole = db.AcademicStaffRoles.Find(id);
            db.AcademicStaffRoles.Remove(academicstaffrole);
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
