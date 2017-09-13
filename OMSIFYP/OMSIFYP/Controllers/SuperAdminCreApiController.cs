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
    public class SuperAdminCreApiController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/SuperAdminCreApi
        public IQueryable<SuperAdminCre> Getsuperadmin()
        {
            return db.superadmin;
        }

        // GET: api/SuperAdminCreApi/5
        [ResponseType(typeof(SuperAdminCre))]
        public IHttpActionResult GetSuperAdminCre(int id)
        {
            SuperAdminCre superAdminCre = db.superadmin.Find(id);
            if (superAdminCre == null)
            {
                return NotFound();
            }

            return Ok(superAdminCre);
        }

        // PUT: api/SuperAdminCreApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSuperAdminCre(int id, SuperAdminCre superAdminCre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != superAdminCre.SuperAdminCreID)
            {
                return BadRequest();
            }

            db.Entry(superAdminCre).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperAdminCreExists(id))
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

        // POST: api/SuperAdminCreApi
        [ResponseType(typeof(SuperAdminCre))]
        public IHttpActionResult PostSuperAdminCre(SuperAdminCre superAdminCre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.superadmin.Add(superAdminCre);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = superAdminCre.SuperAdminCreID }, superAdminCre);
        }

        // DELETE: api/SuperAdminCreApi/5
        [ResponseType(typeof(SuperAdminCre))]
        public IHttpActionResult DeleteSuperAdminCre(int id)
        {
            SuperAdminCre superAdminCre = db.superadmin.Find(id);
            if (superAdminCre == null)
            {
                return NotFound();
            }

            db.superadmin.Remove(superAdminCre);
            db.SaveChanges();

            return Ok(superAdminCre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SuperAdminCreExists(int id)
        {
            return db.superadmin.Count(e => e.SuperAdminCreID == id) > 0;
        }
    }
}