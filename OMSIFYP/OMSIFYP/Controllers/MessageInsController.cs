using OMSIFYP.DAL;
using OMSIFYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OMSIFYP.Controllers
{
    public class MessageInsController : Controller
    {
        private SchoolContext db = new SchoolContext();
        // GET: Message
        public ActionResult Index(string searchName)
        {
            if (searchName != null)
            {

                string curretUser = Session["userEmail"].ToString();
                var msgist = from m in db.Message select m;
                msgist = msgist.Where(t => t.email.Contains(curretUser));
                msgist = msgist.Where(s => s.Sender.ToUpper().Contains(searchName.ToUpper()));
                return View(msgist);

            }
            string currentUser = Session["userEmail"].ToString();
            var msgList = from m in db.Message select m;
            msgList = msgList.Where(t => t.email.Contains(currentUser));
            return View(msgList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MessageSend msg, string role)
        {
            if (!String.IsNullOrEmpty(role))
            {
                if (!String.IsNullOrEmpty(msg.email) && !String.IsNullOrEmpty(msg.subject))
                {
                    db.Message.Add(msg);
                    db.SaveChanges();
                    return RedirectToAction("Index", "MessageIns");
                    ViewBag.msg = "Message sent Successfully to " + msg.email;
                    return RedirectToAction("Index", "MessageIns");
                }
                else
                {
                    ViewBag.role = role;
                    if (role == "Instructor")
                    {
                        ViewBag.email = new SelectList(db.Instructors, "email", "email");
                    }
                    else if (role == "Student")
                    {
                        ViewBag.email = new SelectList(db.Students, "email", "email");
                    }
                    else if (role == "Admin")
                    {
                        ViewBag.email = new SelectList(db.Admin, "email", "email");
                    }
                }
            }
            return View();
        }


        public ActionResult Reply(MessageSend msg, int id, string sender, string sub)
        {
            Session["msgId"] = id;
            Session["msgSender"] = sender;
            Session["msgSub"] = sub;

            if (!String.IsNullOrEmpty(msg.Message))
            {
                db.Message.Add(msg);
                db.SaveChanges();
                ViewBag.msg = "Replied Successfully to " + msg.email;
                return RedirectToAction("Index");
            }
            return View();


        }

        // POST: Course/Delete/5


        public ActionResult Delete(int? id)
        {
            MessageSend msg = db.Message.Find(id);
            db.Message.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}