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
    public class FinesController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Fines
        public ActionResult Index()
        {
            var fine = db.fine.Include(f => f.student);
            return View(fine.ToList());
        }

        // GET: Fines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine fine = db.fine.Find(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            return View(fine);
        }

        // GET: Fines/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstMidName");
            return View();
        }

        // POST: Fines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FineID,StudentID,FineAmount,FineDescription")] Fine fine)
        {
            if (ModelState.IsValid)
            {
                fine.amountCollected = 0;
                db.fine.Add(fine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstMidName", fine.StudentID);
            return View(fine);
        }

        // GET: Fines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine fine = db.fine.Find(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "father", fine.StudentID);
            return View(fine);
        }

        // POST: Fines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FineID,StudentID,FineAmount,amountCollected")] Fine fine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "father", fine.StudentID);
            return View(fine);
        }

        // GET: Fines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine fine = db.fine.Find(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            return View(fine);
        }

        // POST: Fines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fine fine = db.fine.Find(id);
            db.fine.Remove(fine);
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
