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
    public class PTMCallsApiController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/PTMCallsApi
        public IQueryable<PTMCalls> GetptmCall()
        {
            return db.ptmCall;
        }

        // GET: api/PTMCallsApi/5
        [ResponseType(typeof(PTMCalls))]
        public IHttpActionResult GetPTMCalls(int id)
        {
            PTMCalls pTMCalls = db.ptmCall.Find(id);
            if (pTMCalls == null)
            {
                return NotFound();
            }

            return Ok(pTMCalls);
        }

        // PUT: api/PTMCallsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPTMCalls(int id, PTMCalls pTMCalls)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pTMCalls.ID)
            {
                return BadRequest();
            }

            db.Entry(pTMCalls).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PTMCallsExists(id))
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

        // POST: api/PTMCallsApi
        [ResponseType(typeof(PTMCalls))]
        public IHttpActionResult PostPTMCalls(PTMCalls pTMCalls)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ptmCall.Add(pTMCalls);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pTMCalls.ID }, pTMCalls);
        }

        // DELETE: api/PTMCallsApi/5
        [ResponseType(typeof(PTMCalls))]
        public IHttpActionResult DeletePTMCalls(int id)
        {
            PTMCalls pTMCalls = db.ptmCall.Find(id);
            if (pTMCalls == null)
            {
                return NotFound();
            }

            db.ptmCall.Remove(pTMCalls);
            db.SaveChanges();

            return Ok(pTMCalls);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PTMCallsExists(int id)
        {
            return db.ptmCall.Count(e => e.ID == id) > 0;
        }
    }
}