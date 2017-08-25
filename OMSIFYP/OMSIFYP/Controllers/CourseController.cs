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
using System.Data.Entity.Infrastructure;

namespace OMSIFYP.Controllers
{
    public class CourseController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Course
        public ActionResult Index(int? SelectedDepartment)
        {
            if (Session["userRole"].ToString() == "Admin")
            {


                var departments = db.Departments.OrderBy(q => q.Name).ToList();
                ViewBag.SelectedDepartment = new SelectList(departments, "DepartmentID", "Name", SelectedDepartment);
                int departmentID = SelectedDepartment.GetValueOrDefault();

                IQueryable<Course> courses = db.Courses
                    .Where(c => !SelectedDepartment.HasValue || c.DepartmentID == departmentID)
                    .OrderBy(d => d.CourseID)
                    .Include(d => d.Department);
                var sql = courses.ToString();
                return View(courses.ToList());

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userRole"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Course course = db.Courses.Find(id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return View(course);
            }
            else
            {
                return RedirectToAction("login", "login");
            }
          
        }


        public ActionResult Create()
        {
            if (Session["userRole"].ToString() == "Admin")
            {
                PopulateDepartmentsDropDownList();
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")]Course course)
        {

            if (Session["userRole"].ToString() == "Admin")
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Courses.Add(course);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                //catch (Exception ex /* dex */)
                //{
                //    //Log the error (uncomment dex variable name and add a line here to write a log.)
                //    ModelState.AddModelError("Primary key already exist", ex);
                //    return View();

                //}
                catch (Exception ex)
                {
                    var sqlException = ex.InnerException as System.Data.SqlClient.SqlException;

                    if (sqlException.Number == 2601 || sqlException.Number == 2627)
                    {
                        ViewBag.ErrorMessage = "Cannot insert duplicate values.";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error while saving data.";
                    }
                }
                PopulateDepartmentsDropDownList(course.DepartmentID);
                return View(course);
            }
            else
            {
                return RedirectToAction("login", "login");
            }

        }

        public ActionResult Edit(int? id)
        {

            if (Session["userRole"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Course course = db.Courses.Find(id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                PopulateDepartmentsDropDownList(course.DepartmentID);
                return View(course);
            }
            else
            {
                return RedirectToAction("login", "login");
            }

        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {

            if (Session["userRole"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var courseToUpdate = db.Courses.Find(id);
                if (TryUpdateModel(courseToUpdate, "",
                   new string[] { "Title", "Credits", "DepartmentID" }))
                {
                    try
                    {
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
                PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
                return View(courseToUpdate);
            }
            else
            {
                return RedirectToAction("login", "login");
            }

        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {

            if (Session["userRole"].ToString() == "Admin")
            {
                var departmentsQuery = from d in db.Departments
                                       orderby d.Name
                                       select d;
                ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);

            }
           
        }


        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {

            if (Session["userRole"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Course course = db.Courses.Find(id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return View(course);
            }
            else
            {
                return RedirectToAction("login", "login");
            }
      
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (Session["userRole"].ToString() == "Admin")
            {
                Course course = db.Courses.Find(id);
                db.Courses.Remove(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("login", "login");
            }
     
        }

        public ActionResult UpdateCourseCredits()
        {
            if (Session["userRole"].ToString() == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }

           
        }

        [HttpPost]
        public ActionResult UpdateCourseCredits(int? multiplier)
        {

            if (Session["userRole"].ToString() == "Admin")
            {
                if (multiplier != null)
                {
                    ViewBag.RowsAffected = db.Database.ExecuteSqlCommand("UPDATE Course SET Credits = Credits * {0}", multiplier);
                }
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
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
