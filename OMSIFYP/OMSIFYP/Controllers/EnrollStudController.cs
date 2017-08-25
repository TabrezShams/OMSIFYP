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
    public class EnrollStudController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: EnrollStud
        public ActionResult Index()
        {
            var enrollStudent = db.enrollStudent.Include(e => e.genrateClass).Include(e => e.student);
            return View(enrollStudent.ToList());
        }

        // GET: EnrollStud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollStudent enrollStudent = db.enrollStudent.Find(id);
            if (enrollStudent == null)
            {
                return HttpNotFound();
            }
            return View(enrollStudent);
        }

        // GET: EnrollStud/Create
        public ActionResult Create()
        {
            ViewBag.GenrateClassID = new SelectList(db.genrateClass, "GenrateClassID", "Name");
            ViewBag.StudentID = new SelectList(db.People, "ID", "father");
            return View();
        }

        // POST: EnrollStud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GenrateClassID,StudentID,sessional1,sessional2,sessional3")] EnrollStudent enrollStudent)
        {
            if (ModelState.IsValid)
            {
                db.enrollStudent.Add(enrollStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenrateClassID = new SelectList(db.genrateClass, "GenrateClassID", "Name", enrollStudent.GenrateClassID);
            ViewBag.StudentID = new SelectList(db.People, "ID", "father", enrollStudent.StudentID);
            return View(enrollStudent);
        }

        // GET: EnrollStud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollStudent enrollStudent = db.enrollStudent.Find(id);
            if (enrollStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenrateClassID = new SelectList(db.genrateClass, "GenrateClassID", "Name", enrollStudent.GenrateClassID);
            ViewBag.StudentID = new SelectList(db.People, "ID", "father", enrollStudent.StudentID);
            return View(enrollStudent);
        }

        // POST: EnrollStud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GenrateClassID,StudentID,sessional1,sessional2,sessional3")] EnrollStudent enrollStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenrateClassID = new SelectList(db.genrateClass, "GenrateClassID", "Name", enrollStudent.GenrateClassID);
            ViewBag.StudentID = new SelectList(db.People, "ID", "father", enrollStudent.StudentID);
            return View(enrollStudent);
        }

        // GET: EnrollStud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollStudent enrollStudent = db.enrollStudent.Find(id);
            if (enrollStudent == null)
            {
                return HttpNotFound();
            }
            return View(enrollStudent);
        }

        // POST: EnrollStud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollStudent enrollStudent = db.enrollStudent.Find(id);
            db.enrollStudent.Remove(enrollStudent);
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
