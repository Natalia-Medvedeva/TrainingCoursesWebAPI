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
    public class PeopleController : ApiController
    {
        private TrainingCoursesEntities db = new TrainingCoursesEntities();

        // GET: api/People
        public IQueryable<People> GetPeople()
        {
            return db.People;
        }

        // GET: api/People/5
        [ResponseType(typeof(People))]
        public IHttpActionResult GetPeople(int id)
        {
            People people = db.People.Find(id);
            if (people == null)
            {
                return null;
            }
            return Ok(people);
        }
        [ResponseType(typeof(List<People>))]
        [Route("GetPeopleCategoryID")]
        public IHttpActionResult GetPeopleCategoryID()
        {
            List<People> people = db.People.Where(x => x.IDCategory == 2).ToList();
            if (people == null)
            {
                return null;
            }
            return Ok(people);
        }
        [Route("api/GetPeopleLoginPassword")]
        public IHttpActionResult GetPeopleLoginPassword(string login, string password)
        {
            People people = db.People.FirstOrDefault(x => x.Login == login && x.Password == password);
            return Ok(people);
        }

        [Route("api/GetPeopleLogin")]
        public IHttpActionResult GetPeopleLogin(string login)
        {
            People people = db.People.FirstOrDefault(x => x.Login == login);
            return Ok(people);
        }

        public IHttpActionResult GetPeopleContains(List<int> ids)
        {
            List<People> people = db.People.Where(x => ids.Contains(x.PeopleID)).ToList();
            return Ok(people);
        }
        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPeople(int id, People people)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != people.PeopleID)
            {
                return BadRequest();
            }

            db.Entry(people).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleExists(id))
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

        // POST: api/People
        [ResponseType(typeof(People))]
        public IHttpActionResult PostPeople(People people)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(people);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = people.PeopleID }, people);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(People))]
        public IHttpActionResult DeletePeople(int id)
        {
            People people = db.People.Find(id);
            if (people == null)
            {
                return NotFound();
            }

            db.People.Remove(people);
            db.SaveChanges();

            return Ok(people);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PeopleExists(int id)
        {
            return db.People.Count(e => e.PeopleID == id) > 0;
        }
    }
}