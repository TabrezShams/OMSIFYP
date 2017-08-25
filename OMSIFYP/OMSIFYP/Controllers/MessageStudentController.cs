using OMSIFYP.DAL;
using OMSIFYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMSIFYP.Controllers
{
    public class MessageStudentController : Controller
    {
        private SchoolContext db = new SchoolContext();
        // GET: MessageStudent
        public ActionResult Index()
        {
            string currentUser = Session["userEmail"].ToString();
            var msgList = from m in db.Message select m;
            msgList = msgList.Where(t => t.email.Contains(currentUser));
            return View(msgList);
        }

        public ActionResult Delete(int? id)
        {
            MessageSend msg = db.Message.Find(id);
            db.Message.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}