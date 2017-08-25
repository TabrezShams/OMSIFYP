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
    public class PTMCallsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: PTMCalls
        public ActionResult Index()
        {
            string currentUser = Session["userRole"].ToString();
            var msgList = from m in db.ptmCall select m;
            msgList = msgList.Where(t => t.Creater!=currentUser);

            

            return View(msgList);

            //return View(db.ptmCall.ToList());
        }

        public ActionResult IndexAdmin()
        {
            string currentUser = Session["userRole"].ToString();
            var msgList = from m in db.ptmCall select m;
            msgList = msgList.Where(t => t.Creater != currentUser);



            return View(msgList);

            //return View(db.ptmCall.ToList());
        }
        public ActionResult IndexIns()
        {
            string currentUser = Session["userRole"].ToString();
            var msgList = from m in db.ptmCall select m;
            msgList = msgList.Where(t => t.Creater != currentUser);



            return View(msgList);

            //return View(db.ptmCall.ToList());
        }

        // GET: PTMCalls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PTMCalls pTMCalls = db.ptmCall.Find(id);
            if (pTMCalls == null)
            {
                return HttpNotFound();
            }
            return View(pTMCalls);
        }

        public ActionResult CreatebyAdmin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatebyAdmin([Bind(Include = "ID,date,Titile,Message,Creater")] PTMCalls pTMCalls)
        {
            if (ModelState.IsValid)
            {
                ViewBag.msg = "Message sent successfully";
                db.ptmCall.Add(pTMCalls);
                db.SaveChanges();
                return View();
            }

            return View(pTMCalls);
        }











        // GET: PTMCalls/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: PTMCalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,date,Titile,Message,Creater")] PTMCalls pTMCalls)
        {
            if (ModelState.IsValid)
            {
                ViewBag.msg = "Message sent successfully";
                db.ptmCall.Add(pTMCalls);
                db.SaveChanges();
                return View();
            }

            return View(pTMCalls);
        }

        // GET: PTMCalls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PTMCalls pTMCalls = db.ptmCall.Find(id);
            if (pTMCalls == null)
            {
                return HttpNotFound();
            }
            return View(pTMCalls);
        }

        // POST: PTMCalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,date,Titile,Message,Creater")] PTMCalls pTMCalls)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pTMCalls).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pTMCalls);
        }

        // GET: PTMCalls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PTMCalls pTMCalls = db.ptmCall.Find(id);
            if (pTMCalls == null)
            {
                return HttpNotFound();
            }
            return View(pTMCalls);
        }

        // POST: PTMCalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PTMCalls pTMCalls = db.ptmCall.Find(id);
            db.ptmCall.Remove(pTMCalls);
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
