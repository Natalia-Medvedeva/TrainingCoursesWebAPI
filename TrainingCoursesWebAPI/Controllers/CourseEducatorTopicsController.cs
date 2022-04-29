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
    public class CourseEducatorTopicsController : ApiController
    {
        private TrainingCoursesEntities db = new TrainingCoursesEntities();

        // GET: api/CourseEducatorTopics
        public IQueryable<CourseEducatorTopic> GetCourseEducatorTopic()
        {
            return db.CourseEducatorTopic;
        }

        // GET: api/CourseEducatorTopics/5
        [ResponseType(typeof(CourseEducatorTopic))]
        public IHttpActionResult GetCourseEducatorTopic(int id)
        {
            CourseEducatorTopic courseEducatorTopic = db.CourseEducatorTopic.Find(id);
            if (courseEducatorTopic == null)
            {
                return NotFound();
            }

            return Ok(courseEducatorTopic);
        }

        [ResponseType(typeof(List<int>))]
        [Route("GetCourseEducatorTopicEducatorID")]
        public IHttpActionResult GetCourseEducatorTopicEducatorID(int courseID)
        {
            List<int> ids = db.CourseEducatorTopic.Where(x => x.IDCourse == courseID).Select(x => x.IDEducator).ToList();
            return Ok(ids);
        }

        [ResponseType(typeof(List<int>))]
        [Route("GetCourseEducatorTopicTopicID")]
        public IHttpActionResult GetCourseEducatorTopicTopicID(int courseID)
        {
            List<int> ids = db.CourseEducatorTopic.Where(x => x.IDCourse == courseID).Select(x => x.IDTopic).ToList();
            return Ok(ids);
        }

        [ResponseType(typeof(List<CourseEducatorTopic>))]
        [Route("GetCourseEducatorTopicCourseID")]
        public IHttpActionResult GetCourseEducatorTopicCourseID(int courseID)
        {
            List<CourseEducatorTopic> courseEducatorTopics = db.CourseEducatorTopic.Where(x => x.IDCourse == courseID).ToList();
            return Ok(courseEducatorTopics);
        }
        [ResponseType(typeof(List<SelectCourseEducatorTopicIDCourse_Result>))]
        [Route("GetSelectCourseEducatorTopicIDCourse_Result")]
        public IHttpActionResult GetSelectCourseEducatorTopicIDCourse_Result(int courseID)
        {
            List<SelectCourseEducatorTopicIDCourse_Result> courseEducatorTopics = db.SelectCourseEducatorTopicIDCourse(courseID).ToList();
            return Ok(courseEducatorTopics);
        }

        // PUT: api/CourseEducatorTopics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourseEducatorTopic(int id, CourseEducatorTopic courseEducatorTopic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courseEducatorTopic.IDCourse)
            {
                return BadRequest();
            }

            db.Entry(courseEducatorTopic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseEducatorTopicExists(id))
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

        // POST: api/CourseEducatorTopics
        [ResponseType(typeof(CourseEducatorTopic))]
        public IHttpActionResult PostCourseEducatorTopic(CourseEducatorTopic courseEducatorTopic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CourseEducatorTopic.Add(courseEducatorTopic);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CourseEducatorTopicExists(courseEducatorTopic.IDCourse))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = courseEducatorTopic.IDCourse }, courseEducatorTopic);
        }

        // DELETE: api/CourseEducatorTopics/5
        [ResponseType(typeof(CourseEducatorTopic))]
        public IHttpActionResult DeleteCourseEducatorTopic(int id)
        {
            CourseEducatorTopic courseEducatorTopic = db.CourseEducatorTopic.Find(id);
            if (courseEducatorTopic == null)
            {
                return NotFound();
            }

            db.CourseEducatorTopic.Remove(courseEducatorTopic);
            db.SaveChanges();

            return Ok(courseEducatorTopic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseEducatorTopicExists(int id)
        {
            return db.CourseEducatorTopic.Count(e => e.IDCourse == id) > 0;
        }
    }
}