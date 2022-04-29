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
using TrainingCoursesWebAPI.Entities;

namespace TrainingCoursesWebAPI.Controllers
{
    public class EducatorsController : ApiController
    {
        private TrainingCoursesEntities db = new TrainingCoursesEntities();

        // GET: api/Educators
        public IQueryable<Educator> GetEducator()
        {
            return db.Educator;
        }

        // GET: api/Educators/5
        [ResponseType(typeof(Educator))]
        public IHttpActionResult GetEducator(int id)
        {
            Educator educator = db.Educator.Find(id);
            if (educator == null)
            {
                return null;
            }
            return Ok(educator);
        }
        [Route("api/EducatorQ")]
        public IHttpActionResult GetEducators(int QualificationID)
        {
            List<Educator> educator = db.Educator.Where(x => x.IDQualification == QualificationID).ToList();
            return Ok(educator);
        }

        // PUT: api/Educators/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEducator(int id, Educator educator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != educator.EducatorID)
            {
                return BadRequest();
            }

            db.Entry(educator).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducatorExists(id))
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

        // POST: api/Educators
        [ResponseType(typeof(Educator))]
        public IHttpActionResult PostEducator(Educator educator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Educator.Add(educator);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = educator.EducatorID }, educator);
        }

        // DELETE: api/Educators/5
        [ResponseType(typeof(Educator))]
        public IHttpActionResult DeleteEducator(int id)
        {
            Educator educator = db.Educator.Find(id);
            if (educator == null)
            {
                return NotFound();
            }

            db.Educator.Remove(educator);
            db.SaveChanges();

            return Ok(educator);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EducatorExists(int id)
        {
            return db.Educator.Count(e => e.EducatorID == id) > 0;
        }
    }
}