using OMSIFYP.DAL;
using OMSIFYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMSIFYP.Controllers
{
    public class WebController : Controller
    {
        private SchoolContext db = new SchoolContext();
        // GET: Web
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ActionName("Login")]
        public ActionResult LoginConfirm(Login log)
        {
            //  var pers = from m in db.People select m;

            //   Person per = db.People.Find(log.id);
            Person per = db.People.FirstOrDefault(i => i.email == log.email);

            if (per != null)
            {
                if (per.password == log.password)
                {
                    Session["userEmail"] = log.email;
                    Session["userRole"] = per.Role;
                    if (per.Role == "Student")
                    {
                        if (per.logCont == 0)
                        {
                            Student stu = (Student)per;
                            return RedirectToAction("changePassword", "Login", new { id = stu.ID });

                        }
                        Student st = (Student)per;
                        Session["userName"] = st.FirstMidName;
                        Session["userImg"] = st.imgUrl;
                        Session["userId"] = st.ID;
                        return RedirectToAction("Details", "Login", new { id = st.ID });
                    }
                    else if (per.Role == "Accountant")
                    {
                        Accountant act = (Accountant)per;
                        Session["userName"] = act.FirstMidName;
                        Session["userImg"] = act.imgUrl;
                        Session["userId"] = act.ID;
                        return RedirectToAction("DetailsAccountant", "Login", new { id = act.ID });
                    }
                    else if (per.Role == "Instructor")
                    {
                        if (per.logCont == 0)
                        {
                            Instructor inid = (Instructor)per;
                            return RedirectToAction("changePasswordIns", "Login", new { id = inid.ID });

                        }
                        Instructor ins = (Instructor)per;
                        Session["userName"] = ins.FirstMidName;
                        Session["userImg"] = ins.imgUrl;
                        Session["userId"] = ins.ID;
                        return RedirectToAction("Details_Ins", "Login", new { id = ins.ID });
                    }
                    else if (per.Role == "Admin")
                    {
                        if (per.logCont == 0)
                        {
                            Admin adid = (Admin)per;
                            return RedirectToAction("changePasswordadmin", "Login", new { id = adid.ID });

                        }
                        Admin ad = (Admin)per;
                        Session["userName"] = ad.FirstMidName;
                        Session["userImg"] = ad.imgUrl;
                        return RedirectToAction("Index", "AdminView");
                    }
                    //else if (per.Role == "SuperAdmin")
                    //{
                    //    //SuperAdmin sad = (SuperAdmin)per;
                    //    //Session["userName"] = sad.Name;
                    //    //Session["userImg"] = sad.imgUrl;
                    //    return RedirectToAction("Index", "MesssageAdmin");
                    //}

                }
                else
                {
                    ViewBag.loginMessage = "Password Incorrect!";
                }

            }
            else
            {
                ViewBag.loginMessage = "User Not Found!";
            }


            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "ID,name,message,phone,email")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                db.contactus.Add(contactUs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactUs);
        }

        public ActionResult Gallary()
        {
            return View();
        }
    }
}