using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using College_System.Models;
using System.IO;

namespace College_System.Controllers
{
    public class CourseMaterialController : Controller
    {
        private Model db = new Model();

        // GET: /CourseMaterial/
        public ActionResult Index(Guid?id)
        {
            IQueryable<CourseMaterial> coursematerials;
            if (id == null)
                coursematerials = db.CourseMaterials.Include(c => c.Cours).Include(c => c.MaterialType);
            else
                coursematerials = db.CourseMaterials.Where(c => c.CourseID == id);
            return View(coursematerials.ToList());
        }

        // GET: /CourseMaterial/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            if (coursematerial == null)
            {
                return HttpNotFound();
            }
            return View(coursematerial);
        }

        // GET: /CourseMaterial/Create
        public ActionResult Create()
        {            
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            ViewBag.MaterialTypeID = new SelectList(db.MaterialTypes, "MaterialTypeID", "Name");
            return View();
        }

        // POST: /CourseMaterial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaterialID,CourseID,MaterialTypeID,Name,FilePath")] CourseMaterial coursematerial)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    coursematerial.FilePath = Request.Files[0].FileName;
                    string ex = coursematerial.FilePath.Substring(coursematerial.FilePath.IndexOf('.') + 1);
                    if (ex == "pdf" || ex == "doc" || ex == "docx" || ex == "ppt")
                    {
                        coursematerial.FilePath = Path.GetFileName(coursematerial.FilePath);
                        string path = Path.Combine(Server.MapPath("~/App_Data/uploaded files"), coursematerial.FilePath);
                        var file = Request.Files[0];
                        file.SaveAs(path);
                        // file.
                    }
                    coursematerial.MaterialID = Guid.NewGuid();
                    db.CourseMaterials.Add(coursematerial);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursematerial.CourseID);
            ViewBag.MaterialTypeID = new SelectList(db.MaterialTypes, "MaterialTypeID", "Name", coursematerial.MaterialTypeID);
            return View(coursematerial);
        }

        // GET: /CourseMaterial/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            if (coursematerial == null)
            {
                return HttpNotFound();
            }
            if (Request.Files.Count > 0)
            {
                coursematerial.FilePath = Request.Files[0].FileName;
                string ex = coursematerial.FilePath.Substring(coursematerial.FilePath.IndexOf('.') + 1);
                if (ex == "pdf" || ex == "doc" || ex == "docx" || ex == "ppt")
                {
                    coursematerial.FilePath = Path.GetFileName(coursematerial.FilePath);
                    string path = Path.Combine(Server.MapPath("~/App_Data/uploaded files"), coursematerial.FilePath);
                    var file = Request.Files[0];
                    file.SaveAs(path);
                }

                db.CourseMaterials.Add(coursematerial);
                db.SaveChanges();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursematerial.CourseID);
            ViewBag.MaterialTypeID = new SelectList(db.MaterialTypes, "MaterialTypeID", "Name", coursematerial.MaterialTypeID);
            return View(coursematerial);
        }

        // POST: /CourseMaterial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaterialID,CourseID,MaterialTypeID,Name,FilePath")] CourseMaterial coursematerial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursematerial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursematerial.CourseID);
            ViewBag.MaterialTypeID = new SelectList(db.MaterialTypes, "MaterialTypeID", "Name", coursematerial.MaterialTypeID);
            return View(coursematerial);
        }

        // GET: /CourseMaterial/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            if (coursematerial == null)
            {
                return HttpNotFound();
            }
            return View(coursematerial);
        }

        // POST: /CourseMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            db.CourseMaterials.Remove(coursematerial);
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
