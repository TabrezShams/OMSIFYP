using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OMSIFYP.DAL;
using OMSIFYP.Models;

namespace OMSIFYP.Controllers
{
    public class GenrateClassesController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: GenrateClasses
        public ActionResult viewStudents(int id)
        {
            var res = from m in db.enrollStudent select m;
            res = res.Where(e => e.GenrateClassID == id);

            return View(res);
        }




        public ActionResult Index(string searchName)
        {

            if (searchName != null)
            {


                var msgist = from m in db.genrateClass select m;
                msgist = msgist.Where(s => s.Name.ToUpper().Contains(searchName.ToUpper()));
                return View(msgist);

            }
           

            var genrateClass = db.genrateClass.Include(g => g.course).Include(g => g.department).Include(g => g.instructor);
            return View(genrateClass.ToList());
        }

        // GET: GenrateClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenrateClass genrateClass = db.genrateClass.Find(id);
            if (genrateClass == null)
            {
                return HttpNotFound();
            }
            return View(genrateClass);
        }



        // GET: GenrateClasses/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.InstructorID = new SelectList(db.People, "ID", "FirstMidName");
            return View();
        }

        // POST: GenrateClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( GenrateClass genrateClass)
        {
            //IQueryable<string>instqry= from m in db.Instructors
            //                           orderby m.FirstMidName
            //                           select m.FirstMidName;
            if (ModelState.IsValid)
            {
                db.genrateClass.Add(genrateClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", genrateClass.CourseID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", genrateClass.DepartmentID);
           // ViewBag.InstructorID = new SelectList(instqry.Distinct().ToList(),"ID","LastName",genrateClass.InstructorID);
            
            ViewBag.InstructorID = new SelectList(db.Instructors,"ID","FirstMidName", genrateClass.InstructorID);

            return View(genrateClass);
        }

        // GET: GenrateClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenrateClass genrateClass = db.genrateClass.Find(id);
            if (genrateClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", genrateClass.CourseID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", genrateClass.DepartmentID);
            ViewBag.InstructorID = new SelectList(db.People, "ID", "FirstMidName", genrateClass.InstructorID);
            return View(genrateClass);
        }

        // POST: GenrateClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenrateClassID,Name,Section,CourseID,InstructorID,DepartmentID")] GenrateClass genrateClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genrateClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", genrateClass.CourseID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", genrateClass.DepartmentID);
            ViewBag.InstructorID = new SelectList(db.People, "ID", "FirstMidName", genrateClass.InstructorID);
            return View(genrateClass);
        }
        public ActionResult Unroll(int? id)
        {
            EnrollStudent enroll = db.enrollStudent.Find(id);
            db.enrollStudent.Remove(enroll);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: GenrateClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenrateClass genrateClass = db.genrateClass.Find(id);
            if (genrateClass == null)
            {
                return HttpNotFound();
            }
            return View(genrateClass);
        }

        // POST: GenrateClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GenrateClass genrateClass = db.genrateClass.Find(id);
            db.genrateClass.Remove(genrateClass);
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
