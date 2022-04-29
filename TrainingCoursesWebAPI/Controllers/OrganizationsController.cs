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
    public class OrganizationsController : ApiController
    {
        private TrainingCoursesEntities db = new TrainingCoursesEntities();

        // GET: api/Organizations
        [ResponseType(typeof(List<Organization>))]
        public IHttpActionResult GetOrganization()
        {
            return Ok(db.Organization.ToList());
        }

        // GET: api/Organizations/5
        [ResponseType(typeof(Organization))]
        public IHttpActionResult GetOrganization(int id)
        {
            Organization organization = db.Organization.Find(id);
            if (organization == null)
            {
                return NotFound();
            }

            return Ok(organization);
        }

        // PUT: api/Organizations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrganization(int id, Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organization.OrganizationID)
            {
                return BadRequest();
            }

            db.Entry(organization).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
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

        // POST: api/Organizations
        [ResponseType(typeof(Organization))]
        public IHttpActionResult PostOrganization(Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organization.Add(organization);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = organization.OrganizationID }, organization);
        }

        // DELETE: api/Organizations/5
        [ResponseType(typeof(Organization))]
        public IHttpActionResult DeleteOrganization(int id)
        {
            Organization organization = db.Organization.Find(id);
            if (organization == null)
            {
                return NotFound();
            }

            db.Organization.Remove(organization);
            db.SaveChanges();

            return Ok(organization);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationExists(int id)
        {
            return db.Organization.Count(e => e.OrganizationID == id) > 0;
        }
    }
}