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
using Project_API.Models;

namespace Project_API.Controllers
{
    [RoutePrefix("api/Ratings")]
    public class RatingController : ApiController
    {
        private ProjectDBEntities db = new ProjectDBEntities();
        private ProjectDBEntities context = new ProjectDBEntities();

        public RatingController()
        {
            // Add the following code
            // problem will be solved
            db.Configuration.ProxyCreationEnabled = false;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var list = context.Ratings.ToList();
            List<Rating> ratings = new List<Rating>();
            foreach (var item in list)
            {
                Rating obj = new Rating();
                obj.Book = null;
                obj.Book_Id = item.Book_Id;
                obj.Id = item.Id;
                obj.Review = item.Review;
                obj.Stars = item.Stars;
                obj.User = null;
                obj.User_Id = item.User_Id;

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings", Method = "GET", Rel = "Self" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/" + obj.Id, Method = "GET", Rel = "Specific Resource" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings", Method = "POST", Rel = "Resource Create" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/" + obj.Id + "/Books", Method = "GET", Rel = "All Books By Author" });


                ratings.Add(obj);
            }
            return Ok(ratings);
        }

        [Route("{id}", Name = "GetRatingById")]
        [ResponseType(typeof(Rating))]
        public IHttpActionResult Get([FromUri]int id)
        {
            var obj = db.Ratings.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings", Method = "GET", Rel = "All Resource" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/" + obj.Id, Method = "GET", Rel = "Self" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings", Method = "POST", Rel = "Resource Create" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/" + obj.Id + "/Books", Method = "GET", Rel = "All Books By Author" });

            return Ok(obj);
        }

        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating([FromUri]int id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.Id)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        [Route("")]
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ratings.Add(rating);
            db.SaveChanges();

            return CreatedAtRoute("GetRatingById", new { id = rating.Id }, rating);
        }

        [Route("{id}")]
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("Books/{bookId}/Users", Name = "GetReviewsForBook")]
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetReviewsForBook([FromUri]int bookId)
        {
            var list = context.Ratings.Where(m=>m.Book_Id == bookId).ToList();

            if (list == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            List<Rating> ratings = new List<Rating>();
            foreach (var item in list)
            {
                Rating obj = new Rating();
                obj.Book = null;
                obj.Book_Id = item.Book_Id;
                obj.Id = item.Id;
                obj.Review = item.Review;
                obj.Stars = item.Stars;
                obj.User = null;
                obj.User_Id = item.User_Id;
                
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/Books/"+ obj.Book_Id + "/Users", Method = "GET", Rel = "All Reviews For Book" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/Books/" + obj.Book_Id + "/Users" + obj.User_Id, Method = "GET", Rel = "All Reviews For Book By User" });

                ratings.Add(obj);
            }
            return Ok(ratings);
        }

        [Route("Books/{bookId}/Users/{userId}", Name = "GetReviewsForBookByUser")]
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetReviewsForBookByUser([FromUri]int bookId, [FromUri]int userId)
        {
            var obj = db.Ratings.Where(m=>m.Book_Id == bookId && m.User_Id == userId).FirstOrDefault();
            if (obj == null)
            {
                return NotFound();
            }

            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/Books/" + obj.Book_Id + "/Users", Method = "GET", Rel = "All Reviews For Book" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Ratings/Books/" + obj.Book_Id + "/Users/" + obj.User_Id, Method = "GET", Rel = "All Reviews For Book By User" });

            return Ok(obj);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int id)
        {
            return db.Ratings.Count(e => e.Id == id) > 0;
        }
    }
}