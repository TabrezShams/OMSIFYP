using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using OMSIFYP.DAL;
using OMSIFYP.Models;

namespace OMSIFYP.Controllers
{
    public class AdminContactsApiController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/AdminContactsApi
        public IQueryable<AdminContact> GetAdminContacts()
        {
            return db.AdminContacts;
        }

        // GET: api/AdminContactsApi/5
        [ResponseType(typeof(AdminContact))]
        public IHttpActionResult GetAdminContact(int id)
        {
            AdminContact adminContact = db.AdminContacts.Find(id);
            if (adminContact == null)
            {
                return NotFound();
            }

            return Ok(adminContact);
        }

        // PUT: api/AdminContactsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdminContact(int id, AdminContact adminContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adminContact.ID)
            {
                return BadRequest();
            }

            db.Entry(adminContact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminContactExists(id))
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

        // POST: api/AdminContactsApi
        [ResponseType(typeof(AdminContact))]
        public IHttpActionResult PostAdminContact(AdminContact adminContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdminContacts.Add(adminContact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = adminContact.ID }, adminContact);
        }

        // DELETE: api/AdminContactsApi/5
        [ResponseType(typeof(AdminContact))]
        public IHttpActionResult DeleteAdminContact(int id)
        {
            AdminContact adminContact = db.AdminContacts.Find(id);
            if (adminContact == null)
            {
                return NotFound();
            }

            db.AdminContacts.Remove(adminContact);
            db.SaveChanges();

            return Ok(adminContact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminContactExists(int id)
        {
            return db.AdminContacts.Count(e => e.ID == id) > 0;
        }
    }
}