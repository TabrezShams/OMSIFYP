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
    public class AccountantsApiController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/AccountantsApi
        public IQueryable<Accountant> GetPeople()
        {
            return db.accountant;
        }

        // GET: api/AccountantsApi/5
        [ResponseType(typeof(Accountant))]
        public IHttpActionResult GetAccountant(int id)
        {
            Accountant accountant = db.accountant.Find(id);
            if (accountant == null)
            {
                return NotFound();
            }

            return Ok(accountant);
        }

        // PUT: api/AccountantsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccountant(int id, Accountant accountant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountant.ID)
            {
                return BadRequest();
            }

            db.Entry(accountant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountantExists(id))
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

        // POST: api/AccountantsApi
        [ResponseType(typeof(Accountant))]
        public IHttpActionResult PostAccountant(Accountant accountant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(accountant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accountant.ID }, accountant);
        }

        // DELETE: api/AccountantsApi/5
        [ResponseType(typeof(Accountant))]
        public IHttpActionResult DeleteAccountant(int id)
        {
            Accountant accountant = db.accountant.Find(id);
            if (accountant == null)
            {
                return NotFound();
            }

            db.People.Remove(accountant);
            db.SaveChanges();

            return Ok(accountant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountantExists(int id)
        {
            return db.People.Count(e => e.ID == id) > 0;
        }
    }
}