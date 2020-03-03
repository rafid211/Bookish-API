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
    [RoutePrefix("api/Authors")]
    public class AuthorController : ApiController
    {
        private ProjectDBEntities context = new ProjectDBEntities();
        private ProjectDBEntities db = new ProjectDBEntities();

        public AuthorController()
        {
            // Add the following code
            // problem will be solved
            db.Configuration.ProxyCreationEnabled = false;
        }

        [Route("")]
        public IHttpActionResult GetAuthors()
        {
            var list = context.Authors.ToList();
            List<Author> authors = new List<Author>();
            foreach (var item in list)
            {
                Author obj = new Author();
                obj.Books = null;
                obj.Description = item.Description;
                obj.Id = item.Id;
                obj.Image = item.Image;
                obj.Name = item.Name;

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors", Method = "GET", Rel = "Self" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Id, Method = "GET", Rel = "Specific Resource" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors", Method = "POST", Rel = "Resource Create" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Id + "/Books", Method = "GET", Rel = "All Books By Author" });


                authors.Add(obj);
            }
            return Ok(authors);
        }

        [Route("{id}", Name = "GetAuthorById")]
        [ResponseType(typeof(Author))]
        public IHttpActionResult GetAuthor([FromUri]int id)
        {
            Author obj = db.Authors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors", Method = "GET", Rel = "All Resource" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Id, Method = "GET", Rel = "Self" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors", Method = "POST", Rel = "Resource Create" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Id + "/Books", Method = "GET", Rel = "All Books By Author" });
            
            return Ok(obj);
        }


        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthor([FromUri]int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            db.Entry(author).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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
        [ResponseType(typeof(Author))]
        public IHttpActionResult PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Authors.Add(author);
            db.SaveChanges();

            return CreatedAtRoute("GetAuthorById", new { id = author.Id }, author);
        }

        [Route("{id}")]
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor([FromUri]int id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}/Books", Name = "GetBooksByAuthor")]
        [ResponseType(typeof(Author))]
        public IHttpActionResult GetBooksByAuthor([FromUri]int id)
        {
            var list = context.Books.Where(m => m.Author_Id == id).ToList();

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

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Author_Id + "/Books", Method = "GET", Rel = "All Books BY Author" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Author_Id + "/Books/" + obj.Id, Method = "GET", Rel = "Book By Author" });

                books.Add(obj);
            }
            return Ok(books);
        }

        [Route("{id}/Books/{bookId}", Name = "GetBookByAuthor")]
        [ResponseType(typeof(Author))]
        public IHttpActionResult GetBookByAuthor([FromUri]int id, [FromUri]int bookId)
        {
            var item = context.Books.Where(m => m.Author_Id == id && m.Id == bookId).FirstOrDefault();

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


            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Author_Id + "/Books", Method = "GET", Rel = "All Books BY Author" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Author_Id + "/Books/"+ obj.Id, Method = "GET", Rel = "Book By Author" });


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

        private bool AuthorExists(int id)
        {
            return db.Authors.Count(e => e.Id == id) > 0;
        }
    }
}