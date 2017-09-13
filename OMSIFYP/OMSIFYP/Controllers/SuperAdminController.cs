using OMSIFYP.DAL;
using OMSIFYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMSIFYP.ViewModels;
using System.Data.Entity;
using System.IO;
using PagedList;

namespace OMSIFYP.Controllers
{
    public class SuperAdminController : Controller
    {
        private SchoolContext db = new SchoolContext();
        // GET: SuperAdmin

            public ViewResult EmployeeList(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Role" ? "role_desc" : "Role";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var emp = from s in db.employe
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                emp = emp.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    emp = emp.OrderByDescending(s => s.FirstMidName);
                    break;
                case "Role":
                    emp = emp.OrderBy(s => s.Role);
                    break;
                case "role_desc":
                    emp = emp.OrderByDescending(s => s.Role);
                    break;
                default:  // Name ascending 
                    emp = emp.OrderBy(s => s.FirstMidName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(emp.ToPagedList(pageNumber, pageSize));






            //var list = from m in db.employe select m;

            //        return View(list);
        }

        public ActionResult Index( string searchName)
        {
            if (searchName != null)
            {


                var msgist = from m in db.Admin select m;
                msgist = msgist.Where(s => s.FirstMidName.ToUpper().Contains(searchName.ToUpper()) || s.LastName.ToUpper().Contains(searchName.ToUpper()));
                return View(msgist);

            }

            var viewModel = new AdminData();
            viewModel.Admins = db.Admin;
            return View(viewModel);
        }
        public ActionResult CreateAdmin()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult CreateAdmin(HttpPostedFileBase cv, HttpPostedFileBase file, Admin ad)
        {
           
            if (ModelState.IsValid) {
                ad.logCont = 0;
                ad.Role = "Admin";
                string _ImgFileName = Path.GetFileName(file.FileName);
                string _imgpath = Path.Combine(Server.MapPath("~/UploadedFiles/Admin"), _ImgFileName);
                file.SaveAs(_imgpath);
                ad.imgUrl = "/UploadedFiles/Admin/" + _ImgFileName;

                string _FileName = Path.GetFileName(cv.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles/Admin_CV"), _FileName);
                cv.SaveAs(_path);
                ad.cv = "/UploadedFiles/Admin_CV/" + _FileName;
                db.Admin.Add(ad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View();
        }

        public ActionResult Edit(int? id)
        {
            Admin ad = db.Admin.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }
        [HttpPost]
        public ActionResult Edit(Admin ad)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        public ActionResult Delete(int? id)
        {
            Admin ad = db.Admin.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            db.Admin.Remove(ad);
            db.SaveChanges();
            ViewBag.delete = "Admin Deleted Sucessfully!";
            return RedirectToAction("Index");
        }
    }
}