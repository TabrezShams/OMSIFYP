using OMSIFYP.DAL;
using OMSIFYP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OMSIFYP.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private SchoolContext db=new SchoolContext();
        public string Name;
        public string Email; 
        public ActionResult Login()
        {
            return View();
        }
        //Instructor Password
        public ActionResult changePasswordIns(int id)
        {

            return View();
        }
        [HttpPost, ActionName("changePasswordIns")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPostIns(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var PersonToUpdate = db.Instructors.Find(id);
            if (TryUpdateModel(PersonToUpdate, "",
               new string[] { "password" }))
            {
                try
                {
                    PersonToUpdate.logCont = 1;
                    db.SaveChanges();

                    return RedirectToAction("Login", "Login");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(PersonToUpdate);



        }

        //Admin Password

        public ActionResult changePasswordadmin(int id)
        {
           Admin ad= db.Admin.Find(id);
            return View(ad);
        }
        [HttpPost, ActionName("changePasswordadmin")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPostAdmin(Admin ad)
        {
            ad.logCont = 1;
            db.Entry(ad).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("login");
        }



        //Student Password
        public ActionResult changePassword(int id)
        {
           
            return View();
        }
        [HttpPost, ActionName("changePassword")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {

           
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var PersonToUpdate = db.Students.Find(id);
                if (TryUpdateModel(PersonToUpdate, "",
                   new string[] { "password" }))
                {
                    try
                    {
                    PersonToUpdate.logCont = 1;
                        db.SaveChanges();

                        return RedirectToAction("Login","Login");
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
                return View(PersonToUpdate);

        

        }


        [HttpPost,ActionName("Login")]
        public ActionResult LoginConfirm(Login log) {
            //  var pers = from m in db.People select m;

            //   Person per = db.People.Find(log.id);
            Person per = db.People.FirstOrDefault(i => i.email == log.email);
            SuperAdminCre super = db.superadmin.FirstOrDefault(i => i.email == log.email);

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
            else if (super != null)
            {
                
                if (super.pass == log.password)
                {
                    Session["userEmail"] = log.email;
                    Session["userName"] = super.Name;
                    Session["userImg"] = super.imgUrl;
                    return RedirectToAction("Index", "SuperAdmin");


                }
            }
            else
            {
                ViewBag.loginMessage = "User Not Found!";
            }


            return View();
        }
        public ActionResult resetPassword(Student std)
        {
            
            return View(std);
        }
        [HttpPost,ActionName("resetPassword")]
        public ActionResult resetPasswordConfirm(Student std)
        {
            std.logCont = 1;
            db.Entry(std).State = System.Data.Entity.EntityState.Modified;
            
            db.SaveChanges();
            return View();
        }
        public string returnName()
        {
            return Name;
        }
        public string returnEmail()
        {
            return Email;
        }
        //Student Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        public ActionResult DetailsParent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        //Instructor Details
        public ActionResult Details_Ins(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor ins = db.Instructors.Find(id);
            if (ins == null)
            {
                return HttpNotFound();
            }
            return View(ins);
        }
        public ActionResult DetailsAccountant(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accountant ins = db.accountant.Find(id);
            if (ins == null)
            {
                return HttpNotFound();
            }
            return View(ins);
        }
    }
}