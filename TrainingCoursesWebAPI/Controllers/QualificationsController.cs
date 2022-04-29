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
using TrainingCoursesWebAPI.Models;

namespace TrainingCoursesWebAPI.Controllers
{
    public class QualificationsController : ApiController
    {
        private TrainingCoursesEntities db = new TrainingCoursesEntities();

        // GET: api/Qualifications
        [ResponseType(typeof(List<Qualification>))]
        public IHttpActionResult GetQualification()
        {
            return Ok(db.Qualification.ToList());
        }

        // GET: api/Qualifications/5
        [ResponseType(typeof(Qualification))]
        public IHttpActionResult GetQualification(int id)
        {
            Qualification qualification = db.Qualification.Find(id);
            if (qualification == null)
            {
                return NotFound();
            }

            return Ok(qualification);
        }

        // PUT: api/Qualifications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQualification(int id, Qualification qualification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != qualification.QualificationID)
            {
                return BadRequest();
            }

            db.Entry(qualification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(id))
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

        // POST: api/Qualifications
        [ResponseType(typeof(Qualification))]
        public IHttpActionResult PostQualification(Qualification qualification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Qualification.Add(qualification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = qualification.QualificationID }, qualification);
        }

        // DELETE: api/Qualifications/5
        [ResponseType(typeof(Qualification))]
        public IHttpActionResult DeleteQualification(int id)
        {
            Qualification qualification = db.Qualification.Find(id);
            if (qualification == null)
            {
                return NotFound();
            }

            db.Qualification.Remove(qualification);
            db.SaveChanges();

            return Ok(qualification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QualificationExists(int id)
        {
            return db.Qualification.Count(e => e.QualificationID == id) > 0;
        }
    }
}