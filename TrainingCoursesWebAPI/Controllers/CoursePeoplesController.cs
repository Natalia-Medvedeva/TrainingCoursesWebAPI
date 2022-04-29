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
    public class CoursePeoplesController : ApiController
    {
        private TrainingCoursesEntities db = new TrainingCoursesEntities();

        // GET: api/CoursePeoples
        public IQueryable<CoursePeople> GetCoursePeople()
        {
            return db.CoursePeople;
        }

        // GET: api/CoursePeoples/5
        [ResponseType(typeof(CoursePeople))]
        public IHttpActionResult GetCoursePeople(int id)
        {
            CoursePeople coursePeople = db.CoursePeople.Find(id);
            if (coursePeople == null)
            {
                return NotFound();
            }

            return Ok(coursePeople);
        }
        [ResponseType(typeof(List<int>))]
        public IHttpActionResult GetCoursePeopleIDPeople(int courseID)
        {
            List<int> ids = db.CoursePeople.Where(x => x.IDCourse == courseID).Select(x => x.IDPeople).ToList();
            return Ok(ids);
        }
        [ResponseType(typeof(List<int>))]
        public IHttpActionResult GetCoursePeopleIDCourse(int peopleID)
        {
            List<int> ids = db.CoursePeople.Where(x => x.IDPeople == peopleID).Select(x => x.IDCourse).ToList();
            return Ok(ids);
        }

        // PUT: api/CoursePeoples/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoursePeople(int id, CoursePeople coursePeople)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coursePeople.IDCourse)
            {
                return BadRequest();
            }

            db.Entry(coursePeople).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursePeopleExists(id))
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

        // POST: api/CoursePeoples
        [ResponseType(typeof(CoursePeople))]
        public IHttpActionResult PostCoursePeople(CoursePeople coursePeople)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoursePeople.Add(coursePeople);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CoursePeopleExists(coursePeople.IDCourse))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = coursePeople.IDCourse }, coursePeople);
        }

        // DELETE: api/CoursePeoples/5
        [ResponseType(typeof(CoursePeople))]
        public IHttpActionResult DeleteCoursePeople(int id)
        {
            CoursePeople coursePeople = db.CoursePeople.Find(id);
            if (coursePeople == null)
            {
                return NotFound();
            }

            db.CoursePeople.Remove(coursePeople);
            db.SaveChanges();

            return Ok(coursePeople);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoursePeopleExists(int id)
        {
            return db.CoursePeople.Count(e => e.IDCourse == id) > 0;
        }
    }
}