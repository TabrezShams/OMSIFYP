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
using System.IO;

namespace OMSIFYP.Controllers
{
    public class AccountantsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Accountants
        public ActionResult Index()
        {
            return View(db.accountant.ToList());
        }

        // GET: Accountants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accountant accountant = db.accountant.Find(id);
            if (accountant == null)
            {
                return HttpNotFound();
            }
            return View(accountant);
        }

        // GET: Accountants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accountants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, HttpPostedFileBase CV, Accountant accountant)
        {
            if (ModelState.IsValid)
            {
                accountant.Role = "Accountant";
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles/Accountant_profile"), _FileName);

                string _CVName = Path.GetFileName(CV.FileName);
                string _pathCV = Path.Combine(Server.MapPath("~/UploadedFiles/Accountant_CV"), _CVName);



                file.SaveAs(_path);
                CV.SaveAs(_pathCV);
                accountant.imgUrl = "/UploadedFiles/Accountant_profile/" + _FileName;
                accountant.cv = "/UploadedFiles/Accountant_CV/" + _CVName;
                db.accountant.Add(accountant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountant);
        }

        // GET: Accountants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accountant accountant = db.accountant.Find(id);
            if (accountant == null)
            {
                return HttpNotFound();
            }
            return View(accountant);
        }

        // POST: Accountants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,father,gender,blood,datebirth,district,nationality,userId,email,noper,personcnic,Adddress,imgUrl,password,logCont,Role,LastName,FirstMidName,HireDate,salary,cv,qualificatin")] Accountant accountant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountant);
        }

        // GET: Accountants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accountant accountant = db.accountant.Find(id);
            if (accountant == null)
            {
                return HttpNotFound();
            }
            return View(accountant);
        }

        // POST: Accountants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accountant accountant = db.accountant.Find(id);
            db.People.Remove(accountant);
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
