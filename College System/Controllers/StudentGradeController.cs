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
    public class StudentGradeController : Controller
    {
        private Model db = new Model();

        // GET: /StudentGrade/
        public ActionResult Index(Guid? id)
        {
            
            if(id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var studentgrades = db.StudentGrades.Include(s => s.CourseGrade).Where(s=>s.StudentID==id);
            ViewData.Add ("id",id);
            //Tuple<Guid, IEnumerable<StudentGrade>> res = new Tuple<Guid, IEnumerable<StudentGrade>>(id, studentgrades.ToList());
            return View(studentgrades.ToList());
        }

        // GET: /StudentGrade/Details/5
        public ActionResult Details(Guid? id, Guid? GradeID)
        {
            if (id == null||GradeID==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrade studentgrade = db.StudentGrades.Find(id,GradeID);
            if (studentgrade == null)
            {
                return HttpNotFound();
            }
            return View(studentgrade);
        }

        // GET: /StudentGrade/Create
        public ActionResult Create(Guid? id)
        {
            if(id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
             var student=db.Students.Find(id);
            if(student==null)
                return HttpNotFound();
           // IQueryable<Cours> courses = db.Courses.Where(s => s.StudentCourses.Any(c => c.StudentID == id));
            //List<Cours> courses = db.Courses.ToList();
            ViewBag.GradeID = new SelectList(db.CourseGrades, "GradeID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            ViewBag.Name = new SelectList(db.Courses, "Name", "Name");
           // ViewBag.ID = new SelectList(db.Courses, "CourseID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult GetMarks(string courseName)
        {
            Cours c = db.Courses.Where(s => s.Name == courseName).ToList()[0];
            IQueryable < CourseGrade > res= db.CourseGrades.Where(s => s.CourseID == c.CourseID);

            return PartialView("_MarksPage",res.ToList());
        }
        // POST: /StudentGrade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            StudentGrade s = new StudentGrade();
            if (ModelState.IsValid)
            {
                int year=int.Parse(Request["Year"]);                              
                Guid StudentID = Guid.Parse(Request.RequestContext.RouteData.Values["id"].ToString());
                string temp;
                foreach (string item in Request.Params)
                {
                    if (item.Length > 5)
                    {
                        
                        temp = Request[item.Substring(5)];
                        Guid outt;
                        bool fine = Guid.TryParse(temp,out outt);
                        if (item.Substring(0, 4) == "Mark" && fine==true)
                        {
                            s = new StudentGrade();
                            s.GradeID = outt;
                            s.Year = year;
                            s.StudentID = StudentID;

                            s.Mark = decimal.Parse(Request[item]);
                            db.StudentGrades.Add(s);

                        }
                    }

                }
                db.SaveChanges();
                /*Cours c=db.Courses.Where(s=>s.Name==studentgrade.Name).ToArray()[0];
                CourseGrade course = new CourseGrade();
                course.GradeID = new Guid();
                course.CourseID = c.CourseID;
                course.Name = "Mid-Term";
                course.Total = studentgrade.MidTerm;*/

                return RedirectToAction("Index", new { id = StudentID });
            }

            //ViewBag.GradeID = new SelectList(db.CourseGrades, "GradeID", "Name", studentgrade.GradeID);
           // ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentgrade.StudentID);
            return View(s);
        }

        // GET: /StudentGrade/Edit/5
        public ActionResult Edit(Guid? id, Guid? GradeID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrade studentgrade = db.StudentGrades.Find(id,GradeID);
            if (studentgrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeID = new SelectList(db.CourseGrades, "GradeID", "Name", studentgrade.GradeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentgrade.StudentID);
            return View(studentgrade);
        }

        // POST: /StudentGrade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StudentID,GradeID,Mark,Year")] StudentGrade studentgrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentgrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id=studentgrade.StudentID});
            }
            ViewBag.GradeID = new SelectList(db.CourseGrades, "GradeID", "Name", studentgrade.GradeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentgrade.StudentID);
            return View(studentgrade);
        }

        // GET: /StudentGrade/Delete/5
        public ActionResult Delete(Guid? id, Guid? GradeID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrade studentgrade = db.StudentGrades.Find(id,GradeID);
            if (studentgrade == null)
            {
                return HttpNotFound();
            }
            return View(studentgrade);
        }

        // POST: /StudentGrade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id, Guid GradeID)
        {
            StudentGrade studentgrade = db.StudentGrades.Find(id,GradeID);
            db.StudentGrades.Remove(studentgrade);
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
