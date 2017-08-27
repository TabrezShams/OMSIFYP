using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OMSIFYP.DAL;
using OMSIFYP.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.IO;

namespace OMSIFYP.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult allclass()
        {
            string userID = Session["userId"].ToString();
            int stID = Int32.Parse(userID);

 

            var res = from d in db.enrollStudent select d;

            res = res.Where(e => e.StudentID== stID);
            return View(res);



        }
        public ActionResult stumarks()
        {
            string userID = Session["userId"].ToString();
            int stID = Int32.Parse(userID);



            var res = from d in db.enrollStudent select d;

            res = res.Where(e => e.StudentID == stID);
            return View(res);



        }






        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["userRole"].ToString() == "Admin")
            {

                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var students = from s in db.Students
                               select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    students = students.Where(s => s.LastName.Contains(searchString)
                                           || s.FirstMidName.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        students = students.OrderByDescending(s => s.FirstMidName);
                        break;
                    case "Date":
                        students = students.OrderBy(s => s.EnrollmentDate);
                        break;
                    case "date_desc":
                        students = students.OrderByDescending(s => s.EnrollmentDate);
                        break;
                    default:  // Name ascending 
                        students = students.OrderBy(s => s.FirstMidName);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(students.ToPagedList(pageNumber, pageSize));
            }
            else
           {
               return View();
            }




        }


        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["userRole"].ToString() == "Admin")
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

               else
            {
                    return RedirectToAction("Login", "Login");
            }


           
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            if (Session["userRole"].ToString() == "Admin")
            {
                return View();
            }

            else
            {
                return RedirectToAction("Login", "Login");
            }

           
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, HttpPostedFileBase schoolcertificate, Student student)
        {

            if (Session["userRole"].ToString() == "Admin")
            {
                            try
            {
                if (ModelState.IsValid)
                {
                    student.Role = "Student";
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles/Student"), _FileName);
                    string _CVName = Path.GetFileName(schoolcertificate.FileName);
                    string _pathCV = Path.Combine(Server.MapPath("~/UploadedFiles/certificate"), _CVName);

                        file.SaveAs(_path);
                        schoolcertificate.SaveAs(_pathCV);
                        student.imgUrl = "/UploadedFiles/Student/" + _FileName;
                        student.schoolcertificate = "/UploadedFiles/certificate/" + _CVName;

                        db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
            }

            else
            {
                return RedirectToAction("Login", "Login");
            }

        }


        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userRole"].ToString() == "Admin")
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

            else
            {
                return RedirectToAction("Login", "Login");
            }




        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase file, HttpPostedFileBase schoolcertificate)
        {

            if (Session["userRole"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var studentToUpdate = db.Students.Find(id);
                if (TryUpdateModel(studentToUpdate, "",
                   new string[] { "LastName", "FirstMidName", "EnrollmentDate", "Adddress", "imgUrl", "father", "blood", "gender", "datebirth", "district", "nationality", "email", "password", "personcnic", "noper", "fathercnic", "oldschcity", "oldschaddress", "schoolcertificate", "oldclass", "oldschoolname", "studId" }))
                {
                    try
                    {

                        if (schoolcertificate != null)
                        {
                            string _CVName = Path.GetFileName(schoolcertificate.FileName);
                            string _pathCV = Path.Combine(Server.MapPath("~/UploadedFiles/certificate"), _CVName);
                            schoolcertificate.SaveAs(_pathCV);
                            studentToUpdate.schoolcertificate = "/UploadedFiles/certificate/" + _CVName;



                        }

                        if (file != null)
                        {
                            string _FileName = Path.GetFileName(file.FileName);
                            string _path = Path.Combine(Server.MapPath("~/UploadedFiles/Student"), _FileName);

                            file.SaveAs(_path);

                            studentToUpdate.imgUrl = "/UploadedFiles/Student/" + _FileName;
                        }


                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
                return View(studentToUpdate);

            }

            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

   


        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (Session["userRole"].ToString() == "Admin")
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (saveChangesError.GetValueOrDefault())
                {
                    ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
                }
                Student student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }

            else
            {
                return RedirectToAction("Login", "Login");
            }




        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (Session["userRole"].ToString() == "Admin")
            {
                try
                {
                    Student student = db.Students.Find(id);
                    db.Students.Remove(student);
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException/* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    return RedirectToAction("Delete", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("Login", "Login");
            }





            
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
