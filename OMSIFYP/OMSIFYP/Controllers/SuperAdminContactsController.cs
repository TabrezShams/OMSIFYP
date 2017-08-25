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
    public class SuperAdminContactsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: SuperAdminContacts
        public ActionResult Index()
        {
            return View(db.superAdminContact.ToList());
        }

        // GET: SuperAdminContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuperAdminContact superAdminContact = db.superAdminContact.Find(id);
            if (superAdminContact == null)
            {
                return HttpNotFound();
            }
            return View(superAdminContact);
        }

        // GET: SuperAdminContacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuperAdminContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Titile,Message")] SuperAdminContact superAdminContact)
        {
            if (ModelState.IsValid)
            {
                ViewBag.msg = "Your Message is Succesfully Dilieverd to Super Admin!";
                db.superAdminContact.Add(superAdminContact);
                db.SaveChanges();
                return View();
            }

            return View(superAdminContact);
        }

        // GET: SuperAdminContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuperAdminContact superAdminContact = db.superAdminContact.Find(id);
            if (superAdminContact == null)
            {
                return HttpNotFound();
            }
            return View(superAdminContact);
        }

        // POST: SuperAdminContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Titile,Message")] SuperAdminContact superAdminContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(superAdminContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(superAdminContact);
        }

        // GET: SuperAdminContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuperAdminContact superAdminContact = db.superAdminContact.Find(id);
            if (superAdminContact == null)
            {
                return HttpNotFound();
            }
            return View(superAdminContact);
        }

        // POST: SuperAdminContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuperAdminContact superAdminContact = db.superAdminContact.Find(id);
            db.superAdminContact.Remove(superAdminContact);
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
