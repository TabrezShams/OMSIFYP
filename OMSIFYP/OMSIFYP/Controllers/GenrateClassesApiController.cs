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
    public class GenrateClassesApiController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/GenrateClassesApi
        public IQueryable<GenrateClass> GetgenrateClass()
        {
            return db.genrateClass;
        }

        // GET: api/GenrateClassesApi/5
        [ResponseType(typeof(GenrateClass))]
        public IHttpActionResult GetGenrateClass(int id)
        {
            GenrateClass genrateClass = db.genrateClass.Find(id);
            if (genrateClass == null)
            {
                return NotFound();
            }

            return Ok(genrateClass);
        }

        // PUT: api/GenrateClassesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGenrateClass(int id, GenrateClass genrateClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genrateClass.GenrateClassID)
            {
                return BadRequest();
            }

            db.Entry(genrateClass).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenrateClassExists(id))
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

        // POST: api/GenrateClassesApi
        [ResponseType(typeof(GenrateClass))]
        public IHttpActionResult PostGenrateClass(GenrateClass genrateClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.genrateClass.Add(genrateClass);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = genrateClass.GenrateClassID }, genrateClass);
        }

        // DELETE: api/GenrateClassesApi/5
        [ResponseType(typeof(GenrateClass))]
        public IHttpActionResult DeleteGenrateClass(int id)
        {
            GenrateClass genrateClass = db.genrateClass.Find(id);
            if (genrateClass == null)
            {
                return NotFound();
            }

            db.genrateClass.Remove(genrateClass);
            db.SaveChanges();

            return Ok(genrateClass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenrateClassExists(int id)
        {
            return db.genrateClass.Count(e => e.GenrateClassID == id) > 0;
        }
    }
}