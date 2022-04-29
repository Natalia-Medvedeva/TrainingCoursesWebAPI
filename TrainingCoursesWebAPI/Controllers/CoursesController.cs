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
    public class CoursesController : ApiController
    {
        private TrainingCoursesEntities db = new TrainingCoursesEntities();

        // GET: api/Courses
        public IQueryable<Course> GetCourse()
        {
            return db.Course;
        }

        // GET: api/Courses/5
        [ResponseType(typeof(List<Course>))]
        public IHttpActionResult GetCourse(int idOrganization)
        {
            List<Course> course = db.Course.Where(x => x.IDOrganization == idOrganization).ToList();
            if (course == null)
            {
                return null;
            }
            return Ok(course);
        }

        public IHttpActionResult GetCoursePeopleID(int idCourse)
        {
            Course course = db.Course.Find(idCourse);
            if (course == null)
            {
                return null;
            }
            return Ok(course);
        }
        [ResponseType(typeof(string))]
        [Route("GetSelectRegistrationNumber")]
        public IHttpActionResult GetSelectRegistrationNumber(int CourseID, int PeopleID)
        {
            return Ok(db.SelectRegistrationNumber(CourseID, PeopleID).ToList()[0].ToString());
        }

        [Route("GetSelectCourse")]
        public IHttpActionResult GetSelectCourse()
        {
            return Ok(db.SelectCourse());
        }

        [Route("GetSelectCourseCourseID")]
        [ResponseType(typeof(List<SelectCourseCourseID_Result>))]
        public IHttpActionResult GetSelectCourseCourseID(int idPeople)
        {
            List<int> ids = db.CoursePeople.Where(x => x.IDPeople == idPeople).Select(x => x.IDCourse).ToList();
            List<SelectCourseCourseID_Result> courses = new List<SelectCourseCourseID_Result> { };
            for (int i = 0; i < ids.Count; i++)
            {
                courses.Add(db.SelectCourseCourseID(ids[i]).ToList()[0]);
            }
            return Ok(courses);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourse(int id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.CourseID)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        [ResponseType(typeof(Course))]
        public IHttpActionResult PostCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Course.Add(course);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = course.CourseID }, course);
        }

        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult DeleteCourse(int id)
        {
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Course.Remove(course);
            db.SaveChanges();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Course.Count(e => e.CourseID == id) > 0;
        }
    }
}