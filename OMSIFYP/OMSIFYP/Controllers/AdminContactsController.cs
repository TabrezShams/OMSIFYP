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
    public class AdminContactsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: AdminContacts
        public ActionResult Index()
        {
            return View(db.AdminContacts.ToList());
        }

        // GET: AdminContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminContact adminContact = db.AdminContacts.Find(id);
            if (adminContact == null)
            {
                return HttpNotFound();
            }
            return View(adminContact);
        }

        // GET: AdminContacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Titile,Message")] AdminContact adminContact)
        {
            if (ModelState.IsValid)
            {
                ViewBag.msg1 = "Your message has been delivered!";
                db.AdminContacts.Add(adminContact);
                db.SaveChanges();
                return View();
            }

            return View(adminContact);
        }

        // GET: AdminContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminContact adminContact = db.AdminContacts.Find(id);
            if (adminContact == null)
            {
                return HttpNotFound();
            }
            return View(adminContact);
        }

        // POST: AdminContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Titile,Message")] AdminContact adminContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminContact);
        }

        // GET: AdminContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminContact adminContact = db.AdminContacts.Find(id);
            if (adminContact == null)
            {
                return HttpNotFound();
            }
            return View(adminContact);
        }

        // POST: AdminContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminContact adminContact = db.AdminContacts.Find(id);
            db.AdminContacts.Remove(adminContact);
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
