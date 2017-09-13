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
    public class InstructorsApiController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/InstructorsApi
        public IQueryable<Instructor> GetPeople()
        {
            return db.Instructors;
        }

        // GET: api/InstructorsApi/5
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult GetInstructor(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return Ok(instructor);
        }

        // PUT: api/InstructorsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInstructor(int id, Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instructor.ID)
            {
                return BadRequest();
            }

            db.Entry(instructor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
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

        // POST: api/InstructorsApi
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult PostInstructor(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(instructor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = instructor.ID }, instructor);
        }

        // DELETE: api/InstructorsApi/5
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult DeleteInstructor(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            db.People.Remove(instructor);
            db.SaveChanges();

            return Ok(instructor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorExists(int id)
        {
            return db.People.Count(e => e.ID == id) > 0;
        }
    }
}