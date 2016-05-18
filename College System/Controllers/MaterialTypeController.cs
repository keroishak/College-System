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
    public class MaterialTypeController : Controller
    {
        private Model db = new Model();

        // GET: /MaterialType/
        public ActionResult Index()
        {
            return View(db.MaterialTypes.ToList());
        }

        // GET: /MaterialType/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaterialType materialtype = db.MaterialTypes.Find(id);
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        // GET: /MaterialType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MaterialType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaterialTypeID,Name")] MaterialType materialtype)
        {
            if (ModelState.IsValid)
            {
                materialtype.MaterialTypeID = Guid.NewGuid();
                db.MaterialTypes.Add(materialtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materialtype);
        }

        // GET: /MaterialType/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaterialType materialtype = db.MaterialTypes.Find(id);
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        // POST: /MaterialType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaterialTypeID,Name")] MaterialType materialtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materialtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materialtype);
        }

        // GET: /MaterialType/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaterialType materialtype = db.MaterialTypes.Find(id);
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        // POST: /MaterialType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            db.MaterialTypes.Remove(materialtype);
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
