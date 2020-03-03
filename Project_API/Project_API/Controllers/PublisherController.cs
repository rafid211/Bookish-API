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
    [RoutePrefix("api/Publishers")]
    public class PublisherController : ApiController
    {
        private ProjectDBEntities db = new ProjectDBEntities();
        private ProjectDBEntities context = new ProjectDBEntities();

        public PublisherController()
        {
            // Add the following code
            // problem will be solved
            db.Configuration.ProxyCreationEnabled = false;
        }

        [Route("")]
        public IHttpActionResult GetPublishers()
        {
            var list = context.Publishers.ToList();
            List<Publisher> authors = new List<Publisher>();
            foreach (var item in list)
            {
                Publisher obj = new Publisher();
                obj.Books = null;
                obj.Id = item.Id;
                obj.Image = item.Image;
                obj.Name = item.Name;

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers", Method = "GET", Rel = "Self" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Id, Method = "GET", Rel = "Specific Resource" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers", Method = "POST", Rel = "Resource Create" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Id + "/Books", Method = "GET", Rel = "All Books By Publisher" });

                authors.Add(obj);
            }
            return Ok(authors);
        }

        [Route("{id}", Name = "GetPublisherById")]
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult GetPublisher([FromUri]int id)
        {
            Publisher obj = db.Publishers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers", Method = "GET", Rel = "All Resources" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Id, Method = "GET", Rel = "Self" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers", Method = "POST", Rel = "Resource Create" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Id + "/Books", Method = "GET", Rel = "All Books By Publisher" });

            return Ok(obj);
        }


        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPublisher([FromUri]int id, [FromBody]Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publisher.Id)
            {
                return BadRequest();
            }

            db.Entry(publisher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult PostPublisher(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Publishers.Add(publisher);
            db.SaveChanges();

            return CreatedAtRoute("GetPublisherById", new { id = publisher.Id }, publisher);
        }

        [Route("{id}")]
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult DeletePublisher(int id)
        {
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return NotFound();
            }

            db.Publishers.Remove(publisher);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}/Books", Name = "GetBooksByPublisher")]
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult GetBooksByAuthor([FromUri]int id)
        {
            var list = context.Books.Where(m => m.Publisher_Id == id).ToList();

            if (list == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            List<Book> books = new List<Book>();
            foreach (var item in list)
            {
                Book obj = new Book();
                obj.Author_Id = item.Author_Id;
                obj.Category_Id = item.Category_Id;
                obj.Country = item.Country;
                obj.DiscountRate = item.DiscountRate;
                obj.Edition = item.Edition;
                obj.Id = item.Id;
                obj.Image = item.Image;
                obj.ISBN = item.ISBN;
                obj.Language = item.Language;
                obj.NumberOfPage = item.NumberOfPage;
                obj.Price = item.Price;
                obj.Publisher_Id = item.Publisher_Id;
                obj.Summary = item.Summary;
                obj.Title = item.Title;
                obj.Author = null;
                obj.Category = null;
                obj.FeaturedBooks = null;
                obj.OrderDatas = null;
                obj.Publisher = null;
                obj.Ratings = null;
                obj.Reports = null;
                obj.Stocks = null;

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Publisher_Id + "/Books", Method = "GET", Rel = "All Books By Publisher" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Publisher_Id + "/Books/" + obj.Id, Method = "GET", Rel = "Book By Publisher" });


                books.Add(obj);
            }
            return Ok(books);
        }

        [Route("{id}/Books/{bookId}", Name = "GetBookByPublisher")]
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult GetBookByAuthor([FromUri]int id, [FromUri]int bookId)
        {
            var item = context.Books.Where(m => m.Publisher_Id == id && m.Id == bookId).FirstOrDefault();

            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            Book obj = new Book();
            obj.Author_Id = item.Author_Id;
            obj.Category_Id = item.Category_Id;
            obj.Country = item.Country;
            obj.DiscountRate = item.DiscountRate;
            obj.Edition = item.Edition;
            obj.Id = item.Id;
            obj.Image = item.Image;
            obj.ISBN = item.ISBN;
            obj.Language = item.Language;
            obj.NumberOfPage = item.NumberOfPage;
            obj.Price = item.Price;
            obj.Publisher_Id = item.Publisher_Id;
            obj.Summary = item.Summary;
            obj.Title = item.Title;
            obj.Author = null;
            obj.Category = null;
            obj.FeaturedBooks = null;
            obj.OrderDatas = null;
            obj.Publisher = null;
            obj.Ratings = null;
            obj.Reports = null;
            obj.Stocks = null;


            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Publisher_Id + "/Books", Method = "GET", Rel = "All Books By Publisher" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Publishers/" + obj.Publisher_Id + "/Books/" + obj.Id, Method = "GET", Rel = "Book By Publisher" });


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

        private bool PublisherExists(int id)
        {
            return db.Publishers.Count(e => e.Id == id) > 0;
        }
    }
}