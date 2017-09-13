using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OMSIFYP.DAL;
using OMSIFYP.Models;

namespace OMSIFYP.Controllers
{
    public class EnrollStudentsApiController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/EnrollStudentsApi
        public IQueryable<EnrollStudent> GetenrollStudent()
        {
            return db.enrollStudent;
        }

        // GET: api/EnrollStudentsApi/5
        [ResponseType(typeof(EnrollStudent))]
        public IHttpActionResult GetEnrollStudent(int id)
        {
            EnrollStudent enrollStudent = db.enrollStudent.Find(id);
            if (enrollStudent == null)
            {
                return NotFound();
            }

            return Ok(enrollStudent);
        }

        // PUT: api/EnrollStudentsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnrollStudent(int id, EnrollStudent enrollStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrollStudent.ID)
            {
                return BadRequest();
            }

            db.Entry(enrollStudent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollStudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EnrollStudentsApi
        [ResponseType(typeof(EnrollStudent))]
        public IHttpActionResult PostEnrollStudent(EnrollStudent enrollStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.enrollStudent.Add(enrollStudent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = enrollStudent.ID }, enrollStudent);
        }

        // DELETE: api/EnrollStudentsApi/5
        [ResponseType(typeof(EnrollStudent))]
        public IHttpActionResult DeleteEnrollStudent(int id)
        {
            EnrollStudent enrollStudent = db.enrollStudent.Find(id);
            if (enrollStudent == null)
            {
                return NotFound();
            }

            db.enrollStudent.Remove(enrollStudent);
            db.SaveChanges();

            return Ok(enrollStudent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnrollStudentExists(int id)
        {
            return db.enrollStudent.Count(e => e.ID == id) > 0;
        }
    }
}