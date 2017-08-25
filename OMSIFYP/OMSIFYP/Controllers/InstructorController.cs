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
using OMSIFYP.ViewModels;
using System.Data.Entity.Infrastructure;
using System.IO;


namespace ContosoUniversity.Controllers
{
    
    public class InstructorController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult classList()
        {
          
            string userID = Session["userId"].ToString();
            int insID = Int32.Parse(userID);
            
            //    Instructor ins=db.Instructors.Find(userID);
            //string insEmail = ins.email;
           // GenrateClass gc = db.genrateClass.Find(insID);

           var res = from d in db.genrateClass select d;

            res = res.Where(e => e.InstructorID == insID);
            return View(res);
        }
        public ActionResult viewStudents(int id,string name, string section)
        {

            Session["classname"] = name;
            Session["classSection"] = section;

            var res = from m in db.enrollStudent select m;
            res = res.Where(e => e.GenrateClassID == id);
            return View(res);



        }
     



        // GET: Instructor
        public ActionResult Index(int? id, int? courseID, string searchName)
        {

            if (Session["userRole"].ToString() == "Admin")
            {

                var viewModel = new InstructorIndexData();

                viewModel.Instructors = db.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.Courses.Select(c => c.Department))
                    .OrderBy(i => i.FirstMidName);

                if (id != null)
                {
                    ViewBag.InstructorID = id.Value;
                    viewModel.Courses = viewModel.Instructors.Where(
                        i => i.ID == id.Value).Single().Courses;
                }

                if (courseID != null)
                {
                    ViewBag.CourseID = courseID.Value;
                    // Lazy loading
                    //viewModel.Enrollments = viewModel.Courses.Where(
                    //    x => x.CourseID == courseID).Single().Enrollments;
                    // Explicit loading
                    var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                    db.Entry(selectedCourse).Collection(x => x.Enrollments).Load();
                    foreach (Enrollment enrollment in selectedCourse.Enrollments)
                    {
                        db.Entry(enrollment).Reference(x => x.Student).Load();
                    }

                    viewModel.Enrollments = selectedCourse.Enrollments;
                }
                if (searchName != null)
                {
                    viewModel.Instructors = viewModel.Instructors.Where(ins => ins.FirstMidName.ToUpper().Contains(searchName.ToUpper()) || ins.LastName.ToUpper().Contains(searchName.ToUpper()));
                }
                return View(viewModel);

            }
            else
            {
               return RedirectToAction("login", "login");
            }

          
        }


        // GET: Instructor/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userRole"].ToString() == "Admin")
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);

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
                var instructor = new Instructor();
                instructor.Courses = new List<Course>();
                PopulateAssignedCourseData(instructor);
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");

            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, HttpPostedFileBase CV, Instructor instructor, string[] selectedCourses)
        {

            if (Session["userRole"].ToString() == "Admin")
            {

                if (selectedCourses != null)
                {
                    instructor.Courses = new List<Course>();
                    foreach (var course in selectedCourses)
                    {
                        var courseToAdd = db.Courses.Find(int.Parse(course));
                        instructor.Courses.Add(courseToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    instructor.Role = "Instructor";
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles/Instructor_Profile"), _FileName);

                    string _CVName = Path.GetFileName(CV.FileName);
                    string _pathCV = Path.Combine(Server.MapPath("~/UploadedFiles/CV"), _CVName);



                    file.SaveAs(_path);
                    CV.SaveAs(_pathCV);
                    instructor.imgUrl = "/UploadedFiles/Instructor_Profile/" + _FileName;
                    instructor.cv = "/UploadedFiles/CV/" + _CVName;

                    db.Instructors.Add(instructor);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Instructor");
                }
                PopulateAssignedCourseData(instructor);
                return View(instructor);

            }
            else
            {
                return RedirectToAction("login", "login");

            }

        }


        // GET: Instructor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userRole"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Instructor instructor = db.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.Courses)
                    .Where(i => i.ID == id)
                    .Single();
                PopulateAssignedCourseData(instructor);
                if (instructor == null)
                {
                    return HttpNotFound();
                }
                return View(instructor);

            }
            else
            {
                return RedirectToAction("login", "login");

            }



       
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            if (Session["userRole"].ToString() == "Admin")
            {
                var allCourses = db.Courses;
                var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseID));
                var viewModel = new List<AssignedCourseData>();
                foreach (var course in allCourses)
                {
                    viewModel.Add(new AssignedCourseData
                    {
                        CourseID = course.CourseID,
                        Title = course.Title,
                        Assigned = instructorCourses.Contains(course.CourseID)
                    });
                }
                ViewBag.Courses = viewModel;

            }
            

        }
        // POST: Instructor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, int? id, string[] selectedCourses, HttpPostedFileBase CV)
        {

            if (Session["userRole"].ToString() == "Admin")
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var instructorToUpdate = db.Instructors
                   .Include(i => i.OfficeAssignment)
                   .Include(i => i.Courses)
                   .Where(i => i.ID == id)
                   .Single();

                if (TryUpdateModel(instructorToUpdate, "",
                   new string[] { "LastName", "FirstMidName", "HireDate", "OfficeAssignment", "Adddress", "imgUrl", "father", "blood", "gender", "datebirth", "phone", "district", "nationality", "email", "password", "oldjob", "position", "cv", "qualificatin", "time", "orgaddress", "instit", "personcnic", "noper" }))
                {
                    try
                    {
                        if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                        {
                            instructorToUpdate.OfficeAssignment = null;
                        }

                        if (CV != null)
                        {
                            string _CVName = Path.GetFileName(CV.FileName);
                            string _pathCV = Path.Combine(Server.MapPath("~/UploadedFiles/CV"), _CVName);
                            CV.SaveAs(_pathCV);
                            instructorToUpdate.cv = "/UploadedFiles/CV/" + _CVName;



                        }

                        if (file != null)
                        {
                            string _FileName = Path.GetFileName(file.FileName);
                            string _path = Path.Combine(Server.MapPath("~/UploadedFiles/Instructor_Profile"), _FileName);

                            file.SaveAs(_path);

                            instructorToUpdate.imgUrl = "/UploadedFiles/Instructor_Profile/" + _FileName;
                        }


                        UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
                PopulateAssignedCourseData(instructorToUpdate);
                return View(instructorToUpdate);
            }
            else
            {
                return RedirectToAction("login", "login");

            }



        }
        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {


            if (Session["userRole"].ToString() == "Admin")
            {
                if (selectedCourses == null)
                {
                    instructorToUpdate.Courses = new List<Course>();
                    return;
                }

                var selectedCoursesHS = new HashSet<string>(selectedCourses);
                var instructorCourses = new HashSet<int>
                    (instructorToUpdate.Courses.Select(c => c.CourseID));
                foreach (var course in db.Courses)
                {
                    if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                    {
                        if (!instructorCourses.Contains(course.CourseID))
                        {
                            instructorToUpdate.Courses.Add(course);
                        }
                    }
                    else
                    {
                        if (instructorCourses.Contains(course.CourseID))
                        {
                            instructorToUpdate.Courses.Remove(course);
                        }
                    }
                }

            }
        
        }



        // GET: Instructor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userRole"].ToString() == "Admin")
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Instructor instructor = db.Instructors.Find(id);
                if (instructor == null)
                {
                    return HttpNotFound();
                }
                return View(instructor);

            }
            else
            {
                return RedirectToAction("login", "login");

            }



        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userRole"].ToString() == "Admin")
            {
                Instructor instructor = db.Instructors
          .Include(i => i.OfficeAssignment)
          .Where(i => i.ID == id)
          .Single();

                instructor.OfficeAssignment = null;
                db.Instructors.Remove(instructor);

                var department = db.Departments
                    .Where(d => d.InstructorID == id)
                    .SingleOrDefault();
                if (department != null)
                {
                    department.InstructorID = null;
                }

                db.SaveChanges();
                return RedirectToAction("Index");

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
